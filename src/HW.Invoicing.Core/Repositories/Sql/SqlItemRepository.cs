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
		public void Deactivate(int id)
		{
			string query = string.Format(
				@"
update item set inactive = 1
where id = @Id"
			);
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", id));
		}
		
		public override Item Read(int id)
		{
			string query = string.Format(
				@"
SELECT i.Id,
    i.Name,
    i.Description,
    i.Price,
    i.UnitId,
    i.Inactive
FROM Item i
WHERE i.Id = @Id"
			);
			Item i = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					i = new Item {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Description = GetString(rs, 2),
						Price = GetDecimal(rs, 3),
						Unit = new Unit { Id = GetInt32(rs, 4) },
                        Inactive = GetInt32(rs, 5) == 1
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
UPDATE Item SET Name = @Name,
    Description = @Description,
    Price = @Price,
    UnitId = @UnitId,
    Inactive = @Inactive
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", i.Name),
				new SqlParameter("@Description", i.Description),
				new SqlParameter("@Price", i.Price),
				new SqlParameter("@Id", id),
                new SqlParameter("@UnitId", i.Unit.Id),
                new SqlParameter("@Inactive", i.Inactive)
			);
		}
		
		public override void Save(Item i)
		{
			string query = string.Format(
				@"
INSERT INTO Item(Name, Description, Price, UnitId)
VALUES(@Name, @Description, @Price, @UnitId)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", i.Name),
				new SqlParameter("@Description", i.Description),
				new SqlParameter("@Price", i.Price),
				new SqlParameter("@UnitId", i.Unit.Id)
			);
		}
		
		public override IList<Item> FindAll()
		{
			string query = string.Format(
				@"
SELECT i.Id,
	i.Name,
	i.Description,
	i.Price,
	i.UnitId,
	u.Name,
	i.Inactive
FROM Item i
INNER JOIN Unit u ON u.Id = i.UnitId
ORDER BY i.Name"
			);
			var items = new List<Item>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					items.Add(
						new Item {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Description = GetString(rs, 2),
							Price = GetDecimal(rs, 3),
							Unit = new Unit {
								Id = GetInt32(rs, 4),
								Name = GetString(rs, 5)
							},
							Inactive = GetInt32(rs, 6) == 1
						}
					);
				}
			}
			return items;
		}
		
		public IList<Item> FindAllWithCustomerItems(int customerId)
		{
			string query = string.Format(
				@"
SELECT i.Id, i.Name, i.Description, ISNULL(p.Price, i.Price), i.UnitId, u.Name
FROM Item i
INNER JOIN Unit u ON u.Id = i.UnitId
LEFT OUTER JOIN CustomerItem p ON p.ItemId = i.Id AND p.CustomerId = @CustomerId AND p.Inactive != 1
ORDER BY i.Name"
			);
			var items = new List<Item>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					items.Add(
						new Item {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Description = GetString(rs, 2),
							Price = GetDecimal(rs, 3),
							Unit = new Unit { Id = GetInt32(rs, 4), Name = GetString(rs, 5) }
						}
					);
				}
			}
			return items;
		}
	}
}
