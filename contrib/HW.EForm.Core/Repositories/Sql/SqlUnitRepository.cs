using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlUnitRepository : BaseSqlRepository<Unit>
	{
		public SqlUnitRepository()
		{
		}
		
		public override void Save(Unit unit)
		{
			string query = @"
INSERT INTO Unit(
	UnitID, 
	ID
)
VALUES(
	@UnitID, 
	@ID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitID", unit.UnitID),
				new SqlParameter("@ID", unit.ID)
			);
		}
		
		public override void Update(Unit unit, int id)
		{
			string query = @"
UPDATE Unit SET
	UnitID = @UnitID,
	ID = @ID
WHERE UnitID = @UnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitID", unit.UnitID),
				new SqlParameter("@ID", unit.ID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Unit
WHERE UnitID = @UnitID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UnitID", id)
			);
		}
		
		public override Unit Read(int id)
		{
			string query = @"
SELECT 	UnitID, 
	ID
FROM Unit
WHERE UnitID = @UnitID";
			Unit unit = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UnitID", id))) {
				if (rs.Read()) {
					unit = new Unit {
						UnitID = GetInt32(rs, 0),
						ID = GetString(rs, 1)
					};
				}
			}
			return unit;
		}
		
		public override IList<Unit> FindAll()
		{
			string query = @"
SELECT 	UnitID, 
	ID
FROM Unit";
			var units = new List<Unit>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					units.Add(new Unit {
						UnitID = GetInt32(rs, 0),
						ID = GetString(rs, 1)
					});
				}
			}
			return units;
		}
	}
}
