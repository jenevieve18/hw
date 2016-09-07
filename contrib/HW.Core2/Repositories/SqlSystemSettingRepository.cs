using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSystemSettingRepository : BaseSqlRepository<SystemSetting>
	{
		public SqlSystemSettingRepository()
		{
		}
		
		public override void Save(SystemSetting systemSetting)
		{
			string query = @"
INSERT INTO SystemSettings(
	SystemID, 
	ProjectRoundUnitID, 
	ReminderMessage, 
	ReminderSubject, 
	ReminderEmail
)
VALUES(
	@SystemID, 
	@ProjectRoundUnitID, 
	@ReminderMessage, 
	@ReminderSubject, 
	@ReminderEmail
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SystemID", systemSetting.SystemID),
				new SqlParameter("@ProjectRoundUnitID", systemSetting.ProjectRoundUnitID),
				new SqlParameter("@ReminderMessage", systemSetting.ReminderMessage),
				new SqlParameter("@ReminderSubject", systemSetting.ReminderSubject),
				new SqlParameter("@ReminderEmail", systemSetting.ReminderEmail)
			);
		}
		
		public override void Update(SystemSetting systemSetting, int id)
		{
			string query = @"
UPDATE SystemSettings SET
	SystemID = @SystemID,
	ProjectRoundUnitID = @ProjectRoundUnitID,
	ReminderMessage = @ReminderMessage,
	ReminderSubject = @ReminderSubject,
	ReminderEmail = @ReminderEmail
WHERE SystemSettingID = @SystemSettingID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SystemID", systemSetting.SystemID),
				new SqlParameter("@ProjectRoundUnitID", systemSetting.ProjectRoundUnitID),
				new SqlParameter("@ReminderMessage", systemSetting.ReminderMessage),
				new SqlParameter("@ReminderSubject", systemSetting.ReminderSubject),
				new SqlParameter("@ReminderEmail", systemSetting.ReminderEmail)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SystemSettings
WHERE SystemSettingID = @SystemSettingID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SystemSettingID", id)
			);
		}
		
		public override SystemSetting Read(int id)
		{
			string query = @"
SELECT 	SystemID, 
	ProjectRoundUnitID, 
	ReminderMessage, 
	ReminderSubject, 
	ReminderEmail
FROM SystemSettings
WHERE SystemSettingID = @SystemSettingID";
			SystemSetting systemSetting = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SystemSettingID", id))) {
				if (rs.Read()) {
					systemSetting = new SystemSetting {
						SystemID = GetInt32(rs, 0),
						ProjectRoundUnitID = GetInt32(rs, 1),
						ReminderMessage = GetString(rs, 2),
						ReminderSubject = GetString(rs, 3),
						ReminderEmail = GetString(rs, 4)
					};
				}
			}
			return systemSetting;
		}
		
		public override IList<SystemSetting> FindAll()
		{
			string query = @"
SELECT 	SystemID, 
	ProjectRoundUnitID, 
	ReminderMessage, 
	ReminderSubject, 
	ReminderEmail
FROM SystemSettings";
			var systemSettings = new List<SystemSetting>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					systemSettings.Add(new SystemSetting {
						SystemID = GetInt32(rs, 0),
						ProjectRoundUnitID = GetInt32(rs, 1),
						ReminderMessage = GetString(rs, 2),
						ReminderSubject = GetString(rs, 3),
						ReminderEmail = GetString(rs, 4)
					});
				}
			}
			return systemSettings;
		}
	}
}
