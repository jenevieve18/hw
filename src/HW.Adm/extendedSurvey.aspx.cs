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
    public partial class extendedSurvey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            submitBtn.Click += new EventHandler(submit_Click);
            SurveyID.SelectedIndexChanged += new EventHandler(SurveyID_SelectedIndexChanged);

            ExtendedSurvey.Text = "<TR>" +
            "<TD><I>Database total</I></TD>" +
            "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"1\"" + (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "1" ? " CHECKED" : "") + "/></TD>" +
            "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"2\"" + (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "2" ? " CHECKED" : "") + "/></TD>" +
            "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"0\"" + (HttpContext.Current.Request.Form["Measure0"] == null || HttpContext.Current.Request.Form["Measure0"] == "0" ? " CHECKED" : "") + "/></TD>" +
            "</TR>";

            int cx = 0;
            SqlDataReader rs = Db.rs("SELECT " +
                    "s.Sponsor, " +
                    "ses.ProjectRoundID, " +
                    "ses.Internal, " +
                    "ses.RoundText, " +
                    "ss.SurveyID, " +
                    "ss.Internal, " +
                    "r.Internal " +
                    "FROM Sponsor s " +
                    "INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID " +
                    "INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID " +
                    "INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID " +
                    "ORDER BY s.Sponsor, ses.Internal, ses.RoundText");
            while (rs.Read())
            {
                if (!IsPostBack)
                {
                    if (SurveyID.Items.FindByValue(rs.GetInt32(4).ToString()) == null)
                    {
                        SurveyID.Items.Add(new ListItem(rs.GetString(5), rs.GetInt32(4).ToString()));
                        if (SurveyName.Text == "")
                        {
                            SurveyName.Text = rs.GetString(5);
                            if (SurveyName.Text.IndexOf(" ") >= 0)
                            {
                                SurveyName.Text = SurveyName.Text.Substring(0, SurveyName.Text.IndexOf(" "));
                            }
                        }
                    }
                }
                ExtendedSurvey.Text += "<TR" + (cx % 2 == 0 ? " style=\"background-color:#cccccc;\"" : "") + ">" +
                    "<TD>" + rs.GetString(0) + (rs.IsDBNull(2) ? ", " + rs.GetString(2) : "") + (!rs.IsDBNull(3) ? ", " + rs.GetString(3) : "") + (!rs.IsDBNull(6) ? ", " + rs.GetString(6) : "") + "</TD>" +
                    "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"1\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "1" ? " CHECKED" : "") + "/></TD>" +
                    "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"2\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "2" ? " CHECKED" : "") + "/></TD>" +
                    "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"0\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == null || HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "0" ? " CHECKED" : "") + "/></TD>" +
                    "</TR>";
                cx++;
            }
            rs.Close();

            if (!IsPostBack)
            {
                reloadQuestions();
            }
        }

        void SurveyID_SelectedIndexChanged(object sender, EventArgs e)
        {
            reloadQuestions();
        }

        private void reloadQuestions()
        {
            int LID = 1;

            Q.Items.Clear();

            SqlDataReader rs = Db.rs("SELECT " +
                "sq.QuestionID, " +						// 0
                "ql.Question, " +						// 1
                "ISNULL(NULLIF(ql.QuestionArea,''),'/.../') " +	// 2
                "FROM eform..SurveyQuestion sq " +
                "INNER JOIN eform..Question q ON sq.QuestionID = q.QuestionID " +
                "INNER JOIN eform..QuestionOption qo ON q.QuestionID = qo.QuestionID " +
                "INNER JOIN eform..[Option] o ON qo.OptionID = o.OptionID " +
                "INNER JOIN eform..QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + LID + " " +
                "WHERE o.OptionType IN (1,9) AND sq.SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue) + " " +
                "ORDER BY sq.SortOrder");
            while (rs.Read())
            {
                Q.Items.Add(new ListItem("[" + rs.GetString(2) + "] " + rs.GetString(1), rs.GetInt32(0).ToString()));
            }
            rs.Close();
        }

        void submit_Click(object sender, EventArgs e)
        {
            string qs1 = "", qs2 = "";

            if (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "1") { qs1 = ",0"; }
            if (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "2") { qs2 = ",0"; }

            SqlDataReader rs = Db.rs("SELECT " +
                     "s.Sponsor, " +
                     "ses.ProjectRoundID, " +
                     "ses.Internal, " +
                     "ses.RoundText, " +
                     "ss.SurveyID, " +
                     "ss.Internal " +
                     "FROM Sponsor s " +
                     "INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID " +
                     "INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID " +
                     "INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID " +
                     "ORDER BY s.Sponsor, ses.Internal, ses.RoundText");
            while (rs.Read())
            {
                if (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] == "1" && qs1 != ",0") { qs1 += "," + rs.GetInt32(1).ToString(); }
                if (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] == "2" && qs2 != ",0") { qs2 += "," + rs.GetInt32(1).ToString(); }
            }
            rs.Close();

            if (qs1 != "")
            {
                if (Q.SelectedValue != "")
                {
                    string q = "";
                    foreach (ListItem i in Q.Items)
                    {
                        if (i.Selected)
                        {
                            q += (q != "" ? "," : "") + i.Value;
                        }
                    }
                    ClientScript.RegisterStartupScript(this.GetType(), "POP", "<script type=\"text/javascript\">var pop=window.open('" + System.Configuration.ConfigurationManager.AppSettings["eFormURL"] + "/feedbackQuestion.aspx?Q=" + q + "&R1=" + MeasureTxt1.Text + "&R2=" + MeasureTxt2.Text + "&RNDS1=" + qs1.Substring(1) + (qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") + "&SID=" + SurveyID.SelectedValue + "&SN=" + SurveyName.Text + "','','');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "POP", "<script type=\"text/javascript\">var pop=window.open('" + System.Configuration.ConfigurationManager.AppSettings["eFormURL"] + "/feedback.aspx?R1=" + MeasureTxt1.Text + "&R2=" + MeasureTxt2.Text + "&RNDS1=" + qs1.Substring(1) + (qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") + "&SID=" + SurveyID.SelectedValue + "&SN=" + SurveyName.Text + "','','');</script>");
                }
            }
        }
    }
}