using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlUnitRepository : BaseSqlRepository<Unit>
	{
		public override void Save(Unit t)
		{
			string query = @"
INSERT INTO Unit(Name)
VALUES(@Name)";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name)
			);
		}
		
		public override void Update(Unit t, int id)
		{
			string query = @"
UPDATE Unit SET Name = @Name
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name),
				new SqlParameter("@Id", id)
			);
		}
		
		public override Unit Read(int id)
		{
			string query = @"
SELECT Id, Name FROM Unit
WHERE Id = @Id";
			var unit = new Unit();
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					unit = new Unit { Id = GetInt32(rs, 0), Name = GetString(rs, 1) };
				}
			}
			return unit;
		}
		
		public override IList<Unit> FindAll()
		{
			string query = @"
SELECT Id, Name FROM Unit";
			var units = new List<Unit>();
			using (var rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					units.Add(
						new Unit { Id = GetInt32(rs, 0), Name = GetString(rs, 1) }
					);
				}
			}
			return units;
		}
	}
}
