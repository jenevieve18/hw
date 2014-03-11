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

namespace HW.Grp
{
	public partial class Org : System.Web.UI.Page
	{
		int deptID = 0, showDepartmentID = 0, deleteDepartmentID;
		int userID = 0, deleteUserID = 0, sendSponsorInvitationID = 0;
		int sponsorID = 0;
		bool showReg = false;
		string hiddenBqJoin = "", hiddenBqWhere = "";
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			SearchResultList.Text = "";
			SearchResults.Visible = false;
			ActionNav.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			sponsorID = Convert.ToInt32(Session["SponsorID"]);

			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Organization, DateTime.Now);
			string query = "";
			if (sponsorID != 0) {
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
						@"
SELECT sib.BAID,
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

//						Db2.sendMail(rs.GetString(2).Trim(), body, rs.GetString(1));
						Db.sendMail(rs.GetString(2).Trim(), rs.GetString(1), body);
					}
					rs.Close();
					#endregion
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (sendSponsorInvitationID != 0) {
					#region Resend
					query = string.Format(
						@"
SELECT s.InviteTxt,
	s.InviteSubject,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8),
	si.UserID,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12),
	s.LoginTxt,
	s.LoginSubject
FROM Sponsor s
INNER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID
LEFT OUTER JOIN [User] u ON u.UserID = si.UserID
WHERE s.SponsorID = {0} AND si.SponsorInviteID = {1}",
						sponsorID,
						sendSponsorInvitationID
					);
					rs = Db.rs(query);
					if (rs.Read()) {
						if (rs.IsDBNull(4)) {
//							Db2.sendInvitation(sendSponsorInvitationID, rs.GetString(2).Trim(), rs.GetString(0), rs.GetString(1), rs.GetString(3));
							sponsorRepository.UpdateSponsorInviteSent(sendSponsorInvitationID);
//							Db.sendInvitation(sendSponsorInvitationID, rs.GetString(2).Trim(), rs.GetString(0), rs.GetString(1), rs.GetString(3));
							Db.sendInvitation(sendSponsorInvitationID, rs.GetString(2).Trim(), rs.GetString(1), rs.GetString(0), rs.GetString(3));
						} else {
							string body = rs.GetString(7);

							string personalLink = "" + ConfigurationManager.AppSettings["healthWatchURL"] + "";
							if (!rs.IsDBNull(5) && rs.GetInt32(5) > 0) {
								personalLink += "c/" + rs.GetString(6).ToLower() + rs.GetInt32(4).ToString();
							}
							if (body.IndexOf("<LINK/>") >= 0) {
								body = body.Replace("<LINK/>", personalLink);
							} else {
								body += "\r\n\r\n" + personalLink;
							}

//							Db2.sendMail(rs.GetString(2).Trim(), body, rs.GetString(8));
							Db.sendMail(rs.GetString(2).Trim(), rs.GetString(8), body);
						}
					}
					rs.Close();
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
					ImportUnitsParentDepartmentID.Items.Add(new ListItem("< top level >", "NULL"));
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

					ParentDepartmentID.Items.Add(new ListItem("< top level >", "NULL"));
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
				query = string.Format("SELECT DepartmentShort FROM Department WHERE DepartmentShort IS NOT NULL AND SponsorID = " + sponsorID);
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
							query = "SET NOCOUNT ON;" +
								"SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
								"BEGIN TRAN;" +
								"INSERT INTO Department (" +
								"SponsorID," +
								"Department," +
								"DepartmentShort" +
								") VALUES (" +
								"" + sponsorID + "," +
								"'" + unit + "'," +
								"'" + id + "'" +
								");" +
								"SELECT DepartmentID FROM [Department] WHERE SponsorID=" + sponsorID + " AND DepartmentShort = '" + id + "' ORDER BY DepartmentID DESC;COMMIT;";
							rs = Db.rs(query);
							if (rs.Read()) {
								// Update sort order
								query = string.Format("UPDATE Department SET SortOrder = " + rs.GetInt32(0) + " WHERE DepartmentID = " + rs.GetInt32(0));
								Db.exec(query);

								if (Session["SponsorAdminID"].ToString() != "-1") {
									// Add to sponsor admin mapping
									query = string.Format("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES (" + Session["SponsorAdminID"] + "," + rs.GetInt32(0) + ")");
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
						query = string.Format("INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created) VALUES (" + rs.GetInt32(0) + ",1,NULL," + rs2.GetInt32(1) + ",GETDATE())");
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
		
		protected override void OnPreRender(EventArgs e)
		{
			#region Normal org
			string select = "";
			string join = "";
			string BQs = "";
			string BQdesc = "";

			UserUpdate.Visible = (userID != 0);

			if (deleteUserID != 0) {
				DeleteUser.Visible = true;
				Actions.Visible = false;
			}

			if (deleteDepartmentID != 0) {
				DeleteDepartment.Visible = true;
				Actions.Visible = false;
			}

			string aggrBQ = "", aggrBRdesc = "";
			int aggrBQcx = 0;
			string query = string.Format(
				@"
SELECT BQ.Internal,
	BQ.BQID,
	BQ.Type,
	sbq.Hidden,
	sbq.InGrpAdmin,
	sbq.Fn,
	BQ.InternalAggregate,
	BQ.Restricted
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0} AND (sbq.Hidden = 1 OR sbq.InGrpAdmin = 1)
ORDER BY sbq.SortOrder",
				sponsorID
			);
			SqlDataReader rs = Db.rs(query);
			while (rs.Read()) {
				if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1) {
					if (!rs.IsDBNull(7)) {
						// Changed after code sent to Ian, JPE 121214
						select += ", LEFT(CAST(y" + rs.GetInt32(1) + ".SponsorInviteID AS VARCHAR(8)),0)+'*****' AS XI" + rs.GetInt32(1) + "";
						join += "" + " LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					} else if (rs.GetInt32(2) == 1 || rs.GetInt32(2) == 7) {
						select += ", x" + rs.GetInt32(1) + ".Internal AS XI" + rs.GetInt32(1) + "";
						join += "" + " LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1) +
							" LEFT OUTER JOIN BA x" + rs.GetInt32(1) + " ON x" + rs.GetInt32(1) + ".BQID = y" + rs.GetInt32(1) + ".BQID AND y" + rs.GetInt32(1) + ".BAID = x" + rs.GetInt32(1) + ".BAID";
					} else if (rs.GetInt32(2) == 2) {
						select += ", y" + rs.GetInt32(1) + ".ValueText AS XI" + rs.GetInt32(1) + "";
						join += "" + " LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					} else if (rs.GetInt32(2) == 4) {
						select += ", y" + rs.GetInt32(1) + ".ValueInt AS XI" + rs.GetInt32(1) + "";
						join += "" + " LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					} else if (rs.GetInt32(2) == 3) {
						select += ", '' + CAST(DATEPART(yyyy,y" + rs.GetInt32(1) + ".ValueDate) AS VARCHAR(4)) + '-' + CAST(DATEPART(mm,y" + rs.GetInt32(1) + ".ValueDate) AS VARCHAR(2)) + '-' + CAST(DATEPART(dd,y" + rs.GetInt32(1) + ".ValueDate) AS VARCHAR(2)) + '' AS XI" + rs.GetInt32(1) + "";
						join += "" + " LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					}

					BQs += (BQs != "" ? ":" : "") + rs.GetInt32(1).ToString();
					BQdesc += "<TD ALIGN='CENTER' STYLE='font-size:9px;'>&nbsp;<B>" + rs.GetString(0) + "</B>&nbsp;</TD>";
				} else {
					aggrBQcx++;
					aggrBQ += (aggrBQ != "" ? "," : "") + rs.GetInt32(1);
					aggrBRdesc += "<TD ALIGN='CENTER' STYLE='font-size:9px;'>&nbsp;<B>" + rs.GetString(6) + "</B>&nbsp;</TD>";
				}
			}
			rs.Close();

			string ESstart = "", ESdesc = "", ESselect = "", ESuserSelect = "", ESuserJoin = "", ESrounds = "", ESroundTexts = "", ESpreviousRounds = "", ESpreviousRoundTexts = "", ESjoin = "", ESattr = "";
			string bqESselect = "", bqESjoin = "";
			int EScount = 0, totalEScount = 0, tmpEScount = 0;
//			query = string.Format("SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = {0}", sponsorID);
//			rs = Db.rs(query);
//			if (rs.Read()) {
//				totalEScount = rs.GetInt32(0);
//			}
//			rs.Close();
			totalEScount = sponsorRepository.CountExtendedSurveyBySponsor(sponsorID);
			query = string.Format(
				@"
SELECT ses.SponsorExtendedSurveyID,
	ses.Internal,
	ses.ProjectRoundID,
	ses.EformFeedbackID,
	ses.RequiredUserCount,
	ses.PreviousProjectRoundID,
	ses.RoundText,
	ses2.RoundText,
	pr.Started,
	pr.Closed,
	ses.WarnIfMissingQID,
	ses.ExtraEmailSubject
FROM SponsorExtendedSurvey ses
LEFT OUTER JOIN SponsorExtendedSurvey ses2 ON ses.SponsorID = ses2.SponsorID AND ses.PreviousProjectRoundID = ses2.ProjectRoundID
LEFT OUTER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID
WHERE ses.SponsorID = {0}
ORDER BY ses.SponsorExtendedSurveyID",
				sponsorID
			);
			rs = Db.rs(query);
			while (rs.Read()) {
				if (totalEScount <= 8 || tmpEScount >= (totalEScount - 8)) {
					ESstart += (ESstart != "" ? "," : "") + (rs.IsDBNull(8) ? DateTime.MaxValue : rs.GetDateTime(8)).ToString("yyyy-MM-dd");
					ESrounds += (ESrounds != "" ? "," : "") + rs.GetInt32(2);
					ESroundTexts += (ESroundTexts != "" ? "," : "") + (rs.IsDBNull(6) ? "$" : rs.GetString(6));
					ESpreviousRounds += (ESpreviousRounds != "" ? "," : "") + (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
					ESpreviousRoundTexts += (ESpreviousRoundTexts != "" ? "," : "") + (rs.IsDBNull(7) ? "$" : rs.GetString(7));
					// Answers on this department and below
					ESselect += string.Format(
						@",
(
	SELECT COUNT(*)
	FROM UserSponsorExtendedSurvey x
	--INNER JOIN [User] xu ON x.UserID = xu.UserID
	INNER JOIN SponsorInvite xsi ON x.UserID = xsi.UserID
	INNER JOIN Department xd ON xsi.DepartmentID = xd.DepartmentID
	WHERE LEFT(xd.SortString,LEN(d.SortString)) = d.SortString
	AND x.SponsorExtendedSurveyID = {0}
	AND xsi.SponsorID = d.SponsorID
	AND x.AnswerID IS NOT NULL
) ",
						rs.GetInt32(0)
					);
					// Answers on this department
					ESselect += string.Format(
						@",
(
	SELECT COUNT(*)
	FROM UserSponsorExtendedSurvey x
	--INNER JOIN [User] xu ON x.UserID = xu.UserID
	INNER JOIN SponsorInvite xsi ON x.UserID = xsi.UserID
	INNER JOIN Department xd ON xsi.DepartmentID = xd.DepartmentID
	WHERE xd.DepartmentID = d.DepartmentID
	AND x.SponsorExtendedSurveyID = {0}
	AND xsi.SponsorID = d.SponsorID
	AND x.AnswerID IS NOT NULL
) ",
						rs.GetInt32(0)
					);
					ESselect += ", es" + rs.GetInt32(0) + ".ProjectRoundUnitID AS PRUID" + rs.GetInt32(0) + ", sesd" + rs.GetInt32(0) + ".RequiredUserCount, sesd" + rs.GetInt32(0) + ".Hide, sesd" + rs.GetInt32(0) + ".Ext ";

					ESjoin += " " +
						"LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd" + rs.GetInt32(0) + " ON sesd" + rs.GetInt32(0) + ".SponsorExtendedSurveyID = " + rs.GetInt32(0) + " AND sesd" + rs.GetInt32(0) + ".DepartmentID = d.DepartmentID " +
						"LEFT OUTER JOIN eform..ProjectRoundUnit es" + rs.GetInt32(0) + " ON es" + rs.GetInt32(0) + ".ProjectRoundID = " + rs.GetInt32(2) + " " +
						"AND (s.Sponsor + '=' + dbo.cf_departmentTree(d.DepartmentID,'=')) = eform.dbo.cf_projectUnitTree(es" + rs.GetInt32(0) + ".ProjectRoundUnitID,'=') ";
					ESdesc += "<TD ALIGN='CENTER' style='font-size:9px;'>" +
						"&nbsp;<B style='font-size:8px;'>" + rs.GetString(1).Replace(" ", "&nbsp;<br/>&nbsp;") + "</B>&nbsp;" +
						"<br/>" + (rs.IsDBNull(8) ? "" : rs.GetDateTime(8).ToString("yyMMdd")) +
						"<br/>--" +
						"<br/>" + (rs.IsDBNull(9) ? "" : rs.GetDateTime(9).ToString("yyMMdd")) +
						"</TD>";
					ESuserSelect += ", s" + rs.GetInt32(0) + ".AnswerID AS AID" + rs.GetInt32(0) + ", " +
						"s" + rs.GetInt32(0) + ".ProjectRoundUserID AS PRU2ID" + rs.GetInt32(0) + " ";
					ESuserJoin += " " +
						"LEFT OUTER JOIN UserSponsorExtendedSurvey s" + rs.GetInt32(0) + " ON s.UserID = s" + rs.GetInt32(0) + ".UserID " +
						"AND s" + rs.GetInt32(0) + ".SponsorExtendedSurveyID = " + rs.GetInt32(0) + " ";
					ESattr += (ESattr != "" ? "," : "") +
						(!rs.IsDBNull(3) ? rs.GetInt32(3) : 0) + ":" +      // 0 EformFeedbackID
						(!rs.IsDBNull(4) ? rs.GetInt32(4) : 10) + ":" +     // 1 RequiredUserCount
						(!rs.IsDBNull(10) ? rs.GetInt32(10) : 0) + ":" +    // 2 WarnIfMissingQID
						(!rs.IsDBNull(11) ? 1 : 0) + ":" +                  // 3 Has ExtraEmailSubject
						(!rs.IsDBNull(0) ? rs.GetInt32(0) : 0);             // 4 SponsorExtendedSurveyID
					EScount++;

					// Answers for this BQ.BAID
					bqESselect += ", COUNT(s" + rs.GetInt32(0) + ".AnswerID) ";
					bqESjoin += " " +
						"LEFT OUTER JOIN UserSponsorExtendedSurvey s" + rs.GetInt32(0) + " ON u.UserID = s" + rs.GetInt32(0) + ".UserID " +
						"AND s" + rs.GetInt32(0) + ".SponsorExtendedSurveyID = " + rs.GetInt32(0) + " ";
				}

				tmpEScount++;
			}
			rs.Close();
			int[] ESanswerCount = new int[EScount];

			OrgTree.Text = "";

			if (showDepartmentID != 0) {
				OrgTree.Text += string.Format(
					@"
<table border='0' cellspacing='0' cellpadding='0' style='font-size:12px;line-height:1.0;vertical-align:middle;'>
	<tr style='border-bottom:1px solid #333333;'>
		<td colspan='2'><b>Unit/Email</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>Action</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>Active</b>&nbsp;</td>
		<!--<td align='center' style='font-size:9px;'>&nbsp;<b>Active/<br>Activated</b>&nbsp;</td>-->
		{0}
		<td align='center' style='font-size:9px;'>&nbsp;<b>Total</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>Received&nbsp;<br/>&nbsp;inivtation</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>1st invite&nbsp;<br/>&nbsp;sent</b>&nbsp;</td>
		{1}
		{2}
		<TD align='center' style='font-size:9px;'>&nbsp;<b>Unit ID&nbsp;<br/>&nbsp;User status</b>&nbsp;</td>
	</tr>",
					ESdesc,
					aggrBRdesc,
					BQdesc
				);
			} else {
				OrgTree.Text += string.Format(
					@"
<table border='0' cellspacing='0' cellpadding='0' style='font-size:12px;line-height:1.0;vertical-align:middle;'>
	<tr style='border-bottom:1px solid #333333;'>
		<td colspan='2'><b>Unit</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>Action</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>Active</b>&nbsp;</td>
		<!--<td align='center' style='font-size:9px;'>&nbsp;<b>Active/<br>Activated</b>&nbsp;</td>-->
		{0}
		<td align='center' style='font-size:9px;'>&nbsp;<b>Total</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>Received&nbsp;<br/>&nbsp;inivtation</b>&nbsp;</td>
		<td align='center' style='font-size:9px;'>&nbsp;<b>1st invite&nbsp;<br/>&nbsp;sent</b>&nbsp;</td>
		{1}
		<td align='center' style='font-size:9px;'>&nbsp;<b>Unit ID</b>&nbsp;</td>
	</tr>",
					ESdesc,
					aggrBRdesc
				);
			}
			//OrgTree.Text += "<TR><TD COLSPAN='" + (aggrBQcx + 8 + (showDepartmentID != 0 && BQs != "" ? BQs.Split(':').Length : 0) + EScount) + "' style='height:1px;line-height:1px;background-color:#333333'><img src='img/null.gif' width='1' height='1'></TD></TR>";
			OrgTree.Text += string.Format("<tr><td colspan='3'>{0}</td>[xxx]</tr>", Session["Sponsor"]);

			int UX = 0, totalActivated = 0, IX = 0, totalActive = 0;

			int MIN_SHOW = sponsorRepository.ReadSponsor(sponsorID).MinUserCountToDisclose;

			bool[] DX = new bool[8];

			string sql = string.Format(
				@"
SELECT d.Department,
	dbo.cf_departmentDepth(d.DepartmentID),
	d.DepartmentID,
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID
		WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL
	),
	(
		SELECT COUNT(*) FROM Department x
		{0}
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL)
		AND d.SponsorID = x.SponsorID
		AND d.SortString < x.SortString
	),
	--(
	--	SELECT COUNT(*)
	--	FROM SponsorInvite si
	--	INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
	--	WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID
	--),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND (si.StoppedReason IS NULL OR si.StoppedPercent IS NOT NULL)
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN [User] u ON si.UserID = u.UserID
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL
	),
	(
		SELECT MIN(si.Sent)
		FROM SponsorInvite si
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL
	),
	d.DepartmentShort
	{1},
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN [User] u ON si.UserID = u.UserID
		INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID
		WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString
		AND si.SponsorID = d.SponsorID
		AND si.StoppedReason IS NULL
		AND si.UserID IS NOT NULL
	),
	(
		SELECT COUNT(*)
		FROM SponsorInvite si
		INNER JOIN [User] u ON si.UserID = u.UserID
		WHERE si.DepartmentID = d.DepartmentID
		AND si.StoppedReason IS NULL
	),
	d.MinUserCountToDisclose
