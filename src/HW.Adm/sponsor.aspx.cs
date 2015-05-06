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

public partial class sponsor : System.Web.UI.Page
{
    public static string RemoveHTMLTags(string sHtml)
    {
        const string REGEX_REMOVE_TAGS = @"(<[a-z]+[^>]*>)|(</[a-z\d]+>)";
        return System.Text.RegularExpressions.Regex.Replace(sHtml, REGEX_REMOVE_TAGS, " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
    }
    public static string trunc(string txt, int len)
    {
        txt = txt.Replace("\t", " ").Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");
        if (txt.Length > len)
            return txt.Substring(0, len - 3) + "...";
        else
            return txt;
    }

    public static void execExportRepeated(string sponsorIDs, int rExportSurveyID, int rExportLangID)
    {
        execExportRepeated(sponsorIDs, rExportSurveyID, rExportLangID, false);
    }
    public static void execExportRepeated(string sponsorIDs, int rExportSurveyID, int rExportLangID, bool full)
    {
		string tempSponsorIDs = "";
		foreach (string s in sponsorIDs.Split(','))
		{
			if (s != "")
			{
				tempSponsorIDs += "," + Convert.ToInt32(s) + ",-" + Convert.ToInt32(s);
			}
		}
		sponsorIDs = tempSponsorIDs;

        #region EXPORT
        #region init
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string def = "", syn = "";
        bool nextShouldBreak = true;
        int caseCounter = 0, recordCount = 0, ax = 0;
        string rowDelim = "\n";
        #endregion
        #region SPSS header
        nextShouldBreak = true;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
        syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
        
        SqlDataReader rs3;
        if (full)
        {
            nextShouldBreak = true;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(A250)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEmail)." + rowDelim;
            syn += "VARIABLE LABELS sysEmail 'Email'." + rowDelim;
            nextShouldBreak = true;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F2.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserStatus)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserStatus 'User status'." + rowDelim;
            //syn += "VALUE LABELS sysUserStatus";
            //syn += " 0 'Active'";
            //syn += " 1 'Stopped, work related'";
            //syn += " 2 'Stopped, education leave'";
            //syn += " 14 'Stopped, parental leave'";
            //syn += " 24 'Stopped, sick leave'";
            //syn += " 34 'Stopped, do not want to participate'";
            //syn += " 44 'Stopped, no longer associated'";
            //syn += " 4 'Stopped, other reason'";
            //syn += " 5 'Stopped, unknown reason'";
            //syn += " 6 'Stopped, project completed'";
            //syn += "." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(DATETIME17.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserStatusDate)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserStatusDate 'User Status Date Time'." + rowDelim;
            //nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(DATETIME17.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysConsent)." + rowDelim;
            syn += "VARIABLE LABELS sysConsent 'Consent'." + rowDelim;
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F1.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysConsentYN)." + rowDelim;
            syn += "VARIABLE LABELS sysConsentYN 'Consent'." + rowDelim;
            syn += "VALUE LABELS sysConsentYN";
            syn += " 0 'No'";
            syn += " 1 'Yes'";
            syn += "." + rowDelim;
            nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserLogins)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserLogins 'User login count'." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserExercises)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserExercises 'User exercise count'." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserMinutes)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserMinutes 'User minutes spent'." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserHours)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserHours 'User hours spent'." + rowDelim;
            //nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F4.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysOrg)." + rowDelim;
            syn += "VARIABLE LABELS sysOrg 'Organisation'." + rowDelim;
            syn += "VALUE LABELS sysOrg";
            rs3 = Db.rs("SELECT SponsorID, Sponsor FROM Sponsor WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDept)." + rowDelim;
            syn += "VARIABLE LABELS sysDept 'Department'." + rowDelim;
            syn += "VALUE LABELS sysDept";
            rs3 = Db.rs("SELECT DepartmentID, Department FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptTree)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptTree 'Department tree'." + rowDelim;
            syn += "VALUE LABELS sysDeptTree";
            rs3 = Db.rs("SELECT DepartmentID, dbo.cf_DepartmentTree(DepartmentID,' > ') FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptShort)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptShort 'Department short name'." + rowDelim;
            syn += "VALUE LABELS sysDeptShort";
            rs3 = Db.rs("SELECT DepartmentID, DepartmentShort FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptShortTree)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptShortTree 'Department short name tree'." + rowDelim;
            syn += "VALUE LABELS sysDeptShortTree";
            rs3 = Db.rs("SELECT DepartmentID, dbo.cf_DepartmentShortTree(DepartmentID,' > ') FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
        }
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
        syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;

        if (full)
        {
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(DATETIME17.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
            syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(DATETIME17.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
            syn += "VARIABLE LABELS sysEnd 'Date Time'." + rowDelim;
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysSponsorMonth)." + rowDelim;
            syn += "VARIABLE LABELS sysSponsorMonth 'Sponsor Month'." + rowDelim;
        }
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F6.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysYearMonth)." + rowDelim;
        syn += "VARIABLE LABELS sysYearMonth 'Year Month'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F6.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysYearWeek)." + rowDelim;
        syn += "VARIABLE LABELS sysYearWeek 'Year Week'." + rowDelim;
        nextShouldBreak = true;
        #endregion
        #region header and queries
        System.Text.StringBuilder querySelect = new System.Text.StringBuilder();
        System.Text.StringBuilder queryJoin = new System.Text.StringBuilder();
        string varTypes = "", varAttrs = "", varPositions = "", varBreaks = "";

        int varCount = 0, queries = 1, varPerRecord = 0, colsPerRecord = 0, varPos = 0, queryDivide = 30;
        string SQL = "SELECT " +
            "sq.QuestionID, " +			// 0
            "qo.OptionID, " +			// 1
            "o.OptionType, " +			// 2
            "q.Variablename, " +		// 3
            "o.Variablename, " +		// 4
            "ql.Question, " +			// 5
            "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +	// 6
            "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +							// 7
            "sq.SortOrder, " +			// 8
            "sqo.SortOrder " +			// 9
            "FROM SurveyQuestion sq " +
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
            "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID " +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
            "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID " +
            "WHERE sq.SurveyID = " + rExportSurveyID + " " +
            "AND ql.LangID = " + rExportLangID + " " +
            "ORDER BY sq.SortOrder, sqo.SortOrder";
        rs3 = Db.rs(SQL, "eFormSqlConnection");
        while (rs3.Read())
        {
            #region Header
            string var = (rs3.IsDBNull(3) || rs3.GetString(3).Trim() == "" ? "Q" + rs3.GetInt32(0).ToString() : rs3.GetString(3));
            string desc = (rs3.IsDBNull(5) || rs3.GetString(5).Trim() == "" ? var : rs3.GetString(5));
            if (rs3.GetInt32(6) > 1)
            {
                var += (rs3.IsDBNull(4) || rs3.GetString(4).Trim() == "" ? "A" + rs3.GetInt32(1).ToString() : rs3.GetString(4));
                desc += var;
            }
            desc = trunc(RemoveHTMLTags(desc), 230);
            var = var.Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"), "_").Replace(" ", "_").Replace("-", "_").Replace("/", "_");

            syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
            syn += "VARIABLE LABELS " + var + " '" + desc + "'." + rowDelim;

            string defDelim = " ";
            if (varPerRecord > 15 || colsPerRecord > 235 || rs3.GetInt32(2) == 2)
            {
                recordCount++;
                varPerRecord = 0;
                colsPerRecord = 0;
                defDelim = rowDelim + "/" + recordCount + " ";
                varBreaks += (varBreaks != "" ? "," : "") + "1";
            }
            else
            {
                varBreaks += (varBreaks != "" ? "," : "") + "0";
            }

            switch (rs3.GetInt32(2))
            {
                case 0:
                    bool zone = rs3.GetInt32(2) == 9 && rs3.GetInt32(7) > 2;
                    syn += "VALUE LABELS " + var + (zone ? "Zone" : "");
                    int cx = 0;
                    ax = 0;
                    SqlDataReader rs4 = Db.rs("SELECT " +
                        "ocl.Text, " +
                        "oc.ExportValue " +
                        "FROM OptionComponents oc " +
                        "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rExportLangID + " " +
                        "WHERE oc.OptionID = " + rs3.GetInt32(1) + " " +
                        "ORDER BY oc.SortOrder", "eFormSqlConnection");
                    while (rs4.Read())
                    {
                        string s = trunc(RemoveHTMLTags(HttpContext.Current.Server.HtmlDecode(rs4.GetString(0))), 110);
                        ax += s.Length;
                        if (ax > 110)
                        {
                            ax = 0;
                            syn += rowDelim;
                        }
                        syn += " " + (zone ? (cx++) : rs4.GetInt32(1)) + " '" + s + "'";
                    }
                    rs4.Close();
                    syn += "." + rowDelim;
                    break;
                case 1:
                    varPos = 3;
                    colsPerRecord += varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + varPos;
                    def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";
                    goto case 0;
                case 2:
                    varPos = (Math.Min(250, Db.getInt32("SELECT MAX(LEN(CAST(ValueText AS VARCHAR(255)))) FROM AnswerValue WHERE QuestionID = " + rs3.GetInt32(0) + " AND OptionID = " + rs3.GetInt32(1), "eFormSqlConnection") + 16));
                    colsPerRecord += 250;//varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + 250;//varPos;
                    def += defDelim + "V" + (caseCounter) + "(A" + varPos + ")";
                    break;
                case 3:
                    goto case 1;
                case 4:
                    varPos = 8;
                    colsPerRecord += varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + varPos;
                    def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".2)";
                    break;
                case 9:
                    varPos = 3;
                    colsPerRecord += varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + varPos;

                    if (rs3.GetInt32(7) > 2)
                    {
                        def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";

                        defDelim = " ";
                        colsPerRecord += varPos;
                        varPerRecord++;

                        syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
                        syn += "VARIABLE LABELS " + var + "Zone '" + desc + "'." + rowDelim;
                    }

                    def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";
                    goto case 0;
            }
            #endregion

            if (varCount != 0 && varCount % queryDivide == 0)
            {
                querySelect.Append("¤");
                queryJoin.Append("¤");
                queries++;
            }
            varCount++;
            switch (rs3.GetInt32(2))
            {
                case 0:
                    #region Link
                    querySelect.Append(",op" + varCount + ".ExportValue AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL " +
                        "LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
                        "op" + varCount + ".OptionID = av" + varCount + ".OptionID " +
                        "AND " +
                        "op" + varCount + ".OptionComponentID = av" + varCount + ".ValueInt ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "1";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
                case 1:
                    goto case 0;
                case 2:
                    #region Freetext
                    querySelect.Append(",av" + varCount + ".ValueText AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "2";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
                case 3:
                    goto case 0;
                case 4:
                    #region Decimal
                    querySelect.Append(",av" + varCount + ".ValueDecimal AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "4";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
                case 9:
                    #region VAS
                    querySelect.Append(",av" + varCount + ".ValueInt AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "9";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
            }
        }
        rs3.Close();
        string[] varType = varTypes.Split(','), varAttr = varAttrs.Split(','), varPosition = varPositions.Split(','), varBreak = varBreaks.Split(',');
        #endregion

        ArrayList queryResult = new ArrayList();
        int caseCount = 0;
        for (int q = 0; q < queries; q++)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder();

			int userID = 0, userCaseCounter = 0; ArrayList al = new ArrayList();
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
            #region User base
            SQL = "SELECT " +
                "u.UserID, " +              // 0
                "ABS(si.SponsorID), " +
				"ABS(si.DepartmentID), " +
                "a.StartDT, " +
                "a.EndDT, " +
				"(SELECT MIN(six.Sent) FROM healthwatch..SponsorInvite six WHERE six.SponsorID = ABS(si.SponsorID)), " + // 5
                "NULL, " +
                "si.StoppedReason, " +
                "si.Stopped, " +
                "si.Consent, " +
                "si.Email, " +           // 10
				"s.AnswerID " +
                querySelect.ToString().Split('¤')[q] +
                "FROM healthwatch..[User] u " +
				"INNER JOIN healthwatch..SponsorInvite si ON (u.UserID = ABS(si.UserID) OR si.Email = u.Email + 'DELETED' AND ABS(si.SponsorID) = u.SponsorID) " +
                "INNER JOIN healthwatch..UserProjectRoundUser w ON u.UserID = w.UserID " +
                "INNER JOIN healthwatch..UserProjectRoundUserAnswer s ON w.ProjectRoundUserID = s.ProjectRoundUserID " +
                "INNER JOIN Answer a ON s.AnswerID = a.AnswerID " +
                queryJoin.ToString().Split('¤')[q] +
                "WHERE a.EndDT IS NOT NULL AND si.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
                "ORDER BY u.UserID, s.AnswerID";
            SqlDataReader rs = Db.rs(SQL, "eFormSqlConnection");
            while (rs.Read())
            {
				if (!al.Contains(rs.GetInt32(11)))
				{
					al.Add(rs.GetInt32(11));

					if (userID != 0)
					{
						output.Append("¤");
					}

					if (userID == 0 || userID != rs.GetInt32(0))
					{
						userCaseCounter = 0;
						userID = rs.GetInt32(0);
					}
					userCaseCounter++;

					if (q == 0)
					{
						caseCount++;

						output.Append("" +
							userID.ToString().PadRight(5, ' ') +
							(full ?
							rowDelim +
							(rs.IsDBNull(10) ? "" : rs.GetString(10)) +
							rowDelim +
							//(rs.IsDBNull(7) ? 0 : rs.GetInt32(7)).ToString().PadRight(2, ' ') +
							//(rs.IsDBNull(8) ? "".PadRight(17, ' ') : rs.GetDateTime(8).ToString("dd-MMM-yyyy HH:mm")) +
							(rs.IsDBNull(9) ? "".PadRight(17, ' ') : rs.GetDateTime(9).ToString("dd-MMM-yyyy HH:mm")) +
							(rs.IsDBNull(9) ? 0 : 1) +
							"" : ""));
						//SqlDataReader rs2;
						//rs2 = Db.rs("SELECT COUNT(*) FROM Session WHERE UserID = " + userID);
						//if (rs2.Read())
						//{
						//    output.Append(rs2.GetInt32(0).ToString().PadRight(4, ' '));
						//}
						//else
						//{
						//    output.Append(string.Empty.PadRight(4, ' '));
						//}
						//rs2.Close();
						//rs2 = Db.rs("SELECT COUNT(*) FROM ExerciseStats WHERE UserID = " + userID);
						//if (rs2.Read())
						//{
						//    output.Append(rs2.GetInt32(0).ToString().PadRight(4, ' '));
						//}
						//else
						//{
						//    output.Append(string.Empty.PadRight(4, ' '));
						//}
						//rs2.Close();
						//System.Collections.ArrayList dt = new ArrayList();
						//int minutes = 0, hours = 0;
						//rs2 = Db.rs("SELECT " +
						//    "DT, " +
						//    "EndDT, " +
						//    "AutoEnded, " +
						//    "dbo.cf_sessionMinutes(DT, EndDT, AutoEnded) " +
						//    "FROM Session " +
						//    "WHERE UserID = " + userID + " " +
						//    "");
						//while (rs2.Read())
						//{
						//    int minutesThisSession = rs2.GetInt32(3);
						//    if (!dt.Contains(rs2.GetDateTime(0).ToString("yyyy-MM-dd")))
						//    {
						//        SqlDataReader rs4 = Db.rs("SELECT e.Minutes FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID WHERE es.UserID = " + userID + " AND dbo.cf_yearMonthDay(es.DateTime) = '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd") + "'");
						//        while (rs4.Read())
						//        {
						//            minutesThisSession += rs4.GetInt32(0);
						//        }
						//        rs4.Close();
						//    }

						//    minutes += minutesThisSession;
						//    hours += (int)Math.Ceiling((double)minutesThisSession / (double)60);
						//}
						//rs2.Close();
						output.Append((full ?
							//minutes.ToString().PadRight(4, ' ') +
							//hours.ToString().PadRight(4, ' ') +
							rs.GetInt32(1).ToString().PadRight(4, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							//rs.GetInt32(5).ToString().PadRight(5, ' ') +
							"" : "") +
							userCaseCounter.ToString().PadRight(5, ' ') +
							(full ?
							//rs.GetInt32(6).ToString().PadRight(2, ' ') +
							(rs.IsDBNull(3) ? "".PadRight(17, ' ') : rs.GetDateTime(3).ToString("dd-MMM-yyyy HH:mm")) +
							(rs.IsDBNull(4) ? "".PadRight(17, ' ') : rs.GetDateTime(4).ToString("dd-MMM-yyyy HH:mm")) +
							Convert.ToInt32(((double)(((TimeSpan)(rs.GetDateTime(3) - rs.GetDateTime(5))).Days)) / 30.4d).ToString().PadRight(5, ' ') +
							"" : "") +
							rs.GetDateTime(4).ToString("yyyyMM") +
							rs.GetDateTime(4).ToString("yyyy") + cul.Calendar.GetWeekOfYear(rs.GetDateTime(4), System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString().PadLeft(2, '0') +
							//rowDelim +
							"");
					}

					for (int i = q * queryDivide; i < Math.Min((q + 1) * queryDivide, varCount); i++)
					{
						if (varBreak[i] == "1")
						{
							output.Append(rowDelim);
						}

						int pos = i + 12 - q * queryDivide;

						switch (Convert.ToInt32(varType[i]))
						{
							case 1:
								output.Append((rs.IsDBNull(pos) ? "" : rs.GetInt32(pos).ToString()).PadRight(Convert.ToInt32(varPosition[i]), ' '));
								break;
							case 2:
								output.Append(trunc((rs.IsDBNull(pos) ? "" : rs.GetString(pos)), 230));
								break;
							case 3:
								goto case 1;
							case 4:
								output.Append((rs.IsDBNull(pos) ? "" : rs.GetDecimal(pos).ToString("N2").Replace(".", "").Replace(",", "")).PadRight(Convert.ToInt32(varPosition[i]), ' '));
								break;
							case 9:
								output.Append((rs.IsDBNull(pos) ? "" : rs.GetInt32(pos).ToString()).PadRight(Convert.ToInt32(varPosition[i]), ' '));
								if (Convert.ToInt32(varAttr[i]) > 2)
								{
									if (!rs.IsDBNull(pos))
									{
										int cx = 0;
										for (int ii = 0; ii < Convert.ToInt32(varAttr[i]); ii++)
										{
											if (rs.GetInt32(pos) >= 100 / Convert.ToInt32(varAttr[i]) * ii)
											{
												cx = ii;
											}
										}
										output.Append(cx.ToString().PadRight(Convert.ToInt32(varPosition[i]), ' '));
									}
									else
									{
										output.Append("".PadRight(Convert.ToInt32(varPosition[i]), ' '));
									}
								}
								break;
						}
					}
				}
            }
            rs.Close();
            #endregion

            queryResult.Add(output.ToString().Split('¤'));
        }

        System.Text.StringBuilder mergedOutput = new System.Text.StringBuilder();
        for (int p = 0; p < caseCount; p++)
        {
            for (int m = 0; m < queries; m++)
            {
                if (queryResult.Count > m && ((string[])queryResult[m]).Length > p)
                {
                    mergedOutput.Append(((string[])queryResult[m])[p]);
                }
            }
            mergedOutput.Append(rowDelim);
        }

        string fname = HttpContext.Current.Server.MapPath("tmp/" + DateTime.Now.Ticks + ".sps");
        System.IO.FileStream fs = new System.IO.FileStream(fname, System.IO.FileMode.Create);
        System.IO.StreamWriter f = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
        f.Write("DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + mergedOutput.ToString() + "END DATA." + rowDelim + syn + rowDelim);
        f.Close();
        fs.Close();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
        HttpContext.Current.Response.WriteFile(fname);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportRepeatedOneQuestionPerRow(string sponsorIDs, int rExportSurveyID, int rExportLangID)
    {
        #region init
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string def = "", syn = "";
        bool nextShouldBreak = true;
        int caseCounter = 0, recordCount = 0;
        string rowDelim = "\n";
        #endregion
        #region SPSS header
        nextShouldBreak = true;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F7.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
        syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F7.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysAnswerID)." + rowDelim;
        syn += "VARIABLE LABELS sysAnswerID 'Answer ID'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F7.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
        syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
        syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
        syn += "VARIABLE LABELS sysEnd 'End Date Time'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(A32)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysVar)." + rowDelim;
        syn += "VARIABLE LABELS sysVar 'Variable'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(A16)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysVal)." + rowDelim;
        syn += "VARIABLE LABELS sysVar 'Value'." + rowDelim;
        nextShouldBreak = false;
        #endregion
        
        System.Text.StringBuilder output = new System.Text.StringBuilder();

        int userID = 0, answerID = 0, userCaseCounter = 0;
        #region User base
        string SQL = "SELECT " +
            "u.UserID, " +
            "a.AnswerID, " +
            "a.StartDT, " +
            "a.EndDT, " +
            "op.ExportValue, " +
            "av.ValueInt, " +
            "av.ValueText, " +
            "av.ValueDecimal, " +
                
            "sq.QuestionID, " +
            "qo.OptionID, " +
            "o.OptionType, " +
            "q.Variablename, " +
            "o.Variablename, " +
            "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +
            "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID) " +

            "FROM healthwatch..[User] u " +
            "INNER JOIN healthwatch..SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = u.SponsorID " +
            "INNER JOIN healthwatch..UserProjectRoundUser w ON u.UserID = w.UserID " +
            "INNER JOIN healthwatch..UserProjectRoundUserAnswer s ON w.ProjectRoundUserID = s.ProjectRoundUserID " +
            "INNER JOIN Answer a ON s.AnswerID = a.AnswerID " +
            "INNER JOIN AnswerValue av ON av.AnswerID = a.AnswerID AND av.DeletedSessionID IS NULL " +

            "INNER JOIN SurveyQuestion sq ON sq.SurveyID = " + rExportSurveyID + " AND av.QuestionID = sq.QuestionID " +
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
            "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID  AND av.OptionID = qo.OptionID " +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +

            "LEFT OUTER JOIN OptionComponents op ON op.OptionID = av.OptionID AND op.OptionComponentID = av.ValueInt " +

            "WHERE a.EndDT IS NOT NULL AND u.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
            "ORDER BY u.UserID, a.AnswerID";
        SqlDataReader rs = Db.rs(SQL, "eFormSqlConnection");
        while (rs.Read())
        {
            if (userID == 0 || userID != rs.GetInt32(0))
            {
                userCaseCounter = 0;
                answerID = 0;
                userID = rs.GetInt32(0);
            }
            if(answerID == 0 || answerID != rs.GetInt32(1))
            {
                userCaseCounter++;
                answerID = rs.GetInt32(1);
            }

            string tmp = userID.ToString().PadRight(7, ' ') +
                rs.GetInt32(1).ToString().PadRight(7, ' ') +
                userCaseCounter.ToString().PadRight(7, ' ') +
                (rs.IsDBNull(2) ? "".PadRight(17, ' ') : rs.GetDateTime(2).ToString("dd-MMM-yyyy HH:mm")) +
                (rs.IsDBNull(3) ? "".PadRight(17, ' ') : rs.GetDateTime(3).ToString("dd-MMM-yyyy HH:mm"));

            string var = (rs.IsDBNull(11) || rs.GetString(11).Trim() == "" ? "Q" + rs.GetInt32(8).ToString() : rs.GetString(11));
            if (rs.GetInt32(13) > 1)
            {
                var += (rs.IsDBNull(12) || rs.GetString(12).Trim() == "" ? "A" + rs.GetInt32(9).ToString() : rs.GetString(11));
            }
            var = var.Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"),"_").Replace(" ", "_").Replace("-", "_").Replace("/", "_");

            switch (Convert.ToInt32(rs.GetInt32(10)))
            {
                case 1:
                    output.AppendLine(tmp + var.PadRight(32, ' ') + (rs.IsDBNull(4) ? string.Empty : rs.GetInt32(4).ToString()).PadRight(16, ' '));
                    break;
                case 2:
                    output.AppendLine(tmp + var.PadRight(32, ' ') + trunc((rs.IsDBNull(6) ? string.Empty : rs.GetString(6)), 16));
                    break;
                case 3:
                    goto case 1;
                case 4:
                    output.AppendLine(tmp + var.PadRight(32, ' ') + (rs.IsDBNull(7) ? string.Empty : rs.GetDecimal(7).ToString("N2").Replace(".", "").Replace(",", "")).PadRight(16, ' '));
                    break;
                case 9:
                    output.AppendLine(tmp + var.PadRight(32, ' ') + (rs.IsDBNull(5) ? string.Empty : rs.GetInt32(5).ToString()).PadRight(16, ' '));
                    if (rs.GetInt32(14) > 2)
                    {
                        if (!rs.IsDBNull(5))
                        {
                            int cx = 0;
                            for (int ii = 0; ii < rs.GetInt32(14); ii++)
                            {
                                if (rs.GetInt32(5) >= 100 / rs.GetInt32(14) * ii)
                                {
                                    cx = ii;
                                }
                            }
                            output.AppendLine(tmp + (var + "Zone").PadRight(32, ' ') + cx.ToString().PadRight(16, ' '));
                        }
                        else
                        {
                            output.AppendLine(tmp + (var + "Zone").PadRight(32, ' ') + string.Empty.PadRight(16, ' '));
                        }
                    }
                    break;
            }
        }
        rs.Close();

        string ret = "DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + "END DATA." + rowDelim + syn + rowDelim;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
        HttpContext.Current.Response.Write(ret);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportHypergene(int sponsorProjectRoundUnitID)
    {
        int sponsorID = 0, surveyID = 0, onlyEveryDays = 0;
        DateTime surveyDate = DateTime.Now;
        SqlDataReader rs = Db.rs("SELECT " +
            "ABS(spru.SponsorID), " +
            "spru.SurveyID, " +
            "spru.OnlyEveryDays, " +
            "(" +
                "SELECT MIN(uprua.DT) " +
                "FROM UserProjectRoundUser upru " +
                "INNER JOIN UserProjectRoundUserAnswer uprua ON upru.ProjectRoundUserID = uprua.ProjectRoundUserID " +
                "WHERE upru.ProjectRoundUnitID = spru.ProjectRoundUnitID" +
            ") AS DT " +
            "FROM SponsorProjectRoundUnit spru " +
            "WHERE spru.SponsorProjectRoundUnitID = " + sponsorProjectRoundUnitID);
        if (rs.Read())
        {
            sponsorID = rs.GetInt32(0);
            surveyID = rs.GetInt32(1);
            onlyEveryDays = rs.GetInt32(2);
            if (!rs.IsDBNull(3))
            {
                surveyDate = rs.GetDateTime(3);
            }
        }
        rs.Close();

        System.Text.StringBuilder output = new System.Text.StringBuilder();
        string delim = "\t";
        output.AppendLine("" +
            "sysAnswerID" + delim +
            "sysSurveyRoundID" + delim +
            "sysSurveyDate" + delim +
            "sysDept" + delim +
            "gend" + delim +
            "sysVar" + delim +
            "sysVal");

        int surveyRoundID = 1;
        Hashtable seen = new Hashtable();
        #region User base
        string SQL = "SELECT " +
            "u.UserID, " +              // 0
            "a.AnswerID, " +
            "a.StartDT, " +
            "a.EndDT, " +
            "op.ExportValue, " +
            "av.ValueInt, " +           // 5
            "av.ValueText, " +
            "av.ValueDecimal, " +

            "sq.QuestionID, " +
            "qo.OptionID, " +
            "o.OptionType, " +          // 10
            "q.Variablename, " +
            "o.Variablename, " +
            "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +
            "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +
            "d.DepartmentShort, " +      // 15
            "b.ValueInt " +

            "FROM healthwatch..[User] u " +
            "INNER JOIN healthwatch..Department d ON u.DepartmentID = d.DepartmentID " +
            
            "INNER JOIN healthwatch..UserProfileBQ b ON b.UserProfileID = u.UserProfileID AND b.BQID = 2 " +
            
            "INNER JOIN healthwatch..SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = u.SponsorID " +
            "INNER JOIN healthwatch..UserProjectRoundUser w ON u.UserID = w.UserID " +
            "INNER JOIN healthwatch..UserProjectRoundUserAnswer s ON w.ProjectRoundUserID = s.ProjectRoundUserID " +
            "INNER JOIN Answer a ON s.AnswerID = a.AnswerID " +
            "INNER JOIN AnswerValue av ON av.AnswerID = a.AnswerID AND av.DeletedSessionID IS NULL " +

            "INNER JOIN SurveyQuestion sq ON sq.SurveyID = " + surveyID + " AND av.QuestionID = sq.QuestionID " +
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
            "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID  AND av.OptionID = qo.OptionID " +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +

            "LEFT OUTER JOIN OptionComponents op ON op.OptionID = av.OptionID AND op.OptionComponentID = av.ValueInt " +

            "WHERE a.EndDT IS NOT NULL AND u.SponsorID = " + sponsorID + " " +
            "ORDER BY a.AnswerID, sq.SortOrder";
        rs = Db.rs(SQL, "eFormSqlConnection");
        while (rs.Read())
        {
            if (rs.GetDateTime(3) >= surveyDate)
            {
                if (rs.GetDateTime(3) > surveyDate.AddDays(onlyEveryDays))
                {
                    surveyDate = rs.GetDateTime(3);
                    surveyRoundID++;
                }
                string key = rs.GetInt32(0) + "_" + surveyRoundID;
                if (!seen.Contains(key))
                {
                    seen.Add(key, rs.GetInt32(1));
                }
                if ((int)seen[key] == rs.GetInt32(1))
                {
                    string tmp = rs.GetInt32(1).ToString().PadRight(7, ' ') + delim +
                        "HW" + (Convert.ToInt32(sponsorProjectRoundUnitID.ToString().PadRight(5, '0')) + surveyRoundID).ToString() + delim +
                        surveyDate.ToString("yyyy-MM-dd") + delim +
                        rs.GetString(15) + delim +
                        (rs.IsDBNull(16) ? "" : rs.GetInt32(16).ToString());

                    string var = (rs.IsDBNull(11) || rs.GetString(11).Trim() == "" ? "Q" + rs.GetInt32(8).ToString() : rs.GetString(11));
                    if (rs.GetInt32(13) > 1)
                    {
                        var += (rs.IsDBNull(12) || rs.GetString(12).Trim() == "" ? "A" + rs.GetInt32(9).ToString() : rs.GetString(11));
                    }
                    var = var.Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"), "_").Replace(" ", "_").Replace("-", "_").Replace("/", "_");

                    switch (Convert.ToInt32(rs.GetInt32(10)))
                    {
                        case 1:
                            output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(4) ? "" : rs.GetInt32(4).ToString()));
                            break;
                        case 2:
                            output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(6) ? "" : rs.GetString(6)));
                            break;
                        case 3:
                            goto case 1;
                        case 4:
                            output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(7) ? "" : rs.GetDecimal(7).ToString("N2").Replace(".", "").Replace(",", "")));
                            break;
                        case 9:
                            output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(5) ? "" : rs.GetInt32(5).ToString()));
                            if (rs.GetInt32(14) > 2)
                            {
                                if (!rs.IsDBNull(5))
                                {
                                    int cx = 0;
                                    for (int ii = 0; ii < rs.GetInt32(14); ii++)
                                    {
                                        if (rs.GetInt32(5) >= 100 / rs.GetInt32(14) * ii)
                                        {
                                            cx = ii;
                                        }
                                    }
                                    output.AppendLine(tmp + delim + (var + "Zone") + delim + cx.ToString());
                                }
                                else
                                {
                                    output.AppendLine(tmp + delim + (var + "Zone") + delim + "");
                                }
                            }
                            output.AppendLine(tmp + delim + (var + "Grade") + delim + (rs.IsDBNull(5) ? "" : Math.Ceiling(((double)rs.GetInt32(5) + 1d) / 101d * 10d).ToString()));
                            break;
                    }
                }
            }
        }
        rs.Close();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/tab-separated-values";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
        HttpContext.Current.Response.Write(output.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportExtendedHypergene(int sponsorExtendedSurveyID)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        string delim = "\t";
        output.AppendLine("" +
            "sysAnswerID" + delim +
            "sysSurveyRoundID" + delim +
            "sysSurveyDate" + delim +
            "sysDept" + delim +
            "gend" + delim +
            "sysVar" + delim +
            "sysVal");

        #region User base
        string SQL = "SELECT " +
            "u.UserID, " +              // 0
            "a.AnswerID, " +
            "a.StartDT, " +
            "a.EndDT, " +
            "op.ExportValue, " +
            "av.ValueInt, " +           // 5
            "av.ValueText, " +
            "av.ValueDecimal, " +

            "sq.QuestionID, " +
            "qo.OptionID, " +
            "o.OptionType, " +          // 10
            "q.Variablename, " +
            "o.Variablename, " +
            "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +
            "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +
            "d.DepartmentShort, " +      // 15
            "b.ValueInt, " +
            "pr.Closed " +

            "FROM healthwatch..[User] u " +
            "INNER JOIN healthwatch..Department d ON u.DepartmentID = d.DepartmentID " +

            "INNER JOIN healthwatch..UserProfileBQ b ON b.UserProfileID = u.UserProfileID AND b.BQID = 2 " +

            "INNER JOIN healthwatch..SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = u.SponsorID " +
            "INNER JOIN healthwatch..UserSponsorExtendedSurvey w ON u.UserID = w.UserID AND w.SponsorExtendedSurveyID = " + sponsorExtendedSurveyID + " " +
            "INNER JOIN Answer a ON w.AnswerID = a.AnswerID " +
            "INNER JOIN AnswerValue av ON av.AnswerID = a.AnswerID AND av.DeletedSessionID IS NULL " +

            "INNER JOIN ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
            "INNER JOIN SurveyQuestion sq ON sq.SurveyID = pr.SurveyID AND av.QuestionID = sq.QuestionID " +
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
            "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID  AND av.OptionID = qo.OptionID " +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +

            "LEFT OUTER JOIN OptionComponents op ON op.OptionID = av.OptionID AND op.OptionComponentID = av.ValueInt " +

            "WHERE a.EndDT IS NOT NULL " +
            "ORDER BY a.AnswerID, sq.SortOrder";
        SqlDataReader rs = Db.rs(SQL, "eFormSqlConnection");
        while (rs.Read())
        {
            string tmp = rs.GetInt32(1).ToString().PadRight(7, ' ') + delim +
                "HW" + sponsorExtendedSurveyID.ToString().PadLeft(5,'0') + delim +
                (rs.IsDBNull(17) ? "" : rs.GetDateTime(17).ToString("yyyy-MM-dd")) + delim +
                rs.GetString(15) + delim +
                (rs.IsDBNull(16) ? "" : rs.GetInt32(16).ToString());

            string var = (rs.IsDBNull(11) || rs.GetString(11).Trim() == "" ? "Q" + rs.GetInt32(8).ToString() : rs.GetString(11));
            if (rs.GetInt32(13) > 1)
            {
                var += (rs.IsDBNull(12) || rs.GetString(12).Trim() == "" ? "A" + rs.GetInt32(9).ToString() : rs.GetString(11));
            }
            var = var.Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"), "_").Replace(" ", "_").Replace("-", "_").Replace("/", "_");

            if (var.StartsWith("U") && var.IndexOf("_") > 1 && var.IndexOf("_") < 5)    // SPECIAL - only include U1_ -- U13_
            {
                switch (Convert.ToInt32(rs.GetInt32(10)))
                {
                    case 1:
                        output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(4) ? "" : rs.GetInt32(4).ToString()));
                        break;
                    case 2:
                        output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(6) ? "" : rs.GetString(6).Replace("\r\n", "").Replace("\r", "").Replace("\n", "")));
                        break;
                    case 3:
                        goto case 1;
                    case 4:
                        output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(7) ? "" : rs.GetDecimal(7).ToString("N2").Replace(".", "").Replace(",", "")));
                        break;
                    case 9:
                        //if (var.StartsWith("U11") || var.StartsWith("U12"))         // SPECIAL
                        //{
                        //    output.Append(tmp + delim + var + delim);
                        //    if(!rs.IsDBNull(5))
                        //    {
                        //        int i = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rs.GetInt32(5))/10));
                        //        if(i == 0) i = 1;
                        //        output.Append(i.ToString());
                        //    }
                        //    output.AppendLine();
                        //}
                        //else
                        //{
                            output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(5) ? "" : rs.GetInt32(5).ToString()));
                            if (rs.GetInt32(14) > 2)
                            {
                                if (!rs.IsDBNull(5))
                                {
                                    int cx = 0;
                                    for (int ii = 0; ii < rs.GetInt32(14); ii++)
                                    {
                                        if (rs.GetInt32(5) >= 100 / rs.GetInt32(14) * ii)
                                        {
                                            cx = ii;
                                        }
                                    }
                                    output.AppendLine(tmp + delim + (var + "Zone") + delim + cx.ToString());
                                }
                                else
                                {
                                    output.AppendLine(tmp + delim + (var + "Zone") + delim + "");
                                }
                            }
                            output.AppendLine(tmp + delim + (var + "Grade") + delim + (rs.IsDBNull(5) ? "" : Math.Ceiling(((double)rs.GetInt32(5) + 1d) / 101d * 10d).ToString()));
                        //}
                        break;
                }
            }
        }
        rs.Close();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/tab-separated-values";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
        HttpContext.Current.Response.Write(output.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportExtendedHypergeneEform(int projectRoundID)
    {
        System.Text.StringBuilder output = new System.Text.StringBuilder();
        string delim = "\t";
        output.AppendLine("" +
            "sysAnswerID" + delim +
            "sysSurveyRoundID" + delim +
            "sysSurveyDate" + delim +
            "sysDept" + delim +
            "gend" + delim +
            "sysVar" + delim +
            "sysVal");

        #region User base
        string SQL = "SELECT " +
            "prs.ProjectRoundUserID, " +              // 0
            "a.AnswerID, " +
            "a.StartDT, " +
            "a.EndDT, " +
            "op.ExportValue, " +
            "av.ValueInt, " +           // 5
            "av.ValueText, " +
            "av.ValueDecimal, " +

            "sq.QuestionID, " +
            "qo.OptionID, " +
            "o.OptionType, " +          // 10
            "q.Variablename, " +
            "o.Variablename, " +
            "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), " +
            "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID), " +
            "pru.ID, " +      // 15
            "b.ValueInt, " +
            "pr.Closed " +

            "FROM Answer a " +
            "INNER JOIN AnswerValue av ON av.AnswerID = a.AnswerID AND av.DeletedSessionID IS NULL " +
            "INNER JOIN ProjectRound pr ON a.ProjectRoundID = pr.ProjectRoundID " +
            "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +

            //"INNER JOIN Feedback f ON pr.FeedbackID = f.FeedbackID " +
            "INNER JOIN FeedbackQuestion fq ON pr.FeedbackID = fq.FeedbackID " +

            "INNER JOIN SurveyQuestion sq ON sq.SurveyID = pr.SurveyID AND av.QuestionID = sq.QuestionID  AND fq.QuestionID = sq.QuestionID " +
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
            "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID  AND av.OptionID = qo.OptionID " +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +

            "LEFT OUTER JOIN OptionComponents op ON op.OptionID = av.OptionID AND op.OptionComponentID = av.ValueInt " +
            
            "LEFT OUTER JOIN ProjectRoundUser prs ON a.ProjectRoundUserID = prs.ProjectRoundUserID " +
            "LEFT OUTER JOIN healthwatch..[User] u ON (prs.Email COLLATE SQL_Latin1_General_CP1_CI_AS) = u.Email " +
            "LEFT OUTER JOIN healthwatch..UserProfileBQ b ON b.UserProfileID = u.UserProfileID AND b.BQID = 2 " +

            "WHERE a.ProjectRoundID = " + projectRoundID + " AND a.EndDT IS NOT NULL " +
            "ORDER BY a.AnswerID, sq.SortOrder";
        SqlDataReader rs = Db.rs(SQL, "eFormSqlConnection");
        while (rs.Read())
        {
            string tmp = rs.GetInt32(1).ToString().PadRight(7, ' ') + delim +
                "EF" + projectRoundID.ToString().PadLeft(5, '0') + delim +
                (rs.IsDBNull(17) ? "" : rs.GetDateTime(17).ToString("yyyy-MM-dd")) + delim +
                rs.GetString(15) + delim +
                (rs.IsDBNull(16) ? "" : rs.GetInt32(16).ToString());

            string var = (rs.IsDBNull(11) || rs.GetString(11).Trim() == "" ? "Q" + rs.GetInt32(8).ToString() : rs.GetString(11));
            if (rs.GetInt32(13) > 1)
            {
                var += (rs.IsDBNull(12) || rs.GetString(12).Trim() == "" ? "A" + rs.GetInt32(9).ToString() : rs.GetString(11));
            }
            var = var.Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"), "_").Replace(" ", "_").Replace("-", "_").Replace("/", "_");

            //if (var.StartsWith("U") && var.IndexOf("_") > 1 && var.IndexOf("_") < 5)    // SPECIAL - only include U1_ -- U13_
            //{
                switch (Convert.ToInt32(rs.GetInt32(10)))
                {
                    case 1:
                        output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(4) ? "" : rs.GetInt32(4).ToString()));
                        break;
                    case 2:
                        output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(6) ? "" : rs.GetString(6).Replace("\r\n", "").Replace("\r", "").Replace("\n", "")));
                        break;
                    case 3:
                        goto case 1;
                    case 4:
                        output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(7) ? "" : rs.GetDecimal(7).ToString("N2").Replace(".", "").Replace(",", "")));
                        break;
                    case 9:
                        //if (var.StartsWith("U11") || var.StartsWith("U12"))         // SPECIAL
                        //{
                        //    output.Append(tmp + delim + var + delim);
                        //    if (!rs.IsDBNull(5))
                        //    {
                        //        int i = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(rs.GetInt32(5)) / 10));
                        //        if (i == 0) i = 1;
                        //        output.Append(i.ToString());
                        //    }
                        //    output.AppendLine();
                        //}
                        //else
                        //{
                            output.AppendLine(tmp + delim + var + delim + (rs.IsDBNull(5) ? "" : rs.GetInt32(5).ToString()));
                            if (rs.GetInt32(14) > 2)
                            {
                                if (!rs.IsDBNull(5))
                                {
                                    int cx = 0;
                                    for (int ii = 0; ii < rs.GetInt32(14); ii++)
                                    {
                                        if (rs.GetInt32(5) >= 100 / rs.GetInt32(14) * ii)
                                        {
                                            cx = ii;
                                        }
                                    }
                                    output.AppendLine(tmp + delim + (var + "Zone") + delim + cx.ToString());
                                }
                                else
                                {
                                    output.AppendLine(tmp + delim + (var + "Zone") + delim + "");
                                }
                            }
                            output.AppendLine(tmp + delim + (var + "Grade") + delim + (rs.IsDBNull(5) ? "" : Math.Ceiling(((double)rs.GetInt32(5)+1d)/101d*10d).ToString()));
                        //}
                        break;
                }
            //}
        }
        rs.Close();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/tab-separated-values";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
        HttpContext.Current.Response.Write(output.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

	public static void execExportMetadata(string sponsorIDs, int rExportLangID)
	{
		execExportMetadata(sponsorIDs, rExportLangID, "");
	}
    public static void execExportMetadata(string sponsorIDs, int rExportLangID, string toDTstr)
    {
		string tempSponsorIDs = "";
		foreach (string s in sponsorIDs.Split(','))
		{
			if (s != "")
			{
				tempSponsorIDs += "," + Convert.ToInt32(s) + ",-" + Convert.ToInt32(s);
			}
		}
		sponsorIDs = tempSponsorIDs;

        #region EXPORT
        #region init
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string def = "", syn = "";
        bool nextShouldBreak = true;
        int caseCounter = 0, recordCount = 0, ax = 0;
        string rowDelim = "\n";
        #endregion
        #region SPSS header
        nextShouldBreak = true;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
        syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysFirstInviteDT)." + rowDelim;
        syn += "VARIABLE LABELS sysFirstInviteDT 'First invite DT'." + rowDelim;
        nextShouldBreak = false;

		caseCounter++;
		def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
			"V" + (caseCounter) + "(DATETIME17.0)";
		syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserActivationDT)." + rowDelim;
		syn += "VARIABLE LABELS sysUserActivationDT 'User activation DT'." + rowDelim;
		nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F2.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserStatus)." + rowDelim;
        syn += "VARIABLE LABELS sysUserStatus 'User status'." + rowDelim;
        syn += "VALUE LABELS sysUserStatus";
        syn += " 0 'Active'";
        syn += " 1 'Stopped, work related'";
        syn += " 2 'Stopped, education leave'";
        syn += " 14 'Stopped, parental leave'";
        syn += " 24 'Stopped, sick leave'";
        syn += rowDelim;
        syn += " 34 'Stopped, do not want to participate'";
        syn += " 44 'Stopped, no longer associated'";
        syn += " 4 'Stopped, other reason'";
        syn += " 5 'Stopped, unknown reason'";
        syn += " 6 'Stopped, project completed'";
        syn += "." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserStatusDate)." + rowDelim;
        syn += "VARIABLE LABELS sysUserStatusDate 'User Status Date Time'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysConsent)." + rowDelim;
        syn += "VARIABLE LABELS sysConsent 'Consent'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F1.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysConsentYN)." + rowDelim;
        syn += "VARIABLE LABELS sysConsentYN 'Consent'." + rowDelim;
        syn += "VALUE LABELS sysConsentYN";
        syn += " 0 'No'";
        syn += " 1 'Yes'";
        syn += "." + rowDelim;
        nextShouldBreak = false;

        SqlDataReader rs3, rs2, rs;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F4.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserLogins)." + rowDelim;
        syn += "VARIABLE LABELS sysUserLogins 'User login count'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F4.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserExercises)." + rowDelim;
        syn += "VARIABLE LABELS sysUserExercises 'User exercise count'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F4.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserMinutes)." + rowDelim;
        syn += "VARIABLE LABELS sysUserMinutes 'User minutes spent'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F4.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserHours)." + rowDelim;
        syn += "VARIABLE LABELS sysUserHours 'User hours spent'." + rowDelim;
        nextShouldBreak = false;

        ax = 0;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F4.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysOrg)." + rowDelim;
        syn += "VARIABLE LABELS sysOrg 'Organisation'." + rowDelim;
        syn += "VALUE LABELS sysOrg";
        rs3 = Db.rs("SELECT SponsorID, Sponsor FROM Sponsor WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
        while (rs3.Read())
        {
            string s = trunc(rs3.GetString(1), 115);
            ax += s.Length + 10;
            if (ax > 115)
            {
                ax = s.Length + 10;
                syn += rowDelim;
            }
            syn += " " + rs3.GetInt32(0) + " '" + s + "'";
        }
        rs3.Close();
        syn += "." + rowDelim;
        nextShouldBreak = false;

        ax = 0;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDept)." + rowDelim;
        syn += "VARIABLE LABELS sysDept 'Department'." + rowDelim;
        syn += "VALUE LABELS sysDept";
        rs3 = Db.rs("SELECT DepartmentID, Department FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
        while (rs3.Read())
        {
            string s = trunc(rs3.GetString(1), 115);
            ax += s.Length + 10;
            if (ax > 115)
            {
                ax = s.Length + 10;
                syn += rowDelim;
            }
            syn += " " + rs3.GetInt32(0) + " '" + s + "'";
        }
        rs3.Close();
        syn += "." + rowDelim;
        nextShouldBreak = false;

        ax = 0;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptTree)." + rowDelim;
        syn += "VARIABLE LABELS sysDeptTree 'Department tree'." + rowDelim;
        syn += "VALUE LABELS sysDeptTree";
        rs3 = Db.rs("SELECT DepartmentID, dbo.cf_DepartmentTree(DepartmentID,' > ') FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
        while (rs3.Read())
        {
            string s = trunc(rs3.GetString(1), 115);
            ax += s.Length + 10;
            if (ax > 115)
            {
                ax = s.Length + 10;
                syn += rowDelim;
            }
            syn += " " + rs3.GetInt32(0) + " '" + s + "'";
        }
        rs3.Close();
        syn += "." + rowDelim;
        nextShouldBreak = false;

        ax = 0;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptShort)." + rowDelim;
        syn += "VARIABLE LABELS sysDeptShort 'Department short name'." + rowDelim;
        syn += "VALUE LABELS sysDeptShort";
        rs3 = Db.rs("SELECT DepartmentID, DepartmentShort FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
        while (rs3.Read())
        {
            string s = trunc(rs3.GetString(1), 115);
            ax += s.Length + 10;
            if (ax > 115)
            {
                ax = s.Length + 10;
                syn += rowDelim;
            }
            syn += " " + rs3.GetInt32(0) + " '" + s + "'";
        }
        rs3.Close();
        syn += "." + rowDelim;
        nextShouldBreak = false;

        ax = 0;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptShortTree)." + rowDelim;
        syn += "VARIABLE LABELS sysDeptShortTree 'Department short name tree'." + rowDelim;
        syn += "VALUE LABELS sysDeptShortTree";
        rs3 = Db.rs("SELECT DepartmentID, dbo.cf_DepartmentShortTree(DepartmentID,' > ') FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
        while (rs3.Read())
        {
            string s = trunc(rs3.GetString(1), 115);
            ax += s.Length + 10;
            if (ax > 115)
            {
                ax = s.Length + 10;
                syn += rowDelim;
            }
            syn += " " + rs3.GetInt32(0) + " '" + s + "'";
        }
        rs3.Close();
        syn += "." + rowDelim;
        nextShouldBreak = true;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(A250)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEmail)." + rowDelim;
        syn += "VARIABLE LABELS sysEmail 'Email'." + rowDelim;
        nextShouldBreak = true;

        rs = Db.rs("SELECT " +
            "DISTINCT " +
            "bq.BQID, " +
            "bq.Variable, " +
            "bq.Type, " +
            "bql.BQ " +
            "FROM SponsorBQ sbq " +
            "INNER JOIN BQ bq ON sbq.BQID = bq.BQID " +
            "INNER JOIN BQLang bql ON bq.BQID = bql.BQID AND bql.LangID = " + rExportLangID + " " +
            "WHERE bq.Restricted IS NULL AND sbq.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
            "ORDER BY bq.BQID");
        while (rs.Read())
        {
            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter);

            switch (rs.GetInt32(2))
            {
                case 1:
                case 4:
                case 7:
                    def += "(F5.0)"; break;
                case 3:
                    def += "(DATE11)"; break;
                //case 2:
            }
                    
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=" + (rs.IsDBNull(1) || rs.GetString(1).Trim() == "" ? "BQ" + rs.GetInt32(0) : rs.GetString(1)) + ")." + rowDelim;
            syn += "VARIABLE LABELS " + (rs.IsDBNull(1) || rs.GetString(1).Trim() == "" ? "BQ" + rs.GetInt32(0) : rs.GetString(1)) + " '" + rs.GetString(3) + "'." + rowDelim;
            switch (rs.GetInt32(2))
            {
                case 1:
                case 7:
                    syn += "VALUE LABELS " + (rs.IsDBNull(1) || rs.GetString(1).Trim() == "" ? "BQ" + rs.GetInt32(0) : rs.GetString(1)) + "";
                    rs3 = Db.rs("SELECT " +
                        "ba.BAID, " +
                        "bal.BA " +
                        "FROM BA ba " +
                        "INNER JOIN BALang bal ON ba.BAID = bal.BAID AND bal.LangID = " + rExportLangID + " " +
                        "WHERE ba.BQID = " + rs.GetInt32(0) + " " +
                        "ORDER BY ba.SortOrder");
                    while (rs3.Read())
                    {
                        ax += rs3.GetString(1).Length;
                        if (ax > 115)
                        {
                            ax = 0;
                            syn += rowDelim;
                        }
                        syn += " " + rs3.GetInt32(0) + " '" + trunc(rs3.GetString(1), 115) + "'";
                    }
                    rs3.Close();
                    syn += "." + rowDelim;
                break;
            }
            nextShouldBreak = false;
        }
        rs.Close();
        #endregion

        System.Text.StringBuilder output = new System.Text.StringBuilder();

		DateTime toDT = DateTime.MaxValue;
		if (toDTstr != "")
		{
			toDT = DateTime.ParseExact(toDTstr, "yyyyMMdd", (new System.Globalization.CultureInfo("en-US")));
		}

		int userID = 0; ArrayList al = new ArrayList();
        #region User base
        string email = "", SQL = "" +
			"SELECT " +
            "ABS(si.UserID) AS S3, " +        // 0
			"ABS(si.SponsorID), " +     // 1
			"ABS(si.DepartmentID), " +  // 2
            "si.StoppedReason, " +      // 3
            "si.Stopped, " +            // 4
            "si.Consent, " +            // 5
            "u.UserID AS S2, " +        // 6
            "u.ReminderLastSent, " +    // 7
            "si.Email AS S1, " +        // 8
			"(SELECT MIN(six.Sent) FROM SponsorInvite six WHERE ABS(six.DepartmentID) = ABS(si.DepartmentID)), " +
			"u.Created, " +				// 10
			"(SELECT MIN(six2.Consent) FROM SponsorInvite six2 WHERE ABS(six2.DepartmentID) = ABS(si.DepartmentID)), " +
			"(SELECT MAX(six3.Sent) FROM SponsorInvite six3 WHERE ABS(six3.SponsorID) = ABS(si.SponsorID)), " +
			"si.SponsorInviteID " +		// 13
            "FROM SponsorInvite si " +
            "LEFT OUTER JOIN [User] u ON (u.UserID = ABS(si.UserID) OR si.Email = u.Email + 'DELETED' AND ABS(si.SponsorID) = u.SponsorID) " + 
			(toDTstr != "" ? "AND u.Created <= '" + toDT.ToString("yyyy-MM-dd") + "' " : "") +
            "WHERE si.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
            "ORDER BY S1, S2 DESC, S3 DESC";
        rs = Db.rs(SQL);
        while (rs.Read())
        {
			if (rs.GetString(8) != email)
			{
				email = rs.GetString(8);
				userID = (rs.IsDBNull(0) ? (rs.IsDBNull(6) ? 0 : rs.GetInt32(6)) : rs.GetInt32(0));
				if (userID == 0 || !al.Contains(userID))
				{
					al.Add(userID);

					string stoppedReason = (rs.IsDBNull(3) || rs.IsDBNull(4) || rs.GetDateTime(4) > toDT ? 0 : rs.GetInt32(3)).ToString().PadRight(2, ' ');
					string stoppedDate = (rs.IsDBNull(4) || stoppedReason.Trim() == "0" ? "".PadRight(17, ' ') : rs.GetDateTime(4).ToString("dd-MMM-yyyy HH:mm"));
					if (rs.IsDBNull(0) && !rs.IsDBNull(6))
					{
						if (stoppedReason.Trim() == "0" && (rs.IsDBNull(7) || rs.GetDateTime(7) <= toDT))
						{
							stoppedReason = "34";
							stoppedDate = (rs.IsDBNull(7) ? "".PadRight(17, ' ') : rs.GetDateTime(7).ToString("dd-MMM-yyyy HH:mm"));
						}
					}

					DateTime sysFirstInviteDT = (rs.IsDBNull(9) ? DateTime.MaxValue : rs.GetDateTime(9));
					if (!rs.IsDBNull(11) && rs.GetDateTime(11) < sysFirstInviteDT)
					{
						sysFirstInviteDT = rs.GetDateTime(11).Date;
					}
					if (!rs.IsDBNull(12) && rs.GetDateTime(12) < sysFirstInviteDT)
					{
						sysFirstInviteDT = rs.GetDateTime(12).Date;
					}

					if (sysFirstInviteDT <= toDT)
					{
						output.Append("" +
							(userID == 0 || rs.IsDBNull(10) ? "" : userID.ToString()).PadRight(5, ' ') +
							(sysFirstInviteDT == DateTime.MaxValue ? "".PadRight(17, ' ') : sysFirstInviteDT.ToString("dd-MMM-yyyy HH:mm")) +
							(rs.IsDBNull(10) ? "".PadRight(17, ' ') : rs.GetDateTime(10).ToString("dd-MMM-yyyy HH:mm")) +
							stoppedReason +
							stoppedDate +
							(rs.IsDBNull(5) ? "".PadRight(17, ' ') : rs.GetDateTime(5).ToString("dd-MMM-yyyy HH:mm")) +
							(rs.IsDBNull(5) ? 0 : 1) +
							"");
						rs2 = Db.rs("SELECT COUNT(*) FROM Session WHERE UserID = " + userID + (toDTstr != "" ? " AND EndDT <= '" + toDT.ToString("yyyy-MM-dd") + "'" : ""));
						if (rs2.Read())
						{
							output.Append(rs2.GetInt32(0).ToString().PadRight(4, ' '));
						}
						else
						{
							output.Append(string.Empty.PadRight(4, ' '));
						}
						rs2.Close();
						rs2 = Db.rs("SELECT COUNT(*) FROM ExerciseStats WHERE UserID = " + userID + (toDTstr != "" ? " AND DateTime <= '" + toDT.ToString("yyyy-MM-dd") + "'" : ""));
						if (rs2.Read())
						{
							output.Append(rs2.GetInt32(0).ToString().PadRight(4, ' '));
						}
						else
						{
							output.Append(string.Empty.PadRight(4, ' '));
						}
						rs2.Close();
						System.Collections.ArrayList dt = new ArrayList();
						int minutes = 0, hours = 0;
						if (userID != 0)
						{
							rs2 = Db.rs("SELECT " +
								"DT, " +
								"EndDT, " +
								"AutoEnded, " +
								"dbo.cf_sessionMinutes(DT, EndDT, AutoEnded) " +
								"FROM Session " +
								"WHERE UserID = " + userID + " " +
								(toDTstr != "" ? "AND EndDT <= '" + toDT.ToString("yyyy-MM-dd") + "' " : "") +
								"");
							while (rs2.Read())
							{
								int minutesThisSession = rs2.GetInt32(3);
								if (!dt.Contains(rs2.GetDateTime(0).ToString("yyyy-MM-dd")))
								{
									SqlDataReader rs4 = Db.rs("SELECT e.Minutes " +
										"FROM ExerciseStats es " +
										"INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID " +
										"INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
										"INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
										"WHERE es.UserID = " + userID + " AND dbo.cf_yearMonthDay(es.DateTime) = '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd") + "'");
									while (rs4.Read())
									{
										minutesThisSession += rs4.GetInt32(0);
									}
									rs4.Close();
								}

								minutes += minutesThisSession;
								hours += (int)Math.Ceiling((double)minutesThisSession / (double)60);
							}
							rs2.Close();
						}
						output.Append("" +
							minutes.ToString().PadRight(4, ' ') +
							hours.ToString().PadRight(4, ' ') +
							rs.GetInt32(1).ToString().PadRight(4, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rs.GetInt32(2).ToString().PadRight(5, ' ') +
							rowDelim +
							(rs.IsDBNull(8) ? "" : rs.GetString(8)) +
							rowDelim +
							"");

						rs2 = Db.rs("SELECT DISTINCT bq.BQID, bq.Type FROM SponsorBQ sbq INNER JOIN BQ bq ON sbq.BQID = bq.BQID WHERE bq.Restricted IS NULL AND sbq.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") ORDER BY bq.BQID");
						while (rs2.Read())
						{
							rs3 = Db.rs("SELECT " +
								"ISNULL(w.BAID,b.ValueInt), " +
								"ISNULL(w.ValueInt,b.ValueInt), " +
								"ISNULL(w.ValueText,b.ValueText), " +
								"ISNULL(w.ValueDate,b.ValueDate) " +
								"FROM SponsorInvite si " +
								"LEFT OUTER JOIN [User] u ON u.UserID = " + userID + " " +
								"LEFT OUTER JOIN UserProfileBQ b ON b.UserProfileID = u.UserProfileID AND b.BQID = " + rs2.GetInt32(0) + " " +
								"LEFT OUTER JOIN SponsorInviteBQ w ON si.SponsorInviteID = w.SponsorInviteID AND w.BQID = " + rs2.GetInt32(0) + " " +
								"WHERE si.SponsorInviteID = " + rs.GetInt32(13));
							if (rs3.Read())
							{
								switch (rs2.GetInt32(1))
								{
									case 1:
									case 7:
										output.Append((rs3.IsDBNull(0) ? "" : rs3.GetInt32(0).ToString()).PadRight(5, ' ')); break;
									case 4:
										output.Append((rs3.IsDBNull(1) ? "" : rs3.GetInt32(1).ToString()).PadRight(5, ' ')); break;
									case 3:
										output.Append((rs3.IsDBNull(3) ? "" : rs3.GetDateTime(3).ToString("dd-MMM-yyyy")).PadRight(11, ' ')); break;
									//case 2:
								}
							}
							else
							{
								switch (rs2.GetInt32(1))
								{
									case 1:
									case 7:
										output.Append(string.Empty.PadRight(5, ' ')); break;
									case 4:
										output.Append(string.Empty.PadRight(5, ' ')); break;
									case 3:
										output.Append(string.Empty.PadRight(11, ' ')); break;
									//case 2:
								}
							}
							rs3.Close();
						}
						rs2.Close();

						output.Append(rowDelim);
					}
				}
			}
        }
        rs.Close();
        #endregion

        string ret = "DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + "END DATA." + rowDelim + syn + rowDelim;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
        HttpContext.Current.Response.Write(ret);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportLogin(string sponsorIDs)
    {
		string tempSponsorIDs = "";
		foreach (string s in sponsorIDs.Split(','))
		{
			if (s != "")
			{
				tempSponsorIDs += "," + Convert.ToInt32(s) + ",-" + Convert.ToInt32(s);
			}
		}
		sponsorIDs = tempSponsorIDs;

        #region EXPORT
        #region init
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string def = "", syn = "";
        bool nextShouldBreak = true;
        int caseCounter = 0, recordCount = 0;
        string rowDelim = "\n";
        #endregion
        #region SPSS header
        nextShouldBreak = true;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
        syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysLoginDT)." + rowDelim;
        syn += "VARIABLE LABELS sysLoginDT 'Login datetime'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F4.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysMinutes)." + rowDelim;
        syn += "VARIABLE LABELS sysMinutes 'Minutes spent'." + rowDelim;
        nextShouldBreak = false;
        #endregion

		ArrayList al = new ArrayList();
		System.Text.StringBuilder output = new System.Text.StringBuilder();
        #region User base
        string SQL = "SELECT " +
            "ABS(si.UserID) AS S3, " +             // 0
            "s.DT, " +                  // 1
			"dbo.cf_sessionMinutes(s.DT, s.EndDT, s.AutoEnded), " +    // 2
			"si.Email AS S1, " +  // 3
			"u.UserID AS S2, " +
			"s.SessionID " +
            "FROM SponsorInvite si " +
            "INNER JOIN [User] u ON (u.UserID = ABS(si.UserID) OR si.Email = u.Email + 'DELETED' AND ABS(si.SponsorID) = u.SponsorID) " +
            "INNER JOIN [Session] s ON u.UserID = s.UserID " +
            "WHERE si.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
			"ORDER BY S1, S2 DESC, S3 DESC";
        SqlDataReader rs = Db.rs(SQL);
        while (rs.Read())
        {
			if (!al.Contains(rs.GetInt32(5)))
			{
				al.Add(rs.GetInt32(5));

				int userID = (rs.IsDBNull(0) ? (rs.IsDBNull(4) ? 0 : rs.GetInt32(4)) : rs.GetInt32(0));
				int minutesSpent = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
				SqlDataReader rs4 = Db.rs("SELECT e.Minutes FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID WHERE es.UserID = " + userID + " AND dbo.cf_yearMonthDay(es.DateTime) = '" + rs.GetDateTime(1).ToString("yyyy-MM-dd") + "'");
				while (rs4.Read())
				{
					minutesSpent += rs4.GetInt32(0);
				}
				rs4.Close();

				output.Append("" +
					(userID.ToString()).PadRight(5, ' ') +
					(rs.IsDBNull(1) ? "".PadRight(17, ' ') : rs.GetDateTime(1).ToString("dd-MMM-yyyy HH:mm")) +
					minutesSpent.ToString().PadRight(4, ' ') +
					rowDelim +
					"");
			}
        }
        rs.Close();
        #endregion

        string ret = "DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + "END DATA." + rowDelim + syn + rowDelim;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
        HttpContext.Current.Response.Write(ret);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportFirstInvite(string sponsorIDs)
    {
		string tempSponsorIDs = "";
		foreach (string s in sponsorIDs.Split(','))
		{
			if (s != "")
			{
				tempSponsorIDs += "," + Convert.ToInt32(s) + ",-" + Convert.ToInt32(s);
			}
		}
		sponsorIDs = tempSponsorIDs;

        #region EXPORT
        #region init
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string def = "", syn = "";
        bool nextShouldBreak = true;
        int caseCounter = 0, recordCount = 0;
        string rowDelim = "\n";
        #endregion
        #region SPSS header
        nextShouldBreak = true;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
        syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(DATETIME17.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysFirstInviteDT)." + rowDelim;
        syn += "VARIABLE LABELS sysFirstInviteDT 'First invite sent'." + rowDelim;
        nextShouldBreak = false;
        #endregion

        System.Text.StringBuilder output = new System.Text.StringBuilder();
        #region User base
        string SQL = "SELECT " +
            "ABS(si.UserID), " +             // 0
            "(SELECT MIN(six.Sent) FROM SponsorInvite six WHERE six.DepartmentID = ABS(si.DepartmentID)) " +  // 1
            "FROM SponsorInvite si " +
            "WHERE si.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
            "ORDER BY ABS(si.SponsorID), ABS(si.DepartmentID), ABS(si.UserID)";
        SqlDataReader rs = Db.rs(SQL);
        while (rs.Read())
        {
            output.Append("" +
                (rs.IsDBNull(0) ? "" : rs.GetInt32(0).ToString()).PadRight(5, ' ') +
                (rs.IsDBNull(1) ? "".PadRight(17, ' ') : rs.GetDateTime(1).ToString("dd-MMM-yyyy HH:mm")) +
                rowDelim +
                "");
        }
        rs.Close();
        #endregion

        string ret = "DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + "END DATA." + rowDelim + syn + rowDelim;

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
        HttpContext.Current.Response.Write(ret);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    public static void execExportViedoc(string sponsorIDs)
    {
        SqlDataReader rs;

        System.Text.StringBuilder output = new System.Text.StringBuilder();
        string delim = ",";
        output.AppendLine("" +
            "sponsor" + delim +
            "email" + delim +
            "gender");

        string SQL = "SELECT " +
            "s.ExternalID, " +          // 0
            "si.Email, " +
            "b.ValueInt " +
            "FROM [SponsorInvite] si " +
            "INNER JOIN Sponsor s ON si.SponsorID = s.SponsorID " +
            "LEFT OUTER JOIN [User] u ON si.UserID = u.UserID " +
            "LEFT OUTER JOIN [UserProfileBQ] b ON b.UserProfileID = u.UserProfileID AND b.BQID = 2 " +
            "WHERE si.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
            "ORDER BY s.ExternalID, si.Email";
        rs = Db.rs(SQL);
        while (rs.Read())
        {
            output.AppendLine((rs.IsDBNull(0) ? "" : rs.GetInt32(0).ToString()) + delim + rs.GetString(1).Replace(delim,"") + delim + (rs.IsDBNull(2) ? "" : rs.GetInt32(2).ToString()));
        }
        rs.Close();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
        HttpContext.Current.Response.Write(output.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    public static void execExport(string sponsorIDs, int rExportSurveyID, int rExportLangID)
    {
        execExport(sponsorIDs, rExportSurveyID, rExportLangID, false);
    }
    public static void execExport(string sponsorIDs, int rExportSurveyID, int rExportLangID, bool full)
    {
		string tempSponsorIDs = "";
		foreach (string s in sponsorIDs.Split(','))
		{
			if (s != "")
			{
				tempSponsorIDs += "," + Convert.ToInt32(s) + ",-" + Convert.ToInt32(s);
			}
		}
		sponsorIDs = tempSponsorIDs;

        #region EXPORT
        #region init
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        string def = "", syn = "";
        bool nextShouldBreak = true;
        int caseCounter = 0, recordCount = 0, ax = 0;
        string rowDelim = "\n";
        #endregion
        #region SPSS header
        nextShouldBreak = true;
        SqlDataReader rs3;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
        syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;

        if (full)
        {
            nextShouldBreak = true;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(A250)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEmail)." + rowDelim;
            syn += "VARIABLE LABELS sysEmail 'Email'." + rowDelim;
            nextShouldBreak = true;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F2.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserStatus)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserStatus 'User status'." + rowDelim;
            //syn += "VALUE LABELS sysUserStatus";
            //syn += " 0 'Active'";
            //syn += " 1 'Stopped, work related'";
            //syn += " 2 'Stopped, education leave'";
            //syn += " 14 'Stopped, parental leave'";
            //syn += " 24 'Stopped, sick leave'";
            //syn += " 34 'Stopped, do not want to participate'";
            //syn += " 44 'Stopped, no longer associated'";
            //syn += " 4 'Stopped, other reason'";
            //syn += " 5 'Stopped, unknown reason'";
            //syn += " 6 'Stopped, project completed'";
            //syn += "." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(DATETIME17.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserStatusDate)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserStatusDate 'User Status Date Time'." + rowDelim;
            //nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(DATETIME17.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysConsent)." + rowDelim;
            syn += "VARIABLE LABELS sysConsent 'Consent'." + rowDelim;
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F1.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysConsentYN)." + rowDelim;
            syn += "VARIABLE LABELS sysConsentYN 'Consent'." + rowDelim;
            syn += "VALUE LABELS sysConsentYN";
            syn += " 0 'No'";
            syn += " 1 'Yes'";
            syn += "." + rowDelim;
            nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserLogins)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserLogins 'User login count'." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserExercises)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserExercises 'User exercise count'." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserMinutes)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserMinutes 'User minutes spent'." + rowDelim;
            //nextShouldBreak = false;

            //caseCounter++;
            //def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            //    "V" + (caseCounter) + "(F4.0)";
            //syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserHours)." + rowDelim;
            //syn += "VARIABLE LABELS sysUserHours 'User hours spent'." + rowDelim;
            //nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F4.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysOrg)." + rowDelim;
            syn += "VARIABLE LABELS sysOrg 'Organisation'." + rowDelim;
            syn += "VALUE LABELS sysOrg";
            rs3 = Db.rs("SELECT SponsorID, Sponsor FROM Sponsor WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDept)." + rowDelim;
            syn += "VARIABLE LABELS sysDept 'Department'." + rowDelim;
            syn += "VALUE LABELS sysDept";
            rs3 = Db.rs("SELECT DepartmentID, Department FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptTree)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptTree 'Department tree'." + rowDelim;
            syn += "VALUE LABELS sysDeptTree";
            rs3 = Db.rs("SELECT DepartmentID, dbo.cf_DepartmentTree(DepartmentID,' > ') FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptShort)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptShort 'Department short name'." + rowDelim;
            syn += "VALUE LABELS sysDeptShort";
            rs3 = Db.rs("SELECT DepartmentID, DepartmentShort FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
            nextShouldBreak = false;

            ax = 0;
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F5.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptShortTree)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptShortTree 'Department short name tree'." + rowDelim;
            syn += "VALUE LABELS sysDeptShortTree";
            rs3 = Db.rs("SELECT DepartmentID, dbo.cf_DepartmentShortTree(DepartmentID,' > ') FROM Department WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
            while (rs3.Read())
            {
                string s = trunc(rs3.GetString(1), 115);
                ax += s.Length + 10;
                if (ax > 115)
                {
                    ax = s.Length + 10;
                    syn += rowDelim;
                }
                syn += " " + rs3.GetInt32(0) + " '" + s + "'";
            }
            rs3.Close();
            syn += "." + rowDelim;
		}
		nextShouldBreak = false;

		ax = 0;
        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F5.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysMeasure)." + rowDelim;
        syn += "VARIABLE LABELS sysMeasure 'Measure'." + rowDelim;
        syn += "VALUE LABELS sysMeasure";
        rs3 = Db.rs("SELECT SponsorExtendedSurveyID, Internal, RoundText FROM SponsorExtendedSurvey WHERE SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ")");
        while (rs3.Read())
        {
            string s = trunc((!rs3.IsDBNull(1) ? rs3.GetString(1) : "?") + (!rs3.IsDBNull(2) ? " " + rs3.GetString(2) : ""), 115);
            ax += s.Length + 10;
            if (ax > 115)
            {
                ax = s.Length + 10;
                syn += rowDelim;
            }
            syn += " " + rs3.GetInt32(0) + " '" + s + "'";
        }
        rs3.Close();
        syn += "." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F2.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysCase)." + rowDelim;
        syn += "VARIABLE LABELS sysCase 'User case counter'." + rowDelim;
        nextShouldBreak = false;

        //if (full)
        //{
            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F2.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysOrgCase)." + rowDelim;
            syn += "VARIABLE LABELS sysOrgCase 'Organization case counter'." + rowDelim;
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(F2.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysDeptCase)." + rowDelim;
            syn += "VARIABLE LABELS sysDeptCase 'Department case counter'." + rowDelim;
            nextShouldBreak = false;
        //}

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F6.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysYearMonth)." + rowDelim;
        syn += "VARIABLE LABELS sysYearMonth 'Year Month'." + rowDelim;
        nextShouldBreak = false;

        caseCounter++;
        def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
            "V" + (caseCounter) + "(F6.0)";
        syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysYearWeek)." + rowDelim;
        syn += "VARIABLE LABELS sysYearWeek 'Year Week'." + rowDelim;

        if (full)
        {
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(DATETIME17.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysStart)." + rowDelim;
            syn += "VARIABLE LABELS sysStart 'Start Date Time'." + rowDelim;
            nextShouldBreak = false;

            caseCounter++;
            def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                "V" + (caseCounter) + "(DATETIME17.0)";
            syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysEnd)." + rowDelim;
            syn += "VARIABLE LABELS sysEnd 'End Date Time'." + rowDelim;
        }
        nextShouldBreak = true;
        #endregion
        #region header and queries
        System.Text.StringBuilder querySelect = new System.Text.StringBuilder();
        System.Text.StringBuilder queryJoin = new System.Text.StringBuilder();
        string varTypes = "", varAttrs = "", varPositions = "", varBreaks = "", exportSurveyIDs = rExportSurveyID.ToString();
        if (rExportSurveyID == 0)
        {
            exportSurveyIDs = "0,";
            rs3 = Db.rs("SELECT pr.SurveyID " +
                "FROM SponsorExtendedSurvey ses " +
                "INNER JOIN eForm..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID " +
                "WHERE ses.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
                "UNION " +
                "SELECT pr.ExtendedSurveyID " +
                "FROM SponsorExtendedSurvey ses " +
                "INNER JOIN eForm..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID " +
                "WHERE pr.ExtendedSurveyID IS NOT NULL AND ses.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") ");
            while (rs3.Read())
            {
                if (exportSurveyIDs.IndexOf("," + rs3.GetInt32(0) + ",") < 0)
                {
                    exportSurveyIDs += rs3.GetInt32(0) + ",";
                }
            }
            rs3.Close();
            exportSurveyIDs += "0";
        }

        ArrayList vars = new ArrayList();
        int varCount = 0, queries = 1, varPerRecord = 0, colsPerRecord = 0, varPos = 0, queryDivide = 20;
        string SQL = "SELECT DISTINCT " +
            "q.QuestionID, " +			// 0
            "qo.OptionID, " +			// 1
            "o.OptionType, " +			// 2
            "q.Variablename, " +		// 3
            "o.Variablename, " +		// 4
            "CAST(ql.Question AS NVARCHAR(MAX)), " +			// 5
            (rExportSurveyID != 0 ? 
            "(SELECT COUNT(*) FROM [SurveyQuestionOption] x WHERE x.SurveyQuestionID = sq.SurveyQuestionID), "	// 6
            :
            "(SELECT COUNT(*) FROM [QuestionOption] x WHERE x.QuestionID = q.QuestionID), "	// 6
            ) +
            "(SELECT COUNT(*) FROM [OptionComponents] x WHERE x.OptionID = o.OptionID) " +							// 7
            //"sq.SortOrder, " +			// 8
            //"sqo.SortOrder " +			// 9
            "FROM SurveyQuestion sq " +
            "INNER JOIN Question q ON sq.QuestionID = q.QuestionID " +
            "INNER JOIN QuestionLang ql ON q.QuestionID = ql.QuestionID " +
            (rExportSurveyID != 0 ? 
            "INNER JOIN SurveyQuestionOption sqo ON sq.SurveyQuestionID = sqo.SurveyQuestionID " +
            "INNER JOIN QuestionOption qo ON sqo.QuestionOptionID = qo.QuestionOptionID "
            :
            "INNER JOIN QuestionOption qo ON q.QuestionID = qo.QuestionID "
            ) +
            "INNER JOIN [Option] o ON qo.OptionID = o.OptionID " +
            "WHERE sq.SurveyID IN (" + exportSurveyIDs + ") " +
            "AND ql.LangID = " + rExportLangID + " " +
            (rExportSurveyID != 0 ? "ORDER BY sq.SortOrder, sqo.SortOrder" : "ORDER BY q.Variablename, o.Variablename");
        rs3 = Db.rs(SQL, "eFormSqlConnection");
        while (rs3.Read())
        {
            #region Header
            string var = (rs3.IsDBNull(3) || rs3.GetString(3).Trim() == "" ? "Q" + rs3.GetInt32(0).ToString() : rs3.GetString(3));
            string desc = (rs3.IsDBNull(5) || rs3.GetString(5).Trim() == "" ? var : rs3.GetString(5)).Replace("'","");
            if (rs3.GetInt32(6) > 1)
            {
                var += (rs3.IsDBNull(4) || rs3.GetString(4).Trim() == "" ? "A" + rs3.GetInt32(1).ToString() : rs3.GetString(4));
                desc += var;
            }
            desc = trunc(RemoveHTMLTags(desc), 115);
            var = var.Replace(HttpContext.Current.Server.HtmlDecode("&nbsp;"),"_").Replace(" ", "_").Replace("-", "_").Replace("/", "_");
            
            int vx = 0;
            while (vars.Contains(var + (vx == 0 ? "" : "_" + vx))) vx++;
            if (vx > 0) var += "_" + vx;
            vars.Add(var);

            syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + ")." + rowDelim;
            syn += "VARIABLE LABELS " + var + " '" + desc + "'." + rowDelim;

            string defDelim = " ";
            if (varCount == 0 || varPerRecord > 15 || colsPerRecord > 200 || rs3.GetInt32(2) == 2)
            {
                recordCount++;
                varPerRecord = 0;
                colsPerRecord = 0;
                defDelim = rowDelim + "/" + recordCount + " ";
                varBreaks += (varBreaks != "" ? "," : "") + "1";
            }
            else
            {
                varBreaks += (varBreaks != "" ? "," : "") + "0";
            }

            switch (rs3.GetInt32(2))
            {
                case 0:
                    bool zone = rs3.GetInt32(2) == 9 && rs3.GetInt32(7) > 2;
                    syn += "VALUE LABELS " + var + (zone ? "Zone" : "");
                    int cx = 0;
                    ax = 0;
                    SqlDataReader rs4 = Db.rs("SELECT " +
                        "ocl.Text, " +
                        "oc.ExportValue " +
                        "FROM OptionComponents oc " +
                        "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + rExportLangID + " " +
                        "WHERE oc.OptionID = " + rs3.GetInt32(1) + " " +
                        "ORDER BY oc.SortOrder", "eFormSqlConnection");
                    while (rs4.Read())
                    {
                        string s = trunc(RemoveHTMLTags(HttpContext.Current.Server.HtmlDecode(rs4.GetString(0))), 110).Replace("'","");
                        ax += s.Length + 10;
                        if (ax > 115)
                        {
                            ax = s.Length + 10;
                            syn += rowDelim;
                        }
                        syn += " " + (zone ? (cx++) : rs4.GetInt32(1)) + " '" + s + "'";
                    }
                    rs4.Close();
                    syn += "." + rowDelim;
                    break;
                case 1:
                    varPos = 3;
                    colsPerRecord += varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + varPos;
                    def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";
                    goto case 0;
                case 2:
                    varPos = (Math.Min(250, Db.getInt32("SELECT MAX(LEN(CAST(ValueText AS VARCHAR(255)))) FROM AnswerValue WHERE QuestionID = " + rs3.GetInt32(0) + " AND OptionID = " + rs3.GetInt32(1), "eFormSqlConnection") + 16));
                    colsPerRecord += 250;//varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + 250;//varPos;
                    def += defDelim + "V" + (caseCounter) + "(A" + varPos + ")";
                    break;
                case 3:
                    goto case 1;
                case 4:
                    varPos = 8;
                    colsPerRecord += varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + varPos;
                    def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".2)";
                    break;
                case 9:
                    varPos = 3;
                    colsPerRecord += varPos;
                    varPerRecord++;
                    varPositions += (varPositions != "" ? "," : "") + varPos;

                    if (rs3.GetInt32(7) > 2)
                    {
                        def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";

                        defDelim = " ";
                        colsPerRecord += varPos;
                        varPerRecord++;

                        syn += "RENAME VARIABLES (V" + (++caseCounter) + "=" + var + "Zone)." + rowDelim;
                        syn += "VARIABLE LABELS " + var + "Zone '" + desc + "'." + rowDelim;
                    }

                    def += defDelim + "V" + (caseCounter) + "(F" + varPos + ".0)";
                    goto case 0;
            }
            #endregion

            if (varCount != 0 && varCount % queryDivide == 0)
            {
                querySelect.Append("¤");
                queryJoin.Append("¤");
                queries++;
            }
            varCount++;
            switch (rs3.GetInt32(2))
            {
                case 0:
                    #region Link
                    querySelect.Append(",op" + varCount + ".ExportValue AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL " +
                        "LEFT OUTER JOIN OptionComponents op" + varCount + " ON " +
                        "op" + varCount + ".OptionID = av" + varCount + ".OptionID " +
                        "AND " +
                        "op" + varCount + ".OptionComponentID = av" + varCount + ".ValueInt ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "1";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
                case 1:
                    goto case 0;
                case 2:
                    #region Freetext
                    querySelect.Append(",av" + varCount + ".ValueText AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "2";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
                case 3:
                    goto case 0;
                case 4:
                    #region Decimal
                    querySelect.Append(",av" + varCount + ".ValueDecimal AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "4";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
                case 9:
                    #region VAS
                    querySelect.Append(",av" + varCount + ".ValueInt AS var" + varCount + " ");
                    queryJoin.Append("LEFT OUTER JOIN AnswerValue av" + varCount + " ON " +
                        "av" + varCount + ".AnswerID = a.AnswerID " +
                        "AND " +
                        "av" + varCount + ".QuestionID = " + rs3.GetInt32(0) + " " +
                        "AND " +
                        "av" + varCount + ".OptionID = " + rs3.GetInt32(1) + " " +
                        "AND " +
                        "av" + varCount + ".DeletedSessionID IS NULL ");
                    #endregion
                    varTypes += (varTypes != "" ? "," : "") + "9";
                    varAttrs += (varAttrs != "" ? "," : "") + rs3.GetInt32(7);
                    break;
            }
        }
        rs3.Close();
        string[] varType = varTypes.Split(','), varAttr = varAttrs.Split(','), varPosition = varPositions.Split(','), varBreak = varBreaks.Split(',');
        #endregion

        ArrayList queryResult = new ArrayList();
		//object[] queryResult = new object[queries];
        System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.CurrentCulture;
        int caseCount = 0;
        for (int q = 0; q < queries; q++)
        {
            System.Text.StringBuilder output = new System.Text.StringBuilder();

			int userID = 0, userCaseCounter = 0; ArrayList al = new ArrayList();
            #region User base
            SQL = "SELECT " +
                "u.UserID, " +                  // 0
                "ABS(si.SponsorID), " +
                "ABS(si.DepartmentID), " +
                "a.StartDT, " +
                "a.EndDT, " +
                "s.SponsorExtendedSurveyID, " + // 5
                "(SELECT COUNT(*) FROM healthwatch..SponsorExtendedSurvey q WHERE ABS(si.SponsorID) = q.SponsorID AND q.SponsorExtendedSurveyID <= w.SponsorExtendedSurveyID) AS CX, " +
                "si.StoppedReason, " +
                "si.Stopped, " +
                "si.Consent, " +
                "(SELECT COUNT(*) FROM healthwatch..SponsorExtendedSurvey q LEFT OUTER JOIN healthwatch..SponsorExtendedSurveyDepartment qq ON q.SponsorExtendedSurveyID = qq.SponsorExtendedSurveyID AND qq.DepartmentID = ABS(si.DepartmentID) WHERE qq.Hide IS NULL AND ABS(si.SponsorID) = q.SponsorID AND q.SponsorExtendedSurveyID <= w.SponsorExtendedSurveyID) AS CX2, " +
                "si.Email, " +                   // 11
				"a.AnswerID " +
                querySelect.ToString().Split('¤')[q] +
                "FROM healthwatch..[User] u " +
				"INNER JOIN healthwatch..SponsorInvite si ON (u.UserID = ABS(si.UserID) OR si.Email = u.Email + 'DELETED' AND ABS(si.SponsorID) = u.SponsorID) " +
                "INNER JOIN healthwatch..UserSponsorExtendedSurvey w ON u.UserID = w.UserID " +
				"INNER JOIN healthwatch..SponsorExtendedSurvey s ON w.SponsorExtendedSurveyID = s.SponsorExtendedSurveyID AND s.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
                "INNER JOIN Answer a ON w.AnswerID = a.AnswerID " +
                queryJoin.ToString().Split('¤')[q] +
                "WHERE a.EndDT IS NOT NULL AND si.SponsorID IN (0" + sponsorIDs.Replace("'", "").Replace(" ", "") + ") " +
                "ORDER BY u.UserID, w.AnswerID";
			if (HttpContext.Current.Request.QueryString["DEBUG"] != null)
			{
				HttpContext.Current.Response.Write(SQL + "\r\n\r\n");
			}
			else
			{
				SqlDataReader rs = Db.rs(SQL, "eFormSqlConnection");
				while (rs.Read())
				{
					if (!al.Contains(rs.GetInt32(12)))
					{
						al.Add(rs.GetInt32(12));

						if (userID != 0)
						{
							output.Append("¤");
						}

						if (userID == 0 || userID != rs.GetInt32(0))
						{
							userCaseCounter = 0;
							userID = rs.GetInt32(0);
						}
						userCaseCounter++;

						if (q == 0)
						{
							caseCount++;

							output.Append("" +
								userID.ToString().PadRight(5, ' ') +
								(full ?
								rowDelim +
								(rs.IsDBNull(11) ? "" : rs.GetString(11)) +
								rowDelim +
								//(rs.IsDBNull(7) ? 0 : rs.GetInt32(7)).ToString().PadRight(2, ' ') +
								//(rs.IsDBNull(8) ? "".PadRight(17, ' ') : rs.GetDateTime(8).ToString("dd-MMM-yyyy HH:mm")) +
								(rs.IsDBNull(9) ? "".PadRight(17, ' ') : rs.GetDateTime(9).ToString("dd-MMM-yyyy HH:mm")) +
								(rs.IsDBNull(9) ? 0 : 1) +
								"" : ""));
							//SqlDataReader rs2 = Db.rs("SELECT COUNT(*) FROM Session WHERE UserID = " + userID);
							//if (rs2.Read())
							//{
							//    output.Append(rs2.GetInt32(0).ToString().PadRight(4, ' '));
							//}
							//else
							//{
							//    output.Append(string.Empty.PadRight(4, ' '));
							//}
							//rs2.Close(); 
							//rs2 = Db.rs("SELECT COUNT(*) FROM ExerciseStats WHERE UserID = " + userID);
							//if (rs2.Read())
							//{
							//    output.Append(rs2.GetInt32(0).ToString().PadRight(4, ' '));
							//}
							//else
							//{
							//    output.Append(string.Empty.PadRight(4, ' '));
							//}
							//rs2.Close();
							//System.Collections.ArrayList dt = new ArrayList();
							//int minutes = 0, hours = 0;
							//rs2 = Db.rs("SELECT " +
							//    "DT, " +
							//    "EndDT, " +
							//    "AutoEnded, " +
							//    "dbo.cf_sessionMinutes(DT, EndDT, AutoEnded) " +
							//    "FROM Session " +
							//    "WHERE UserID = " + userID + " " +
							//    "");
							//while (rs2.Read())
							//{
							//    int minutesThisSession = rs2.GetInt32(3);
							//    if (!dt.Contains(rs2.GetDateTime(0).ToString("yyyy-MM-dd")))
							//    {
							//        SqlDataReader rs4 = Db.rs("SELECT e.Minutes FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID WHERE es.UserID = " + userID + " AND dbo.cf_yearMonthDay(es.DateTime) = '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd") + "'");
							//        while (rs4.Read())
							//        {
							//            minutesThisSession += rs4.GetInt32(0);
							//        }
							//        rs4.Close();
							//    }

							//    minutes += minutesThisSession;
							//    hours += (int)Math.Ceiling((double)minutesThisSession / (double)60);
							//}
							//rs2.Close();
							output.Append("" +
								(full ?
								//minutes.ToString().PadRight(4, ' ') +
								//hours.ToString().PadRight(4, ' ') +
								rs.GetInt32(1).ToString().PadRight(4, ' ') +
								rs.GetInt32(2).ToString().PadRight(5, ' ') +
								rs.GetInt32(2).ToString().PadRight(5, ' ') +
								rs.GetInt32(2).ToString().PadRight(5, ' ') +
								rs.GetInt32(2).ToString().PadRight(5, ' ') +
								"" : "") +
								rs.GetInt32(5).ToString().PadRight(5, ' ') +
								userCaseCounter.ToString().PadRight(2, ' ') +
								//(full ?
								rs.GetInt32(6).ToString().PadRight(2, ' ') +
								rs.GetInt32(10).ToString().PadRight(2, ' ') +
								//"" : "") +
								rs.GetDateTime(4).ToString("yyyyMM") +
								rs.GetDateTime(4).ToString("yyyy") + cul.Calendar.GetWeekOfYear(rs.GetDateTime(4), System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday).ToString().PadLeft(2, '0') +
								(full ?
								(rs.IsDBNull(3) ? "".PadRight(17, ' ') : rs.GetDateTime(3).ToString("dd-MMM-yyyy HH:mm")) +
								(rs.IsDBNull(4) ? "".PadRight(17, ' ') : rs.GetDateTime(4).ToString("dd-MMM-yyyy HH:mm")) +
								"" : ""));
						}

						for (int i = q * queryDivide; i < Math.Min((q + 1) * queryDivide, varCount); i++)
						{
							if (varBreak[i] == "1")
							{
								output.Append(rowDelim);
							}

							int pos = i + 13 - q * queryDivide;

							switch (Convert.ToInt32(varType[i]))
							{
								case 1:
									output.Append((rs.IsDBNull(pos) ? "" : rs.GetInt32(pos).ToString()).PadRight(Convert.ToInt32(varPosition[i]), ' '));
									break;
								case 2:
									output.Append(trunc((rs.IsDBNull(pos) ? "" : rs.GetString(pos)), 230));
									break;
								case 3:
									goto case 1;
								case 4:
									output.Append((rs.IsDBNull(pos) ? "" : rs.GetDecimal(pos).ToString("N2").Replace(".", "").Replace(",", "")).PadRight(Convert.ToInt32(varPosition[i]), ' '));
									break;
								case 9:
									output.Append((rs.IsDBNull(pos) ? "" : rs.GetInt32(pos).ToString()).PadRight(Convert.ToInt32(varPosition[i]), ' '));
									if (Convert.ToInt32(varAttr[i]) > 2)
									{
										if (!rs.IsDBNull(pos))
										{
											int cx = 0;
											for (int ii = 0; ii < Convert.ToInt32(varAttr[i]); ii++)
											{
												if (rs.GetInt32(pos) >= 100 / Convert.ToInt32(varAttr[i]) * ii)
												{
													cx = ii;
												}
											}
											output.Append(cx.ToString().PadRight(Convert.ToInt32(varPosition[i]), ' '));
										}
										else
										{
											output.Append("".PadRight(Convert.ToInt32(varPosition[i]), ' '));
										}
									}
									break;
							}
						}
					}
				}
				rs.Close();
			}
            #endregion

            queryResult.Add(output.ToString().Split('¤'));
			//queryResult[q] = output.ToString().Split('¤');
        }
		if (HttpContext.Current.Request.QueryString["DEBUG"] != null)
		{
			HttpContext.Current.Response.End();
		}

        System.Text.StringBuilder mergedOutput = new System.Text.StringBuilder();
        for (int p = 0; p < caseCount; p++)
        {
            for (int m = 0; m < queries; m++)
            {
                if (queryResult.Count > m && ((string[])queryResult[m]).Length > p)
                {
                    mergedOutput.Append(((string[])queryResult[m])[p]);
                }
            }
            mergedOutput.Append(rowDelim);
        }

        string fname = HttpContext.Current.Server.MapPath("tmp/" + DateTime.Now.Ticks + ".sps");
        System.IO.FileStream fs = new System.IO.FileStream(fname, System.IO.FileMode.Create);
        System.IO.StreamWriter f = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
        f.Write("DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + mergedOutput.ToString() + "END DATA." + rowDelim + syn + rowDelim);
        f.Close();
        fs.Close();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
        HttpContext.Current.Response.WriteFile(fname);
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        #endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataReader rs;

        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.QueryString["SIT"] != null)
            {
                #region Parse SponsorInviteTest
                rs = Db.rs("SELECT sit.SponsorInviteTestID, sit.TestData, sit.TestTypeID " +
                    "FROM SponsorInviteTest sit INNER JOIN SponsorInvite si ON si.SponsorInviteID = sit.SponsorInviteID");
                while (rs.Read())
                {
                    if (rs.GetInt32(2) == 1 && Db.getInt32("SELECT COUNT(*) FROM SponsorInviteTestMeasureComponent WHERE SponsorInviteTestID = " + rs.GetInt32(0)) == 0)
                    {
                        try
                        {
                            int measureComponentID = 0;
                            using (System.Xml.XmlReader r = System.Xml.XmlReader.Create(new System.IO.StringReader("<?xml version=\"1.0\"?>" + rs.GetString(1))))
                            {
                                while (r.Read())
                                {
                                    switch (r.NodeType)
                                    {
                                        case System.Xml.XmlNodeType.Element:
                                            {
                                                switch (r.Name)
                                                {
                                                    case "Intensity":
                                                        {
                                                            measureComponentID = 80;
                                                            break;
                                                        }
                                                    case "Left":    // This will be dupe so check parent first
                                                        {
                                                            measureComponentID = 81;
                                                            break;
                                                        }
                                                    case "Right":    // This will be dupe so check parent first
                                                        {
                                                            measureComponentID = 82;
                                                            break;
                                                        }
                                                    case "Noise":
                                                        {
                                                            measureComponentID = 83;
                                                            break;
                                                        }
                                                }
                                                HttpContext.Current.Response.Write("<br/><b>" + r.Name + "</b> ");
                                                break;
                                            }
                                        case System.Xml.XmlNodeType.Text:
                                            {
                                                Db.exec("INSERT INTO SponsorInviteTestMeasureComponent (SponsorInviteTestID,MeasureComponentID,ValDec) VALUES (" + rs.GetInt32(0) + "," + measureComponentID + "," + r.Value + ")");
                                                HttpContext.Current.Response.Write(r.Value);
                                                break;
                                            }
                                    }
                                }
                                r.Close();
                            }
                        }
                        catch (Exception ex) { HttpContext.Current.Response.Write(rs.GetInt32(2) + ":" + ex.Message); }
                    }
                }
                rs.Close();
                #endregion
            }
            else if (HttpContext.Current.Request.QueryString["ExportSITMC"] != null)
            {
                #region EXPORT
                #region init
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                string def = "", syn = "";
                bool nextShouldBreak = true;
                int caseCounter = 0, recordCount = 0, rExportLangID = 2;
                string rowDelim = "\n";
                #endregion
                #region SPSS header
                nextShouldBreak = true;

                caseCounter++;
                def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                    "V" + (caseCounter) + "(F5.0)";
                syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysUserID)." + rowDelim;
                syn += "VARIABLE LABELS sysUserID 'User ID'." + rowDelim;
                nextShouldBreak = false;

                caseCounter++;
                def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                    "V" + (caseCounter) + "(F5.0)";
                syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysSponsorInviteID)." + rowDelim;
                syn += "VARIABLE LABELS sysSponsorInviteID 'Sponsor invite ID'." + rowDelim;
                nextShouldBreak = false;

                #region bq
                rs = Db.rs("SELECT " +
                    "DISTINCT " +
                    "bq.BQID, " +
                    "bq.Variable, " +
                    "bq.Type, " +
                    "bql.BQ " +
                    "FROM SponsorBQ sbq " +
                    "INNER JOIN BQ bq ON sbq.BQID = bq.BQID " +
                    "INNER JOIN BQLang bql ON bq.BQID = bql.BQID AND bql.LangID = " + rExportLangID + " " +
                    "WHERE sbq.Forced = 1 AND sbq.SponsorID = 1 " +
                    "ORDER BY bq.BQID");
                while (rs.Read())
                {
                    int ax = 0;
                    caseCounter++;
                    def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                        "V" + (caseCounter);

                    switch (rs.GetInt32(2))
                    {
                        case 1:
                        case 4:
                        case 7:
                            def += "(F5.0)"; break;
                        case 3:
                            def += "(DATE11)"; break;
                        //case 2:
                    }

                    syn += "RENAME VARIABLES (V" + (caseCounter) + "=" + (rs.IsDBNull(1) || rs.GetString(1).Trim() == "" ? "BQ" + rs.GetInt32(0) : rs.GetString(1)) + ")." + rowDelim;
                    syn += "VARIABLE LABELS " + (rs.IsDBNull(1) || rs.GetString(1).Trim() == "" ? "BQ" + rs.GetInt32(0) : rs.GetString(1)) + " '" + rs.GetString(3) + "'." + rowDelim;
                    switch (rs.GetInt32(2))
                    {
                        case 1:
                        case 7:
                            syn += "VALUE LABELS " + (rs.IsDBNull(1) || rs.GetString(1).Trim() == "" ? "BQ" + rs.GetInt32(0) : rs.GetString(1)) + "";
                            SqlDataReader rs3 = Db.rs("SELECT " +
                                "ba.BAID, " +
                                "bal.BA " +
                                "FROM BA ba " +
                                "INNER JOIN BALang bal ON ba.BAID = bal.BAID AND bal.LangID = " + rExportLangID + " " +
                                "WHERE ba.BQID = " + rs.GetInt32(0) + " " +
                                "ORDER BY ba.SortOrder");
                            while (rs3.Read())
                            {
                                ax += rs3.GetString(1).Length;
                                if (ax > 115)
                                {
                                    ax = 0;
                                    syn += rowDelim;
                                }
                                syn += " " + rs3.GetInt32(0) + " '" + trunc(rs3.GetString(1), 115) + "'";
                            }
                            rs3.Close();
                            syn += "." + rowDelim;
                            break;
                    }
                    nextShouldBreak = false;
                }
                rs.Close();
                #endregion

                caseCounter++;
                def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                    "V" + (caseCounter) + "(DATETIME17.0)";
                syn += "RENAME VARIABLES (V" + (caseCounter) + "=sysTestDT)." + rowDelim;
                syn += "VARIABLE LABELS sysTestDT 'Test datetime'." + rowDelim;
                nextShouldBreak = false;

                rs = Db.rs("SELECT DISTINCT mc.MeasureComponent, mc.MeasureComponentID FROM SponsorInviteTestMeasureComponent sitmc INNER JOIN MeasureComponent mc ON sitmc.MeasureComponentID = mc.MeasureComponentID ORDER BY mc.MeasureComponentID, mc.MeasureComponent");
                while (rs.Read())
                {
                    caseCounter++;
                    def += (nextShouldBreak ? rowDelim + "/" + (++recordCount) : "") + " " +
                        "V" + (caseCounter) + "(F5.0)";
                    syn += "RENAME VARIABLES (V" + (caseCounter) + "=" + rs.GetString(0) + ")." + rowDelim;
                    syn += "VARIABLE LABELS " + rs.GetString(0) + " '" + rs.GetString(0) + "'." + rowDelim;
                    nextShouldBreak = false;
                }
                rs.Close();
                #endregion

                System.Text.StringBuilder output = new System.Text.StringBuilder();
                #region User base
                string SQL = "SELECT " +
                    "si.UserID, " +             // 0
                    "si.SponsorInviteID, " +                  // 1
                    "sit.DT, " +
                    "sit.SponsorInviteTestID " +
                    "FROM SponsorInvite si " +
                    "INNER JOIN SponsorInviteTest sit ON si.SponsorInviteID = sit.SponsorInviteID " +
                    "WHERE sit.TestTypeID = 1";
                rs = Db.rs(SQL);
                while (rs.Read())
                {
                    output.Append("" +
                        (rs.IsDBNull(0) ? "" : rs.GetInt32(0).ToString()).PadRight(5, ' ') +
                        (rs.IsDBNull(1) ? "" : rs.GetInt32(1).ToString()).PadRight(5, ' ') +
                        "");

                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "DISTINCT " +
                        "bq.BQID, bq.Type " +
                        "FROM SponsorBQ sbq " +
                        "INNER JOIN BQ bq ON sbq.BQID = bq.BQID " +
                        "INNER JOIN BQLang bql ON bq.BQID = bql.BQID AND bql.LangID = " + rExportLangID + " " +
                        "WHERE sbq.Forced = 1 AND sbq.SponsorID = 1 " +
                        "ORDER BY bq.BQID");
                    while (rs2.Read())
                    {
                        SqlDataReader rs3 = Db.rs("SELECT " +
                            "ISNULL(w.BAID,b.ValueInt), " +
                            "ISNULL(w.ValueInt,b.ValueInt), " +
                            "ISNULL(w.ValueText,b.ValueText), " +
                            "ISNULL(w.ValueDate,b.ValueDate) " +
                            "FROM SponsorInvite si " +
                            "LEFT OUTER JOIN [User] u ON u.UserID = si.UserID AND si.SponsorID = u.SponsorID " +
                            "LEFT OUTER JOIN UserProfileBQ b ON b.UserProfileID = u.UserProfileID AND b.BQID = " + rs2.GetInt32(0) + " " +
                            "LEFT OUTER JOIN SponsorInviteBQ w ON si.SponsorInviteID = w.SponsorInviteID AND w.BQID = " + rs2.GetInt32(0) + " " +
                            "WHERE si.SponsorInviteID = " + rs.GetInt32(1));
                        if (rs3.Read())
                        {
                            switch (rs2.GetInt32(1))
                            {
                                case 1:
                                case 7:
                                    output.Append((rs3.IsDBNull(0) ? "" : rs3.GetInt32(0).ToString()).PadRight(5, ' ')); break;
                                case 4:
                                    output.Append((rs3.IsDBNull(1) ? "" : rs3.GetInt32(1).ToString()).PadRight(5, ' ')); break;
                                case 3:
                                    output.Append((rs3.IsDBNull(3) ? "" : rs3.GetDateTime(3).ToString("dd-MMM-yyyy")).PadRight(11, ' ')); break;
                                //case 2:
                            }
                        }
                        else
                        {
                            switch (rs2.GetInt32(1))
                            {
                                case 1:
                                case 7:
                                    output.Append(string.Empty.PadRight(5, ' ')); break;
                                case 4:
                                    output.Append(string.Empty.PadRight(5, ' ')); break;
                                case 3:
                                    output.Append(string.Empty.PadRight(11, ' ')); break;
                                //case 2:
                            }
                        }
                        rs3.Close();
                    }
                    rs2.Close();

                    output.Append("" +
                        (rs.IsDBNull(2) ? "".PadRight(17, ' ') : rs.GetDateTime(2).ToString("dd-MMM-yyyy HH:mm")) +
                        "");

                    rs2 = Db.rs("SELECT DISTINCT mc.MeasureComponentID FROM SponsorInviteTestMeasureComponent sitmc INNER JOIN MeasureComponent mc ON sitmc.MeasureComponentID = mc.MeasureComponentID ORDER BY mc.MeasureComponentID");
                    while (rs2.Read())
                    {
                        SqlDataReader rs3 = Db.rs("SELECT sitmc.ValDec " +
                        "FROM SponsorInviteTestMeasureComponent sitmc " +
                        "WHERE sitmc.SponsorInviteTestID = " + rs.GetInt32(3) + " AND sitmc.MeasureComponentID = " + rs2.GetInt32(0));
                        if (rs3.Read())
                        {
                            output.Append((rs3.IsDBNull(0) ? "" : Convert.ToInt32(rs3.GetDecimal(0)).ToString()).PadRight(5, ' '));
                        }
                        rs3.Close();
                    }
                    rs2.Close();

                    output.Append(rowDelim);
                }
                rs.Close();
                #endregion

                string ret = "DATA LIST FIXED RECORDS=" + recordCount + def + "." + rowDelim + "BEGIN DATA" + rowDelim + output.ToString() + "END DATA." + rowDelim + syn + rowDelim;

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.ContentType = "text/x-spss-syntax";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".sps");
                HttpContext.Current.Response.Write(ret);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();

                #endregion

            }
            else if (HttpContext.Current.Request.QueryString["ProjectRoundID"] != null)
            {
                execExportExtendedHypergeneEform(Convert.ToInt32(HttpContext.Current.Request.QueryString["ProjectRoundID"].ToString()));
            }
            else if (HttpContext.Current.Request.QueryString["SponsorExtendedSurveyID"] != null)
            {
                execExportExtendedHypergene(Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorExtendedSurveyID"].ToString()));
            }
            else if (HttpContext.Current.Request.QueryString["SponsorProjectRoundUnitID"] != null)
            {
                execExportHypergene(Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorProjectRoundUnitID"].ToString()));
            }
            else if (HttpContext.Current.Request.QueryString["ExportExtendedSponsorID"] != null)
            {
                execExport(HttpContext.Current.Request.QueryString["ExportExtendedSponsorID"].ToString(), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportExtendedSurveyID"].ToString()), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportExtendedLangID"].ToString()));
            }
            else if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null && HttpContext.Current.Request.QueryString["Type"] != null && HttpContext.Current.Request.QueryString["Type"] == "5")
            {
                execExportRepeated(HttpContext.Current.Request.QueryString["ExportSponsorID"].ToString(), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportSurveyID"].ToString()), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportLangID"].ToString()));
            }
            else if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null && HttpContext.Current.Request.QueryString["Type"] != null && HttpContext.Current.Request.QueryString["Type"] == "7")
            {
                execExportRepeatedOneQuestionPerRow(HttpContext.Current.Request.QueryString["ExportSponsorID"].ToString(), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportSurveyID"].ToString()), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportLangID"].ToString()));
            }
            else if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null && HttpContext.Current.Request.QueryString["Type"] != null && HttpContext.Current.Request.QueryString["Type"] == "6")
            {
				execExportMetadata(HttpContext.Current.Request.QueryString["ExportSponsorID"].ToString(), Convert.ToInt32(HttpContext.Current.Request.QueryString["ExportLangID"].ToString()), (HttpContext.Current.Request.QueryString["ToDT"] != null ? HttpContext.Current.Request.QueryString["ToDT"].ToString() : ""));
            }
            else if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null && HttpContext.Current.Request.QueryString["Type"] != null && HttpContext.Current.Request.QueryString["Type"] == "12")
            {
                execExportLogin(HttpContext.Current.Request.QueryString["ExportSponsorID"].ToString());
            }
            else if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null && HttpContext.Current.Request.QueryString["Type"] != null && HttpContext.Current.Request.QueryString["Type"] == "13")
            {
                execExportFirstInvite(HttpContext.Current.Request.QueryString["ExportSponsorID"].ToString());
            }
            else if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null && HttpContext.Current.Request.QueryString["Type"] != null && HttpContext.Current.Request.QueryString["Type"] == "11")
            {
                execExportViedoc(HttpContext.Current.Request.QueryString["ExportSponsorID"].ToString());
            }
            if (HttpContext.Current.Request.QueryString["CONNECT"] != null)
            {
                #region Connect
                int sponsorID = Convert.ToInt32(HttpContext.Current.Request.QueryString["CONNECT"]);

                rs = Db.rs("SELECT SponsorInviteID FROM SponsorInvite WHERE SponsorID = " + sponsorID);
                while (rs.Read())
                {
                    Db.connectSPI(rs.GetInt32(0));
                }
                rs.Close();
                #endregion
            }
            if (HttpContext.Current.Request.QueryString["COPY"] != null)
            {
                #region Copy
                int sponsorID = 0, PRUID = 0; string sponsorKey = "", oldSponsorKey = "";

                rs = Db.rs("SELECT ProjectRoundUnitID FROM SystemSettings");
                if (rs.Read())
                {
                    PRUID = rs.GetInt32(0);
                }
                rs.Close();

                rs = Db.rs("SELECT " +
                  "[Sponsor]" +                // 0
                  ",[Application]" +
                  ",[ProjectRoundUnitID]" +
                  ",[InviteTxt]" +
                  ",[InviteReminderTxt]" +
                  ",[LoginTxt]" +                    // 5
                  ",[InviteSubject]" +
                  ",[InviteReminderSubject]" +
                  ",[LoginSubject]" +
                  ",[LoginDays]" +
                  ",[LoginWeekday]" +                // 10
                  ",[LID]" +
                  ",[TreatmentOffer]" +
                  ",[TreatmentOfferText]" +
                  ",[TreatmentOfferEmail]" +
                  ",[TreatmentOfferIfNeededText]" +  // 15
                  ",[TreatmentOfferBQ]" +
                  ",[TreatmentOfferBQfn]" +
                  ",[TreatmentOfferBQmorethan] " +
                  ",CAST(SponsorKey AS VARCHAR(64)) " +
                  "FROM [Sponsor] WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["COPY"]));
                if (rs.Read())
                {
                    PRUID = Db.createProjectRoundUnit(PRUID, rs.GetString(0).Replace("'", "''") + " (copy)", 0, 0, 0);
                    oldSponsorKey = rs.GetString(19).Substring(0, 8);

                    Db.exec("INSERT INTO [Sponsor] (" +
                        "[Sponsor]" +
                        ",[Application]" +
                        ",[ProjectRoundUnitID]" +
                        ",[InviteTxt]" +
                        ",[InviteReminderTxt]" +
                        ",[LoginTxt]" +
                        ",[InviteSubject]" +
                        ",[InviteReminderSubject]" +
                        ",[LoginSubject]" +
                        ",[LoginDays]" +
                        ",[LoginWeekday]" +
                        ",[LID]" +
                        ",[TreatmentOffer]" +
                        ",[TreatmentOfferText]" +
                        ",[TreatmentOfferEmail]" +
                        ",[TreatmentOfferIfNeededText]" +
                        ",[TreatmentOfferBQ]" +
                        ",[TreatmentOfferBQfn]" +
                        ",[TreatmentOfferBQmorethan]" +
                        ") VALUES (" +
                        (rs.IsDBNull(0) ? "NULL" : "'" + rs.GetString(0).Replace("'", "''") + " (copy)'") + ", " +
                        (rs.IsDBNull(1) ? "NULL" : "'" + rs.GetString(1).Replace("'", "''") + "'") + ", " +
                        PRUID + "," +
                        (rs.IsDBNull(3) ? "NULL" : "'" + rs.GetString(3).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(4) ? "NULL" : "'" + rs.GetString(4).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(5) ? "NULL" : "'" + rs.GetString(5).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(6) ? "NULL" : "'" + rs.GetString(6).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(7) ? "NULL" : "'" + rs.GetString(7).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(8) ? "NULL" : "'" + rs.GetString(8).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(9) ? "NULL" : "" + rs.GetInt32(9) + "") + ", " +
                        (rs.IsDBNull(10) ? "NULL" : "" + rs.GetInt32(10) + "") + ", " +
                        (rs.IsDBNull(11) ? "NULL" : "" + rs.GetInt32(11) + "") + ", " +
                        (rs.IsDBNull(12) ? "NULL" : "" + rs.GetInt32(12) + "") + ", " +
                        (rs.IsDBNull(13) ? "NULL" : "'" + rs.GetString(13).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(14) ? "NULL" : "'" + rs.GetString(14).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(15) ? "NULL" : "'" + rs.GetString(15).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(16) ? "NULL" : "" + rs.GetInt32(16) + "") + ", " +
                        (rs.IsDBNull(17) ? "NULL" : "" + rs.GetInt32(17) + "") + ", " +
                        (rs.IsDBNull(18) ? "NULL" : "" + rs.GetInt32(18) + "") + "" +
                        ")" +
                    "");
                }
                rs.Close();

                rs = Db.rs("SELECT TOP 1 SponsorID, CAST(SponsorKey AS VARCHAR(64)) FROM Sponsor ORDER BY SponsorID DESC");
                if (rs.Read())
                {
                    sponsorID = rs.GetInt32(0);
                    sponsorKey = rs.GetString(1).Substring(0, 8);
                }
                rs.Close();


                string dir = System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\img\\sponsor";
                if (System.IO.File.Exists(dir + "\\" + oldSponsorKey + Convert.ToInt32(HttpContext.Current.Request.QueryString["COPY"]).ToString() + ".gif"))
                {
                    System.IO.File.Copy(
                        dir + "\\" + oldSponsorKey + Convert.ToInt32(HttpContext.Current.Request.QueryString["COPY"]).ToString() + ".gif",
                        dir + "\\" + sponsorKey + sponsorID.ToString() + ".gif"
                        );
                }

                rs = Db.rs("SELECT " +
                  "[BQID]" +
                  ",[Forced]" +
                  ",[Hidden]" +
                  ",[Fn]" +
                  ",[InGrpAdmin] " +
                  "FROM [SponsorBQ] WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["COPY"]) + " ORDER BY SortOrder");
                while (rs.Read())
                {
                    Db.exec("INSERT INTO SponsorBQ (SponsorID,BQID,Forced,Hidden,Fn,InGrpAdmin) VALUES (" +
                        sponsorID + "," +
                        (rs.IsDBNull(0) ? "NULL" : "" + rs.GetInt32(0) + "") + ", " +
                        (rs.IsDBNull(1) ? "NULL" : "" + rs.GetInt32(1) + "") + ", " +
                        (rs.IsDBNull(2) ? "NULL" : "" + rs.GetInt32(2) + "") + ", " +
                        (rs.IsDBNull(3) ? "NULL" : "" + rs.GetInt32(3) + "") + ", " +
                        (rs.IsDBNull(4) ? "NULL" : "" + rs.GetInt32(4) + "") + " " +
                    ")");
                }
                rs.Close();

                rs = Db.rs("SELECT " +
                  "[SponsorProjectRoundUnitID]" +
                  ",[ProjectRoundUnitID]" +
                  ",[Nav]" +
                  ",[Feedback]" +
                  ",[Ext]" +
                  ",[SurveyID] " +
                  "FROM [SponsorProjectRoundUnit] " +
                  "WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["COPY"]) + " ORDER BY SortOrder");
                while (rs.Read())
                {
                    int individualReportID = 0, reportID = 0;
                    SqlDataReader rs2 = Db.rs("SELECT IndividualReportID, ReportID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + rs.GetInt32(1), "eFormSqlConnection");
                    if (rs2.Read())
                    {
                        individualReportID = (rs2.IsDBNull(0) ? 0 : rs2.GetInt32(0));
                        reportID = (rs2.IsDBNull(1) ? 0 : rs2.GetInt32(1));
                    }
                    rs2.Close();
                    int sPRUID = Db.createProjectRoundUnit(PRUID, rs.GetString(2), rs.GetInt32(5), individualReportID, reportID);
                    Db.exec("INSERT INTO [SponsorProjectRoundUnit] (" +
                        "[SponsorID]" +
                        ",[ProjectRoundUnitID]" +
                        ",[Nav]" +
                        ",[Feedback]" +
                        ",[Ext]" +
                        ",[SurveyID]" +
                        ") VALUES (" +
                        sponsorID + "," +
                        sPRUID + "," +
                        (rs.IsDBNull(2) ? "NULL" : "'" + rs.GetString(2).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(3) ? "NULL" : "'" + rs.GetString(3).Replace("'", "''") + "'") + ", " +
                        (rs.IsDBNull(4) ? "NULL" : "" + rs.GetInt32(4) + "") + ", " +
                        (rs.IsDBNull(5) ? "NULL" : "" + rs.GetInt32(5) + "") + " " +
                        ")");
                    int spruid = Db.getInt32("SELECT TOP 1 SponsorProjectRoundUnitID FROM SponsorProjectRoundUnit ORDER BY SponsorProjectRoundUnitID DESC");
                    
                    rs2 = Db.rs("SELECT LangID, Nav, Feedback FROM SponsorProjectRoundUnitLang WHERE SponsorProjectRoundUnitID = " + rs.GetInt32(0));
                    while (rs2.Read())
                    {
                        Db.exec("INSERT INTO SponsorProjectRoundUnitLang (SponsorProjectRoundUnitID, LangID, Nav, Feedback) VALUES (" + spruid + "," + rs2.GetInt32(0) + ",'" + (rs2.IsDBNull(1) ? "" : rs2.GetString(1).Replace("'", "''")) + "','" + (rs2.IsDBNull(2) ? "" : rs2.GetString(2).Replace("'", "''")) + "')");
                    }
                    rs2.Close();

                    rs2 = Db.rs("SELECT MeasureCategoryID, MeasureCategory FROM MeasureCategory WHERE SPRUID = " + rs.GetInt32(0));
                    while (rs2.Read())
                    {
                        Db.exec("INSERT INTO MeasureCategory (MeasureCategory,MeasureTypeID,SPRUID) VALUES (" +
                            (rs2.IsDBNull(1) ? "NULL" : "'" + rs2.GetString(1).Replace("'", "''") + "'") + "," +
                            "2," +
                            spruid + ")");
                        int mcid = Db.getInt32("SELECT TOP 1 MeasureCategoryID FROM MeasureCategory ORDER BY MeasureCategoryID DESC");

                        SqlDataReader rs3 = Db.rs("SELECT LangID, MeasureCategory FROM MeasureCategoryLang WHERE MeasureCategoryID = " + rs2.GetInt32(0));
                        while (rs3.Read())
                        {
                            Db.exec("INSERT INTO MeasureCategoryLang (MeasureCategoryID,LangID,MeasureCategory) VALUES (" +
                                mcid + "," +
                                rs3.GetInt32(0) + "," +
                                (rs3.IsDBNull(1) ? "NULL" : "'" + rs3.GetString(1).Replace("'", "''") + "'") + "" +
                                ")");
                        }
                        rs3.Close();
                    }
                    rs2.Close();
                }
                rs.Close();

                rs = Db.rs("SELECT SuperAdminID, SeeUsers FROM SuperAdminSponsor WHERE SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["COPY"]));
                while (rs.Read())
                {
                    Db.exec("INSERT INTO SuperAdminSponsor (SponsorID,SuperAdminID, SeeUsers) VALUES (" + sponsorID + "," + rs.GetInt32(0) + "," + (rs.IsDBNull(1) ? "NULL" : rs.GetInt32(1).ToString()) + ")");
                }
                rs.Close();

                Db.exec("UPDATE SponsorBQ SET SortOrder = SponsorBQID WHERE SortOrder IS NULL");
                Db.exec("UPDATE SponsorProjectRoundUnit SET SortOrder = SponsorProjectRoundUnitID WHERE SortOrder IS NULL");
                Db.exec("UPDATE MeasureCategory SET SortOrder = MeasureCategoryID WHERE SortOrder IS NULL");
                #endregion
                HttpContext.Current.Response.Redirect("sponsorSetup.aspx?SponsorID=" + sponsorID, true);
            }
            if (HttpContext.Current.Request.QueryString["Logins"] != null)
            {
                #region Export logins
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                int maxDates = 0;
                rs = Db.rs("SELECT " +
                    "u.UserID, " +          // 0
                    "u.Email, " +           // 1
                    "si.Stopped, " +        // 2
                    "si.StoppedReason " +   // 3
                    "FROM [SponsorInvite] si " +
                    "INNER JOIN [User] u ON si.UserID = u.UserID " +
                    "WHERE si.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Logins"]));
                while (rs.Read())
                {
                    sb.Append("\r\n");

                    sb.Append(rs.GetInt32(0));
                    sb.Append("\t");
                    sb.Append(rs.GetString(1));
                    sb.Append("\t");
                    sb.Append((rs.IsDBNull(2) || rs.IsDBNull(3) ? "" : rs.GetDateTime(2).ToString("yyyy-MM-dd") + ", Code " + (rs.GetInt32(3) % 10).ToString()));
                    sb.Append("\t");

                    int minutes = 0; int dates = 0;
                    System.Text.StringBuilder times = new System.Text.StringBuilder();
                    System.Collections.ArrayList dt = new ArrayList();

                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "DT, " +
                        "EndDT, " +
                        "AutoEnded, " +
                        "dbo.cf_sessionMinutes(DT, EndDT, AutoEnded) " +
                        "FROM Session " +
                        "WHERE UserID = " + rs.GetInt32(0) + " " +
                        "");
                    while (rs2.Read())
                    {
                        int minutesThisSession = rs2.GetInt32(3);
                        if (!dt.Contains(rs2.GetDateTime(0).ToString("yyyy-MM-dd")))
                        {
                            SqlDataReader rs3 = Db.rs("SELECT e.Minutes FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID WHERE es.UserID = " + rs.GetInt32(0) + " AND dbo.cf_yearMonthDay(es.DateTime) = '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd") + "'");
                            while (rs3.Read())
                            {
                                minutesThisSession += rs3.GetInt32(0);
                            }
                            rs3.Close();
                        }

                        times.Append("\t");
                        times.Append(rs2.GetDateTime(0).ToString("yyyy-MM-dd") + ", " + minutesThisSession + " min");

                        minutes += minutesThisSession;
                        dates++;
                        if(dates > maxDates) { maxDates = dates;}
                        dt.Add(rs2.GetDateTime(0).ToString("yyyy-MM-dd"));
                    }
                    rs2.Close();

                    sb.Append(minutes + " min" + times.ToString());
                }
                rs.Close();

                for (int i = 0; i < maxDates; i++)
                {
                    sb.Insert(0,"\t" + "Date");
                }
                sb.Insert(0, "" +
                    "SystemID" +
                    "\t" +
                    "Email" +
                    "\t" +
                    "Cancel" +
                    "\t" +
                    "Mintues spent" +
                    "");
                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                HttpContext.Current.Response.Write(sb.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                #endregion
            }
            if (HttpContext.Current.Request.QueryString["ExportSponsorID"] != null)
            {
                if (HttpContext.Current.Request.QueryString["Type"] == null || HttpContext.Current.Request.QueryString["Type"] == "0")
                {
                    DateTime dt = DateTime.MinValue;
                    if (HttpContext.Current.Request.QueryString["AM"] != "0")
                    {
                        dt = Convert.ToDateTime(HttpContext.Current.Request.QueryString["AM"] + "-01");
                    }
                    #region Export time brief
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("" +
                        "Identifier" + 
                        (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0" ? " 1" + "\t" + "Identifier 2" : "") +
                        "\t" +
                        "Sponsor" +
                        "\t" +
                        "Department" +
                        "\t" +
                        "Start date" +
                        "\t" +
                        "End date" +
                        "\t" +
                        "End reason" +
                        "\t" +
                        "OLD Hours spent (rounded)" +
                        "\t" +
                        "OLD Minutes spent (exact)" +
                        "\t" +
                        "NEW Hours spent (rounded)" +
                        "\t" +
                        "NEW Minutes spent (exact)" +
                        "\t" +
                        "NEW2 Hours spent (rounded)" +
                        "\t" +
                        "NEW2 Minutes spent (exact)" +
                        "\t" +
                        "Start date (adjusted)");

                    rs = Db.rs("SELECT " +
                        "u.UserID, " +          // 0
                        "u.Email, " +           // 1
                        "u.Created, " +         // 2
                        "ISNULL(NULLIF(CAST(upb.ValueText AS VARCHAR(64)),''),w.ValueText), " +     // 3
                        "ISNULL(upb.ValueInt,w.ValueInt), " +      // 4
                        "ISNULL(upb.ValueDate,w.ValueDate), " +     // 5
                        "ba.Internal, " +       // 6
                        "si.Stopped, " +        // 7
                        "si.StoppedReason, " +  // 8
                        "(SELECT MIN(six.Sent) FROM SponsorInvite six WHERE six.SponsorID = si.SponsorID), " + // 9
                        "s.Sponsor, " +         // 10
                        "d.Department " +       // 11
                        (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0" ? "," +
                        "ISNULL(NULLIF(CAST(upb2.ValueText AS VARCHAR(64)),''),w2.ValueText), " +    // 12
                        "ISNULL(upb2.ValueInt,w2.ValueInt), " +     // 13
                        "ISNULL(upb2.ValueDate,w2.ValueDate), " +    // 14
                        "ba2.Internal " +       // 15
                        "" : "") +
                        "FROM [SponsorInvite] si " +
                        "INNER JOIN [User] u ON si.UserID = u.UserID " +
                        "INNER JOIN Sponsor s ON si.SponsorID = s.SponsorID " +
                        "INNER JOIN Department d ON si.DepartmentID = d.DepartmentID " +
                        "LEFT OUTER JOIN UserProfileBQ upb ON u.UserProfileID = upb.UserProfileID AND upb.BQID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID"]) + " " +
                        "LEFT OUTER JOIN SponsorInviteBQ w ON si.SponsorInviteID = w.SponsorInviteID AND w.BQID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID"]) + " " +
                        "LEFT OUTER JOIN BA ba ON ISNULL(upb.ValueInt,w.ValueInt) = ba.BAID AND ba.BQID = upb.BQID " +
                        (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0" ? "" +
                        "LEFT OUTER JOIN UserProfileBQ upb2 ON u.UserProfileID = upb2.UserProfileID AND upb2.BQID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID2"]) + " " +
                        "LEFT OUTER JOIN SponsorInviteBQ w2 ON si.SponsorInviteID = w2.SponsorInviteID AND w2.BQID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID2"]) + " " +
                        "LEFT OUTER JOIN BA ba2 ON ISNULL(upb2.ValueInt,w2.ValueInt) = ba2.BAID AND ba2.BQID = upb2.BQID " +
                        "" : "") +
                        "WHERE si.SponsorID IN (0" + HttpContext.Current.Request.QueryString["ExportSponsorID"].Replace("'", "").Replace(" ", "") + ") " +
                        (dt > DateTime.MinValue ? "AND u.Created < '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "' " : "") +
                        //"AND (si.Stopped IS NULL OR si.Stopped >= '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "') " +
                        "");
                    while (rs.Read())
                    {
                        sb.Append("\r\n");

                        if (!rs.IsDBNull(6) && rs.GetString(6).Trim() != "")
                        {
                            sb.Append(rs.GetString(6));
                        }
                        else if (!rs.IsDBNull(5))
                        {
                            sb.Append(rs.GetDateTime(5).ToString("yyyy-MM-dd"));
                        }
                        else if (!rs.IsDBNull(4))
                        {
                            sb.Append(rs.GetInt32(4).ToString());
                        }
                        else if (!rs.IsDBNull(3) && rs.GetString(3).Trim() != "")
                        {
                            sb.Append(rs.GetString(3));
                        }
                        else if (!rs.IsDBNull(1) && rs.GetString(1).Trim() != "")
                        {
                            sb.Append(rs.GetString(1));
                        }
                        else
                        {
                            sb.Append(rs.GetInt32(0).ToString());
                        }
                        sb.Append("\t");
                        if (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0")
                        {
                            if (!rs.IsDBNull(15) && rs.GetString(15).Trim() != "")
                            {
                                sb.Append(rs.GetString(15));
                            }
                            else if (!rs.IsDBNull(14))
                            {
                                sb.Append(rs.GetDateTime(14).ToString("yyyy-MM-dd"));
                            }
                            else if (!rs.IsDBNull(13))
                            {
                                sb.Append(rs.GetInt32(13).ToString());
                            }
                            else if (!rs.IsDBNull(12) && rs.GetString(12).Trim() != "")
                            {
                                sb.Append(rs.GetString(12));
                            }
                            sb.Append("\t");
                        }
                        sb.Append(rs.GetString(10));
                        sb.Append("\t");
                        sb.Append(rs.GetString(11));
                        sb.Append("\t");
                        sb.Append(rs.GetDateTime(2).ToString("yyyy-MM-dd"));
                        sb.Append("\t");
                        sb.Append((rs.IsDBNull(7) || rs.GetDateTime(7) >= dt.AddMonths(1) ? "" : rs.GetDateTime(7).ToString("yyyy-MM-dd")));
                        sb.Append("\t");
                        sb.Append((rs.IsDBNull(8) || rs.IsDBNull(7) || rs.GetDateTime(7) >= dt.AddMonths(1) ? "" : (rs.GetInt32(8) % 10).ToString()));
                        sb.Append("\t");
                        int minutes = 0, minutes2 = 0, minutes3 = 0;

                        SqlDataReader rs2 = Db.rs("SELECT " +
                            "DT, " +
                            "EndDT, " +
                            "AutoEnded, " +
                            "dbo.cf_sessionMinutes(DT, EndDT, AutoEnded) " +
                            "FROM Session " +
                            "WHERE UserID = " + rs.GetInt32(0) + " " +
                            (dt > DateTime.MinValue ? "AND DT >= '" + dt.ToString("yyyy-MM-dd") + "' " +
                            "AND DT < '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "'" +
                            "" : "") +
                            "");
                        while (rs2.Read())
                        {
                            if (rs2.IsDBNull(0) || rs2.IsDBNull(1))
                            {
                                // Start or end date missing, assume 10 minutes
                                minutes += 10;
                            }
                            else if (rs2.IsDBNull(2) || !rs2.GetBoolean(2))
                            {
                                // Session ended on demand, or we don't know, subtract start date from end date
                                minutes += ((TimeSpan)rs2.GetDateTime(1).Subtract(rs2.GetDateTime(0))).Minutes;
                            }
                            else
                            {
                                // Session autoended, subtract start date from end date minus an assumed 10 minutes
                                minutes += ((TimeSpan)rs2.GetDateTime(1).AddMinutes(-10).Subtract(rs2.GetDateTime(0))).Minutes;
                            }
                            minutes2 += rs2.GetInt32(3);
                            minutes3 += rs2.GetInt32(3);
                        }
                        rs2.Close();
                        rs2 = Db.rs("SELECT e.Minutes FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID WHERE es.UserID = " + rs.GetInt32(0) + " " +
                            (dt > DateTime.MinValue ? "AND es.DateTime >= '" + dt.ToString("yyyy-MM-dd") + "' AND es.DateTime < '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "'" : ""));
                        while (rs2.Read())
                        {
                            minutes += rs2.GetInt32(0);
                            minutes3 += rs2.GetInt32(0);
                        }
                        rs2.Close();
                        sb.Append(Math.Round(Math.Ceiling(Convert.ToDouble(minutes) / 60d), 0));
                        sb.Append("\t");
                        sb.Append(minutes);
                        sb.Append("\t");
                        sb.Append(Math.Round(Math.Ceiling(Convert.ToDouble(minutes2) / 60d), 0));
                        sb.Append("\t");
                        sb.Append(minutes2);
                        sb.Append("\t");
                        sb.Append(Math.Round(Math.Ceiling(Convert.ToDouble(minutes3) / 60d), 0));
                        sb.Append("\t");
                        sb.Append(minutes3);
                        sb.Append("\t");
                        if (rs.GetDateTime(9) > rs.GetDateTime(2))
                        {
                            sb.Append(rs.GetDateTime(9).ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            sb.Append(rs.GetDateTime(2).ToString("yyyy-MM-dd"));
                        }
                    }
                    rs.Close();

                    HttpContext.Current.Response.Clear();

                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                    HttpContext.Current.Response.ContentType = "text/plain";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                    HttpContext.Current.Response.Write(sb.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    #endregion
                }
                else if (HttpContext.Current.Request.QueryString["Type"] == "1")
                {
                    DateTime dt = DateTime.MinValue;
                    if (HttpContext.Current.Request.QueryString["AM"] != "0")
                    {
                        dt = Convert.ToDateTime(HttpContext.Current.Request.QueryString["AM"] + "-01");
                    }
                    #region Export time verbose
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("" +
                        "Identifier" +
                        (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0" ? " 1" + "\t" + "Identifier 2" : "") +
                        "\t" +
                        "Sponsor" +
                        "\t" +
                        "Department" +
                        "\t" +
                        "Cancel date" +
                        "\t" +
                        "Cancel reason" +
                        "\t" +
                        "Activity date" +
                        "\t" +
                        "Activity minutes" +
                        "\t" +
                        "Activity hours" +
                        "\t" +
                        "Activity rounded hours" +
                        "\t" +
                        "Activities");

                    rs = Db.rs("SELECT " +
                        "u.UserID, " +          // 0
                        "u.Email, " +           // 1
                        "u.Created, " +         // 2
                        "upb.ValueText, " +     // 3
                        "upb.ValueInt, " +      // 4
                        "upb.ValueDate, " +     // 5
                        "ba.Internal, " +       // 6
                        "si.Stopped, " +        // 7
                        "si.StoppedReason, " +  // 8
                        "(SELECT MIN(six.Sent) FROM SponsorInvite six WHERE six.SponsorID = si.SponsorID), " + // 9
                        "u.LID, " +             // 10
                        "s.Sponsor, " +         // 11
                        "d.Department " +       // 12
                        (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0" ? "," +
                        "upb2.ValueText, " +    // 13
                        "upb2.ValueInt, " +     // 14
                        "upb2.ValueDate, " +    // 15
                        "ba2.Internal " +       // 16
                        "" : "") +
                        "FROM [SponsorInvite] si " +
                        "INNER JOIN [User] u ON si.UserID = u.UserID " +
                        "INNER JOIN Sponsor s ON si.SponsorID = s.SponsorID " +
                        "INNER JOIN Department d ON si.DepartmentID = d.DepartmentID " +
                        "LEFT OUTER JOIN UserProfileBQ upb ON u.UserProfileID = upb.UserProfileID AND upb.BQID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID"]) + " " +
                        "LEFT OUTER JOIN BA ba ON upb.ValueInt = ba.BAID AND ba.BQID = upb.BQID " +
                        (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0" ? "" +
                        "LEFT OUTER JOIN UserProfileBQ upb2 ON u.UserProfileID = upb2.UserProfileID AND upb2.BQID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID2"]) + " " +
                        "LEFT OUTER JOIN BA ba2 ON upb2.ValueInt = ba2.BAID AND ba2.BQID = upb2.BQID " +
                        "" : "") +
                        "WHERE si.SponsorID IN (0" + HttpContext.Current.Request.QueryString["ExportSponsorID"].Replace("'", "").Replace(" ", "") + ") " +
                        (dt > DateTime.MinValue ? "AND u.Created < '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "' " : "") +
                        //"AND (si.Stopped IS NULL OR si.Stopped >= '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "') " +
                        "");
                    while (rs.Read())
                    {
                        string ident = "\r\n";

                        if (!rs.IsDBNull(6) && rs.GetString(6).Trim() != "")
                        {
                            ident += rs.GetString(6);
                        }
                        else if (!rs.IsDBNull(5))
                        {
                            ident += rs.GetDateTime(5).ToString("yyyy-MM-dd");
                        }
                        else if (!rs.IsDBNull(4))
                        {
                            ident += rs.GetInt32(4).ToString();
                        }
                        else if (!rs.IsDBNull(3) && rs.GetString(3).Trim() != "")
                        {
                            ident += rs.GetString(3);
                        }
                        else if (!rs.IsDBNull(1) && rs.GetString(1).Trim() != "")
                        {
                            ident += rs.GetString(1);
                        }
                        else
                        {
                            ident += rs.GetInt32(0).ToString();
                        }

                        ident += "\t";

                        if (HttpContext.Current.Request.QueryString["BQID2"] != null && HttpContext.Current.Request.QueryString["BQID2"].ToString() != "0")
                        {
                            if (!rs.IsDBNull(16) && rs.GetString(16).Trim() != "")
                            {
                                ident += rs.GetString(16);
                            }
                            else if (!rs.IsDBNull(15))
                            {
                                ident += rs.GetDateTime(15).ToString("yyyy-MM-dd");
                            }
                            else if (!rs.IsDBNull(14))
                            {
                                ident += rs.GetInt32(14).ToString();
                            }
                            else if (!rs.IsDBNull(13) && rs.GetString(13).Trim() != "")
                            {
                                ident += rs.GetString(13);
                            }
                            ident += "\t";
                        }
                        ident += rs.GetString(11);
                        ident += "\t";
                        ident += rs.GetString(12);
                        ident += "\t";

                        ident += (rs.IsDBNull(7) || rs.GetDateTime(7) >= dt.AddMonths(1) ? "" : rs.GetDateTime(7).ToString("yyyy-MM-dd"));
                        ident += "\t";
                        ident += (rs.IsDBNull(8) || rs.IsDBNull(7) || rs.GetDateTime(7) >= dt.AddMonths(1) ? "" : (rs.GetInt32(8) % 10).ToString());
                        ident += "\t";

                        SqlDataReader rs2 = Db.rs("SELECT " +
                            "DT, " +
                            "EndDT, " +
                            "AutoEnded, " +
                            "dbo.cf_sessionMinutes(DT, EndDT, AutoEnded) " +
                            "FROM Session " +
                            "WHERE UserID = " + rs.GetInt32(0) + " " +
                            (dt > DateTime.MinValue ? "AND DT >= '" + dt.ToString("yyyy-MM-dd") + "' " +
                            "AND DT < '" + dt.AddMonths(1).ToString("yyyy-MM-dd") + "'" +
                            "" : "") +
                            "");
                        while (rs2.Read())
                        {
                            int minutes = rs2.GetInt32(3);
                            string desc = "Login";

                            SqlDataReader rs3 = Db.rs("SELECT " +
                                "mc.MeasureComponent " +
                                "FROM UserMeasure um " +
                                "INNER JOIN UserMeasureComponent umc ON um.UserMeasureID = umc.UserMeasureID " +
                                "INNER JOIN MeasureComponent mc ON umc.MeasureComponentID = mc.MeasureComponentID " +
                                "WHERE um.UserID = " + rs.GetInt32(0) + " " +
                                "AND um.DT >= '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd HH:mm") + "' " +
                                "AND um.DT < '" + rs2.GetDateTime(0).AddMinutes(minutes).ToString("yyyy-MM-dd HH:mm") + "'");
                            while (rs3.Read())
                            {
                                desc += ", Measure " + rs3.GetString(0);
                            }
                            rs3.Close();

                            rs3 = Db.rs("SELECT " +
                                "s.Nav " +
                                "FROM UserProjectRoundUser u " +
                                "INNER JOIN UserProjectRoundUserAnswer a ON u.ProjectRoundUserID = a.ProjectRoundUserID " +
                                "INNER JOIN SponsorProjectRoundUnit s ON u.ProjectRoundUnitID = s.ProjectRoundUnitID " +
                                "WHERE u.UserID = " + rs.GetInt32(0) + " " +
                                "AND a.DT >= '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd HH:mm") + "' " +
                                "AND a.DT < '" + rs2.GetDateTime(0).AddMinutes(minutes).ToString("yyyy-MM-dd HH:mm") + "'");
                            while (rs3.Read())
                            {
                                desc += ", Form " + rs3.GetString(0) + ", Statistics";
                            }
                            rs3.Close();

                            rs3 = Db.rs("SELECT " +
                                "d.DiaryID " +
                                "FROM Diary d " +
                                "WHERE d.UserID = " + rs.GetInt32(0) + " " +
                                "AND d.CreatedDT >= '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd HH:mm") + "' " +
                                "AND d.CreatedDT < '" + rs2.GetDateTime(0).AddMinutes(minutes).ToString("yyyy-MM-dd HH:mm") + "'");
                            while (rs3.Read())
                            {
                                desc += ", Diary writing";
                            }
                            rs3.Close();

                            rs3 = Db.rs("SELECT " +
                                "e.Minutes, " +
                                "el.Exercise " +
                                "FROM ExerciseStats es " +
                                "INNER JOIN ExerciseVariantLang evl ON evl.ExerciseVariantLangID = es.ExerciseVariantLangID " +
                                "INNER JOIN ExerciseVariant ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
                                "INNER JOIN Exercise e ON ev.ExerciseID = e.ExerciseID " +
                                "INNER JOIN ExerciseLang el ON e.ExerciseID = el.ExerciseID AND evl.Lang = el.Lang " +
                                "WHERE es.UserID = " + rs.GetInt32(0) + " " +
                                "AND es.DateTime >= '" + rs2.GetDateTime(0).ToString("yyyy-MM-dd HH:mm") + "' " +
                                "AND es.DateTime < '" + rs2.GetDateTime(0).AddMinutes(minutes).ToString("yyyy-MM-dd HH:mm") + "'");
                            while (rs3.Read())
                            {
                                minutes += rs3.GetInt32(0);
                                desc += ", Exercise " + rs3.GetString(1);
                            }
                            rs3.Close();

                            if (!rs2.IsDBNull(2) && !rs2.GetBoolean(2))
                                desc += ", Articles";

                            sb.Append(ident + rs2.GetDateTime(0).ToString("yyyy-MM-dd HH:mm") + "\t" + minutes + "\t" + Math.Round((double)minutes/60,2) + "\t" + Math.Ceiling((double)minutes/60) + "\t" + desc);
                        }
                        rs2.Close();
                    }
                    rs.Close();

                    HttpContext.Current.Response.Clear();

                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                    HttpContext.Current.Response.ContentType = "text/plain";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                    HttpContext.Current.Response.Write(sb.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                    #endregion
                }
                else if (HttpContext.Current.Request.QueryString["Type"] == "3" || HttpContext.Current.Request.QueryString["Type"] == "4")
                {
                    #region Export absence
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    sb.Append("" +
                        "UserID" +
                        "\t" +
                        "Sponsor" +
                        "\t" +
                        "SponsorID" +
                        "\t" +
                        "Department" +
                        "\t" +
                        "DepartmentID" +
                        "\t" +
                        "Sponsor start" +
                        "\t" +
                        "User invited" +
                        "\t" +
                        "User start" +
                        "\t" +
                        "Cancel date" +
                        "\t" +
                        "Cancel reason");

                    string sel = "", join = "";
                    int cx = 0;
                    rs = Db.rs("SELECT BQID, Internal FROM BQ ORDER BY BQID");
                    while(rs.Read())
                    {
                        join += "LEFT OUTER JOIN UserProfileBQ upb" + cx + " ON u.UserProfileID = upb" + cx + ".UserProfileID AND upb" + cx + ".BQID = " + rs.GetInt32(0) + " " +
                                "LEFT OUTER JOIN BA ba" + cx + " ON upb" + cx + ".ValueInt = ba" + cx + ".BAID AND ba" + cx + ".BQID = upb" + cx + ".BQID ";
                        sel += ", upb" + cx + ".ValueText" +
                               ", upb" + cx + ".ValueInt" +
                               ", upb" + cx + ".ValueDate" +
                               ", ba" + cx + ".Internal";
                        sb.Append("\t" + rs.GetString(1));
                        cx++;
                    }
                    rs.Close();

                    if (HttpContext.Current.Request.QueryString["Type"] == "4")
                    {
                        for (int i = 6; i >= 1; i--)
                        {
                            for (int ii = 1; ii <= 7; ii++)
                            {
                                sb.Append("" +
                                "\t" +
                                "mm" + i + "t" + ii);
                            }
                        }

                        for (int i = 1; i <= 18; i++)
                        {
                            for (int ii = 1; ii <= 7; ii++)
                            {
                                sb.Append("" +
                                "\t" +
                                "m" + i + "t" + ii);
                            }
                        }
                    }
                    else
                    {
                        sb.Append("" +
                            "\t" +
                            "Month" +
                            "\t" +
                            "Sponsor start offset" +
                            "\t" +
                            "Type" +
                            "\t" +
                            "Minutes");
                    }

                    rs = Db.rs("SELECT " +
                       "u.UserID, " +          // 0
                       "u.Email, " +           // 1
                       "u.Created, " +         // 2
                       "si.Stopped, " +        // 3
                       "si.StoppedReason, " +  // 4
                       "(SELECT MIN(six.Sent) FROM SponsorInvite six WHERE six.SponsorID = si.SponsorID), " + // 5
                       "u.LID, " +             // 6
                       "s.Sponsor, " +         // 7
                       "d.Department, " +      // 8
                       "s.SponsorID, " +       // 9
                       "d.DepartmentID," +     // 10
                       "si.Sent, " +           // 11
                       "si.SponsorInviteID" +  // 12
                       sel + " " +
                       "FROM [SponsorInvite] si " +
                       "INNER JOIN Sponsor s ON si.SponsorID = s.SponsorID " +
                       "INNER JOIN Department d ON si.DepartmentID = d.DepartmentID " +
                       "LEFT OUTER JOIN [User] u ON si.UserID = u.UserID " +
                       join +
                       "WHERE si.SponsorID IN (0" + HttpContext.Current.Request.QueryString["ExportSponsorID"].Replace("'", "").Replace(" ", "") + ") " +
                       "");
                    while (rs.Read())
                    {
                        string s = "\r\n" +(rs.IsDBNull(0) ? "" : rs.GetInt32(0).ToString()) + 
                            "\t" + rs.GetString(7) + 
                            "\t" + rs.GetInt32(9) + 
                            "\t" + rs.GetString(8) + 
                            "\t" + rs.GetInt32(10) +
                            "\t" + rs.GetDateTime(5).ToString("yyyy-MM-dd") +
                            "\t" + (rs.IsDBNull(11) ? "" : rs.GetDateTime(11).ToString("yyyy-MM-dd")) +
                            "\t" + (rs.IsDBNull(2) ? "" : rs.GetDateTime(2).ToString("yyyy-MM-dd")) +
                            "\t" +(rs.IsDBNull(3) ? "" : rs.GetDateTime(3).ToString("yyyy-MM-dd")) +
                            "\t" + (rs.IsDBNull(4) ? "" : (rs.GetInt32(4) % 10).ToString());
                        for (int i = 0; i < cx; i++)
                        {
                            int ii = 13 + i*4;

                            s += "\t";
                            if (!rs.IsDBNull(ii+3) && rs.GetString(ii+3).Trim() != "")
                            {
                                s += rs.GetString(ii+3);
                            }
                            else if (!rs.IsDBNull(ii+2))
                            {
                                s += rs.GetDateTime(ii+2).ToString("yyyy-MM-dd");
                            }
                            else if (!rs.IsDBNull(ii+1))
                            {
                                s += rs.GetInt32(ii+1).ToString();
                            }
                            else if (!rs.IsDBNull(ii) && rs.GetString(ii).Trim() != "")
                            {
                                s += rs.GetString(ii);
                            }
                        }

                        if (HttpContext.Current.Request.QueryString["Type"] == "4")
                        {
                            sb.Append(s);
                            DateTime startMonth = rs.GetDateTime(5);

                            for (int i = -6; i < 18; i++)
                            {
                                for (int x = 1; x <= 7; x++)
                                {
                                    int minutes = 0;
                                    SqlDataReader rs2 = Db.rs("SELECT SUM(Minutes) FROM SponsorInviteAbsence WHERE SponsorInviteID = " + rs.GetInt32(12) + " AND TypeID = " + x + " AND Month = " + startMonth.AddMonths(i).ToString("yyyyMM"));
                                    if (rs2.Read() && !rs2.IsDBNull(0))
                                    {
                                        minutes = rs2.GetInt32(0);
                                    }
                                    rs2.Close();
                                    sb.Append("\t" + minutes);
                                }
                            }
                        }
                        else
                        {
                            int mi = 0, ma = 0;
                            SqlDataReader rs2 = Db.rs("SELECT MIN(Month), MAX(Month) FROM SponsorInviteAbsence sia INNER JOIN SponsorInvite si ON sia.SponsorInviteID = si.SponsorInviteID WHERE SponsorID = " + rs.GetInt32(9));
                            if (rs2.Read() && !rs2.IsDBNull(0))
                            {
                                mi = rs2.GetInt32(0);
                                ma = rs2.GetInt32(1);
                            }
                            rs2.Close();

                            int miy = Convert.ToInt32(mi.ToString().Substring(0, 4));
                            int mim = Convert.ToInt32(mi.ToString().Substring(4, 2));
                            int may = Convert.ToInt32(ma.ToString().Substring(0, 4));
                            int mam = Convert.ToInt32(ma.ToString().Substring(4, 2));
                            for (int i = miy; i <= may; i++)
                            {
                                int low = 1, high = 12;
                                if (i == miy) { low = mim; }
                                if (i == may) { high = mam; }
                                for (int ii = low; ii <= high; ii++)
                                {
                                    for (int x = 1; x <= 7; x++)
                                    {
                                        int minutes = 0;
                                        rs2 = Db.rs("SELECT SUM(Minutes) FROM SponsorInviteAbsence WHERE SponsorInviteID = " + rs.GetInt32(12) + " AND TypeID = " + x + " AND Month = " + i.ToString() + ii.ToString().PadLeft(2, '0'));
                                        if (rs2.Read() && !rs2.IsDBNull(0))
                                        {
                                            minutes = rs2.GetInt32(0);
                                        }
                                        rs2.Close();
                                        int o =
                                            (Convert.ToInt32(i.ToString()) * 12 + Convert.ToInt32(ii.ToString().PadLeft(2, '0')))
                                            -
                                            (Convert.ToInt32(rs.GetDateTime(5).ToString("yyyy")) * 12 + Convert.ToInt32(rs.GetDateTime(5).ToString("MM")));
                                        sb.Append(s + "\t" + i.ToString() + ii.ToString().PadLeft(2, '0') + "\t" + o + "\t" + x + "\t" + minutes);
                                    }
                                }
                            }
                        }
                    }
                    rs.Close();
                    #endregion

                    HttpContext.Current.Response.Clear();

                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                    HttpContext.Current.Response.ContentType = "text/plain";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                    HttpContext.Current.Response.Write(sb.ToString());
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
            }
            if (HttpContext.Current.Request.QueryString["Inactive"] != null)
            {
                #region Export inactive
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("" +
                    "Email" +
                    "\t" +
                    "Department");

                rs = Db.rs("SELECT " +
                    "si.Email, " +          // 0
                    "d.Department " +       // 1
                    "FROM [SponsorInvite] si " +
                    "INNER JOIN [Department] d ON si.DepartmentID = d.DepartmentID " +
                    "WHERE si.UserID IS NULL AND si.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Inactive"]) + " ORDER BY d.Department, si.Email");
                while (rs.Read())
                {
                    sb.Append("\r\n");

                    sb.Append(rs.GetString(0));
                    sb.Append("\t");
                    sb.Append(rs.GetString(1));
                }
                rs.Close();

                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                HttpContext.Current.Response.Write(sb.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                #endregion
            }
            if (HttpContext.Current.Request.QueryString["Org"] != null)
            {
                #region Export org
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("" +
                    "ID" +
                    "\t" +
                    "parentID" +
                    "\t" +
                    "unit");

                rs = Db.rs("SELECT " +
                    "d.DepartmentShort, " +
                    "dd.DepartmentShort, " +
                    "d.Department, " +
                    "d.DepartmentID, " +
                    "d.DepartmentKey " +
                    "FROM [Department] d " +
                    "LEFT OUTER JOIN [Department] dd ON d.ParentDepartmentID = dd.DepartmentID " +
                    "WHERE d.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Org"]) + " ORDER BY d.SortString");
                while (rs.Read())
                {
                    string key = rs.GetSqlGuid(4).ToString().Substring(0, 4) + rs.GetInt32(3);

                    sb.Append("\r\n");

                    sb.Append(rs.GetString(0));
                    sb.Append("\t");
                    sb.Append((rs.IsDBNull(1) ? "" : rs.GetString(1)));
                    sb.Append("\t");
                    sb.Append(rs.GetString(2));
                    sb.Append("\t");
                    sb.Append(key);
                    sb.Append("\t");
                    sb.Append("https://healthwatch.se/code/" + key);
                }
                rs.Close();

                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                HttpContext.Current.Response.Write(sb.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                #endregion
            }
            if (HttpContext.Current.Request.QueryString["All"] != null)
            {
                #region Export all
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("" +
                    "Email" +
                    "\t" +
                    "Department" +
                    "\t" +
                    "Department code" +
                    "\t" +
                    "Status");

                rs = Db.rs("SELECT " +
                    "si.Email, " +          // 0
                    "d.Department, " +      // 1
                    "d.DepartmentShort, " +
                    "si.StoppedReason " +
                    "FROM [SponsorInvite] si " +
                    "INNER JOIN [Department] d ON si.DepartmentID = d.DepartmentID " +
                    "WHERE si.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["All"]) + " ORDER BY d.Department, si.Email");
                while (rs.Read())
                {
                    sb.Append("\r\n");

                    sb.Append(rs.GetString(0));
                    sb.Append("\t");
                    sb.Append(rs.GetString(1));
                    sb.Append("\t");
                    sb.Append(rs.GetString(2));
                    sb.Append("\t");
                    sb.Append((rs.IsDBNull(3) ? 0 : rs.GetInt32(3)));
                }
                rs.Close();

                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
                HttpContext.Current.Response.ContentType = "text/plain";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.Ticks + ".txt");
                HttpContext.Current.Response.Write(sb.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
                #endregion
            }
        }
        int mergeFrom = (HttpContext.Current.Request.Form["MergeFrom"] != null ? Convert.ToInt32(HttpContext.Current.Request.Form["MergeFrom"]) : 0);
        int mergeTo = (HttpContext.Current.Request.Form["MergeTo"] != null ? Convert.ToInt32(HttpContext.Current.Request.Form["MergeTo"]) : 0);
        bool showMerge = (mergeFrom != 0 && mergeTo != 0 && mergeFrom != mergeTo);
        Merge.Visible = showMerge; ExecMerge.Visible = showMerge;
        Merge.Text = "";
        if (showMerge)
        {
            rs = Db.rs("SELECT " +
                "spru.ProjectRoundUnitID, " +
                "spru.Nav, " +
                "s.Sponsor, " +
                "s2.Sponsor " +
                "FROM SponsorProjectRoundUnit spru " +
                "INNER JOIN Sponsor s ON spru.SponsorID = s.SponsorID " +
                "INNER JOIN Sponsor s2 ON s2.SponsorID = " + mergeTo + " " +
                "WHERE spru.SponsorID = " + mergeFrom);
            if (rs.Read())
            {
                Merge.Text += "<TR><TD>Merge from " + rs.GetString(2) + "</td><td>Merge to " + rs.GetString(3) + "</td></tr>";
                do
                {
                    Merge.Text += "<TR><TD>" + rs.GetString(1) + "</TD><TD><SELECT NAME=\"PRUID" + rs.GetInt32(0) + "\">";
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "spru.ProjectRoundUnitID, " +
                        "spru.Nav " +
                        "FROM SponsorProjectRoundUnit spru " +
                        "WHERE spru.SponsorID = " + mergeTo);
                    while (rs2.Read())
                    {
                        Merge.Text += "<OPTION VALUE=\"" + rs2.GetInt32(0) + "\">" + rs2.GetString(1) + "</OPTION>";
                    }
                    rs2.Close();
                    Merge.Text += "</SELECT></TD></TR>";
                }
                while (rs.Read());
            }
            rs.Close();

            ExecMerge.Click += new EventHandler(ExecMerge_Click);
        }
    }

    void ExecMerge_Click(object sender, EventArgs e)
    {
        int mergeFrom = (HttpContext.Current.Request.Form["MergeFrom"] != null ? Convert.ToInt32(HttpContext.Current.Request.Form["MergeFrom"]) : 0);
        int mergeTo = (HttpContext.Current.Request.Form["MergeTo"] != null ? Convert.ToInt32(HttpContext.Current.Request.Form["MergeTo"]) : 0);

        SqlDataReader rs = Db.rs("SELECT SponsorExtendedSurveyID FROM SponsorExtendedSurvey WHERE SponsorID = " + mergeFrom);
        while (rs.Read())
        {
            SqlDataReader rs2 = Db.rs("SELECT DepartmentID FROM Department WHERE SponsorID = " + mergeTo);
            while (rs2.Read())
            {
                Db.exec("INSERT INTO SponsorExtendedSurveyDepartment (SponsorExtendedSurveyID,DepartmentID,Hide) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + ",1)");
            }
            rs2.Close();
        }
        rs.Close();

        rs = Db.rs("SELECT SponsorExtendedSurveyID FROM SponsorExtendedSurvey WHERE SponsorID = " + mergeTo);
        while (rs.Read())
        {
            SqlDataReader rs2 = Db.rs("SELECT DepartmentID FROM Department WHERE SponsorID = " + mergeFrom);
            while (rs2.Read())
            {
                Db.exec("INSERT INTO SponsorExtendedSurveyDepartment (SponsorExtendedSurveyID,DepartmentID,Hide) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + ",1)");
            }
            rs2.Close();
        }
        rs.Close();

        Db.exec("UPDATE SponsorExtendedSurvey SET SponsorID = " + mergeTo + " WHERE SponsorID = " + mergeFrom);
        Db.exec("UPDATE Department SET SponsorID = " + mergeTo + " WHERE SponsorID = " + mergeFrom);
        Db.exec("UPDATE SponsorInvite SET SponsorID = " + mergeTo + " WHERE SponsorID = " + mergeFrom);
        Db.exec("UPDATE SponsorAdmin SET SponsorID = " + mergeTo + " WHERE SponsorID = " + mergeFrom);
        Db.exec("UPDATE [User] SET SponsorID = " + mergeTo + " WHERE SponsorID = " + mergeFrom);
        Db.exec("UPDATE UserProfile SET SponsorID = " + mergeTo + " WHERE SponsorID = " + mergeFrom);
        Db.exec("UPDATE Sponsor SET Sponsor = Sponsor+' (merged/removed)' WHERE SponsorID = " + mergeFrom);

        rs = Db.rs("SELECT " +
                "spru.ProjectRoundUnitID " +
                "FROM SponsorProjectRoundUnit spru " +
                "WHERE spru.SponsorID = " + mergeFrom);
        while (rs.Read())
        {
            Db.exec("UPDATE UserProjectRoundUser SET ProjectRoundUnitID = " + Convert.ToInt32(HttpContext.Current.Request.Form["PRUID" + rs.GetInt32(0)]) + " WHERE ProjectRoundUnitID = " + rs.GetInt32(0));
            Db.exec("UPDATE Answer SET ProjectRoundUnitID = " + Convert.ToInt32(HttpContext.Current.Request.Form["PRUID" + rs.GetInt32(0)]) + " WHERE ProjectRoundUnitID = " + rs.GetInt32(0), "eFormSqlConnection");
            Db.exec("UPDATE ProjectRoundUser SET ProjectRoundUnitID = " + Convert.ToInt32(HttpContext.Current.Request.Form["PRUID" + rs.GetInt32(0)]) + " WHERE ProjectRoundUnitID = " + rs.GetInt32(0), "eFormSqlConnection");
        }
        rs.Close();
    }
    protected override void  OnPreRender(EventArgs e)
    {
 	    base.OnPreRender(e);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("<TR>" +
            "<TD COLSPAN=\"4\"><B>Name</B></TD>" +
            "<TD><B>Admin</B></TD>" +
            "<TD style=\"font-size:9px;\"><B>Contact</B></TD>" +
            "<TD style=\"font-size:9px;\">&nbsp;<B># of<br/>&nbsp;ext<br/>&nbsp;surveys</B></TD>" +
            "<TD style=\"font-size:9px;\">&nbsp;<B># of<br/>&nbsp;invited<br/>&nbsp;users</B></TD>" +
            "<TD style=\"font-size:9px;\">&nbsp;<B># of<br/>&nbsp;activated<br/>&nbsp;users</B></TD>" +
            "<TD style=\"font-size:9px;\">&nbsp;<B>1st invite<br/>&nbsp;sent</B></TD>" +
            "<TD style=\"font-size:9px;\"><B>Activity</B><br/><select name=\"ActivityMonth\">");
        SqlDataReader rs = Db.rs("SELECT MIN(Created) FROM [User]");
        sb.Append("<OPTION VALUE=\"0\">All time</OPTION>");
        if(rs.Read())
        {
            for (int i = DateTime.Now.Year; i >= rs.GetDateTime(0).Year; i--)
            {
                if(i==rs.GetDateTime(0).Year)
                {
                    for(int ii=12; ii>=rs.GetDateTime(0).Month; ii--)
                    {
                        sb.Append("<OPTION VALUE=\"" + i + "-" + ii.ToString().PadLeft(2, '0') + "\">" + i + "-" + ii.ToString().PadLeft(2, '0') + "</OPTION>");
                    }
                }
                else if(i==DateTime.Now.Year)
                {
                    for (int ii = DateTime.Now.Month; ii >= 1; ii--)
                    {
                        sb.Append("<OPTION VALUE=\"" + i + "-" + ii.ToString().PadLeft(2, '0') + "\">" + i + "-" + ii.ToString().PadLeft(2, '0') + "</OPTION>");
                    }
                }
                else
                {
                    for(int ii=12; ii>=1; ii--)
                    {
                        sb.Append("<OPTION VALUE=\"" + i + "-" + ii.ToString().PadLeft(2, '0') + "\">" + i + "-" + ii.ToString().PadLeft(2, '0') + "</OPTION>");
                    }
                }
            }
        }
        rs.Close();
        sb.Append("</select><br/><SELECT style=\"width:75px;\" NAME=\"BQID\"><OPTION VALUE=\"0\">Email</OPTION>");

        rs = Db.rs("SELECT DISTINCT bq.BQID, bq.Variable FROM SponsorBQ sbq INNER JOIN BQ bq ON sbq.BQID = bq.BQID ORDER BY bq.Variable");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</select><br/><SELECT style=\"width:75px;\" NAME=\"BQID2\"><OPTION VALUE=\"0\">&lt; none &gt;</OPTION>");

        rs = Db.rs("SELECT DISTINCT bq.BQID, bq.Variable FROM SponsorBQ sbq INNER JOIN BQ bq ON sbq.BQID = bq.BQID ORDER BY bq.Variable");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><br/>" +
            "<A HREF=\"javascript:exportRedirect(0);\">Export brief</A><br/><A HREF=\"javascript:exportRedirect(1);\">Export verbose</A><br/><A HREF=\"javascript:exportRedirect(3);\">Export absence/user/month</A><br/><A HREF=\"javascript:exportRedirect(4);\">Export absence/user</A>");

        sb.Append("</TD>" +
            "<TD>&nbsp;<b>Administrators</b></TD>" +
            "</TR>");
        int cx = 0, ssID = -1, totInvitees = 0, totNonClosedInvites = 0, totActive = 0, totNonClosedActive = 0;
        rs = Db.rs("SELECT " +
            "s.SponsorID, " +                     // 0
            "s.Sponsor, " +
            "LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8), " +
            "(SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = s.SponsorID), " +
            "(SELECT COUNT(*) FROM SponsorInvite si WHERE si.SponsorID = s.SponsorID), " +
            "(SELECT COUNT(*) FROM SponsorInvite si INNER JOIN [User] u ON si.UserID = u.UserID WHERE si.SponsorID = s.SponsorID), " +
            "(SELECT MIN(si.Sent) FROM SponsorInvite si WHERE si.SponsorID = s.SponsorID), " +
            "s.TreatmentOffer, " +
            "(SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = s.SponsorID AND ses.IndividualFeedbackID IS NOT NULL), " +
            "s.TreatmentOfferBQ, " +
            "s.TreatmentOfferIfNeededText, " +   // 10
            "s.InfoText, " +
            "s.ConsentText, " +
            "s.Closed, " +
            "ss.SuperSponsor, " +
            "ss.SuperSponsorID, " +              // 15
            "s.Comment, " +
            "ss.Comment, " +
            "(SELECT COUNT(*) FROM SponsorExtendedSurvey ses INNER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID WHERE pr.Started < GETDATE() AND (pr.Closed IS NULL OR pr.Closed > GETDATE()) AND ses.SponsorID = s.SponsorID), " +
            "(SELECT COUNT(*) FROM SponsorExtendedSurvey ses INNER JOIN eform..ProjectRound pr ON ses.ProjectRoundID = pr.ProjectRoundID WHERE pr.Started > GETDATE() AND ses.SponsorID = s.SponsorID), " +
            "SUBSTRING(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(s.InfoText AS NVARCHAR(MAX)))),3,32), " +       // 20
            "SUBSTRING(sys.fn_sqlvarbasetostr(HashBytes('MD5', CAST(s.ConsentText AS NVARCHAR(MAX)))),3,32), " +
            "LEN(CAST(s.InviteTxt AS NVARCHAR(MAX))), " +
            "LEN(CAST(s.InviteSubject AS NVARCHAR(MAX))), " +
            "(SELECT COUNT(*) FROM SponsorInvite si WHERE si.StoppedReason IS NULL AND si.SponsorID = s.SponsorID) " +
            "FROM Sponsor s " +
            "LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID " +
            "WHERE s.Deleted IS NULL " +
            "ORDER BY ss.SuperSponsor, ss.Comment, s.Sponsor");
        while (rs.Read())
        {
            if (ssID != (rs.IsDBNull(15) ? 0 : rs.GetInt32(15)))
            {
                ssID = (rs.IsDBNull(15) ? 0 : rs.GetInt32(15));
                sb.Append("<TR>" +
                    "<TD COLSPAN=\"7\">&nbsp;</TD>" +
                    "<TD>" + (totNonClosedInvites != totInvitees ? "<span style=\"text-decoration:line-through;color:#cc0000;\">" + totInvitees + "</span><br/>" : "") + "<b>" + totNonClosedInvites + "</b></TD>" +
                    "<TD>" + (totNonClosedActive != totActive ? "<span style=\"text-decoration:line-through;color:#cc0000;\">" + totActive + "</span><br/>" : "") + "<b>" + totNonClosedActive + "</b></TD>" +
                    "</TR>");
                sb.Append("<TR><TD COLSPAN=\"7\"><h3 style=\"margin-top:10px;margin-bottom:5px;\">" + (ssID == 0 ? "" : "<img src=\"" + System.Configuration.ConfigurationManager.AppSettings["hwURL"] + "/img/partner/" + ssID + ".gif\"/> ") + (rs.IsDBNull(14) ? "OTHER SPONSOR" : rs.GetString(14).ToUpper()) + (rs.IsDBNull(17) ? "" : " " + rs.GetString(17).ToUpper()) + "</h3></TD></TR>");
                totInvitees = 0;
                totNonClosedInvites = 0;
                totActive = 0;
                totNonClosedActive = 0;
            }
            totInvitees += rs.GetInt32(4);
            if (rs.IsDBNull(13))
            {
                totNonClosedInvites += rs.GetInt32(4);
            }
            totActive += rs.GetInt32(5);
            if (rs.IsDBNull(13))
            {
                totNonClosedActive += rs.GetInt32(5);
            }
            sb.Append("<TR" + (cx++ % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + ">" +
                "<TD><INPUT ONCLICK=\"document.forms[0].submit();\" TYPE=\"radio\" NAME=\"MergeFrom\" VALUE=\"" + rs.GetInt32(0) + "\"" + (HttpContext.Current.Request.Form["MergeFrom"] != null && Convert.ToInt32(HttpContext.Current.Request.Form["MergeFrom"]) == rs.GetInt32(0) ? " CHECKED" : "") + "/></TD>" +
                "<TD>" + (HttpContext.Current.Request.Form["MergeFrom"] != null && Convert.ToInt32(HttpContext.Current.Request.Form["MergeFrom"]) != rs.GetInt32(0) ? "<INPUT ONCLICK=\"document.forms[0].submit();\" TYPE=\"radio\" NAME=\"MergeTo\" VALUE=\"" + rs.GetInt32(0) + "\"" + (HttpContext.Current.Request.Form["MergeTo"] != null && Convert.ToInt32(HttpContext.Current.Request.Form["MergeTo"]) == rs.GetInt32(0) ? " CHECKED" : "") + "/>" : "") + "</TD>" +
                "<TD>" +
                    (ssID == 0 ? "<img src=\"" + System.Configuration.ConfigurationManager.AppSettings["hwURL"] + "/img/sponsor/" + rs.GetString(2) + rs.GetInt32(0).ToString() + ".gif\" height=\"16\"/>" : "") +
                    "<A" + (!rs.IsDBNull(13) ? " style=\"text-decoration:line-through;color:#cc0000;\"" : "") + " HREF=\"sponsorSetup.aspx?SponsorID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>&nbsp;&nbsp;</TD>" +
                "<TD style=\"font-size:9px;\"><a href=\"sponsor.aspx?Org=" + rs.GetInt32(0) + "\">¤</a>&nbsp;" + (rs.IsDBNull(11) ? "" : "I") + (rs.IsDBNull(12) ? "" : "C") + "&nbsp;</TD>" +
                "<TD>" +
                    "<A HREF=\"" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/default.aspx?SKEY=" + rs.GetString(2) + rs.GetInt32(0).ToString() + "\" TARGET=\"_blank\">GRP (" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURLtext"] + ")</A>" +
                    "&nbsp;" +
                    "<A HREF=\"" + System.Configuration.ConfigurationManager.AppSettings["hwStageGrpURL"] + "/default.aspx?SKEY=" + rs.GetString(2) + rs.GetInt32(0).ToString() + "\" TARGET=\"_blank\">(" + System.Configuration.ConfigurationManager.AppSettings["hwStageGrpURLtext"] + ")</A>" +
                    "&nbsp;" +
                    "<A HREF=\"javascript:if(confirm('Are you sure?')){location.href='sponsor.aspx?COPY=" + rs.GetInt32(0).ToString() + "';}\">Copy</A>" +
                    "&nbsp;&nbsp;" +
                    "<A HREF=\"javascript:if(confirm('Are you sure?')){location.href='sponsor.aspx?CONNECT=" + rs.GetInt32(0).ToString() + "';}\">Connect</A>" +
                    "&nbsp;&nbsp;" +
                "</TD>" +
                "<TD style=\"font-size:9px;\">" + (rs.IsDBNull(7) ? (rs.GetInt32(8) > 0 ? "<span style=\"color:#cc0000;\">" : "") + "No" + (rs.GetInt32(8) > 0 ? "</span>" : "") : (rs.GetInt32(8) != rs.GetInt32(3) ? "<span style=\"color:#cc0000;\">" : "") + "Yes" + (!rs.IsDBNull(7) ? "+O" : "") + (!rs.IsDBNull(9) ? "+A" : "") + (!rs.IsDBNull(10) ? "(C)" : "") + (rs.GetInt32(8) != rs.GetInt32(3) ? "</span>" : "")) + "</TD>" +
                "<TD" + (rs.GetInt32(18) > 0 ? " style=\"background-color:#CCFF99;\"" : (rs.GetInt32(19) > 0 ? " style=\"background-color:#FFFFCC;\"" : "")) + "><a href=\"" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/feedback.aspx?SponsorID=" + rs.GetInt32(0) + "\" title=\"Check feedback\" TARGET=\"_blank\">" + rs.GetInt32(3) + "/" + rs.GetInt32(8) + "</a></TD>" +
                "<TD><a href=\"sponsor.aspx?All=" + rs.GetInt32(0) + "\">" + rs.GetInt32(4) + "</a></TD>" +
                "<TD><a href=\"sponsor.aspx?Logins=" + rs.GetInt32(0) + "\">" + rs.GetInt32(5) + "</a> / " + rs.GetInt32(24) + (rs.GetInt32(4) != rs.GetInt32(5) ? "&nbsp;(<a href=\"sponsor.aspx?Inactive=" + rs.GetInt32(0) + "\">&#916;</a>)" : "") + "&nbsp;</TD>" +
                "<TD style=\"font-size:9px;\">" + (rs.IsDBNull(6) ? "N/A" : rs.GetDateTime(6).ToString("yyMMdd")) + "&nbsp;</TD>" +
                "<TD style=\"font-size:9px;\"><input type=\"checkbox\" name=\"ExportSponsorID\" value=\"" + rs.GetInt32(0) + "\"/>");
            //<SELECT style=\"width:75px;\" NAME=\"BQID" + rs.GetInt32(0) + "\"><OPTION VALUE=\"0\">Email</OPTION>");

            //SqlDataReader rs2 = Db.rs("SELECT bq.BQID, bq.Variable FROM SponsorBQ sbq INNER JOIN BQ bq ON sbq.BQID = bq.BQID WHERE sbq.SponsorID = " + rs.GetInt32(0) + " ORDER BY sbq.SortOrder");
            //while (rs2.Read())
            //{
            //    sb.Append("<OPTION VALUE=\"" + rs2.GetInt32(0) + "\">" + rs2.GetString(1) + "</OPTION>");
            //}
            //rs2.Close();
            
            sb.Append(
                //"</SELECT>" +
                //"<A HREF=\"javascript:location.href=" +
                //"'sponsor.aspx?EXPORT=" + rs.GetInt32(0) + "&BQID='" +
                //"+document.forms[0].BQID" + rs.GetInt32(0) + "[document.forms[0].BQID" + rs.GetInt32(0) + ".selectedIndex].value+" +
                //"'&AM='" +
                //"+document.forms[0].ActivityMonth[document.forms[0].ActivityMonth.selectedIndex].value;\">Export</A>" +
                "&nbsp;" + (rs.IsDBNull(16) ? "" : rs.GetString(16)) + "&nbsp;</TD><TD style=\"font-size:9px;\">");
            if (!rs.IsDBNull(13))
            {
                sb.Append("<span style=\"color:#cc0000;\">Closed " + rs.GetDateTime(13).ToString("yyyy-MM-dd") + "</span>");
            }
            else
            {
                int bx = 0;
                SqlDataReader rs2 = Db.rs("SELECT sa.Username, sas.SeeUsers FROM SuperAdmin sa INNER JOIN SuperAdminSponsor sas ON sa.SuperAdminID = sas.SuperAdminID WHERE sas.SponsorID = " + rs.GetInt32(0));
                while (rs2.Read())
                {
                    sb.Append((bx++ > 0 ? ", " : "") + (rs2.IsDBNull(1) ? "" : "<b>") + rs2.GetString(0) + (rs2.IsDBNull(1) ? "" : "</b>"));
                }
                rs2.Close();
            }
            sb.Append("" +
                "</TD>" +
                "<td style=\"font-size:9px;\">" + (rs.IsDBNull(20) ? "" : rs.GetString(20).Substring(0, 4)) + "</td>" +
                "<td style=\"font-size:9px;\">" + (rs.IsDBNull(21) ? "" : rs.GetString(21).Substring(0, 4)) + "</td>" +
                "<td style=\"font-size:9px;\">" + ((rs.IsDBNull(22) ? 0 : Convert.ToInt32(rs.GetValue(22)))+(rs.IsDBNull(23) ? 0 : Convert.ToInt32(rs.GetValue(23)))) + "</td>" +
                "</TR>");
        }
        rs.Close();
        sb.Append("<TR>" +
            "<TD COLSPAN=\"7\">&nbsp;</TD>" +
            "<TD>" + (totNonClosedInvites != totInvitees ? "<span style=\"text-decoration:line-through;color:#cc0000;\">" + totInvitees + "</span><br/>" : "") + "<b>" + totNonClosedInvites + "</b></TD>" +
            "<TD>" + (totNonClosedActive != totActive ? "<span style=\"text-decoration:line-through;color:#cc0000;\">" + totActive + "</span><br/>" : "") + "<b>" + totNonClosedActive + "</b></TD>" +
            "</TR><TR>" +
            "<TD COLSPAN=\"12\"><br/><SELECT NAME=\"ESID\"><OPTION VALUE=\"0\">&lt; ALL &gt;</OPTION>");

        rs = Db.rs("SELECT DISTINCT s.SurveyID, s.Internal FROM Survey s INNER JOIN ProjectRound p ON s.SurveyID = p.SurveyID INNER JOIN healthwatch..SponsorExtendedSurvey w ON p.ProjectRoundID = w.ProjectRoundID ORDER BY s.Internal", "eFormSqlConnection");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">[" + rs.GetInt32(0).ToString().PadLeft(3,'0') + "]" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><SELECT NAME=\"ELID\">");

        rs = Db.rs("SELECT DISTINCT l.LangID, l.Lang FROM Lang l INNER JOIN ProjectRound p ON l.LangID = p.LangID INNER JOIN healthwatch..SponsorExtendedSurvey w ON p.ProjectRoundID = w.ProjectRoundID", "eFormSqlConnection");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><A HREF=\"javascript:exportRedirect(2);\">Export extended surveys</A><br/><SELECT NAME=\"RSID\">");

        rs = Db.rs("SELECT DISTINCT " +
            "s.SurveyID, " +
            "s.Internal " +
            "FROM Survey s " +
            "INNER JOIN healthwatch..SponsorProjectRoundUnit w ON s.SurveyID = w.SurveyID " +
            "ORDER BY s.Internal", "eFormSqlConnection");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">[" + rs.GetInt32(0).ToString().PadLeft(3, '0') + "]" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><SELECT NAME=\"RLID\">");

        rs = Db.rs("SELECT DISTINCT l.LangID, l.Lang FROM Lang l", "eFormSqlConnection");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><A HREF=\"javascript:exportRedirect(5);\">Export repeated surveys</A> <A HREF=\"javascript:exportRedirect(7);\">Export repeated surveys, one variable per row</A><br/><SELECT NAME=\"ExportSPRUID\">");

        rs = Db.rs("SELECT " +
            "w.SponsorProjectRoundUnitID, " +
            "s.Sponsor, " +
            "w.Nav, " +
            "es.Internal " +
            "FROM Sponsor s " +
            "INNER JOIN SponsorProjectRoundUnit w ON s.SponsorID = ABS(w.SponsorID) " +
            "INNER JOIN eform..Survey es ON w.SurveyID = es.SurveyID " +
            "WHERE w.OnlyEveryDays IS NOT NULL " +
            "ORDER BY s.Sponsor");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">[" + rs.GetInt32(0).ToString().PadLeft(3, '0') + "]" + rs.GetString(1) + " / " + rs.GetString(2) + " / " + rs.GetString(3) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><A HREF=\"javascript:exportRedirect(8);\">Export hypergene format</A><br/><SELECT NAME=\"ExportSESID\">");

        rs = Db.rs("SELECT " +
            "w.SponsorExtendedSurveyID, " +
            "s.Sponsor, " +
            "w.Internal, " +
            "w.RoundText " +
            "FROM Sponsor s " +
            "INNER JOIN SponsorExtendedSurvey w ON s.SponsorID = ABS(w.SponsorID) " +
            "ORDER BY s.Sponsor, w.Internal, w.RoundText");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">[" + rs.GetInt32(0).ToString().PadLeft(3, '0') + "]" + rs.GetString(1) + (rs.IsDBNull(2) ? "" : " / " + rs.GetString(2)) + (rs.IsDBNull(3) ? "" : " / " + rs.GetString(3)) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><A HREF=\"javascript:exportRedirect(9);\">Export hypergene format</A><br/><SELECT NAME=\"ExportProjectRoundID\">");

        rs = Db.rs("SELECT " +
            "DISTINCT " +
            "p.ProjectRoundID, " +
            "q.Internal, " +
            "p.Internal " +
            "FROM Sponsor s " +
            "INNER JOIN SponsorExtendedSurvey w ON s.SponsorID = ABS(w.SponsorID) " +
            "INNER JOIN eform..ProjectRound pr ON w.ProjectRoundID = pr.ProjectRoundID " +
            "INNER JOIN eform..ProjectRound p ON pr.ProjectID = p.ProjectID " +
            "INNER JOIN eform..Project q ON q.ProjectID = p.ProjectID " +
            "ORDER BY q.Internal, p.Internal");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">[" + rs.GetInt32(0).ToString().PadLeft(3, '0') + "]" + rs.GetString(1) + (rs.IsDBNull(2) ? "" : " / " + rs.GetString(2)) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><A HREF=\"javascript:exportRedirect(10);\">Export hypergene format with eForm units and feedback questions</A><br/>");

        sb.Append("<A HREF=\"javascript:exportRedirect(11);\">Export invites in Viedoc format</A><br/><SELECT NAME=\"MLID\">");

        rs = Db.rs("SELECT DISTINCT l.LID, l.Language FROM LID l");
        while (rs.Read())
        {
            sb.Append("<OPTION VALUE=\"" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</OPTION>");
        }
        rs.Close();
        sb.Append("</SELECT><A HREF=\"javascript:exportRedirect(6);\">Export meta-data</A><br/><A HREF=\"javascript:exportRedirect(12);\">Export logins</A><br/><A HREF=\"javascript:exportRedirect(13);\">Export first login</A><br/></TD></TR>");

        Sponsor.Text = sb.ToString();
    }
}
