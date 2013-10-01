using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

namespace HW.Adm
{
    public partial class export : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataReader rs;

            if (!IsPostBack)
            {
                rs = Db.rs("SELECT SponsorID, Sponsor FROM Sponsor ORDER BY SponsorID");
                while (rs.Read())
                {
                    SponsorID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();
                loadSurveys();
            }

            SponsorID.SelectedIndexChanged += new EventHandler(SponsorID_SelectedIndexChanged);
            Execute.Click += new EventHandler(Execute_Click);
        }

        void SponsorID_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSurveys();
        }

        void loadSurveys()
        {
            SurveyID.Items.Clear();

            SqlDataReader rs = Db.rs("SELECT ProjectRoundUnitID, Nav FROM SponsorProjectRoundUnit WHERE SponsorID = " + SponsorID.SelectedValue);
            while (rs.Read())
            {
                SurveyID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
            }
            rs.Close();
        }

        public static string RemoveHTMLTags(string sHtml)
        {
            const string REGEX_REMOVE_TAGS = @"(<[a-z]+[^>]*>)|(</[a-z\d]+>)";
            return Regex.Replace(sHtml, REGEX_REMOVE_TAGS, " ", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }
        public static string trunc255(string txt)
        {
            if (txt.Length > 255)
                return txt.Substring(0, 250) + "...";
            else
                return txt;
        }

        void Execute_Click(object sender, EventArgs e)
        {
            bool exportSpssData = true;
            bool exportData = false;

            StringBuilder output = new StringBuilder();
            string header = "", def = "", syn = "";
            bool first = true;
            int answerID = 0, userID = 0;

            string caseDelim = "\t";
            string rowDelim = "\r\n";

            if (exportSpssData)
            {
                caseDelim = "\r\n";
            }

            int caseCounter = 0;
            int userCaseCounter = 0;

            int tmpcx = 0;
            string SQL = "SELECT " +
                "sq.QuestionID, " +			// 0
                "qo.OptionID, " +			// 1
                "o.OptionType, " +			// 2
                "dbo.cf_isBlank(q.QuestionID,q.Variablename,sq.Variablename) AS s3, " +									// 3
                "dbo.cf_isBlank(qo.OptionID,o.Variablename,sqo.Variablename) AS s4, " +									// 4
                "pru.ID, " +				// 5
                "a.AnswerID, " +			// 6
                "a.AnswerID, " +			// 7
                "CAST(ISNULL(xql.Question,ql.Question) AS VARCHAR(8000)), " +											// 8
                "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +	// 9
                "ql.LangID, " +				// 10
                "usr.Extended, " +			// 11
                "a.StartDT, " +				// 12
                "a.EndDT, " +				// 13
                "usr.Extra, " +				// 14
                "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +							// 15
                "usr.ProjectRoundUserID, " +// 16
                "usr.NoSend, " +			// 17
                "usr.Terminated, " +		// 18
                "usr.SendCount, " +			// 19
                "usr.ReminderCount, " +		// 20
                "pru.ProjectRoundUnitID, " +// 21
                "pru.Terminated, " +		// 22
                "sq.SortOrder AS s1, " +	// 23
                "sqo.SortOrder AS s2, " +	// 24
                "pru.ProjectRoundID, " +	// 25
                "a.ExtendedFirst, " +		// 26
                "REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') " +	// 27
                "FROM ProjectRoundUnit pru " +
                "INNER JOIN SurveyQuestion sq ON sq.SurveyID = dbo.cf_unitSurveyID(pru.ProjectRoundUnitID) " +
                "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
                "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID AND ql.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
                "LEFT OUTER JOIN SurveyQuestionLang xql ON sq.SurveyQuestionID = xql.SurveyQuestionID AND xql.LangID = ql.LangID " +
                "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
                "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
                "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
                "INNER JOIN Answer a ON pru.ProjectRoundUnitID = a.ProjectRoundUnitID " +
                "INNER JOIN ProjectRoundUser usr ON usr.ProjectRoundUserID = a.ProjectRoundUserID " +
                "WHERE pru.ProjectRoundUnitID = " + SurveyID.SelectedValue + " " +
                "ORDER BY " +
                "usr.Extra, " +
                "usr.ProjectRoundUserID, " +
                "a.AnswerID, " +
                "s1, " +
                "s2, " +
                "s3, " +
                "s4" +
                "";

            #region User base
            SqlDataReader rs3, rs2, rs = Db.rs(SQL, "eFormSqlConnection");
            while (rs.Read())
            {
                if (userID == 0 || userID != rs.GetInt32(16))
                {
                    tmpcx++;
                    userCaseCounter = 0;
                    userID = rs.GetInt32(16);
                }
                if (answerID == 0 || answerID != rs.GetInt32(7))
                {
                    userCaseCounter++;
                    if (answerID == 0)
                    {
                        #region Header
                        answerID = rs.GetInt32(7);
                        header += "" +
                            "USER" + caseDelim +
                            "UNITCODE" + caseDelim +
                            "UNIT" + caseDelim +
                            "CASE" + caseDelim +
                            "START_DT" + caseDelim +
                            "END_DT" + caseDelim +
                            "SUBMITTED" + caseDelim +
                            "EXT" + caseDelim +
                            "ALT" + caseDelim +
                            "EXTRA" + caseDelim +
                            "SENDCOUNT" + caseDelim +
                            "REMINDERCOUNT" + caseDelim +
                            "NOSEND" + caseDelim +
                            "TERMINATED";

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUser)." + rowDelim;
                        syn += "VARIABLE LABELS sysUser 'Temporary user identification'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUcde)." + rowDelim;
                        syn += "VARIABLE LABELS sysUcde 'Unit code'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnit)." + rowDelim;
                        syn += "VARIABLE LABELS sysUnit 'Unit'." + rowDelim;
                        /*
                        syn += "VALUE LABELS sysUnit";
                        rs3 = Db.rs("SELECT ProjectRoundUnitID, Unit FROM ProjectRoundUnit WHERE ProjectRoundID = " + rs.GetInt32(25), "eFormSqlConnection");
                        while (rs3.Read())
                        {
                            syn += " " + rs3.GetInt32(0) + " '" + rs3.GetString(1) + "'";
                        }
                        rs3.Close();
                        syn += "." + rowDelim;
                        */

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
                        syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
                        syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
                        syn += "VARIABLE LABELS sysEnd 'End Date Time'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCmpl)." + rowDelim;
                        syn += "VARIABLE LABELS sysCmpl 'Survey completed'." + rowDelim;
                        syn += "VALUE LABELS sysCmpl 0 'No' 1 'Yes'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExt)." + rowDelim;
                        syn += "VARIABLE LABELS sysExt 'Extended survey'." + rowDelim;
                        syn += "VALUE LABELS sysExt 0 'No' 1 'Yes'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysAlt)." + rowDelim;
                        syn += "VARIABLE LABELS sysAlt 'Alternative survey'." + rowDelim;
                        syn += "VALUE LABELS sysAlt 0 'No' 1 'Yes'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(A255)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysExtra)." + rowDelim;
                        syn += "VARIABLE LABELS sysExtra 'Extra info'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysSndCt)." + rowDelim;
                        syn += "VARIABLE LABELS sysSndCt 'Invitation send count'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysRemCt)." + rowDelim;
                        syn += "VARIABLE LABELS sysRemCt 'Reminder send count'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysNoSnd)." + rowDelim;
                        syn += "VARIABLE LABELS sysNoSnd 'Unsubscribed for further reminders'." + rowDelim;
                        syn += "VALUE LABELS sysNoSnd 0 'No' 1 'Yes'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserTerm)." + rowDelim;
                        syn += "VARIABLE LABELS sysUserTerm 'User terminated/withdrawn'." + rowDelim;
                        syn += "VALUE LABELS sysUserTerm 0 'No' 1 'Yes'." + rowDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUnitTerm)." + rowDelim;
                        syn += "VARIABLE LABELS sysUnitTerm 'Unit terminated/withdrawn'." + rowDelim;
                        syn += "VALUE LABELS sysUnitTerm 0 'No' 1 'Yes'." + rowDelim;
                        #endregion

                        header += "" +
                            "hwUNIT" + caseDelim;

                        def += "/" + (++caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                        syn += "RENAME VARIABLES (V" + (caseCounter) + "=hwUnit)." + rowDelim;
                        syn += "VARIABLE LABELS hwUnit 'Unit'." + rowDelim;
                        syn += "VALUE LABELS hwUnit";
                        rs2 = Db.rs("SELECT DepartmentID, Department FROM Department WHERE SponsorID = " + SponsorID.SelectedValue);
                        while (rs2.Read())
                        {
                            syn += " " + rs2.GetInt32(0) + " '" + rs2.GetString(1) + "'";
                        }
                        rs2.Close();
                        syn += "." + rowDelim;

                        rs2 = Db.rs("SELECT " +
                            "b.BQID, " +
                            "b.Internal, " +
                            "b.Type, " +
                            "ISNULL(b.Variable,'BQ'+CAST(b.BQID AS VARCHAR(4))), " +
                            "(SELECT COUNT(*) FROM [BA] a WHERE a.BQID = b.BQID) " +	// 4
                            "FROM [BQ] b " +
                            "INNER JOIN SponsorBQ sbq ON b.BQID = sbq.BQID AND sbq.SponsorID = " + SponsorID.SelectedValue + " " +
                            "ORDER BY sbq.SortOrder");
                        while (rs2.Read())
                        {
                            string var = "hw" + rs2.GetString(3);
                            header += caseDelim + var;

                            syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
                            syn += "VARIABLE LABELS " + var + " '" + trunc255(RemoveHTMLTags(rs2.GetString(1))) + "'." + rowDelim;

                            #region Value labels
                            switch (rs2.GetInt32(2))
                            {
                                case 0:
                                    syn += "VALUE LABELS " + var + (rs2.GetInt32(2) == 9 && rs2.GetInt32(4) > 2 ? "Zone" : "");
                                    int cx = 0;
                                    rs3 = Db.rs("SELECT " +
                                        "BA.Internal, " +
                                        "BA.BAID " +
                                        "FROM BA WHERE BA.BQID = " + rs2.GetInt32(0) + " " +
                                        "ORDER BY BA.SortOrder");
                                    while (rs3.Read())
                                    {
                                        syn += " " + (rs2.GetInt32(2) == 9 && rs2.GetInt32(4) > 2 ? (cx++) : rs3.GetInt32(1)) + " '" + trunc255(RemoveHTMLTags(rs3.GetString(0))) + "'";
                                    }
                                    rs3.Close();
                                    syn += "." + rowDelim;
                                    break;
                                case 1:
                                    def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                                    goto case 0;
                                case 2:
                                    def += "/" + (caseCounter) + " V" + (caseCounter) +
                                        "(A" +
                                        (Math.Min(250, Db.getInt32("SELECT MAX(LEN(CAST(Internal AS VARCHAR(8000)))) FROM BA WHERE BQID = " + rs2.GetInt32(0)) + 16)) +
                                        ")" + rowDelim;
                                    break;
                                case 3:
                                    def += "/" + (caseCounter) + " V" + (caseCounter) + "(A16)" + rowDelim;
                                    break;
                                case 4:
                                    def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                                    break;
                                case 7:
                                    goto case 1;
                                case 9:
                                    if (rs2.GetInt32(4) > 2)
                                    {
                                        def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;

                                        syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
                                        syn += "VARIABLE LABELS " + var + "Zone '" + rs2.GetString(1) + "'." + rowDelim;
                                    }
                                    def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                                    goto case 0;
                            }
                            #endregion
                        }
                        rs2.Close();
                    }
                    else if (answerID != rs.GetInt32(7))
                    {
                        first = false;
                        output.Append(rowDelim);
                    }
                    answerID = rs.GetInt32(7);
                    output.Append(tmpcx + caseDelim +
                        rs.GetString(5) + caseDelim +
                        rs.GetInt32(21) + caseDelim +
                        userCaseCounter + caseDelim +
                        (rs.IsDBNull(12) ? "" : rs.GetDateTime(12).ToString("yyyy-MM-dd HH:mm")) + caseDelim +
                        (rs.IsDBNull(13) ? "" : rs.GetDateTime(13).ToString("yyyy-MM-dd HH:mm")) + caseDelim +
                        (rs.IsDBNull(13) ? 0 : 1) + caseDelim +
                        (rs.IsDBNull(26) ? 0 : rs.GetInt32(26)) + caseDelim +
                        (rs.IsDBNull(11) ? 0 : rs.GetInt32(11)) + caseDelim +
                        (rs.IsDBNull(14) ? "" : rs.GetString(14)) + caseDelim +
                        (rs.IsDBNull(19) ? 0 : rs.GetInt32(19)) + caseDelim +
                        (rs.IsDBNull(20) ? 0 : rs.GetInt32(20)) + caseDelim +
                        (rs.IsDBNull(17) ? 0 : rs.GetInt32(17)) + caseDelim +
                        (rs.IsDBNull(18) ? 0 : rs.GetInt32(18)) + caseDelim +
                        (rs.IsDBNull(22) ? 0 : 1));

                    #region HW BQ
                    string SQL3 = "SELECT " +
                            "ISNULL(d.DepartmentID,0), " +	// 0
                            "ISNULL(d.Department,''), " +	// 1
                            "p.ValueInt, " +				// 2
                            "p.ValueText, " +				// 3
                            "p.ValueDate, " +				// 4
                            "ba.Internal, " +				// 5
                            "BQ.Type, " +					// 6
                            "(SELECT COUNT(*) FROM BA x WHERE x.BQID = b.BQID) " +	// 7
                            "FROM Sponsor s " +
                            "INNER JOIN SponsorBQ b ON s.SponsorID = b.SponsorID " +
                            "INNER JOIN BQ ON BQ.BQID = b.BQID " +
                            "LEFT OUTER JOIN UserProjectRoundUserAnswer a ON a.ProjectRoundUserID = " + rs.GetInt32(16) + " AND a.AnswerKey = '" + rs.GetString(27) + "' " +
                            "LEFT OUTER JOIN UserProfile u ON a.UserProfileID = u.UserProfileID " +
                            "LEFT OUTER JOIN Department d ON u.DepartmentID = d.DepartmentID AND d.SponsorID = s.SponsorID " +
                            "LEFT OUTER JOIN UserProfileBQ p ON b.BQID = p.BQID AND p.UserProfileID = u.UserProfileID " +
                            "LEFT OUTER JOIN BA ON p.ValueInt = BA.BAID AND BA.BQID = b.BQID " +
                            "WHERE s.SponsorID = " + SponsorID.SelectedValue + " " +
                            "ORDER BY b.SortOrder";
                    rs2 = Db.rs(SQL3);
                    if (rs2.Read())
                    {
                        output.Append(caseDelim + rs2.GetInt32(0).ToString());
                        do
                        {
                            output.Append(caseDelim);

                            switch (rs2.GetInt32(6))
                            {
                                case 1:
                                    output.Append((rs2.IsDBNull(2) ? "" : rs2.GetInt32(2).ToString()));
                                    break;
                                case 2:
                                    output.Append((rs2.IsDBNull(3) ? "" : rs2.GetString(3).Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ")));
                                    break;
                                case 3:
                                    output.Append((rs2.IsDBNull(4) ? "" : rs2.GetDateTime(4).ToString("yyyy-MM-dd")));
                                    break;
                                case 4:
                                    goto case 1;
                                case 7:
                                    goto case 1;
                                case 9:
                                    output.Append((rs2.IsDBNull(2) ? "" : rs2.GetInt32(2).ToString()));
                                    if (rs2.GetInt32(7) > 2)
                                    {
                                        output.Append(caseDelim);

                                        if (!rs2.IsDBNull(2))
                                        {
                                            int cx = 0;
                                            for (int i = 0; i < rs2.GetInt32(7); i++)
                                            {
                                                if (rs2.GetInt32(2) >= 100 / rs2.GetInt32(7) * i)
                                                {
                                                    cx = i;
                                                }
                                            }
                                            output.Append(cx);
                                        }
                                    }
                                    break;
                            }
                        }
                        while (rs2.Read());
                    }
                    rs2.Close();
                    #endregion
                }
                if (first)
                {
                    #region Header
                    string var = rs.GetString(3);
                    if (rs.GetInt32(9) > 1)
                    {
                        var += rs.GetString(4);
                    }

                    header += caseDelim + var;

                    syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
                    syn += "VARIABLE LABELS " + var + " '" + trunc255(RemoveHTMLTags(rs.GetString(8))) + "'." + rowDelim;

                    #region Value labels
                    switch (rs.GetInt32(2))
                    {
                        case 0:
                            syn += "VALUE LABELS " + var + (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2 ? "Zone" : "");
                            int cx = 0;
                            rs3 = Db.rs("SELECT " +
                                "ocl.Text, " +
                                "oc.ExportValue " +
                                "FROM OptionComponents oc " +
                                "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rs.GetInt32(10) + " " +
                                "WHERE oc.OptionID = " + rs.GetInt32(1) + " " +
                                "ORDER BY oc.SortOrder", "eFormSqlConnection");
                            while (rs3.Read())
                            {
                                syn += " " + (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2 ? (cx++) : rs3.GetInt32(1)) + " '" + trunc255(RemoveHTMLTags(rs3.GetString(0))) + "'";
                            }
                            rs3.Close();
                            syn += "." + rowDelim;
                            break;
                        case 1:
                            def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                            goto case 0;
                        case 2:
                            def += "/" + (caseCounter) + " V" + (caseCounter) +
                                "(A" +
                                (Math.Min(255, Db.getInt32("SELECT MAX(LEN(CAST(ValueText AS VARCHAR(8000)))) FROM AnswerValue WHERE QuestionID = " + rs.GetInt32(0) + " AND OptionID = " + rs.GetInt32(1)) + 16)) +
                                ")" + rowDelim;
                            break;
                        case 3:
                            goto case 1;
                        case 4: // should be decimal
                            def += "/" + (caseCounter) + " V" + (caseCounter) + "(F6.0)" + rowDelim;
                            break;
                        case 9:
                            if (rs.GetInt32(15) > 2)
                            {
                                def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;

                                syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
                                syn += "VARIABLE LABELS " + var + "Zone '" + rs.GetString(8) + "'." + rowDelim;
                            }
                            def += "/" + (caseCounter) + " V" + (caseCounter) + "(F3.0)" + rowDelim;
                            goto case 0;
                    }
                    #endregion
                    #endregion
                }
                output.Append(caseDelim);

                string SQL2 = "SELECT " +
                    "SUBSTRING(av.ValueText,1,250), " +		// 0
                    "op.ExportValue, " +	// 1
                    "av.ValueDecimal, " +	// 2
                    "av.ValueInt " +		// 3
                    "FROM AnswerValue av " +
                    "LEFT OUTER JOIN OptionComponents op ON av.ValueInt = op.OptionComponentID AND op.OptionID = av.OptionID " +
                    "WHERE av.AnswerID = " + rs.GetInt32(7) + " " +
                    "AND av.QuestionID = " + rs.GetInt32(0) + " " +
                    "AND av.OptionID = " + rs.GetInt32(1) + " " +
                    "AND av.DeletedSessionID IS NULL";

                rs2 = Db.rs(SQL2, "eFormSqlConnection");
                if (rs2.Read())
                {
                    try
                    {
                        switch (rs.GetInt32(2))
                        {
                            case 1:
                                output.Append((rs2.IsDBNull(1) ? "" : rs2.GetInt32(1).ToString()));
                                break;
                            case 2:
                                output.Append((rs2.IsDBNull(0) ? "" : rs2.GetString(0).Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ")));
                                break;
                            case 3:
                                goto case 1;
                            case 4:
                                output.Append((rs2.IsDBNull(2) ? "" : ((float)rs2.GetDecimal(2)).ToString().Replace(".", ",")));
                                break;
                            case 9:
                                output.Append((rs2.IsDBNull(3) ? "" : rs2.GetInt32(3).ToString()));
                                if (rs.GetInt32(15) > 2)
                                {
                                    output.Append(caseDelim);

                                    if (!rs2.IsDBNull(3))
                                    {
                                        int cx = 0;
                                        for (int i = 0; i < rs.GetInt32(15); i++)
                                        {
                                            if (rs2.GetInt32(3) >= 100 / rs.GetInt32(15) * i)
                                            {
                                                cx = i;
                                            }
                                        }
                                        output.Append(cx);
                                    }
                                }
                                break;
                        }
                    }
                    catch (Exception) { }
                }
                else if (rs.GetInt32(2) == 9 && rs.GetInt32(15) > 2)
                {
                    output.Append(caseDelim);
                }
                rs2.Close();
            }
            rs.Close();
            #endregion

            string ret = "";
            if (exportData)
            {
                ret = header + rowDelim + output.ToString() + rowDelim;
            }
            else
            {
                ret = "DATA LIST FIXED RECORDS=" + caseCounter + rowDelim + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + rowDelim + "END DATA." + rowDelim + syn + rowDelim;
            }
            try
            {
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("tmp")))
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("tmp"));
                System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("tmp/" + DateTime.Now.Ticks + ".sps"), System.IO.FileMode.Create);
                System.IO.StreamWriter f = new System.IO.StreamWriter(fs, System.Text.Encoding.Default);
                f.Write(ret);
                f.Close();
                fs.Close();
            }
            catch (Exception) { }
            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            if (exportData)
            {
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
            }
            else
            {
                HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
            }
            HttpContext.Current.Response.Write(ret);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}