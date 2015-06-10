using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlInvoiceRepository : BaseSqlRepository<Invoice>, IInvoiceRepository
	{
		public SqlInvoiceRepository()
		{
		}
		
		public override void Save(Invoice i)
		{
			string query = string.Format(
				@"
INSERT INTO Invoice(Date, CustomerId)
VALUES(@Date, @CustomerId)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Date", i.Date),
				new SqlParameter("@CustomerId", i.Customer.Id)
			);
		}
		
		public override Invoice Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Date, CustomerId
FROM Invoice
WHERE Id = @id"
			);
			Invoice i = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				while (rs.Read()) {
					i = new Invoice { Id = GetInt32(rs, 0), Date = GetDateTime(rs, 1) };
				}
			}
			return i;
		}
		
		public override IList<Invoice> FindAll()
		{
			string query = string.Format(
				@"
SELECT i.Id, i.Date, c.Id, c.Name
FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id"
			);
			var invoices = new List<Invoice>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					invoices.Add(
						new Invoice {
							Id = GetInt32(rs, 0),
							Date = GetDateTime(rs, 1),
							Customer = new Customer {
								Id = GetInt32(rs, 2),
								Name = GetString(rs, 3)
							}
						}
					);
				}
			}
			return invoices;
		}
	}
}
