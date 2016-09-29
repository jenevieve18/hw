using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundUnitManagerRepository : BaseSqlRepository<ProjectRoundUnitManager>
	{
		public SqlProjectRoundUnitManagerRepository()
		{
		}
		
		public override void Save(ProjectRoundUnitManager projectRoundUnitManager)
		{
			string query = @"
INSERT INTO ProjectRoundUnitManager(
	ProjectRoundUnitManagerID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID
)
VALUES(
	@ProjectRoundUnitManagerID, 
	@ProjectRoundUnitID, 
	@ProjectRoundUserID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUnitManagerID", projectRoundUnitManager.ProjectRoundUnitManagerID),
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnitManager.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundUserID", projectRoundUnitManager.ProjectRoundUserID)
			);
		}
		
		public override void Update(ProjectRoundUnitManager projectRoundUnitManager, int id)
		{
			string query = @"
UPDATE ProjectRoundUnitManager SET
	ProjectRoundUnitManagerID = @ProjectRoundUnitManagerID,
	ProjectRoundUnitID = @ProjectRoundUnitID,
	ProjectRoundUserID = @ProjectRoundUserID
WHERE ProjectRoundUnitManagerID = @ProjectRoundUnitManagerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUnitManagerID", projectRoundUnitManager.ProjectRoundUnitManagerID),
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnitManager.ProjectRoundUnitID),
				new SqlParameter("@ProjectRoundUserID", projectRoundUnitManager.ProjectRoundUserID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundUnitManager
WHERE ProjectRoundUnitManagerID = @ProjectRoundUnitManagerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundUnitManagerID", id)
			);
		}
		
		public override ProjectRoundUnitManager Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundUnitManagerID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID
FROM ProjectRoundUnitManager
WHERE ProjectRoundUnitManagerID = @ProjectRoundUnitManagerID";
			ProjectRoundUnitManager projectRoundUnitManager = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundUnitManagerID", id))) {
				if (rs.Read()) {
					projectRoundUnitManager = new ProjectRoundUnitManager {
						ProjectRoundUnitManagerID = GetInt32(rs, 0),
						ProjectRoundUnitID = GetInt32(rs, 1),
						ProjectRoundUserID = GetInt32(rs, 2)
					};
				}
			}
			return projectRoundUnitManager;
		}
		
		public override IList<ProjectRoundUnitManager> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundUnitManagerID, 
	ProjectRoundUnitID, 
	ProjectRoundUserID
FROM ProjectRoundUnitManager";
			var projectRoundUnitManagers = new List<ProjectRoundUnitManager>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundUnitManagers.Add(new ProjectRoundUnitManager {
						ProjectRoundUnitManagerID = GetInt32(rs, 0),
						ProjectRoundUnitID = GetInt32(rs, 1),
						ProjectRoundUserID = GetInt32(rs, 2)
					});
				}
			}
			return projectRoundUnitManagers;
		}
	}
}
