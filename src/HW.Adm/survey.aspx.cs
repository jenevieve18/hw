using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;

namespace HW.Adm
{
    public partial class survey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            submit.Click += new EventHandler(submit_Click);

            if (!IsPostBack)
            {
                for (int i = 2006; i <= DateTime.Now.Year; i++)
                {
                    for (int ii = 1; ii <= 12; ii++)
                    {
                        DateTime dt = new DateTime(i, ii, 1);
                        FromDT.Items.Add(new ListItem(i.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[ii - 1], dt.ToString("yyyy-MM-dd")));
                        if (i == DateTime.Now.Year - 1 && ii == DateTime.Now.Month)
                        {
                            FromDT.SelectedValue = dt.ToString("yyyy-MM-dd");
                        }
                        dt = dt.AddMonths(1);
                        ToDT.Items.Add(new ListItem(i.ToString() + " " + System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[ii - 1], dt.ToString("yyyy-MM-dd")));
                        if (i == DateTime.Now.Year && ii == DateTime.Now.Month)
                        {
                            ToDT.SelectedValue = dt.ToString("yyyy-MM-dd");
                        }
                    }
                }
            }
        }

        void submit_Click(object sender, EventArgs e)
        {
            string qs1 = "", qs2 = "", qsd1 = "", qsd2 = "";

            if (Request.Form["Measure0"] != null && Request.Form["Measure0"] == "1") { qs1 = ",0"; }
            if (Request.Form["Measure0"] != null && Request.Form["Measure0"] == "2") { qs2 = ",0"; }
            if (Request.Form["Department0"] != null && Request.Form["Department0"] == "1") { qsd1 = ",0"; }
            if (Request.Form["Department0"] != null && Request.Form["Department0"] == "2") { qsd2 = ",0"; }

            SqlDataReader rs = Db.rs("SELECT " +
                    "s.Sponsor, " +
                    "ses.ProjectRoundUnitID, " +
                    "ISNULL(r.SurveyID,ss.SurveyID), " +
                    "ss.Internal, " +
                    "s.SponsorID " +
                    "FROM Sponsor s " +
                    "INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID " +
                    "INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID " +
                    "INNER JOIN eform..ProjectRound rr ON r.ProjectRoundID = rr.ProjectRoundID " +
                    "INNER JOIN eform..Survey ss ON ISNULL(r.SurveyID,ss.SurveyID) = ss.SurveyID " +
                    "ORDER BY s.Sponsor, ses.Nav");
            while (rs.Read())
            {
                if (Request.Form["Measure" + rs.GetInt32(1)] != null && Request.Form["Measure" + rs.GetInt32(1)] == "1" && qs1 != ",0") { qs1 += "," + rs.GetInt32(1).ToString(); }
                if (Request.Form["Measure" + rs.GetInt32(1)] != null && Request.Form["Measure" + rs.GetInt32(1)] == "2" && qs2 != ",0") { qs2 += "," + rs.GetInt32(1).ToString(); }

                if (Session["ExpandSID"] != null && Session["ExpandSID"].ToString().IndexOf("," + rs.GetInt32(4) + ",") >= 0)
                {
                    SqlDataReader rs2 = Db.rs("SELECT DISTINCT d.DepartmentID, r.ProjectRoundUserID FROM Department d " +
                        "INNER JOIN [UserProfile] u ON d.DepartmentID = u.DepartmentID " +
                        "INNER JOIN UserProjectRoundUserAnswer a ON u.UserProfileID = a.UserProfileID " +
                        "INNER JOIN UserProjectRoundUser r ON a.ProjectRoundUserID = r.ProjectRoundUserID " +
                        "WHERE r.ProjectRoundUnitID = " + rs.GetInt32(1) + " AND d.SponsorID = " + rs.GetInt32(4));
                    while (rs2.Read())
                    {
                        if (Request.Form["Department" + rs2.GetInt32(0)] != null && Request.Form["Department" + rs2.GetInt32(0)] == "1" && qsd1 != ",0") { qsd1 += "," + rs2.GetInt32(1).ToString(); }
                        if (Request.Form["Department" + rs2.GetInt32(0)] != null && Request.Form["Department" + rs2.GetInt32(0)] == "2" && qsd2 != ",0") { qsd2 += "," + rs2.GetInt32(1).ToString(); }
                    }
                    rs2.Close();
                }
            }
            rs.Close();

            string s = "#";
            if (qs1 != "" || qsd1 != "")
            {
                s = "" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/superstats.aspx?" +
                "N=0" +
                "&FDT=" + FromDT.SelectedValue + "" +
                "&TDT=" + ToDT.SelectedValue + "" +
                "&R1=" + MeasureTxt1.Text +
                "&R2=" + MeasureTxt2.Text +
                (qs1 != "" ? "&RNDS1=" + qs1.Substring(1) : "") +
                (qsd1 != "" ? "&PID1=" + qsd1.Substring(1) : "") +
                (qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") +
                (qsd2 != "" ? "&PID2=" + qsd2.Substring(1) : "") +
                "&RID=" + ReportID.SelectedValue;
            }
            Res.Text = "<br/><br/><a style=\"margin:20px 20px 20px 20px;padding:20px 20px 20px 20px;border:2px solid #98c2a6;background-color:#FFF7D6;font-size:12px;font-weight:bold;\" href=\"" + s + "\" target=\"_blank\">" + (s != "#" ? "GO TO STATISTICS" : "NO DATA FOUND") + "</a><br/><br/><br/><br/>";
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Survey.Text = "<TR>" +
                "<TD><I>Database total</I></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"1\"" + (Request.Form["Measure0"] != null && Request.Form["Measure0"] == "1" ? " CHECKED" : "") + "/></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"2\"" + (Request.Form["Measure0"] != null && Request.Form["Measure0"] == "2" ? " CHECKED" : "") + "/></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"0\"" + (Request.Form["Measure0"] == null || Request.Form["Measure0"] == "0" ? " CHECKED" : "") + "/></TD>" +
                "</TR>";

            //SqlDataReader rs = Db.rs("SELECT DISTINCT ses.SurveyID, ss.Internal FROM Sponsor s " +
            //    "INNER JOIN SponsorProjectRoundUnit ses ON s.SponsorID = ses.SponsorID " +
            //    "INNER JOIN eform..Survey ss ON ses.SurveyID = ss.SurveyID");
            //while (rs.Read())
            //{
            //    SurveyID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
            //}
            //rs.Close();

            if (Request.QueryString["ExpandSID"] != null)
            {
                if (Session["ExpandSID"] == null)
                {
                    Session["ExpandSID"] = "0," + Convert.ToInt32(Request.QueryString["ExpandSID"]) + ",0";
                }
                else
                {
                    Session["ExpandSID"] = Session["ExpandSID"].ToString() + "," + Convert.ToInt32(Request.QueryString["ExpandSID"]) + ",0";
                }
            }

            int cx = 0, ssID = -1;
            SqlDataReader rs = Db.rs("SELECT DISTINCT " +
                    "s.Sponsor, " +                 // 0
                    "ses.ProjectRoundUnitID, " +
                    "ses.Nav, " +
                    "rep.ReportID, " +
                    "rep.Internal, " +
                    "s.Closed, " +                  // 5
                    "s.Deleted, " +
                    "ss.SuperSponsor, " +
                    "ss.SuperSponsorID, " +
                    "s.SponsorID " +
                    "FROM Sponsor s " +
                    "INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID " +
                    "INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID " +
                    "INNER JOIN eform..Report rep ON rep.ReportID = r.ReportID " +
                    "LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID " +
                    "ORDER BY ss.SuperSponsor, s.Sponsor, ses.Nav");
            while (rs.Read())
            {
                if (ssID != (rs.IsDBNull(8) ? 0 : rs.GetInt32(8)))
                {
                    ssID = (rs.IsDBNull(8) ? 0 : rs.GetInt32(8));
                    Survey.Text += "<TR><TD COLSPAN=\"4\"><h3 style=\"margin-top:10px;margin-bottom:5px;\">" + (ssID == 0 ? "" : "<img src=\"" + System.Configuration.ConfigurationManager.AppSettings["hwURL"] + "/img/partner/" + ssID + ".gif\"/> ") + (rs.IsDBNull(7) ? "OTHER SPONSOR" : rs.GetString(7).ToUpper()) + "</h3></TD></TR>";
                }
                if (ReportID.Items.FindByValue(rs.GetInt32(3).ToString()) == null)
                {
                    ReportID.Items.Add(new ListItem(rs.GetString(4), rs.GetInt32(3).ToString()));
                }
                Survey.Text += "<tr style=\"" + (cx % 2 == 0 ? "background-color:#cccccc;" : "") + "\">" +
                    "<td style=\"" + (!rs.IsDBNull(5) || !rs.IsDBNull(6) ? "text-decoration:line-through;color:#cc0000;" : "") + "\"><a href=\"survey.aspx?ExpandSID=" + rs.GetInt32(9) + "\">" + rs.GetString(0) + ", " + rs.GetString(2) + "</a></td>" +
                    "<td><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"1\"" + (Request.Form["Measure" + rs.GetInt32(1)] != null && Request.Form["Measure" + rs.GetInt32(1)] == "1" ? " CHECKED" : "") + "/></td>" +
                    "<td><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"2\"" + (Request.Form["Measure" + rs.GetInt32(1)] != null && Request.Form["Measure" + rs.GetInt32(1)] == "2" ? " CHECKED" : "") + "/></td>" +
                    "<td><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"0\"" + (Request.Form["Measure" + rs.GetInt32(1)] == null || Request.Form["Measure" + rs.GetInt32(1)] == "0" ? " CHECKED" : "") + "/></td>" +
                    "</tr>";
                if (Session["ExpandSID"] != null && Session["ExpandSID"].ToString().IndexOf("," + rs.GetInt32(9) + ",") >= 0)
                {
                    SqlDataReader rs2 = Db.rs("SELECT DepartmentID, Department, SortString FROM Department WHERE SponsorID = " + rs.GetInt32(9) + " ORDER BY SortString");
                    while (rs2.Read())
                    {
                        Survey.Text += "<tr style=\"" + (cx % 2 == 0 ? "background-color:#cccccc;" : "") + "\">" +
                            "<td>";
                        for (int i = 0; i < rs2.GetString(2).Length; i++)
                        {
                            Survey.Text += "&nbsp;";
                        }
                        Survey.Text += rs2.GetString(1) + "</td>" +
                            "<td><INPUT TYPE=\"radio\" NAME=\"Department" + rs2.GetInt32(0) + "\" VALUE=\"1\"" + (Request.Form["Department" + rs2.GetInt32(0)] != null && Request.Form["Department" + rs2.GetInt32(0)] == "1" ? " CHECKED" : "") + "/></td>" +
                            "<td><INPUT TYPE=\"radio\" NAME=\"Department" + rs2.GetInt32(0) + "\" VALUE=\"2\"" + (Request.Form["Department" + rs2.GetInt32(0)] != null && Request.Form["Department" + rs2.GetInt32(0)] == "2" ? " CHECKED" : "") + "/></td>" +
                            "<td><INPUT TYPE=\"radio\" NAME=\"Department" + rs2.GetInt32(0) + "\" VALUE=\"0\"" + (Request.Form["Department" + rs2.GetInt32(0)] == null || Request.Form["Department" + rs2.GetInt32(0)] == "0" ? " CHECKED" : "") + "/></td>" +
                            "</tr>";
                    }
                    rs2.Close();
                }
                cx++;
            }
            rs.Close();
        }
    }
}