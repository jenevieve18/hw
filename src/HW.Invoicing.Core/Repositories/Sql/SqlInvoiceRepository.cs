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
            string query = @"
INSERT INTO Invoice(Date, CustomerId, Comments)
VALUES(@Date, @CustomerId, @Comments);
SELECT CAST(scope_identity() AS int)";
            int id = (int)ExecuteScalar(
                query, 
                "invoicing",
                new SqlParameter("@Date", i.Date),
                new SqlParameter("@CustomerId", i.Customer.Id),
                new SqlParameter("@Comments", i.Comments)
            );
            
			query = @"
insert into invoicetimebook(invoiceid, customertimebookid)
values(@InvoiceId, @CustomerTimebookId)";
			foreach (var t in i.Timebooks) {
				ExecuteNonQuery(
					query,
					"invoicing",
					new SqlParameter("@CustomerTimebookId", t.Timebook.Id),
                    new SqlParameter("@InvoiceId", id)
				);
			}
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

        public List<InvoiceTimebook> FindTimebooks(int invoiceId)
        {
            string query = @"
select it.id, it.customertimebookid, ct.quantity, ct.price
from invoicetimebook it
inner join customertimebook ct on ct.id = it.customertimebookid
where it.invoiceid = @InvoiceId";
            var timebooks = new List<InvoiceTimebook>();
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@InvoiceId", invoiceId)))
            {
                while (rs.Read())
                {
                    timebooks.Add(new InvoiceTimebook
                    {
                        Id = GetInt32(rs, 0),
                        Timebook = new CustomerTimebook
                        {
                            Id = GetInt32(rs, 1),
                            Quantity = GetDecimal(rs, 2),
                            Price = GetDecimal(rs, 3)
                        }
                    });
                }
            }
            return timebooks;
        }
		
		public override IList<Invoice> FindAll()
		{
			string query = string.Format(
				@"
SELECT i.Id, i.Date, c.Id, c.Name, c.Number
FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id"
			);
			var invoices = new List<Invoice>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					invoices.Add(
						new Invoice {
							Id = GetInt32(rs, 0),
							Date = GetDateTime(rs, 1, DateTime.Now),
							Customer = new Customer {
								Id = GetInt32(rs, 2),
								Name = GetString(rs, 3),
                                Number = GetString(rs, 4)
							}
						}
					);
				}
			}
            foreach (var i in invoices)
            {
                i.Timebooks = FindTimebooks(i.Id);
            }
			return invoices;
		}
	}
}
