using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class extendedSurvey : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        submitBtn.Click += new EventHandler(submit_Click);
        submitTabBtn.Click += new EventHandler(submitTabBtn_Click);
        SurveyID.SelectedIndexChanged += new EventHandler(SurveyID_SelectedIndexChanged);

        ExtendedSurvey.Text = "" +
        "<TD COLSPAN=\"3\"><I>Database total</I></TD>" +
        "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"1\"" + (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "1" ? " CHECKED" : "") + "/></TD>" +
        "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"2\"" + (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "2" ? " CHECKED" : "") + "/></TD>" +
        "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"3\"" + (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "3" ? " CHECKED" : "") + "/></TD>" +
        "<TD><INPUT TYPE=\"radio\" NAME=\"Measure0\" VALUE=\"4\"" + (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "4" ? " CHECKED" : "") + "/></TD>" +
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
                "r.Internal, " +
                "(SELECT COUNT(*) FROM eform..Answer a WHERE a.EndDT IS NOT NULL AND a.ProjectRoundID = r.ProjectRoundID) AS CX, " +
                "ses.SponsorExtendedSurveyID " +
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
                "<TD>" + rs.GetInt32(8) + "&nbsp;</TD>" +
                "<TD>" + rs.GetInt32(1) + "&nbsp;</TD>" +
                "<TD>" + rs.GetString(0) + (rs.IsDBNull(2) ? ", " + rs.GetString(2) : "") + (!rs.IsDBNull(3) ? ", " + rs.GetString(3) : "") + (!rs.IsDBNull(6) ? ", " + rs.GetString(6) : "") + "</TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"1\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "1" ? " CHECKED" : "") + "/></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"2\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "2" ? " CHECKED" : "") + "/></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"3\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "3" ? " CHECKED" : "") + "/></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"4\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "4" ? " CHECKED" : "") + "/></TD>" +
                "<TD><INPUT TYPE=\"radio\" NAME=\"Measure" + rs.GetInt32(1) + "\" VALUE=\"0\"" + (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == null || HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1) + ""] == "0" ? " CHECKED" : "") + "/></TD>" +
                "<TD>" + rs.GetInt32(7) + "</TD>" +
                "</TR>";
            cx++;
        }
        rs.Close();

        if (!IsPostBack)
        {
            reloadQuestions();

            rs = Db.rs("SELECT LID, Language FROM LID");
            while(rs.Read())
            {
                LID.Items.Add(new ListItem(rs.GetString(1),rs.GetInt32(0).ToString()));
            }
            rs.Close();
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

    private void getQS(ref string qs1, ref string qs2, ref string qs3, ref string qs4, ref string q)
    {
        if (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "1") { qs1 = ",0"; }
        if (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "2") { qs2 = ",0"; }
        if (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "3") { qs3 = ",0"; }
        if (HttpContext.Current.Request.Form["Measure0"] != null && HttpContext.Current.Request.Form["Measure0"] == "4") { qs4 = ",0"; }

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
            if (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] == "3" && qs3 != ",0") { qs3 += "," + rs.GetInt32(1).ToString(); }
            if (HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] != null && HttpContext.Current.Request.Form["Measure" + rs.GetInt32(1)] == "4" && qs4 != ",0") { qs4 += "," + rs.GetInt32(1).ToString(); }
        }
        rs.Close();

        foreach (ListItem i in Q.Items)
        {
            if (i.Selected)
            {
                q += (q != "" ? "," : "") + i.Value;
            }
        }
    }

    void submit_Click(object sender, EventArgs e)
    {
       string qs1 = "", qs2 = "", qs3 = "", qs4 = "", q = "";

       getQS(ref qs1, ref qs2, ref qs3, ref qs4, ref q);

       if (qs1 != "")
       {
           SqlDataReader rs = Db.rs("SELECT COUNT(DISTINCT u.Email) FROM eform..Answer a INNER JOIN eform..ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID WHERE a.EndDT IS NOT NULL AND a.ProjectRoundID IN (0" + qs1 + ")");
           if (rs.Read())
           {
               M1CX.Text = rs.GetInt32(0).ToString();
           }
           rs.Close();
           if (qs2 != "")
           {
               rs = Db.rs("SELECT COUNT(DISTINCT u.Email) FROM eform..Answer a INNER JOIN eform..ProjectRoundUser u ON a.ProjectRoundUserID = u.ProjectRoundUserID WHERE a.EndDT IS NOT NULL AND a.ProjectRoundID IN (0" + qs2 + ")");
               if (rs.Read())
               {
                   M2CX.Text = rs.GetInt32(0).ToString();
               }
               rs.Close();
           }
           if (q != "")
           {
               ClientScript.RegisterStartupScript(this.GetType(), "POP", "<script type=\"text/javascript\">var pop=window.open('" + System.Configuration.ConfigurationManager.AppSettings["eFormURL"] + "/feedbackQuestion.aspx?LID=" + LID.SelectedValue + "&Q=" + q + "&R1=" + MeasureTxt1.Text + "&R2=" + MeasureTxt2.Text + "&RNDS1=" + qs1.Substring(1) + (qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") + "&SID=" + SurveyID.SelectedValue + "&SN=" + SurveyName.Text + "','','');</script>");
           }
           else
           {
               ClientScript.RegisterStartupScript(this.GetType(), "POP", "<script type=\"text/javascript\">var pop=window.open('" + System.Configuration.ConfigurationManager.AppSettings["eFormURL"] + "/feedback.aspx?LID=" + LID.SelectedValue + "&R1=" + MeasureTxt1.Text + "&R2=" + MeasureTxt2.Text + "&RNDS1=" + qs1.Substring(1) + (qs2 != "" ? "&RNDS2=" + qs2.Substring(1) : "") + "&SID=" + SurveyID.SelectedValue + "&SN=" + SurveyName.Text + "','','');</script>");
           }
       }
    }

    void submitTabBtn_Click(object sender, EventArgs e)
    {
        string qs1 = "", qs2 = "", qs3 = "", qs4 = "", q = "";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        getQS(ref qs1, ref qs2, ref qs3, ref qs4, ref q);

        Hashtable ht = new Hashtable();
        System.Text.StringBuilder sb1 = new System.Text.StringBuilder(), sb2 = new System.Text.StringBuilder(), sb3 = new System.Text.StringBuilder(), sb4 = new System.Text.StringBuilder();
        sb1.Append("\"" + "Measure" + "\",\"\""); sb2.Append("\"" + "Unit ID" + "\",\"\""); sb3.Append("\"" + "Unit name" + "\",\"\""); sb4.Append("\"" + "Response count" + "\",\"\""); 
        SqlDataReader rs = Db.rs("SELECT pru.Unit, pru.ProjectRoundID, pru.ID, (SELECT COUNT(*) FROM Answer a INNER JOIN ProjectRoundUnit p ON a.ProjectRoundUnitID = p.ProjectRoundUnitID WHERE a.EndDT IS NOT NULL AND LEFT(p.SortString,LEN(pru.SortString)) = pru.SortString) AS CX, pru.ProjectRoundUnitID FROM ProjectRoundUnit pru WHERE pru.ProjectRoundID IN (0" + qs1 + qs2 + qs3 + qs4 + ") ORDER BY pru.SortString", "eFormSqlConnection");
        while (rs.Read())
        {
            if ((qs1 + ",").IndexOf("," + rs.GetInt32(1).ToString() + ",") >= 0) { sb1.Append(",\"" + MeasureTxt1.Text + "\""); }
            if ((qs2 + ",").IndexOf("," + rs.GetInt32(1).ToString() + ",") >= 0) { sb1.Append(",\"" + MeasureTxt2.Text + "\""); }
            if ((qs3 + ",").IndexOf("," + rs.GetInt32(1).ToString() + ",") >= 0) { sb1.Append(",\"" + MeasureTxt3.Text + "\""); }
            if ((qs4 + ",").IndexOf("," + rs.GetInt32(1).ToString() + ",") >= 0) { sb1.Append(",\"" + MeasureTxt4.Text + "\""); }

            sb2.Append(",\"" + r(rs, 2) + "\"");
            sb3.Append(",\"" + r(rs, 0) + "\"");
            sb4.Append(",\"" + rs.GetInt32(3).ToString() + "\"");

            ht.Add(rs.GetInt32(4), rs.GetInt32(3));
        }
        rs.Close();

        sb.AppendLine(sb1.ToString());
        sb.AppendLine(sb2.ToString());
        sb.AppendLine(sb3.ToString());
        sb.AppendLine(sb4.ToString());

        string qidoid = "";
        rs = Db.rs("SELECT " +
            "q.QuestionID, " +
            "ISNULL(sqlang.Question,ql.Question), " +
            "qo.OptionID, " +
            "o.OptionType, " +
            "ocl.Text, " +
            "(SELECT COUNT(*) FROM [QuestionOption] oo WHERE oo.QuestionID = q.QuestionID) AS CX, " +
            "ocs.OptionComponentID " +
            "FROM SurveyQuestion sq " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
            "INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID " +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID AND qo.QuestionOptionID = sqo.QuestionOptionID " +
            "INNER JOIN OptionComponents ocs ON o.OptionID = ocs.OptionID " +
            "LEFT OUTER JOIN OptionComponentLang ocl ON ocs.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + Convert.ToInt32(LID.SelectedValue) + " " +
            "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = " + Convert.ToInt32(LID.SelectedValue) + " " +
            "LEFT OUTER JOIN SurveyQuestionLang sqlang ON sq.SurveyQuestionID = sqlang.SurveyQuestionID AND sqlang.LangID = " + Convert.ToInt32(LID.SelectedValue) + " " +
            "WHERE o.OptionType <> 2 AND sq.SurveyID = " + Convert.ToInt32(SurveyID.SelectedValue) + (q != "" ? " AND q.QuestionID IN (" + q + ")" : "") + " " +
            "ORDER BY sq.SortOrder, sqo.SortOrder, ocs.SortOrder", "eFormSqlConnection");
        while (rs.Read())
        {
            string key = rs.GetInt32(0).ToString() + rs.GetInt32(2).ToString();
            int type = rs.GetInt32(3); bool multitype = (type == 1 || type == 3), texttype = (type == 2 || type == 4);
            if (qidoid != key || multitype)
            {
                sb.Append("\"" + r(rs, 1) + "\",\"");
                if(multitype || texttype && rs.GetInt32(5) > 1)
                {
                    sb.Append(r(rs, 4));
                }
                sb.Append("\"");

                SqlDataReader rs3, rs2 = Db.rs("SELECT pru.SortString, pru.ProjectRoundUnitID FROM ProjectRoundUnit pru WHERE pru.ProjectRoundID IN (0" + qs1 + qs2 + qs3 + qs4 + ") ORDER BY pru.SortString", "eFormSqlConnection");
                while (rs2.Read())
                {
                    sb.Append(",\"");
                    switch (type)
                    {
                        case 1:
                        case 3:
                            rs3 = Db.rs("SELECT COUNT(*) FROM AnswerValue av INNER JOIN Answer a ON av.AnswerID = a.AnswerID INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID WHERE av.DeletedSessionID IS NULL AND a.EndDT IS NOT NULL AND LEFT(pru.SortString," + rs2.GetString(0).Length + ") = '" + rs2.GetString(0) + "' AND av.QuestionID = " + rs.GetInt32(0) + " AND av.OptionID = " + rs.GetInt32(2) + " AND av.ValueInt = " + rs.GetInt32(6), "eFormSqlConnection");
                            if (rs3.Read() && !rs3.IsDBNull(0))
                            {
                                sb.Append(Math.Round(Convert.ToDouble(rs3.GetInt32(0)) / Convert.ToDouble(ht[rs2.GetInt32(1)]) * 100d, 0).ToString("################0.##") + "%");
                            }
                            rs3.Close();
                            break;
                        case 4:
                            rs3 = Db.rs("SELECT AVG(av.ValueDecimal) FROM AnswerValue av INNER JOIN Answer a ON av.AnswerID = a.AnswerID INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID WHERE av.DeletedSessionID IS NULL AND a.EndDT IS NOT NULL AND LEFT(pru.SortString," + rs2.GetString(0).Length + ") = '" + rs2.GetString(0) + "' AND av.QuestionID = " + rs.GetInt32(0) + " AND av.OptionID = " + rs.GetInt32(2), "eFormSqlConnection");
                            if (rs3.Read() && !rs3.IsDBNull(0))
                            {
                                sb.Append(Math.Round(rs3.GetDecimal(0),2).ToString("################0.##"));
                            }
                            rs3.Close();
                            break;
                        case 9:
                            rs3 = Db.rs("SELECT AVG(av.ValueInt) FROM AnswerValue av INNER JOIN Answer a ON av.AnswerID = a.AnswerID INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID WHERE av.DeletedSessionID IS NULL AND a.EndDT IS NOT NULL AND LEFT(pru.SortString," + rs2.GetString(0).Length + ") = '" + rs2.GetString(0) + "' AND av.QuestionID = " + rs.GetInt32(0) + " AND av.OptionID = " + rs.GetInt32(2), "eFormSqlConnection");
                            if (rs3.Read() && !rs3.IsDBNull(0))
                            {
                                sb.Append(Math.Round(Convert.ToDouble(rs3.GetValue(0)),2).ToString("################0.##"));
                            }
                            rs3.Close();
                            break;
                    }
                    sb.Append("\"");
                }
                rs2.Close();

                sb.AppendLine();
            }
            qidoid = key;
        }
        rs.Close();

        HttpContext.Current.Response.Clear();

        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".csv");
        HttpContext.Current.Response.Write(sb.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    private string r(SqlDataReader rs, int i)
    {
        return stripBreaks(HttpUtility.HtmlDecode(RemoveHTMLTags(rs.GetString(i).Replace("<BR>"," ").Replace("<br>"," ").Replace("<BR/>"," ").Replace("<br/>"," ").Replace("-","")))).Trim().Replace("\"", "\"\"");
    }
    public static string RemoveHTMLTags(string sHtml)
    {
        const string REGEX_REMOVE_TAGS = @"(<[a-z]+[^>]*>)|(</[a-z\d]+>)";
        return Regex.Replace(System.Web.HttpUtility.HtmlDecode(sHtml), REGEX_REMOVE_TAGS, "", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
    }
    public static string stripBreaks(string s)
    {
        return s.Replace("\r\n", " ").Replace("\n\r", " ").Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");
    }
}
