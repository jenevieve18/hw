using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserRepository : BaseSqlRepository<User>
	{
		public SqlUserRepository()
		{
		}
		
		public override void Save(User user)
		{
			string query = @"
INSERT INTO User(
	UserID, 
	Username, 
	Email, 
	Password, 
	Created, 
	UserKey, 
	SponsorID, 
	Reminder, 
	AttitudeSurvey, 
	UserProfileID, 
	DepartmentID, 
	ReminderLastSent, 
	EmailFailure, 
	ReminderType, 
	ReminderLink, 
	ReminderSettings, 
	ReminderNextSend, 
	LID, 
	AltEmail
)
VALUES(
	@UserID, 
	@Username, 
	@Email, 
	@Password, 
	@Created, 
	@UserKey, 
	@SponsorID, 
	@Reminder, 
	@AttitudeSurvey, 
	@UserProfileID, 
	@DepartmentID, 
	@ReminderLastSent, 
	@EmailFailure, 
	@ReminderType, 
	@ReminderLink, 
	@ReminderSettings, 
	@ReminderNextSend, 
	@LID, 
	@AltEmail
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserID", user.UserID),
				new SqlParameter("@Username", user.Username),
				new SqlParameter("@Email", user.Email),
				new SqlParameter("@Password", user.Password),
				new SqlParameter("@Created", user.Created),
				new SqlParameter("@UserKey", user.UserKey),
				new SqlParameter("@SponsorID", user.SponsorID),
				new SqlParameter("@Reminder", user.Reminder),
				new SqlParameter("@AttitudeSurvey", user.AttitudeSurvey),
				new SqlParameter("@UserProfileID", user.UserProfileID),
				new SqlParameter("@DepartmentID", user.DepartmentID),
				new SqlParameter("@ReminderLastSent", user.ReminderLastSent),
				new SqlParameter("@EmailFailure", user.EmailFailure),
				new SqlParameter("@ReminderType", user.ReminderType),
				new SqlParameter("@ReminderLink", user.ReminderLink),
				new SqlParameter("@ReminderSettings", user.ReminderSettings),
				new SqlParameter("@ReminderNextSend", user.ReminderNextSend),
				new SqlParameter("@LID", user.LID),
				new SqlParameter("@AltEmail", user.AltEmail)
			);
		}
		
		public override void Update(User user, int id)
		{
			string query = @"
UPDATE User SET
	UserID = @UserID,
	Username = @Username,
	Email = @Email,
	Password = @Password,
	Created = @Created,
	UserKey = @UserKey,
	SponsorID = @SponsorID,
	Reminder = @Reminder,
	AttitudeSurvey = @AttitudeSurvey,
	UserProfileID = @UserProfileID,
	DepartmentID = @DepartmentID,
	ReminderLastSent = @ReminderLastSent,
	EmailFailure = @EmailFailure,
	ReminderType = @ReminderType,
	ReminderLink = @ReminderLink,
	ReminderSettings = @ReminderSettings,
	ReminderNextSend = @ReminderNextSend,
	LID = @LID,
	AltEmail = @AltEmail
WHERE UserID = @UserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserID", user.UserID),
				new SqlParameter("@Username", user.Username),
				new SqlParameter("@Email", user.Email),
				new SqlParameter("@Password", user.Password),
				new SqlParameter("@Created", user.Created),
				new SqlParameter("@UserKey", user.UserKey),
				new SqlParameter("@SponsorID", user.SponsorID),
				new SqlParameter("@Reminder", user.Reminder),
				new SqlParameter("@AttitudeSurvey", user.AttitudeSurvey),
				new SqlParameter("@UserProfileID", user.UserProfileID),
				new SqlParameter("@DepartmentID", user.DepartmentID),
				new SqlParameter("@ReminderLastSent", user.ReminderLastSent),
				new SqlParameter("@EmailFailure", user.EmailFailure),
				new SqlParameter("@ReminderType", user.ReminderType),
				new SqlParameter("@ReminderLink", user.ReminderLink),
				new SqlParameter("@ReminderSettings", user.ReminderSettings),
				new SqlParameter("@ReminderNextSend", user.ReminderNextSend),
				new SqlParameter("@LID", user.LID),
				new SqlParameter("@AltEmail", user.AltEmail)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM User
WHERE UserID = @UserID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserID", id)
			);
		}
		
		public override User Read(int id)
		{
			string query = @"
SELECT 	UserID, 
	Username, 
	Email, 
	Password, 
	Created, 
	UserKey, 
	SponsorID, 
	Reminder, 
	AttitudeSurvey, 
	UserProfileID, 
	DepartmentID, 
	ReminderLastSent, 
	EmailFailure, 
	ReminderType, 
	ReminderLink, 
	ReminderSettings, 
	ReminderNextSend, 
	LID, 
	AltEmail
FROM User
WHERE UserID = @UserID";
			User user = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserID", id))) {
				if (rs.Read()) {
					user = new User {
						UserID = GetInt32(rs, 0),
						Username = GetString(rs, 1),
						Email = GetString(rs, 2),
						Password = GetString(rs, 3),
						Created = GetDateTime(rs, 4),
						UserKey = GetGuid(rs, 5),
						SponsorID = GetInt32(rs, 6),
						Reminder = GetInt32(rs, 7),
						AttitudeSurvey = GetDateTime(rs, 8),
						UserProfileID = GetInt32(rs, 9),
						DepartmentID = GetInt32(rs, 10),
						ReminderLastSent = GetDateTime(rs, 11),
						EmailFailure = GetDateTime(rs, 12),
						ReminderType = GetInt32(rs, 13),
						ReminderLink = GetInt32(rs, 14),
						ReminderSettings = GetString(rs, 15),
						ReminderNextSend = GetDateTime(rs, 16),
						LID = GetInt32(rs, 17),
						AltEmail = GetString(rs, 18)
					};
				}
			}
			return user;
		}
		
		public override IList<User> FindAll()
		{
			string query = @"
SELECT 	UserID, 
	Username, 
	Email, 
	Password, 
	Created, 
	UserKey, 
	SponsorID, 
	Reminder, 
	AttitudeSurvey, 
	UserProfileID, 
	DepartmentID, 
	ReminderLastSent, 
	EmailFailure, 
	ReminderType, 
	ReminderLink, 
	ReminderSettings, 
	ReminderNextSend, 
	LID, 
	AltEmail
FROM User";
			var users = new List<User>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					users.Add(new User {
						UserID = GetInt32(rs, 0),
						Username = GetString(rs, 1),
						Email = GetString(rs, 2),
						Password = GetString(rs, 3),
						Created = GetDateTime(rs, 4),
						UserKey = GetGuid(rs, 5),
						SponsorID = GetInt32(rs, 6),
						Reminder = GetInt32(rs, 7),
						AttitudeSurvey = GetDateTime(rs, 8),
						UserProfileID = GetInt32(rs, 9),
						DepartmentID = GetInt32(rs, 10),
						ReminderLastSent = GetDateTime(rs, 11),
						EmailFailure = GetDateTime(rs, 12),
						ReminderType = GetInt32(rs, 13),
						ReminderLink = GetInt32(rs, 14),
						ReminderSettings = GetString(rs, 15),
						ReminderNextSend = GetDateTime(rs, 16),
						LID = GetInt32(rs, 17),
						AltEmail = GetString(rs, 18)
					});
				}
			}
			return users;
		}
	}
}
