using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlManagerProjectRoundRepository : BaseSqlRepository<ManagerProjectRound>
	{
		public SqlManagerProjectRoundRepository()
		{
		}
		
		public override void Save(ManagerProjectRound managerProjectRound)
		{
			string query = @"
INSERT INTO ManagerProjectRound(
	ManagerProjectRoundID, 
	ProjectRoundID, 
	ManagerID, 
	ShowAllUnits, 
	EmailSubject, 
	EmailBody, 
	MPRK
)
VALUES(
	@ManagerProjectRoundID, 
	@ProjectRoundID, 
	@ManagerID, 
	@ShowAllUnits, 
	@EmailSubject, 
	@EmailBody, 
	@MPRK
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerProjectRoundID", managerProjectRound.ManagerProjectRoundID),
				new SqlParameter("@ProjectRoundID", managerProjectRound.ProjectRoundID),
				new SqlParameter("@ManagerID", managerProjectRound.ManagerID),
				new SqlParameter("@ShowAllUnits", managerProjectRound.ShowAllUnits),
				new SqlParameter("@EmailSubject", managerProjectRound.EmailSubject),
				new SqlParameter("@EmailBody", managerProjectRound.EmailBody),
				new SqlParameter("@MPRK", managerProjectRound.MPRK)
			);
		}
		
		public override void Update(ManagerProjectRound managerProjectRound, int id)
		{
			string query = @"
UPDATE ManagerProjectRound SET
	ManagerProjectRoundID = @ManagerProjectRoundID,
	ProjectRoundID = @ProjectRoundID,
	ManagerID = @ManagerID,
	ShowAllUnits = @ShowAllUnits,
	EmailSubject = @EmailSubject,
	EmailBody = @EmailBody,
	MPRK = @MPRK
WHERE ManagerProjectRoundID = @ManagerProjectRoundID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerProjectRoundID", managerProjectRound.ManagerProjectRoundID),
				new SqlParameter("@ProjectRoundID", managerProjectRound.ProjectRoundID),
				new SqlParameter("@ManagerID", managerProjectRound.ManagerID),
				new SqlParameter("@ShowAllUnits", managerProjectRound.ShowAllUnits),
				new SqlParameter("@EmailSubject", managerProjectRound.EmailSubject),
				new SqlParameter("@EmailBody", managerProjectRound.EmailBody),
				new SqlParameter("@MPRK", managerProjectRound.MPRK)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ManagerProjectRound
WHERE ManagerProjectRoundID = @ManagerProjectRoundID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerProjectRoundID", id)
			);
		}
		
		public override ManagerProjectRound Read(int id)
		{
			string query = @"
SELECT 	ManagerProjectRoundID, 
	ProjectRoundID, 
	ManagerID, 
	ShowAllUnits, 
	EmailSubject, 
	EmailBody, 
	MPRK
FROM ManagerProjectRound
WHERE ManagerProjectRoundID = @ManagerProjectRoundID";
			ManagerProjectRound managerProjectRound = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerProjectRoundID", id))) {
				if (rs.Read()) {
					managerProjectRound = new ManagerProjectRound {
						ManagerProjectRoundID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ManagerID = GetInt32(rs, 2),
						ShowAllUnits = GetInt32(rs, 3),
						EmailSubject = GetString(rs, 4),
						EmailBody = GetString(rs, 5),
						MPRK = GetGuid(rs, 6)
					};
				}
			}
			return managerProjectRound;
		}
		
		public override IList<ManagerProjectRound> FindAll()
		{
			string query = @"
SELECT 	ManagerProjectRoundID, 
	ProjectRoundID, 
	ManagerID, 
	ShowAllUnits, 
	EmailSubject, 
	EmailBody, 
	MPRK
FROM ManagerProjectRound";
			var managerProjectRounds = new List<ManagerProjectRound>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					managerProjectRounds.Add(new ManagerProjectRound {
						ManagerProjectRoundID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ManagerID = GetInt32(rs, 2),
						ShowAllUnits = GetInt32(rs, 3),
						EmailSubject = GetString(rs, 4),
						EmailBody = GetString(rs, 5),
						MPRK = GetGuid(rs, 6)
					});
				}
			}
			return managerProjectRounds;
		}
		
		public IList<ManagerProjectRound> FindByManager(int managerID)
		{
			string query = @"
SELECT 	ManagerProjectRoundID, 
	ProjectRoundID, 
	ManagerID, 
	ShowAllUnits, 
	EmailSubject, 
	EmailBody, 
	MPRK
FROM ManagerProjectRound
WHERE ManagerID = @ManagerID";
			var managerProjectRounds = new List<ManagerProjectRound>();
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerID", managerID))) {
				while (rs.Read()) {
					managerProjectRounds.Add(new ManagerProjectRound {
						ManagerProjectRoundID = GetInt32(rs, 0),
						ProjectRoundID = GetInt32(rs, 1),
						ManagerID = GetInt32(rs, 2),
						ShowAllUnits = GetInt32(rs, 3),
						EmailSubject = GetString(rs, 4),
						EmailBody = GetString(rs, 5),
						MPRK = GetGuid(rs, 6)
					});
				}
			}
			return managerProjectRounds;
		}
	}
}
