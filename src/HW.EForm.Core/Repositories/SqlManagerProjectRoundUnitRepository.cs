using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlManagerProjectRoundUnitRepository : BaseSqlRepository<ManagerProjectRoundUnit>
	{
		public SqlManagerProjectRoundUnitRepository()
		{
		}
		
		public override void Save(ManagerProjectRoundUnit managerProjectRoundUnit)
		{
			string query = @"
INSERT INTO ManagerProjectRoundUnit(
	ManagerProjectRoundUnitID, 
	ManagerID, 
	ProjectRoundID, 
	ProjectRoundUnitID
)
VALUES(
	@ManagerProjectRoundUnitID, 
	@ManagerID, 
	@ProjectRoundID, 
	@ProjectRoundUnitID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerProjectRoundUnitID", managerProjectRoundUnit.ManagerProjectRoundUnitID),
				new SqlParameter("@ManagerID", managerProjectRoundUnit.ManagerID),
				new SqlParameter("@ProjectRoundID", managerProjectRoundUnit.ProjectRoundID),
				new SqlParameter("@ProjectRoundUnitID", managerProjectRoundUnit.ProjectRoundUnitID)
			);
		}
		
		public override void Update(ManagerProjectRoundUnit managerProjectRoundUnit, int id)
		{
			string query = @"
UPDATE ManagerProjectRoundUnit SET
	ManagerProjectRoundUnitID = @ManagerProjectRoundUnitID,
	ManagerID = @ManagerID,
	ProjectRoundID = @ProjectRoundID,
	ProjectRoundUnitID = @ProjectRoundUnitID
WHERE ManagerProjectRoundUnitID = @ManagerProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerProjectRoundUnitID", managerProjectRoundUnit.ManagerProjectRoundUnitID),
				new SqlParameter("@ManagerID", managerProjectRoundUnit.ManagerID),
				new SqlParameter("@ProjectRoundID", managerProjectRoundUnit.ProjectRoundID),
				new SqlParameter("@ProjectRoundUnitID", managerProjectRoundUnit.ProjectRoundUnitID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ManagerProjectRoundUnit
WHERE ManagerProjectRoundUnitID = @ManagerProjectRoundUnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ManagerProjectRoundUnitID", id)
			);
		}
		
		public override ManagerProjectRoundUnit Read(int id)
		{
			string query = @"
SELECT 	ManagerProjectRoundUnitID, 
	ManagerID, 
	ProjectRoundID, 
	ProjectRoundUnitID
FROM ManagerProjectRoundUnit
WHERE ManagerProjectRoundUnitID = @ManagerProjectRoundUnitID";
			ManagerProjectRoundUnit managerProjectRoundUnit = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ManagerProjectRoundUnitID", id))) {
				if (rs.Read()) {
					managerProjectRoundUnit = new ManagerProjectRoundUnit {
						ManagerProjectRoundUnitID = GetInt32(rs, 0),
						ManagerID = GetInt32(rs, 1),
						ProjectRoundID = GetInt32(rs, 2),
						ProjectRoundUnitID = GetInt32(rs, 3)
					};
				}
			}
			return managerProjectRoundUnit;
		}
		
		public override IList<ManagerProjectRoundUnit> FindAll()
		{
			string query = @"
SELECT 	ManagerProjectRoundUnitID, 
	ManagerID, 
	ProjectRoundID, 
	ProjectRoundUnitID
FROM ManagerProjectRoundUnit";
			var managerProjectRoundUnits = new List<ManagerProjectRoundUnit>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					managerProjectRoundUnits.Add(new ManagerProjectRoundUnit {
						ManagerProjectRoundUnitID = GetInt32(rs, 0),
						ManagerID = GetInt32(rs, 1),
						ProjectRoundID = GetInt32(rs, 2),
						ProjectRoundUnitID = GetInt32(rs, 3)
					});
				}
			}
			return managerProjectRoundUnits;
		}
	}
}
