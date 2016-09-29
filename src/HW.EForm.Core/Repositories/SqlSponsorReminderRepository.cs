using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSponsorReminderRepository : BaseSqlRepository<SponsorReminder>
	{
		public SqlSponsorReminderRepository()
		{
		}
		
		public override void Save(SponsorReminder sponsorReminder)
		{
			string query = @"
INSERT INTO SponsorReminder(
	SponsorReminderID, 
	SponsorID, 
	Reminder, 
	FromEmail, 
	Subject, 
	Body
)
VALUES(
	@SponsorReminderID, 
	@SponsorID, 
	@Reminder, 
	@FromEmail, 
	@Subject, 
	@Body
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorReminderID", sponsorReminder.SponsorReminderID),
				new SqlParameter("@SponsorID", sponsorReminder.SponsorID),
				new SqlParameter("@Reminder", sponsorReminder.Reminder),
				new SqlParameter("@FromEmail", sponsorReminder.FromEmail),
				new SqlParameter("@Subject", sponsorReminder.Subject),
				new SqlParameter("@Body", sponsorReminder.Body)
			);
		}
		
		public override void Update(SponsorReminder sponsorReminder, int id)
		{
			string query = @"
UPDATE SponsorReminder SET
	SponsorReminderID = @SponsorReminderID,
	SponsorID = @SponsorID,
	Reminder = @Reminder,
	FromEmail = @FromEmail,
	Subject = @Subject,
	Body = @Body
WHERE SponsorReminderID = @SponsorReminderID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorReminderID", sponsorReminder.SponsorReminderID),
				new SqlParameter("@SponsorID", sponsorReminder.SponsorID),
				new SqlParameter("@Reminder", sponsorReminder.Reminder),
				new SqlParameter("@FromEmail", sponsorReminder.FromEmail),
				new SqlParameter("@Subject", sponsorReminder.Subject),
				new SqlParameter("@Body", sponsorReminder.Body)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorReminder
WHERE SponsorReminderID = @SponsorReminderID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorReminderID", id)
			);
		}
		
		public override SponsorReminder Read(int id)
		{
			string query = @"
SELECT 	SponsorReminderID, 
	SponsorID, 
	Reminder, 
	FromEmail, 
	Subject, 
	Body
FROM SponsorReminder
WHERE SponsorReminderID = @SponsorReminderID";
			SponsorReminder sponsorReminder = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorReminderID", id))) {
				if (rs.Read()) {
					sponsorReminder = new SponsorReminder {
						SponsorReminderID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						Reminder = GetString(rs, 2),
						FromEmail = GetString(rs, 3),
						Subject = GetString(rs, 4),
						Body = GetString(rs, 5)
					};
				}
			}
			return sponsorReminder;
		}
		
		public override IList<SponsorReminder> FindAll()
		{
			string query = @"
SELECT 	SponsorReminderID, 
	SponsorID, 
	Reminder, 
	FromEmail, 
	Subject, 
	Body
FROM SponsorReminder";
			var sponsorReminders = new List<SponsorReminder>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorReminders.Add(new SponsorReminder {
						SponsorReminderID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						Reminder = GetString(rs, 2),
						FromEmail = GetString(rs, 3),
						Subject = GetString(rs, 4),
						Body = GetString(rs, 5)
					});
				}
			}
			return sponsorReminders;
		}
	}
}