FROM Department d
INNER JOIN Sponsor s ON d.SponsorID = s.SponsorID
{2}
{3}
d.SponsorID = {4} ORDER BY d.SortString",
				(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = " + Session["SponsorAdminID"] + " " : ""),
				ESselect,
				ESjoin,
				(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " AND " : "WHERE "),
				sponsorID
			);
			rs = Db.rs(sql);
//			Dictionary<string, double> actives = new Dictionary<string, double>();
			double extendedSurveyTotal = sponsorRepository.GetExtendedSurveyTotal(sponsorID);
			while (rs.Read()) {
				int depth = rs.GetInt32(1);
				DX[depth] = (rs.GetInt32(6) > 0);

				UX += rs.GetInt32(3);
				totalActivated += rs.GetInt32(4);
				IX += rs.GetInt32(5);
				int active = rs.GetInt32(12 + 6 * EScount);
				totalActive += rs.GetInt32(12 + 6 * EScount + 1);
				int deptMinUserCountToDisclose = rs.IsDBNull(12 + 6 * EScount + 2) ? MIN_SHOW : rs.GetInt32(12 + 6 * EScount + 2);

				OrgTree.Text += string.Format(
					@"
<tr{0}>
	<td colspan='2'>
		<table border='0' cellspacing='0' cellpadding='0'>
			<tr>
				<td>",
					(depth == 1 || depth == 4 ? " style='background-color:#EEEEEE'" : (depth == 2 || depth == 5 ? " style='background-color:#F6F6F6'" : ""))
				);
				for (int i = 1; i <= depth; i++) {
					OrgTree.Text += string.Format("<img src='img/{0}.gif' width='19' height='20'/>", (i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null")));
				}
				string s = rs.GetString(0);
				if (s.Length > 30) {
					int i0 = s.Length / 2;
					string s1 = s.Substring(i0);
					int i1 = s1.IndexOf(" ");
					if (i1 >= 0) {
						s = s.Substring(0, i0);
						s += s1.Substring(0, i1);
						s += "<br/>&nbsp;";
						s += s1.Substring(i1);
					}
				}
				string key = Guid.NewGuid().ToString();
//				actives.Add(key, active);
				OrgTree.Text += string.Format(
					@"
				</td>
				<td style='vertical-align:middle;{0}'>&nbsp;{1}{2}{3}&nbsp;</td>
			</tr>
		</table>
	</td>
	<td align='center'>{4}{5}</td>
	<td align='center' style='font-size:9px;'>
		<span title='{6}'>{7}{8}</span>
	</td>",
					(s.Length > 20 ? "font-size:10px;" : ""),
					(deptID == rs.GetInt32(2) || showDepartmentID == rs.GetInt32(2) ? "<b>" : ""),
					s,
					(deptID == rs.GetInt32(2) || showDepartmentID == rs.GetInt32(2) ? "</b>" : ""),
					(Convert.ToInt32(Session["ReadOnly"]) == 0 ? "<a href='org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DID=" + rs.GetInt32(2) + "'><img src='img/unt_edt.gif' border='0'/></A>" + "" : ""),
					(rs.GetInt32(3) > 0 ? "<a href='org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&SDID=" + rs.GetInt32(2) + "'><img src='img/usr_on.gif' border='0'/></A>" : (Convert.ToInt32(Session["ReadOnly"]) == 0 ? "<a href='org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DeleteDID=" + rs.GetInt32(2) + "'><img src='img/unt_del.gif' border='0'/></a>" : "")),
					(rs.GetInt32(3) > 0 && rs.GetInt32(8) != rs.GetInt32(4) ? "" + (rs.GetInt32(4) >= deptMinUserCountToDisclose ? rs.GetInt32(4).ToString() : (showReg ? rs.GetInt32(4).ToString() : "")) + "" : ""),
//					(rs.GetInt32(8) >= deptMinUserCountToDisclose ? active.ToString() + " / " + rs.GetInt32(8).ToString() : "<img src='img/key.gif'/>")
					(active >= deptMinUserCountToDisclose ? active.ToString() : "<img src='img/key.gif'/>"),
//					(active >= deptMinUserCountToDisclose ? string.Format(" ({0}%)", key) : "")
					(active > deptMinUserCountToDisclose ? string.Format(" ({0}%)", ((float)active / rs.GetInt32(7) * 100).ToString("0.0")) : "")
				);

				for (int i = 0; i < EScount; i++) {
					int idx = 12 + 6 * i;
					ESanswerCount[i] += rs.GetInt32(idx + 1);
					int rac = (rs.IsDBNull(idx + 3) ? Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1]) : rs.GetInt32(idx + 3));
					if (rs.IsDBNull(idx + 4)) {
						OrgTree.Text += string.Format(
							@"
	<td align='center' style='font-size:9px;'>&nbsp;{0}{1}&nbsp;{2}&nbsp;</td>",
							(!rs.IsDBNull(idx + 5) ? "E" : ""),
							(
								!rs.IsDBNull(idx + 2) && Convert.ToInt32(ESattr.Split(',')[i].Split(':')[0]) != 0 && rac <= rs.GetInt32(idx)
								? string.Format(
									"<a href=\"JavaScript:void(window.open('{0}feedback.aspx?AB=1&R={1}&{2}{3}{4}U={5}&RAC={6}&UD={7}&N={8}','es{9}','scrollbars=1,width=880,height=700,resizable=1,toolbar=0,status=0,menubar=0,location=0'));\"><img src='img/graphIcon2.gif' border='0'/></a>",
									ConfigurationManager.AppSettings["eFormURL"],
									(ESrounds.Split(',')[i]),
									(ESpreviousRounds.Split(',')[i] != "0" ? "RR=" + ESpreviousRounds.Split(',')[i] + "&" : ""),
									(ESpreviousRounds.Split(',')[i] != "0" ? "R1=" + ESroundTexts.Split(',')[i] + "&" : ""),
									(ESpreviousRounds.Split(',')[i] != "0" ? "R2=" + ESpreviousRoundTexts.Split(',')[i] + "&" : ""),
									rs.GetInt32(idx + 2).ToString(),
									rac,
									Server.HtmlEncode(rs.GetString(0)).Replace("&", "_0_").Replace("#", "_1_"),
									Server.HtmlEncode(Session["Sponsor"].ToString()).Replace("&", "_0_").Replace("#", "_1_"),
									i
								) : ""
							),
							(
								rs.GetInt32(idx) >= rac
								? string.Format(
									"<span title='{0}'>{1}</span>",
									(
										rs.GetInt32(3) > 0 && rs.GetInt32(idx) != rs.GetInt32(idx + 1)
										? "" + (
											rs.GetInt32(idx + 1) >= rac
											? rs.GetInt32(idx + 1).ToString()
											: (showReg ? rs.GetInt32(idx + 1).ToString() : "")
										)
										: ""
									),
									string.Format("{0} ({1}%)", rs.GetInt32(idx).ToString(), (rs.GetInt32(idx) / extendedSurveyTotal * 100).ToString("0.0"))
								)
								: string.Format("<img src='img/key.gif' title='{0}'/>", (showReg ? rs.GetInt32(idx + 1).ToString() : ""))
							)
						);
					} else {
						OrgTree.Text += "<td align='center'>&nbsp;</td>";
					}
				}
				OrgTree.Text += string.Format("<td align='center' style='font-size:9px;'>&nbsp;{0}{1}&nbsp;</td>", rs.GetInt32(7),  (rs.GetInt32(3) > 0 && rs.GetInt32(3) != rs.GetInt32(7) ? " (" + rs.GetInt32(3) + ")" : ""));
				OrgTree.Text += "<td align='center' style='font-size:9px;'>&nbsp;" + (rs.GetInt32(7) > 0 ? Math.Round(((float)rs.GetInt32(9) / (float)rs.GetInt32(7)) * 100, 0).ToString() + "%" : "-") + (rs.GetInt32(3) > 0 && rs.GetInt32(5) != rs.GetInt32(9) && Math.Round(((float)rs.GetInt32(9) / (float)rs.GetInt32(7)) * 100, 0) != Math.Round((float)rs.GetInt32(5) / (float)rs.GetInt32(3) * 100, 0) ? " (" + Math.Round((float)rs.GetInt32(5) / (float)rs.GetInt32(3) * 100, 0).ToString() + "%" + ")" : "") + "&nbsp;</td>";
				OrgTree.Text += string.Format("<td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>", (rs.IsDBNull(10) ? "N/A" : rs.GetDateTime(10).ToString("yyMMdd")));

				if (aggrBQcx != 0) {
					foreach (string a in aggrBQ.Split(',')) {
						OrgTree.Text += "<td align='center'>&nbsp;";
						query = string.Format(
							@"
SELECT AVG(DATEDIFF(year, upbq.ValueDate, GETDATE())),
	COUNT(upbq.ValueDate)
FROM Department d
INNER JOIN Department sid ON LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND sid.SponsorID = d.SponsorID
INNER JOIN SponsorInvite si ON sid.DepartmentID = si.DepartmentID
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfileBQ upbq ON u.UserProfileID = upbq.UserProfileID AND upbq.BQID = {0}
WHERE d.DepartmentID = {1}",
							Convert.ToInt32(a),
							rs.GetInt32(2)
						);
						SqlDataReader rs2 = Db.rs(query);
						if (rs2.Read() && !rs2.IsDBNull(0)) {
							OrgTree.Text += (rs2.GetInt32(1) >= deptMinUserCountToDisclose ? rs2.GetValue(0).ToString() : "<img src='img/key.gif'/>");
						}
						rs2.Close();
						OrgTree.Text += "&nbsp;</td>";
					}
				}
				if (showDepartmentID != 0 && BQs != "") {
					for (int i = 0; i < BQs.Split(':').Length; i++) {
						OrgTree.Text += "<td align='center' style='font-size:9px;'>&nbsp;</td>";
					}
				}
				OrgTree.Text += string.Format("<td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>", (rs.IsDBNull(11) ? "N/A" : rs.GetString(11)));
				OrgTree.Text += "</tr>";

				if (showDepartmentID == rs.GetInt32(2)) {
					#region Show department
					int dynamicIdx = 9;
					StringBuilder usr = new StringBuilder();

					sql = string.Format(
						@"
SELECT s.SponsorInviteID,
	s.Email,
	s.Sent,
	s.UserID,
	u.ReminderLink,
	LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)),
	s.PreviewExtendedSurveys,
	s.Stopped,
	s.StoppedReason
	{0}{1}
FROM SponsorInvite s{2}{3}
LEFT OUTER JOIN [User] u ON s.UserID = u.UserID
WHERE s.SponsorID = {4}
AND s.DepartmentID = {5}
ORDER BY s.Email",
						select,
						ESuserSelect,
						join,
						ESuserJoin,
						sponsorID,
						rs.GetInt32(2)
					);
					SqlDataReader rs2 = Db.rs(sql);
					while (rs2.Read()) {
						usr.Append("<tr style='background-color:#FFF7D6'><td>");
						for (int i = 1; i <= depth; i++) {
							usr.Append(string.Format("<img src='img/{0}.gif' width='19' height='20'/>", (DX[i] ? "I" : "null")));
						}
						usr.Append("</td><td style='font-size:9px'>" + (rs2.IsDBNull(1) ? "" : rs2.GetString(1)) + "</td>" +
						           "<td align='center'>" +
						           (Convert.ToInt32(Session["ReadOnly"]) == 0 ?
						            "<a href='org.aspx?SDID=" + showDepartmentID.ToString() + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&UID=" + rs2.GetInt32(0).ToString() + "'><img src='img/usr_edt.gif' border='0'/></a>" +
						            "<a href='org.aspx?SDID=" + showDepartmentID.ToString() + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DeleteUID=" + rs2.GetInt32(0).ToString() + "'><img src='img/usr_del.gif' border='0'/></a>" +
						            "" : "") +
						           "</td>" +
						           "<td align='center'>");
						if (showReg) {
							if (!rs2.IsDBNull(3) && !rs2.IsDBNull(5)) {
								usr.Append(string.Format("<a title='Log on to users account' href='{0}a/{1}{2}' target='_blank'>{3}/{4}</a>", ConfigurationManager.AppSettings["healthWatchURL"], rs2.GetString(5), rs2.GetInt32(3), rs2.GetInt32(3), (rs2.IsDBNull(4) ? "0" : rs2.GetInt32(4).ToString())));

								query = string.Format(
									@"
SELECT u.UserID,
	s.Sponsor
FROM [User] u
LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID
WHERE u.UserID <> {0} AND u.Email = '{1}'",
									rs2.GetInt32(3),
									rs2.GetString(1).Replace("'", "''")
								);
								SqlDataReader rs3 = Db.rs(query);
								while (rs3.Read()) {
									usr.Append(string.Format("<br/><span title='{0}'>{1}</span>", (rs3.IsDBNull(1) ? "Private" : rs3.GetString(1)), rs3.GetInt32(0)));
								}
								rs3.Close();
							} else {
								query = string.Format(
									@"
SELECT u.UserID,
	s.Sponsor
FROM [User] u
LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID
WHERE u.Email = '{0}'",
									rs2.GetString(1).Replace("'", "''")
								);
								SqlDataReader rs3 = Db.rs(query);
								while (rs3.Read()) {
									usr.Append("<a title='Connect " + (rs3.IsDBNull(1) ? "Private" : rs3.GetString(1)) + "' href='org.aspx?" + (showReg ? "ShowReg=1&" : "") + "SDID=" + showDepartmentID.ToString() + "&ConnectSPIID=" + rs2.GetInt32(0) + "&WithUID=" + rs3.GetInt32(0) + "&AndDID=" + rs.GetInt32(2) + "'>" + rs3.GetInt32(0) + "</a><br/>");
								}
								rs3.Close();
							}
						}

						usr.Append("</td>");
						for (int i = 0; i < EScount; i++) {
							usr.Append("<td align='center'>");
							if (showReg) {
								int idx = (BQs != "" ? BQs.Split(':').Length : 0) + dynamicIdx + i * 2;
								if (rs2.IsDBNull(idx + 1)) {
									usr.Append("&nbsp;");
								} else {
									if (rs2.IsDBNull(idx)) {
										usr.Append("<img srC='img/star.gif'/>");
										if (Convert.ToInt32(ESattr.Split(',')[i].Split(':')[3]) != 0) {
											usr.Append("<a href='org.aspx?" +
											           "ShowReg=1" +
											           "&SESID=" + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[4]) + "" +
											           "&SendExtra=" + rs2.GetInt32(3) + "" +
											           "&SDID=" + showDepartmentID.ToString() + "" +
											           "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "" +
											           "' title='Send extra'>!</a>");
										}
										query = string.Format(
											@"
SELECT a.AnswerID,
	a.CurrentPage
FROM Answer a
WHERE a.ProjectRoundUserID = {0}",
											rs2.GetInt32(idx + 1)
										);
										SqlDataReader rs3 = Db.rs(query, "eFormSqlConnection");
										if (rs3.Read() && !rs3.IsDBNull(1)) {
											usr.Append("<a href='org.aspx?" +
											           "ShowReg=1" +
											           "&SubmitAID=" + rs3.GetInt32(0) + "" +
											           "&SubmitUID=" + rs2.GetInt32(idx + 1) + "" +
											           "&SDID=" + showDepartmentID.ToString() + "" +
											           "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "" +
											           "' title='Submit survey (number indicates what page the user is on)'>" + rs3.GetInt32(1) + "</a>" + "");
										}
										rs3.Close();
									} else {
										usr.Append("<a href='org.aspx?" +
										           "ShowReg=1" +
										           "&ReclaimAID=" + rs2.GetInt32(idx) + "" +
										           "&ReclaimUID=" + rs2.GetInt32(idx + 1) + "" +
										           "&SDID=" + showDepartmentID.ToString() + "" +
										           "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "" +
										           "' title='Withdraw submission of survey (mark as not submitted and allow changes)'><IMG SRC='img/starOK.gif' BORDER='0'/></a>" + "");
										if (Convert.ToInt32(ESattr.Split(',')[i].Split(':')[2]) != 0) {
											query = string.Format("SELECT COUNT(*) FROM AnswerValue WHERE AnswerID = " + rs2.GetInt32(idx) + " AND QuestionID = " + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[2]) + " AND DeletedSessionID IS NULL AND (ValueInt IS NOT NULL OR ValueDecimal IS NOT NULL OR ValueDateTime IS NOT NULL OR ValueText IS NOT NULL)");
											SqlDataReader rs3 = Db.rs(query, "eFormSqlConnection");
											if (!rs3.Read() || rs3.IsDBNull(0) || rs3.GetInt32(0) == 0) {
												usr.Append("<span style='font-weight:bold;color:#cc0000;'>!</span>");
											}
											rs3.Close();
										}
									}
								}
							} else {
								usr.Append("&nbsp;");
							}
							if (Convert.ToDateTime(ESstart.Split(',')[i]) > DateTime.Now && Convert.ToInt32(Session["ReadOnly"]) == 0) {
								usr.Append("<a title='Klicka för att låsa upp/låsa möjlighet till förhandsinmatning av enkäten' href='org.aspx?PESSIID=" + rs2.GetInt32(0) + "&Flip=" + (rs2.IsDBNull(6) ? 1 : 0) + "&" + (showReg ? "ShowReg=1&" : "") + "SDID=" + showDepartmentID.ToString() + "'><img src='img/" + (rs2.IsDBNull(6) ? "locked" : "unlock") + ".gif' border='0'/></a>");
							}
							usr.Append("</td>");
						}
						usr.Append("<td>&nbsp;</td><td align='center' style='font-size:9px;'>&nbsp;");
						if (rs2.IsDBNull(2)) {
							usr.Append("No");
						} else {
							usr.Append("Yes, " + rs2.GetDateTime(2).ToString("yyyyMMdd"));
						}
						if (Convert.ToInt32(Session["ReadOnly"]) == 0) {
							usr.Append(", <a href='org.aspx?" + (showReg ? "ShowReg=1&" : "") + "SendSPIID=" + rs2.GetInt32(0) + "&SDID=" + showDepartmentID.ToString() + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "'>" + (rs2.IsDBNull(2) ? "Send" : "Resend") + "</a>");
						}
						usr.Append("&nbsp;</td>");
						usr.Append("<td align='center'>&nbsp;</td>");
						if (aggrBQcx != 0) {
							usr.Append("<td colspan='" + aggrBQcx + "'>&nbsp;</td>");
						}
						if (BQs != "") {
							for (int i = 0; i < BQs.Split(':').Length; i++) {
								if (rs2.IsDBNull(i + dynamicIdx)) {
									usr.Append("<td align='center' style='font-size:9px;'>&nbsp;&lt;&nbsp;N/A&nbsp;&gt;&nbsp;</td>");
								} else if (rs2.IsDBNull(3)) {
									usr.Append("<td align='center' style='font-size:9px;'>&nbsp;" + rs2.GetValue(i + dynamicIdx) + "&nbsp;</td>");
								} else {
									query = string.Format(
										@"
SELECT b.UserBQID
FROM [User] u
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
INNER JOIN UserProfileBQ b ON up.UserProfileID = b.UserProfileID AND b.BQID = {0}
WHERE up.UserID = {1}",
										BQs.Split(':')[i],
										rs2.GetInt32(3)
									);
									SqlDataReader rs3 = Db.rs(query);
									if (rs3.Read()) {
										usr.Append(string.Format("<td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;</td>", rs2.GetValue(i + dynamicIdx)));
									} else {
										usr.Append(string.Format("<td align='center' style='font-size:9px;'>&nbsp;<a href='org.aspx?SDID={0}&UID={1}&BQID={2}'>{3}</a>&nbsp;</td>", showDepartmentID, rs2.GetInt32(3), BQs.Split(':')[i], rs2.GetValue(i + dynamicIdx)));
									}
									rs3.Close();
								}
							}
						}
						usr.Append("<td align='center' style='font-size:9px;'>");
						switch ((rs2.IsDBNull(8) ? 0 : rs2.GetInt32(8))) {
								case 1: usr.Append("Stopped, work related"); break;
								case 2: usr.Append("Education leave"); break;
								case 14: usr.Append("Parental leave"); break;
								case 24: usr.Append("Sick leave"); break;
								case 54: usr.Append("Retired"); break;
								case 34: usr.Append("Do not want to participate"); break;
								case 44: usr.Append("No longer associated"); break;
								case 4: usr.Append("Stopped, other reason"); break;
								case 5: usr.Append("Stopped, unknown reason"); break;
								case 6: usr.Append("Project completed"); break;
								default: break;
						}
						if (!rs2.IsDBNull(7)) {
							if (rs2.IsDBNull(8)) {
								usr.Append("Reactivated");
							}
							usr.Append(" (" + rs2.GetDateTime(7).ToString("yyyy-MM-dd") + ")");
						}
						usr.Append("</td>");
					}
					rs2.Close();

					OrgTree.Text += usr;
					#endregion
				}
			}
			rs.Close();
			OrgTree.Text += "<tr><td colspan='" + (aggrBQcx + 8 + (showDepartmentID != 0 && BQs != "" ? BQs.Split(':').Length : 0) + EScount) + "' style='border-top:1px solid #333333'>&nbsp;</td></tr>";
