using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlItemRepository : BaseSqlRepository<Item>, IItemRepository
	{
		public override Item Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Name, Description, Price
FROM Item
WHERE Id = @Id"
			);
			Item i = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					i = new Item {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2),
						Price = GetDecimal(rs, 3)
					};
				}
			}
			return i;
		}
		
		public override void Delete(int id)
		{
			string query = string.Format(
				@"
DELETE FROM Item
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public override void Update(Item i, int id)
		{
			string query = string.Format(
				@"
UPDATE Item SET Name = @Name, Description = @Description, Price = @Price
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", i.Name),
				new SqlParameter("@Description", i.Description),
				new SqlParameter("@Price", i.Price),
				new SqlParameter("@Id", id)
			);
		}
		
		public override void Save(Item i)
		{
			string query = string.Format(
				@"
INSERT INTO Item(Name, Description, Price)
VALUES(@Name, @Description, @Price)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", i.Name),
				new SqlParameter("@Description", i.Description),
				new SqlParameter("@Price", i.Price)
			);
		}
		
		public override IList<Item> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id, Name, Description, Price
FROM Item"
			);
			var items = new List<Item>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					items.Add(
						new Item {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Description = GetString(rs, 2),
							Price = GetDecimal(rs, 3)
						}
					);
				}
			}
			return items;
		}
	}
}
