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