//			string header = "<td align='center' style='font-size:9px;'>" + totalActive.ToString() + " / " + (totalActivated >= MIN_SHOW ? totalActivated.ToString() : "<img src='img/key.gif'/>") + "</td>";
			string header = string.Format("<td align='center' style='font-size:9px;'>{0}</td>", (totalActive >= MIN_SHOW ? totalActive.ToString() : "<img src='img/key.gif'/>"));
			for (int i = 0; i < EScount; i++) {
				header += string.Format(
					@"<td align='center' style='font-size:9px;'>&nbsp;{0}&nbsp;{1}&nbsp;</td>",
					(
						Convert.ToInt32(ESattr.Split(',')[i].Split(':')[0]) != 0 && Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1]) <= ESanswerCount[i]
						? string.Format("<a href=\"JavaScript:void(window.open('{0}feedback.aspx?R={1}&", ConfigurationManager.AppSettings["eFormURL"], (ESrounds.Split(',')[i])) +
						(ESpreviousRounds.Split(',')[i] != "0" ? "RR=" + ESpreviousRounds.Split(',')[i] + "&" : "") +
						(ESpreviousRounds.Split(',')[i] != "0" ? "R1=" + ESroundTexts.Split(',')[i] + "&" : "") +
						(ESpreviousRounds.Split(',')[i] != "0" ? "R2=" + ESpreviousRoundTexts.Split(',')[i] + "&" : "") +
						"N=" + Server.HtmlEncode(Session["Sponsor"].ToString()).Replace("&", "_0_").Replace("#", "_1_") + "','es" + i + "','scrollbars=1,width=880,height=700,resizable=1,toolbar=0,status=0,menubar=0,location=0'));\"><img src='img/graphIcon2.gif' border='0'/></a>" : ""
					),
					ESanswerCount[i]
				);
			}
			header += "<td align='center' style='font-size:9px;'>" + UX + "</td>";
			header += "<td align='center' style='font-size:9px;'>&nbsp;" + Math.Round(((float)IX / (float)UX) * 100, 0) + "%&nbsp;</td>";
			header += "<td align='center' style='font-size:9px;'>&nbsp;</td>";

			if (aggrBQcx != 0) {
				foreach (string a in aggrBQ.Split(',')) {
					header += "<td align='center'>&nbsp;";
					query = string.Format(
						@"
SELECT AVG(DATEDIFF(year, upbq.ValueDate, GETDATE())), COUNT(upbq.ValueDate)
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfileBQ upbq ON u.UserProfileID = upbq.UserProfileID AND upbq.BQID = {0}
WHERE si.SponsorID = {1}",
						Convert.ToInt32(a),
						sponsorID
					);
					SqlDataReader rs2 = Db.rs(query);
					if (rs2.Read() && !rs2.IsDBNull(0)) {
						header += (rs2.GetInt32(1) >= MIN_SHOW ? rs2.GetValue(0).ToString() : "<img src='img/key.gif'/>");
					}
					rs2.Close();
					header += "&nbsp;</td>";
				}
			}

			OrgTree.Text = OrgTree.Text.Replace("[xxx]", header) + "</table>";
