using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using System.Configuration;

namespace HW.Grp
{
	public partial class SuperAdmin : System.Web.UI.Page
	{
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		int superAdminID;
		HW.Core.Models.SuperAdmin superAdmin;
//		protected int lid;
		SqlUserRepository userRepository = new SqlUserRepository();
//		protected int lid = Language.ENGLISH;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["SuperAdminID"] == null) {
				Response.Redirect("default.aspx?SuperLogout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			superAdminID = ConvertHelper.ToInt32(Session["SuperAdminID"]);
//			lid = ConvertHelper.ToInt32(Session["lid"], 2);
			var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			superAdmin = sponsorRepository.ReadSuperAdmin(superAdminID);
			submit.Click += new EventHandler(submit_Click);
			submit2.Click += new EventHandler(submit2_Click);
			
			int sendSponsorInvitationID = ConvertHelper.ToInt32(Request.QueryString["SendSPIID"]);
			int sponsorID = ConvertHelper.ToInt32(Request.QueryString["SponsorID"]);
			if (sendSponsorInvitationID != 0) {
				var i = sponsorRepository.ReadSponsorInviteBySponsor(sendSponsorInvitationID, sponsorID);
				if (i != null) {
					if (i.User == null) {
						sponsorRepository.UpdateSponsorInviteSent(sendSponsorInvitationID);
						Db.sendInvitation(sendSponsorInvitationID, i.Email.Trim(), i.Sponsor.InviteSubject, i.Sponsor.InviteText, i.InvitationKey);
					} else {
						string body = i.Sponsor.LoginText;

						string personalLink = "" + ConfigurationManager.AppSettings["healthWatchURL"] + "";
						if (i.User.ReminderLink > 0) {
							personalLink += "c/" + i.User.UserKey.ToLower() + i.User.Id.ToString();
						}
						if (body.IndexOf("<LINK/>") >= 0) {
							body = body.Replace("<LINK/>", personalLink);
						} else {
							body += "\r\n\r\n" + personalLink;
						}

						Db.sendMail2(i.Email.Trim(), i.Sponsor.LoginSubject, body);
					}
				}
			}

			if (!IsPostBack) {
				if (Request.QueryString["SearchEmail"] != null) {
					SearchEmail.Text = Request.QueryString["SearchEmail"];
					Search_Click(this, null);
				}
				
				for (int i = 2006; i <= DateTime.Now.Year; i++) {
					for (int j = 1; j <= 12; j++) {
						DateTime dt = new DateTime(i, j, 1);
						FromDT.Items.Add(new ListItem(i.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[j - 1], dt.ToString("yyyy-MM-dd")));
						if (i == DateTime.Now.Year - 1 && j == DateTime.Now.Month) {
							FromDT.SelectedValue = dt.ToString("yyyy-MM-dd");
						}
						dt = dt.AddMonths(1);
						ToDT.Items.Add(new ListItem(i.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[j - 1], dt.ToString("yyyy-MM-dd")));
						if (i == DateTime.Now.Year && j == DateTime.Now.Month) {
							ToDT.SelectedValue = dt.ToString("yyyy-MM-dd");
						}
					}
				}
			}
		}

