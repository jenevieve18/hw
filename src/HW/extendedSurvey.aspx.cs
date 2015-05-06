using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
    public partial class extendedSurvey : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Db.checkAndLogin();

            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("inactivity.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            SqlDataReader rs;

            /*if (HttpContext.Current.Request.QueryString["AK"] != null && HttpContext.Current.Request.QueryString["UK"] != null)
            {
                rs = Db.rs("SELECT a.AnswerID, u.ProjectRoundUserID FROM ProjectRoundUser pru INNER JOIN Answer a ON pru.ProjectRoundUserID = a.ProjectRoundUserID WHERE LEFT(CONVERT(VARCHAR(255),pru.UserKey),8) = '" + HttpContext.Current.Request.QueryString["UK"].ToString().Replace("'", "") + "' AND LEFT(CONVERT(VARCHAR(255),pru.UserKey),8) = '" + HttpContext.Current.Request.QueryString["UK"].ToString().Replace("'", "") + "'", "eFormSqlConnection");
                if (rs.Read())
                {
                    Db.exec("UPDATE UserSponsorExtendedSurvey SET AnswerID = " + rs.GetInt32(0) + " WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " AND ProjectRoundUserID = " + rs.GetInt32(1));
                }
                rs.Close();

                HttpContext.Current.Response.Redirect("/extendedSurvey.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }*/

            bool check = false, individualFeedback = false;
            Hashtable prevSES = new Hashtable();
            rs = Db.rs("SELECT " +
                        "ses.SponsorExtendedSurveyID, " +       // 0
                        "ses.ProjectRoundID, " +                // 1
                        "u.ProjectRoundUserID, " +              // 2
                        "ses.PreviousProjectRoundID, " +        // 3
                        "u.AnswerID, " +                        // 4
                        "ses.IndividualFeedbackID, " +          // 5
                        "ISNULL(d.PreviewExtendedSurveys, si.PreviewExtendedSurveys) " +    // 6
                        "FROM SponsorExtendedSurvey ses " +
                        "INNER JOIN [User] x ON x.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                        "LEFT OUTER JOIN UserSponsorExtendedSurvey u ON ses.SponsorExtendedSurveyID = u.SponsorExtendedSurveyID AND u.UserID = x.UserID " +

                        "LEFT OUTER JOIN Department d ON x.DepartmentID = d.DepartmentID " +
                        "LEFT OUTER JOIN SponsorInvite si ON si.UserID = x.UserID AND si.SponsorID = ses.SponsorID " +
                        "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +

                        "WHERE sesd.Hide IS NULL AND ses.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                        "ORDER BY ses.SponsorExtendedSurveyID");
            while (rs.Read() && !check)
            {
                individualFeedback = !rs.IsDBNull(5);

                if (!prevSES.Contains(rs.GetInt32(1)))
                {
                    prevSES.Add(rs.GetInt32(1), (rs.IsDBNull(4) ? 0 : rs.GetInt32(4)));
                }
                else
                {
                    Db.sendMail("support@healthwatch.se", "support@healthwatch.se", "SESID:" + rs.GetInt32(0) + ", PRID:" + rs.GetInt32(1), "Check extended survey for user ID " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                }

                if (rs.IsDBNull(4) && !rs.IsDBNull(2))
                {
                    SqlDataReader rs3 = Db.rs("SELECT " +
                        "pr.Started, " +
                        "pr.Closed, " +
                        "ISNULL(prl2.InvitationSubject,prl.InvitationSubject), " +
                        "ISNULL(prl2.InvitationBody,prl.InvitationBody), " +
                        "LEFT(CONVERT(VARCHAR(255),pru.UserKey),8), " +
                        "ISNULL(prl2.SurveyName,prl.SurveyName) " +
                        "FROM ProjectRound pr " +
                        "INNER JOIN ProjectRoundUser pru ON pru.ProjectRoundUserID = " + rs.GetInt32(2) + " " +
                        "LEFT OUTER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = pr.LangID " +
                        "LEFT OUTER JOIN ProjectRoundLang prl2 ON pr.ProjectRoundID = prl2.ProjectRoundID AND prl2.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "WHERE pr.ProjectRoundID = " + rs.GetInt32(1), "eFormSqlConnection");
                    if (rs3.Read())
                    {
                        if (!rs.IsDBNull(6) || !rs3.IsDBNull(0) && rs3.GetDateTime(0) < DateTime.Now)
                        {
                            if (rs3.IsDBNull(1) || rs3.GetDateTime(1) > DateTime.Now)
                            {
                                string cont = "";
                                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                                {
                                    case 1:
                                        cont = "Vidare till";
                                        break;
                                    case 2:
                                        cont = "Continue to";
                                        break;
                                }
                                mainHeader.InnerText = rs3.GetString(2);
                                string body = rs3.GetString(3).Replace("\r\n", "<BR>").Replace("\n\r", "<BR>").Replace("\n", "<BR>");
                                string link = "<A HREF=\"JavaScript:;\" ONCLICK=\"pop=window.open('" +
                                            System.Configuration.ConfigurationSettings.AppSettings["eFormURL"] +
                                            "/submit.aspx" +
                                            "?Domain=healthwatch.se" +
                                            "&LID=" + Convert.ToInt32(HttpContext.Current.Session["LID"]) +
                                            "&RL=1" +
                                            (!rs.IsDBNull(3) ? "&PPRID=" + rs.GetInt32(3) + (prevSES.Contains(rs.GetInt32(3)) ? "&PAID=" + prevSES[rs.GetInt32(3)].ToString() : "") : "") +
                                            "&K=" + rs3.GetString(4) + rs.GetInt32(2).ToString() + "" +
                                            "','extSurvey','width=850,height=700,scrollbars=1,resizeable=1');\">" + cont + " " + rs3.GetString(5) + "&nbsp;&raquo;</A>";
                                if (body.IndexOf("<LINK>") >= 0)
                                {
                                    body = body.Replace("<LINK>", link);
                                }
                                else
                                {
                                    body = body + "<BR><BR>" + link;
                                }
                                Survey.Text = body;
                                check = true;
                            }
                        }
                    }
                    rs3.Close();

                    if (check)
                    {
                        rs3 = Db.rs("SELECT AnswerID FROM Answer WHERE ProjectRoundUserID = " + rs.GetInt32(2) + " AND EndDT IS NOT NULL", "eFormSqlConnection");
                        if (rs3.Read())
                        {
                            Db.exec("UPDATE UserSponsorExtendedSurvey SET AnswerID = " + rs3.GetInt32(0) + " WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " AND ProjectRoundUserID = " + rs.GetInt32(2));
                            check = false;
                        }
                        rs3.Close();
                    }
                }
            }
            rs.Close();

            if (!check)
            {
                if (individualFeedback)
                {
                    HttpContext.Current.Response.Redirect("/extendedSurveys.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
                else if (Convert.ToInt32(HttpContext.Current.Session["NoReminderSet"]) == 1)
                {
                    HttpContext.Current.Response.Redirect("/reminder.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/submit.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
            }
        }
        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion
    }
}