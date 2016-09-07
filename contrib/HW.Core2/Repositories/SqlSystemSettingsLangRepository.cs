using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSystemSettingsLangRepository : BaseSqlRepository<SystemSettingsLang>
	{
		public SqlSystemSettingsLangRepository()
		{
		}
		
		public override void Save(SystemSettingsLang systemSettingsLang)
		{
			string query = @"
INSERT INTO SystemSettingsLang(
	SystemSettingsLangID, 
	SystemID, 
	LID, 
	ReminderMessage, 
	ReminderSubject, 
	ReminderAutoLogin
)
VALUES(
	@SystemSettingsLangID, 
	@SystemID, 
	@LID, 
	@ReminderMessage, 
	@ReminderSubject, 
	@ReminderAutoLogin
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SystemSettingsLangID", systemSettingsLang.SystemSettingsLangID),
				new SqlParameter("@SystemID", systemSettingsLang.SystemID),
				new SqlParameter("@LID", systemSettingsLang.LID),
				new SqlParameter("@ReminderMessage", systemSettingsLang.ReminderMessage),
				new SqlParameter("@ReminderSubject", systemSettingsLang.ReminderSubject),
				new SqlParameter("@ReminderAutoLogin", systemSettingsLang.ReminderAutoLogin)
			);
		}
		
		public override void Update(SystemSettingsLang systemSettingsLang, int id)
		{
			string query = @"
UPDATE SystemSettingsLang SET
	SystemSettingsLangID = @SystemSettingsLangID,
	SystemID = @SystemID,
	LID = @LID,
	ReminderMessage = @ReminderMessage,
	ReminderSubject = @ReminderSubject,
	ReminderAutoLogin = @ReminderAutoLogin
WHERE SystemSettingsLangID = @SystemSettingsLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SystemSettingsLangID", systemSettingsLang.SystemSettingsLangID),
				new SqlParameter("@SystemID", systemSettingsLang.SystemID),
				new SqlParameter("@LID", systemSettingsLang.LID),
				new SqlParameter("@ReminderMessage", systemSettingsLang.ReminderMessage),
				new SqlParameter("@ReminderSubject", systemSettingsLang.ReminderSubject),
				new SqlParameter("@ReminderAutoLogin", systemSettingsLang.ReminderAutoLogin)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SystemSettingsLang
WHERE SystemSettingsLangID = @SystemSettingsLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SystemSettingsLangID", id)
			);
		}
		
		public override SystemSettingsLang Read(int id)
		{
			string query = @"
SELECT 	SystemSettingsLangID, 
	SystemID, 
	LID, 
	ReminderMessage, 
	ReminderSubject, 
	ReminderAutoLogin
FROM SystemSettingsLang
WHERE SystemSettingsLangID = @SystemSettingsLangID";
			SystemSettingsLang systemSettingsLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SystemSettingsLangID", id))) {
				if (rs.Read()) {
					systemSettingsLang = new SystemSettingsLang {
						SystemSettingsLangID = GetInt32(rs, 0),
						SystemID = GetInt32(rs, 1),
						LID = GetInt32(rs, 2),
						ReminderMessage = GetString(rs, 3),
						ReminderSubject = GetString(rs, 4),
						ReminderAutoLogin = GetString(rs, 5)
					};
				}
			}
			return systemSettingsLang;
		}
		
		public override IList<SystemSettingsLang> FindAll()
		{
			string query = @"
SELECT 	SystemSettingsLangID, 
	SystemID, 
	LID, 
	ReminderMessage, 
	ReminderSubject, 
	ReminderAutoLogin
FROM SystemSettingsLang";
			var systemSettingsLangs = new List<SystemSettingsLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					systemSettingsLangs.Add(new SystemSettingsLang {
						SystemSettingsLangID = GetInt32(rs, 0),
						SystemID = GetInt32(rs, 1),
						LID = GetInt32(rs, 2),
						ReminderMessage = GetString(rs, 3),
						ReminderSubject = GetString(rs, 4),
						ReminderAutoLogin = GetString(rs, 5)
					});
				}
			}
			return systemSettingsLangs;
		}
	}
}