//			foreach (string key in actives.Keys) {
//				OrgTree.Text = OrgTree.Text.Replace(key, (actives[key] / totalActive * 100).ToString("0.0"));
//			}
			#endregion

			if (Session["SuperAdminID"] != null || Session["SponsorAdminID"] != null && Session["SponsorAdminID"].ToString() == "-1") {
				query = string.Format(
					@"
SELECT BQ.BQID,
	BQ.Internal,
	(SELECT COUNT(*) FROM BA WHERE BA.BQID = BQ.BQID)
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0} AND sbq.Organize = 1",
					sponsorID
				);
				rs = Db.rs(query);
				while (rs.Read()) {
					int cx = 0;
					OrgTree.Text += "<table border='0' cellspacing='0' cellpadding='0' style='font-size:12px;line-height:1.0;vertical-align:middle;'>";
					OrgTree.Text += "<tr style='border-bottom:1px solid #333333;'><td><b style='color:#cc0000;'>" + rs.GetString(1) + "</b>&nbsp;</td><td><b>Activated</b>&nbsp;</td>" + ESdesc + "<td><b>Total</b>&nbsp;</td></tr>";
					query = string.Format(
						@"
SELECT BA.BAID,
	BA.Internal,
	COUNT(u.UserID),
	COUNT(si.SponsorInviteID)
	{0}
FROM SponsorInvite si
LEFT OUTER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = {1}
LEFT OUTER JOIN [User] u ON si.UserID = u.UserID
LEFT OUTER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
LEFT OUTER JOIN UserProfileBQ upb ON up.UserProfileID = upb.UserProfileID AND upb.BQID = {2}
LEFT OUTER JOIN BA ON ISNULL(sib.BAID,upb.ValueInt) = BA.BAID
{3}
WHERE si.SponsorID = {4}
GROUP BY BA.BAID, BA.Internal",
						bqESselect,
						rs.GetInt32(0),
						rs.GetInt32(0),
						bqESjoin,
						sponsorID
					);
					SqlDataReader rs2 = Db.rs(query);
					while (rs2.Read()) {
						cx++;
						OrgTree.Text += "<tr style='background-color:#EEEEEE'><td><table border='0' cellspacing='0' cellpadding='0'><tr><td><img src='img/" + (cx == rs.GetInt32(2) ? "L" : "T") + ".gif' width='19' height='20'/></td><td>" + (rs2.IsDBNull(1) ? "?" : rs2.GetString(1)) + "&nbsp;</td></tr></table></td><td align='center'>&nbsp;" + rs2.GetInt32(2) + "&nbsp;</td>";
						for (int i = 0; i < EScount; i++) {
							int idx = 4 + i;
							if (!rs2.IsDBNull(idx) && rs2.GetInt32(idx) >= Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1])) {
								StringBuilder sb = new StringBuilder();
								query = string.Format(
									@"
SELECT usesX.AnswerID
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserSponsorExtendedSurvey usesX ON u.UserID = usesX.UserID AND usesX.SponsorExtendedSurveyID = {0}
LEFT OUTER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = {1}
LEFT OUTER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
LEFT OUTER JOIN UserProfileBQ upb ON up.UserProfileID = upb.UserProfileID AND upb.BQID = {2}
WHERE usesX.AnswerID IS NOT NULL AND si.SponsorID = {3} AND ISNULL(sib.BAID,upb.ValueInt) = {4}",
									Convert.ToInt32(ESattr.Split(',')[i].Split(':')[4]),
									rs.GetInt32(0),
									rs.GetInt32(0),
									sponsorID,
									rs2.GetInt32(0)
								);
								SqlDataReader rs3 = Db.rs(query);
								while (rs3.Read()) {
									sb.Append("," + rs3.GetInt32(0));
								}
								rs3.Close();

								OrgTree.Text += "<td align='center'>&nbsp;" +
									"<a href=\"JavaScript:void(window.open('" + ConfigurationManager.AppSettings["eFormURL"] + "feedback.aspx?" +
									"R=" + (ESrounds.Split(',')[i]) + "&" +
									"AIDS=0" + sb.ToString() + "&" +
									"UD=" + rs2.GetString(1) + "&" +
									"RAC=" + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1]) + "&" +
									"N=" + Server.HtmlEncode(Session["Sponsor"].ToString()).Replace("&", "_0_").Replace("#", "_1_") + "" +
									"','esBQ" + i + "','scrollbars=1,width=880,height=700,resizable=1,toolbar=0,status=0,menubar=0,location=0'));\"><img src='img/graphIcon2.gif' border='0'/></A>" +
									"&nbsp;" + rs2.GetInt32(idx) + "&nbsp;</td>";
							} else if (!rs2.IsDBNull(idx)) {
								OrgTree.Text += "<td align='center' style='color:#EEEEEE'><img src='img/key.gif'/>&nbsp;" + rs2.GetInt32(idx) + "&nbsp;</td>";
							} else {
								OrgTree.Text += "<td align='center'><img src='img/key.gif'/></td>";
							}
						}
						OrgTree.Text += "<td align='center'>&nbsp;" + rs2.GetInt32(3) + "&nbsp;</td></tr>";
					}
					rs2.Close();
					OrgTree.Text += "</table>";
				}
				rs.Close();
			}
		}

		void Search_Click(object sender, EventArgs e)
		{
			if (SearchEmail.Text != "") {
				bool found = false;

				string q = string.Format(
					@"
SELECT si.SponsorInviteID,
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
					SearchResultList.Text += "<tr><td><b>No match found!</b></td></tr>";
				}
				SearchResults.Visible = true;
			}
		}

		void SaveUser_Click(object sender, EventArgs e)
		{
			string url = "";
			SqlDataReader rs;
			bool exists = false;
			int stoppedReason = Convert.ToInt32(StoppedReason.SelectedValue);

			string query = "";
			if (userID == 0) {
				#region create new sponsor invite, if email not already in org
				query = string.Format(
					@"
SELECT UserID
FROM SponsorInvite
WHERE SponsorID = {0} AND Email = '{1}'",
					sponsorID,
					Email.Text.Replace("'", "''")
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
										rs2.GetInt32(1)
									);
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
				#region update SponsorInviteBQ
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
				AddUserError.Text = "A user with this email address already exist!<br/>";
			}
		}

		void Cancel_Click(object sender, EventArgs e)
		{
			Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}

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
	}
}