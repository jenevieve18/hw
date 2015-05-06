using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;

/// <summary>
/// Summary description for Db
/// </summary>
public class Db
{
    public static void updateUserToPrivate(int userID, int sponsorID)
    {
        int profileID = 0;
        SqlDataReader rs2 = Db.rs("SELECT u.UserProfileID, up.ProfileComparisonID FROM [User] u INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID WHERE u.SponsorID = " + sponsorID + " AND u.UserID = " + userID);
        while (rs2.Read())
        {
            #region Create new profile
            Db.exec("INSERT INTO UserProfile (UserID,SponsorID,DepartmentID,ProfileComparisonID,Created) VALUES (" + userID + ",1,NULL," + rs2.GetInt32(1) + ",GETDATE())");
            SqlDataReader rs3 = Db.rs("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + userID + " ORDER BY UserProfileID DESC");
            if (rs3.Read())
            {
                profileID = rs3.GetInt32(0);
            }
            rs3.Close();
            #endregion

            #region Copy old profile
            rs3 = Db.rs("SELECT BQID, ValueInt, ValueText, ValueDate FROM UserProfileBQ WHERE UserProfileID = " + rs2.GetInt32(0));
            while (rs3.Read())
            {
                Db.exec("INSERT INTO UserProfileBQ (UserProfileID,BQID,ValueInt,ValueText,ValueDate) VALUES (" + profileID + "," + rs3.GetInt32(0) + "," +
                    (rs3.IsDBNull(1) ? "NULL" : rs3.GetInt32(1).ToString()) + "," +
                    (rs3.IsDBNull(2) ? "NULL" : "'" + rs3.GetString(2).Replace("'", "") + "'") + "," +
                    (rs3.IsDBNull(3) ? "NULL" : "'" + rs3.GetDateTime(3).ToString("yyyy-MM-dd") + "'") +
                    ")");
            }
            rs3.Close();
            #endregion
        }
        rs2.Close();

        Db.exec("UPDATE [User] SET UserProfileID = " + profileID + ", DepartmentID = NULL, SponsorID = 1 WHERE SponsorID = " + sponsorID + " AND UserID = " + userID);
    }
    public static void connectSPI(int connectSPIID)
    {
        int sponsorID = 0, userID = 0, departmentID = 0, fromSponsorID = 0; string email = "";
        SqlDataReader rs = Db.rs("SELECT SponsorID, Email, DepartmentID FROM SponsorInvite WHERE UserID IS NULL AND SponsorInviteID = " + connectSPIID);
        if (rs.Read())
        {
            sponsorID = rs.GetInt32(0);
            email = rs.GetString(1);
            departmentID = rs.GetInt32(2);
        }
        rs.Close();

        if (sponsorID != 0)
        {
            rs = Db.rs("SELECT " +
                "u2.UserID, " +
                "u2.SponsorID " +
                "FROM [User] u2 " +
                "LEFT OUTER JOIN SponsorInvite si ON u2.UserID = si.UserID " +
                "WHERE u2.Email = '" + email.Replace("'", "''") + "' OR si.Email = '" + email.Replace("'", "''") + "'" + 
                "");
            if (rs.Read())
            {
                userID = rs.GetInt32(0);
                fromSponsorID = rs.GetInt32(1);

                Db.rewritePRU(fromSponsorID, sponsorID, userID);
                Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE UserID = " + userID);
                Db.exec("UPDATE SponsorInvite SET UserID = " + userID + ", Sent = GETDATE() WHERE SponsorInviteID = " + connectSPIID);
                Db.exec("UPDATE [User] SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID);
                Db.exec("UPDATE UserProfile SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID);
                
                while (rs.Read())
                {
                    userID = rs.GetInt32(0);
                    Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE UserID = " + userID);
                    Db.exec("UPDATE [User] SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID);
                    Db.exec("UPDATE UserProfile SET DepartmentID = NULL, SponsorID = 1 WHERE UserID = " + userID);
                } 
            }
            rs.Close();

            Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE Email = '" + email.Replace("'", "") + "' AND SponsorInviteID <> " + connectSPIID);
        }
    }
    public static void rewritePRU(int fromSponsorID, int sponsorID, int userID)
    {
        rewritePRU(fromSponsorID, sponsorID, userID, 0);
    }
    public static void rewritePRU(int fromSponsorID, int sponsorID, int fromUserID, int userID)
    {
        SqlDataReader rs = Db.rs("SELECT " +
            "spru.ProjectRoundUnitID, " +
            "spru.SurveyID, " +
            (userID != 0 ? "upru.ProjectRoundUserID " : "NULL ") +
            "FROM SponsorProjectRoundUnit spru " +
            (userID != 0 ? "INNER JOIN UserProjectRoundUser upru ON spru.ProjectRoundUnitID = upru.ProjectRoundUnitID AND upru.UserID = " + userID + " " : "") +
            "WHERE spru.SponsorID = " + sponsorID);
        while (rs.Read())
        {
            SqlDataReader rs2 = Db.rs("SELECT " +
                "upru.UserProjectRoundUserID, " +
                "upru.ProjectRoundUserID " +
                "FROM UserProjectRoundUser upru " +
                "INNER JOIN [user] hu ON upru.UserID = hu.UserID " +
                "INNER JOIN [eform]..[ProjectRoundUser] pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID " +
                "INNER JOIN [eform]..[ProjectRoundUnit] u ON pru.ProjectRoundUnitID = u.ProjectRoundUnitID " +
                "WHERE hu.SponsorID = " + fromSponsorID + " " +
                "AND u.SurveyID = " + rs.GetInt32(1) + " " +
                "AND upru.UserID = " + fromUserID);
            while (rs2.Read())
            {
                if (userID != 0)
                {
                    Db.exec("UPDATE UserProjectRoundUserAnswer SET ProjectRoundUserID = " + rs.GetInt32(2) + " WHERE ProjectRoundUserID = " + rs2.GetInt32(1));
                }
                Db.exec("UPDATE UserProjectRoundUser SET ProjectRoundUnitID = " + rs.GetInt32(0) + " WHERE UserProjectRoundUserID = " + rs2.GetInt32(0));
                Db.exec("UPDATE [eform]..[ProjectRoundUser] SET ProjectRoundUnitID = " + rs.GetInt32(0) + " WHERE ProjectRoundUserID = " + rs2.GetInt32(1));
                Db.exec("UPDATE [eform]..[Answer] SET ProjectRoundUnitID = " + rs.GetInt32(0) + (userID != 0 ? ", ProjectRoundUserID = " + rs.GetInt32(2) + "" : "") + " WHERE ProjectRoundUserID = " + rs2.GetInt32(1));
            }
            rs2.Close();
        }
        rs.Close();

        if (userID != 0)
        {
            Db.exec("UPDATE UserProjectRoundUser SET UserID = -ABS(UserID) WHERE UserID = " + fromUserID);
        }
    }

    public static int createProjectRoundUnit(int parentProjectRoundUnitID, string name, int SID, int individualReportID, int reportID)
    {
        int ID = 0;

        SqlDataReader rs = Db.rs("SELECT ProjectRoundID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + parentProjectRoundUnitID, "eFormSqlConnection");
        if (rs.Read())
        {
            Db.exec("INSERT INTO ProjectRoundUnit (" +
                "UserCount," +
                "LangID," +
                "SurveyID," +
                "ProjectRoundID," +
                "Unit," +
                "ParentProjectRoundUnitID," +
                "IndividualReportID," +
                "ReportID" +
                ") VALUES (" +
                "0," +
                "0," +
                "" + SID + "," +
                "" + rs.GetInt32(0) + "," +
                "'" + name.Replace("'", "''") + "'," +
                "" + parentProjectRoundUnitID + "," +
                "" + individualReportID + "," +
                "" + reportID + "" +
                ")", "eFormSqlConnection");
            SqlDataReader rs2 = Db.rs("SELECT ProjectRoundUnitID FROM [ProjectRoundUnit] " +
                "WHERE ProjectRoundID=" + rs.GetInt32(0) + " AND Unit = '" + name.Replace("'", "''") + "' " +
                "ORDER BY ProjectRoundUnitID DESC", "eFormSqlConnection");
            if (rs2.Read())
            {
                ID = rs2.GetInt32(0);
            }
            rs2.Close();
        }
        rs.Close();

        Db.exec("UPDATE ProjectRoundUnit " +
            "SET ID = dbo.cf_unitExtID(" + ID + ",dbo.cf_unitDepth(" + ID + "),''), " +
            "SortOrder = " + ID + " " +
            "WHERE ProjectRoundUnitID = " + ID, "eFormSqlConnection");

        Db.exec("UPDATE ProjectRoundUnit SET SortString = dbo.cf_unitSortString(ProjectRoundUnitID) WHERE ProjectRoundUnitID = " + ID, "eFormSqlConnection");

        return ID;
    }

    public static SqlDataReader rs(string sqlString)
    {
        return rs(sqlString, "SqlConnection");
    }
    public static SqlDataReader rs(string sqlString, string con)
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
        dataConnection.Open();
        SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
        dataCommand.CommandTimeout = 300;
        SqlDataReader dataReader = dataCommand.ExecuteReader(CommandBehavior.CloseConnection);
        return dataReader;
    }
    public static void exec(string sqlString)
    {
        exec(sqlString, "SqlConnection");
    }
    public static void exec(string sqlString, string con)
    {
        SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
        dataConnection.Open();
        SqlCommand dataCommand = new SqlCommand(sqlString, dataConnection);
        dataCommand.ExecuteNonQuery();
        dataConnection.Close();
        dataConnection.Dispose();
    }
	public static int getInt32(string sqlString)
	{
		return getInt32(sqlString, "SqlConnection");
	}
	public static int getInt32(string sqlString, string con)
	{
		int returnValue = 0;
		SqlConnection dataConnection = new SqlConnection(ConfigurationSettings.AppSettings[con]);
		dataConnection.Open();
		SqlCommand dataCommand = new SqlCommand(sqlString.Replace("\\", "\\\\"), dataConnection);
		dataCommand.CommandTimeout = 900;
		SqlDataReader dataReader = dataCommand.ExecuteReader();
		if (dataReader.Read())
			if (!dataReader.IsDBNull(0))
				returnValue = Convert.ToInt32(dataReader.GetValue(0));
		dataReader.Close();
		dataConnection.Close();
		dataConnection.Dispose();
		return returnValue;
	}
    public static string HashMD5(string str)
    {
        System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashByteArray = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("HW" + str + "HW"));
        string hash = "";
        for (int i = 0; i < hashByteArray.Length; i++)
            hash += hashByteArray[i];
        return hash;
    }
	public static string header()
    {
        string ret = "<TITLE>HealthWatch</TITLE>";
        ret += "<link href=\"main.css?V=" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\" rel=\"stylesheet\" type=\"text/css\">";
        ret += "<meta http-equiv=\"Pragma\" content=\"no-cache\">";
        ret += "<meta http-equiv=\"Expires\" content=\"-1\">";
        ret += "<meta name=\"Robots\" content=\"noarchive\">";
        ret += "<script language=\"JavaScript\">window.history.forward(1);</script>";

        return ret;
    }

    public static bool isEmail(string inputEmail)
    {
        string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        Regex re = new Regex(strRegex);
        if (re.IsMatch(inputEmail))
            return true;
        else
            return false;
    }

    public static string bottom()
    {
        return "</div></div>";
    }

    public static string nav()
    {
        SqlDataReader r;

        string ret = "<div id=\"main\">";

        ret += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        ret += "<tr>";
        ret += "<td><img src=\"img/null.gif\" width=\"150\" height=\"125\" border=\"0\" USEMAP=\"#top\"><MAP NAME=\"top\"><AREA SHAPE=\"poly\" COORDS=\"11,14, 11,90, 181,88, 179,16\" HREF=\"default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\"></MAP></td>";
        ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
        ret += "<td>" +
            "<A class=\"unli\" HREF=\"news.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">News</A><br/>" +
            "<A class=\"unli\" HREF=\"rss.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">News setup</A><br/>" +
            "<A class=\"unli\" HREF=\"sponsor.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Sponsors</A><br/>" +
            "<A class=\"unli\" HREF=\"grpUser.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Sponsor managers</A><br/>" +
            "<A class=\"unli\" HREF=\"sponsorStats.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Sponsor statistics</A><br/>" +
            "<A class=\"unli\" HREF=\"superAdmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Super managers</A><br/>" +
            "</td>";
        ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
        ret += "<td>" +
            "<A class=\"unli\" HREF=\"bq.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Background questions</A><br/>" +
            "<A class=\"unli\" HREF=\"messages.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Messages</A><br/>" +
            "<A class=\"unli\" HREF=\"export.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Data export</A><br/>" +
            "<A class=\"unli\" HREF=\"stats.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Usage statistics</A><br/>" +
            "<A class=\"unli\" HREF=\"tx.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">File uploads</A><br/>" +
            "<A class=\"unli\" HREF=\"issue.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Issues</A><br/>" +
            "</td>";
        ret += "<td><img src=\"img/null.gif\" width=\"25\" height=\"1\"></td>";
        ret += "<td>" +
            "<A class=\"unli\" HREF=\"exercise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Exercises</A><br/>" +
            "<A class=\"unli\" HREF=\"users.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Users</A><br/>" +
            "<A class=\"unli\" HREF=\"extendedSurvey.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Extended survey statistics</A><br/>" +
            "<A class=\"unli\" HREF=\"survey.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Survey statistics</A><br/>" +
            "<A class=\"unli\" HREF=\"wise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "\">Words of wisdom</A><br/>" +
            "<br/>" +
            "</td>";
        ret += "</tr>";
        ret += "</table>";
       
        ret += "<div id=\"container\">";

        return ret;
    }
}
