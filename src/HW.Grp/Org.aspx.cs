using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using S = HW.Core.Helpers.StrHelper;

namespace HW.Grp
{
	public partial class Org : System.Web.UI.Page
	{
		int deptID = 0;
		int showDepartmentID = 0;
		int deleteDepartmentID;
		int userID = 0;
		int deleteUserID = 0;
		int sendSponsorInvitationID = 0;
		int sponsorID = 0;
		bool showReg = false;
		string hiddenBqJoin = "";
		string hiddenBqWhere = "";
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		SponsorAdmin sponsorAdmin;
		int sponsorAdminID;
		IAdmin sponsor;
		SqlUserRepository userRepository = new SqlUserRepository();
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);


        /// <summary>
        /// Start here Page Load
        /// </summary>
        #region
        protected void Page_Load(object sender, EventArgs e)
		{
			SearchResultList.Text = "";
			SearchResults.Visible = false;
			ActionNav.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			sponsorID = Convert.ToInt32(Session["SponsorID"]);
			sponsor = sponsorRepository.ReadSponsor(sponsorID);
			sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"]);

			HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Organization), "default.aspx", true);

			var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			
			ReminderHelper.SetLanguageID(lid);
			LanguageFactory.SetCurrentCulture(lid);

			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Organization, DateTime.Now);
			string query = "";
			if (sponsorID != 0) {

				sponsorAdmin = sponsorRepository.ReadSponsorAdmin(sponsorID, sponsorAdminID);

				if (!IsPostBack) {
					StoppedReason.Items.Clear();
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.active", "Active"), "0"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.work", "Stopped, work related"), "1"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.education", "Stopped, education leave"), "2"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.parent", "Stopped, parental leave"), "14"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.sick", "Stopped, sick leave"), "24"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.not.participate", "Stopped, do not want to participate"), "34"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.not.associated", "Stopped, no longer associated"), "44"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.other", "Stopped, other reason"), "4"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.unknown", "Stopped, unknown reason"), "5"));
					StoppedReason.Items.Add(new ListItem(R.Str(lid, "status.stop.complete", "Stopped, project completed"), "6"));

					UserUpdateFrom.Items.Clear();
                    UserUpdateFrom.Items.Add(new ListItem(R.Str(lid, "user.update.onwards", "Update the user profile with these settings from today and onwards."), "1") { Selected = true });
					UserUpdateFrom.Items.Add(new ListItem(R.Str(lid, "user.update.start", "Update the user profile as if these settings were set from start."), "0"));
					UserUpdateFrom.Items.Add(new ListItem(R.Str(lid, "user.update.previous", "The previously registered email address has never been correct and the created account should be detached from organization."), "2"));

					DeleteUserFrom.Items.Clear();
                    DeleteUserFrom.Items.Add(new ListItem(R.Str(lid, "disassociate.today", "From today and onwards, disassociate this user with the organization."), "1") { Selected = true });
					DeleteUserFrom.Items.Add(new ListItem(R.Str(lid, "disassociate.start", "Disassociate this user with the organization from start."), "0"));
				}

				if (Request.QueryString["ShowReg"] != null && Convert.ToInt32(Session["ReadOnly"]) == 0) {
					showReg = true;
				} else if (Convert.ToInt32(Session["SeeUsers"]) == 1 && Convert.ToInt32(Session["ReadOnly"]) == 0) {
					showReg = true;
				}
				deptID = ConvertHelper.ToInt32(Request.QueryString["DID"]);
				deleteDepartmentID = ConvertHelper.ToInt32(Request.QueryString["DeleteDID"]);
				showDepartmentID = (Request.QueryString["SDID"] != null ? Convert.ToInt32(Request.QueryString["SDID"]) : 0);
				userID = (Request.QueryString["UID"] != null ? Convert.ToInt32(Request.QueryString["UID"]) : 0);
				deleteUserID = (Request.QueryString["DeleteUID"] != null ? Convert.ToInt32(Request.QueryString["DeleteUID"]) : 0);
				sendSponsorInvitationID = (Request.QueryString["SendSPIID"] != null ? Convert.ToInt32(Request.QueryString["SendSPIID"]) : 0);

				SqlDataReader rs;

				if (Request.QueryString["PESSIID"] != null && Request.QueryString["Flip"] != null) {
					query = string.Format(
						@"
UPDATE SponsorInvite SET PreviewExtendedSurveys = {0}
WHERE SponsorInviteID = {1}",
						Request.QueryString["Flip"] == "1" ? "1" : "NULL",
						Convert.ToInt32(Request.QueryString["PESSIID"])
					);
					Db.exec(query);
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["ConnectSPIID"] != null) {
					rs = Db.rs(string.Format("SELECT UserID, SponsorID FROM [User] WHERE UserID = {0}", Convert.ToInt32(Request.QueryString["WithUID"])));
					if (rs.Read()) {
						RewritePRU(rs.GetInt32(1), sponsorID, rs.GetInt32(0));
						Db.exec(string.Format("UPDATE SponsorInvite SET UserID = NULL WHERE UserID = {0}", rs.GetInt32(0)));
						Db.exec(string.Format("UPDATE SponsorInvite SET UserID = {0}, Sent = GETDATE() WHERE SponsorInviteID = {1}", rs.GetInt32(0), Convert.ToInt32(Request.QueryString["ConnectSPIID"])));
						Db.exec(string.Format("UPDATE [User] SET DepartmentID = {0}, SponsorID = {1} WHERE UserID = {2}", Convert.ToInt32(Request.QueryString["AndDID"]), sponsorID, rs.GetInt32(0)));
						Db.exec(string.Format("UPDATE UserProfile SET DepartmentID = {0}, SponsorID = {1} WHERE UserID = {2}", Convert.ToInt32(Request.QueryString["AndDID"]), sponsorID, rs.GetInt32(0)));
					}
					rs.Close();
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["ReclaimUID"] != null && Request.QueryString["ReclaimAID"] != null) {
					Db.exec(string.Format("UPDATE UserSponsorExtendedSurvey SET AnswerID = NULL WHERE ProjectRoundUserID = {0} AND AnswerID = {1}", Request.QueryString["ReclaimUID"], Request.QueryString["ReclaimAID"]));
					Db.exec(string.Format("UPDATE Answer SET EndDT = NULL WHERE ProjectRoundUserID = {0} AND AnswerID = {1}", Request.QueryString["ReclaimUID"], Request.QueryString["ReclaimAID"]), "eFormSqlConnection");
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["SubmitUID"] != null && Request.QueryString["SubmitAID"] != null) {
					Db.exec(string.Format("UPDATE UserSponsorExtendedSurvey SET AnswerID = {0} WHERE AnswerID IS NULL AND ProjectRoundUserID = {1}", Convert.ToInt32(Request.QueryString["SubmitAID"]), Convert.ToInt32(Request.QueryString["SubmitUID"])));
					Db.exec(string.Format("UPDATE Answer SET EndDT = GETDATE() WHERE ProjectRoundUserID = {0} AND AnswerID = {1}", Convert.ToInt32(Request.QueryString["SubmitUID"]), Convert.ToInt32(Request.QueryString["SubmitAID"])), "eFormSqlConnection");
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["BQID"] != null) {
					query = string.Format(
@"SELECT sib.BAID,
	sib.ValueInt,
	sib.ValueText,
	sib.ValueDate,
	bq.Type,
	up.UserProfileID
FROM SponsorInvite si
INNER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = {0}
INNER JOIN bq ON sib.BQID = bq.BQID
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
LEFT OUTER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = bq.BQID
WHERE upbq.UserBQID IS NULL
AND si.UserID = {1}
AND si.SponsorID = {2}",
						Convert.ToInt32(Request.QueryString["BQID"]),
						Convert.ToInt32(Request.QueryString["UID"]),
						sponsorID
					);
					rs = Db.rs(query);
					if (rs.Read()) {
						query = string.Format(
							@"
INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate)
VALUES ({0},{1},{2},{3},{4})",
							rs.GetInt32(5),
							Convert.ToInt32(Request.QueryString["BQID"]),
							(rs.IsDBNull(1) ? (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) : rs.GetInt32(1).ToString()),
							(rs.IsDBNull(2) ? "NULL" : "'" + rs.GetString(2).Replace("'", "") + "'"),
							(rs.IsDBNull(3) ? "NULL" : "'" + rs.GetDateTime(3).ToString("yyyy-MM-dd") + "'")
						);
						Db.exec(query);
					}
					rs.Close();
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["SendExtra"] != null) {
					#region Send extra
					query = string.Format(
						@"
SELECT ses.ExtraEmailBody,
	ses.ExtraEmailSubject,
	u.Email,
	u.UserID,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {0}
WHERE u.UserID = {1}",
						Convert.ToInt32(Request.QueryString["SESID"]),
						Convert.ToInt32(Request.QueryString["SendExtra"])
					);
					rs = Db.rs(query);
					if (rs.Read()) {
						string body = rs.GetString(0);

						string personalLink = "" + ConfigurationManager.AppSettings["healthWatchURL"] + "";
						if (!rs.IsDBNull(4) && rs.GetInt32(4) > 0) {
							personalLink += "c/" + rs.GetString(5).ToLower() + rs.GetInt32(3).ToString();
						}
						if (body.IndexOf("<LINK/>") >= 0) {
							body = body.Replace("<LINK/>", personalLink);
						} else {
							body += "\r\n\r\n" + personalLink;
						}

						Db.sendMail2(rs.GetString(2).Trim(), rs.GetString(1), body);
					}
					rs.Close();
					#endregion
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (sendSponsorInvitationID != 0) {
					#region Resend
//					query = string.Format(
//						@"
					//SELECT s.InviteTxt,
//	s.InviteSubject,
//	si.Email,
//	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8),
//	si.UserID,
//	u.ReminderLink,
//	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12),
//	s.LoginTxt,
//	s.LoginSubject
					//FROM Sponsor s
					//INNER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID
					//LEFT OUTER JOIN [User] u ON u.UserID = si.UserID
					//WHERE s.SponsorID = {0} AND si.SponsorInviteID = {1}",
//						sponsorID,
//						sendSponsorInvitationID
//					);
//					rs = Db.rs(query);
					var i = sponsorRepository.ReadSponsorInviteBySponsor(sendSponsorInvitationID, sponsorID);
//					if (rs.Read()) {
					if (i != null) {
//						if (rs.IsDBNull(4)) {
						if (i.User == null) {
							sponsorRepository.UpdateSponsorInviteSent(sendSponsorInvitationID);
//							Db.sendInvitation(sendSponsorInvitationID, rs.GetString(2).Trim(), rs.GetString(1), rs.GetString(0), rs.GetString(3));
							Db.sendInvitation(sendSponsorInvitationID, i.Email.Trim(), i.Sponsor.InviteSubject, i.Sponsor.InviteText, i.InvitationKey);
						} else {
//							string body = rs.GetString(7);
							string body = i.Sponsor.LoginText;

							string personalLink = "" + ConfigurationManager.AppSettings["healthWatchURL"] + "";
//							if (!rs.IsDBNull(5) && rs.GetInt32(5) > 0) {
							if (i.User.ReminderLink > 0) {
//								personalLink += "c/" + rs.GetString(6).ToLower() + rs.GetInt32(4).ToString();
								personalLink += "c/" + i.User.UserKey.ToLower() + i.User.Id.ToString();
							}
							if (body.IndexOf("<LINK/>") >= 0) {
								body = body.Replace("<LINK/>", personalLink);
							} else {
								body += "\r\n\r\n" + personalLink;
							}

//							Db.sendMail(rs.GetString(2).Trim(), rs.GetString(8), body);
							Db.sendMail2(i.Email.Trim(), i.Sponsor.LoginSubject, body);
						}
					}
//					rs.Close();
					#endregion
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}

				#region Populate hidden variables
				query = string.Format(
					@"
SELECT BQ.Internal,
	BQ.BQID,
	BQ.Type
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0}
AND sbq.Hidden = 1
ORDER BY sbq.SortOrder",
					sponsorID
				);
				rs = Db.rs(query);
				while (rs.Read()) {
					Hidden.Controls.Add(new LiteralControl("<span class='desc'>" + rs.GetString(0) + "</span>"));
					if (rs.GetInt32(2) == 7 || rs.GetInt32(2) == 1) {
						DropDownList rbl = new DropDownList();
						rbl.ID = "Hidden" + rs.GetInt32(1);
						rbl.Items.Add(new ListItem("-", "NULL"));
						query = string.Format("SELECT BAID, Internal FROM BA WHERE BQID = {0} ORDER BY SortOrder", rs.GetInt32(1));
						SqlDataReader rs2 = Db.rs(query);
						while (rs2.Read()) {
							rbl.Items.Add(new ListItem(rs2.GetString(1), rs2.GetInt32(0).ToString()));
						}
						rs2.Close();
						Hidden.Controls.Add(rbl);
					} else if (rs.GetInt32(2) == 4 || rs.GetInt32(2) == 2) {
						TextBox tb = new TextBox();
						tb.ID = "Hidden" + rs.GetInt32(1);
						tb.Width = Unit.Pixel(150);
						Hidden.Controls.Add(tb);
						if (rs.GetInt32(2) == 2) {
							hiddenBqJoin += "LEFT OUTER JOIN SponsorInviteBQ upb" + rs.GetInt32(1) + " ON si.SponsorInviteID = upb" + rs.GetInt32(1) + ".SponsorInviteID AND upb" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1) + " ";
							hiddenBqWhere += " OR upb" + rs.GetInt32(1) + ".ValueText LIKE [x]";
						}
					} else if (rs.GetInt32(2) == 3) {
						DropDownList ddl = new DropDownList();
						ddl.ID = "Hidden" + rs.GetInt32(1) + "Y";
						ddl.Items.Add(new ListItem("-", "0"));
						for (int i = 1900; i <= DateTime.Now.Year; i++) {
							ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
						}
						Hidden.Controls.Add(ddl);

						ddl = new DropDownList();
						ddl.ID = "Hidden" + rs.GetInt32(1) + "M";
						ddl.Items.Add(new ListItem("-", "0"));
						for (int i = 1; i <= 12; i++) {
							ddl.Items.Add(new ListItem(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[i - 1], i.ToString()));
						}
						Hidden.Controls.Add(ddl);

						ddl = new DropDownList();
						ddl.ID = "Hidden" + rs.GetInt32(1) + "D";
						ddl.Items.Add(new ListItem("-", "0"));
						for (int i = 1; i <= 31; i++) {
							ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
						}
						Hidden.Controls.Add(ddl);
					}
					Hidden.Controls.Add(new LiteralControl("<br/>"));
				}
				rs.Close();
				#endregion

				if (Request.QueryString["Action"] != null || deptID != 0 || userID != 0 || deleteUserID != 0) {
					if (Request.QueryString["Action"] != null && Request.QueryString["Action"] == "AddUnit" || deptID != 0) {
						AddUnit.Visible = true;
					}
					if (Request.QueryString["Action"] != null && Request.QueryString["Action"] == "AddUser" || userID != 0) {
						AddUser.Visible = true;
					}
					if (Request.QueryString["Action"] != null && Request.QueryString["Action"] == "ImportUser") {
						ImportUsers.Visible = true;
					}
					if (Request.QueryString["Action"] != null && Request.QueryString["Action"] == "ImportUnit") {
						ImportUnits.Visible = true;
					}
					Actions.Visible = false;
				}

				if (!IsPostBack) {
					//ImportUnitsParentDepartmentID.Items.Add(new ListItem("< top level >", "NULL"));
                    ImportUnitsParentDepartmentID.Items.Add(new ListItem(R.Str(lid, "department.toplevel", "< top level >"), "NULL"));
					//ImportUsersParentDepartmentID.Items.Add(new ListItem("< top level >", "NULL"));
					query = string.Format(
						@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
ORDER BY d.SortString",
						(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " AND " : "WHERE "),
						sponsorID
					);
					rs = Db.rs(query);
					while (rs.Read()) {
						ImportUnitsParentDepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
						ImportUsersParentDepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
					}
					rs.Close();

					//ParentDepartmentID.Items.Add(new ListItem("< top level >", "NULL"));
                    ParentDepartmentID.Items.Add(new ListItem(R.Str(lid, "department.toplevel", "< top level >"), "NULL"));
					if (deptID != 0) {
						string sortString = "";
						string parentDepartmentID = "NULL";
						string department = "", departmentShort = "";
						query = string.Format("SELECT d.SortString, d.ParentDepartmentID, d.Department, d.DepartmentShort FROM Department d WHERE d.SponsorID = {0} AND d.DepartmentID = {1}", sponsorID, deptID);
						rs = Db.rs(query);
						if (rs.Read()) {
							sortString = rs.GetString(0);
							if (!rs.IsDBNull(1)) {
								parentDepartmentID = rs.GetInt32(1).ToString();
							}
							department = rs.GetString(2);
							departmentShort = rs.GetString(3);
						}
						rs.Close();
						query = string.Format(
							@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
AND LEFT(d.SortString,{2}) <> '{3}'
ORDER BY d.SortString",
							(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " AND " : "WHERE "),
							sponsorID,
							sortString.Length,
							sortString
						);
						rs = Db.rs(query);
						while (rs.Read()) {
							ParentDepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
						}
						rs.Close();

						Department.Text = department;
						DepartmentShort.Text = departmentShort;
						ParentDepartmentID.SelectedValue = parentDepartmentID;
					} else {
						query = string.Format(
							@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
ORDER BY d.SortString",
							(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " AND " : "WHERE "),
							sponsorID
						);
						rs = Db.rs(query);
						while (rs.Read()) {
							ParentDepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
						}
						rs.Close();
					}
					query = string.Format(
						@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{0} d.SponsorID = {1}
ORDER BY d.SortString",
						(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " AND " : "WHERE "),
						sponsorID
					);
					rs = Db.rs(query);
					while (rs.Read()) {
						DepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
					}
					rs.Close();

					if (userID != 0) {
						query = string.Format("SELECT Email, DepartmentID, StoppedReason, Stopped FROM SponsorInvite WHERE SponsorInviteID = {0}", userID);
						rs = Db.rs(query);
						if (rs.Read()) {
							Email.Text = rs.GetString(0);
							DepartmentID.SelectedValue = (rs.IsDBNull(1) ? "NULL" : rs.GetInt32(1).ToString());
							StoppedReason.Items.FindByValue((rs.IsDBNull(2) ? "0" : rs.GetInt32(2).ToString())).Selected = true;
							Stopped.Text = (rs.IsDBNull(3) ? DateTime.Today.ToString("yyyy-MM-dd") : rs.GetDateTime(3).ToString("yyyy-MM-dd"));
						}
						rs.Close();
						query = string.Format("SELECT s.BQID, s.BAID, BQ.Type, s.ValueInt, s.ValueDate, s.ValueText, BQ.Restricted FROM SponsorInviteBQ s INNER JOIN BQ ON BQ.BQID = s.BQID WHERE s.SponsorInviteID = {0}", userID);
						rs = Db.rs(query);
						while (rs.Read()) {
							if (rs.GetInt32(2) == 7 || rs.GetInt32(2) == 1) {
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0)) != null && !rs.IsDBNull(1)) {
									((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue = rs.GetInt32(1).ToString();
								}
							} else if (rs.GetInt32(2) == 2) {
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0)) != null && !rs.IsDBNull(5)) {
									((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text = (rs.IsDBNull(6) ? rs.GetString(5) : "*****");
								}
							} else if (rs.GetInt32(2) == 4) {
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0)) != null && !rs.IsDBNull(3)) {
									((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text = rs.GetInt32(3).ToString();
								}
							} else if (rs.GetInt32(2) == 3 && !rs.IsDBNull(4)) {
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0) + "Y") != null) {
									((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "Y")).SelectedValue = rs.GetDateTime(4).ToString("yyyy");
								}
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0) + "M") != null) {
									((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "M")).SelectedValue = rs.GetDateTime(4).ToString("MM");
								}
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0) + "D") != null) {
									((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "D")).SelectedValue = rs.GetDateTime(4).ToString("dd");
								}
							}
						}
						rs.Close();
					}

					if (deleteUserID != 0) {
						query = string.Format("SELECT Email FROM SponsorInvite WHERE SponsorInviteID = {0}", deleteUserID);
						rs = Db.rs(query);
						if (rs.Read()) {
							DeleteUserEmail.Text = rs.GetString(0);
						}
						rs.Close();
					}

					if (deleteDepartmentID != 0) {
						query = string.Format("SELECT dbo.cf_departmentTree(d.DepartmentID,' » ') FROM Department d WHERE d.DepartmentID = {0}", deleteDepartmentID);
						rs = Db.rs(query);
						if (rs.Read()) {
							DeleteDepartmentName.Text = rs.GetString(0);
						}
						rs.Close();
					}
				}

				SaveUnit.Click += new EventHandler(SaveUnit_Click);
				SaveDeleteDepartment.Click += new EventHandler(SaveDeleteDepartment_Click);
				SaveUser.Click += new EventHandler(SaveUser_Click);
				SaveDeleteUser.Click += new EventHandler(SaveDeleteUser_Click);
				CancelUnit.Click += new EventHandler(Cancel_Click);
				CancelDeleteDepartment.Click += new EventHandler(Cancel_Click);
				CancelUser.Click += new EventHandler(Cancel_Click);
				CancelDeleteUser.Click += new EventHandler(Cancel_Click);
				Search.Click += new EventHandler(Search_Click);
				SaveImportUnit.Click += new EventHandler(SaveImportUnit_Click);
				CancelImportUnit.Click += new EventHandler(Cancel_Click);
				CancelImportUser.Click += new EventHandler(Cancel_Click);
				SaveImportUser.Click += new EventHandler(SaveImportUser_Click);
			} else {
				Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
		}
