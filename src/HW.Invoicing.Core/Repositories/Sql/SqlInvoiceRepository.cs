using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlInvoiceRepository : BaseSqlRepository<Invoice>, IInvoiceRepository
	{
        public void UpdateInternalComments(string comments, int id)
        {
            string query = @"
update invoice set internalcomments = @InternalComments
where id = @Id";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@InternalComments", comments), new SqlParameter("@Id", id));
        }

        public void Exported(int id)
        {
            string query = @"
update invoice set exported = 1
where id = @Id";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", id));
        }

        public void Update(Invoice i, int id)
        {
            string query = @"
update invoice set date = @date, maturitydate = @maturitydate, comments = @comments
where id = @id";
            ExecuteNonQuery(
                query, 
                "invoicing",
                new SqlParameter("@date", i.Date),
                new SqlParameter("@maturitydate", i.MaturityDate),
                new SqlParameter("@comments", i.Comments),
                new SqlParameter("@id", id)
            );
            query = @"
delete from invoicetimebook
where invoiceid = @invoiceid";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@invoiceid", id));

            query = @"
insert into invoicetimebook(invoiceid, customertimebookid)
values(@invoiceid, @customertimebookid)";
            foreach (var t in i.Timebooks)
            {
                ExecuteNonQuery(query, "invoicing", new SqlParameter("@customertimebookid", t.Timebook.Id), new SqlParameter("@invoiceid", id));
            }
        }

        public void ReceivePayment(int id)
        {
            string query = @"
update invoice set status = 2 where id = @Id";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", id));
        }

        public void RevertPayment(int id)
        {
            string query = @"
update invoice set status = 1 where id = @Id";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", id));
        }

        public int GetLatestInvoiceNumber()
        {
            string query = @"
select top 1 invoice from generatednumber";
            int id = 0;
            using (SqlDataReader rs = ExecuteReader(query, "invoicing"))
            {
                if (rs.Read())
                {
                    id = GetInt32(rs, 0);
                }
            }
            return id + 1;
        }
		
		public override void Save(Invoice i)
		{
            string query = @"
INSERT INTO Invoice(Date, CustomerId, Comments, Number, MaturityDate, Status)
VALUES(@Date, @CustomerId, @Comments, @Number, @MaturityDate, 1);
SELECT CAST(scope_identity() AS int)";
            int id = (int)ExecuteScalar(
                query, 
                "invoicing",
                new SqlParameter("@Date", i.Date),
                new SqlParameter("@CustomerId", i.Customer.Id),
                new SqlParameter("@Comments", i.Comments),
                new SqlParameter("@Number", i.Number),
                new SqlParameter("@MaturityDate", i.MaturityDate)
            );

            query = @"
declare @key int

select top 1 @key = id from generatednumber

if (@key is not null)
    update generatednumber set invoice = @Invoice
else
    insert into generatednumber(invoice) values(@Invoice)";
            ExecuteNonQuery(query, "invoicing", new SqlParameter("@Invoice", i.Id));
            
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
SELECT i.Id,
    i.Date,
    i.CustomerId,
    c.Name,
    c.invoiceaddress,
    c.purchaseordernumber,
    c.yourreferenceperson,
    c.ourreferenceperson,
    i.number,
    c.number,
    i.status,
    i.comments
FROM Invoice i
INNER JOIN Customer c ON c.Id = i.CustomerId
WHERE i.Id = @Id"
			);
			Invoice i = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
                    i = new Invoice
                    {
                        Id = GetInt32(rs, 0),
                        Date = GetDateTime(rs, 1),
                        Number = GetString(rs, 8),
                        Customer = new Customer {
                            Id = GetInt32(rs, 2),
                            Name = GetString(rs, 3),
                            InvoiceAddress = GetString(rs, 4),
                            PurchaseOrderNumber = GetString(rs, 5),
                            YourReferencePerson = GetString(rs, 6),
                            OurReferencePerson = GetString(rs, 7),
                            Number = GetString(rs, 9)
                        },
                        Status = GetInt32(rs, 10),
                        Comments = GetString(rs, 11)
                    };
				}
			}
            if (i != null)
            {
                i.Timebooks = FindTimebooks(id);
            }
			return i;
		}

        public List<InvoiceTimebook> FindTimebooks(int invoiceId)
        {
            string query = @"
select it.id,
    it.customertimebookid,
    ct.quantity,
    ct.price,
    ct.vat,
    ct.itemid,
    i.name,
    i.unitid,
    u.name,
    ct.comments,
    ct.consultant
from invoicetimebook it
inner join customertimebook ct on ct.id = it.customertimebookid
inner join item i on i.id = ct.itemid
inner join unit u on u.id = i.unitid
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
                            Price = GetDecimal(rs, 3),
                            VAT = GetDecimal(rs, 4),
                            Item = new Item
                            {
                                Id = GetInt32(rs, 5),
                                Name = GetString(rs, 6),
                                Unit = new Unit { Id = GetInt32(rs, 7), Name = GetString(rs, 8) }
                            },
                            Comments = GetString(rs, 9),
                            Consultant = GetString(rs, 10)
                        }
                    });
                }
            }
            return timebooks;
        }

        public IList<Invoice> FindByDate(DateTime dateFrom, DateTime dateTo)
        {
            string query = @"
SELECT i.Id,
    i.Date,
    c.Id,
    c.Name,
    c.Number,
    i.Number,
    i.Status,
    i.Comments,
    i.Exported,
    i.InternalComments
FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id
WHERE i.Date BETWEEN @DateFrom and @DateTo
ORDER BY i.number desc";
            var invoices = new List<Invoice>();
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@DateFrom", dateFrom), new SqlParameter("@DateTo", dateTo)))
            {
                while (rs.Read())
                {
                    invoices.Add(
                        new Invoice
                        {
                            Id = GetInt32(rs, 0),
                            Date = GetDateTime(rs, 1, DateTime.Now),
                            Customer = new Customer
                            {
                                Id = GetInt32(rs, 2),
                                Name = GetString(rs, 3),
                                Number = GetString(rs, 4)
                            },
                            Number = GetString(rs, 5),
                            Status = GetInt32(rs, 6),
                            Comments = GetString(rs, 7),
                            Exported = GetInt32(rs, 8) == 1,
                            InternalComments = GetString(rs, 9)
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
		
		public override IList<Invoice> FindAll()
		{
			string query = string.Format(
				@"
SELECT i.Id,
    i.Date,
    c.Id,
    c.Name,
    c.Number,
    i.Number,
    i.Status,
    i.Comments,
    i.Exported,
    i.InternalComments
FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id
ORDER BY i.number desc"
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
							},
                            Number = GetString(rs, 5),
                            Status = GetInt32(rs, 6),
                            Comments = GetString(rs, 7),
                            Exported = GetInt32(rs, 8) == 1,
                            InternalComments = GetString(rs, 9)
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
