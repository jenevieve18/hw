using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlItemRepository : BaseSqlRepository<Item>
	{
		public SqlItemRepository()
		{
		}
		
		public override Item Read(int id)
		{
			
			string query = string.Format(
				@"
SELECT Id, Name
FROM Item
WHERE Id = @Id"
			);
			Item i = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					i = new Item { Id = GetInt32(rs, 0), Name = GetString(rs, 1) };
				}
			}
			return i;
		}
		
		public void Update(Item i, int id)
		{
			string query = string.Format(
				@"
UPDATE Item SET Name = @Name
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", i.Name),
				new SqlParameter("@Description", i.Description),
				new SqlParameter("@Id", id)
			);
		}
		
		public void Save(Item i)
		{
			string query = string.Format(
				@"
INSERT INTO Item(Name)
VALUES(@Name)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", i.Name)
			);
		}
		
		public override IList<Item> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id, Name
FROM Item"
			);
			var items = new List<Item>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					items.Add(
						new Item { Id = GetInt32(rs, 0), Name = GetString(rs, 1) }
					);
				}
			}
			return items;
		}
	}
}
