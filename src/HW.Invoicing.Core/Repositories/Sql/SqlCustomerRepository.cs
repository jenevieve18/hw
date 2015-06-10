using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlCustomerRepository : BaseSqlRepository<Customer>, ICustomerRepository
	{
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Customer WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public void DeleteNotes(int id)
		{
			string query = @"
DELETE FROM CustomerNotes WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public void DeleteContact(int id)
		{
			string query = @"
DELETE FROM CustomerContact WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public void SaveContact(CustomerContact contact, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerContact(CustomerId, Contact, Phone, Mobile, Email)
VALUES(@CustomerId, @Contact, @Phone, @Mobile, @Email)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Contact", contact.Contact),
				new SqlParameter("@Phone", contact.Phone),
				new SqlParameter("@Mobile", contact.Mobile),
				new SqlParameter("@Email", contact.Email)
			);
		}
		
		public void SaveNotes(CustomerNotes notes, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerNotes(CustomerId, Notes, CreatedAt, CreatedBy)
VALUES(@CustomerId, @Notes, @CreatedAt, @CreatedBy)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Notes", notes.Notes),
				new SqlParameter("@CreatedAt", notes.CreatedAt),
				new SqlParameter("@CreatedBy", notes.CreatedBy.Id)
			);
		}
		
		public void SaveTimebook(CustomerTimebook time, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerTimebook(CustomerId, CustomerContactId, ItemId, Quantity, Price, Consultant, Comments)
VALUES(@CustomerId, @CustomerContactId, @ItemId, @Quantity, @Price, @Consultant, @Comments)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@CustomerContactId", time.Contact.Id),
				new SqlParameter("@ItemId", time.Item.Id),
				new SqlParameter("@Quantity", time.Quantity),
				new SqlParameter("@Price", time.Price),
				new SqlParameter("@Consultant", time.Consultant),
				new SqlParameter("@Comments", time.Comments)
			);
		}
		
		public void SaveItem(CustomerItem price, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerItem(CustomerId, ItemId, Price)
VALUES(@CustomerId, @ItemId, @Price)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@ItemId", price.Item.Id),
				new SqlParameter("@Price", price.Price)
			);
		}
		
		public override void Save(Customer c)
		{
			string query = string.Format(
				@"
INSERT INTO Customer(Name, Number)
VALUES(@Name, @Number)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", c.Name),
				new SqlParameter("@Number", c.Number)
			);
		}
		
		public override void Update(Customer c, int id)
		{
			string query = string.Format(
				@"
UPDATE Customer SET Name = @Name, Number = @Number
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", c.Name),
				new SqlParameter("@Id", id),
				new SqlParameter("@Number", c.Number)
			);
		}
		
		public override void Deactivate(int id)
		{
			string query = string.Format(
				@"
UPDATE Customer SET Inactive = @Inactive
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Inactive", true),
				new SqlParameter("@Id", id)
			);
		}
		
		public override Customer Read(int id)
		{
			string query = string.Format(
				@"
SELECT Id, Name, Number
FROM Customer
WHERE Id = @Id"
			);
			Customer c = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new Customer {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Number = GetString(rs, 2)
					};
				}
			}
			return c;
		}
		
		public IList<CustomerItem> FindItems(int customerId)
		{
			string query = string.Format(
				@"
SELECT p.Id, p.Price, i.Name
FROM CustomerItem p,
Item i
WHERE p.ItemId = i.Id 
AND CustomerId = @CustomerId"
			);
			var prices = new List<CustomerItem>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					prices.Add(
						new CustomerItem {
							Id = GetInt32(rs, 0),
							Price = GetDecimal(rs, 1),
							Item = new Item { Name = GetString(rs, 2) }
						}
					);
				}
			}
			return prices;
		}
		
		public IList<CustomerNotes> FindNotes(int customerId)
		{
			string query = string.Format(
				@"
SELECT n.Id, Notes, CreatedAt, CreatedBy, u.Name
FROM CustomerNotes n,
[User] u
WHERE CustomerId = @CustomerId
AND n.CreatedBy = u.Id
ORDER BY n.CreatedAt DESC"
			);
			var notes = new List<CustomerNotes>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					notes.Add(
						new CustomerNotes {
							Id = GetInt32(rs, 0),
							Notes = GetString(rs, 1),
							CreatedAt = GetDateTime(rs, 2),
							CreatedBy = new User { Id = GetInt32(rs, 3), Name = GetString(rs, 4) }
						}
					);
				}
			}
			return notes;
		}
		
		public IList<CustomerTimebook> FindTimebooks(int customerId)
		{
			string query = string.Format(
				@"
SELECT t.CustomerContactId, t.ItemId, t.Quantity, t.Price, t.Consultant, t.Comments, c.Contact 
FROM CustomerTimebook t
INNER JOIN CustomerContact c ON c.Id = t.CustomerContactId
WHERE t.CustomerId = @CustomerId"
			);
			var timebooks = new List<CustomerTimebook>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					timebooks.Add(
						new CustomerTimebook {
							Contact = new CustomerContact { Id = GetInt32(rs, 0), Contact = GetString(rs, 6) },
							Item = new Item { Id = GetInt32(rs, 1) },
							Quantity = GetDecimal(rs, 2),
							Price = GetDecimal(rs, 3),
							Consultant = GetString(rs, 4),
							Comments = GetString(rs, 5)
						}
					);
				}
			}
			return timebooks;
		}
		
		public IList<CustomerContact> FindContacts(int customerId)
		{
			string query = string.Format(
				@"
SELECT Contact, Phone, Mobile, Email, Id
FROM CustomerContact
WHERE CustomerId = @CustomerId"
			);
			var contacts = new List<CustomerContact>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					contacts.Add(
						new CustomerContact {
							Contact = GetString(rs, 0),
							Phone = GetString(rs, 1),
							Mobile = GetString(rs, 2),
							Email = GetString(rs, 3),
							Id = GetInt32(rs, 4)
						}
					);
				}
			}
			return contacts;
		}
		
		public override IList<Customer> FindAll()
		{
			string query = string.Format(
				@"
SELECT Id, Name, Number
FROM Customer"
			);
			var customers = new List<Customer>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing")) {
				while (rs.Read()) {
					customers.Add(
						new Customer {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Number = GetString(rs, 2)
						}
					);
				}
			}
			return customers;
		}
	}
}
