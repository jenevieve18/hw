using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlReminderRepository : BaseSqlRepository<Reminder>
	{
		public SqlReminderRepository()
		{
		}
		
		public override void Save(Reminder reminder)
		{
			string query = @"
INSERT INTO Reminder(
	ReminderID, 
	UserID, 
	DT, 
	Subject, 
	Body
)
VALUES(
	@ReminderID, 
	@UserID, 
	@DT, 
	@Subject, 
	@Body
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReminderID", reminder.ReminderID),
				new SqlParameter("@UserID", reminder.UserID),
				new SqlParameter("@DT", reminder.DT),
				new SqlParameter("@Subject", reminder.Subject),
				new SqlParameter("@Body", reminder.Body)
			);
		}
		
		public override void Update(Reminder reminder, int id)
		{
			string query = @"
UPDATE Reminder SET
	ReminderID = @ReminderID,
	UserID = @UserID,
	DT = @DT,
	Subject = @Subject,
	Body = @Body
WHERE ReminderID = @ReminderID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReminderID", reminder.ReminderID),
				new SqlParameter("@UserID", reminder.UserID),
				new SqlParameter("@DT", reminder.DT),
				new SqlParameter("@Subject", reminder.Subject),
				new SqlParameter("@Body", reminder.Body)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Reminder
WHERE ReminderID = @ReminderID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReminderID", id)
			);
		}
		
		public override Reminder Read(int id)
		{
			string query = @"
SELECT 	ReminderID, 
	UserID, 
	DT, 
	Subject, 
	Body
FROM Reminder
WHERE ReminderID = @ReminderID";
			Reminder reminder = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ReminderID", id))) {
				if (rs.Read()) {
					reminder = new Reminder {
						ReminderID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetDateTime(rs, 2),
						Subject = GetString(rs, 3),
						Body = GetString(rs, 4)
					};
				}
			}
			return reminder;
		}
		
		public override IList<Reminder> FindAll()
		{
			string query = @"
SELECT 	ReminderID, 
	UserID, 
	DT, 
	Subject, 
	Body
FROM Reminder";
			var reminders = new List<Reminder>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					reminders.Add(new Reminder {
						ReminderID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DT = GetDateTime(rs, 2),
						Subject = GetString(rs, 3),
						Body = GetString(rs, 4)
					});
				}
			}
			return reminders;
		}
	}
}
