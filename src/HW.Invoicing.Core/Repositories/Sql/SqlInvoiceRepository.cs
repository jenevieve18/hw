using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlInvoiceRepository : BaseSqlRepository<Invoice>, IInvoiceRepository
	{
		public void Revert(Invoice invoice)
		{
			string query = @"
DELETE FROM InvoiceTimebook WHERE InvoiceId = @InvoiceId;
DELETE FROM Invoice WHERE Id = @InvoiceId
UPDATE GeneratedNumber SET Invoice = Invoice - 1 WHERE CompanyId = @CompanyId";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@InvoiceId", invoice.Id),
				new SqlParameter("@CompanyId", invoice.Customer.Company.Id)
			);
		}

		public void RevertAll(int companyId, int invoiceNumber)
		{
			string query = @"
DELETE it FROM InvoiceTimebook it
INNER JOIN Invoice i ON i.Id = it.InvoiceId
INNER JOIN Customer c ON i.CustomerId = c.Id
WHERE c.CompanyId = @CompanyId;

DELETE i FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id
WHERE c.CompanyId = @CompanyId;

UPDATE GeneratedNumber SET Invoice = @InvoiceNumber
WHERE CompanyId = @CompanyId";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CompanyId", companyId),
				new SqlParameter("@InvoiceNumber", invoiceNumber)
			);
		}

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
UPDATE Invoice SET Date = @Date,
    MaturityDate = @MaturityDate,
    Comments = @Comments
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Date", i.Date),
				new SqlParameter("@MaturityDate", i.MaturityDate),
				new SqlParameter("@Comments", i.Comments),
				new SqlParameter("@Id", id)
			);
			query = @"
DELETE FROM InvoiceTimebook
WHERE InvoiceId = @InvoiceId";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@InvoiceId", id));

			query = @"
INSERT INTO InvoiceTimebook(InvoiceId, CustomerTimebookId, SortOrder)
VALUES(@InvoiceId, @CustomerTimebookId, @SortOrder)";
			foreach (var t in i.Timebooks)
			{
				ExecuteNonQuery(
					query,
					"invoicing",
					new SqlParameter("@CustomerTimebookId", t.Timebook.Id),
					new SqlParameter("@InvoiceId", id),
					new SqlParameter("@SortOrder", t.SortOrder)
				);
			}
		}

		public void ReceivePayment(int id)
		{
			string query = @"
UPDATE Invoice SET Status = 2 WHERE Id = @Id";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", id));
		}

		public void RevertPayment(int id)
		{
			string query = @"
UPDATE Invoice SET Status = 1 WHERE Id = @Id";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Id", id));
		}

		public int GetLatestInvoiceNumber(int companyId)
		{
			string query = @"
SELECT TOP 1 Invoice
FROM GeneratedNumber
WHERE CompanyId = @CompanyId";
			int id = 0;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CompanyId", companyId)))
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
VALUES(@Date, @CustomerId, @Comments, @Number, @MaturityDate, @Status);
SELECT CAST(scope_identity() AS int)";
			int id = (int)ExecuteScalar(
				query,
				"invoicing",
				new SqlParameter("@Date", i.Date),
				new SqlParameter("@CustomerId", i.Customer.Id),
				new SqlParameter("@Comments", i.Comments),
				new SqlParameter("@Number", i.Number),
				new SqlParameter("@MaturityDate", i.MaturityDate),
				new SqlParameter("@Status", Invoice.INVOICED)
//				new SqlParameter("@YourReferencePerson", i.YourReferencePerson),
//				new SqlParameter("@OurReferencePerson", i.OurReferencePerson),
//				new SqlParameter("@PurchaseOrderNumber", i.PurchaseOrderNumber)
			);

			query = @"
DECLARE @key INT

SELECT TOP 1 @key = Id FROM GeneratedNumber
WHERE CompanyId = @CompanyId

IF (@key IS NOT NULL)
    UPDATE GeneratedNumber SET Invoice = @Invoice
    WHERE CompanyId = @CompanyId
ELSE
    INSERT INTO GeneratedNumber(Invoice, CompanyId) VALUES(@Invoice, @CompanyId)";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Invoice", i.Id), new SqlParameter("@CompanyId", i.Customer.Company.Id));
			
			query = @"
