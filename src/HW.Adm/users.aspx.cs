using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && HttpContext.Current.Request.QueryString["Search"] != null && HttpContext.Current.Request.QueryString["Search"].ToString() != "")
        {
            search.Text = HttpContext.Current.Request.QueryString["Search"].ToString();
        }

        //OK.Click += new EventHandler(OK_Click);
        SqlDataReader rs;

        if (HttpContext.Current.Request.QueryString["RemoveReminder"] != null)
        {
            Db.exec("UPDATE [User] SET Reminder = 0, ReminderNextSend = NULL WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["RemoveReminder"]));
            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.QueryString["ResetUSESID"] != null && HttpContext.Current.Request.QueryString["ResetPRUID"] != null)
        {
            Db.exec("UPDATE UserSponsorExtendedSurvey SET UserID = -ABS(UserID), SponsorExtendedSurveyID = -ABS(SponsorExtendedSurveyID), ProjectRoundUserID = -ABS(ProjectRoundUserID) WHERE UserSponsorExtendedSurveyID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["ResetUSESID"]));
            Db.exec("UPDATE ProjectRoundUser SET ProjectRoundID = -ABS(ProjectRoundID), ProjectRoundUnitID = -ABS(ProjectRoundUnitID) WHERE ProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["ResetPRUID"]), "eFormSqlConnection");
            Db.exec("UPDATE Answer SET ProjectRoundUserID = -ABS(ProjectRoundUserID), ProjectRoundID = -ABS(ProjectRoundID), ProjectRoundUnitID = -ABS(ProjectRoundUnitID) WHERE ProjectRoundUserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["ResetPRUID"]), "eFormSqlConnection");
            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.Form["Merge"] != null && HttpContext.Current.Request.Form["With"] != null)
        {
            int merge = Convert.ToInt32(HttpContext.Current.Request.Form["Merge"]);
            int with = Convert.ToInt32(HttpContext.Current.Request.Form["With"]);
            int departmentID = 0, sponsorID = 0, fromSponsorID = 0;

            rs = Db.rs("SELECT DepartmentID, SponsorID FROM [User] WHERE UserID = " + with);
            if (rs.Read())
            {
                departmentID = (rs.IsDBNull(0) ? -1 : rs.GetInt32(0));
                sponsorID = (rs.IsDBNull(1) ? -1 : rs.GetInt32(1));
            }
            rs.Close();
            rs = Db.rs("SELECT SponsorID FROM [User] WHERE UserID = " + merge);
            if (rs.Read())
            {
                fromSponsorID = (rs.IsDBNull(0) ? -1 : rs.GetInt32(0));
            }
            rs.Close();

            Db.exec("UPDATE [User] SET Username = Username + '_MERGED_WITH_" + with + "', Email = Email + 'DELETED', Reminder = 0, ReminderNextSend = NULL WHERE UserID = " + merge);
            Db.exec("UPDATE [SponsorInvite] SET UserID = NULL WHERE UserID = " + merge);

            Db.exec("UPDATE [Diary] SET UserID = " + with + " WHERE UserID = " + merge);
            Db.exec("UPDATE [ExerciseStats] SET UserID = " + with + " WHERE UserID = " + merge);
            Db.exec("UPDATE [UserMeasure] SET UserID = " + with + " WHERE UserID = " + merge);
            Db.exec("UPDATE [UserSponsorExtendedSurvey] SET UserID = " + with + " WHERE UserID = " + merge);
            Db.exec("UPDATE [UserProfile] SET DepartmentID = " + (departmentID == -1 ? "NULL" : departmentID.ToString()) + ", SponsorID = " + (sponsorID == -1 ? "NULL" : sponsorID.ToString()) + ", UserID = " + with + " WHERE UserID = " + merge);

            Db.rewritePRU(fromSponsorID, sponsorID, merge, with);

            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.QueryString["Undelete"] != null)
        {
            Db.exec("UPDATE [User] SET Username = REPLACE(Username,'DELETED',''), Email = REPLACE(Email,'DELETED','') WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Undelete"]));
            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.QueryString["Fake1SPIID"] != null)
        {
            Db.exec("UPDATE [SponsorInvite] SET Sent = DATEADD(month,-1,Sent) WHERE SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Fake1SPIID"]));
            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.QueryString["Fake2UID"] != null)
        {
            Db.exec("UPDATE [User] SET ReminderLastSent = DATEADD(month,-1,ReminderLastSent) WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Fake2UID"]));
            Db.exec("UPDATE [Session] SET DT = DATEADD(month,-1,DT), EndDT = DATEADD(month,-1,EndDT) WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Fake2UID"]));
            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.QueryString["DeleteSPIID"] != null)
        {
            int deleteSPIID = Convert.ToInt32(HttpContext.Current.Request.QueryString["DeleteSPIID"]);
            rs = Db.rs("SELECT si.UserID, si.SponsorID FROM SponsorInvite si WHERE si.SponsorInviteID = " + deleteSPIID);
            if (rs.Read() && !rs.IsDBNull(0))
            {
                Db.updateUserToPrivate(rs.GetInt32(0), rs.GetInt32(1));
            }
            rs.Close();
            Db.exec("UPDATE SponsorInvite SET SponsorID = -ABS(SponsorID), DepartmentID = -ABS(DepartmentID), UserID = -ABS(UserID) WHERE SponsorInviteID = " + deleteSPIID);
            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (HttpContext.Current.Request.QueryString["ConnectSPIID"] != null)
        {
            int connectSPIID = Convert.ToInt32(HttpContext.Current.Request.QueryString["ConnectSPIID"]);

            Db.connectSPI(connectSPIID);

            HttpContext.Current.Response.Redirect("users.aspx?Search=" + search.Text, true);
        }
        if (search.Text != "")
        {
            searchUser();
        }

        FindDupes.Click += new EventHandler(FindDupes_Click);
    }

    void FindDupes_Click(object sender, EventArgs e)
    {
        searchUser(true);
    }

    public static string cleanString(string s)
    {
        return System.Text.RegularExpressions.Regex.Replace(s.ToUpper(), "[^A-Z]", "");
    }
    //void OK_Click(object sender, EventArgs e)
    //{
    //    searchUser();
    //}
    private void searchUser()
    {
        searchUser(false);
    }
    private void searchUser(bool dupes)
    {
        SqlDataReader rs;

        if (dupes)
        {
            rs = Db.rs("SELECT TOP 1000 u.UserID, u.Email FROM [User] u WHERE u.DupeCheck IS NULL AND u.Email IS NOT NULL AND u.Email <> ''");
            while (rs.Read())
            {
                Db.exec("UPDATE [User] SET DupeCheck = '" + Db.HashMD5(cleanString(rs.GetString(1))) + "' WHERE UserID = " + rs.GetInt32(0));
            }
            rs.Close();

            rs = Db.rs("SELECT TOP 1000 u.SponsorInviteID, u.Email FROM [SponsorInvite] u WHERE u.DupeCheck IS NULL AND u.Email IS NOT NULL AND u.Email <> ''");
            while (rs.Read())
            {
                Db.exec("UPDATE [SponsorInvite] SET DupeCheck = '" + Db.HashMD5(cleanString(rs.GetString(1))) + "' WHERE SponsorInviteID = " + rs.GetInt32(0));
            }
            rs.Close();
        }

        string mergeEmail = "";
        int merge = (HttpContext.Current.Request.Form["Merge"] != null ? Convert.ToInt32(HttpContext.Current.Request.Form["Merge"]) : 0);
        if (merge != 0)
        {
            rs = Db.rs("SELECT Email FROM [User] WHERE UserID = " + merge);
            if (rs.Read())
            {
                mergeEmail = rs.GetString(0);
            }
            rs.Close();
        }

        list.Text = "";

        string sqlWhere = "", sqlJoin = ""; int bx = 0;
        rs = Db.rs("SELECT BQ.BQID FROM BQ WHERE BQ.Type = 2");
        while (rs.Read())
        {
            bx++;
            sqlJoin += "LEFT OUTER JOIN UserProfileBQ upbq" + bx + " ON up.UserProfileID = upbq" + bx + ".UserProfileID AND upbq" + bx + ".BQID = " + rs.GetInt32(0) + " ";
            sqlWhere += " OR upbq" + bx + ".ValueText LIKE '%" + search.Text.Replace("'", "").Replace("-", "%").Replace(" ", "%") + "%'";
        }
        rs.Close();

        rs = Db.rs("SELECT " +
            "u.Username, " +
            "u.Email, " +
            "u.Reminder, " +
            "s.Sponsor, " +
            "d.Department, " +
            "u.UserID, " +
            "LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)), " +
            "(SELECT COUNT(*) FROM [User] u2 WHERE u2.Email = u.Email), " +
            "si.SponsorInviteID " +
            "FROM [User] u " +
            "INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID " +
            "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
            "LEFT OUTER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID AND u.UserID = si.UserID " +
            sqlJoin +
            "LEFT OUTER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
            "WHERE u.Username NOT LIKE '%MERGED_WITH%' " +
            (dupes ? "AND u.Email IS NOT NULL AND u.Email <> '' AND (SELECT COUNT(*) FROM [User] u3 WHERE u3.DupeCheck IS NOT NULL AND u.DupeCheck = u3.DupeCheck) > 1 " : "") +
            "AND (u.Username LIKE '%" + search.Text.Replace("'", "") + "%' OR u.Email LIKE '%" + search.Text.Replace("'", "") + "%'" + sqlWhere + ") " +
            "ORDER BY u.DupeCheck, u.UserID");
        if (rs.Read())
        {
            int cx = 0;
            list.Text += "<TR><TD>Merge&nbsp;</TD><TD>With&nbsp;</TD><TD><B>Username</B>&nbsp;</TD><TD><B>Email</B>&nbsp;</TD><TD><B>Sponsor</B>&nbsp;</TD><TD><B>Department</B>&nbsp;</TD><TD><B>Reminder</B></TD><TD>&nbsp;</TD><TD><B title=\"Extended surveys\">ES</B></TD></TR>";
            do
            {
                list.Text += "<TR" + (rs.GetString(1).EndsWith("DELETED") ? " style=\"background-color:#FF6666;text-decoration:line-through;\"" : (cx++ % 2 == 0 ? " style=\"background-color:#FFF7D6;\"" : "")) + ">" +
                    "<TD>" + (rs.GetInt32(7) > 1 ? "<INPUT STYLE=\"margin:0;padding:0;\" TYPE=\"radio\" ONCLICK=\"document.forms[0].submit();\" NAME=\"Merge\" VALUE=\"" + rs.GetInt32(5) + "\"" + (merge == rs.GetInt32(5) ? " CHECKED" : "") + ">" : "") + "</TD>" +
                    "<TD>" + (mergeEmail.ToLower().Trim() == rs.GetString(1).ToLower().Trim() && merge != rs.GetInt32(5) ? "<INPUT STYLE=\"margin:0;padding:0;\" TYPE=\"radio\" ONCLICK=\"document.forms[0].submit();\" NAME=\"With\" VALUE=\"" + rs.GetInt32(5) + "\">" : "<span style=\"color:#dddddd;\">" + rs.GetInt32(5) + "</span>") + "</TD>" +
                    "<TD>" + rs.GetString(0).Replace("DELETED", "") + "&nbsp;</TD>" +
                    "<TD>" + rs.GetString(1).Replace("DELETED", "") + "&nbsp;</TD>" +
                    "<TD>" + rs.GetString(3) + (!rs.IsDBNull(8) ? " [<a href=\"users.aspx?Search=" + search.Text + "&DeleteSPIID=" + rs.GetInt32(8) + "\">disconnect</a>] [<a href=\"users.aspx?Search=" + search.Text + "&Fake1SPIID=" + rs.GetInt32(8) + "\">fake 1</a>] [<a href=\"users.aspx?Search=" + search.Text + "&Fake2UID=" + rs.GetInt32(5) + "\">fake 2</a>]" : "") + "&nbsp;</TD>" +
                    "<TD>" + (rs.IsDBNull(4) ? "-" : rs.GetString(4)) + "&nbsp;</TD>" +
                    "<TD>" + (rs.IsDBNull(2) || rs.GetInt32(2) == 0 ? "No" : "<A TITLE=\"Turn off reminder\" HREF=\"users.aspx?Search=" + search.Text + "&RemoveReminder=" + rs.GetInt32(5) + "\">Yes</A>") + "</TD>" +
                    (rs.GetString(1).EndsWith("DELETED") ? "<TD style=\"text-decoration:none;\">[<A HREF=\"users.aspx?Search=" + search.Text + "&Undelete=" + rs.GetInt32(5) + "\">Undelete</A>]</TD>" : "<TD>[<A HREF=\"https://www.healthwatch.se/a/" + rs.GetString(6) + rs.GetInt32(5).ToString() + "\" TARGET=\"_blank\">Log&nbsp;on</A>]</TD>") +
                    "<TD>";

                SqlDataReader rs2 = Db.rs("SELECT usesx.UserSponsorExtendedSurveyID, a.StartDT, a.EndDT, usesx.ProjectRoundUserID FROM UserSponsorExtendedSurvey usesx INNER JOIN eform..Answer a ON usesx.ProjectRoundUserID = a.ProjectRoundUserID WHERE usesx.UserID = " + rs.GetInt32(5));
                while (rs2.Read())
                {
                    list.Text += "<a title=\"Reset survey:\nStarted " + rs2.GetDateTime(1).ToString("yyyy-MM-dd HH:mm:ss") + (!rs2.IsDBNull(2) ? "\nEnded " + rs2.GetDateTime(2).ToString("yyyy-MM-dd HH:mm:ss") : "") + "\" href=\"javascript:if(confirm('Are you sure you want to reset this?')){location.href='users.aspx?ResetPRUID=" + rs2.GetInt32(3) + "&ResetUSESID=" + rs2.GetInt32(0) + "&Search=" + search.Text + "';}\">" + rs2.GetInt32(0) + "</a> ";
                }
                rs2.Close();
                
                list.Text += "&nbsp;</td>" +
                    "</TR>";
            }
            while (rs.Read());
        }
        rs.Close();

        rs = Db.rs("SELECT " +
            "y.Username, " +
            "u.Email, " +
            "y.Reminder, " +
            "s.Sponsor, " +
            "d.Department, " +
            "y.UserID, " +
            "LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),y.UserKey),'-',''),12)), " +
            "(SELECT COUNT(*) FROM [User] u2 WHERE u2.Email = u.Email), " +
            "u.SponsorInviteID, " +
            "y.Email " +
            "FROM [SponsorInvite] u " +
            "INNER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
            "INNER JOIN Department d ON u.DepartmentID = d.DepartmentID " +
            "LEFT OUTER JOIN [User] y ON u.UserID = y.UserID " +
            "WHERE " +
            (dupes ? 
            "u.Email IS NOT NULL AND u.Email <> '' AND (SELECT COUNT(*) FROM [SponsorInvite] u3 WHERE u3.SponsorID > 0 AND u3.DupeCheck IS NOT NULL AND u.DupeCheck = u3.DupeCheck) > 1 " 
            : 
            "(y.UserID IS NULL OR y.Email <> u.Email) "
            ) +
            "AND u.Email LIKE '%" + search.Text.Replace("'", "") + "%' " +
            "ORDER BY u.DupeCheck, u.UserID");
        if (rs.Read())
        {
            list.Text += "<TR><TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD><B>Username</B>&nbsp;</TD><TD><B>Email</B>&nbsp;</TD><TD><B>Sponsor</B>&nbsp;</TD><TD><B>Department</B>&nbsp;</TD><TD><B>Reminder</B></TD></TR>";
            do
            {
                list.Text += "<TR><TD>&nbsp;</TD><TD>&nbsp;</TD><TD>" + (rs.IsDBNull(0) ? "&lt; not reg &gt;" : rs.GetString(0)) + "&nbsp;</TD><TD>" + rs.GetString(1) + (!rs.IsDBNull(9) && rs.GetString(1) != rs.GetString(9) ? " (" + rs.GetString(9) + ")" : "") + "&nbsp;</TD>" +
                "<TD>" + rs.GetString(3) + (!rs.IsDBNull(8) ? " [<a href=\"users.aspx?Search=" + search.Text + "&DeleteSPIID=" + rs.GetInt32(8) + "\">disconnect</a>] [<a href=\"users.aspx?Search=" + search.Text + "&Fake1SPIID=" + rs.GetInt32(8) + "\">fake 1</a>]" : "") + "&nbsp;</TD>" +
                "<TD>" + (rs.IsDBNull(4) ? "-" : rs.GetString(4)) + "&nbsp;</TD><TD>" + (rs.IsDBNull(2) || rs.GetInt32(2) == 0 ? "No" : "Yes") + "</TD><TD>" + (rs.IsDBNull(0) ? (rs.GetInt32(7) > 0 ? "[<a href=\"users.aspx?Search=" + search.Text + "&ConnectSPIID=" + rs.GetInt32(8) + "\">connect</a>]" : "&lt; not reg &gt;") : "[<A HREF=\"https://www.healthwatch.se/a/" + rs.GetString(6) + rs.GetInt32(5).ToString() + "\" TARGET=\"_blank\">Log&nbsp;on</A>]") + "</TD></TR>";
            }
            while (rs.Read());
        }
        rs.Close();
    }
}