#endregion PageLoad



        /// <summary>
        /// Update Project Round User Function
        /// </summary>
        #region
        void RewritePRU(int fromSponsorID, int sponsorID, int userID)
		{
			string query = string.Format(
				@"
SELECT spru.ProjectRoundUnitID,
spru.SurveyID
FROM SponsorProjectRoundUnit spru
WHERE spru.SponsorID = {0}",
				sponsorID
			);
			SqlDataReader rs = Db.rs(query);
			while (rs.Read()) {
				query = string.Format(
					@"
SELECT upru.UserProjectRoundUserID,
	upru.ProjectRoundUserID
FROM UserProjectRoundUser upru
INNER JOIN [user] hu ON upru.UserID = hu.UserID
INNER JOIN [eform]..[ProjectRoundUser] pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID
INNER JOIN [eform]..[ProjectRoundUnit] u ON pru.ProjectRoundUnitID = u.ProjectRoundUnitID
WHERE hu.SponsorID = {0}
AND u.SurveyID = {1}
AND upru.UserID = {2}",
					fromSponsorID,
					rs.GetInt32(1),
					userID
				);
				SqlDataReader rs2 = Db.rs(query);
				while (rs2.Read()) {
					query = string.Format(
						@"
UPDATE UserProjectRoundUser SET ProjectRoundUnitID = {0}
WHERE UserProjectRoundUserID = {1}",
						rs.GetInt32(0),
						rs2.GetInt32(0)
					);
					Db.exec(query);
					query = string.Format(
						@"
UPDATE [eform]..[ProjectRoundUser] SET ProjectRoundUnitID = {0}
WHERE ProjectRoundUserID = {1}",
						rs.GetInt32(0),
						rs2.GetInt32(1)
					);
					Db.exec(query);
					query = string.Format(
						@"
UPDATE [eform]..[Answer] SET ProjectRoundUnitID = {0}
WHERE ProjectRoundUserID = {1}",
						rs.GetInt32(0),
						rs2.GetInt32(1)
					);
					Db.exec(query);
				}
				rs2.Close();
			}
			rs.Close();
		}

