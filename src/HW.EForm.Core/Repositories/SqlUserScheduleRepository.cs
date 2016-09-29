using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUserScheduleRepository : BaseSqlRepository<UserSchedule>
	{
		public SqlUserScheduleRepository()
		{
		}
		
		public override void Save(UserSchedule userSchedule)
		{
			string query = @"
INSERT INTO UserSchedule(
	UserScheduleID, 
	UserID, 
	UserProjectRoundUserID, 
	DT, 
	SponsorReminderID, 
	Reminder, 
	Note, 
	Email, 
	SentDT, 
	NoteJapaneseUnicode
)
VALUES(
	@UserScheduleID, 
	@UserID, 
	@UserProjectRoundUserID, 
	@DT, 
	@SponsorReminderID, 
	@Reminder, 
	@Note, 
	@Email, 
	@SentDT, 
	@NoteJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserScheduleID", userSchedule.UserScheduleID),
				new SqlParameter("@UserID", userSchedule.UserID),
				new SqlParameter("@UserProjectRoundUserID", userSchedule.UserProjectRoundUserID),
				new SqlParameter("@DT", userSchedule.DT),
				new SqlParameter("@SponsorReminderID", userSchedule.SponsorReminderID),
				new SqlParameter("@Reminder", userSchedule.Reminder),
				new SqlParameter("@Note", userSchedule.Note),
				new SqlParameter("@Email", userSchedule.Email),
				new SqlParameter("@SentDT", userSchedule.SentDT),
				new SqlParameter("@NoteJapaneseUnicode", userSchedule.NoteJapaneseUnicode)
			);
		}
		
		public override void Update(UserSchedule userSchedule, int id)
		{
			string query = @"
UPDATE UserSchedule SET
	UserScheduleID = @UserScheduleID,
	UserID = @UserID,
	UserProjectRoundUserID = @UserProjectRoundUserID,
	DT = @DT,
	SponsorReminderID = @SponsorReminderID,
	Reminder = @Reminder,
	Note = @Note,
	Email = @Email,
	SentDT = @SentDT,
	NoteJapaneseUnicode = @NoteJapaneseUnicode
WHERE UserScheduleID = @UserScheduleID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserScheduleID", userSchedule.UserScheduleID),
				new SqlParameter("@UserID", userSchedule.UserID),
				new SqlParameter("@UserProjectRoundUserID", userSchedule.UserProjectRoundUserID),
				new SqlParameter("@DT", userSchedule.DT),
				new SqlParameter("@SponsorReminderID", userSchedule.SponsorReminderID),
				new SqlParameter("@Reminder", userSchedule.Reminder),
				new SqlParameter("@Note", userSchedule.Note),
				new SqlParameter("@Email", userSchedule.Email),
				new SqlParameter("@SentDT", userSchedule.SentDT),
				new SqlParameter("@NoteJapaneseUnicode", userSchedule.NoteJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserSchedule
WHERE UserScheduleID = @UserScheduleID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserScheduleID", id)
			);
		}
		
		public override UserSchedule Read(int id)
		{
			string query = @"
SELECT 	UserScheduleID, 
	UserID, 
	UserProjectRoundUserID, 
	DT, 
	SponsorReminderID, 
	Reminder, 
	Note, 
	Email, 
	SentDT, 
	NoteJapaneseUnicode
FROM UserSchedule
WHERE UserScheduleID = @UserScheduleID";
			UserSchedule userSchedule = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserScheduleID", id))) {
				if (rs.Read()) {
					userSchedule = new UserSchedule {
						UserScheduleID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						UserProjectRoundUserID = GetInt32(rs, 2),
						DT = GetDateTime(rs, 3),
						SponsorReminderID = GetInt32(rs, 4),
						Reminder = GetInt32(rs, 5),
						Note = GetString(rs, 6),
						Email = GetString(rs, 7),
						SentDT = GetDateTime(rs, 8),
						NoteJapaneseUnicode = GetString(rs, 9)
					};
				}
			}
			return userSchedule;
		}
		
		public override IList<UserSchedule> FindAll()
		{
			string query = @"
SELECT 	UserScheduleID, 
	UserID, 
	UserProjectRoundUserID, 
	DT, 
	SponsorReminderID, 
	Reminder, 
	Note, 
	Email, 
	SentDT, 
	NoteJapaneseUnicode
FROM UserSchedule";
			var userSchedules = new List<UserSchedule>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userSchedules.Add(new UserSchedule {
						UserScheduleID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						UserProjectRoundUserID = GetInt32(rs, 2),
						DT = GetDateTime(rs, 3),
						SponsorReminderID = GetInt32(rs, 4),
						Reminder = GetInt32(rs, 5),
						Note = GetString(rs, 6),
						Email = GetString(rs, 7),
						SentDT = GetDateTime(rs, 8),
						NoteJapaneseUnicode = GetString(rs, 9)
					});
				}
			}
			return userSchedules;
		}
	}
}
