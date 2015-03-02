using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlInvoiceRepository : BaseSqlRepository<Invoice>
	{
		public SqlInvoiceRepository()
		{
		}
		
		public void Save(Invoice i)
		{
			string query = string.Format(
				@"
INSERT INTO Invoice(Date)
VALUES(@Date)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Date", i.Date)
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
SELECT Id, Date, CustomerId
FROM Invoice"
			);
			var invoices = new List<Invoice>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					invoices.Add(
						new Invoice { Id = GetInt32(rs, 0), Date = GetDateTime(rs, 1) }
					);
				}
			}
			return invoices;
		}
	}
}