#endregion 





        /// <summary>
        /// Save Import User Click Function
        /// </summary>
        #region
        void SaveImportUser_Click(object sender, EventArgs e)
		{
			// 0 Email
			// 1 DepartmentShort
			// n Background
			// n+1 Stopped reason
			string query = "";
			if (UsersFilename.PostedFile != null && UsersFilename.PostedFile.ContentLength != 0) {
				System.IO.StreamReader f = new System.IO.StreamReader(UsersFilename.PostedFile.InputStream, System.Text.Encoding.Default);
				string s = f.ReadToEnd();
				f.Close();
				s = s.Replace("\r", "\n");
				s = s.Replace("\n\n", "\n");
				string[] sa = s.Split('\n');

				string units = "";
				bool valid = true;
				ImportUsersError.Text = "";
				SqlDataReader rs;

				System.Collections.ArrayList emails = new ArrayList();
				foreach (string a in sa) {
					//Response.Write(a + "<BR>");
					string email = a.Split('\t')[0].Replace("'", "").Trim().ToLower();
					if (email != "Email" && email != "") {
						if (!Db.isEmail(email)) {
							valid = false;
							ImportUsersError.Text += "Error: Invalid email-address \"" + email + "\"<BR/>";
						} else if (emails.Contains(email)) {
							valid = false;
							ImportUsersError.Text += "Error: Duplicate email-address \"" + email + "\"<BR/>";
						} else {
							emails.Add(email);
						}
						//rs = Db.rs("SELECT SponsorInviteID FROM SponsorInvite WHERE Email = '" + email + "' AND SponsorID = " + sponsorID);
						//if (rs.Read()) {
						//    valid = false;
						//    ImportUsersError.Text += "Error: Email-address \"" + email + "\" already exist<BR/>";
						//}
						//rs.Close();
						if (a.Split('\t').Length > 1) {
							string unitID = a.Split('\t')[1].Replace("'", "").ToLower().Trim();
							if (unitID != "" && ("," + units).IndexOf("," + unitID) < 0) {
								units += (units != "" ? "," : "") + unitID + "";
							}
						}
					}
				}
				//Response.End();
				System.Collections.Hashtable existingUnits = new System.Collections.Hashtable();
				query = string.Format("SELECT DepartmentShort, DepartmentID FROM Department WHERE DepartmentShort IS NOT NULL AND SponsorID = {0}", sponsorID);
				rs = Db.rs(query);
				while (rs.Read()) {
					existingUnits.Add(rs.GetString(0).ToLower().Trim(), rs.GetInt32(1));
//					if (!existingUnits.Contains(rs.GetString(0).ToLower().Trim())) {
//						existingUnits.Add(rs.GetString(0).ToLower().Trim(), rs.GetInt32(1));
//					}
				}
				rs.Close();

				foreach (string u in units.Split(',')) {
					//Response.Write(u + "<BR>");
					if (!existingUnits.Contains(u) && u != "") {
						valid = false;
						ImportUsersError.Text += "Error: Unit with ID \"" + u + "\" does not exist<BR/>";
					}
				}
				//Response.End();
				string extra = "", extraType = ""; int extraCount = 0;
				query = string.Format("SELECT s.BQID, b.Type FROM SponsorBQ s INNER JOIN BQ b ON s.BQID = b.BQID WHERE s.Hidden = 1 AND s.SponsorID = {0} ORDER BY s.SortOrder", sponsorID);
				rs = Db.rs(query);
				while (rs.Read()) {
					extraCount++;
					extra += (extra != "" ? "," : "") + rs.GetInt32(0).ToString();
					extraType += (extraType != "" ? "," : "") + rs.GetInt32(1).ToString();
				}
				rs.Close();

				if (valid) {
					foreach (string a in sa) {
						string[] u = a.Split('\t');
						string email = u[0].Replace("'", "").Trim();

						if (email != "Email" && email != "") {
							string unit = ImportUsersParentDepartmentID.SelectedValue.Replace("'", "");

							if (u.Length > 1) {
								string unitID = u[1].Replace("'", "").ToLower().Trim();
								if (unitID != "") {
									unit = existingUnits[unitID].ToString();
								}
							}

							int uid = 0, stoppedReason = 0;
							DateTime stopped = DateTime.MinValue;

							query = string.Format("SELECT SponsorInviteID, Stopped, StoppedReason FROM SponsorInvite WHERE Email = '{0}' AND SponsorID = {1}", email, sponsorID);
							rs = Db.rs(query);
							if (rs.Read()) {
								uid = rs.GetInt32(0);
								if (!rs.IsDBNull(1)) {
									stopped = rs.GetDateTime(1);
								}
								if (!rs.IsDBNull(2)) {
									stoppedReason = rs.GetInt32(2);
								}
							}
							rs.Close();

							if (u.Length > 2 + extraCount && u[2 + extraCount] != "") {
								if (stoppedReason != Convert.ToInt32(u[2 + extraCount])) {
									stoppedReason = Convert.ToInt32(u[2 + extraCount]);
									stopped = DateTime.Now;
								}
							}
							if (uid != 0) {
								Db.exec("UPDATE SponsorInvite SET DepartmentID = " + unit + ", Stopped = " + (stopped != DateTime.MinValue ? "'" + stopped.ToString("yyyy-MM-dd") + "'" : "NULL") + ", StoppedReason = " + (stoppedReason != 0 ? stoppedReason.ToString() : "NULL") + " WHERE SponsorInviteID = " + uid);

								query = string.Format("SELECT u.UserID FROM [User] u INNER JOIN SponsorInvite si ON u.UserID = si.UserID WHERE si.SponsorInviteID = " + uid);
								rs = Db.rs(query);
								while (rs.Read()) {
									query = string.Format(
										@"
UPDATE [User] SET DepartmentID = {0}
WHERE UserID = {1} AND SponsorID = {2}",
										unit,
										rs.GetInt32(0),
										sponsorID
									);
									Db.exec(query);
									query = string.Format(
										@"
UPDATE UserProfile SET DepartmentID = {0}
WHERE UserID = {1} AND SponsorID = {2}",
										unit,
										rs.GetInt32(0),
										sponsorID
									);
									Db.exec(query);
								}
								rs.Close();
							} else {
								query = string.Format(
									@"
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
INSERT INTO SponsorInvite (SponsorID,DepartmentID,Email,Stopped, StoppedReason )
VALUES ({0},{1},'{2}',{3},{4});
SELECT SponsorInviteID FROM [SponsorInvite] WHERE SponsorID={0} AND Email = '{2}' ORDER BY SponsorInviteID DESC;
COMMIT;",
									sponsorID,
									unit,
									email,
									(stopped != DateTime.MinValue ? "'" + stopped.ToString("yyyy-MM-dd") + "'" : "NULL"),
									(stoppedReason != 0 ? stoppedReason.ToString() : "NULL")
								);
								rs = Db.rs(query);
								if (rs.Read()) {
									uid = rs.GetInt32(0);
								}
								rs.Close();
							}
							string[] extras = extra.Split(',');
							string[] extraTypes = extraType.Split(',');
							//Response.Write(extra + "<BR/>");
							for (int i = 0; i < extraCount; i++) {
								//Response.Write(extras[i] + "<BR/>");
								//Response.Write(u[2 + i] + "<BR/>");
								if (u.Length > 2 + i && u[2 + i] != "" && extras[i].ToString() != "") {
									// Added after code sent to Ian, JPE 121214
									Db.exec(string.Format("UPDATE SponsorInviteBQ SET SponsorInviteID = -ABS(SponsorInviteID) WHERE SponsorInviteID = {0} AND BQID = {1}", uid, extras[i]));

									if (extraTypes[i] == "1" || extraTypes[i] == "7") {
										query = string.Format("SELECT BAID FROM BA WHERE BQID = {0} AND Value = {1}", extras[i], Convert.ToInt32(u[2 + i]));
										rs = Db.rs(query);
										if (rs.Read()) {
											//Response.Write(rs.GetInt32(0) + "<BR/>");
											query = string.Format(
												@"
INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,BAID)
VALUES ({0},{1},{2})",
												uid,
												extras[i],
												rs.GetInt32(0)
											);
											Db.exec(query);
										}
										rs.Close();
									} else if (extraTypes[i] == "2") {
										try {
											query = string.Format("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueText) VALUES (" + uid + "," + extras[i] + ",'" + u[2 + i].Replace("'", "''") + "')");
											Db.exec(query);
										} catch (Exception) { }
									} else if (extraTypes[i] == "4") {
										try {
											query = string.Format("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueInt) VALUES (" + uid + "," + extras[i] + "," + Convert.ToInt32(u[2 + i]) + ")");
											Db.exec(query);
										} catch (Exception) { }
									} else if (extraTypes[i] == "3") {
										try {
											query = string.Format("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueDate) VALUES (" + uid + "," + extras[i] + ",'" + Convert.ToDateTime(u[2 + i]) + "')");
											Db.exec(query);
										} catch (Exception) { }
									}
								}
							}
						}
					}
					//Response.End();
					Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
			}
		}
