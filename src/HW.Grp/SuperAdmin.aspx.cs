using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using System.Configuration;

namespace HW.Grp
{
	public partial class SuperAdmin : System.Web.UI.Page
	{
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["SuperAdminID"] == null)
			{
				Response.Redirect("default.aspx?SuperLogout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			submit.Click += new EventHandler(submit_Click);
			submit2.Click += new EventHandler(submit2_Click);

			if (!IsPostBack)
			{
				for (int i = 2006; i <= DateTime.Now.Year; i++)
				{
					for (int j = 1; j <= 12; j++)
					{
						DateTime dt = new DateTime(i, j, 1);
						FromDT.Items.Add(new ListItem(i.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[j - 1], dt.ToString("yyyy-MM-dd")));
						if (i == DateTime.Now.Year - 1 && j == DateTime.Now.Month)
						{
							FromDT.SelectedValue = dt.ToString("yyyy-MM-dd");
						}
						dt = dt.AddMonths(1);
						ToDT.Items.Add(new ListItem(i.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[j - 1], dt.ToString("yyyy-MM-dd")));
						if (i == DateTime.Now.Year && j == DateTime.Now.Month)
						{
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
			foreach (var spru in sponsorRepository.FindSponsorProjectRoundUnits(Convert.ToInt32(Session["SuperAdminID"])))
			{
//				not += "," + rs.GetInt32(1);
				not += "," + spru.ProjectRoundUnit.Id;
//				if (Request.Form["Measure_" + rs.GetInt32(1)] != null && Request.Form["Measure_" + rs.GetInt32(1)] == "1" && qs1 != ",0") {
				if (Request.Form["Measure_" + spru.ProjectRoundUnit.Id] != null && Request.Form["Measure_" + spru.ProjectRoundUnit.Id] == "1" && qs1 != ",0") {
//					qs1 += "," + rs.GetInt32(1).ToString();
					qs1 += "," + spru.ProjectRoundUnit.Id;
				}
//				if (Request.Form["Measure_" + rs.GetInt32(1)] != null && Request.Form["Measure_" + rs.GetInt32(1)] == "2" && qs2 != ",0") {
				if (Request.Form["Measure_" + spru.ProjectRoundUnit.Id] != null && Request.Form["Measure_" + spru.ProjectRoundUnit.Id] == "2" && qs2 != ",0") {
//					qs2 += "," + rs.GetInt32(1).ToString();
					qs2 += "," + spru.ProjectRoundUnit.Id;
				}
			}
//			rs.Close();
			if (qs1 != "")
			{
				//Response.Redirect("http://" + Request.Url.Host + Request.Url.PathAndQuery.Substring(0, Request.Url.PathAndQuery.LastIndexOf("/")) + "/superstats.aspx?" +
				Response.Redirect(
					"superstats.aspx?" +
					"N=" + not.Substring(1) + "" +
					"&FDT=" + FromDT.SelectedValue + "" +
					"&TDT=" + ToDT.SelectedValue + "" +
					"&R1=" + Measure2Txt1.Text + "" +
					"&R2=" + Measure2Txt2.Text + "" +
					"&RNDS1=" + qs1.Substring(1) +
					(qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") +
					"&RID=" + ReportID.SelectedValue,
					true
				);
			}
		}

		void submit_Click(object sender, EventArgs e)
		{
			string qs1 = "", qs2 = "", not = "";

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
			if (qs1 != "")
			{
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

			ExtendedSurvey.Text = "" +
				"<TR>" +
				"<TD><I>Database, all <B>other</B> organizations</I></TD>" +
				"<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"1\"/></TD>" +
				"<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"2\"/></TD>" +
				"<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"0\" CHECKED/></TD>" +
				"</TR>";

			int cx = 0, bx = 0;
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
			foreach (var ses in sponsorRepository.FindExtendedSurveysBySuperAdmin(Convert.ToInt32(Session["SuperAdminID"])))
			{
//				if (SurveyID.Items.FindByValue(rs.GetInt32(4).ToString()) == null)
				if (SurveyID.Items.FindByValue(ses.ProjectRound.Survey.Id.ToString()) == null)
				{
//					SurveyID.Items.Add(new ListItem(rs.GetString(5), rs.GetInt32(4).ToString()));
					SurveyID.Items.Add(new ListItem(ses.ProjectRound.Survey.Internal, ses.ProjectRound.Survey.Id.ToString()));
					if (SurveyName.Text == "")
					{
//						SurveyName.Text = rs.GetString(5);
						SurveyName.Text = ses.ProjectRound.Survey.Internal;
						if (SurveyName.Text.IndexOf(" ") >= 0)
						{
							SurveyName.Text = SurveyName.Text.Substring(0, SurveyName.Text.IndexOf(" "));
						}
					}
				}
//				ExtendedSurvey.Text += "<TR" + (cx % 2 == 0 ? " style=\"background-color:#F2F2F2;\"" : "") + "><TD>" + rs.GetString(0) + (rs.IsDBNull(2) ? ", " + rs.GetString(2) : "") + (!rs.IsDBNull(3) ? ", " + rs.GetString(3) : "") + "</TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"1\"/></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"2\"/></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"0\" CHECKED/></TD><TD>" + rs.GetInt32(6) + "</TD></TR>";
				ExtendedSurvey.Text += "<TR" + (cx % 2 == 0 ? " style=\"background-color:#F2F2F2;\"" : "") + "><TD>" + ses.Sponsor.Name + (ses.Internal != "" ? ", " + ses.Internal : "") + (ses.RoundText != "" ? ", " + ses.RoundText : "") + "</TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure" + ses.ProjectRound.Id + "\" VALUE=\"1\"/></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure" + ses.ProjectRound.Id + "\" VALUE=\"2\"/></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure" + ses.ProjectRound.Id + "\" VALUE=\"0\" CHECKED/></TD><TD>" + ses.ProjectRound.Answers.Count + "</TD></TR>";
				cx++;
//				bx += rs.GetInt32(6);
				bx += ses.ProjectRound.Answers.Count;
			}
//			rs.Close();
			ExtendedSurvey.Text += "" +
				"<TR style=\"background-color:#cccccc;\">" +
				"<TD><i>Total for your organization(s)</i></TD>" +
				"<TD></TD>" +
				"<TD></TD>" +
				"<TD></TD>" +
				"<TD>" + bx + "</TD>" +
				"</TR>";
			ESS.Visible = cx > 0;

			SponsorID.Text = "" +
				"<TR>" +
				"<TD><B>Name</B></TD>" +
				"<TD><B># of ext<br/>surveys</B></TD>" +
				"<TD><B># of added<br/>users</B></TD>" +
				"<TD><B># of invited<br/>users</B></TD>" +
				"<TD><B># of activated<br/>users</B></TD>" +
				"<TD><B>1st invite sent</B></TD>" +
				"<TD><B>Super privileges</B></TD>" +
				"</TR>";

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
			foreach (var sap in sponsorRepository.FindSuperAdminSponsors(Convert.ToInt32(Session["SuperAdminID"])))
			{
//				totInvitees += rs.GetInt32(4);
				totInvitees += sap.Sponsor.SentInvites.Count;
//				if (rs.IsDBNull(9))
				if (sap.Sponsor.ClosedAt == null)
				{
//					totNonClosedInvites += rs.GetInt32(4);
					totNonClosedInvites += sap.Sponsor.SentInvites.Count;
				}
//				totActive += rs.GetInt32(5);
				totActive += sap.Sponsor.ActiveInvites.Count;
//				if (rs.IsDBNull(9))
				if (sap.Sponsor.ClosedAt == null)
				{
//					totNonClosedActive += rs.GetInt32(5);
					totNonClosedActive += sap.Sponsor.ActiveInvites.Count;
				}
//				SponsorID.Text += "<TR" + (cx % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + ">" +
//					"<TD><A" + (!rs.IsDBNull(9) ? " style=\"text-decoration:line-through;color:#cc0000;\"" : "") + " HREF=\"default.aspx?SA=0&SKEY=" + rs.GetString(2) + rs.GetInt32(0).ToString() + "\" TARGET=\"_blank\">" + rs.GetString(1) + "</A></TD>" +
//					"<TD>" + rs.GetInt32(3) + "</TD>" +
//					"<TD>" + rs.GetInt32(8) + "</TD>" +
//					"<TD>" + rs.GetInt32(4) + "</TD>" +
//					"<TD>" + rs.GetInt32(5) + "</TD>" +
//					"<TD>" + (rs.IsDBNull(6) ? "N/A" : rs.GetDateTime(6).ToString("yyyy-MM-dd")) + "</TD>" +
//					"<TD>" + (rs.IsDBNull(7) ? "No" : "Yes") + "</TD>" +
//					"<TD>" + (!rs.IsDBNull(6) ? "<A HREF=\"superadmin.aspx?ATSID=" + rs.GetInt32(0) + "\"><img src=\"img/auditTrail.gif\" border=\"0\"/></A>" : "") + (rs.IsDBNull(9) ? "" : " <span style=\"color:#cc0000;\">Closed " + rs.GetDateTime(9).ToString("yyyy-MM-dd") + "</span>") + "</TD>" +
//					"</TR>";
				SponsorID.Text += "<TR" + (cx % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + ">" +
					"<TD><A" + (sap.Sponsor.ClosedAt != null ? " style=\"text-decoration:line-through;color:#cc0000;\"" : "") + " HREF=\"default.aspx?SA=0&SKEY=" + sap.Sponsor.SponsorKey + sap.Sponsor.Id.ToString() + "\" TARGET=\"_blank\">" + sap.Sponsor.Name + "</A></TD>" +
					"<TD>" + sap.Sponsor.ExtendedSurveys.Capacity + "</TD>" +
					"<TD>" + sap.Sponsor.Invites.Capacity + "</TD>" +
					"<TD>" + sap.Sponsor.SentInvites.Capacity + "</TD>" +
					"<TD>" + sap.Sponsor.ActiveInvites.Capacity + "</TD>" +
					"<TD>" + (sap.Sponsor.MinimumInviteDate == null ? "N/A" : sap.Sponsor.MinimumInviteDate.Value.ToString("yyyy-MM-dd")) + "</TD>" +
					"<TD>" + (sap.SeeUsers ? "No" : "Yes") + "</TD>" +
					"<TD>" + (sap.Sponsor.MinimumInviteDate != null ? "<A HREF=\"superadmin.aspx?ATSID=" + sap.Sponsor.Id + "\"><img src=\"img/auditTrail.gif\" border=\"0\"/></A>" : "") + (sap.Sponsor.ClosedAt == null ? "" : " <span style=\"color:#cc0000;\">Closed " + sap.Sponsor.ClosedAt.Value.ToString("yyyy-MM-dd") + "</span>") + "</TD>" +
					"</TR>";

				if (Request.QueryString["ATSID"] != null && Convert.ToInt32(Request.QueryString["ATSID"]) == sap.Sponsor.Id)
				{
					int y = sap.Sponsor.MinimumInviteDate.Value.Year; int m = sap.Sponsor.MinimumInviteDate.Value.Month;
					int y2 = DateTime.Now.Year; int m2 = DateTime.Now.Month;
					for (int i = y; i <= y2; i++)
					{
						for (int ii = (i == y ? m : 1); ii <= (i == y2 ? m2 : 12); ii++)
						{
							string iii = i.ToString() + "-" + ii.ToString().PadLeft(2, '0');

							SponsorID.Text += "<TR" + (cx % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + "><td>" + iii + "</td><td>&nbsp;</td><td>&nbsp;</td><td>";

							DateTime dt = DateTime.Parse(iii + "-01").AddMonths(1);
//							query = string.Format(
//								"SELECT COUNT(*) " +
//								"FROM SponsorInvite si LEFT OUTER JOIN [User] u ON si.UserID = u.UserID " +
//								"WHERE si.SponsorID = " + sap.Sponsor.Id + " AND (ISNULL(u.Created,si.Sent) < '" + dt.ToString("yyyy-MM-dd") + "' OR si.Sent < '" + dt.ToString("yyyy-MM-dd") + "')"
//							);
//							SqlDataReader rs2 = Db.rs(query);
//							if (rs2.Read())
							int invites = sponsorRepository.CountSentInvitesBySponsor2(sap.Sponsor.Id, dt);
							if (invites > 0)
							{
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
							if (invites2 > 0)
							{
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
			SponsorID.Text += "<TR>" +
				"<TD COLSPAN=\"3\">&nbsp;</TD>" +
				"<TD>" + (totNonClosedInvites != totInvitees ? "<span style=\"text-decoration:line-through;color:#cc0000;\">" + totInvitees + "</span><br/>" : "") + "<b>" + totNonClosedInvites + "</b></TD>" +
				"<TD>" + (totNonClosedActive != totActive ? "<span style=\"text-decoration:line-through;color:#cc0000;\">" + totActive + "</span><br/>" : "") + "<b>" + totNonClosedActive + "</b></TD>" +
				"</TR>";

			Survey.Text = "<TR><TD><I>Database, all <B>other</B> organizations</I></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure_0\" VALUE=\"1\"/></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure_0\" VALUE=\"2\"/></TD><TD><INPUT TYPE=\"radio\" NAME=\"Measure_0\" VALUE=\"0\" CHECKED/></TD></TR>";

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
//				Survey.Text += "<TR" + (cx % 2 == 0 ? " style=\"background-color:#F2F2F2;\"" : "") + ">" +
//					"<TD>" + rs.GetString(0) + ", " + rs.GetString(2) + "</TD>" +
//					"<TD><INPUT TYPE=\"radio\" NAME=\"Measure_" + rs.GetInt32(1) + "\" VALUE=\"1\"/></TD>" +
//					"<TD><INPUT TYPE=\"radio\" NAME=\"Measure_" + rs.GetInt32(1) + "\" VALUE=\"2\"/></TD>" +
//					"<TD><INPUT TYPE=\"radio\" NAME=\"Measure_" + rs.GetInt32(1) + "\" VALUE=\"0\" CHECKED/></TD>" +
//					"<TD>" + rs.GetInt32(5) + "</TD>" +
//					"</TR>";
				Survey.Text += "<TR" + (cx % 2 == 0 ? " style=\"background-color:#F2F2F2;\"" : "") + ">" +
					"<TD>" + ss.Sponsor.Name + ", " + ss.Navigation + "</TD>" +
					"<TD><INPUT TYPE=\"radio\" NAME=\"Measure_" + ss.ProjectRoundUnit.Id + "\" VALUE=\"1\"/></TD>" +
					"<TD><INPUT TYPE=\"radio\" NAME=\"Measure_" + ss.ProjectRoundUnit.Id + "\" VALUE=\"2\"/></TD>" +
					"<TD><INPUT TYPE=\"radio\" NAME=\"Measure_" + ss.ProjectRoundUnit.Id + "\" VALUE=\"0\" CHECKED/></TD>" +
					"<TD>" + ss.ProjectRoundUnit.Answers.Count + "</TD>" +
					"</TR>";
				cx++;
//				bx += rs.GetInt32(5);
				bx += ss.ProjectRoundUnit.Answers.Count;
			}
//			rs.Close();
			Survey.Text += "<TR style=\"background-color:#cccccc;\">" +
				"<TD><i>Total for your organization(s)</i></TD>" +
				"<TD></TD>" +
				"<TD></TD>" +
				"<TD></TD>" +
				"<TD>" + bx + "</TD>" +
				"</TR>";
		}
	}
}