		void submit2_Click(object sender, EventArgs e)
		{
			string qs1 = "";
			string qs2 = "";
			string not = "";

			if (Request.Form["Measure_0"] != null && Request.Form["Measure_0"] == "1") {
				qs1 = ",0";
			}
			if (Request.Form["Measure_0"] != null && Request.Form["Measure_0"] == "2") {
				qs2 = ",0";
			}

			foreach (var spru in sponsorRepository.FindSponsorProjectRoundUnits(Convert.ToInt32(Session["SuperAdminID"]))) {
				not += "," + spru.ProjectRoundUnit.Id;
				if (Request.Form["Measure_" + spru.ProjectRoundUnit.Id] != null && Request.Form["Measure_" + spru.ProjectRoundUnit.Id] == "1" && qs1 != ",0") {
					qs1 += "," + spru.ProjectRoundUnit.Id;
				}
				if (Request.Form["Measure_" + spru.ProjectRoundUnit.Id] != null && Request.Form["Measure_" + spru.ProjectRoundUnit.Id] == "2" && qs2 != ",0") {
					qs2 += "," + spru.ProjectRoundUnit.Id;
				}
			}
			if (qs1 != "") {
				string url = string.Format(
					@"superstats.aspx?N={0}&FDT={1}&TDT={2}&R1={3}&R2={4}&RNDS1={5}{6}&RID={7}",
					not.Substring(1),
					FromDT.SelectedValue,
					ToDT.SelectedValue,
					Measure2Txt1.Text,
					Measure2Txt2.Text,
					qs1.Substring(1),
					(qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : ""),
					ReportID.SelectedValue
				);
//				Response.Redirect(
//					url,
//					true
//				);
				ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "')", true);
			}

//			string query = string.Format(
//				@"
			//SELECT s.Sponsor,
//	ses.ProjectRoundUnitID,
//	ISNULL(r.SurveyID, ss.SurveyID),
//	ss.Internal
			//FROM Sponsor s
			//INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID
			//INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
			//INNER JOIN eform..ProjectRound rr ON r.ProjectRoundID = rr.ProjectRoundID
			//INNER JOIN eform..Survey ss ON ISNULL(r.SurveyID, ss.SurveyID) = ss.SurveyID
			//INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
			//WHERE sas.SuperAdminID = {0}
			//ORDER BY s.Sponsor, ses.Nav",
//				Convert.ToInt32(Session["SuperAdminID"])
//			);
//			SqlDataReader rs = Db.rs(query);
//			while (rs.Read())
//				not += "," + rs.GetInt32(1);
//				if (Request.Form["Measure_" + rs.GetInt32(1)] != null && Request.Form["Measure_" + rs.GetInt32(1)] == "1" && qs1 != ",0") {
//					qs1 += "," + rs.GetInt32(1).ToString();
//				if (Request.Form["Measure_" + rs.GetInt32(1)] != null && Request.Form["Measure_" + rs.GetInt32(1)] == "2" && qs2 != ",0") {
//					qs2 += "," + rs.GetInt32(1).ToString();
//			rs.Close();
			//Response.Redirect("http://" + Request.Url.Host + Request.Url.PathAndQuery.Substring(0, Request.Url.PathAndQuery.LastIndexOf("/")) + "/superstats.aspx?" +
		}