#endregion



        /// <summary>
        /// Save ImportUnit Click Function
        /// </summary>
        #region
        void SaveImportUnit_Click(object sender, EventArgs e)
		{
			string query = "";
			if (UnitsFilename.PostedFile != null && UnitsFilename.PostedFile.ContentLength != 0) {
				System.IO.StreamReader f = new System.IO.StreamReader(UnitsFilename.PostedFile.InputStream, System.Text.Encoding.Default);
				string s = f.ReadToEnd();
				f.Close();
				s = s.Replace("\r", "\n");
				s = s.Replace("\n\n", "\n");
				string[] sa = s.Split('\n');

				string units = "''", parentUnits = "''";
				bool valid = true;
				ImportUnitsError.Text = "";

				foreach (string a in sa) {
					string id = a.Split('\t')[0].Replace("'", "");
					if (id != "ID" && id != "") {
						string parentID = a.Split('\t')[1].Replace("'", "");
						units += ",'" + id + "'";
						if (parentID != "") {
							parentUnits += ",'" + parentID + "'";
						}
					}
				}

				// Check if any of the new IDs already exist
				query = string.Format("SELECT dbo.cf_DepartmentTree(DepartmentID,' » '), DepartmentShort FROM Department WHERE SponsorID = " + sponsorID + " AND DepartmentShort IN (" + units + ")");
				SqlDataReader rs = Db.rs(query);
				while (rs.Read()) {
					valid = false;
					ImportUnitsError.Text += "Error: Unit with ID \"" + rs.GetString(1) + "\" already exist (" + rs.GetString(0) + ")<BR/>";
				}
				rs.Close();

				// Add all present IDs
				query = string.Format("SELECT DepartmentShort FROM Department WHERE DepartmentShort IS NOT NULL AND SponsorID = {0}", sponsorID);
				rs = Db.rs(query);
				while (rs.Read()) {
					units += ",'" + rs.GetString(0).Replace("'", "") + "'";
				}
				rs.Close();

				// Check if any of the parent IDs can't be matched
				foreach (string p in parentUnits.Split(',')) {
					if (units.IndexOf(p) < 0) {
						valid = false;
						ImportUnitsError.Text += "Error: Unit with ID \"" + p + "\" specified as parent unit does not exist<BR/>";
					}
				}

				if (valid) {
					foreach (string a in sa) {
						string[] u = a.Split('\t');
						string id = u[0].Replace("'", "");

						if (id != "ID" && id != "") {
							string unit = u[2].Replace("'", "");

							// Insert new department
							query = string.Format(
								@"
SET NOCOUNT ON;
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRAN;
INSERT INTO Department (SponsorID, Department, DepartmentShort)
VALUES ({0}, '{1}', '{2}');
SELECT DepartmentID FROM [Department] WHERE SponsorID={0} AND DepartmentShort = '{2}' ORDER BY DepartmentID DESC;
COMMIT;",
								sponsorID,
								unit,
								id
							);
							rs = Db.rs(query);
							if (rs.Read()) {
								// Update sort order
								query = string.Format(
									@"
UPDATE Department SET SortOrder = {0} WHERE DepartmentID = {0}",
									rs.GetInt32(0)
								);
								Db.exec(query);

								if (Session["SponsorAdminID"].ToString() != "-1") {
									// Add to sponsor admin mapping
									query = string.Format(
										@"
INSERT INTO SponsorAdminDepartment (SponsorAdminID, DepartmentID)
VALUES ({0}, {1})",
										Session["SponsorAdminID"],
										rs.GetInt32(0)
									);
									Db.exec(query);
								}
							}
							rs.Close();
						}
					}
					foreach (string a in sa) {
						string[] u = a.Split('\t');
						string id = u[0].Replace("'", "");

						if (id != "ID" && id != "") {
							// Loop through all new departments
							query = string.Format("SELECT DepartmentID FROM Department WHERE DepartmentShort = '" + id + "' AND SponsorID = " + sponsorID);
							rs = Db.rs(query);
							if (rs.Read()) {
								string parentDepartmentID = ImportUnitsParentDepartmentID.SelectedValue.Replace("'", "");

								if (u[1] != "") {
									// Fetch parent department ID
									query = string.Format("SELECT DepartmentID FROM Department WHERE DepartmentShort = '" + u[1].Replace("'", "") + "' AND SponsorID = " + sponsorID);
									SqlDataReader rs2 = Db.rs(query);
									if (rs2.Read()) {
										parentDepartmentID = rs2.GetInt32(0).ToString();
									}
									rs2.Close();
								}

								if (parentDepartmentID != "NULL") {
									// Update new department with parent department
									query = string.Format("UPDATE Department SET ParentDepartmentID = " + parentDepartmentID + " WHERE DepartmentID = " + rs.GetInt32(0));
									Db.exec(query);
								}
							}
							rs.Close();
						}
					}

					query = string.Format("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = " + sponsorID);
					Db.exec(query);

					Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
			}
		}
#endregion 


        /// <summary>
        /// Save Delete Departmen Click
        /// </summary>
        #region
        void SaveDeleteDepartment_Click(object sender, EventArgs e)
		{
			string query = string.Format("SELECT ParentDepartmentID FROM Department WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			SqlDataReader rs = Db.rs(query);
			if (rs.Read()) {
				query = string.Format("UPDATE [User] SET DepartmentID = " + (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) + " WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
				Db.exec(query);
				query = string.Format("UPDATE UserProfile SET DepartmentID = " + (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) + " WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
				Db.exec(query);
				query = string.Format("UPDATE Department SET ParentDepartmentID = " + (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) + " WHERE SponsorID = " + sponsorID + " AND ParentDepartmentID = " + deleteDepartmentID);
				Db.exec(query);
			}
			rs.Close();
			query = string.Format("UPDATE Department SET SponsorID = -ABS(SponsorID) WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			Db.exec(query);
			query = string.Format("UPDATE SponsorAdminDepartment SET DepartmentID = -ABS(DepartmentID) WHERE DepartmentID = " + deleteDepartmentID);
			Db.exec(query);
			query = string.Format("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = " + sponsorID + "");
			Db.exec(query);

			Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}
        #endregion



        /// <summary>
        /// Save Delete User Click Function
        /// </summary>
        #region
        void SaveDeleteUser_Click(object sender, EventArgs e)
		{
			string query = string.Format("SELECT si.UserID FROM SponsorInvite si WHERE si.SponsorInviteID = " + deleteUserID);;
			SqlDataReader rs = Db.rs(query);
			if (rs.Read() && !rs.IsDBNull(0)) {
				if (DeleteUserFrom.SelectedValue == "0") {
					#region Update all
					query = string.Format("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);
					Db.exec(query);
					query = string.Format("UPDATE UserProfile SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);
					Db.exec(query);

					RewritePRU(sponsorID, 1, rs.GetInt32(0));

					#region Delete hidden variables - REMOVED
					
//					query = string.Format("SELECT UserProfileID FROM UserProfile WHERE UserID = " + rs.GetInt32(0));
//					SqlDataReader rs2 = Db.rs(query);
//					while (rs2.Read()) {
//						query = string.Format("SELECT BQ.BQID FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
//						SqlDataReader rs3 = Db.rs(query);
//						while (rs3.Read()) {
//							query = string.Format("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + rs2.GetInt32(0));
//							Db.exec(query);
//						}
//						rs3.Close();
//					}
//					rs2.Close();
					
					#endregion
					#endregion
				} else {
					// HOW ABOUT rewritePRU here?
					#region Update from now
					query = string.Format("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);
					Db.exec(query);

					query = string.Format("SELECT u.UserProfileID, up.ProfileComparisonID FROM [User] u INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID WHERE u.UserID = " + rs.GetInt32(0));
					SqlDataReader rs2 = Db.rs(query);
					while (rs2.Read()) {
						#region Create new profile
						query = string.Format(
							@"
INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created)
VALUES ({0},1,NULL,{1},GETDATE())",
							rs.GetInt32(0),
							rs2.IsDBNull(1) ? "NULL" : rs2.GetInt32(1).ToString()
						);
						// TODO: Investigate why ProfileComparisonID is NULL
						Db.exec(query);
						int profileID = 0;
						query = string.Format("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + rs.GetInt32(0) + " ORDER BY UserProfileID DESC");
						SqlDataReader rs3 = Db.rs(query);
						if (rs3.Read()) {
							profileID = rs3.GetInt32(0);
						}
						rs3.Close();
						#endregion

						#region Copy old profile
						query = string.Format("SELECT BQID, ValueInt, ValueText, ValueDate FROM UserProfileBQ WHERE UserProfileID = " + rs2.GetInt32(0));
						rs3 = Db.rs(query);
						while (rs3.Read()) {
							query = string.Format(
								"INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," +
								(rs3.IsDBNull(1) ? "NULL" : rs3.GetInt32(1).ToString()) + "," +
								(rs3.IsDBNull(2) ? "NULL" : "'" + rs3.GetString(2).Replace("'", "") + "'") + "," +
								(rs3.IsDBNull(3) ? "NULL" : "'" + rs3.GetDateTime(3).ToString("yyyy-MM-dd") + "'") +
								")"
							);
							Db.exec(query);
						}
						rs3.Close();
						#endregion

						#region Delete new hidden variables - REMOVED
						
//						rs3 = Db.rs("SELECT BQ.BQID FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
//						while (rs3.Read()) {
//							Db.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
//						}
//						rs3.Close();
						
						#endregion

						query = string.Format("UPDATE [User] SET UserProfileID = " + profileID + " WHERE UserID = " + rs.GetInt32(0));
						Db.exec(query);
					}
					rs2.Close();
					#endregion
				}
			}
			rs.Close();
			query = string.Format("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE SponsorInviteID = {0}", deleteUserID);
			Db.exec(query);
			Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}
        #endregion



        /// <summary>
        /// Modified OnPreRender page using GRP-WS for displaying Department Tree data.
        /// </summary>
        #region
        protected override void OnPreRender(EventArgs e)
        {
            Search.Text = R.Str(lid, "search", "Search");
            CancelUnit.Text = R.Str(lid, "cancel", "Cancel");
            SaveUnit.Text = R.Str(lid, "save", "Save");
            CancelImportUnit.Text = R.Str(lid, "cancel", "Cancel");
            SaveImportUnit.Text = R.Str(lid, "save", "Save");
            CancelUser.Text = R.Str(lid, "cancel", "Cancel");
            SaveUser.Text = R.Str(lid, "save", "Save");
            CancelImportUser.Text = R.Str(lid, "cancel", "Cancel");
            SaveImportUser.Text = R.Str(lid, "save", "Save");
            CancelDeleteUser.Text = R.Str(lid, "cancel", "Cancel");
            SaveDeleteUser.Text = R.Str(lid, "execute", "Execute");
            CancelDeleteDepartment.Text = R.Str(lid, "cancel", "Cancel");
            SaveDeleteDepartment.Text = R.Str(lid, "execute", "Execute");

            #region Normal org

            UserUpdate.Visible = (userID != 0);

            if (deleteUserID != 0)
            {
                DeleteUser.Visible = true;
                Actions.Visible = false;
            }

            if (deleteDepartmentID != 0)
            {
                DeleteDepartment.Visible = true;
                Actions.Visible = false;
            }
            
            OrgTree.Text = "";

            if (showDepartmentID != 0)
            {
                OrgTree.Text += string.Format(
                    @"
        <table border='0' cellspacing='0' cellpadding='0' style='font-size:12px;line-height:1.0;vertical-align:middle;'>
        	<tr style='border-bottom:1px solid #333333;'>
        		<td colspan='2'><b>{0}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{1}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{2}</b>&nbsp;</td>
        		<!--<td align='center' style='font-size:9px;'>&nbsp;<b>{3}</b>&nbsp;</td>-->
        		<!--{0}-->
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{4}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{5}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{6}</b>&nbsp;</td>
        		<!--{1}-->
        		<!--{2}-->
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{7}</b>&nbsp;</td>
        		<!--<td align='center' style='font-size:9px;'>&nbsp;<b>{8}</b>&nbsp;</td>-->
        	</tr>",
                    R.Str(lid, "unit.email", "Unit/Email"),
                    R.Str(lid, "action", "Action"),
                    R.Str(lid, "active", "Active"),
                    R.Str(lid, "active.activated", "Active/<br>Activated"),
                    R.Str(lid, "total", "Total"),
                    R.Str(lid, "invitation.received", "Received&nbsp;<br/>&nbsp;inivtation"),
                    R.Str(lid, "invite.first", "1st invite&nbsp;<br/>&nbsp;sent"),
                    R.Str(lid, "unit.id", "Unit ID&nbsp;<br/>&nbsp;User status"),
                    R.Str(lid, "reminder.text", "Reminder")
                );
            }
            else
            {
                OrgTree.Text += string.Format(
                    @"
        <table border='0' cellspacing='0' cellpadding='0' style='font-size:12px;line-height:1.0;vertical-align:middle;'>
        	<tr style='border-bottom:1px solid #333333;'>
        		<td colspan='2'><b>{0}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{1}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{2}</b>&nbsp;</td>
        		<!--<td align='center' style='font-size:9px;'>&nbsp;<b>Active/<br>Activated</b>&nbsp;</td>-->
        		<!--{0}-->
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{3}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{4}</b>&nbsp;</td>
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{7}</b>&nbsp;</td>
        		<!--{1}-->
        		<td align='center' style='font-size:9px;'>&nbsp;<b>{5}</b>&nbsp;</td>
                <!--<td align='center' style='font-size:9px;'>&nbsp;<b>{6}</b>&nbsp;</td>-->
        	</tr>",
                    R.Str(lid, "unit", "Unit"),
                    R.Str(lid, "action", "Action"),
                    R.Str(lid, "active", "Active"),
                    R.Str(lid, "total", "Total"),
                    R.Str(lid, "invitation.received", "Received&nbsp;<br/>&nbsp;inivtation"),
                    R.Str(lid, "unit.id", "Unit ID"),
                    R.Str(lid, "reminder.text", "Reminder"),
                    R.Str(lid, "invite.first", "1st invite&nbsp;<br/>&nbsp;sent")
                );
            }
            OrgTree.Text += string.Format("<tr><td colspan='3'>{0}</td>[xxx]</tr>", Session["Sponsor"]);

            int UX = 0; int totalActivated = 0; int IX = 0; int totalActive = 0;
            int MIN_SHOW = sponsor.MinUserCountToDisclose;

            Dictionary<int, bool> DX = new Dictionary<int, bool>();

            /// <summary>
            /// Initialize GRP-WS and access GetDepartmentTree WebMethod for the Department Tree data.
            /// </summary>
            var soapService = new HW.Grp.WebService.Soap();
            var soapResponse = soapService.GetDepartmentTree(Session["Token"].ToString(), 20);
            if (soapResponse.SponsorID != 0 && soapResponse.Departments.Count > 0)
            {
                UX = soapResponse.TotalCount;
                totalActivated = soapResponse.Departments[0].TotalActive;
                IX = Convert.ToInt32(soapResponse.TotalCountReceiveInvitation.Replace("%", ""));
                /// <summary>
                /// Populate Department Tree from GRP-WS return.
                /// </summary>
                for (int listCount = 0; listCount < soapResponse.Departments.Count; listCount++)
                {
                    int depth = soapResponse.Departments[listCount].Depth;
                    DX[depth] = (soapResponse.Departments[listCount].DepthX > 0);

                    /// <summary>
                    /// For Unit Column
                    /// </summary>
                    OrgTree.Text += string.Format(
                        @"
        <tr{0}>
        	<td colspan='2'>
        		<table border='0' cellspacing='0' cellpadding='0'>
        			<tr>
        				<td>",
                        (depth == 1 || depth == 4 ? " style='background-color:#EEEEEE'" : (depth == 2 || depth == 5 ? " style='background-color:#F6F6F6'" : ""))
                    );
                    for (int i = 1; i <= depth; i++)
                    {
                        if (!DX.ContainsKey(i))
                        {
                            DX[i] = false;
                        }
                        OrgTree.Text += string.Format(
                            @"
        					<img src='assets/theme1/img/{0}.gif' width='19' height='20'/>",
                            (i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null"))
                        );
                    }
                    string s = soapResponse.Departments[listCount].DepartmentName; 
                    if (s.Length > 30)
                    {
                        int i0 = s.Length / 2;
                        string s1 = s.Substring(i0);
                        int i1 = s1.IndexOf(" ");
                        if (i1 >= 0)
                        {
                            s = s.Substring(0, i0);
                            s += s1.Substring(0, i1);
                            s += "<br/>&nbsp;";
                            s += s1.Substring(i1);
                        }
                    }
                    ///<summary>
                    ///For Action
                    ///</summary>
                    OrgTree.Text += string.Format(
                        @"
        				</td>
        				<td style='vertical-align:middle;{0}'>&nbsp;{1}{2}{3}&nbsp;</td>
        			</tr>
        		</table>
        	</td>
        	<td align='center'>{4}{5}</td>",
                        (s.Length > 20 ? "font-size:10px;" : ""),
                        (""),
                        s,
                        (""),
                        (
                            Convert.ToInt32(Session["ReadOnly"]) == 0
                            ? "<a href='org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DID=" + soapResponse.Departments[listCount].DepartmentId + "'><img src='assets/theme1/img/unt_edt.gif' border='0'/></A>" + ""
                            : ""
                        ),
                        (
                            soapResponse.Departments[listCount].Total > 0 /*rs.GetInt32(3) > 0*/
                            ? "<a href='org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&SDID=" + soapResponse.Departments[listCount].DepartmentId + "'><img src='assets/theme1/img/usr_on.gif' border='0'/></A>"
                            : (
                                Convert.ToInt32(Session["ReadOnly"]) == 0
                                ? "<a href='org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DeleteDID=" + soapResponse.Departments[listCount].DepartmentId + "'><img src='assets/theme1/img/unt_del.gif' border='0'/></a>"
                                : ""
                            )
                        )
                    );
                    /// <summary>
                    /// Active Column in Department Tree
                    /// </summary>
                    OrgTree.Text += string.Format(
                        @"
            <td align='center'>&nbsp;{0}&nbsp;</td>",
                        (soapResponse.Departments[listCount].Active == 0 ? "<img src = 'assets/theme1/img/key.gif'/>" : soapResponse.Departments[listCount].Active.ToString())
                        );
                    /// <summary>
                    /// Total Column in Department Tree
                    /// </summary>
                    OrgTree.Text += string.Format(
                        @"
            <td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>",
                        soapResponse.Departments[listCount].SubTotal + (soapResponse.Departments[listCount].Total <= 0 ? "" : " (" + soapResponse.Departments[listCount].Total.ToString() + ")")
                    );
                    /// <summary>
                    /// Received Invitation Column in Department Tree
                    /// </summary>
                    OrgTree.Text += string.Format(
                        @"
            <td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>",
                        soapResponse.Departments[listCount].ReceiveInvitation.ToString().Equals("") ? "-" : soapResponse.Departments[listCount].ReceiveInvitation.ToString()
                    );
                    /// <summary>
                    /// 1st Invite Column in Department Tree
                    /// </summary>
                    OrgTree.Text += string.Format(
                        @"
        	<td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>",
                        soapResponse.Departments[listCount].FirstInviteSent.ToString().Equals("") ? "N/A" : soapResponse.Departments[listCount].FirstInviteSent.ToString()
                    );
                    /// <summary>
                    /// Unit ID Column in Department Tree
                    /// </summary>
                    OrgTree.Text += string.Format(
                        @"
        	<td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>",
                        (soapResponse.Departments[listCount].DepartmentShort.ToString().Equals("") ? "N/A" : soapResponse.Departments[listCount].DepartmentShort.ToString())
                    );
                    OrgTree.Text += @"
        </tr>";
                }
            }

            OrgTree.Text += string.Format(
                @"
        <tr>
        	<td colspan='{0}' style='border-top:1px solid #333333'>&nbsp;</td>
        </tr>",
                (9)
            );
            string header = string.Format(@"
        	<td align='center' style='font-size:9px;'>{0}</td>", (totalActive >= MIN_SHOW ? totalActive.ToString() : "<img src='assets/theme1/img/key.gif'/>"));
            header += string.Format(
                @"
        	<td align='center' style='font-size:9px;'>{0}</td>
        	<td align='center' style='font-size:9px;'>&nbsp;{1}%&nbsp;</td>
        	<td align='center' style='font-size:9px;'>&nbsp;</td>",
                UX,
                IX.ToString()
            );

            OrgTree.Text = OrgTree.Text.Replace("[xxx]", header) + "</table>";
            #endregion
        }
        #endregion End here for modification




        /// <summary>
        /// Search User Click Function
        /// </summary>
        #region
        void Search_Click(object sender, EventArgs e)
		{
			if (SearchEmail.Text != "") {
				bool found = false;

				string q = string.Format(
                    
@"SELECT si.SponsorInviteID,
si.DepartmentID,
dbo.cf_departmentTree(si.DepartmentID,' » ') + ' » ' + si.Email
FROM SponsorInvite si
{0}
{1}
si.SponsorID = {2}
AND (si.Email LIKE '%{3}%'{4})",
hiddenBqJoin,
(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " AND " : "WHERE "),
sponsorID,
SearchEmail.Text.Replace("'", ""),
hiddenBqWhere.Replace("[x]", "'%" + SearchEmail.Text.Replace("'", "") + "%'")
				);
				SqlDataReader rs = Db.rs(q);
				while (rs.Read()) {
					found = true;
					SearchResultList.Text += "<tr><td>" + (rs.IsDBNull(1) ? "Error, please contact <a href='mailto:support@healthwatch.se'>support@healthwatch.se</a>" : "<A HREF='org.aspx?SDID=" + rs.GetInt32(1) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&UID=" + rs.GetInt32(0).ToString() + "'>" + rs.GetString(2) + "</a>") + "</td></tr>";
				}
				rs.Close();

				if (!found) {
					SearchResultList.Text += string.Format("<tr><td><b>{0}</b></td></tr>", R.Str(lid, "match.no", "The search yielded no results!"));
				}
				SearchResults.Visible = true;
			}
		}
        #endregion


        /// <summary>
        ///  Save User Click function
        /// </summary>
        #region

        void SaveUser_Click(object sender, EventArgs e)
		{
			string url = "";
			SqlDataReader rs;
			bool exists = false;
			int stoppedReason = Convert.ToInt32(StoppedReason.SelectedValue);

			string query = "";
			if (userID == 0) {
				#region Create new sponsor invite, if email not already in org
				query = string.Format(
					@"
SELECT UserID
FROM SponsorInvite
WHERE SponsorID = {0} AND Email = '{1}'",
					sponsorID,
					Email.Text.Replace("'", "''").Trim()
				);
				rs = Db.rs(query);
				if (rs.Read()) {
					exists = true;
				}
				rs.Close();
				if (!exists) {
					query = string.Format(
						@"
INSERT INTO SponsorInvite (SponsorID,DepartmentID,Email,StoppedReason,Stopped)
VALUES ({0},{1},'{2}',{3},{4})",
						sponsorID,
						DepartmentID.SelectedValue,
						Email.Text.Replace("'", "''").Trim(),
						(stoppedReason == 0 ? "NULL" : stoppedReason.ToString()),
						(stoppedReason == 0 ? "NULL" : "GETDATE()")
					);
					Db.exec(query);
					query = string.Format(
						@"
SELECT TOP 1 SponsorInviteID
FROM SponsorInvite
WHERE SponsorID = {0}
ORDER BY SponsorInviteID DESC",
						sponsorID
					);
					rs = Db.rs(query);
					if (rs.Read()) {
						userID = rs.GetInt32(0);
					}
					rs.Close();
				}
				#endregion
			} else {
				query = string.Format(
					@"
SELECT UserID,
	Email
FROM SponsorInvite
WHERE SponsorInviteID <> {0} AND SponsorID = {1} AND Email = '{2}'",
					userID,
					sponsorID,
					Email.Text.Replace("'", "''")
				);
				rs = Db.rs(query);
				if (rs.Read()) {
					exists = true;
				}
				rs.Close();

				if (!exists) {
					int oldStoppedReason = 0;

					#region Update
					string sql = "";
					query = string.Format(
						@"
SELECT si.Email,
	si.UserID,
	si.StoppedReason
FROM SponsorInvite si
WHERE si.SponsorInviteID = {0}",
						userID
					);
					rs = Db.rs(query);
					if (rs.Read()) {
						oldStoppedReason = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
						if (rs.IsDBNull(1)) {
							#region not activated
							if (rs.GetString(0) != Email.Text) {
								sql += ", Sent = NULL, InvitationKey = NEWID() ";
							}
							#endregion
						} else {
							if (UserUpdateFrom.SelectedValue == "2") {
								sql += ", Sent = NULL, InvitationKey = NEWID(), UserID = NULL ";

								#region Remove
								query = string.Format(
									@"
UPDATE [User] SET DepartmentID = NULL,
	SponsorID = 1
WHERE UserID = {0} AND SponsorID = {1}",
									rs.GetInt32(1),
									sponsorID
								);
								Db.exec(query);
								query = string.Format(
									@"
UPDATE UserProfile SET DepartmentID = NULL,
	SponsorID = 1
WHERE UserID = {0} AND SponsorID = {1}",
									rs.GetInt32(1),
									sponsorID
								);
								Db.exec(query);

								query = string.Format(
									@"
SELECT UserProfileID
FROM UserProfile WHERE UserID = {0}",
									rs.GetInt32(1)
								);
								SqlDataReader rs2 = Db.rs(query);
								while (rs2.Read()) {
									query = string.Format(
										@"
SELECT BQ.BQID
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = {0} AND sbq.Hidden = 1",
										sponsorID
									);
									SqlDataReader rs3 = Db.rs(query);
									while (rs3.Read()) {
										query = string.Format(
											@"
DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}",
											rs3.GetInt32(0),
											rs2.GetInt32(0)
										);
										Db.exec(query);
									}
									rs3.Close();
								}
								rs2.Close();
								#endregion
							} else if (UserUpdateFrom.SelectedValue == "0") {
								#region Update all
								query = string.Format(
									@"
UPDATE [User] SET DepartmentID = {0}
WHERE UserID = {1} AND SponsorID = {2}",
									DepartmentID.SelectedValue,
									rs.GetInt32(1),
									sponsorID
								);
								Db.exec(query);
								query = string.Format(
									@"
UPDATE UserProfile SET DepartmentID = {0}
WHERE UserID = {1} AND SponsorID = {2}",
									DepartmentID.SelectedValue,
									rs.GetInt32(1),
									sponsorID
								);
								Db.exec(query);

								query = string.Format(
									@"
SELECT UserProfileID FROM UserProfile WHERE UserID = {0}",
									rs.GetInt32(1)
								);
								SqlDataReader rs2 = Db.rs(query);
								while (rs2.Read()) {
									int profileID = rs2.GetInt32(0);

									query = string.Format(
										@"
SELECT BQ.BQID,
	BQ.Type,
	BQ.Restricted
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0} AND sbq.Hidden = 1",
										sponsorID
									);
									SqlDataReader rs3 = Db.rs(query);
									while (rs3.Read()) {
										switch (rs3.GetInt32(1)) {
											case 1:
												goto case 7;
												//Db.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												//if (((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL") {
												//    Db.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
												//}
												//break;
											case 2:
												string newVal = ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text.Replace("'", "''");
												if (rs3.IsDBNull(2) || newVal != "*****") {
													query = string.Format(
														@"
DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}",
														rs3.GetInt32(0),
														profileID
													);
													Db.exec(query);
													query = string.Format(
														@"
INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES ({0},{1},'{2}')",
														profileID,
														rs3.GetInt32(0),
														newVal.Replace("'", "''")
													);
													Db.exec(query);
												}
												break;
											case 3:
												{
													query = string.Format(
														@"
DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}",
														rs3.GetInt32(0),
														profileID
													);
													Db.exec(query);
													string y = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
													string m = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
													string d = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
													if (y != "0" && m != "0" && d != "0") {
														try {
															DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
															if (tempDateTime < DateTime.Now) {
																query = string.Format(
																	@"
INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES ({0},{1},'{2}')",
																	profileID,
																	rs3.GetInt32(0),
																	tempDateTime.ToString("yyyy-MM-dd")
																);
																Db.exec(query);
															}
														} catch (Exception) { }
													}
												}
												break;
											case 4:
												query = string.Format(
													@"
DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}",
													rs3.GetInt32(0),
													profileID
												);
												Db.exec(query);
												query = string.Format(
													@"
INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES ({0},{1},{2})",
													profileID,
													rs3.GetInt32(0),
													Convert.ToInt32("0" + ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text)
												);
												Db.exec(query);
												break;
											case 7:
												query = string.Format(
													@"
DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}",
													rs3.GetInt32(0),
													profileID
												);
												Db.exec(query);
												if (((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL") {
													query = string.Format(
														@"
INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES ({0},{1},{2})",
														profileID,
														rs3.GetInt32(0),
														Convert.ToInt32("0" + ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue)
													);
													Db.exec(query);
												}
												break;
										}
									}
									rs3.Close();
								}
								rs2.Close();
								#endregion
							} else {
								#region Update from now
								query = string.Format(
									@"
UPDATE [User] SET DepartmentID = {0} WHERE UserID = {1} AND SponsorID = {2}",
									DepartmentID.SelectedValue,
									rs.GetInt32(1),
									sponsorID
								);
								Db.exec(query);

								query = string.Format(
									@"
SELECT u.UserProfileID,
	up.ProfileComparisonID
FROM [User] u
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
WHERE u.UserID = {0}",
									rs.GetInt32(1)
								);
								SqlDataReader rs2 = Db.rs(query);
								while (rs2.Read()) {
									#region Create new profile
									query = string.Format(
										@"
INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created)
VALUES ({0},{1},{2},{3},GETDATE())",
										rs.GetInt32(1),
										sponsorID,
										DepartmentID.SelectedValue,
										rs2.IsDBNull(1) ? "NULL" : rs2.GetInt32(1).ToString()
									);
									// TODO: Investigate why ProfileComparisonID is NULL
									Db.exec(query);
									int profileID = 0;
									query = string.Format(
										@"
SELECT TOP 1 UserProfileID
FROM UserProfile
WHERE UserID = {0}
ORDER BY UserProfileID DESC",
										rs.GetInt32(1)
									);
									SqlDataReader rs3 = Db.rs(query);
									if (rs3.Read()) {
										profileID = rs3.GetInt32(0);
									}
									rs3.Close();
									#endregion

									#region Copy old profile
									query = string.Format(
										@"
SELECT BQID, ValueInt, ValueText, ValueDate FROM UserProfileBQ WHERE UserProfileID = {0}",
										rs2.GetInt32(0)
									);
									rs3 = Db.rs(query);
									while (rs3.Read()) {
										query = string.Format(
											"INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," +
											(rs3.IsDBNull(1) ? "NULL" : rs3.GetInt32(1).ToString()) + "," +
											(rs3.IsDBNull(2) ? "NULL" : "'" + rs3.GetString(2).Replace("'", "") + "'") + "," +
											(rs3.IsDBNull(3) ? "NULL" : "'" + rs3.GetDateTime(3).ToString("yyyy-MM-dd") + "'") +
											")");
										Db.exec(query);
									}
									rs3.Close();
									#endregion

									#region Update with new hidden variables
									query = string.Format(
										@"
SELECT BQ.BQID,
	BQ.Type,
	BQ.Restricted
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0} AND sbq.Hidden = 1",
										sponsorID
									);
									rs3 = Db.rs(query);
									while (rs3.Read()) {
										switch (rs3.GetInt32(1)) {
											case 1:
												goto case 7;
												//Db.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												//if (((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL") {
												//    Db.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
												//}
												//break;
											case 2:
												string newVal = ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text.Replace("'", "''");
												if (rs3.IsDBNull(2) || newVal != "*****") {
													query = string.Format("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
													Db.exec(query);
													query = string.Format("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + profileID + "," + rs3.GetInt32(0) + ",'" + newVal.Replace("'", "''") + "')");
													Db.exec(query);
												}
												break;
											case 3:
												{
													query = string.Format("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
													Db.exec(query);
													string y = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
													string m = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
													string d = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
													if (y != "0" && m != "0" && d != "0") {
														try {
															DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
															if (tempDateTime < DateTime.Now) {
																query = string.Format("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + ",'" + tempDateTime.ToString("yyyy-MM-dd") + "')");
																Db.exec(query);
															}
														} catch (Exception) { }
													}
												}
												break;
											case 4:
												query = string.Format("DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}", rs3.GetInt32(0), profileID);
												Db.exec(query);
												query = string.Format("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES ({0},{1},{2})", profileID, rs3.GetInt32(0), Convert.ToInt32("0" + ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text));
												Db.exec(query);
												break;
											case 7:
												query = string.Format("DELETE FROM UserProfileBQ WHERE BQID = {0} AND UserProfileID = {1}", rs3.GetInt32(0), profileID);
												Db.exec(query);
												if (((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL") {
													query = string.Format("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
													Db.exec(query);
												}
												break;
										}
									}
									rs3.Close();
									#endregion

									query = string.Format("UPDATE [User] SET UserProfileID = {0} WHERE UserID = {1}", profileID, rs.GetInt32(1));
									Db.exec(query);
								}
								rs2.Close();
								#endregion
							}
						}
					}
					rs.Close();
					#endregion
					query = string.Format(
						@"
UPDATE SponsorInvite SET Email = '{0}'{1},
{2}
DepartmentID = {3} WHERE SponsorInviteID = {4}",
						Email.Text.Replace("'", "''").Trim(),
						sql,
						(stoppedReason != oldStoppedReason ? "" + "StoppedReason=" + (stoppedReason == 0 ? "NULL" : stoppedReason.ToString()) + ",Stopped=" + (stoppedReason == 0 || Stopped.Text == "" ? "NULL" : "'" + Stopped.Text.Replace("'", "") + "'") + "," + "" : ""),
						DepartmentID.SelectedValue,
						userID
					);
					Db.exec(query);
					url += "&SDID=" + showDepartmentID;
				}
			}
			if (!exists) {
				#region Update SponsorInviteBQ
				query = string.Format(
					@"
SELECT BQ.BQID,
	BQ.Type
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0} AND sbq.Hidden = 1",
					sponsorID
				);
				rs = Db.rs(query);
				while (rs.Read()) {
					int val = int.MinValue;

					if (rs.GetInt32(1) == 1 || rs.GetInt32(1) == 7) {
						//if (rs.GetInt32(1) == 7)
						//{
						if (((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedIndex != -1 && ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue != "NULL") {
							val = Convert.ToInt32(((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue);
						}
						//}
						//else if (rs.GetInt32(1) == 1)
						//{
						//    if (((RadioButtonList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedIndex != -1 && ((RadioButtonList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue != "NULL")
						//    {
						//        val = Convert.ToInt32(((RadioButtonList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue);
						//    }
						//}
						if (val != int.MinValue) {
							query = string.Format(
								@"
DELETE FROM SponsorInviteBQ
WHERE BQID = {0} AND SponsorInviteID = {1}",
								rs.GetInt32(0),
								userID
							);
							Db.exec(query);
							query = string.Format(
								@"
INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,BAID)
VALUES ({0},{1},{2})",
								userID,
								rs.GetInt32(0),
								val
							);
							Db.exec(query);
						}
					} else if (rs.GetInt32(1) == 2) {
						string valText = "*****";
						try {
							if (((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text != "") {
								valText = ((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text;
							}
						} catch (Exception) { }
						if (valText != "*****") {
							query = string.Format("DELETE FROM SponsorInviteBQ WHERE BQID = {0} AND SponsorInviteID = {1}", rs.GetInt32(0), userID);
							Db.exec(query);
							query = string.Format("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueText) VALUES ({0},{1},'{2}')", userID, rs.GetInt32(0), valText.Replace("'", "''"));
							Db.exec(query);
						}
					} else if (rs.GetInt32(1) == 4) {
						try {
							if (((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text != "") {
								val = Convert.ToInt32("0" + ((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text);
							}
						} catch (Exception) { }
						if (val != int.MinValue) {
							query = string.Format("DELETE FROM SponsorInviteBQ WHERE BQID = {0} AND SponsorInviteID = {1}", rs.GetInt32(0), userID);
							Db.exec(query);
							query = string.Format("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueInt) VALUES ({0},{1},{2})", userID, rs.GetInt32(0), val);
							Db.exec(query);
						}
					} else if (rs.GetInt32(1) == 3) {
						string y = ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
						string m = ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
						string d = ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
						if (y != "0" && m != "0" && d != "0") {
							try {
								DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
								if (tempDateTime < DateTime.Now) {
									query = string.Format("DELETE FROM SponsorInviteBQ WHERE BQID = {0} AND SponsorInviteID = {1}", rs.GetInt32(0), userID);
									Db.exec(query);
									query = string.Format("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueDate) VALUES ({0},{1},'{2}')", userID, rs.GetInt32(0), tempDateTime.ToString("yyyy-MM-dd"));
									Db.exec(query);
								}
							} catch (Exception) { }
						}
					}
				}
				rs.Close();
				#endregion

				Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + url + (showReg ? "&ShowReg=1" : ""), true);
			}
			if (exists) {
				AddUserError.Text = R.Str(lid, "user.exists", "A user with this email address already exist!") + "<br/>";
			}
		}
      #endregion


        /// <summary>
        /// Cancel Click function
        /// </summary>
        #region
        void Cancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}

         #endregion



        /// <summary>
        /// Save Unit Click function
        /// </summary>
        #region 
        void SaveUnit_Click(object sender, EventArgs e)
		{
//			string query = "";
			if (deptID == 0) {
//				query = string.Format(
//					@"
				//INSERT INTO Department (SponsorID,Department,ParentDepartmentID)
				//VALUES ({0},'{1}',{2})",
//					sponsorID,
//					Department.Text.Replace("'", "''"),
//					ParentDepartmentID.SelectedValue
//				);
//				Db.exec(query);
				departmentRepository.Save(
					new Department {
						Sponsor = new Sponsor { Id = sponsorID },
						Name = Department.Text.Replace("'", "''"),
						Parent = ConvertHelper.ToInt32(ParentDepartmentID.SelectedValue) == 0 ? null : new Department { Id = ConvertHelper.ToInt32(ParentDepartmentID.SelectedValue) }}
				);
//				query = string.Format("SELECT DepartmentID FROM Department WHERE SponsorID = {0} ORDER BY DepartmentID DESC", sponsorID);
//				SqlDataReader rs = Db.rs(query);
//				if (rs.Read()) {
//					deptID = rs.GetInt32(0);
//				}
//				rs.Close();
				deptID = departmentRepository.GetLatestDepartmentID(sponsorID);
//				query = string.Format("UPDATE Department SET DepartmentShort = '{0}', SortOrder = {1} WHERE DepartmentID = {1}", (DepartmentShort.Text.Replace("'", "''") != "" ? DepartmentShort.Text.Replace("'", "''") : deptID.ToString()), deptID);
//				Db.exec(query);
				departmentRepository.UpdateDepartment2(
					new Department {
						Id = deptID,
						ShortName = DepartmentShort.Text.Replace("'", "''") != "" ? DepartmentShort.Text.Replace("'", "''") : deptID.ToString(),
						SortOrder = deptID
					}
				);
				if (Session["SponsorAdminID"].ToString() != "-1") {
//					query = string.Format("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES ({0},{1})", Session["SponsorAdminID"], deptID);
//					Db.exec(query);
					sponsorRepository.SaveSponsorAdminDepartment(
						new SponsorAdminDepartment {
							Id = ConvertHelper.ToInt32(Session["SponsorAdminID"]),
							Department = new Department { Id = deptID }
						}
					);
				}
			} else {
//				query = string.Format(
//					@"
				//UPDATE Department SET Department = '{0}',
				//DepartmentShort = '{1}',
				//ParentDepartmentID = {2}
				//WHERE DepartmentID = {3}",
//					Department.Text.Replace("'", "''"),
//					DepartmentShort.Text.Replace("'", "''"),
//					ParentDepartmentID.SelectedValue,
//					deptID
//				);
//				Db.exec(query);
				departmentRepository.UpdateDepartment(
					new Department {
						Id = deptID,
						Name = Department.Text.Replace("'", "''"),
						ShortName = DepartmentShort.Text.Replace("'", "''"),
						Parent = ConvertHelper.ToInt32(ParentDepartmentID.SelectedValue) == 0 ? null : new Department { Id = ConvertHelper.ToInt32(ParentDepartmentID.SelectedValue) }
					}
				);
			}
//			query = string.Format("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = {0}", sponsorID);
//			Db.exec(query);
			departmentRepository.UpdateDepartmentSortString(sponsorID);

			Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}
         #endregion
    }
}