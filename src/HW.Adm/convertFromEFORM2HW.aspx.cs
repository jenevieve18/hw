using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using System.Data.SqlClient;

namespace HW.Adm
{
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

			string query = string.Format(
				@"
SELECT x.ProjectRoundUserID,
	x.Email,
	u.Unit
FROM ProjectRoundUser x
INNER JOIN ProjectRoundUnit u ON x.ProjectRoundUnitID = u.ProjectRoundUnitID
WHERE x.ProjectRoundUserID NOT IN ({0}) AND x.ProjectRoundUnitID NOT IN ({1}) AND x.ProjectRoundID = {2}",
				dontConvertUsers,
				dontConvertUnits,
				PRID
			);
			SqlDataReader rs = Db.rs(query, "eFormSqlConnection");
			while (rs.Read()) {
				Response.Write(rs.GetString(1) + " <BR>\r\n");

				#region createdepartment
				int departmentID = 0;
				query = string.Format(
					@"
SELECT DepartmentID
FROM Department
WHERE SponsorID = {0} AND Department = '{1}'",
					sponsorID,
					rs.GetString(2).Replace("'", "")
				);
				SqlDataReader rs2 = Db.rs(query);
				if (rs2.Read()) {
					departmentID = rs2.GetInt32(0);
				} else {
					rs2.Close();
					query = string.Format(
						@"
INSERT INTO Department (SponsorID,Department)
VALUES ({0},'{1}')",
						sponsorID,
						rs.GetString(2).Replace("'", "")
					);
					Db.exec(query);
					query = string.Format(
						@"
SELECT DepartmentID
FROM Department
WHERE SponsorID = {0} AND Department = '{1}'",
						sponsorID,
						rs.GetString(2).Replace("'", "")
					);
					rs2 = Db.rs(query);
					if (rs2.Read()) {
						query = string.Format(
							@"
UPDATE Department SET SortOrder = DepartmentID,
	SortString = dbo.cf_departmentSortString(DepartmentID)
WHERE DepartmentID = {0}",
							rs2.GetInt32(0)
						);
						Db.exec(query);
						departmentID = rs2.GetInt32(0);
					}
				}
				rs2.Close();
				#endregion

				#region createusername
				string usern = rs.GetString(1).Substring(0, 3);
				try {
					usern += rs.GetString(1).Substring(rs.GetString(1).IndexOf(".") + 1, 3);
				} catch (Exception) {
					usern += rs.GetString(1).Substring(0, 3);
				}
				usern = usern.Replace("'", "");
				bool usernameValid = false;
				while (!usernameValid) {
					query = string.Format(
						@"
SELECT UserID FROM [User] WHERE Username = '{0}'",
						usern
					);
					rs2 = Db.rs(query);
					if (!rs2.Read()) {
						usernameValid = true;
					} else {
						usern += "1";
					}
					rs2.Close();
				}
				#endregion

				string pass = Db.HashMD5(usern + "_AUTO_CREATED_PASSWORD");

				#region create profilecomparison
				int profileComparisonID = 0;
				query = string.Format(
					@"
SELECT ProfileComparisonID
FROM ProfileComparison WHERE Hash = '{0}'",
					Db.HashMD5("EMPTY")
				);
				rs2 = Db.rs(query);
				if (rs2.Read()) {
					profileComparisonID = rs2.GetInt32(0);
				}
				rs2.Close();
				if (profileComparisonID == 0) {
					query = string.Format(
						@"
INSERT INTO ProfileComparison (Hash) VALUES ('{0}')",
						Db.HashMD5("EMPTY")
					);
					Db.exec(query);
					query = string.Format(
						@"
SELECT TOP 1 ProfileComparisonID
FROM ProfileComparison WHERE Hash = '{0}'",
						Db.HashMD5("EMPTY")
					);
					rs2 = Db.rs(query);
					if (rs2.Read()) {
						profileComparisonID = rs2.GetInt32(0);
					}
					rs2.Close();
				}
				#endregion

				int userID = 0; string userKey = "";

				#region createuser
				query = string.Format(
					@"
INSERT INTO [User] (Username, Email, Password, SponsorID, DepartmentID, UserProfileID, ReminderLink, ReminderType)
VALUES ('{0}','{1}','{2}',{3},{4},0,1,0)",
					usern,
					rs.GetString(1).Replace("'", ""),
					pass,
					sponsorID,
					departmentID
				);
				Db.exec(query);
				query = string.Format(
					@"
SELECT UserID,
	LEFT(REPLACE(CONVERT(VARCHAR(255),UserKey),'-',''),12)
FROM [User] WHERE Email = '{0}' AND Username = '{1}' AND Password = '{2}'
ORDER BY UserID DESC",
					rs.GetString(1).Replace("'", ""),
					usern.Replace("'", ""),
					pass
				);
				rs2 = Db.rs(query);
				if (rs2.Read()) {
					userID = rs2.GetInt32(0);
					userKey = rs2.GetString(1);
				}
				rs2.Close();
				#endregion

				#region Create userprofile
				int userProfileID = 0;
				query = string.Format(
					@"
INSERT INTO UserProfile (UserID, SponsorID, DepartmentID, ProfileComparisonID)
VALUES ({0},{1},{2},{3})",
					userID,
					sponsorID,
					departmentID ,
					profileComparisonID
				);
				Db.exec(query);
				query = string.Format(
					@"
SELECT TOP 1 UserProfileID
FROM UserProfile WHERE UserID = {0} ORDER BY UserProfileID DESC",
					userID
				);
				rs2 = Db.rs(query);
				if (rs2.Read()) {
					userProfileID = rs2.GetInt32(0);
				}
				rs2.Close();
				#endregion

				query = string.Format(
					@"
UPDATE [User] SET UserProfileID = {0} WHERE UserID = {1}",
					userProfileID,
					userID
				);
				Db.exec(query);

				query = string.Format(
					@"
INSERT INTO UserProjectRoundUser (UserID,ProjectRoundUnitID,ProjectRoundUserID)
VALUES ({0},{1},{2})",
					userID,
					targetPRUID,
					rs.GetInt32(0)
				);
				Db.exec(query);
				query = string.Format(
					@"
SELECT AnswerID,
	EndDT,
	REPLACE(CONVERT(VARCHAR(255),AnswerKey),'-','')
FROM Answer WHERE EndDT IS NOT NULL AND ProjectRoundUserID = {0} ORDER BY EndDT ASC",
					rs.GetInt32(0)
				);
				rs2 = Db.rs(query, "eFormSqlConnection");
				while (rs2.Read()) {
					query = string.Format(
						@"
INSERT INTO UserProjectRoundUserAnswer (ProjectRoundUserID,AnswerKey,DT,UserProfileID,AnswerID)
VALUES ({0},'{1}','{2}',{3},{4})",
						rs.GetInt32(0),
						rs2.GetString(2).Replace("'", ""),
						rs2.GetDateTime(1).ToString("yyyy-MM-dd HH:mm"),
						userProfileID,
						rs2.GetInt32(0)
					);
					Db.exec(query);
				}
				rs2.Close();

				query = string.Format(
					@"
UPDATE ProjectRoundUser SET ProjectRoundID = {0}, ProjectRoundUnitID = {1} WHERE ProjectRoundUserID = {2}",
					targetPRID,
					targetPRUID,
					rs.GetInt32(0)
				);
				Db.exec(query, "eFormSqlConnection");
				query = string.Format(
					@"
UPDATE Answer SET ProjectRoundID = {0}, ProjectRoundUnitID = {1} WHERE ProjectRoundUserID = {2}",
					targetPRID,
					targetPRUID,
					rs.GetInt32(0)
				);
				Db.exec(query, "eFormSqlConnection");
				query = string.Format(
					@"
INSERT INTO SponsorInvite SELECT SponsorID, DepartmentID, Email, UserID, GETDATE(), NEWID() FROM [User] WHERE UserID = {0}",
					userID
				);
				Db.exec(query);

				System.Web.Mail.SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SmtpServer"];
				System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();
				mail.To = rs.GetString(1);
				mail.Bcc = "jens@interactivehealthgroup.com";
				mail.From = "info@healthwatch.se";
				mail.Subject = "Viktig information ang. Hälso- och stressenkäten";
				mail.Body = "Hej." +
					"\r\n" +
					"\r\n" +
					"Vi har nu överfört ditt enkätkonto på HealthWatch från prototypversionen till den skarpa versionen. För dig innebär detta tillgång till mer utförlig statistik, övningar, dagbok m.m." +
					"\r\n" +
					"\r\n" +
					"Klicka på länken nedan för att logga in." +
					"\r\n" +
					"https://www.healthwatch.se/c/" + userKey.ToLower() + userID.ToString() +
					"\r\n" +
					"\r\n" +
					"Vi rekommenderar att du börjar med att klicka på \"Ändra profil\" (i den mörkgrå listen längst upp). Här kan du ställa in din jämförelseprofil, välja ett lösenord samt ev. ändra ditt användarnamn som tillfälligt är satt till \"" + usern + "\"." +
					"\r\n" +
					"\r\n" +
					"Påminnelseintervall är satt på organisationsnivå till varannan vecka. Utöver detta kan du sätta ett eget påminnelseintervall under menynalternativet \"Påminnelser\"." +
					"\r\n" +
					"\r\n" +
					"Trevlig sommar önskar" +
					"\r\n" +
					"HealthWatch";
				System.Web.Mail.SmtpMail.Send(mail);
			}
			rs.Close();
		}
	}
}