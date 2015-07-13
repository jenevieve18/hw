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
		public void Deactivate(int id)
		{
			string query = @"
update unit set inactive = 1
where id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
delete from unit
where id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public override void Save(Unit t)
		{
			string query = @"
INSERT INTO Unit(Name, CompanyId)
VALUES(@Name, @CompanyId)";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", t.Name),
                new SqlParameter("@CompanyId", t.Company.Id)
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
		
		public IList<Unit> FindByCompany(int companyId)
		{
			string query = @"
SELECT Id,
	Name,
	Inactive
FROM Unit
WHERE CompanyId = @CompanyId
ORDER BY Name";
			var units = new List<Unit>();
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@CompanyId", companyId))) {
				while (rs.Read()) {
					units.Add(
						new Unit {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Inactive = GetInt32(rs, 2) == 1
						}
					);
				}
			}
			return units;
		}
	}
}
