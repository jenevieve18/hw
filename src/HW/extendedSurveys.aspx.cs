using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace HW
{
    public partial class extendedSurveys : System.Web.UI.Page
    {
        string treatmentOfferEmail = "", alternativeTreatmentOfferEmail = "", treatmentOfferLinks = "";
        int SESID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Db.checkAndLogin();

            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("inactivity.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1:
                    PageHeader.Text = "Enkäter";
                    Continue.Text = "Gå vidare till formulär";
                    Send.Text = "Skicka";
                    NameTxt.Text = "Namn";
                    EmailTxt.Text = "E-post";
                    PhoneTxt.Text = "Telefon";
                    IncludeTxt.Text = "Skicka med mina ifyllda enkäter och återkopplingar i förfrågan";
                    break;
                case 2:
                    PageHeader.Text = "Surveys";
                    Continue.Text = "Continue to forms";
                    Send.Text = "Send";
                    NameTxt.Text = "Name";
                    EmailTxt.Text = "Email";
                    PhoneTxt.Text = "Phone";
                    IncludeTxt.Text = "Attach my surveys and feedbacks to the request";
                    break;
            }

            System.Collections.Hashtable prevSES = new System.Collections.Hashtable();

            Survey.Text = "";

            Send.Click += new EventHandler(Send_Click);
            SqlDataReader rs = Db.rs("SELECT " +
                "ses.SponsorExtendedSurveyID, " +       // 0
                "ses.ProjectRoundID, " +                // 1
                "u.ProjectRoundUserID, " +              // 2
                "ses.PreviousProjectRoundID, " +        // 3
                "u.AnswerID, " +                        // 4
                "ses.IndividualFeedbackID, " +          // 5
                "s.TreatmentOffer, " +                  // 6
                "x.Email, " +                           // 7
                "ISNULL(sl.TreatmentOfferText, s.TreatmentOfferText), " +
                "ISNULL(sesd.TreatmentOfferEmail,s.TreatmentOfferEmail), " +             // 9
                "ISNULL(sl.TreatmentOfferIfNeededText, s.TreatmentOfferIfNeededText), " +
                "s.TreatmentOfferBQ, " +                // 11
                "s.TreatmentOfferBQfn, " +              // 12
                "s.TreatmentOfferBQmorethan, " +        // 13
                "a.FeedbackAlert, " +                   // 14
                "ISNULL(d.PreviewExtendedSurveys, si.PreviewExtendedSurveys), " +                   // 15
                "ISNULL(sl.AlternativeTreatmentOfferText, s.AlternativeTreatmentOfferText), " +     // 16
                "s.AlternativeTreatmentOfferEmail " +                                               // 17
                "FROM SponsorExtendedSurvey ses " +
                "INNER JOIN Sponsor s ON ses.SponsorID = s.SponsorID " +
                "INNER JOIN [User] x ON x.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                "LEFT OUTER JOIN UserSponsorExtendedSurvey u ON ses.SponsorExtendedSurveyID = u.SponsorExtendedSurveyID AND u.UserID = x.UserID " +
                "LEFT OUTER JOIN SponsorLang sl ON s.SponsorID = sl.SponsorID AND sl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "LEFT OUTER JOIN eForm..Answer a ON u.AnswerID = a.AnswerID " +

                "LEFT OUTER JOIN Department d ON x.DepartmentID = d.DepartmentID " +
                "LEFT OUTER JOIN SponsorInvite si ON si.UserID = x.UserID AND si.SponsorID = s.SponsorID " +
                "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID " +

                "WHERE sesd.Hide IS NULL AND ses.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + " " +
                "ORDER BY ses.SponsorExtendedSurveyID");
            while (rs.Read())
            {
                treatmentOfferEmail = (!rs.IsDBNull(9) ? rs.GetString(9) : "");
                alternativeTreatmentOfferEmail = (!rs.IsDBNull(17) ? rs.GetString(17) : "");

                //if (!rs.IsDBNull(5) && rs.GetInt32(5) == 1)         // IndividualFeedbackID
                //{
                if (!IsPostBack && !rs.IsDBNull(6) && SESID == 0)
                {
                    #region Treatment offer
                    bool qualify = false;
                    if (!rs.IsDBNull(8))
                    {
                        #region TreatmentOfferText
                        if (!rs.IsDBNull(11))
                        {
                            #region TreatmentOfferBQ
                            SqlDataReader rs2 = Db.rs("SELECT " +
                                "TOP 1 " +
                                "b.ValueDate " +
                                "FROM UserProfile u " +
                                "INNER JOIN UserProfileBQ b ON u.UserProfileID = b.UserProfileID " +
                                "WHERE b.BQID = " + rs.GetInt32(11) + " " +
                                "AND u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                                "ORDER BY u.UserProfileID DESC");
                            if (rs2.Read() && !rs2.IsDBNull(0))
                            {
                                if (!rs.IsDBNull(12) && rs.GetInt32(12) == 1)   // Age
                                {
                                    DateTime d = rs2.GetDateTime(0);
                                    int t = DateTime.Now.Year - d.Year;
                                    d = d.AddYears(t);
                                    if (DateTime.Now.CompareTo(d) < 0) { t--; }

                                    if (t > rs.GetInt32(13))
                                    {
                                        qualify = true;
                                        TreatmentOfferText.Text += "<INPUT ID=\"Offer1a\" ONCLICK=\"updateChecks();\" NAME=\"Offer\"" + (HttpContext.Current.Request.Form["Offer"] != null && HttpContext.Current.Request.Form["Offer"] == rs.GetString(8) ? " CHECKED" : "") + " TYPE=\"radio\" VALUE=\"a_" + rs.GetString(8) + "\"/>" + rs.GetString(8) + "<br/>";
                                        if (!rs.IsDBNull(16) && !rs.IsDBNull(17))
                                        {
                                            TreatmentOfferText.Text += "<INPUT ID=\"Offer1b\" ONCLICK=\"updateChecks();\" NAME=\"Offer\"" + (HttpContext.Current.Request.Form["Offer"] != null && HttpContext.Current.Request.Form["Offer"] == rs.GetString(16) ? " CHECKED" : "") + " TYPE=\"radio\" VALUE=\"b_" + rs.GetString(16) + "\"/>" + rs.GetString(16) + "<br/>";
                                        }
                                    }
                                }
                            }
                            rs2.Close();
                            #endregion
                        }
                        else
                        {
                            #region Always
                            qualify = true;
                            TreatmentOfferText.Text += "<INPUT ID=\"Offer1\" ONCLICK=\"updateChecks();\" NAME=\"Offer\"" + (HttpContext.Current.Request.Form["Offer"] != null && HttpContext.Current.Request.Form["Offer"] == rs.GetString(8) ? " CHECKED" : "") + " TYPE=\"radio\" VALUE=\"a_" + rs.GetString(8) + "\"/>" + rs.GetString(8) + "<br/>";
                            if (!rs.IsDBNull(16) && !rs.IsDBNull(17))
                            {
                                TreatmentOfferText.Text += "<INPUT ID=\"Offer1b\" ONCLICK=\"updateChecks();\" NAME=\"Offer\"" + (HttpContext.Current.Request.Form["Offer"] != null && HttpContext.Current.Request.Form["Offer"] == rs.GetString(16) ? " CHECKED" : "") + " TYPE=\"radio\" VALUE=\"b_" + rs.GetString(16) + "\"/>" + rs.GetString(16) + "<br/>";
                            }
                            #endregion
                        }
                        #endregion
                    }
                    if (!rs.IsDBNull(10) && !rs.IsDBNull(14))
                    {
                        #region TreatmentOfferIfNeededText
                        TreatmentOfferText.Text += "<INPUT ID=\"Offer2\" ONCLICK=\"updateChecks();\" NAME=\"Offer\"" + (HttpContext.Current.Request.Form["Offer"] != null && HttpContext.Current.Request.Form["Offer"] == rs.GetString(10) ? " CHECKED" : "") + " TYPE=\"radio\" VALUE=\"a_" + rs.GetString(10) + "\"/>" + rs.GetString(10) + "<br/>";
                        qualify = true;
                        #endregion
                    }
                    if (qualify)
                    {
                        TreatmentOfferText.Text = "<INPUT ID=\"Offer0\" ONCLICK=\"updateChecks();\" NAME=\"Offer\"" + (HttpContext.Current.Request.Form["Offer"] == null || HttpContext.Current.Request.Form["Offer"] == "Ingen kontakt önskas" ? " CHECKED" : "") + " TYPE=\"radio\" VALUE=\"Ingen kontakt önskas\"/>Ingen kontakt önskas<br/>" + TreatmentOfferText.Text;
                        if (HttpContext.Current.Session["TreatmentRequest"] != null)
                        {
                            TreatmentOffer.Visible = false;
                            Sent.Visible = true;
                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1:
                                    Sent.Text = "Förfrågan om kontakt skickad!";
                                    break;
                                case 2:
                                    Sent.Text = "Request for contact sent!";
                                    break;
                            }
                        }
                        else
                        {
                            TreatmentOffer.Visible = true;
                            Email.Text = rs.GetString(7);
                            //TreatmentOfferText.Text = rs.GetString(8);
                        }
                    }
                    #endregion
                }
                //}
                if (!rs.IsDBNull(2))
                {
                    prevSES.Add(rs.GetInt32(1), (rs.IsDBNull(4) ? 0 : rs.GetInt32(4)));

                    #region answered?
                    bool answered = !rs.IsDBNull(4);
                    if (!answered)
                    {
                        SqlDataReader rs2 = Db.rs("SELECT " +
                            "AnswerID " +
                            "FROM Answer " +
                            "WHERE ProjectRoundUserID = " + rs.GetInt32(2) + " " +
                            "AND EndDT IS NOT NULL", "eFormSqlConnection");
                        if (rs2.Read())
                        {
                            answered = true;
                            Db.exec("UPDATE UserSponsorExtendedSurvey SET " +
                                "AnswerID = " + rs2.GetInt32(0) + " " +
                                "WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                                "AND ProjectRoundUserID = " + rs.GetInt32(2));
                        }
                        rs2.Close();
                    }
                    #endregion

                    bool ongoing = false;
                    string name = "", link = "", feedback = "";

                    SqlDataReader rs3 = Db.rs("SELECT " +
                        "pr.Started, " +                                                            // 0
                        "pr.Closed, " +
                        "prl.InvitationSubject, " +
                        "prl.InvitationBody, " +
                        "LEFT(CONVERT(VARCHAR(255),pru.UserKey),8) AS UK, " +
                        "prl.SurveyName, " +                                                        // 5
                        "LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-',''),8)) AS SM, " +
                        "REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') AS AK, " +
                        "a.EndDT " +                                                                // 8
                        "FROM ProjectRound pr " +
                        "INNER JOIN ProjectRoundLang prl ON pr.ProjectRoundID = prl.ProjectRoundID AND prl.LangID = pr.LangID " +
                        "INNER JOIN ProjectRoundUser pru ON pru.ProjectRoundUserID = " + rs.GetInt32(2) + " " +
                        "LEFT OUTER JOIN Answer a ON a.ProjectRoundUserID = pru.ProjectRoundUserID " +
                        "WHERE pr.ProjectRoundID = " + rs.GetInt32(1), "eFormSqlConnection");
                    if (rs3.Read())
                    {
                        name = "&bull; " + (rs3.IsDBNull(8) ? rs3.GetDateTime(0) : rs3.GetDateTime(8)).ToString("MMM yyyy") + ", ";

                        if (!rs.IsDBNull(15) || !rs3.IsDBNull(0) && rs3.GetDateTime(0) < DateTime.Now)
                        {
                            if (rs3.IsDBNull(1) || rs3.GetDateTime(1) > DateTime.Now)
                            {
                                ongoing = true;
                            }
                        }

                        name += rs3.GetString(2);
                        string url = "https://eform.healthwatch.se/submit.aspx" +
                           "?Domain=healthwatch.se" +
                           "&LID=" + Convert.ToInt32(HttpContext.Current.Session["LID"]) +
                           "&RL=1" +
                           (!rs.IsDBNull(3) ? "&PPRID=" + rs.GetInt32(3) + (prevSES.Contains(rs.GetInt32(3)) ? "&PAID=" + prevSES[rs.GetInt32(3)].ToString() : "") : "") +
                           (answered ? "&AIOP=1&SM=" + rs3.GetString(6) : "") +
                           "&K=" + rs3.GetString(4) + rs.GetInt32(2).ToString();
                        link = "<A HREF=\"JavaScript:;\" ONCLICK=\"pop=window.open('" +
                           url + "" +
                           "','extSurvey','width=850,height=700,scrollbars=1,resizeable=1');\">";
                        if (answered)
                        {
                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1:
                                    link += "Visa enkäten";
                                    treatmentOfferLinks += "\r\nFormulär, " + name + "\r\n" + url;
                                    break;
                                case 2:
                                    link += "Show survey";
                                    treatmentOfferLinks += "\r\nForm, " + name + "\r\n" + url;
                                    break;
                            }
                        }
                        else if (ongoing && !rs3.IsDBNull(6))
                        {
                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1:
                                    link += "Fortsätt ifyllnad";
                                    break;
                                case 2:
                                    link += "Continue survey";
                                    break;
                            }
                        }
                        else if (ongoing)
                        {
                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1:
                                    link += "Öppna enkäten";
                                    break;
                                case 2:
                                    link += "Open survey";
                                    break;
                            }
                        }
                        link += "</A>";

                        if (!rs.IsDBNull(5) && answered)
                        {
                            string feedbackURL = "https://eform.healthwatch.se/downloadPDF.aspx?AK=" + rs3.GetString(7) + "";
                            switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                            {
                                case 1:
                                    feedback = "<A class=\"lnk\" HREF=\"" + feedbackURL + "\">Visa återkoppling</A>";
                                    treatmentOfferLinks += "\r\nÅterkoppling, " + name + "\r\n" + feedbackURL;
                                    break;
                                case 2:
                                    feedback = "<A class=\"lnk\" HREF=\"" + feedbackURL + "\">Show feedback</A>";
                                    treatmentOfferLinks += "\r\nFeedback, " + name + "\r\n" + feedbackURL;
                                    break;
                            }
                        }
                    }
                    rs3.Close();

                    if (answered || ongoing)
                    {
                        Survey.Text += "<TR><TD>" + name + "&nbsp;</TD><TD>" + link + "&nbsp;</TD><TD>" + feedback + "</TD></TR>";
                    }
                }
                SESID = rs.GetInt32(0);
            }
            rs.Close();
        }

        void Send_Click(object sender, EventArgs e)
        {
            try
            {
                string extra = "";
                SqlDataReader rs = Db.rs("SELECT BQ.Type, BQ.Internal, BA.Internal, upbq.ValueInt, upbq.ValueText, upbq.ValueDate " +
                    "FROM UserProfileBQ upbq " +
                    "INNER JOIN BQ ON upbq.BQID = BQ.BQID " +
                    "INNER JOIN [User] u ON upbq.UserProfileID = u.UserProfileID " +
                    "INNER JOIN SponsorBQ sbq ON upbq.BQID = sbq.BQID " +
                    "LEFT OUTER JOIN BA ON upbq.ValueInt = BA.BAID AND BA.BQID = BQ.BQID " +
                    "WHERE u.UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]) + " " +
                    "AND sbq.IncludeInTreatmentReq = 1 " +
                    "AND sbq.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
                while (rs.Read())
                {
                    switch (rs.GetInt32(0))
                    {
                        case 7:
                        case 1: extra += rs.GetString(1) + ": " + (rs.IsDBNull(2) ? "?" : rs.GetString(2)) + "\r\n"; break;
                        case 2: extra += rs.GetString(1) + ": " + (rs.IsDBNull(4) ? "?" : rs.GetString(4)) + "\r\n"; break;
                        case 3: extra += rs.GetString(1) + ": " + (rs.IsDBNull(5) ? "?" : rs.GetDateTime(5).ToString("yyyy-MM-dd")) + "\r\n"; break;
                        case 4: extra += rs.GetString(1) + ": " + (rs.IsDBNull(3) ? "?" : rs.GetInt32(3).ToString()) + "\r\n"; break;
                    }
                }
                rs.Close();

                string offerText = (HttpContext.Current.Request.Form["Offer"] != null ? HttpContext.Current.Request.Form["Offer"].ToString() : "a_Jag vill bli kontaktad");
                string offerEmail = treatmentOfferEmail;
                switch (offerText.Substring(0, 1))
                {
                    case "b": offerEmail = alternativeTreatmentOfferEmail; break;
                }

                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SmtpServer"]);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(
                    //(Db.isEmail(Email.Text) ? Email.Text : "support@healthwatch.se"),
                    "support@healthwatch.se",
                    (Db.isEmail(offerEmail) ? offerEmail : "support@healthwatch.se"),
                    offerText.Substring(2),
                    "" +
                    "Namn: " + Name.Text + "\r\n" +
                    "Email: " + Email.Text + "\r\n" +
                    "Telefon: " + Phone.Text + "\r\n" +
                    extra +
                    (Include.Checked ? treatmentOfferLinks.Replace("&bull;", "") + "\r\n" : "") +
                    "\r\n" +
                    "Sent through HealthWatch"
                    );
                if (Db.isEmail(Email.Text))
                {
                    mail.ReplyToList.Add(Email.Text);
                }
                //mail.Bcc.Add("jens@healthwatch.se");
                mailClient.Send(mail);
                Db.exec("UPDATE UserSponsorExtendedSurvey " +
                    "SET " +
                    "ContactRequest = '" + offerText.Replace("'", "''") + "', " +
                    "ContactRequestDT = GETDATE() " +
                    "WHERE SponsorExtendedSurveyID = " + SESID + " " +
                    "AND UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                HttpContext.Current.Session["TreatmentRequest"] = 1;
            }
            catch (Exception ex)
            {
                System.Net.Mail.SmtpClient mailClient = new System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings["SmtpServer"]);
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(
                    "support@healthwatch.se",
                    "jens@healthwatch.se",
                    "COULD NOT SEND TREATMENT REQUEST",
                    "FOR USERID: " + HttpContext.Current.Session["UserID"].ToString() +
                    "\r\n\r\n" + ex.Message +
                    "\r\n\r\n" + ex.StackTrace
                    );
                mailClient.Send(mail);
            }
            HttpContext.Current.Response.Redirect("extendedSurvey.aspx", true);
        }
    }
}