INSERT INTO InvoiceTimebook(InvoiceId, CustomerTimebookId, SortOrder)
VALUES(@InvoiceId, @CustomerTimebookId, @SortOrder)";
			
			var cr = new SqlCustomerRepository();
			var c = cr.Read(i.Customer.Id);
			foreach (var t in i.Timebooks) {
				if (t.Timebook.Id == 0)
				{
					t.Timebook.Id = SaveTimebook(t.Timebook);
				}
				ExecuteNonQuery(
					query,
					"invoicing",
					new SqlParameter("@CustomerTimebookId", t.Timebook.Id),
					new SqlParameter("@InvoiceId", id),
					new SqlParameter("@SortOrder", t.SortOrder)
				);

				// Unscribe customer when subscription timebook end date equals to it's subscription end date is invoiced.
				var ti = cr.ReadTimebook(t.Timebook.Id);
				if (ti != null)
				{
					if (ti.IsSubscription && c.SubscriptionHasEndDate && ti.SubscriptionEndDate.Value.Date == c.SubscriptionEndDate.Value.Date)
					{
						ExecuteNonQuery(
							"UPDATE Customer SET HasSubscription = @HasSubscription WHERE Id = @Id",
							"invoicing",
							new SqlParameter("@HasSubscription", false),
							new SqlParameter("@Id", c.Id)
						);
					}
				}
			}
		}

		int SaveTimebook(CustomerTimebook i)
		{
			string query = @"
INSERT INTO CustomerTimebook(CustomerId, ItemId, Quantity, Price, Date, VAT)
VALUES(@CustomerId, @ItemId, @Quantity, @Price, @Date, @VAT);
SELECT CAST(scope_identity() AS int)";
			int id = (int)ExecuteScalar(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", i.Customer.Id),
				new SqlParameter("@ItemId", i.Item.Id),
				new SqlParameter("@Quantity", i.Quantity),
				new SqlParameter("@Price", i.Price),
				new SqlParameter("@Date", i.Date),
				new SqlParameter("@VAT", i.VAT)
			);
			return id;
		}
		
		public override Invoice Read(int id)
		{
			string query = string.Format(
				@"
SELECT i.Id,
    i.Date,
    i.CustomerId,
    c.Name,
    c.InvoiceAddress,
    c.PurchaseOrderNumber,
    c.YourReferencePerson,
    c.OurReferencePerson,
    i.Number,
    c.Number,
    i.Status,
    i.Comments,
    co.Id,
    c.InvoiceEmail,
    c.ContactPersonId,
    c.Language,
    c.Currency,
    c.InvoiceEmailCC
FROM Invoice i
INNER JOIN Customer c ON c.Id = i.CustomerId
INNER JOIN Company co ON co.Id = c.CompanyId
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
						Customer = new Customer
						{
							Id = GetInt32(rs, 2),
							Name = GetString(rs, 3),
							InvoiceAddress = GetString(rs, 4),
							//                            PurchaseOrderNumber = GetString(rs, 5),
							OurReferencePerson = GetString(rs, 7),
							Number = GetString(rs, 9),
							Company = new Company
							{
								Id = GetInt32(rs, 12)
							},
							InvoiceEmail = GetString(rs, 13),
							ContactPerson = new CustomerContact {
								Id = GetInt32(rs, 14)
							},
							Language = Language.GetLanguage(GetInt32(rs, 15)),
							Currency = Currency.GetCurrency(GetInt32(rs, 16)),
							InvoiceEmailCC = GetString(rs, 17)
						},
						Status = GetInt32(rs, 10),
						Comments = GetString(rs, 11)
//						YourReferencePerson = GetString(rs, 18),
//						OurReferencePerson = GetString(rs, 19),
//						PurchaseOrderNumber = GetString(rs, 20)
					};
				}
			}
			if (i != null)
			{
				i.Customer.Contacts = new SqlCustomerRepository().FindContacts(i.Customer.Id);
				i.Customer.ContactPerson = new SqlCustomerRepository().ReadContact(i.Customer.ContactPerson.Id);
				i.Timebooks = FindTimebooks(id);
			}
			return i;
		}

		public List<InvoiceTimebook> FindTimebooks(int invoiceId)
		{
			string query = @"
SELECT it.Id,
    it.CustomerTimebookId,
    ct.Quantity,
    ct.Price,
    ct.VAT,
    ct.ItemId,
    i.Name,
    i.UnitId,
    u.Name,
    ct.Comments,
    ct.Consultant,
    ct.IsSubscription,
    ct.Date,
    ct.DateHidden,
    ct.IsHeader,
    it.SortOrder
FROM InvoiceTimebook it
INNER JOIN CustomerTimebook ct ON ct.Id = it.CustomerTimebookId
LEFT OUTER JOIN Item i ON i.Id = ct.ItemId
LEFT OUTER JOIN Unit u ON u.Id = i.UnitId
WHERE it.InvoiceId = @InvoiceId
ORDER BY it.SortOrder";
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
					              		Consultant = GetString(rs, 10),
					              		IsSubscription = GetInt32(rs, 11) == 1,
					              		Date = GetDateTime(rs, 12),
					              		DateHidden = GetInt32(rs, 13) == 1,
					              		IsHeader = GetInt32(rs, 14) == 1
					              	},
					              	SortOrder = GetInt32(rs, 15)
					              });
				}
			}
			return timebooks;
		}

		public IList<int> FindDistinctYears()
		{
			string query = @"
SELECT DISTINCT YEAR(Date) y
FROM Invoice
ORDER BY y DESC";
			var years = new List<int>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing"))
			{
				while (rs.Read())
				{
					years.Add(GetInt32(rs, 0));
				}
			}
			return years;
		}

		public bool HasInvoicesByCompany(DateTime dateFrom, DateTime dateTo, int companyId)
		{
			string query = @"
SELECT COUNT(*)
FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id
WHERE i.Date BETWEEN @DateFrom and @DateTo
AND c.CompanyId = @CompanyId";
			bool hasInovices = false;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@DateFrom", dateFrom), new SqlParameter("@DateTo", dateTo), new SqlParameter("@CompanyId", companyId)))
			{
				if (rs.Read())
				{
					hasInovices = GetInt32(rs, 0) > 0;
				}
			}
			return hasInovices;
		}

		public IList<Invoice> FindByDateAndCompany(DateTime dateFrom, DateTime dateTo, int companyId)
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
AND c.CompanyId = @CompanyId
ORDER BY i.Number desc";
			var invoices = new List<Invoice>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@DateFrom", dateFrom), new SqlParameter("@DateTo", dateTo), new SqlParameter("@CompanyId", companyId)))
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
