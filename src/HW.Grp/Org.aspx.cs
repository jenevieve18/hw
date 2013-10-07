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

		private void rewritePRU(int fromSponsorID, int sponsorID, int userID)
		{
			SqlDataReader rs = Db2.rs("SELECT spru.ProjectRoundUnitID, spru.SurveyID FROM SponsorProjectRoundUnit spru WHERE spru.SponsorID = " + sponsorID);
			while (rs.Read()) {
				SqlDataReader rs2 = Db2.rs("SELECT upru.UserProjectRoundUserID, upru.ProjectRoundUserID " +
				                           "FROM UserProjectRoundUser upru " +
				                           "INNER JOIN [user] hu ON upru.UserID = hu.UserID " +
				                           "INNER JOIN [eform]..[ProjectRoundUser] pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID " +
				                           "INNER JOIN [eform]..[ProjectRoundUnit] u ON pru.ProjectRoundUnitID = u.ProjectRoundUnitID " +
				                           "WHERE hu.SponsorID = " + fromSponsorID + " " +
				                           "AND u.SurveyID = " + rs.GetInt32(1) + " " +
				                           "AND upru.UserID = " + userID);
				while (rs2.Read()) {
					Db2.exec("UPDATE UserProjectRoundUser SET ProjectRoundUnitID = " + rs.GetInt32(0) + " WHERE UserProjectRoundUserID = " + rs2.GetInt32(0));
					Db2.exec("UPDATE [eform]..[ProjectRoundUser] SET ProjectRoundUnitID = " + rs.GetInt32(0) + " WHERE ProjectRoundUserID = " + rs2.GetInt32(1));
					Db2.exec("UPDATE [eform]..[Answer] SET ProjectRoundUnitID = " + rs.GetInt32(0) + " WHERE ProjectRoundUserID = " + rs2.GetInt32(1));
				}
				rs2.Close();
			}
			rs.Close();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			SearchResultList.Text = "";
			SearchResults.Visible = false;
			ActionNav.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act1.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act2.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act3.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act4.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act5.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act6.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act7.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act8.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);
			//act9.Visible = (Convert.ToInt32(Session["ReadOnly"]) == 0);

			sponsorID = Convert.ToInt32(Session["SponsorID"]);

			if (sponsorID != 0) {
				if (Request.QueryString["ShowReg"] != null && Convert.ToInt32(Session["ReadOnly"]) == 0) {
					showReg = true;
				} else if (Convert.ToInt32(Session["SeeUsers"]) == 1 && Convert.ToInt32(Session["ReadOnly"]) == 0) {
					showReg = true;
				}
				deptID = (Request.QueryString["DID"] != null ? Convert.ToInt32(Request.QueryString["DID"]) : 0);
				deleteDepartmentID = (Request.QueryString["DeleteDID"] != null ? Convert.ToInt32(Request.QueryString["DeleteDID"]) : 0);
				showDepartmentID = (Request.QueryString["SDID"] != null ? Convert.ToInt32(Request.QueryString["SDID"]) : 0);
				userID = (Request.QueryString["UID"] != null ? Convert.ToInt32(Request.QueryString["UID"]) : 0);
				deleteUserID = (Request.QueryString["DeleteUID"] != null ? Convert.ToInt32(Request.QueryString["DeleteUID"]) : 0);
				sendSponsorInvitationID = (Request.QueryString["SendSPIID"] != null ? Convert.ToInt32(Request.QueryString["SendSPIID"]) : 0);

				SqlDataReader rs;

				if (Request.QueryString["PESSIID"] != null && Request.QueryString["Flip"] != null) {
					Db2.exec("UPDATE SponsorInvite SET PreviewExtendedSurveys = " + (Request.QueryString["Flip"] == "1" ? "1" : "NULL") + " WHERE SponsorInviteID = " + Convert.ToInt32(Request.QueryString["PESSIID"]));
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["ConnectSPIID"] != null) {
					rs = Db.rs("SELECT UserID, SponsorID FROM [User] WHERE UserID = " + Convert.ToInt32(Request.QueryString["WithUID"]));
					if (rs.Read()) {
						rewritePRU(rs.GetInt32(1), sponsorID, rs.GetInt32(0));
						Db2.exec("UPDATE SponsorInvite SET UserID = NULL WHERE UserID = " + rs.GetInt32(0));
						Db2.exec("UPDATE SponsorInvite SET UserID = " + rs.GetInt32(0) + ", Sent = GETDATE() WHERE SponsorInviteID = " + Convert.ToInt32(Request.QueryString["ConnectSPIID"]));
						Db2.exec("UPDATE [User] SET DepartmentID = " + Convert.ToInt32(Request.QueryString["AndDID"]) + ", SponsorID = " + sponsorID + " WHERE UserID = " + rs.GetInt32(0));
						Db2.exec("UPDATE UserProfile SET DepartmentID = " + Convert.ToInt32(Request.QueryString["AndDID"]) + ", SponsorID = " + sponsorID + " WHERE UserID = " + rs.GetInt32(0));
					}
					rs.Close();
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["ReclaimUID"] != null && Request.QueryString["ReclaimAID"] != null) {
					Db2.exec("UPDATE UserSponsorExtendedSurvey SET AnswerID = NULL WHERE ProjectRoundUserID = " + Request.QueryString["ReclaimUID"] + " AND AnswerID = " + Request.QueryString["ReclaimAID"]);
					Db2.exec("UPDATE Answer SET EndDT = NULL WHERE ProjectRoundUserID = " + Request.QueryString["ReclaimUID"] + " AND AnswerID = " + Request.QueryString["ReclaimAID"], "eFormSqlConnection");
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["SubmitUID"] != null && Request.QueryString["SubmitAID"] != null) {
					Db2.exec("UPDATE UserSponsorExtendedSurvey SET AnswerID = " + Convert.ToInt32(Request.QueryString["SubmitAID"]) + " WHERE AnswerID IS NULL AND ProjectRoundUserID = " + Convert.ToInt32(Request.QueryString["SubmitUID"]));
					Db2.exec("UPDATE Answer SET EndDT = GETDATE() WHERE ProjectRoundUserID = " + Convert.ToInt32(Request.QueryString["SubmitUID"]) + " AND AnswerID = " + Convert.ToInt32(Request.QueryString["SubmitAID"]), "eFormSqlConnection");
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["BQID"] != null) {
					rs = Db.rs("SELECT " +
					           "sib.BAID, " +              // 0
					           "sib.ValueInt, " +          // 1
					           "sib.ValueText, " +         // 2
					           "sib.ValueDate, " +         // 3
					           "bq.Type, " +               // 4
					           "up.UserProfileID " +       // 5
					           "FROM SponsorInvite si " +
					           "INNER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = " + Convert.ToInt32(Request.QueryString["BQID"]) + " " +
					           "INNER JOIN bq ON sib.BQID = bq.BQID " +
					           "INNER JOIN [User] u ON si.UserID = u.UserID " +
					           "INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
					           "LEFT OUTER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = bq.BQID " +
					           "WHERE upbq.UserBQID IS NULL " +
					           "AND si.UserID = " + Convert.ToInt32(Request.QueryString["UID"]) + " " +
					           "AND si.SponsorID = " + sponsorID);
					if (rs.Read()) {
						Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + rs.GetInt32(5) + "," + Convert.ToInt32(Request.QueryString["BQID"]) + "," +
						         (rs.IsDBNull(1) ? (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) : rs.GetInt32(1).ToString()) + "," +
						         (rs.IsDBNull(2) ? "NULL" : "'" + rs.GetString(2).Replace("'", "") + "'") + "," +
						         (rs.IsDBNull(3) ? "NULL" : "'" + rs.GetDateTime(3).ToString("yyyy-MM-dd") + "'") +
						         ")");
					}
					rs.Close();
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (Request.QueryString["SendExtra"] != null) {
					#region Send extra
					rs = Db2.rs("SELECT " +
					            "ses.ExtraEmailBody, " +           // 0
					            "ses.ExtraEmailSubject, " +        // 1
					            "u.Email, " +                      // 2
					            "u.UserID, " +                     // 3
					            "u.ReminderLink, " +               // 4
					            "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12) " +
					            "FROM [User] u " +
					            "INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = " + Convert.ToInt32(Request.QueryString["SESID"]) + " " +
					            "WHERE u.UserID = " + Convert.ToInt32(Request.QueryString["SendExtra"]));
					if (rs.Read())
					{
						string body = rs.GetString(0);

						string personalLink = "" + ConfigurationSettings.AppSettings["healthWatchURL"] + "";
						if (!rs.IsDBNull(4) && rs.GetInt32(4) > 0)
						{
							personalLink += "c/" + rs.GetString(5).ToLower() + rs.GetInt32(3).ToString();
						}
						if (body.IndexOf("<LINK/>") >= 0)
						{
							body = body.Replace("<LINK/>", personalLink);
						}
						else
						{
							body += "\r\n\r\n" + personalLink;
						}

//						Db2.sendMail(rs.GetString(2).Trim(), body, rs.GetString(1));
						Db.sendMail(rs.GetString(2).Trim(), body, rs.GetString(1));
					}
					rs.Close();
					#endregion
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
				if (sendSponsorInvitationID != 0) {
					#region Resend
					rs = Db2.rs("SELECT " +
					            "s.InviteTxt, " +           // 0
					            "s.InviteSubject, " +       // 1
					            "si.Email, " +              // 2
					            "LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8), " +
					            "si.UserID, " +             // 4
					            "u.ReminderLink, " +        // 5
					            "LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12), " +
					            "s.LoginTxt, " +            // 7
					            "s.LoginSubject " +         // 8
					            "FROM Sponsor s " +
					            "INNER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID " +
					            "LEFT OUTER JOIN [User] u ON u.UserID = si.UserID " +
					            "WHERE s.SponsorID = " + sponsorID + " AND si.SponsorInviteID = " + sendSponsorInvitationID);
					if (rs.Read())
					{
						if (rs.IsDBNull(4))
						{
//							Db2.sendInvitation(sendSponsorInvitationID, rs.GetString(2).Trim(), rs.GetString(0), rs.GetString(1), rs.GetString(3));
							sponsorRepository.UpdateSponsorInviteSent(sendSponsorInvitationID);
							Db.sendInvitation(sendSponsorInvitationID, rs.GetString(2).Trim(), rs.GetString(0), rs.GetString(1), rs.GetString(3));
						}
						else
						{
							string body = rs.GetString(7);

							string personalLink = "" + ConfigurationSettings.AppSettings["healthWatchURL"] + "";
							if (!rs.IsDBNull(5) && rs.GetInt32(5) > 0)
							{
								personalLink += "c/" + rs.GetString(6).ToLower() + rs.GetInt32(4).ToString();
							}
							if (body.IndexOf("<LINK/>") >= 0)
							{
								body = body.Replace("<LINK/>", personalLink);
							}
							else
							{
								body += "\r\n\r\n" + personalLink;
							}

//							Db2.sendMail(rs.GetString(2).Trim(), body, rs.GetString(8));
							Db.sendMail(rs.GetString(2).Trim(), body, rs.GetString(8));
						}
					}
					rs.Close();
					#endregion
					Response.Redirect("org.aspx?SDID=" + showDepartmentID + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}

				#region Populate hidden variables
				rs = Db2.rs("SELECT " +
				            "BQ.Internal, " +
				            "BQ.BQID, " +
				            "BQ.Type " +
				            "FROM SponsorBQ sbq " +
				            "INNER JOIN BQ ON sbq.BQID = BQ.BQID " +
				            "WHERE sbq.SponsorID = " + sponsorID + " " +
				            "AND sbq.Hidden = 1 " +
				            "ORDER BY sbq.SortOrder");
				while (rs.Read()) {
					Hidden.Controls.Add(new LiteralControl("<span class=\"desc\">" + rs.GetString(0) + "</span>"));
					if (rs.GetInt32(2) == 7 || rs.GetInt32(2) == 1) {
						DropDownList rbl = new DropDownList();
						rbl.ID = "Hidden" + rs.GetInt32(1);
						rbl.Items.Add(new ListItem("-", "NULL"));
						SqlDataReader rs2 = Db2.rs("SELECT BAID, Internal FROM BA WHERE BQID = " + rs.GetInt32(1) + " ORDER BY SortOrder");
						while (rs2.Read())
						{
							rbl.Items.Add(new ListItem(rs2.GetString(1), rs2.GetInt32(0).ToString()));
						}
						rs2.Close();
						Hidden.Controls.Add(rbl);
					}
					//else if (rs.GetInt32(2) == 1)
					//{
					//    RadioButtonList rbl = new RadioButtonList();
					//    rbl.ID = "Hidden" + rs.GetInt32(1);
					//    rbl.RepeatDirection = RepeatDirection.Horizontal;
					//    rbl.RepeatLayout = RepeatLayout.Flow;
					//    SqlDataReader rs2 = Db2.rs("SELECT BAID, Internal FROM BA WHERE BQID = " + rs.GetInt32(1) + " ORDER BY SortOrder");
					//    while (rs2.Read())
					//    {
					//        rbl.Items.Add(new ListItem(rs2.GetString(1), rs2.GetInt32(0).ToString()));
					//    }
					//    rs2.Close();
					//    Hidden.Controls.Add(rbl);
					//}
					else if (rs.GetInt32(2) == 4 || rs.GetInt32(2) == 2) {
						TextBox tb = new TextBox();
						tb.ID = "Hidden" + rs.GetInt32(1);
						tb.Width = Unit.Pixel(150);
						Hidden.Controls.Add(tb);
						if (rs.GetInt32(2) == 2)
						{
							hiddenBqJoin += "LEFT OUTER JOIN SponsorInviteBQ upb" + rs.GetInt32(1) + " ON si.SponsorInviteID = upb" + rs.GetInt32(1) + ".SponsorInviteID AND upb" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1) + " ";
							hiddenBqWhere += " OR upb" + rs.GetInt32(1) + ".ValueText LIKE [x]";
						}
					}
					else if (rs.GetInt32(2) == 3) {
						DropDownList ddl = new DropDownList();
						ddl.ID = "Hidden" + rs.GetInt32(1) + "Y";
						ddl.Items.Add(new ListItem("-", "0"));
						for (int i = 1900; i <= DateTime.Now.Year; i++)
						{
							ddl.Items.Add(new ListItem(i.ToString(), i.ToString()));
						}
						Hidden.Controls.Add(ddl);

						ddl = new DropDownList();
						ddl.ID = "Hidden" + rs.GetInt32(1) + "M";
						ddl.Items.Add(new ListItem("-", "0"));
						for (int i = 1; i <= 12; i++)
						{
							ddl.Items.Add(new ListItem(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[i - 1], i.ToString()));
						}
						Hidden.Controls.Add(ddl);

						ddl = new DropDownList();
						ddl.ID = "Hidden" + rs.GetInt32(1) + "D";
						ddl.Items.Add(new ListItem("-", "0"));
						for (int i = 1; i <= 31; i++)
						{
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
					rs = Db2.rs("SELECT " +
					            "d.DepartmentID, " +
					            "dbo.cf_departmentTree(d.DepartmentID,' » ') " +
					            "FROM Department d " +
					            (Session["SponsorAdminID"].ToString() != "-1" ?
					             "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
					             "WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " " +
					             "AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
					            "ORDER BY d.SortString");
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
						rs = Db2.rs("SELECT d.SortString, d.ParentDepartmentID, d.Department, d.DepartmentShort FROM Department d WHERE d.SponsorID = " + sponsorID + " AND d.DepartmentID = " + deptID);
						if (rs.Read()) {
							sortString = rs.GetString(0);
							if (!rs.IsDBNull(1))
							{
								parentDepartmentID = rs.GetInt32(1).ToString();
							}
							department = rs.GetString(2);
							departmentShort = rs.GetString(3);
						}
						rs.Close();
						rs = Db2.rs("SELECT d.DepartmentID, dbo.cf_departmentTree(d.DepartmentID,' » ') " +
						            "FROM Department d " +
						            (Session["SponsorAdminID"].ToString() != "-1" ?
						             "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
						             "WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " " +
						             "AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
						            "AND LEFT(d.SortString," + sortString.Length + ") <> '" + sortString + "' " +
						            "ORDER BY d.SortString");
						while (rs.Read()) {
							ParentDepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
						}
						rs.Close();

						Department.Text = department;
						DepartmentShort.Text = departmentShort;
						ParentDepartmentID.SelectedValue = parentDepartmentID;
					} else {
						rs = Db2.rs("SELECT " +
						            "d.DepartmentID, " +
						            "dbo.cf_departmentTree(d.DepartmentID,' » ') " +
						            "FROM Department d " +
						            (Session["SponsorAdminID"].ToString() != "-1" ?
						             "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
						             "WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " " +
						             "AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
						            "ORDER BY d.SortString");
						while (rs.Read()) {
							ParentDepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
						}
						rs.Close();
					}
					rs = Db2.rs("SELECT " +
					            "d.DepartmentID, " +
					            "dbo.cf_departmentTree(d.DepartmentID,' » ') " +
					            "FROM Department d " +
					            (Session["SponsorAdminID"].ToString() != "-1" ?
					             "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
					             "WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " " +
					             "AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
					            "ORDER BY d.SortString");
					while (rs.Read()) {
						DepartmentID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
					}
					rs.Close();

					if (userID != 0) {
						rs = Db2.rs("SELECT Email, DepartmentID, StoppedReason, Stopped FROM SponsorInvite WHERE SponsorInviteID = " + userID);
						if (rs.Read()) {
							Email.Text = rs.GetString(0);
							DepartmentID.SelectedValue = (rs.IsDBNull(1) ? "NULL" : rs.GetInt32(1).ToString());
							StoppedReason.Items.FindByValue((rs.IsDBNull(2) ? "0" : rs.GetInt32(2).ToString())).Selected = true;
							Stopped.Text = (rs.IsDBNull(3) ? DateTime.Today.ToString("yyyy-MM-dd") : rs.GetDateTime(3).ToString("yyyy-MM-dd"));
						}
						rs.Close();
						rs = Db2.rs("SELECT s.BQID, s.BAID, BQ.Type, s.ValueInt, s.ValueDate, s.ValueText, BQ.Restricted FROM SponsorInviteBQ s INNER JOIN BQ ON BQ.BQID = s.BQID WHERE s.SponsorInviteID = " + userID);
						while (rs.Read()) {
							if (rs.GetInt32(2) == 7 || rs.GetInt32(2) == 1) {
								if (Hidden.FindControl("Hidden" + rs.GetInt32(0)) != null && !rs.IsDBNull(1)) {
									((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue = rs.GetInt32(1).ToString();
								}
							}
							//else if(rs.GetInt32(2) == 1)
							//{
							//    if (Hidden.FindControl("Hidden" + rs.GetInt32(0)) != null && !rs.IsDBNull(1))
							//    {
							//        ((RadioButtonList)Hidden.FindControl("Hidden" + rs.GetInt32(0))).SelectedValue = rs.GetInt32(1).ToString();
							//    }
							//}
							else if (rs.GetInt32(2) == 2) {
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
						rs = Db2.rs("SELECT Email FROM SponsorInvite WHERE SponsorInviteID = " + deleteUserID);
						if (rs.Read()) {
							DeleteUserEmail.Text = rs.GetString(0);
						}
						rs.Close();
					}

					if (deleteDepartmentID != 0) {
						rs = Db2.rs("SELECT dbo.cf_departmentTree(d.DepartmentID,' » ') FROM Department d WHERE d.DepartmentID = " + deleteDepartmentID);
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

//		public static bool isEmail(string inputEmail)
//		{
//			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
//				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
//				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
//			Regex re = new Regex(strRegex);
//			if (re.IsMatch(inputEmail))
//				return true;
//			else
//				return false;
//		}
//
		void SaveImportUser_Click(object sender, EventArgs e)
		{
			// 0 Email
			// 1 DepartmentShort
			// n Background
			// n+1 Stopped reason
			if (UsersFilename.PostedFile != null && UsersFilename.PostedFile.ContentLength != 0)
			{
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
				foreach (string a in sa)
				{
					//Response.Write(a + "<BR>");
					string email = a.Split('\t')[0].Replace("'", "").Trim().ToLower();
					if (email != "Email" && email != "")
					{
//						if (!isEmail(email))
						if (!Db.isEmail(email))
						{
							valid = false;
							ImportUsersError.Text += "Error: Invalid email-address \"" + email + "\"<BR/>";
						}
						else if (emails.Contains(email))
						{
							valid = false;
							ImportUsersError.Text += "Error: Duplicate email-address \"" + email + "\"<BR/>";
						}
						else
						{
							emails.Add(email);
						}
						//rs = Db2.rs("SELECT SponsorInviteID FROM SponsorInvite WHERE Email = '" + email + "' AND SponsorID = " + sponsorID);
						//if (rs.Read())
						//{
						//    valid = false;
						//    ImportUsersError.Text += "Error: Email-address \"" + email + "\" already exist<BR/>";
						//}
						//rs.Close();
						if (a.Split('\t').Length > 1)
						{
							string unitID = a.Split('\t')[1].Replace("'", "").ToLower().Trim();
							if (unitID != "" && (","+units).IndexOf(","+unitID) < 0)
							{
								units += (units != "" ? "," : "") + unitID + "";
							}
						}
					}
				}
				//Response.End();

				System.Collections.Hashtable existingUnits = new System.Collections.Hashtable();
				rs = Db2.rs("SELECT DepartmentShort, DepartmentID FROM Department WHERE DepartmentShort IS NOT NULL AND SponsorID = " + sponsorID);
				while (rs.Read())
				{
					existingUnits.Add(rs.GetString(0).ToLower().Trim(), rs.GetInt32(1));
				}
				rs.Close();

				foreach (string u in units.Split(','))
				{
					//Response.Write(u + "<BR>");
					if (!existingUnits.Contains(u) && u != "")
					{
						valid = false;
						ImportUsersError.Text += "Error: Unit with ID \"" + u + "\" does not exist<BR/>";
					}
				}
				//Response.End();

				string extra = "", extraType = ""; int extraCount = 0;
				rs = Db2.rs("SELECT s.BQID, b.Type FROM SponsorBQ s INNER JOIN BQ b ON s.BQID = b.BQID WHERE s.Hidden = 1 AND s.SponsorID = " + sponsorID + " ORDER BY s.SortOrder");
				while (rs.Read())
				{
					extraCount++;
					extra += (extra != "" ? "," : "") + rs.GetInt32(0).ToString();
					extraType += (extraType != "" ? "," : "") + rs.GetInt32(1).ToString();
				}
				rs.Close();

				if (valid)
				{
					foreach (string a in sa)
					{
						string[] u = a.Split('\t');
						string email = u[0].Replace("'", "").Trim();

						if (email != "Email" && email != "")
						{
							string unit = ImportUsersParentDepartmentID.SelectedValue.Replace("'", "");

							if (u.Length > 1)
							{
								string unitID = u[1].Replace("'", "").ToLower().Trim();
								if (unitID != "")
								{
									unit = existingUnits[unitID].ToString();
								}
							}

							int uid = 0, stoppedReason = 0;
							DateTime stopped = DateTime.MinValue;

							rs = Db2.rs("SELECT SponsorInviteID, Stopped, StoppedReason FROM SponsorInvite WHERE Email = '" + email + "' AND SponsorID = " + sponsorID);
							if (rs.Read())
							{
								uid = rs.GetInt32(0);
								if (!rs.IsDBNull(1))
								{
									stopped = rs.GetDateTime(1);
								}
								if (!rs.IsDBNull(2))
								{
									stoppedReason = rs.GetInt32(2);
								}
							}
							rs.Close();

							if (u.Length > 2 + extraCount && u[2 + extraCount] != "")
							{
								if (stoppedReason != Convert.ToInt32(u[2 + extraCount]))
								{
									stoppedReason = Convert.ToInt32(u[2 + extraCount]);
									stopped = DateTime.Now;
								}
							}
							if (uid != 0)
							{
								Db2.exec("UPDATE SponsorInvite SET DepartmentID = " + unit + ", Stopped = " + (stopped != DateTime.MinValue ? "'" + stopped.ToString("yyyy-MM-dd") + "'" : "NULL") + ", StoppedReason = " + (stoppedReason != 0 ? stoppedReason.ToString() : "NULL") + " WHERE SponsorInviteID = " + uid);

								rs = Db2.rs("SELECT u.UserID FROM [User] u INNER JOIN SponsorInvite si ON u.UserID = si.UserID WHERE si.SponsorInviteID = " + uid);
								while (rs.Read())
								{
									Db2.exec("UPDATE [User] SET DepartmentID = " + unit + " " +
									         "WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);
									Db2.exec("UPDATE UserProfile SET DepartmentID = " + unit + " " +
									         "WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);
								}
								rs.Close();
							}
							else
							{
								rs = Db2.rs("SET NOCOUNT ON;SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;BEGIN TRAN;INSERT INTO SponsorInvite (" +
								            "SponsorID," +
								            "DepartmentID," +
								            "Email," +
								            "Stopped, " +
								            "StoppedReason " +
								            ") VALUES (" +
								            "" + sponsorID + "," +
								            "" + unit + "," +
								            "'" + email + "'," +
								            "" + (stopped != DateTime.MinValue ? "'" + stopped.ToString("yyyy-MM-dd") + "'" : "NULL") + "," +
								            "" + (stoppedReason != 0 ? stoppedReason.ToString() : "NULL") + "" +
								            ");SELECT SponsorInviteID FROM [SponsorInvite] WHERE SponsorID=" + sponsorID + " AND Email = '" + email + "' ORDER BY SponsorInviteID DESC;COMMIT;");
								if (rs.Read())
								{
									uid = rs.GetInt32(0);
								}
								rs.Close();
							}
							string[] extras = extra.Split(',');
							string[] extraTypes = extraType.Split(',');
							//Response.Write(extra + "<BR/>");
							for (int i = 0; i < extraCount; i++)
							{
								//Response.Write(extras[i] + "<BR/>");
								//Response.Write(u[2 + i] + "<BR/>");
								if (u.Length > 2 + i && u[2 + i] != "" && extras[i].ToString() != "")
								{
									// Added after code sent to Ian, JPE 121214
									Db.exec("UPDATE SponsorInviteBQ SET SponsorInviteID = -ABS(SponsorInviteID) WHERE SponsorInviteID = " + uid + " AND BQID = " + extras[i]);

									if (extraTypes[i] == "1" || extraTypes[i] == "7")
									{
										rs = Db2.rs("SELECT BAID FROM BA WHERE BQID = " + extras[i] + " AND Value = " + Convert.ToInt32(u[2 + i]));
										if (rs.Read())
										{
											//Response.Write(rs.GetInt32(0) + "<BR/>");
											Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,BAID) VALUES (" + uid + "," + extras[i] + "," + rs.GetInt32(0) + ")");
										}
										rs.Close();
									}
									else if (extraTypes[i] == "2")
									{
										try
										{
											Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueText) VALUES (" + uid + "," + extras[i] + ",'" + u[2 + i].Replace("'", "''") + "')");
										}
										catch (Exception) { }
									}
									else if (extraTypes[i] == "4")
									{
										try
										{
											Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueInt) VALUES (" + uid + "," + extras[i] + "," + Convert.ToInt32(u[2 + i]) + ")");
										}
										catch (Exception) { }
									}
									else if (extraTypes[i] == "3")
									{
										try
										{
											Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueDate) VALUES (" + uid + "," + extras[i] + ",'" + Convert.ToDateTime(u[2 + i]) + "')");
										}
										catch (Exception) { }
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
			if (UnitsFilename.PostedFile != null && UnitsFilename.PostedFile.ContentLength != 0)
			{
				System.IO.StreamReader f = new System.IO.StreamReader(UnitsFilename.PostedFile.InputStream, System.Text.Encoding.Default);
				string s = f.ReadToEnd();
				f.Close();
				s = s.Replace("\r", "\n");
				s = s.Replace("\n\n", "\n");
				string[] sa = s.Split('\n');

				string units = "''", parentUnits = "''";
				bool valid = true;
				ImportUnitsError.Text = "";

				foreach (string a in sa)
				{
					string id = a.Split('\t')[0].Replace("'", "");
					if (id != "ID" && id != "")
					{
						string parentID = a.Split('\t')[1].Replace("'", "");
						units += ",'" + id + "'";
						if (parentID != "")
						{
							parentUnits += ",'" + parentID + "'";
						}
					}
				}

				// Check if any of the new IDs already exist
				SqlDataReader rs = Db2.rs("SELECT dbo.cf_DepartmentTree(DepartmentID,' » '), DepartmentShort FROM Department WHERE SponsorID = " + sponsorID + " AND DepartmentShort IN (" + units + ")");
				while (rs.Read())
				{
					valid = false;
					ImportUnitsError.Text += "Error: Unit with ID \"" + rs.GetString(1) + "\" already exist (" + rs.GetString(0) + ")<BR/>";
				}
				rs.Close();

				// Add all present IDs
				rs = Db2.rs("SELECT DepartmentShort FROM Department WHERE DepartmentShort IS NOT NULL AND SponsorID = " + sponsorID);
				while (rs.Read())
				{
					units += ",'" + rs.GetString(0).Replace("'", "") + "'";
				}
				rs.Close();

				// Check if any of the parent IDs can't be matched
				foreach (string p in parentUnits.Split(','))
				{
					if (units.IndexOf(p) < 0)
					{
						valid = false;
						ImportUnitsError.Text += "Error: Unit with ID \"" + p + "\" specified as parent unit does not exist<BR/>";
					}
				}

				if (valid)
				{
					foreach (string a in sa)
					{
						string[] u = a.Split('\t');
						string id = u[0].Replace("'", "");

						if (id != "ID" && id != "")
						{
							string unit = u[2].Replace("'", "");

							// Insert new department
							rs = Db2.rs("SET NOCOUNT ON;" +
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
							            "SELECT DepartmentID FROM [Department] WHERE SponsorID=" + sponsorID + " AND DepartmentShort = '" + id + "' ORDER BY DepartmentID DESC;COMMIT;");
							if (rs.Read())
							{
								// Update sort order
								Db2.exec("UPDATE Department SET SortOrder = " + rs.GetInt32(0) + " WHERE DepartmentID = " + rs.GetInt32(0));

								if (Session["SponsorAdminID"].ToString() != "-1")
								{
									// Add to sponsor admin mapping
									Db2.exec("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES (" + Session["SponsorAdminID"] + "," + rs.GetInt32(0) + ")");
								}
							}
							rs.Close();
						}
					}
					foreach (string a in sa)
					{
						string[] u = a.Split('\t');
						string id = u[0].Replace("'", "");

						if (id != "ID" && id != "")
						{
							// Loop through all new departments
							rs = Db2.rs("SELECT DepartmentID FROM Department WHERE DepartmentShort = '" + id + "' AND SponsorID = " + sponsorID);
							if (rs.Read())
							{
								string parentDepartmentID = ImportUnitsParentDepartmentID.SelectedValue.Replace("'", "");

								if (u[1] != "")
								{
									// Fetch parent department ID
									SqlDataReader rs2 = Db2.rs("SELECT DepartmentID FROM Department WHERE DepartmentShort = '" + u[1].Replace("'", "") + "' AND SponsorID = " + sponsorID);
									if (rs2.Read())
									{
										parentDepartmentID = rs2.GetInt32(0).ToString();
									}
									rs2.Close();
								}

								if (parentDepartmentID != "NULL")
								{
									// Update new department with parent department
									Db2.exec("UPDATE Department SET ParentDepartmentID = " + parentDepartmentID + " WHERE DepartmentID = " + rs.GetInt32(0));
								}
							}
							rs.Close();
						}
					}

					Db2.exec("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = " + sponsorID);

					Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
				}
			}
		}

		void SaveDeleteDepartment_Click(object sender, EventArgs e)
		{
			SqlDataReader rs = Db2.rs("SELECT ParentDepartmentID FROM Department WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			if (rs.Read())
			{
				Db2.exec("UPDATE [User] SET DepartmentID = " + (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) + " WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
				Db2.exec("UPDATE UserProfile SET DepartmentID = " + (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) + " WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
				Db2.exec("UPDATE Department SET ParentDepartmentID = " + (rs.IsDBNull(0) ? "NULL" : rs.GetInt32(0).ToString()) + " WHERE SponsorID = " + sponsorID + " AND ParentDepartmentID = " + deleteDepartmentID);
			}
			rs.Close();
			Db2.exec("UPDATE Department SET SponsorID = -ABS(SponsorID) WHERE SponsorID = " + sponsorID + " AND DepartmentID = " + deleteDepartmentID);
			Db2.exec("UPDATE SponsorAdminDepartment SET DepartmentID = -ABS(DepartmentID) WHERE DepartmentID = " + deleteDepartmentID);
			Db2.exec("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = " + sponsorID + "");

			Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}

		void SaveDeleteUser_Click(object sender, EventArgs e)
		{
			SqlDataReader rs = Db2.rs("SELECT si.UserID FROM SponsorInvite si WHERE si.SponsorInviteID = " + deleteUserID);
			if (rs.Read() && !rs.IsDBNull(0))
			{
				if (DeleteUserFrom.SelectedValue == "0")
				{
					#region Update all
					Db2.exec("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);
					Db2.exec("UPDATE UserProfile SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);

					rewritePRU(sponsorID, 1, rs.GetInt32(0));

					#region Delete hidden variables - REMOVED
					/*
				SqlDataReader rs2 = Db2.rs("SELECT UserProfileID FROM UserProfile WHERE UserID = " + rs.GetInt32(0));
				while (rs2.Read())
                {
                   
                    SqlDataReader rs3 = Db2.rs("SELECT BQ.BQID FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
					while (rs3.Read())
					{
						Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + rs2.GetInt32(0));
					}
					rs3.Close();
                   
                }
				rs2.Close();
					 */
					#endregion
					#endregion
				}
				else
				{
					// HOW ABOUT rewritePRU here?

					#region Update from now
					Db2.exec("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(0) + " AND SponsorID = " + sponsorID);

					SqlDataReader rs2 = Db2.rs("SELECT u.UserProfileID, up.ProfileComparisonID FROM [User] u INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID WHERE u.UserID = " + rs.GetInt32(0));
					while (rs2.Read())
					{
						#region Create new profile
						Db2.exec("INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created) VALUES (" + rs.GetInt32(0) + ",1,NULL," + rs2.GetInt32(1) + ",GETDATE())");
						int profileID = 0;
						SqlDataReader rs3 = Db2.rs("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + rs.GetInt32(0) + " ORDER BY UserProfileID DESC");
						if (rs3.Read())
						{
							profileID = rs3.GetInt32(0);
						}
						rs3.Close();
						#endregion

						#region Copy old profile
						rs3 = Db2.rs("SELECT BQID, ValueInt, ValueText, ValueDate FROM UserProfileBQ WHERE UserProfileID = " + rs2.GetInt32(0));
						while (rs3.Read())
						{
							Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," +
							         (rs3.IsDBNull(1) ? "NULL" : rs3.GetInt32(1).ToString()) + "," +
							         (rs3.IsDBNull(2) ? "NULL" : "'" + rs3.GetString(2).Replace("'", "") + "'") + "," +
							         (rs3.IsDBNull(3) ? "NULL" : "'" + rs3.GetDateTime(3).ToString("yyyy-MM-dd") + "'") +
							         ")");
						}
						rs3.Close();
						#endregion

						#region Delete new hidden variables - REMOVED
						/*
					rs3 = Db2.rs("SELECT BQ.BQID FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
					while (rs3.Read())
					{
						Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
					}
					rs3.Close();
						 */
						#endregion

						Db2.exec("UPDATE [User] SET UserProfileID = " + profileID + " WHERE UserID = " + rs.GetInt32(0));
					}
					rs2.Close();
					#endregion
				}
			}
			rs.Close();
			Db2.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE SponsorInviteID = " + deleteUserID);
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

			string aggrBQ = "", aggrBRdesc = "";
			int aggrBQcx = 0;
			SqlDataReader rs = Db2.rs("SELECT " +
			                          "BQ.Internal, " +           // 0
			                          "BQ.BQID, " +               // 1
			                          "BQ.Type, " +               // 2
			                          "sbq.Hidden, " +            // 3
			                          "sbq.InGrpAdmin, " +        // 4
			                          "sbq.Fn, " +                // 5
			                          "BQ.InternalAggregate, " +  // 6
			                          "BQ.Restricted " +          // 7
			                          "FROM SponsorBQ sbq " +
			                          "INNER JOIN BQ ON sbq.BQID = BQ.BQID " +
			                          "WHERE sbq.SponsorID = " + sponsorID + " AND (sbq.Hidden = 1 OR sbq.InGrpAdmin = 1) " +
			                          "ORDER BY sbq.SortOrder");
			while (rs.Read())
			{
				if (!rs.IsDBNull(3) && rs.GetInt32(3) == 1)
				{
					if (!rs.IsDBNull(7))
					{
						// Changed after code sent to Ian, JPE 121214
						select += ", LEFT(CAST(y" + rs.GetInt32(1) + ".SponsorInviteID AS VARCHAR(8)),0)+'*****' AS XI" + rs.GetInt32(1) + "";
						join += "" +
							" LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					}
					else if (rs.GetInt32(2) == 1 || rs.GetInt32(2) == 7)
					{
						select += ", x" + rs.GetInt32(1) + ".Internal AS XI" + rs.GetInt32(1) + "";
						join += "" +
							" LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1) +
							" LEFT OUTER JOIN BA x" + rs.GetInt32(1) + " ON x" + rs.GetInt32(1) + ".BQID = y" + rs.GetInt32(1) + ".BQID AND y" + rs.GetInt32(1) + ".BAID = x" + rs.GetInt32(1) + ".BAID";
					}
					else if (rs.GetInt32(2) == 2)
					{
						select += ", y" + rs.GetInt32(1) + ".ValueText AS XI" + rs.GetInt32(1) + "";
						join += "" +
							" LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					}
					else if (rs.GetInt32(2) == 4)
					{
						select += ", y" + rs.GetInt32(1) + ".ValueInt AS XI" + rs.GetInt32(1) + "";
						join += "" +
							" LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					}
					else if (rs.GetInt32(2) == 3)
					{
						select += ", '' + CAST(DATEPART(yyyy,y" + rs.GetInt32(1) + ".ValueDate) AS VARCHAR(4)) + '-' + CAST(DATEPART(mm,y" + rs.GetInt32(1) + ".ValueDate) AS VARCHAR(2)) + '-' + CAST(DATEPART(dd,y" + rs.GetInt32(1) + ".ValueDate) AS VARCHAR(2)) + '' AS XI" + rs.GetInt32(1) + "";
						join += "" +
							" LEFT OUTER JOIN SponsorInviteBQ y" + rs.GetInt32(1) + " ON y" + rs.GetInt32(1) + ".SponsorInviteID = s.SponsorInviteID AND y" + rs.GetInt32(1) + ".BQID = " + rs.GetInt32(1);
					}

					BQs += (BQs != "" ? ":" : "") + rs.GetInt32(1).ToString();
					BQdesc += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>" + rs.GetString(0) + "</B>&nbsp;</TD>";
				}
				else
				{
					aggrBQcx++;
					aggrBQ += (aggrBQ != "" ? "," : "") + rs.GetInt32(1);
					aggrBRdesc += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>" + rs.GetString(6) + "</B>&nbsp;</TD>";
				}
			}
			rs.Close();

			string ESstart = "", ESdesc = "", ESselect = "", ESuserSelect = "", ESuserJoin = "", ESrounds = "", ESroundTexts = "", ESpreviousRounds = "", ESpreviousRoundTexts = "", ESjoin = "", ESattr = "";
			string bqESselect = "", bqESjoin = "";
			int EScount = 0, totalEScount = 0, tmpEScount = 0;
			rs = Db2.rs("SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = " + sponsorID);
			if (rs.Read())
			{
				totalEScount = rs.GetInt32(0);
			}
			rs.Close();
			rs = Db2.rs("SELECT " +
			            "ses.SponsorExtendedSurveyID, " +       // 0
			            "ses.Internal, " +                      // 1
			            "ses.ProjectRoundID, " +                // 2
			            "ses.EformFeedbackID, " +               // 3
			            "ses.RequiredUserCount, " +             // 4
			            "ses.PreviousProjectRoundID, " +        // 5
			            "ses.RoundText, " +                     // 6
			            "ses2.RoundText, " +                    // 7
			            "pr.Started, " +                        // 8
			            "pr.Closed, " +                         // 9
			            "ses.WarnIfMissingQID, " +              // 10
			            "ses.ExtraEmailSubject " +              // 11
			            "FROM SponsorExtendedSurvey ses " +
			            "LEFT OUTER JOIN SponsorExtendedSurvey ses2 ON ses.SponsorID = ses2.SponsorID AND ses.PreviousProjectRoundID = ses2.ProjectRoundID " +
			            "LEFT OUTER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID " +
			            "WHERE ses.SponsorID = " + sponsorID + " " +
			            "ORDER BY ses.SponsorExtendedSurveyID");
			while (rs.Read())
			{
				if (totalEScount <= 8 || tmpEScount >= (totalEScount - 8))
				{
					ESstart += (ESstart != "" ? "," : "") + (rs.IsDBNull(8) ? DateTime.MaxValue : rs.GetDateTime(8)).ToString("yyyy-MM-dd");
					ESrounds += (ESrounds != "" ? "," : "") + rs.GetInt32(2);
					ESroundTexts += (ESroundTexts != "" ? "," : "") + (rs.IsDBNull(6) ? "$" : rs.GetString(6));
					ESpreviousRounds += (ESpreviousRounds != "" ? "," : "") + (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
					ESpreviousRoundTexts += (ESpreviousRoundTexts != "" ? "," : "") + (rs.IsDBNull(7) ? "$" : rs.GetString(7));
					// Answers on this department and below
					ESselect += ", (" +
						"SELECT COUNT(*) " +
						"FROM UserSponsorExtendedSurvey x " +
						//"INNER JOIN [User] xu ON x.UserID = xu.UserID " +
						"INNER JOIN SponsorInvite xsi ON x.UserID = xsi.UserID " +
						"INNER JOIN Department xd ON xsi.DepartmentID = xd.DepartmentID " +
						"WHERE LEFT(xd.SortString,LEN(d.SortString)) = d.SortString " +
						"AND x.SponsorExtendedSurveyID = " + rs.GetInt32(0) + " " +
						"AND xsi.SponsorID = d.SponsorID " +
						"AND x.AnswerID IS NOT NULL " +
						") ";
					// Answers on this department
					ESselect += ", (" +
						"SELECT COUNT(*) " +
						"FROM UserSponsorExtendedSurvey x " +
						//"INNER JOIN [User] xu ON x.UserID = xu.UserID " +
						"INNER JOIN SponsorInvite xsi ON x.UserID = xsi.UserID " +
						"INNER JOIN Department xd ON xsi.DepartmentID = xd.DepartmentID " +
						"WHERE xd.DepartmentID = d.DepartmentID " +
						"AND x.SponsorExtendedSurveyID = " + rs.GetInt32(0) + " " +
						"AND xsi.SponsorID = d.SponsorID " +
						"AND x.AnswerID IS NOT NULL " +
						") ";
					ESselect += ", es" + rs.GetInt32(0) + ".ProjectRoundUnitID AS PRUID" + rs.GetInt32(0) + ", sesd" + rs.GetInt32(0) + ".RequiredUserCount, sesd" + rs.GetInt32(0) + ".Hide, sesd" + rs.GetInt32(0) + ".Ext ";

					ESjoin += " " +
						"LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd" + rs.GetInt32(0) + " ON sesd" + rs.GetInt32(0) + ".SponsorExtendedSurveyID = " + rs.GetInt32(0) + " AND sesd" + rs.GetInt32(0) + ".DepartmentID = d.DepartmentID " +
						"LEFT OUTER JOIN eform..ProjectRoundUnit es" + rs.GetInt32(0) + " ON es" + rs.GetInt32(0) + ".ProjectRoundID = " + rs.GetInt32(2) + " " +
						"AND (s.Sponsor + '=' + dbo.cf_departmentTree(d.DepartmentID,'=')) = eform.dbo.cf_projectUnitTree(es" + rs.GetInt32(0) + ".ProjectRoundUnitID,'=') ";
					ESdesc += "<TD ALIGN=\"CENTER\" style=\"font-size:9px;\">" +
						"&nbsp;<B style=\"font-size:8px;\">" + rs.GetString(1).Replace(" ", "&nbsp;<br/>&nbsp;") + "</B>&nbsp;" +
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
				OrgTree.Text += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\" style=\"font-size:12px;line-height:1.0;vertical-align:middle;\"><TR style=\"border-bottom:1px solid #333333;\">" +
					"<TD COLSPAN=\"2\"><B>Unit/Email</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Action</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Activated</B>&nbsp;</TD>" +
					"" + ESdesc + "" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Total</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Received&nbsp;<br/>&nbsp;inivtation</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>1st invite&nbsp;<br/>&nbsp;sent</B>&nbsp;</TD>" +
					"" + aggrBRdesc + "" +
					"" + BQdesc + "" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Unit ID&nbsp;<br/>&nbsp;User status</B>&nbsp;</TD>" +
					"</TR>";
			} else {
				OrgTree.Text += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\" style=\"font-size:12px;line-height:1.0;vertical-align:middle;\"><TR style=\"border-bottom:1px solid #333333;\">" +
					"<TD COLSPAN=\"2\"><B>Unit</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Action</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Activated</B>&nbsp;</TD>" +
					"" + ESdesc + "" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Total</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Received&nbsp;<br/>&nbsp;inivtation</B>&nbsp;</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>1st invite&nbsp;<br/>&nbsp;sent</B>&nbsp;</TD>" +
					"" + aggrBRdesc + "" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<B>Unit ID</B>&nbsp;</TD>" +
					"</TR>";
			}
			//OrgTree.Text += "<TR><TD COLSPAN=\"" + (aggrBQcx + 8 + (showDepartmentID != 0 && BQs != "" ? BQs.Split(':').Length : 0) + EScount) + "\" style=\"height:1px;line-height:1px;background-color:#333333\"><img src=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>";
			OrgTree.Text += "<TR><TD COLSPAN=\"3\">" + Session["Sponsor"] + "</TD>[xxx]</TR>";

			int UX = 0, AX = 0, IX = 0;

			int MIN_SHOW = 10;

			bool[] DX = new bool[8];

			string sql = "SELECT " +
				"d.Department, " +
				"dbo.cf_departmentDepth(d.DepartmentID), " +
				"d.DepartmentID, " +
				"(" +
				"SELECT COUNT(*) " +
				"FROM SponsorInvite si " +
				"WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID" +
				"), " +		// 3 - Number of invites at current department
				"(" +
				"SELECT COUNT(*) " +
				"FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID " +
				"WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID" +
				"), " +		// 4 - Number of registered users at current department
				"(" +
				"SELECT COUNT(*) " +
				"FROM SponsorInvite si " +
				"WHERE si.DepartmentID = d.DepartmentID AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL" +
				"), " +		// 5 - Number of invites that's been sent at current department
				"(" +
				"SELECT COUNT(*) FROM Department x " +
				(Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = " + Session["SponsorAdminID"] + " " : "") +
				"WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL) " +
				"AND d.SponsorID = x.SponsorID " +
				"AND d.SortString < x.SortString" +
				"), " +		// 6 - Number of departments on same level after this one
				"(" +
				"SELECT COUNT(*) " +
				"FROM SponsorInvite si " +
				"INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID " +
				"WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID" +
				"), " +		// 7 - Number of invites at current department and down
				"(" +
				"SELECT COUNT(*) " +
				"FROM SponsorInvite si " +
				"INNER JOIN [User] u ON si.UserID = u.UserID " +
				"INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID " +
				"WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID" +
				"), " +		// 8 - Number of registered users at current department and down
				"(" +
				"SELECT COUNT(*) " +
				"FROM SponsorInvite si " +
				"INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID " +
				"WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL" +
				"), " +		// 9 - Number of invites that's been sent at current department and down
				"(" +
				"SELECT MIN(si.Sent) " +
				"FROM SponsorInvite si " +
				"INNER JOIN Department sid ON si.DepartmentID = sid.DepartmentID " +
				"WHERE LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND si.SponsorID = d.SponsorID AND si.Sent IS NOT NULL" +
				")," +                  // 10
				"d.DepartmentShort " +  // 11
				ESselect +
				"FROM Department d " +
				"INNER JOIN Sponsor s ON d.SponsorID = s.SponsorID " +
				ESjoin +
				(Session["SponsorAdminID"].ToString() != "-1" ?
				 "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
				 "WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " " +
				 "AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
				"ORDER BY d.SortString";
			//Response.Write("<!--" + sql + "-->");
			//Response.End();
			rs = Db2.rs(sql);
			while (rs.Read()) {
				int depth = rs.GetInt32(1);
				DX[depth] = (rs.GetInt32(6) > 0);

				UX += rs.GetInt32(3);
				AX += rs.GetInt32(4);
				IX += rs.GetInt32(5);

				OrgTree.Text += "<TR" + (depth == 1 || depth == 4 ? " style=\"background-color:#EEEEEE\"" : (depth == 2 || depth == 5 ? " style=\"background-color:#F6F6F6\"" : "")) + "><TD COLSPAN=\"2\"><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD>";
				for (int i = 1; i <= depth; i++) {
					OrgTree.Text += "<img src=\"img/";
					if (i == depth) {
						OrgTree.Text += (DX[i] ? "T" : "L");
					} else {
						OrgTree.Text += (DX[i] ? "I" : "null");
					}
					OrgTree.Text += ".gif\" width=\"19\" height=\"20\"/>";
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
				OrgTree.Text += "</TD><TD style=\"vertical-align:MIDDLE;" + (s.Length > 20 ? "font-size:10px;" : "") + "\">&nbsp;" + (deptID == rs.GetInt32(2) || showDepartmentID == rs.GetInt32(2) ? "<B>" : "") + s + (deptID == rs.GetInt32(2) || showDepartmentID == rs.GetInt32(2) ? "</B>" : "") + "&nbsp;</TD></TR></TABLE></TD>" +
					"<TD ALIGN=\"CENTER\">" +
					(Convert.ToInt32(Session["ReadOnly"]) == 0 ?
					 "<A HREF=\"org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DID=" + rs.GetInt32(2) + "\"><img src=\"img/unt_edt.gif\" border=\"0\"/></A>" +
					 "" : "") +
					"" + (rs.GetInt32(3) > 0 ? "<A HREF=\"org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&SDID=" + rs.GetInt32(2) + "\"><img src=\"img/usr_on.gif\" border=\"0\"/></A>" : (Convert.ToInt32(Session["ReadOnly"]) == 0 ? "<A HREF=\"org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DeleteDID=" + rs.GetInt32(2) + "\"><img src=\"img/unt_del.gif\" border=\"0\"/></A>" : "")) + "" +
					"</TD>" +
					"<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\"><span title=\"" + (rs.GetInt32(3) > 0 && rs.GetInt32(8) != rs.GetInt32(4) ? "" + (rs.GetInt32(4) >= MIN_SHOW ? rs.GetInt32(4).ToString() : (showReg ? rs.GetInt32(4).ToString() : "")) + "" : "") + "\">" + (rs.GetInt32(8) >= MIN_SHOW ? rs.GetInt32(8).ToString() : "<img src=\"img/key.gif\"/>") + "</span></TD>";

				for (int i = 0; i < EScount; i++) {
					int idx = 12 + 6 * i;
					ESanswerCount[i] += rs.GetInt32(idx + 1);
					int rac = (rs.IsDBNull(idx + 3) ? Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1]) : rs.GetInt32(idx + 3));
					if (rs.IsDBNull(idx + 4)) {
						OrgTree.Text += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" +
							(!rs.IsDBNull(idx + 5) ? "E" : "") +
							(
								!rs.IsDBNull(idx + 2)
								&&
								Convert.ToInt32(ESattr.Split(',')[i].Split(':')[0]) != 0
								&&
								rac <= rs.GetInt32(idx)
								?
								"<A HREF=\"JavaScript:void(window.open('" + ConfigurationSettings.AppSettings["eFormURL"] + "feedback.aspx?" +
								"AB=1&" +
								"R=" + (ESrounds.Split(',')[i]) + "&" +
								(ESpreviousRounds.Split(',')[i] != "0" ? "RR=" + ESpreviousRounds.Split(',')[i] + "&" : "") +
								(ESpreviousRounds.Split(',')[i] != "0" ? "R1=" + ESroundTexts.Split(',')[i] + "&" : "") +
								(ESpreviousRounds.Split(',')[i] != "0" ? "R2=" + ESpreviousRoundTexts.Split(',')[i] + "&" : "") +
								"U=" + rs.GetInt32(idx + 2).ToString() + "&" +
								"RAC=" + rac + "&" +
								"UD=" + Server.HtmlEncode(rs.GetString(0)).Replace("&", "_0_").Replace("#", "_1_") + "&" +
								"N=" + Server.HtmlEncode(Session["Sponsor"].ToString()).Replace("&", "_0_").Replace("#", "_1_") + "" +
								"','es" + i + "','scrollbars=1,width=880,height=700,resizable=1,toolbar=0,status=0,menubar=0,location=0'));\"><IMG SRC=\"img/graphIcon2.gif\" BORDER=\"0\"/></A>" : "") + "&nbsp;" +
							(rs.GetInt32(idx) >= rac
							 ? "<span title=\"" +
							 (rs.GetInt32(3) > 0 && rs.GetInt32(idx) != rs.GetInt32(idx + 1)
							  ?
							  "" +
							  (rs.GetInt32(idx + 1) >= rac
							   ? rs.GetInt32(idx + 1).ToString()
							   : (showReg ? rs.GetInt32(idx + 1).ToString() : "")) +
							  ""
							  : "") +
							 "\">" + rs.GetInt32(idx).ToString() + "</span>" : "<img src=\"img/key.gif\" title=\"" + (showReg ? rs.GetInt32(idx + 1).ToString() : "") + "\"/>") +
							"&nbsp;</TD>";
					} else {
						OrgTree.Text += "<TD ALIGN=\"CENTER\">&nbsp;</TD>";
					}
				}
				OrgTree.Text += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + rs.GetInt32(7) + (rs.GetInt32(3) > 0 && rs.GetInt32(3) != rs.GetInt32(7) ? " (" + rs.GetInt32(3) + ")" : "") + "&nbsp;</TD>";
				OrgTree.Text += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + (rs.GetInt32(7) > 0 ? Math.Round(((float)rs.GetInt32(9) / (float)rs.GetInt32(7)) * 100, 0).ToString() + "%" : "-") + (rs.GetInt32(3) > 0 && rs.GetInt32(5) != rs.GetInt32(9) && Math.Round(((float)rs.GetInt32(9) / (float)rs.GetInt32(7)) * 100, 0) != Math.Round((float)rs.GetInt32(5) / (float)rs.GetInt32(3) * 100, 0) ? " (" + Math.Round((float)rs.GetInt32(5) / (float)rs.GetInt32(3) * 100, 0).ToString() + "%" + ")" : "") + "&nbsp;</TD>";
				OrgTree.Text += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + (rs.IsDBNull(10) ? "N/A" : rs.GetDateTime(10).ToString("yyMMdd")) + "&nbsp;</TD>";

				if (aggrBQcx != 0) {
					foreach (string a in aggrBQ.Split(',')) {
						OrgTree.Text += "<TD ALIGN=\"CENTER\">&nbsp;";
						SqlDataReader rs2 = Db2.rs("SELECT " +
						                           "AVG(DATEDIFF(year, upbq.ValueDate, GETDATE())), COUNT(upbq.ValueDate) " +
						                           "FROM Department d " +
						                           "INNER JOIN Department sid ON LEFT(sid.SortString,LEN(d.SortString)) = d.SortString AND sid.SponsorID = d.SponsorID " +
						                           "INNER JOIN SponsorInvite si ON sid.DepartmentID = si.DepartmentID " +
						                           "INNER JOIN [User] u ON si.UserID = u.UserID " +
						                           "INNER JOIN UserProfileBQ upbq ON u.UserProfileID = upbq.UserProfileID AND upbq.BQID = " + Convert.ToInt32(a) + " " +
						                           "WHERE d.DepartmentID = " + rs.GetInt32(2));
						if (rs2.Read() && !rs2.IsDBNull(0)) {
							OrgTree.Text += (rs2.GetInt32(1) >= MIN_SHOW ? rs2.GetValue(0).ToString() : "<img src=\"img/key.gif\"/>");
						}
						rs2.Close();
						OrgTree.Text += "&nbsp;</TD>";
					}
				}
				if (showDepartmentID != 0 && BQs != "") {
					for (int i = 0; i < BQs.Split(':').Length; i++) OrgTree.Text += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;</TD>";
				}
				OrgTree.Text += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + (rs.IsDBNull(11) ? "N/A" : rs.GetString(11)) + "&nbsp;</TD>";
				OrgTree.Text += "</TR>";

				if (showDepartmentID == rs.GetInt32(2)) {
					#region Show department
					int dynamicIdx = 9;
					StringBuilder usr = new StringBuilder();

					sql = "SELECT " +
						"s.SponsorInviteID, " +
						"s.Email, " +
						"s.Sent, " +
						"s.UserID, " +
						"u.ReminderLink, " +
						"LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)), " +    // 5
						"s.PreviewExtendedSurveys, " +
						"s.Stopped, " +
						"s.StoppedReason " +
						"" + select + ESuserSelect + " " +
						"FROM SponsorInvite s" + join + ESuserJoin + " " +
						"LEFT OUTER JOIN [User] u ON s.UserID = u.UserID " +
						"WHERE s.SponsorID = " + sponsorID + " " +
						"AND s.DepartmentID = " + rs.GetInt32(2) + " " +
						"ORDER BY s.Email";
					//Response.Write("<!-- " + sql + " -->");
					SqlDataReader rs2 = Db2.rs(sql);
					while (rs2.Read()) {
						usr.Append("<TR style=\"background-color:#FFF7D6\"><TD>");
						for (int i = 1; i <= depth; i++) {
							usr.Append("<IMG SRC=\"img/" + (DX[i] ? "I" : "null") + ".gif\" width=\"19\" height=\"20\"/>");
						}
						usr.Append("</TD><TD>" + (rs2.IsDBNull(1) ? "" : rs2.GetString(1)) + "</TD>" +
						           "<TD ALIGN=\"CENTER\">" +
						           (Convert.ToInt32(Session["ReadOnly"]) == 0 ?
						            "<A HREF=\"org.aspx?SDID=" + showDepartmentID.ToString() + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&UID=" + rs2.GetInt32(0).ToString() + "\"><img src=\"img/usr_edt.gif\" border=\"0\"/></A>" +
						            "<A HREF=\"org.aspx?SDID=" + showDepartmentID.ToString() + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&DeleteUID=" + rs2.GetInt32(0).ToString() + "\"><img src=\"img/usr_del.gif\" border=\"0\"/></A>" +
						            "" : "") +
						           "</TD>" +
						           "<TD ALIGN=\"CENTER\">");
						if (showReg) {
							if (!rs2.IsDBNull(3) && !rs2.IsDBNull(5)) {
								usr.Append("<A TITLE=\"Log on to users account\" HREF=\"" + ConfigurationSettings.AppSettings["healthWatchURL"] + "a/" + rs2.GetString(5) + rs2.GetInt32(3).ToString() + "\" TARGET=\"_blank\">" + rs2.GetInt32(3).ToString() + "/" + (rs2.IsDBNull(4) ? "0" : rs2.GetInt32(4).ToString()) + "</A>");

								SqlDataReader rs3 = Db2.rs("SELECT u.UserID, s.Sponsor FROM [User] u LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID WHERE u.UserID <> " + rs2.GetInt32(3) + " AND u.Email = '" + rs2.GetString(1).Replace("'", "''") + "'");
								while (rs3.Read()) {
									usr.Append("<br/><span title=\"" + (rs3.IsDBNull(1) ? "Private" : rs3.GetString(1)) + "\">" + rs3.GetInt32(0) + "</span>");
								}
								rs3.Close();
							} else {
								SqlDataReader rs3 = Db2.rs("SELECT u.UserID, s.Sponsor FROM [User] u LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID WHERE u.Email = '" + rs2.GetString(1).Replace("'", "''") + "'");
								while (rs3.Read()) {
									usr.Append("<A TITLE=\"Connect " + (rs3.IsDBNull(1) ? "Private" : rs3.GetString(1)) + "\" HREF=\"org.aspx?" + (showReg ? "ShowReg=1&" : "") + "SDID=" + showDepartmentID.ToString() + "&ConnectSPIID=" + rs2.GetInt32(0) + "&WithUID=" + rs3.GetInt32(0) + "&AndDID=" + rs.GetInt32(2) + "\">" + rs3.GetInt32(0) + "</A><br/>");
								}
								rs3.Close();
							}
						}

						usr.Append("</TD>");
						for (int i = 0; i < EScount; i++) {
							usr.Append("<TD ALIGN=\"CENTER\">");
							if (showReg) {
								int idx = (BQs != "" ? BQs.Split(':').Length : 0) + dynamicIdx + i * 2;
								if (rs2.IsDBNull(idx + 1)) {
									usr.Append("&nbsp;");
								} else {
									if (rs2.IsDBNull(idx)) {
										usr.Append("<IMG SRC=\"img/star.gif\"/>");
										if (Convert.ToInt32(ESattr.Split(',')[i].Split(':')[3]) != 0) {
											usr.Append("<a href=\"org.aspx?" +
											           "ShowReg=1" +
											           "&SESID=" + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[4]) + "" +
											           "&SendExtra=" + rs2.GetInt32(3) + "" +
											           "&SDID=" + showDepartmentID.ToString() + "" +
											           "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "" +
											           "\" title=\"Send extra\">!</a>");
										}
										SqlDataReader rs3 = Db2.rs("SELECT " +
										                           "a.AnswerID, " +
										                           "a.CurrentPage " +
										                           "FROM Answer a " +
										                           "WHERE a.ProjectRoundUserID = " + rs2.GetInt32(idx + 1), "eFormSqlConnection");
										if (rs3.Read() && !rs3.IsDBNull(1)) {
											usr.Append("<A HREF=\"org.aspx?" +
											           "ShowReg=1" +
											           "&SubmitAID=" + rs3.GetInt32(0) + "" +
											           "&SubmitUID=" + rs2.GetInt32(idx + 1) + "" +
											           "&SDID=" + showDepartmentID.ToString() + "" +
											           "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "" +
											           "\" TITLE=\"Submit survey (number indicates what page the user is on)\">" + rs3.GetInt32(1) + "</A>" + "");
										}
										rs3.Close();
									} else {
										usr.Append("<A HREF=\"org.aspx?" +
										           "ShowReg=1" +
										           "&ReclaimAID=" + rs2.GetInt32(idx) + "" +
										           "&ReclaimUID=" + rs2.GetInt32(idx + 1) + "" +
										           "&SDID=" + showDepartmentID.ToString() + "" +
										           "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "" +
										           "\" TITLE=\"Withdraw submission of survey (mark as not submitted and allow changes)\"><IMG SRC=\"img/starOK.gif\" BORDER=\"0\"/></A>" + "");
										if (Convert.ToInt32(ESattr.Split(',')[i].Split(':')[2]) != 0) {
											SqlDataReader rs3 = Db2.rs("SELECT COUNT(*) FROM AnswerValue WHERE AnswerID = " + rs2.GetInt32(idx) + " AND QuestionID = " + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[2]) + " AND DeletedSessionID IS NULL AND (ValueInt IS NOT NULL OR ValueDecimal IS NOT NULL OR ValueDateTime IS NOT NULL OR ValueText IS NOT NULL)", "eFormSqlConnection");
											if (!rs3.Read() || rs3.IsDBNull(0) || rs3.GetInt32(0) == 0) {
												usr.Append("<span style=\"font-weight:bold;color:#cc0000;\">!</span>");
											}
											rs3.Close();
										}
									}
								}
							} else {
								usr.Append("&nbsp;");
							}
							if (Convert.ToDateTime(ESstart.Split(',')[i]) > DateTime.Now && Convert.ToInt32(Session["ReadOnly"]) == 0) {
								usr.Append("<a title=\"Klicka för att låsa upp/låsa möjlighet till förhandsinmatning av enkäten\" href=\"org.aspx?PESSIID=" + rs2.GetInt32(0) + "&Flip=" + (rs2.IsDBNull(6) ? 1 : 0) + "&" + (showReg ? "ShowReg=1&" : "") + "SDID=" + showDepartmentID.ToString() + "\"><img src=\"img/" + (rs2.IsDBNull(6) ? "locked" : "unlock") + ".gif\" border=\"0\"/></A>");
							}
							usr.Append("</TD>");
						}
						usr.Append("<TD>&nbsp;</TD><TD ALIGN=\"CENTER\" style=\"font-size:9px;\">&nbsp;");
						if (rs2.IsDBNull(2)) {
							usr.Append("No");
						}
						else {
							usr.Append("Yes, " + rs2.GetDateTime(2).ToString("yyyyMMdd"));
						}
						if (Convert.ToInt32(Session["ReadOnly"]) == 0) {
							usr.Append(", <A HREF=\"org.aspx?" + (showReg ? "ShowReg=1&" : "") + "SendSPIID=" + rs2.GetInt32(0) + "&SDID=" + showDepartmentID.ToString() + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">" + (rs2.IsDBNull(2) ? "Send" : "Resend") + "</A>");
						}
						usr.Append("&nbsp;</TD>");
						usr.Append("<TD ALIGN=\"CENTER\">&nbsp;</TD>");
						if (aggrBQcx != 0) {
							usr.Append("<TD COLSPAN=\"" + aggrBQcx + "\">&nbsp;</TD>");
						}
						if (BQs != "") {
							for (int i = 0; i < BQs.Split(':').Length; i++) {
								if (rs2.IsDBNull(i + dynamicIdx)) {
									usr.Append("<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;&lt;&nbsp;N/A&nbsp;&gt;&nbsp;</TD>");
								} else if (rs2.IsDBNull(3)) {
									usr.Append("<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + rs2.GetValue(i + dynamicIdx) + "&nbsp;</TD>");
								} else {
									SqlDataReader rs3 = Db.rs("" +
									                          "SELECT " +
									                          "b.UserBQID " +
									                          "FROM [User] u " +
									                          "INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
									                          "INNER JOIN UserProfileBQ b ON up.UserProfileID = b.UserProfileID AND b.BQID = " + BQs.Split(':')[i] + " " +
									                          "WHERE up.UserID = " + rs2.GetInt32(3));
									if (rs3.Read()) {
										usr.Append("<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + rs2.GetValue(i + dynamicIdx) + "&nbsp;</TD>");
									} else {
										usr.Append("<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;<a href=\"org.aspx?SDID=" + showDepartmentID + "&UID=" + rs2.GetInt32(3) + "&BQID=" + BQs.Split(':')[i] + "\">" + rs2.GetValue(i + dynamicIdx) + "</a>&nbsp;</TD>");
									}
									rs3.Close();
								}
							}
						}
						usr.Append("<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">");
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
						usr.Append("</TD>");
					}
					rs2.Close();

					OrgTree.Text += usr;
					#endregion
				}
			}
			rs.Close();
			OrgTree.Text += "<TR><TD COLSPAN=\"" + (aggrBQcx + 8 + (showDepartmentID != 0 && BQs != "" ? BQs.Split(':').Length : 0) + EScount) + "\" style=\"border-top:1px solid #333333\">&nbsp;</TD></TR>";
			string header = "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">" + (AX >= MIN_SHOW ? AX.ToString() : "<img src=\"img/key.gif\"/>") + "</TD>";
			for (int i = 0; i < EScount; i++) {
				header += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" +
					(Convert.ToInt32(ESattr.Split(',')[i].Split(':')[0]) != 0 && Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1]) <= ESanswerCount[i] ? "<A HREF=\"JavaScript:void(window.open('" + ConfigurationSettings.AppSettings["eFormURL"] + "feedback.aspx?" +
					 "R=" + (ESrounds.Split(',')[i]) + "&" +
					 (ESpreviousRounds.Split(',')[i] != "0" ? "RR=" + ESpreviousRounds.Split(',')[i] + "&" : "") +
					 (ESpreviousRounds.Split(',')[i] != "0" ? "R1=" + ESroundTexts.Split(',')[i] + "&" : "") +
					 (ESpreviousRounds.Split(',')[i] != "0" ? "R2=" + ESpreviousRoundTexts.Split(',')[i] + "&" : "") +
					 "N=" + Server.HtmlEncode(Session["Sponsor"].ToString()).Replace("&", "_0_").Replace("#", "_1_") + "','es" + i + "','scrollbars=1,width=880,height=700,resizable=1,toolbar=0,status=0,menubar=0,location=0'));\"><IMG SRC=\"img/graphIcon2.gif\" BORDER=\"0\"/></A>" : "") +
					"&nbsp;" + ESanswerCount[i] + "&nbsp;</TD>";
			}
			header += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">" + UX + "</TD>";
			header += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;" + Math.Round(((float)IX / (float)UX) * 100, 0) + "%&nbsp;</TD>";
			header += "<TD ALIGN=\"CENTER\" STYLE=\"font-size:9px;\">&nbsp;</TD>";

			if (aggrBQcx != 0) {
				foreach (string a in aggrBQ.Split(',')) {
					header += "<TD ALIGN=\"CENTER\">&nbsp;";
					SqlDataReader rs2 = Db2.rs("SELECT " +
					                           "AVG(DATEDIFF(year, upbq.ValueDate, GETDATE())), COUNT(upbq.ValueDate) " +
					                           "FROM SponsorInvite si " +
					                           "INNER JOIN [User] u ON si.UserID = u.UserID " +
					                           "INNER JOIN UserProfileBQ upbq ON u.UserProfileID = upbq.UserProfileID AND upbq.BQID = " + Convert.ToInt32(a) + " " +
					                           "WHERE si.SponsorID = " + sponsorID);
					if (rs2.Read() && !rs2.IsDBNull(0)) {
						header += (rs2.GetInt32(1) >= MIN_SHOW ? rs2.GetValue(0).ToString() : "<img src=\"img/key.gif\"/>");
					}
					rs2.Close();
					header += "&nbsp;</TD>";
				}
			}

			OrgTree.Text = OrgTree.Text.Replace("[xxx]", header) + "</TABLE>";
			#endregion

			if (Session["SuperAdminID"] != null || Session["SponsorAdminID"] != null && Session["SponsorAdminID"].ToString() == "-1") {
				rs = Db2.rs("SELECT BQ.BQID, BQ.Internal, (SELECT COUNT(*) FROM BA WHERE BA.BQID = BQ.BQID) FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Organize = 1");
				while (rs.Read()) {
					int cx = 0;
					OrgTree.Text += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\" style=\"font-size:12px;line-height:1.0;vertical-align:middle;\">";
					OrgTree.Text += "<TR style=\"border-bottom:1px solid #333333;\"><TD><b style=\"color:#cc0000;\">" + rs.GetString(1) + "</b>&nbsp;</TD><TD><b>Activated</b>&nbsp;</TD>" + ESdesc + "<TD><b>Total</b>&nbsp;</TD></TR>";
					SqlDataReader rs2 = Db2.rs("SELECT BA.BAID, BA.Internal, COUNT(u.UserID), COUNT(si.SponsorInviteID) " +
					                           bqESselect +
					                           "FROM SponsorInvite si " +
					                           "LEFT OUTER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = " + rs.GetInt32(0) + " " +
					                           "LEFT OUTER JOIN [User] u ON si.UserID = u.UserID " +
					                           "LEFT OUTER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
					                           "LEFT OUTER JOIN UserProfileBQ upb ON up.UserProfileID = upb.UserProfileID AND upb.BQID = " + rs.GetInt32(0) + " " +
					                           "LEFT OUTER JOIN BA ON ISNULL(sib.BAID,upb.ValueInt) = BA.BAID " +
					                           bqESjoin +
					                           "WHERE si.SponsorID = " + sponsorID + " " +
					                           "GROUP BY BA.BAID, BA.Internal");
					while (rs2.Read()) {
						cx++;
						OrgTree.Text += "<TR style=\"background-color:#EEEEEE\"><TD><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD><img src=\"img/" + (cx == rs.GetInt32(2) ? "L" : "T") + ".gif\" width=\"19\" height=\"20\"/></TD><TD>" + (rs2.IsDBNull(1) ? "?" : rs2.GetString(1)) + "&nbsp;</TD></TR></TABLE></TD><TD ALIGN=\"CENTER\">&nbsp;" + rs2.GetInt32(2) + "&nbsp;</TD>";
						for (int i = 0; i < EScount; i++) {
							int idx = 4 + i;
							if (!rs2.IsDBNull(idx) && rs2.GetInt32(idx) >= Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1])) {
								StringBuilder sb = new StringBuilder();
								SqlDataReader rs3 = Db2.rs("SELECT usesX.AnswerID " +
								                           "FROM SponsorInvite si " +
								                           "INNER JOIN [User] u ON si.UserID = u.UserID " +
								                           "INNER JOIN UserSponsorExtendedSurvey usesX ON u.UserID = usesX.UserID AND usesX.SponsorExtendedSurveyID = " + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[4]) + " " +
								                           "LEFT OUTER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = " + rs.GetInt32(0) + " " +
								                           "LEFT OUTER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
								                           "LEFT OUTER JOIN UserProfileBQ upb ON up.UserProfileID = upb.UserProfileID AND upb.BQID = " + rs.GetInt32(0) + " " +
								                           "WHERE usesX.AnswerID IS NOT NULL AND si.SponsorID = " + sponsorID + " AND ISNULL(sib.BAID,upb.ValueInt) = " + rs2.GetInt32(0));
								while (rs3.Read()) {
									sb.Append("," + rs3.GetInt32(0));
								}
								rs3.Close();

								OrgTree.Text += "<TD ALIGN=\"CENTER\">&nbsp;" +
									"<A HREF=\"JavaScript:void(window.open('" + ConfigurationSettings.AppSettings["eFormURL"] + "feedback.aspx?" +
									"R=" + (ESrounds.Split(',')[i]) + "&" +
									"AIDS=0" + sb.ToString() + "&" +
									"UD=" + rs2.GetString(1) + "&" +
									"RAC=" + Convert.ToInt32(ESattr.Split(',')[i].Split(':')[1]) + "&" +
									"N=" + Server.HtmlEncode(Session["Sponsor"].ToString()).Replace("&", "_0_").Replace("#", "_1_") + "" +
									"','esBQ" + i + "','scrollbars=1,width=880,height=700,resizable=1,toolbar=0,status=0,menubar=0,location=0'));\"><IMG SRC=\"img/graphIcon2.gif\" BORDER=\"0\"/></A>" +
									"&nbsp;" + rs2.GetInt32(idx) + "&nbsp;</TD>";
							} else if (!rs2.IsDBNull(idx)) {
								OrgTree.Text += "<TD ALIGN=\"CENTER\" style=\"color:#EEEEEE\"><img src=\"img/key.gif\"/>&nbsp;" + rs2.GetInt32(idx) + "&nbsp;</TD>";
							} else {
								OrgTree.Text += "<TD ALIGN=\"CENTER\"><img src=\"img/key.gif\"/></TD>";
							}
						}
						OrgTree.Text += "<TD ALIGN=\"CENTER\">&nbsp;" + rs2.GetInt32(3) + "&nbsp;</TD></TR>";
					}
					rs2.Close();
					OrgTree.Text += "</TABLE>";
				}
				rs.Close();
			}
		}

		void Search_Click(object sender, EventArgs e)
		{
			if (SearchEmail.Text != "") {
				bool found = false;

				string q = "SELECT " +
					"si.SponsorInviteID, " +
					"si.DepartmentID, " +
					"dbo.cf_departmentTree(si.DepartmentID,' » ') + ' » ' + si.Email " +
					"FROM SponsorInvite si " +
					hiddenBqJoin +
					(Session["SponsorAdminID"].ToString() != "-1" ?
					 "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID " +
					 "WHERE sad.SponsorAdminID = " + Session["SponsorAdminID"] + " " +
					 "AND " : "WHERE ") + "si.SponsorID = " + sponsorID + " " +
					"AND (si.Email LIKE '%" + SearchEmail.Text.Replace("'", "") + "%'" + hiddenBqWhere.Replace("[x]", "'%" + SearchEmail.Text.Replace("'", "") + "%'") + ")";
				SqlDataReader rs = Db2.rs(q);
				while (rs.Read()) {
					found = true;
					SearchResultList.Text += "<TR><TD>" + (rs.IsDBNull(1) ? "Error, please contact <a href=\"mailto:support@healthwatch.se\">support@healthwatch.se</a>" : "<A HREF=\"org.aspx?SDID=" + rs.GetInt32(1) + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&UID=" + rs.GetInt32(0).ToString() + "\">" + rs.GetString(2) + "</A>") + "</TD></TR>";
				}
				rs.Close();

				if (!found) {
					SearchResultList.Text += "<TR><TD><B>No match found!</B></TD></TR>";
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

			if (userID == 0) {
				#region create new sponsor invite, if email not already in org
				rs = Db2.rs("SELECT UserID FROM SponsorInvite WHERE SponsorID = " + sponsorID + " AND Email = '" + Email.Text.Replace("'", "''") + "'");
				if (rs.Read())
				{
					exists = true;
				}
				rs.Close();
				if (!exists)
				{
					Db2.exec("INSERT INTO SponsorInvite (SponsorID,DepartmentID,Email,StoppedReason,Stopped) VALUES (" + sponsorID + "," + DepartmentID.SelectedValue + ",'" + Email.Text.Replace("'", "''").Trim() + "'," + (stoppedReason == 0 ? "NULL" : stoppedReason.ToString()) + "," + (stoppedReason == 0 ? "NULL" : "GETDATE()") + ")");
					rs = Db2.rs("SELECT TOP 1 SponsorInviteID FROM SponsorInvite WHERE SponsorID = " + sponsorID + " ORDER BY SponsorInviteID DESC");
					if (rs.Read())
					{
						userID = rs.GetInt32(0);
					}
					rs.Close();
				}
				#endregion
			} else {
				rs = Db2.rs("SELECT UserID, Email FROM SponsorInvite WHERE SponsorInviteID <> " + userID + " " +
				            "AND SponsorID = " + sponsorID + " AND Email = '" + Email.Text.Replace("'", "''") + "'");
				if (rs.Read()) {
					exists = true;
				}
				rs.Close();

				if (!exists) {
					int oldStoppedReason = 0;

					#region Update
					string sql = "";
					rs = Db2.rs("SELECT si.Email, si.UserID, si.StoppedReason FROM SponsorInvite si " +
					            "WHERE si.SponsorInviteID = " + userID);
					if (rs.Read()) {
						oldStoppedReason = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
						if (rs.IsDBNull(1)) {
							#region not activated
							if (rs.GetString(0) != Email.Text)
							{
								sql += ", Sent = NULL, InvitationKey = NEWID() ";
							}
							#endregion
						} else {
							if (UserUpdateFrom.SelectedValue == "2") {
								sql += ", Sent = NULL, InvitationKey = NEWID(), UserID = NULL ";

								#region Remove
								Db2.exec("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(1) + " AND SponsorID = " + sponsorID);
								Db2.exec("UPDATE UserProfile SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + rs.GetInt32(1) + " AND SponsorID = " + sponsorID);

								SqlDataReader rs2 = Db2.rs("SELECT UserProfileID FROM UserProfile WHERE UserID = " + rs.GetInt32(1));
								while (rs2.Read())
								{
									SqlDataReader rs3 = Db2.rs("SELECT BQ.BQID FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
									while (rs3.Read())
									{
										Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + rs2.GetInt32(0));
									}
									rs3.Close();
								}
								rs2.Close();
								#endregion
							} else if (UserUpdateFrom.SelectedValue == "0") {
								#region Update all
								Db2.exec("UPDATE [User] SET DepartmentID = " + DepartmentID.SelectedValue + " " +
								         "WHERE UserID = " + rs.GetInt32(1) + " AND SponsorID = " + sponsorID);
								Db2.exec("UPDATE UserProfile SET DepartmentID = " + DepartmentID.SelectedValue + " " +
								         "WHERE UserID = " + rs.GetInt32(1) + " AND SponsorID = " + sponsorID);

								SqlDataReader rs2 = Db2.rs("SELECT UserProfileID FROM UserProfile " +
								                           "WHERE UserID = " + rs.GetInt32(1));
								while (rs2.Read())
								{
									int profileID = rs2.GetInt32(0);

									SqlDataReader rs3 = Db2.rs("SELECT BQ.BQID, BQ.Type, BQ.Restricted FROM SponsorBQ sbq " +
									                           "INNER JOIN BQ ON sbq.BQID = BQ.BQID " +
									                           "WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
									while (rs3.Read())
									{
										switch (rs3.GetInt32(1))
										{
											case 1:
												goto case 7;
												//Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												//if (((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL")
												//{
												//    Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
												//}
												//break;
											case 2:
												string newVal = ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text.Replace("'", "''");
												if (rs3.IsDBNull(2) || newVal != "*****")
												{
													Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
													Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + profileID + "," + rs3.GetInt32(0) + ",'" + newVal.Replace("'", "''") + "')");
												}
												break;
											case 3:
												{
													Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
													string y = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
													string m = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
													string d = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
													if (y != "0" && m != "0" && d != "0")
													{
														try
														{
															DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
															if (tempDateTime < DateTime.Now)
															{
																Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + ",'" + tempDateTime.ToString("yyyy-MM-dd") + "')");
															}
														}
														catch (Exception) { }
													}
												}
												break;
											case 4:
												Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text) + ")");
												break;
											case 7:
												Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												if (((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL")
												{
													Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
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
								Db2.exec("UPDATE [User] SET DepartmentID = " + DepartmentID.SelectedValue + " WHERE UserID = " + rs.GetInt32(1) + " AND SponsorID = " + sponsorID);

								SqlDataReader rs2 = Db2.rs("SELECT u.UserProfileID, up.ProfileComparisonID FROM [User] u INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID WHERE u.UserID = " + rs.GetInt32(1));
								while (rs2.Read())
								{
									#region Create new profile
									Db2.exec("INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created) VALUES (" + rs.GetInt32(1) + "," + sponsorID + "," + DepartmentID.SelectedValue + "," + rs2.GetInt32(1) + ",GETDATE())");
									int profileID = 0;
									SqlDataReader rs3 = Db2.rs("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + rs.GetInt32(1) + " ORDER BY UserProfileID DESC");
									if (rs3.Read())
									{
										profileID = rs3.GetInt32(0);
									}
									rs3.Close();
									#endregion

									#region Copy old profile
									rs3 = Db2.rs("SELECT BQID, ValueInt, ValueText, ValueDate FROM UserProfileBQ WHERE UserProfileID = " + rs2.GetInt32(0));
									while (rs3.Read())
									{
										Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," +
										         (rs3.IsDBNull(1) ? "NULL" : rs3.GetInt32(1).ToString()) + "," +
										         (rs3.IsDBNull(2) ? "NULL" : "'" + rs3.GetString(2).Replace("'", "") + "'") + "," +
										         (rs3.IsDBNull(3) ? "NULL" : "'" + rs3.GetDateTime(3).ToString("yyyy-MM-dd") + "'") +
										         ")");
									}
									rs3.Close();
									#endregion

									#region Update with new hidden variables
									rs3 = Db2.rs("SELECT BQ.BQID, BQ.Type, BQ.Restricted FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
									while (rs3.Read())
									{
										switch (rs3.GetInt32(1))
										{
											case 1:
												goto case 7;
												//Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												//if (((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL")
												//{
												//    Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((RadioButtonList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
												//}
												//break;
											case 2:
												string newVal = ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text.Replace("'", "''");
												if (rs3.IsDBNull(2) || newVal != "*****")
												{
													Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
													Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueText) VALUES (" + profileID + "," + rs3.GetInt32(0) + ",'" + newVal.Replace("'", "''") + "')");
												}
												break;
											case 3:
												{
													Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
													string y = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
													string m = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
													string d = ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
													if (y != "0" && m != "0" && d != "0")
													{
														try
														{
															DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
															if (tempDateTime < DateTime.Now)
															{
																Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + ",'" + tempDateTime.ToString("yyyy-MM-dd") + "')");
															}
														}
														catch (Exception) { }
													}
												}
												break;
											case 4:
												Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((TextBox)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).Text) + ")");
												break;
											case 7:
												Db2.exec("DELETE FROM UserProfileBQ WHERE BQID = " + rs3.GetInt32(0) + " AND UserProfileID = " + profileID);
												if (((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedIndex != -1 && ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue != "NULL")
												{
													Db2.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," + Convert.ToInt32("0" + ((DropDownList)Hidden.FindControl("Hidden" + rs3.GetInt32(0))).SelectedValue) + ")");
												}
												break;
										}
									}
									rs3.Close();
									#endregion

									Db2.exec("UPDATE [User] SET UserProfileID = " + profileID + " WHERE UserID = " + rs.GetInt32(1));
								}
								rs2.Close();
								#endregion
							}
						}
					}
					rs.Close();
					#endregion
					Db2.exec("UPDATE SponsorInvite SET Email = '" + Email.Text.Replace("'", "''").Trim() + "'" + sql + ", " +
					         (stoppedReason != oldStoppedReason ? "" +
					          "StoppedReason=" + (stoppedReason == 0 ? "NULL" : stoppedReason.ToString()) + ",Stopped=" + (stoppedReason == 0 || Stopped.Text == "" ? "NULL" : "'" + Stopped.Text.Replace("'", "") + "'") + "," +
					          "" : "") +
					         "DepartmentID = " + DepartmentID.SelectedValue + " WHERE SponsorInviteID = " + userID);
					url += "&SDID=" + showDepartmentID;
				}
			}
			if (!exists) {
				#region update SponsorInviteBQ
				rs = Db2.rs("SELECT BQ.BQID, BQ.Type FROM SponsorBQ sbq INNER JOIN BQ ON sbq.BQID = BQ.BQID WHERE sbq.SponsorID = " + sponsorID + " AND sbq.Hidden = 1");
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
							Db2.exec("DELETE FROM SponsorInviteBQ WHERE BQID = " + rs.GetInt32(0) + " AND SponsorInviteID = " + userID);
							Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,BAID) VALUES (" + userID + "," + rs.GetInt32(0) + "," + val + ")");
						}
					} else if (rs.GetInt32(1) == 2) {
						string valText = "*****";
						try {
							if (((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text != "") {
								valText = ((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text;
							}
						} catch (Exception) { }
						if (valText != "*****") {
							Db2.exec("DELETE FROM SponsorInviteBQ WHERE BQID = " + rs.GetInt32(0) + " AND SponsorInviteID = " + userID);
							Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueText) VALUES (" + userID + "," + rs.GetInt32(0) + ",'" + valText.Replace("'", "''") + "')");
						}
					} else if (rs.GetInt32(1) == 4) {
						try {
							if (((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text != "") {
								val = Convert.ToInt32("0" + ((TextBox)Hidden.FindControl("Hidden" + rs.GetInt32(0))).Text);
							}
						} catch (Exception) { }
						if (val != int.MinValue) {
							Db2.exec("DELETE FROM SponsorInviteBQ WHERE BQID = " + rs.GetInt32(0) + " AND SponsorInviteID = " + userID);
							Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueInt) VALUES (" + userID + "," + rs.GetInt32(0) + "," + val + ")");
						}
					} else if (rs.GetInt32(1) == 3) {
						string y = ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "Y")).SelectedValue.Replace("'", "");
						string m = ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "M")).SelectedValue.Replace("'", "");
						string d = ((DropDownList)Hidden.FindControl("Hidden" + rs.GetInt32(0) + "D")).SelectedValue.Replace("'", "");
						if (y != "0" && m != "0" && d != "0") {
							try {
								DateTime tempDateTime = Convert.ToDateTime(y + "-" + m.PadLeft(2, '0') + '-' + d.PadLeft(2, '0'));
								if (tempDateTime < DateTime.Now) {
									Db2.exec("DELETE FROM SponsorInviteBQ WHERE BQID = " + rs.GetInt32(0) + " AND SponsorInviteID = " + userID);
									Db2.exec("INSERT INTO SponsorInviteBQ (SponsorInviteID,BQID,ValueDate) VALUES (" + userID + "," + rs.GetInt32(0) + ",'" + tempDateTime.ToString("yyyy-MM-dd") + "')");
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
			if (deptID == 0) {
				Db2.exec("INSERT INTO Department (" +
				         "SponsorID," +
				         "Department," +
				         "ParentDepartmentID" +
				         ") VALUES (" + sponsorID + "," +
				         "'" + Department.Text.Replace("'", "''") + "'," +
				         "" + ParentDepartmentID.SelectedValue + ")");
				SqlDataReader rs = Db2.rs("SELECT DepartmentID FROM Department WHERE SponsorID = " + sponsorID + " ORDER BY DepartmentID DESC");
				if (rs.Read()) {
					deptID = rs.GetInt32(0);
				}
				rs.Close();
				Db2.exec("UPDATE Department SET DepartmentShort = '" + (DepartmentShort.Text.Replace("'", "''") != "" ? DepartmentShort.Text.Replace("'", "''") : deptID.ToString()) + "', SortOrder = " + deptID + " WHERE DepartmentID = " + deptID);
				if (Session["SponsorAdminID"].ToString() != "-1") {
					Db2.exec("INSERT INTO SponsorAdminDepartment (SponsorAdminID,DepartmentID) VALUES (" + Session["SponsorAdminID"] + "," + deptID + ")");
				}
			} else {
				Db2.exec("UPDATE Department SET Department = '" + Department.Text.Replace("'", "''") + "'" +
				         ",DepartmentShort = '" + DepartmentShort.Text.Replace("'", "''") + "'" +
				         ", ParentDepartmentID = " + ParentDepartmentID.SelectedValue + " WHERE DepartmentID = " + deptID);
			}
			Db2.exec("UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID) WHERE SponsorID = " + sponsorID + "");

			Response.Redirect("org.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (showReg ? "&ShowReg=1" : ""), true);
		}
	}
}