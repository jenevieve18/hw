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

public partial class convertFromEFORM2HW : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int PRID = 13;
        int targetPRUID = 911;
        int targetPRID = 14;
        int sponsorID = 4;
        string dontConvertUnits = "97";
        string dontConvertUsers = "20117";

        SqlDataReader rs = Db.rs("SELECT " +
            "x.ProjectRoundUserID, " +
            "x.Email, " +
            "u.Unit " +
            "FROM ProjectRoundUser x " +
            "INNER JOIN ProjectRoundUnit u ON x.ProjectRoundUnitID = u.ProjectRoundUnitID " +
            "WHERE x.ProjectRoundUserID NOT IN (" + dontConvertUsers + ") AND x.ProjectRoundUnitID NOT IN (" + dontConvertUnits + ") AND x.ProjectRoundID = " + PRID, "eFormSqlConnection");
        while (rs.Read())
        {
            HttpContext.Current.Response.Write(rs.GetString(1) + " <BR>\r\n");

            #region createdepartment
            int departmentID = 0;
            SqlDataReader rs2 = Db.rs("SELECT DepartmentID FROM Department WHERE SponsorID = " + sponsorID + " AND Department = '" + rs.GetString(2).Replace("'","") + "'");
            if(rs2.Read())
            {
                departmentID = rs2.GetInt32(0);
            }
            else
            {
                rs2.Close();
                Db.exec("INSERT INTO Department (SponsorID,Department) VALUES (" + sponsorID + ",'" + rs.GetString(2).Replace("'","") + "')");
                rs2 = Db.rs("SELECT DepartmentID FROM Department WHERE SponsorID = " + sponsorID + " AND Department = '" + rs.GetString(2).Replace("'","") + "'");
                if(rs2.Read())
                {
                    Db.exec("UPDATE Department SET SortOrder = DepartmentID, SortString = dbo.cf_departmentSortString(DepartmentID) WHERE DepartmentID = " + rs2.GetInt32(0));
                    departmentID = rs2.GetInt32(0);
                }
            }
            rs2.Close();
            #endregion

            #region createusername
            string usern = rs.GetString(1).Substring(0, 3);
            try
            {
                usern += rs.GetString(1).Substring(rs.GetString(1).IndexOf(".") + 1, 3);
            }
            catch (Exception)
            {
                usern += rs.GetString(1).Substring(0, 3);
            }
            usern = usern.Replace("'", "");
            bool usernameValid = false;
            while(!usernameValid)
            {
                rs2 = Db.rs("SELECT UserID FROM [User] WHERE Username = '" + usern + "'");
                if(!rs2.Read())
                {
                    usernameValid = true;
                }
                else
                {
                    usern += "1";
                }
                rs2.Close();
            }
            #endregion

            string pass = Db.HashMD5(usern + "_AUTO_CREATED_PASSWORD");

            #region create profilecomparison
            int profileComparisonID = 0;
            rs2 = Db.rs("SELECT ProfileComparisonID FROM ProfileComparison WHERE Hash = '" + Db.HashMD5("EMPTY") + "'");
            if (rs2.Read())
            {
                profileComparisonID = rs2.GetInt32(0);
            }
            rs2.Close();
            if (profileComparisonID == 0)
            {
                Db.exec("INSERT INTO ProfileComparison (Hash) VALUES ('" + Db.HashMD5("EMPTY") + "')");
                rs2 = Db.rs("SELECT TOP 1 ProfileComparisonID FROM ProfileComparison WHERE Hash = '" + Db.HashMD5("EMPTY") + "'");
                if (rs2.Read())
                {
                    profileComparisonID = rs2.GetInt32(0);
                }
                rs2.Close();
            }
            #endregion

            int userID = 0; string userKey = "";

            #region createuser
            Db.exec("INSERT INTO [User] (Username, Email, Password, SponsorID, DepartmentID, UserProfileID, ReminderLink, ReminderType) VALUES ('" + usern + "','" + rs.GetString(1).Replace("'", "") + "','" + pass + "'," + sponsorID + "," + departmentID + ",0,1,0)");
            rs2 = Db.rs("SELECT UserID, LEFT(REPLACE(CONVERT(VARCHAR(255),UserKey),'-',''),12) FROM [User] WHERE Email = '" + rs.GetString(1).Replace("'","") + "' AND Username = '" + usern.Replace("'", "") + "' AND Password = '" + pass + "' ORDER BY UserID DESC");
            if (rs2.Read())
            {
                userID = rs2.GetInt32(0);
                userKey = rs2.GetString(1);
            }
            rs2.Close();
            #endregion

            #region Create userprofile
            int userProfileID = 0;
            Db.exec("INSERT INTO UserProfile (UserID, SponsorID, DepartmentID, ProfileComparisonID) VALUES (" + userID + "," + sponsorID + "," + departmentID + "," + profileComparisonID + ")");
            rs2 = Db.rs("SELECT TOP 1 UserProfileID FROM UserProfile WHERE UserID = " + userID + " ORDER BY UserProfileID DESC");
            if (rs2.Read())
            {
                userProfileID = rs2.GetInt32(0);
            }
            rs2.Close();
            #endregion

            Db.exec("UPDATE [User] SET UserProfileID = " + userProfileID + " WHERE UserID = " + userID);

            Db.exec("INSERT INTO UserProjectRoundUser (UserID,ProjectRoundUnitID,ProjectRoundUserID) VALUES (" + userID + "," + targetPRUID + "," + rs.GetInt32(0) + ")");
            rs2 = Db.rs("SELECT AnswerID, EndDT, REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','') FROM Answer WHERE EndDT IS NOT NULL AND ProjectRoundUserID = " + rs.GetInt32(0) + " ORDER BY EndDT ASC", "eFormSqlConnection");
            while (rs2.Read())
            {
                Db.exec("INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID,AnswerKey,DT,UserProfileID,AnswerID) VALUES (" + rs.GetInt32(0) + ",'" + rs2.GetString(2).Replace("'","") + "','" + rs2.GetDateTime(1).ToString("yyyy-MM-dd HH:mm") + "'," + userProfileID + "," + rs2.GetInt32(0) + ")");
            }
            rs2.Close();

            Db.exec("UPDATE ProjectRoundUser SET ProjectRoundID = " + targetPRID + ", ProjectRoundUnitID = " + targetPRUID + " WHERE ProjectRoundUserID = " + rs.GetInt32(0), "eFormSqlConnection");
            Db.exec("UPDATE Answer SET ProjectRoundID = " + targetPRID + ", ProjectRoundUnitID = " + targetPRUID + " WHERE ProjectRoundUserID = " + rs.GetInt32(0), "eFormSqlConnection");
            Db.exec("INSERT INTO SponsorInvite SELECT SponsorID, DepartmentID, Email, UserID, GETDATE(), NEWID() FROM [User] WHERE UserID = " + userID);

            System.Web.Mail.SmtpMail.SmtpServer = System.Configuration.ConfigurationSettings.AppSettings["SmtpServer"];
            System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
            mail.To = rs.GetString(1);
            mail.Bcc = "jens@interactivehealthgroup.com";
            mail.From = "info@healthwatch.se";
            mail.Subject = "Viktig information ang. H�lso- och stressenk�ten";
            mail.Body = "Hej." +
                "\r\n" +
                "\r\n" +
                "Vi har nu �verf�rt ditt enk�tkonto p� HealthWatch fr�n prototypversionen till den skarpa versionen. F�r dig inneb�r detta tillg�ng till mer utf�rlig statistik, �vningar, dagbok m.m." +
                "\r\n" +
                "\r\n" +
                "Klicka p� l�nken nedan f�r att logga in." +
                "\r\n" +
                "https://www.healthwatch.se/c/" + userKey.ToLower() + userID.ToString() +
                "\r\n" +
                "\r\n" +
                "Vi rekommenderar att du b�rjar med att klicka p� \"�ndra profil\" (i den m�rkgr� listen l�ngst upp). H�r kan du st�lla in din j�mf�relseprofil, v�lja ett l�senord samt ev. �ndra ditt anv�ndarnamn som tillf�lligt �r satt till \"" + usern + "\"." +
                "\r\n" +
                "\r\n" +
                "P�minnelseintervall �r satt p� organisationsniv� till varannan vecka. Ut�ver detta kan du s�tta ett eget p�minnelseintervall under menynalternativet \"P�minnelser\"." +
                "\r\n" +
                "\r\n" +
                "Trevlig sommar �nskar" +
                "\r\n" +
                "HealthWatch";
            System.Web.Mail.SmtpMail.Send(mail);
        }
        rs.Close();
    }
}
