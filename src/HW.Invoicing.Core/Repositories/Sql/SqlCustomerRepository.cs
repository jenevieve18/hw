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
		public void DeactivateContact(int id)
		{
			string query = @"
update customercontact set inactive = 1
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}

        public void DeactivateTimebook(int id)
        {
            string query = @"
update CustomerTimebook set inactive = 1
WHERE Id = @Id";
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@Id", id)
            );
        }
		
		public void DeactivateItem(int id)
		{
			string query = @"
update customeritem set inactive = 1
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public void DeactivateNote(int id)
		{
			string query = @"
update customernotes set inactive = 1
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
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

        public void DeleteTimebook(int id)
        {
            string query = @"
DELETE FROM CustomerTimebook WHERE Id = @Id";
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@Id", id)
            );
        }
		
		public void DeleteItem(int id)
		{
			string query = @"
DELETE FROM CustomerItem WHERE Id = @Id";
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
INSERT INTO CustomerContact(CustomerId, Contact, Phone, Mobile, Email, Type)
VALUES(@CustomerId, @Contact, @Phone, @Mobile, @Email, @Type)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Contact", contact.Contact),
				new SqlParameter("@Phone", contact.Phone),
				new SqlParameter("@Mobile", contact.Mobile),
				new SqlParameter("@Email", contact.Email),
				new SqlParameter("@Type", contact.Type)
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
INSERT INTO CustomerTimebook(CustomerId, CustomerContactId, ItemId, Quantity, Price, Consultant, Comments, Department, Date)
VALUES(@CustomerId, @CustomerContactId, @ItemId, @Quantity, @Price, @Consultant, @Comments, @Department, @Date)"
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
				new SqlParameter("@Comments", time.Comments),
				new SqlParameter("@Department", time.Department),
				new SqlParameter("@Date", time.Date)
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
UPDATE Customer SET Number = @Number,
InvoiceAddress = @InvoiceAddress,
PostalAddress = @PostalAddress,
PurchaseOrderNumber = @PurchaseOrderNumber,
YourReferencePerson = @YourReferencePerson,
OurReferencePerson = @OurReferencePerson
WHERE Id = @Id"
            );
			ExecuteNonQuery(
				query,
				"invoicing",
                new SqlParameter("@Number", c.Number),
                new SqlParameter("@InvoiceAddress", c.InvoiceAddress),
                new SqlParameter("@PostalAddress", c.PostalAddress),
                new SqlParameter("@PurchaseOrderNumber", c.PurchaseOrderNumber),
                new SqlParameter("@YourReferencePerson", c.YourReferencePerson),
                new SqlParameter("@OurReferencePerson", c.OurReferencePerson),
                new SqlParameter("@Id", id)
			);
		}

        public void UpdateNotes(CustomerNotes c, int id)
        {
            string query = string.Format(
                @"
UPDATE CustomerNotes SET Notes = @Notes,
Inactive = @Inactive
WHERE Id = @Id"
            );
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@Notes", c.Notes),
                new SqlParameter("@Id", id),
                new SqlParameter("@Inactive", c.Inactive)
            );
        }

        public void UpdateContact(CustomerContact c, int id)
        {
            string query = string.Format(
                @"
UPDATE CustomerContact SET Contact = @Contact,
Phone = @Phone,
Mobile = @Mobile,
Email = @Email,
Inactive = @Inactive,
Type = @Type
WHERE Id = @Id"
            );
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@Contact", c.Contact),
                new SqlParameter("@Phone", c.Phone),
                new SqlParameter("@Mobile", c.Mobile),
                new SqlParameter("@Email", c.Email),
                new SqlParameter("@Id", id),
                new SqlParameter("@Inactive", c.Inactive),
                new SqlParameter("@Type", c.Type)
            );
        }

        public void UpdateTimebook(CustomerTimebook c, int id)
        {
            string query = string.Format(
                @"
UPDATE CustomerTimebook SET CustomerContactId = @CustomerContactId,
ItemId = @ItemId,
Quantity = @Quantity,
Price = @Price,
Consultant = @Consultant,
Comments = @Comments,
Department = @Department,
Date = @Date
WHERE Id = @Id"
            );
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@CustomerContactId", c.Contact.Id),
                new SqlParameter("@ItemId", c.Item.Id),
                new SqlParameter("@Quantity", c.Quantity),
                new SqlParameter("@Price", c.Price),
                new SqlParameter("@Consultant", c.Consultant),
                new SqlParameter("@Comments", c.Comments),
                new SqlParameter("@Department", c.Department),
                new SqlParameter("@Date", c.Date),
                new SqlParameter("@Id", id)
            );
        }

        public void UpdateItem(CustomerItem c, int id)
        {
            string query = string.Format(
                @"
UPDATE CustomerItem SET ItemId = @ItemId,
    Price = @Price,
Inactive = @Inactive
WHERE Id = @Id"
            );
            ExecuteNonQuery(
                query,
                "invoicing",
                new SqlParameter("@ItemId", c.Item.Id),
                new SqlParameter("@Price", c.Price),
                new SqlParameter("@Id", id),
                new SqlParameter("@Inactive", c.Inactive)
            );
        }
		
		public void Deactivate(int id)
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
SELECT Id,
Name,
Number,
InvoiceAddress,
PostalAddress,
PurchaseOrderNumber,
YourReferencePerson,
OurReferencePerson
FROM Customer
WHERE Id = @Id"
            );
			Customer c = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new Customer {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Number = GetString(rs, 2),
                        InvoiceAddress = GetString(rs, 3),
                        PostalAddress = GetString(rs, 4),
                        PurchaseOrderNumber = GetString(rs, 5),
                        YourReferencePerson = GetString(rs, 6),
                        OurReferencePerson = GetString(rs, 7)
					};
				}
			}
			return c;
		}

        public CustomerNotes ReadNote(int id)
        {
            string query = string.Format(
                @"
SELECT Id,
    Notes,
Inactive
FROM CustomerNotes
WHERE Id = @Id"
            );
            CustomerNotes c = null;
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id)))
            {
                if (rs.Read())
                {
                    c = new CustomerNotes
                    {
                        Id = GetInt32(rs, 0),
                        Notes = GetString(rs, 1),
                        Inactive = GetInt32(rs, 2) == 1
                    };
                }
            }
            return c;
        }

        public CustomerTimebook ReadTimebook(int id)
        {
            string query = string.Format(
                @"
SELECT Id,
    CustomerId,
    CustomerContactId,
    ItemId,
    Quantity,
    Price,
    Consultant,
    Comments,
    Department,
    Date
FROM CustomerTimebook
WHERE Id = @Id"
            );
            CustomerTimebook c = null;
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id)))
            {
                if (rs.Read())
                {
                    c = new CustomerTimebook
                    {
                        Id = GetInt32(rs, 0),
                        Contact = new CustomerContact { Id = GetInt32(rs, 2) },
                        Item = new Item { Id = GetInt32(rs, 3) },
                        Quantity = GetDecimal(rs, 4),
                        Price = GetDecimal(rs, 5),
                        Consultant = GetString(rs, 6),
                        Comments = GetString(rs, 7),
                        Department = GetString(rs, 8),
                        Date = GetDateTime(rs, 9)
                    };
                }
            }
            return c;
        }

        public CustomerContact ReadContact(int id)
        {
            string query = string.Format(
                @"
SELECT Id,
    Contact,
    Phone,
    Mobile,
    Email,
Inactive,
Type
FROM CustomerContact
WHERE Id = @Id"
            );
            CustomerContact c = null;
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id)))
            {
                if (rs.Read())
                {
                    c = new CustomerContact
                    {
                        Id = GetInt32(rs, 0),
                        Contact = GetString(rs, 1),
                        Phone = GetString(rs, 2),
                        Mobile = GetString(rs, 3),
                        Email = GetString(rs, 4),
                        Inactive = GetInt32(rs, 5) == 1,
                        Type = GetInt32(rs, 6, 3)
                    };
                }
            }
            return c;
        }

        public CustomerItem ReadItem(int id)
        {
            string query = string.Format(
                @"
SELECT Id,
    Price,
    ItemId,
    Inactive
FROM CustomerItem
WHERE Id = @Id"
            );
            CustomerItem c = null;
            using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id)))
            {
                if (rs.Read())
                {
                    c = new CustomerItem
                    {
                        Id = GetInt32(rs, 0),
                        Price = GetDecimal(rs, 1),
                        Item = new Item { Id = GetInt32(rs, 2) },
                        Inactive = GetInt32(rs, 3) == 1
                    };
                }
            }
            return c;
        }
		
		public IList<CustomerItem> FindItems(int customerId)
		{
			string query = string.Format(
				@"
SELECT p.Id,
	p.Price,
	i.Name,
	p.Inactive
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
							Item = new Item {
								Name = GetString(rs, 2)
							},
							Inactive = GetInt32(rs, 3) == 1
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
SELECT n.Id,
	Notes,
	CreatedAt,
	CreatedBy,
	u.Name,
	Inactive
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
							CreatedBy = new User {
								Id = GetInt32(rs, 3),
								Name = GetString(rs, 4)
							},
							Inactive = GetInt32(rs, 5) == 1
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
SELECT t.CustomerContactId,
	t.ItemId,
	t.Quantity,
	t.Price,
	t.Consultant,
	t.Comments,
	c.Contact,
	i.Name,
	u.Id,
	u.Name,
	t.Date,
	t.Department,
    t.Id,
    t.Inactive
FROM CustomerTimebook t
INNER JOIN CustomerContact c ON c.Id = t.CustomerContactId
INNER JOIN Item i ON i.Id = t.ItemId
INNER JOIN UNit u ON u.Id = i.UnitId
WHERE t.CustomerId = @CustomerId"
			);
			var timebooks = new List<CustomerTimebook>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					timebooks.Add(
						new CustomerTimebook {
							Contact = new CustomerContact {
								Id = GetInt32(rs, 0),
								Contact = GetString(rs, 6)
							},
							Item = new Item {
								Id = GetInt32(rs, 1),
								Name = GetString(rs, 7),
								Unit = new Unit { Id = GetInt32(rs, 8), Name = GetString(rs, 9) }
							},
							Quantity = GetDecimal(rs, 2),
							Price = GetDecimal(rs, 3),
							Consultant = GetString(rs, 4),
							Comments = GetString(rs, 5),
							Date = GetDateTime(rs, 10),
							Department = GetString(rs, 11),
                            Id = GetInt32(rs, 12),
                            Inactive = GetInt32(rs, 13) == 1
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
SELECT Contact,
	Phone,
	Mobile,
	Email,
	Id,
	Inactive,
	Type
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
							Id = GetInt32(rs, 4),
							Inactive = GetInt32(rs, 5) == 1,
							Type = GetInt32(rs, 6)
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