		void submit_Click(object sender, EventArgs e)
		{
			string qs1 = "";
			string qs2 = "";
			string not = "";

			if (Request.Form["Measure0"] != null && Request.Form["Measure0"] == "1") {
				qs1 = ",0";
			}
			if (Request.Form["Measure0"] != null && Request.Form["Measure0"] == "2") {
				qs2 = ",0";
			}

//			string query = string.Format(
//				@"
			//SELECT s.Sponsor,
//	ses.ProjectRoundID,
//	ses.Internal,
//	ses.RoundText,
//	ss.SurveyID,
//	ss.Internal
			//FROM Sponsor s
			//INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID
			//INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID
			//INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID
			//INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
			//WHERE sas.SuperAdminID =  {0}
			//ORDER BY s.Sponsor, ses.Internal, ses.RoundText",
//				Convert.ToInt32(Session["SuperAdminID"])
//			);
//			SqlDataReader rs = Db.rs(query);
//			while (rs.Read())
			foreach (var ses in sponsorRepository.FindExtendedSurveysBySuperAdmin2(Convert.ToInt32(Session["SuperAdminID"])))
			{
//				not += "," + rs.GetInt32(1);
				not += "," + ses.ProjectRound.Id;
//				if (Request.Form["Measure" + rs.GetInt32(1)] != null && Request.Form["Measure" + rs.GetInt32(1)] == "1" && qs1 != ",0") {
				if (Request.Form["Measure" + ses.ProjectRound.Id] != null && Request.Form["Measure" + ses.ProjectRound.Id] == "1" && qs1 != ",0") {
//					qs1 += "," + rs.GetInt32(1).ToString();
					qs1 += "," + ses.ProjectRound.Id.ToString();
				}
//				if (Request.Form["Measure" + rs.GetInt32(1)] != null && Request.Form["Measure" + rs.GetInt32(1)] == "2" && qs2 != ",0") {
				if (Request.Form["Measure" + ses.ProjectRound.Id] != null && Request.Form["Measure" + ses.ProjectRound.Id] == "2" && qs2 != ",0") {
//					qs2 += "," + rs.GetInt32(1).ToString();
					qs2 += "," + ses.ProjectRound.Id.ToString();
				}
			}
//			rs.Close();
			if (qs1 != "") {
				Response.Redirect(
					"" + ConfigurationManager.AppSettings["eFormURL"] + "feedback.aspx?" +
					"RNDS=" + not.Substring(1) + "" +
					"&R1=" + MeasureTxt1.Text + "&R2=" + MeasureTxt2.Text + "&RNDS1=" + qs1.Substring(1) + (qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") + "&SID=" + SurveyID.SelectedValue + "&SN=" + SurveyName.Text,
					true
				);
			}
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			Search.Text = R.Str(lid, "search", "Search");
			submit.Text = R.Str(lid, "submit", "Submit");
			submit2.Text = R.Str(lid, "submit", "Submit");

			ExtendedSurvey.Text = string.Format(
				@"
<tr>
	<td><i>{0}</i></td>
	<td><input type='radio' name='Measure0' value='1'/></td>
	<td><input type='radio' name='Measure0' value='2'/></td>
	<td><input type='radio' name='Measure0' value='0' checked/></td>
</tr>", R.Str(lid, "org.database", "Database, all <b>other</b> organizations"));

			int cx = 0;
			int bx = 0;
//			string query = string.Format(
//				@"
			//SELECT s.Sponsor,
//	ses.ProjectRoundID,
//	ses.Internal,
//	ses.RoundText,
//	ss.SurveyID,
//	ss.Internal,
//	(SELECT COUNT(*) FROM eform..Answer a WHERE a.ProjectRoundID = r.ProjectRoundID AND a.EndDT IS NOT NULL) AS CX
			//FROM Sponsor s
			//INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID
			//INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID
			//INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID
			//INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
			//WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
			//ORDER BY s.Sponsor, ses.Internal, ses.RoundText",
//				Convert.ToInt32(Session["SuperAdminID"])
//			);
//			SqlDataReader rs = Db.rs(query);
//			while (rs.Read())
			foreach (var ses in sponsorRepository.FindExtendedSurveysBySuperAdmin(Convert.ToInt32(Session["SuperAdminID"]))) {
//				if (SurveyID.Items.FindByValue(rs.GetInt32(4).ToString()) == null)
				if (SurveyID.Items.FindByValue(ses.ProjectRound.Survey.Id.ToString()) == null) {
//					SurveyID.Items.Add(new ListItem(rs.GetString(5), rs.GetInt32(4).ToString()));
					SurveyID.Items.Add(new ListItem(ses.ProjectRound.Survey.Internal, ses.ProjectRound.Survey.Id.ToString()));
					if (SurveyName.Text == "") {
//						SurveyName.Text = rs.GetString(5);
						SurveyName.Text = ses.ProjectRound.Survey.Internal;
						if (SurveyName.Text.IndexOf(" ") >= 0) {
							SurveyName.Text = SurveyName.Text.Substring(0, SurveyName.Text.IndexOf(" "));
						}
					}
				}
//				ExtendedSurvey.Text += "<tr" + (cx % 2 == 0 ? " style='background-color:#F2F2F2;'" : "") + "><td>" + rs.GetString(0) + (rs.IsDBNull(2) ? ", " + rs.GetString(2) : "") + (!rs.IsDBNull(3) ? ", " + rs.GetString(3) : "") + "</TD><TD><INPUT TYPE='radio' NAME='Measure" + rs.GetInt32(1) + "' VALUE='1'/></TD><TD><INPUT TYPE='radio' NAME='Measure" + rs.GetInt32(1) + "' VALUE='2'/></TD><TD><INPUT TYPE='radio' NAME='Measure" + rs.GetInt32(1) + "' VALUE='0' CHECKED/></TD><TD>" + rs.GetInt32(6) + "</TD></TR>";
				ExtendedSurvey.Text += string.Format(
					@"
<tr{0}>
	<td>{1}{2}</TD>
	<td><input type='radio' name='Measure{3}' value='1'/></td>
	<td><input type='radio' name='Measure{3}' value='2'/></td>
	<td><input type='radio' name='Measure{3}' value='0' checked/></td>
	<td>{4}</td>
</tr>",
					(cx % 2 == 0 ? " style='background-color:#F2F2F2;'" : ""),
					ses.Sponsor.Name + (ses.Internal != "" ? ", " + ses.Internal : ""),
					(ses.RoundText != "" ? ", " + ses.RoundText : ""),
					ses.ProjectRound.Id,
					ses.ProjectRound.Answers.Capacity
				);
				cx++;
//				bx += rs.GetInt32(6);
				bx += ses.ProjectRound.Answers.Capacity;
			}
//			rs.Close();
			ExtendedSurvey.Text += string.Format(
				@"
<tr style='background-color:#cccccc;'>
	<td><i>{1}</i></td>
	<td></td>
	<td></td>
	<td></td>
	<td>{0}</td>
</tr>",
				bx,
				R.Str(lid, "org.total", "Total for your organization(s)")
			);
			ESS.Visible = cx > 0;

			SponsorID.Text = string.Format(
				@"
<tr>
    <td><b>{0}</b></td>
    <td><b>{1}</b></td>
    <td><b>{2}</b></td>
    <td><b>{3}</b></td>
    <td><b>{4}</b></td>
    <td><b>{5}</b></td>
    <td><b>{6}</b></td>
</tr>",
				R.Str(lid, "manager.name", "Name"),
				R.Str(lid, "survey.extended.no", "# of ext<br/>surveys"),
				R.Str(lid, "users.no", "# of added<br/>users"),
				R.Str(lid, "users.invited.no", "# of invited<br/>users"),
				R.Str(lid, "users.activated.no", "# of activated<br/>users"),
				R.Str(lid, "invite.first", "1st invite sent"),
				R.Str(lid, "privilege.super", "Super privileges")
			);

			cx = 0;
			int totInvitees = 0, totNonClosedInvites = 0, totActive = 0, totNonClosedActive = 0;
//			query = string.Format(
//				@"
			//SELECT s.SponsorID,
//	s.Sponsor,
//	LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8),
//	(SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = s.SponsorID),
//	(SELECT COUNT(*) FROM SponsorInvite si WHERE si.Sent IS NOT NULL AND si.SponsorID = s.SponsorID),
//	(SELECT COUNT(*) FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID WHERE si.SponsorID = s.SponsorID),
//	(SELECT MIN(si.Sent) FROM SponsorInvite si WHERE si.SponsorID = s.SponsorID),
//	sas.SeeUsers,
//	(SELECT COUNT(*) FROM SponsorInvite si WHERE si.SponsorID = s.SponsorID),
//	s.Closed
			//FROM Sponsor s
			//INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
			//WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
			//ORDER BY s.Sponsor",
//				Convert.ToInt32(Session["SuperAdminID"])
//			);
//			rs = Db.rs(query);
//			while (rs.Read())
			foreach (var sap in sponsorRepository.FindSuperAdminSponsors(Convert.ToInt32(Session["SuperAdminID"]), superAdmin.HideClosedSponsors))
			{
//				totInvitees += rs.GetInt32(4);
				totInvitees += sap.Sponsor.SentInvites.Capacity;
//				if (rs.IsDBNull(9))
				if (sap.Sponsor.Closed == null)
				{
//					totNonClosedInvites += rs.GetInt32(4);
					totNonClosedInvites += sap.Sponsor.SentInvites.Capacity;
				}
//				totActive += rs.GetInt32(5);
				totActive += sap.Sponsor.ActiveInvites.Capacity;
//				if (rs.IsDBNull(9))
				if (sap.Sponsor.Closed == null)
				{
//					totNonClosedActive += rs.GetInt32(5);
					totNonClosedActive += sap.Sponsor.ActiveInvites.Capacity;
				}
//				SponsorID.Text += "<tr" + (cx % 2 == 0 ? " BGCOLOR='#F2F2F2'" : "") + ">" +
//					"<td><a" + (!rs.IsDBNull(9) ? " style='text-decoration:line-through;color:#cc0000;'" : "") + " HREF='default.aspx?SA=0&SKEY=" + rs.GetString(2) + rs.GetInt32(0).ToString() + "' target='_blank'>" + rs.GetString(1) + "</A></TD>" +
//					"<td>" + rs.GetInt32(3) + "</TD>" +
//					"<td>" + rs.GetInt32(8) + "</TD>" +
//					"<td>" + rs.GetInt32(4) + "</TD>" +
//					"<td>" + rs.GetInt32(5) + "</TD>" +
//					"<td>" + (rs.IsDBNull(6) ? "N/A" : rs.GetDateTime(6).ToString("yyyy-MM-dd")) + "</TD>" +
//					"<td>" + (rs.IsDBNull(7) ? "No" : "Yes") + "</TD>" +
//					"<td>" + (!rs.IsDBNull(6) ? "<A HREF='superadmin.aspx?ATSID=" + rs.GetInt32(0) + "'><img src='img/auditTrail.gif' border='0'/></a>" : "") + (rs.IsDBNull(9) ? "" : " <span style='color:#cc0000;'>Closed " + rs.GetDateTime(9).ToString("yyyy-MM-dd") + "</span>") + "</td>" +
//					"</tr>";
				SponsorID.Text += string.Format(
                    @"
<tr{0}>
	<td><a{1} href='default.aspx?SA=0&SKEY={2}{3}' target='sponsor{3}'>{4}</a></td>
    <!--<td><a{1} onclick='window.open(""default.aspx?SA=0&SKEY={2}{3}"", ""sponsor{3}"", ""width=1280,height=720"")'>{4}</a></td>-->
	<td>{5}</td>
	<td>{6}</td>
	<td>{7}</td>
	<td>{8}</td>
	<td>{9}</td>
	<td>{10}</td>
	<td>{11}{12}</td>
</tr>",
					(cx % 2 == 0 ? " bgcolor='#F2F2F2'" : ""),
					(sap.Sponsor.Closed != null ? " style='text-decoration:line-through;color:#cc0000;'" : ""),
					sap.Sponsor.SponsorKey,
					sap.Sponsor.Id,
					sap.Sponsor.Name,
					sap.Sponsor.ExtendedSurveys.Capacity,
					sap.Sponsor.Invites.Capacity,
					sap.Sponsor.SentInvites.Capacity,
					sap.Sponsor.ActiveInvites.Capacity,
					(sap.Sponsor.MinimumInviteDate == null ? "N/A" : sap.Sponsor.MinimumInviteDate.Value.ToString("yyyy-MM-dd")),
					(sap.SeeUsers ? "No" : "Yes"),
					(sap.Sponsor.MinimumInviteDate != null ? "<a href='superadmin.aspx?ATSID=" + sap.Sponsor.Id + "'><img src='img/auditTrail.gif' border='0'/></a>" : ""),
					(sap.Sponsor.Closed == null ? "" : " <span style='color:#cc0000;'>Closed " + sap.Sponsor.Closed.Value.ToString("yyyy-MM-dd") + "</span>")
				);

				if (Request.QueryString["ATSID"] != null && Convert.ToInt32(Request.QueryString["ATSID"]) == sap.Sponsor.Id) {
					int y = sap.Sponsor.MinimumInviteDate.Value.Year; int m = sap.Sponsor.MinimumInviteDate.Value.Month;
					int y2 = DateTime.Now.Year; int m2 = DateTime.Now.Month;
					for (int i = y; i <= y2; i++) {
						for (int ii = (i == y ? m : 1); ii <= (i == y2 ? m2 : 12); ii++) {
							string iii = i.ToString() + "-" + ii.ToString().PadLeft(2, '0');

							SponsorID.Text += "<tr" + (cx % 2 == 0 ? " BGCOLOR='#F2F2F2'" : "") + "><td>" + iii + "</td><td>&nbsp;</td><td>&nbsp;</td><td>";

							DateTime dt = DateTime.Parse(iii + "-01").AddMonths(1);
//							query = string.Format(
//								"SELECT COUNT(*) " +
//								"FROM SponsorInvite si LEFT OUTER JOIN [User] u ON si.UserID = u.UserID " +
//								"WHERE si.SponsorID = " + sap.Sponsor.Id + " AND (ISNULL(u.Created,si.Sent) < '" + dt.ToString("yyyy-MM-dd") + "' OR si.Sent < '" + dt.ToString("yyyy-MM-dd") + "')"
//							);
//							SqlDataReader rs2 = Db.rs(query);
//							if (rs2.Read())
							int invites = sponsorRepository.CountSentInvitesBySponsor2(sap.Sponsor.Id, dt);
							if (invites > 0) {
//								SponsorID.Text += rs2.GetInt32(0).ToString();
								SponsorID.Text += invites.ToString();
							}
//							rs2.Close();
							SponsorID.Text += "</td><td>";
//							query = string.Format(
//								"SELECT COUNT(*) " +
//								"FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID " +
//								"WHERE si.SponsorID = " + sap.Sponsor.Id + " AND u.Created < '" + dt.ToString("yyyy-MM-dd") + "'"
//							);
//							rs2 = Db.rs(query);
//							if (rs2.Read())
							int invites2 = sponsorRepository.CountSentInvitesBySponsor3(sap.Sponsor.Id, dt);
							if (invites2 > 0) {
//								SponsorID.Text += rs2.GetInt32(0).ToString();
								SponsorID.Text += invites2.ToString();
							}
//							rs2.Close();
							SponsorID.Text += "</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
						}
					}
				}
				cx++;
			}
//			rs.Close();
			SponsorID.Text += string.Format(
				@"
<tr>
	<td colspan='3'>&nbsp;</td>
	<td>{0}<b>{1}</b></td>
	<td>{2}<b>{3}</b></td>
</tr>",
				(totNonClosedInvites != totInvitees ? "<span style='text-decoration:line-through;color:#cc0000;'>" + totInvitees + "</span><br/>" : ""),
				totNonClosedInvites,
				(totNonClosedActive != totActive ? "<span style='text-decoration:line-through;color:#cc0000;'>" + totActive + "</span><br/>" : ""),
				totNonClosedActive
			);

			Survey.Text = string.Format(
				@"
<tr>
	<td><i>Database, all <b>other</b> organizations</i></td>
	<td><input type='radio' name='Measure_0' value='1'{0}/></td>
	<td><input type='radio' name='Measure_0' value='2'{1}/></td>
	<td><input type='radio' name='Measure_0' value='0'{2}/></td>
</tr>",
				ConvertHelper.ToInt32(Request["Measure_0"], 0) == 1 ? " checked" : "",
				ConvertHelper.ToInt32(Request["Measure_0"], 0) == 2 ? " checked" : "",
				ConvertHelper.ToInt32(Request["Measure_0"], 0) == 0 ? " checked" : ""
			);

			//SqlDataReader rs = Db.rs("SELECT DISTINCT ses.SurveyID, ss.Internal FROM Sponsor s " +
			//    "INNER JOIN SponsorProjectRoundUnit ses ON s.SponsorID = ses.SponsorID " +
			//    "INNER JOIN eform..Survey ss ON ses.SurveyID = ss.SurveyID");
			//while (rs.Read())
			//{
			//    SurveyID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
			//}
			//rs.Close();
			cx = 0; bx = 0;
//			query = string.Format(
//				@"
			//SELECT DISTINCT
//	s.Sponsor,
//	ses.ProjectRoundUnitID,
//	ses.Nav,
//	rep.ReportID,
//	rep.Internal,
//	(SELECT COUNT(DISTINCT a.ProjectRoundUserID) FROM eform..Answer a WHERE a.ProjectRoundUnitID = r.ProjectRoundUnitID AND a.EndDT >= '{0}' AND a.EndDT < '{1}') AS CX
			//FROM Sponsor s
			//INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID
			//INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
			//INNER JOIN eform..Report rep ON rep.ReportID = r.ReportID
			//INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
			//WHERE s.Deleted IS NULL AND sas.SuperAdminID = {2}
			//ORDER BY s.Sponsor, ses.Nav",
//				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
//				DateTime.Now.ToString("yyyy-MM-01"),
//				Convert.ToInt32(Session["SuperAdminID"])
//			);
//			rs = Db.rs(query);
//			while (rs.Read())
			foreach (var ss in sponsorRepository.FindDistinctRoundUnitsWithReportBySuperAdmin(Convert.ToInt32(Session["SuperAdminID"])))
			{
//				if (ReportID.Items.FindByValue(rs.GetInt32(3).ToString()) == null)
				if (ReportID.Items.FindByValue(ss.ProjectRoundUnit.Report.Id.ToString()) == null)
				{
//					ReportID.Items.Add(new ListItem(rs.GetString(4), rs.GetInt32(3).ToString()));
					ReportID.Items.Add(new ListItem(ss.ProjectRoundUnit.Report.Internal, ss.ProjectRoundUnit.Report.Id.ToString()));
				}
//				Survey.Text += "<tr" + (cx % 2 == 0 ? " style='background-color:#F2F2F2;'" : "") + ">" +
//					"<td>" + rs.GetString(0) + ", " + rs.GetString(2) + "</TD>" +
//					"<td><input type='radio' name='Measure_" + rs.GetInt32(1) + "' value='1'/></td>" +
//					"<td><input type='radio' name='Measure_" + rs.GetInt32(1) + "' value='2'/></td>" +
//					"<td><input type='radio' name='Measure_" + rs.GetInt32(1) + "' value='0' checked/></td>" +
//					"<td>" + rs.GetInt32(5) + "</td>" +
//					"</tr>";
				Survey.Text += string.Format(
					@"
<tr{0}>
	<td>{1}, {2}</td>
	<td><input type='radio' name='Measure_{3}' value='1'{5}/></td>
	<td><input type='radio' name='Measure_{3}' value='2'{6}/></td>
	<td><input type='radio' name='Measure_{3}' value='0'{7}/></td>
	<td>{4}</td>
</tr>",
					(cx % 2 == 0 ? " style='background-color:#F2F2F2;'" : ""),
					ss.Sponsor.Name,
					ss.Navigation,
					ss.ProjectRoundUnit.Id,
					ss.ProjectRoundUnit.Answers.Capacity,
					ConvertHelper.ToInt32(Request["Measure_" + ss.ProjectRoundUnit.Id.ToString()], 0) == 1 ? " checked" : "",
					ConvertHelper.ToInt32(Request["Measure_" + ss.ProjectRoundUnit.Id.ToString()], 0) == 2 ? " checked" : "",
					ConvertHelper.ToInt32(Request["Measure_" + ss.ProjectRoundUnit.Id.ToString()], 0) == 0 ? " checked" : ""
				);
				cx++;
//				bx += rs.GetInt32(5);
				bx += ss.ProjectRoundUnit.Answers.Capacity;
			}
//			rs.Close();
			Survey.Text += string.Format(
				@"
<tr style='background-color:#cccccc;'>
	<td><i>{1}</i></td>
	<td></td>
	<td></td>
	<td></td>
	<td>{0}</td>
</tr>",
				bx,
				R.Str(lid, "org.total", "Total for your organization(s)"));
		}
		
		protected IList<User> users;
		protected string searchQuery;
//		SqlUserRepository userRepository = new SqlUserRepository();

		protected void Search_Click(object sender, EventArgs e)
		{
			if (SearchEmail.Text != "")
			{
				searchQuery = SearchEmail.Text.Replace("'", "");
				users = userRepository.FindLikeUsers(superAdminID, searchQuery);
			}
		}
	}
}