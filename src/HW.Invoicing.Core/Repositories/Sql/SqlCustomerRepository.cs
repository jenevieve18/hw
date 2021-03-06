﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using System.Linq;

namespace HW.Invoicing.Core.Repositories.Sql
{
	public class SqlCustomerRepository : BaseSqlRepository<Customer>, ICustomerRepository
	{
		public void Swap(CustomerItem item1, CustomerItem item2)
		{
			string query = @"
UPDATE CustomerItem SET SortOrder = @SortOrder
WHERE Id = @Id";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@SortOrder", item1.SortOrder), new SqlParameter("@Id", item2.Id));
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@SortOrder", item2.SortOrder), new SqlParameter("@Id", item1.Id));
		}

		public void DeactivateContact(int id)
		{
			string query = @"
UPDATE CustomerContact SET Inactive = 1
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id)
			);
		}
		
		public void UpdateTimebookConsultant(string consultant, int id)
		{
			string query = @"
UPDATE CustomerTimebook SET Consultant = @Consultant
WHERE Id = @Id";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Consultant", consultant), new SqlParameter("@Id", id));
		}
		
		public void UpdateTimebookComments(string comments, int id)
		{
			string query = @"
UPDATE CustomerTimebook SET Comments = @Comments
WHERE Id = @Id";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@Comments", comments), new SqlParameter("@Id", id));
		}

		public void DeactivateTimebook(int id)
		{
			string query = @"
UPDATE CustomerTimebook SET Inactive = 1
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
UPDATE CustomerItem SET Inactive = 1
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
UPDATE CustomerNotes SET Inactive = 1
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
UPDATE Customer SET Status = @Status
WHERE Id = @Id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Id", id),
				new SqlParameter("@Status", Customer.DELETED)
			);
		}

		public void Undelete(int id)
		{
			string query = @"
UPDATE Customer SET Status = 0
WHERE Id = @Id";
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
INSERT INTO CustomerContact(CustomerId, Contact, Phone, Mobile, Email, Type, Title, PurchaseOrderNumber)
VALUES(@CustomerId, @Contact, @Phone, @Mobile, @Email, @Type, @Title, @PurchaseOrderNumber)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Contact", contact.Name),
				new SqlParameter("@Phone", contact.Phone),
				new SqlParameter("@Mobile", contact.Mobile),
				new SqlParameter("@Email", contact.Email),
				new SqlParameter("@Type", contact.Type),
				new SqlParameter("@Title", contact.Title),
				new SqlParameter("@PurchaseOrderNumber", contact.PurchaseOrderNumber)
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

		public void SaveSubscriptionTimebooks(List<CustomerTimebook> timebooks)
		{
			string saveQuery = string.Format(
				@"
INSERT INTO CustomerTimebook(CustomerId, ItemId, Quantity, Price, Comments, VAT, SubscriptionStartDate, SubscriptionEndDate, IsSubscription)
VALUES(@CustomerId, @ItemId, @Quantity, @Price, @Comments, @VAT, @SubscriptionStartDate, @SubscriptionEndDate, @IsSubscription)"
			);
			string updateQuery = string.Format(
				@"
UPDATE CustomerTimebook SET CustomerId = @CustomerId,
ItemId = @ItemId,
Quantity = @Quantity,
Price = @Price,
Comments = @Comments,
VAT = @VAT,
SubscriptionStartDate = @SubscriptionStartDate,
SubscriptionEndDate = @SubscriptionEndDate,
IsSubscription = @IsSubscription
WHERE Id = @Id"
			);
			foreach (var t in timebooks) {
				if (t.Id <= 0) {
					ExecuteNonQuery(
						saveQuery,
						"invoicing",
						new SqlParameter("@CustomerId", t.Customer.Id),
						new SqlParameter("@ItemId", t.Item.Id),
						new SqlParameter("@Quantity", t.Quantity),
						new SqlParameter("@Price", t.Price),
						new SqlParameter("@Comments", t.Comments),
						new SqlParameter("@VAT", t.VAT),
						new SqlParameter("@SubscriptionStartDate", t.SubscriptionStartDate),
						new SqlParameter("@SubscriptionEndDate", t.SubscriptionEndDate),
						new SqlParameter("@IsSubscription", t.IsSubscription)
					);
					/*if (t.SubscriptionEndDate.Value == t.Customer.SubscriptionEndDate.Value.Date)
                    {
                        ExecuteNonQuery(
                            "UPDATE CUSTOMER SET HasSubscription = @HasSubscription WHERE Id = @Id ",
                            "invoicing",
                            new SqlParameter("@HasSubscription", false),
                            new SqlParameter("@Id", t.Customer.Id)
                        );
                    }*/
				} else {
					ExecuteNonQuery(
						updateQuery,
						"invoicing",
						new SqlParameter("@CustomerId", t.Customer.Id),
						new SqlParameter("@ItemId", t.Item.Id),
						new SqlParameter("@Quantity", t.Quantity),
						new SqlParameter("@Price", t.Price),
						new SqlParameter("@Comments", t.Comments),
						new SqlParameter("@VAT", t.VAT),
						new SqlParameter("@SubscriptionStartDate", t.SubscriptionStartDate),
						new SqlParameter("@SubscriptionEndDate", t.SubscriptionEndDate),
						new SqlParameter("@IsSubscription", t.IsSubscription),
						new SqlParameter("@Id", t.Id)
					);
				}
			}
		}

		public void SaveAgreement(CustomerAgreement agreement, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerAgreement(CustomerId, Date, Lecturer, LectureTitle, Contact, Mobile, Email, Compensation, PaymentTerms, BillingAddress, OtherInformation)
VALUES(@CustomerId, @Date, @Lecturer, @LectureTitle, @Contact, @Mobile, @Email, @Compensation, @PaymentTerms, @BillingAddress, @OtherInformation)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Date", agreement.Date),
				new SqlParameter("@Lecturer", agreement.Lecturer),
				new SqlParameter("@LectureTitle", agreement.LectureTitle),
				new SqlParameter("@Contact", agreement.Contact),
				new SqlParameter("@Mobile", agreement.Mobile),
				new SqlParameter("@Email", agreement.Email),
				new SqlParameter("@Compensation", agreement.Compensation),
				new SqlParameter("@PaymentTerms", agreement.PaymentTerms),
				new SqlParameter("@BillingAddress", agreement.BillingAddress),
				new SqlParameter("@OtherInformation", agreement.OtherInformation)
			);
		}

		public void SaveHeaderTimebook(CustomerTimebook t, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerTimebook(CustomerId, IsHeader, Comments, Date)
VALUES(@CustomerId, @IsHeader, @Comments, @Date)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@IsHeader", t.IsHeader),
				new SqlParameter("@Comments", t.Comments),
				new SqlParameter("@Date", t.Date)
			);
		}

		public void SaveTimebook(CustomerTimebook t, int customerId)
		{
			string query = string.Format(
				@"
INSERT INTO CustomerTimebook(CustomerId, CustomerContactId, ItemId, Quantity, Price, Consultant, Comments, Department, Date, InternalComments, VAT, DateHidden)
VALUES(@CustomerId, @CustomerContactId, @ItemId, @Quantity, @Price, @Consultant, @Comments, @Department, @Date, @InternalComments, @VAT, @DateHidden)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@CustomerContactId", t.Contact.Id),
				new SqlParameter("@ItemId", t.Item.Id),
				new SqlParameter("@Quantity", t.Quantity),
				new SqlParameter("@Price", t.Price),
				new SqlParameter("@Consultant", t.Consultant),
				new SqlParameter("@Comments", t.Comments),
				new SqlParameter("@Department", t.Department),
				new SqlParameter("@Date", t.Date),
				new SqlParameter("@InternalComments", t.InternalComments),
				new SqlParameter("@VAT", t.VAT),
				new SqlParameter("@DateHidden", t.DateHidden)
			);
		}
		
		public void SaveItem(CustomerItem price, int customerId)
		{
			string query = @"
SELECT SortOrder
FROM CustomerItem WHERE
CustomerId = @CustomerId
ORDER BY SortOrder DESC";
			int order = 0;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				if (rs.Read()) {
					order = GetInt32(rs, 0);
					order += 1;
				}
			}

			query = string.Format(
				@"
INSERT INTO CustomerItem(CustomerId, ItemId, Price, SortOrder)
VALUES(@CustomerId, @ItemId, @Price, @SortOrder)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@ItemId", price.Item.Id),
				new SqlParameter("@Price", price.Price),
				new SqlParameter("@SortOrder", order)
			);
		}
		
		public override void Save(Customer c)
		{
			string query = string.Format(
				@"
INSERT INTO Customer(Name, Number, PostalAddress, InvoiceAddress, PurchaseOrderNumber, YourReferencePerson, OurReferencePerson, Phone, Email, Language, CompanyId, HasSubscription, SubscriptionItemId, SubscriptionStartDate, SubscriptionEndDate, SubscriptionHasEndDate, Currency, InvoiceEmail, InvoiceEmailCC)
VALUES(@Name, @Number, @PostalAddress, @InvoiceAddress, @PurchaseOrderNumber, @YourReferencePerson, @OurReferencePerson, @Phone, @Email, @Language, @CompanyId, @HasSubscription, @SubscriptionItemId, @SubscriptionStartDate, @SubscriptionEndDate, @SubscriptionHasEndDate, @Currency, @InvoiceEmail, @InvoiceEmailCC)"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", c.Name),
				new SqlParameter("@Number", c.Number),
				new SqlParameter("@PostalAddress", c.PostalAddress),
				new SqlParameter("@InvoiceAddress", c.InvoiceAddress),
				//new SqlParameter("@PurchaseOrderNumber", c.PurchaseOrderNumber),
				new SqlParameter("@PurchaseOrderNumber", ""),
				new SqlParameter("@YourReferencePerson", ""),
				new SqlParameter("@OurReferencePerson", c.OurReferencePerson),
				new SqlParameter("@Phone", c.Phone),
				new SqlParameter("@Email", c.Email),
				new SqlParameter("@Language", c.Language.Id),
				new SqlParameter("@Currency", c.Currency.Id),
				new SqlParameter("@CompanyId", c.Company.Id),
				new SqlParameter("@HasSubscription", c.HasSubscription),
				new SqlParameter("@SubscriptionItemId", c.SubscriptionItem.Id),
				new SqlParameter("@SubscriptionStartDate", c.SubscriptionStartDate),
				new SqlParameter("@SubscriptionEndDate", c.SubscriptionEndDate),
				new SqlParameter("@SubscriptionHasEndDate", c.SubscriptionHasEndDate),
				new SqlParameter("@InvoiceEmail", c.InvoiceEmail),
				new SqlParameter("@InvoiceEmailCC", c.InvoiceEmailCC)
			);
		}
		
		public override void Update(Customer c, int id)
		{
			string query = string.Format(
				@"
UPDATE Customer SET Name = @Name,
    Number = @Number,
    InvoiceAddress = @InvoiceAddress,
    PostalAddress = @PostalAddress,
    PurchaseOrderNumber = @PurchaseOrderNumber,
    YourReferencePerson = @YourReferencePerson,
    OurReferencePerson = @OurReferencePerson,
    Email = @Email,
    Phone = @Phone,
    Language = @Language,
    InvoiceEmail = @InvoiceEmail,
    InvoiceEmailCC = @InvoiceEmailCC,
    --ContactPersonId = @ContactPersonId,
    Currency = @Currency
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", c.Name),
				new SqlParameter("@Number", c.Number),
				new SqlParameter("@InvoiceAddress", c.InvoiceAddress),
				new SqlParameter("@PostalAddress", c.PostalAddress),
//				new SqlParameter("@PurchaseOrderNumber", c.PurchaseOrderNumber),
				new SqlParameter("@PurchaseOrderNumber", ""),
				new SqlParameter("@YourReferencePerson", ""),
				new SqlParameter("@OurReferencePerson", c.OurReferencePerson),
				new SqlParameter("@Phone", c.Phone),
				new SqlParameter("@Email", c.Email),
				new SqlParameter("@Language", c.Language.Id),
				new SqlParameter("@Id", id),
				new SqlParameter("@InvoiceEmail", c.InvoiceEmail),
				new SqlParameter("@InvoiceEmailCC", c.InvoiceEmailCC),
//				new SqlParameter("@ContactPersonId", c.ContactPerson.Id),
				new SqlParameter("@Currency", c.Currency.Id)
			);
		}

		public void Update2(Customer c, int id)
		{
			string query = string.Format(
				@"
UPDATE Customer SET Name = @Name,
    Number = @Number,
    InvoiceAddress = @InvoiceAddress,
    PostalAddress = @PostalAddress,
    PurchaseOrderNumber = @PurchaseOrderNumber,
    Address = @Address
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Name", c.Name),
				new SqlParameter("@Number", c.Number),
				new SqlParameter("@InvoiceAddress", c.InvoiceAddress),
				new SqlParameter("@PostalAddress", c.PostalAddress),
				new SqlParameter("@PurchaseOrderNumber", c.PurchaseOrderNumber),
				new SqlParameter("@Address", c.Address),
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
    Type = @Type,
    Title = @Title,
    PurchaseOrderNumber = @PurchaseOrderNumber
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Contact", c.Name),
				new SqlParameter("@Phone", c.Phone),
				new SqlParameter("@Mobile", c.Mobile),
				new SqlParameter("@Email", c.Email),
				new SqlParameter("@Id", id),
				new SqlParameter("@Inactive", c.Inactive),
				new SqlParameter("@Type", c.Type),
				new SqlParameter("@Title", c.Title),
				new SqlParameter("@PurchaseOrderNumber", c.PurchaseOrderNumber)
			);
		}

		public void UpdateAgreement(CustomerAgreement a, int id)
		{
			string query = string.Format(
				@"
UPDATE CustomerAgreement SET Date = @Date,
    Lecturer = @Lecturer,
    LectureTitle = @LectureTitle,
    Contact = @Contact,
    Mobile = @Mobile,
    Email = @Email,
    Compensation = @Compensation,
    PaymentTerms = @PaymentTerms,
    BillingAddress = @BillingAddress,
    OtherInformation = @OtherInformation,
    IsClosed = @IsClosed,
    ContactPlaceSigned = @ContactPlaceSigned,
    ContactDateSigned = @ContactDateSigned,
    ContactName = @ContactName,
    ContactTitle = @ContactTitle,
    ContactCompany = @ContactCompany,
    DateSigned = @DateSigned
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Date", a.Date),
				new SqlParameter("@Lecturer", a.Lecturer),
				new SqlParameter("@LectureTitle", a.LectureTitle),
				new SqlParameter("@Contact", a.Contact),
				new SqlParameter("@Mobile", a.Mobile),
				new SqlParameter("@Email", a.Email),
				new SqlParameter("@Compensation", a.Compensation),
				new SqlParameter("@PaymentTerms", a.PaymentTerms),
				new SqlParameter("@BillingAddress", a.BillingAddress),
				new SqlParameter("@OtherInformation", a.OtherInformation),
				new SqlParameter("@IsClosed", a.IsClosed),
				new SqlParameter("@ContactPlaceSigned", a.ContactPlaceSigned),
				new SqlParameter("@ContactDateSigned", a.ContactDateSigned),
				new SqlParameter("@ContactName", a.ContactName),
				new SqlParameter("@ContactTitle", a.ContactTitle),
				new SqlParameter("@ContactCompany", a.ContactCompany),
				new SqlParameter("@DateSigned", a.DateSigned),
				new SqlParameter("@Id", id)
			);

			if (a.DateTimeAndPlaces != null && a.DateTimeAndPlaces.Count > 0) {
				ExecuteNonQuery(
					"DELETE FROM CustomerAgreementDateTimeAndPlace WHERE CustomerAgreementId = @CustomerAgreementId",
					"invoicing",
					new SqlParameter("@CustomerAgreementId", id)
				);
				query = string.Format(
					@"
INSERT INTO CustomerAgreementDateTimeAndPlace(CustomerAgreementId, Date, TimeFrom, TimeTo, Address)
VALUES(@CustomerAgreementId, @Date, @TimeFrom, @TimeTo, @Address)"
				);
				foreach (var d in a.DateTimeAndPlaces) {
					ExecuteNonQuery(
						query,
						"invoicing",
						new SqlParameter("@CustomerAgreementId", id),
						new SqlParameter("@Date", d.Date),
						new SqlParameter("@TimeFrom", d.TimeFrom),
						new SqlParameter("@TimeTo", d.TimeTo),
						new SqlParameter("@Address", d.Address)
					);
				}
			}
		}

		public void UpdateSubscriptionTimebook(CustomerTimebook c, int id)
		{
			string query = string.Format(
				@"
UPDATE CustomerTimebook SET SubscriptionStartDate = @SubscriptionStartDate,
    SubscriptionEndDate = @SubscriptionEndDate,
    Quantity = @Quantity,
    Comments = @Comments,
    Price = @Price,
    Inactive = @Inactive
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@SubscriptionStartDate", c.SubscriptionStartDate),
				new SqlParameter("@SubscriptionEndDate", c.SubscriptionEndDate),
				new SqlParameter("@Quantity", c.Quantity),
				new SqlParameter("@Comments", c.Comments),
				new SqlParameter("@Id", id),
				new SqlParameter("@Price", c.Price),
				new SqlParameter("@Inactive", c.Inactive)
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
    Date = @Date,
    DateHidden = @DateHidden,
    Inactive = @Inactive,
    InternalComments = @InternalComments,
    VAT = @VAT
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
				new SqlParameter("@DateHidden", c.DateHidden),
				new SqlParameter("@Inactive", c.Inactive),
				new SqlParameter("@Id", id),
				new SqlParameter("@InternalComments", c.InternalComments),
				new SqlParameter("@VAT", c.VAT)
			);
		}

		public void UpdateHeaderTimebook(CustomerTimebook c, int id)
		{
			string query = string.Format(
				@"
UPDATE CustomerTimebook SET Comments = @Comments
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Comments", c.Comments),
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

		public void UpdateSubscription(Customer c, int id)
		{
			string query = @"
update customer set hassubscription = @hassubscription,
subscriptionitemid = @subscriptionitemid,
subscriptionstartdate = @subscriptionstartdate,
subscriptionenddate = @subscriptionenddate,
subscriptionhasenddate = @subscriptionhasenddate
where id = @id";
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@hassubscription", c.HasSubscription),
				new SqlParameter("@subscriptionitemid", c.SubscriptionItem.Id),
				new SqlParameter("@subscriptionstartdate", c.SubscriptionStartDate),
				new SqlParameter("@subscriptionenddate", c.SubscriptionEndDate),
				new SqlParameter("@subscriptionhasenddate", c.SubscriptionHasEndDate),
				new SqlParameter("@id", id)
			);
		}
		
		public void Deactivate(int id)
		{
			string query = string.Format(
				@"
UPDATE Customer SET Status = @Status --Inactive = @Inactive
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Status", Customer.INACTIVE),
				new SqlParameter("@Id", id)
			);
		}

		public void Reactivate(int id)
		{
			string query = string.Format(
				@"
UPDATE Customer SET Status = @Status --Inactive = @Inactive
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query,
				"invoicing",
				new SqlParameter("@Status", Customer.ACTIVE),
				new SqlParameter("@Id", id)
			);
		}
		
		public Customer Read2(int id)
		{
			string query = string.Format(
				@"
SELECT c.Id,
    c.Name,
    c.Number,
    c.InvoiceAddress,
    c.PostalAddress,
    c.PurchaseOrderNumber,
    c.OurReferencePerson,
    c.Email,
    c.Phone,
    c.Language,
    c.HasSubscription,
    c.SubscriptionItemId,
    c.SubscriptionStartDate,
    c.SubscriptionEndDate,
    c.SubscriptionHasEndDate,
    c.Status,
    c.InvoiceEmail,
    c.InvoiceEmailCC,
    c.Currency
FROM Customer c
WHERE c.Id = @Id"
			);
			Customer c = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new Customer {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Number = GetString(rs, 2),
						InvoiceAddress = GetString(rs, 3, ""),
						PostalAddress = GetString(rs, 4, ""),
						PurchaseOrderNumber = GetString(rs, 5),
						OurReferencePerson = GetString(rs, 6),
						Email = GetString(rs, 7),
						Phone = GetString(rs, 8),
						Language = Language.GetLanguage(GetInt32(rs, 9, 1)),
						HasSubscription = GetInt32(rs, 10) == 1,
						SubscriptionItem = new Item { Id = GetInt32(rs, 11) },
						SubscriptionStartDate = GetDateTime(rs, 12),
						SubscriptionEndDate = GetDateTime(rs, 13),
						SubscriptionHasEndDate = GetInt32(rs, 14) == 1,
						Status = GetInt32(rs, 15),
						InvoiceEmail = GetString(rs, 16),
						InvoiceEmailCC = GetString(rs, 17),
//						ContactPerson = new CustomerContact { Id = GetInt32(rs, 17) },
						Currency = Currency.GetCurrency(GetInt32(rs, 18, 1))
					};
				}
			}
			return c;
		}
		
		public override Customer Read(int id)
		{
			string query = string.Format(
				@"
SELECT c.Id,
    c.Name,
    c.Number,
    c.InvoiceAddress,
    c.PostalAddress,
    c.PurchaseOrderNumber,
    c.YourReferencePerson,
    c.OurReferencePerson,
    c.Email,
    c.Phone,
    NULL, --c.Inactive,
    c.Language,
    NULL, --l.Name,
    c.HasSubscription,
    c.SubscriptionItemId,
    c.SubscriptionStartDate,
    c.SubscriptionEndDate,
    c.SubscriptionHasEndDate,
    c.Status,
    c.InvoiceEmail,
    c.InvoiceEmailCC,
    c.ContactPersonId,
    c.Currency,
    c.CompanyId
FROM Customer c
--INNER JOIN Lang l ON c.Language = l.Id
--INNER JOIN Item i ON i.Id = c.SubscriptionItemId
--INNER JOIN Unit u ON u.Id = i.UnitId
--LEFT OUTER JOIN CustomerContact cc ON cc.CustomerId = c.Id
WHERE c.Id = @Id"
			);
			Customer c = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new Customer {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						Number = GetString(rs, 2),
						InvoiceAddress = GetString(rs, 3, ""),
						PostalAddress = GetString(rs, 4, ""),
//						PurchaseOrderNumber = GetString(rs, 5),
						OurReferencePerson = GetString(rs, 7),
						Email = GetString(rs, 8),
						Phone = GetString(rs, 9),
						Language = Language.GetLanguage(GetInt32(rs, 11, 1)),
						HasSubscription = GetInt32(rs, 13) == 1,
						SubscriptionItem = new Item {
							Id = GetInt32(rs, 14)
						},
						SubscriptionStartDate = GetDateTime(rs, 15),
						SubscriptionEndDate = GetDateTime(rs, 16),
						SubscriptionHasEndDate = GetInt32(rs, 17) == 1,
						Status = GetInt32(rs, 18),
						InvoiceEmail = GetString(rs, 19),
						InvoiceEmailCC = GetString(rs, 20),
//						ContactPerson = new CustomerContact { Id = GetInt32(rs, 21) },
						Currency = Currency.GetCurrency(GetInt32(rs, 22, 1)),
						Company = new Company { Id = GetInt32(rs, 23) }
					};
				}
			}
//			if (c != null && c.HasSubscription)
			if (c != null)
			{
				c.Contacts = FindContacts(id);
				if (c.HasSubscription) {
					//c.ContactPerson = ReadContact(c.ContactPerson.Id);
					query = @"
SELECT i.Id,
	i.Name,
	u.Id,
	u.Name,
	i.Price
FROM Item i
INNER JOIN Unit u ON u.Id = i.UnitId
WHERE i.Id = @Id";
					using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", c.SubscriptionItem.Id))) {
						if (rs.Read()) {
							c.SubscriptionItem = new Item {
								Id = GetInt32(rs, 0),
								Name = GetString(rs, 1),
								Unit = new Unit {
									Id = GetInt32(rs, 2),
									Name = GetString(rs, 3)
								},
								Price = GetDecimal(rs, 4)
							};
						}
					}
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
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new CustomerNotes {
						Id = GetInt32(rs, 0),
						Notes = GetString(rs, 1),
						Inactive = GetInt32(rs, 2) == 1
					};
				}
			}
			return c;
		}

		public void ClearSubscriptionTimebooks(int companyId)
		{
			string query = @"
DELETE ct FROM CustomerTimebook ct
INNER JOIN Customer c ON c.Id = ct.CustomerId
WHERE IsSubscription = 1
AND c.CompanyId = @CompanyId";
			ExecuteNonQuery(query, "invoicing", new SqlParameter("@CompanyId", companyId));
		}

		public bool HasSubscriptionTimebookWithDate(DateTime startDate)
		{
			string query = @"
SELECT 1
FROM CustomerTimebook
WHERE SubscriptionStartDate = @SubscriptionStartDate
AND IsSubscription = 1";
			bool found = false;
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@SubscriptionStartDate", startDate))) {
				if (rs.Read()) {
					found = true;
				}
			}
			return found;
		}

		public CustomerTimebook ReadTimebook(int id)
		{
			string query = string.Format(
				@"
SELECT ct.Id,
    ct.CustomerId,
    ct.CustomerContactId,
    ct.ItemId,
    ct.Quantity,
    ct.Price,
    ct.Consultant,
    ct.Comments,
    ct.Department,
    ct.Date,
    ct.Inactive,
    ct.InternalComments,
    ct.VAT,
    ct.SubscriptionStartDate,
    ct.SubscriptionEndDate,
    ct.IsSubscription,
    ct.DateHidden,
    u.Name,
    ct.IsHeader
FROM CustomerTimebook ct
--INNER JOIN Item i ON i.Id = ct.ItemId
--INNER JOIN Unit u ON u.Id = i.UnitId
LEFT OUTER JOIN Item i ON i.Id = ct.ItemId
LEFT OUTER JOIN Unit u ON u.Id = i.UnitId
WHERE ct.Id = @Id"
			);
			CustomerTimebook c = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					c = new CustomerTimebook {
						Id = GetInt32(rs, 0),
						Contact = new CustomerContact { Id = GetInt32(rs, 2) },
						Item = new Item {
							Id = GetInt32(rs, 3),
							Unit = new Unit { Name = GetString(rs, 17) }
						},
						Quantity = GetDecimal(rs, 4),
						Price = GetDecimal(rs, 5),
						Consultant = GetString(rs, 6),
						Comments = GetString(rs, 7),
						Department = GetString(rs, 8),
						Date = GetDateTime(rs, 9),
						Inactive = GetInt32(rs, 10) == 1,
						InternalComments = GetString(rs, 11),
						VAT = GetDecimal(rs, 12, 25),
						SubscriptionStartDate = GetDateTime(rs, 13),
						SubscriptionEndDate = GetDateTime(rs, 14),
						IsSubscription = GetInt32(rs, 15) == 1,
						DateHidden = GetInt32(rs, 16) == 1,
						IsHeader = GetInt32(rs, 18) == 1
					};
				}
			}
			return c;
		}

		public CustomerAgreement ReadAgreement(int id)
		{
			string query = string.Format(
				@"
SELECT a.Id,
    a.Date,
    a.Lecturer,
    a.LectureTitle,
    a.Contact,
    a.Mobile,
    a.Email,
    a.Compensation,
    a.PaymentTerms,
    a.BillingAddress,
    a.OtherInformation,
    a.IsClosed,
    a.CustomerId,
    a.ContactPlaceSigned,
    a.ContactDateSigned,
    a.ContactName,
    a.ContactTitle,
    a.ContactCompany,
    a.DateSigned,
    c.Name,
    c.InvoiceAddress
FROM CustomerAgreement a
INNER JOIN Customer c ON c.Id = a.CustomerId
WHERE a.Id = @Id"
			);
			CustomerAgreement a = null;
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@Id", id))) {
				if (rs.Read()) {
					a = new CustomerAgreement {
						Id = GetInt32(rs, 0),
						Date = GetDateTime(rs, 1),
						Lecturer = GetString(rs, 2),
						LectureTitle = GetString(rs, 3),
						Contact = GetString(rs, 4),
						Mobile = GetString(rs, 5),
						Email = GetString(rs, 6),
						Compensation = GetDecimal(rs, 7),
						PaymentTerms = GetString(rs, 8),
						BillingAddress = GetString(rs, 9),
						OtherInformation = GetString(rs, 10),
						IsClosed = GetInt32(rs, 11) == 1,
						Customer = new Customer {
							Id = GetInt32(rs, 12),
							Name = GetString(rs, 19),
							InvoiceAddress = GetString(rs, 20)
						},
						ContactPlaceSigned = GetString(rs, 13),
						ContactDateSigned = GetDateTime(rs, 14),
						ContactName = GetString(rs, 15),
						ContactTitle = GetString(rs, 16),
						ContactCompany = GetString(rs, 17),
						DateSigned = GetDateTime(rs, 18)
					};
				}
			}

			a.DateTimeAndPlaces = FindAgreementDateTimeAndPlaces(id);

			return a;
		}

		List<CustomerAgreementDateTimeAndPlace> FindAgreementDateTimeAndPlaces(int agreementId)
		{
			string query = string.Format(
				@"
SELECT Id,
    CustomerAgreementId,
    Date,
    TimeFrom,
    TimeTo,
    Address
FROM CustomerAgreementDateTimeAndPlace
WHERE CustomerAgreementId = @CustomerAgreementId"
			);
			List<CustomerAgreementDateTimeAndPlace> dates = new List<CustomerAgreementDateTimeAndPlace>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerAgreementId", agreementId)))
			{
				while (rs.Read())
				{
					dates.Add(
						new CustomerAgreementDateTimeAndPlace
						{
							Id = GetInt32(rs, 0),
							CustomerAgreement = new CustomerAgreement { Id = GetInt32(rs, 1) },
							Date = GetDateTime(rs, 2),
							TimeFrom = GetString(rs, 3),
							TimeTo = GetString(rs, 4),
							Address = GetString(rs, 5)
						}
					);
				}
			}
			return dates;
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
    Type,
    Title,
    PurchaseOrderNumber
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
						Name = GetString(rs, 1),
						Phone = GetString(rs, 2),
						Mobile = GetString(rs, 3),
						Email = GetString(rs, 4),
						Inactive = GetInt32(rs, 5) == 1,
						Type = GetInt32(rs, 6, 3),
						Title = GetString(rs, 7),
						PurchaseOrderNumber = GetString(rs, 8)
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
		
		public IList<CustomerItem> lalala(int sortOrder, int customerId)
		{
			string query = @"
select top 2 id,
price,
itemid,
inactive,
sortorder
from customeritem
where sortorder <= @SortOrder
and customerid = @CustomerId
order by sortorder desc";
			var prices = new List<CustomerItem>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@SortOrder", sortOrder), new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					prices.Add(
						new CustomerItem {
							Id = GetInt32(rs, 0),
							Price = GetDecimal(rs, 1),
							Item = new Item {
								Id = GetInt32(rs, 2)
							},
							Inactive = GetInt32(rs, 3) == 1,
							SortOrder = GetInt32(rs, 4)
						}
					);
				}
			}
			return prices;
		}

		public IList<CustomerItem> lololo(int sortOrder, int customerId)
		{
			string query = @"
select top 2 id,
price,
itemid,
inactive,
sortorder
from customeritem p
where sortorder >= @SortOrder
and customerid = @CustomerId
order by sortorder";
			var prices = new List<CustomerItem>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@SortOrder", sortOrder), new SqlParameter("@CustomerId", customerId)))
			{
				while (rs.Read())
				{
					prices.Add(
						new CustomerItem
						{
							Id = GetInt32(rs, 0),
							Price = GetDecimal(rs, 1),
							Item = new Item
							{
								Id = GetInt32(rs, 2)
							},
							Inactive = GetInt32(rs, 3) == 1,
							SortOrder = GetInt32(rs, 4)
						}
					);
				}
			}
			return prices;
		}
		
		public IList<CustomerItem> FindItems(int customerId)
		{
			string query = string.Format(
				@"
SELECT p.Id,
	p.Price,
	i.Name,
	p.Inactive,
    p.SortOrder,
    i.Price
FROM CustomerItem p,
Item i
WHERE p.ItemId = i.Id
AND CustomerId = @CustomerId
order by p.sortorder"
			);
			var prices = new List<CustomerItem>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					prices.Add(
						new CustomerItem {
							Id = GetInt32(rs, 0),
							Price = GetDecimal(rs, 1),
							Item = new Item {
								Name = GetString(rs, 2),
								Price = GetDecimal(rs, 5)
							},
							Inactive = GetInt32(rs, 3) == 1,
							SortOrder = GetInt32(rs, 4)
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
	Inactive,
u.Color
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
								Name = GetString(rs, 4),
								Color = GetString(rs, 6, "")
							},
							Inactive = GetInt32(rs, 5) == 1
						}
					);
				}
			}
			return notes;
		}

		public IList<CustomerAgreement> FindAgreements(int customerId)
		{
			string query = string.Format(
				@"
SELECT a.Id,
    a.Date,
    Lecturer,
    LectureTitle,
    Contact,
    Mobile,
    Email,
    Compensation,
    PaymentTerms,
    BillingAddress,
    OtherInformation,
    IsClosed,
    ContactPlaceSigned,
    ContactDateSigned,
    ContactName,
    ContactTitle,
    ContactCompany,
    DateSigned
FROM CustomerAgreement a
WHERE CustomerId = @CustomerId
ORDER BY a.Date DESC"
			);
			var agreements = new List<CustomerAgreement>();
			using (SqlDataReader rs = ExecuteReader(
				query,
				"invoicing",
				new SqlParameter("@CustomerId", customerId)
			))
			{
				while (rs.Read())
				{
					agreements.Add(
						new CustomerAgreement
						{
							Id = GetInt32(rs, 0),
							Date = GetDateTime(rs, 1),
							Lecturer = GetString(rs, 2),
							LectureTitle = GetString(rs, 3),
							Contact = GetString(rs, 4),
							Mobile = GetString(rs, 5),
							Email = GetString(rs, 6),
							Compensation = GetDecimal(rs, 7),
							PaymentTerms = GetString(rs, 8),
							BillingAddress = GetString(rs, 9),
							OtherInformation = GetString(rs, 10),
							IsClosed = GetInt32(rs, 11) == 1,
							ContactPlaceSigned = GetString(rs, 12),
							ContactDateSigned = GetDateTime(rs, 13),
							ContactName = GetString(rs, 14),
							ContactTitle = GetString(rs, 15),
							ContactCompany = GetString(rs, 16),
							DateSigned = GetDateTime(rs, 17)
						}
					);
				}
			}
			return agreements;
		}

		public IList<CustomerTimebook> FindOpenTimebooks(int customerId)
		{
			string query = @"
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
    t.Inactive,
    t.InternalComments,
    t.VAT,
    t.IsHeader
FROM CustomerTimebook t
--INNER JOIN CustomerContact c on c.Id = t.CustomerContactId
LEFT OUTER JOIN CustomerContact c on c.Id = t.CustomerContactId
LEFT OUTER JOIN Item i ON i.Id = t.ItemId
LEFT OUTER JOIN Unit u ON u.Id = i.UnitId
WHERE t.Customerid = @CustomerId
AND NOT EXISTS (
    SELECT 1 FROM InvoiceTimebook it WHERE it.CustomerTimebookid = t.Id
)";
			var t = new List<CustomerTimebook>();
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					t.Add(
						new CustomerTimebook {
							Contact = new CustomerContact {
								Id = GetInt32(rs, 0),
								Name = GetString(rs, 6)
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
							Inactive = GetInt32(rs, 13) == 1,
							InternalComments = GetString(rs, 14),
							VAT = GetDecimal(rs, 15),
							IsHeader = GetInt32(rs, 16) == 1
						}
					);
				}
			}
			return t;
		}
		
		public bool HasOpenTimebooks(int customerId)
		{
			string query = @"
SELECT COUNT(*)
FROM CustomerTimebook t
LEFT OUTER JOIN CustomerContact c on c.Id = t.CustomerContactId
LEFT OUTER JOIN Item i ON i.Id = t.ItemId
LEFT OUTER JOIN Unit u ON u.Id = i.UnitId
WHERE t.Customerid = @CustomerId
AND NOT EXISTS (
    SELECT 1 FROM InvoiceTimebook it WHERE it.CustomerTimebookid = t.Id
)";
			int count = 0;
			using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
			        count = GetInt32(rs, 0);
				}
			}
			return count > 0;
		}
		
		public int CountAllTimebooks(int customerId)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM CustomerTimebook
WHERE CustomerId = @CustomerId"
			);
			return (int)ExecuteScalar(query, "invoicing", new SqlParameter("@CustomerId", customerId));
		}
		
		public IList<CustomerTimebook> FindTimebooksByOffset(int customerId, int offset, int pageSize)
		{
			string query = string.Format(
				@"
WITH Results_CTE AS (
	SELECT t.CustomerContactId,
		t.ItemId,
		t.Quantity,
		t.Price,
		t.Consultant,
		t.Comments,
		c.Contact,
		i.Name,
		u.Id UnitId,
		u.Name UnitName,
		t.Date,
		t.Department,
	    t.Id,
	    t.Inactive,
	    t.InternalComments,
	    t.VAT,
	    t.IsSubscription,
	    t.SubscriptionStartDate,
	    t.SubscriptionEndDate,
	    t.DateHidden,
	    t.IsHeader,
        ROW_NUMBER() OVER (ORDER BY t.Status, t.Date DESC, t.Id DESC) AS RowNum
	FROM CustomerTimebook t
	LEFT OUTER JOIN CustomerContact c ON c.Id = t.CustomerContactId
	LEFT OUTER JOIN Item i ON i.Id = t.ItemId
	LEFT OUTER JOIN Unit u ON u.Id = i.UnitId
	WHERE t.CustomerId = @CustomerId
)
SELECT *
FROM Results_CTE
WHERE RowNum >= @Offset
AND RowNum < @Offset + @Limit"
			);
			var timebooks = new List<CustomerTimebook>();
			using (SqlDataReader rs = ExecuteReader(
				query, 
				"invoicing", 
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Offset", offset),
				new SqlParameter("@Limit", pageSize))) {
				while (rs.Read()) {
					timebooks.Add(
						new CustomerTimebook {
							Contact = new CustomerContact {
								Id = GetInt32(rs, 0),
								Name = GetString(rs, 6)
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
							Inactive = GetInt32(rs, 13) == 1,
							InternalComments = GetString(rs, 14),
							VAT = GetDecimal(rs, 15, 25),
							IsSubscription = GetInt32(rs, 16) == 1,
							SubscriptionStartDate = GetDateTime(rs, 17),
							SubscriptionEndDate = GetDateTime(rs, 18),
							DateHidden = GetInt32(rs, 19) == 1,
							IsHeader = GetInt32(rs, 20) == 1
						}
					);
				}
			}
			AssignInvoiceTimebooks(timebooks);
			timebooks = timebooks.OrderBy(x => x.Status).ThenByDescending(x => x.IsSubscription).ThenByDescending(x => x.Date).ToList();
			return timebooks;
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
    t.Inactive,
    t.InternalComments,
    t.VAT,
    t.IsSubscription,
    t.SubscriptionStartDate,
    t.SubscriptionEndDate,
    t.DateHidden,
    t.IsHeader
FROM CustomerTimebook t
LEFT OUTER JOIN CustomerContact c ON c.Id = t.CustomerContactId
--INNER JOIN Item i ON i.Id = t.ItemId
--INNER JOIN UNit u ON u.Id = i.UnitId
LEFT OUTER JOIN Item i ON i.Id = t.ItemId
LEFT OUTER JOIN Unit u ON u.Id = i.UnitId
WHERE t.CustomerId = @CustomerId
ORDER BY Status, t.Date DESC, t.Id DESC"
			);
			var timebooks = new List<CustomerTimebook>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					timebooks.Add(
						new CustomerTimebook {
							Contact = new CustomerContact {
								Id = GetInt32(rs, 0),
								Name = GetString(rs, 6)
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
							Inactive = GetInt32(rs, 13) == 1,
							InternalComments = GetString(rs, 14),
							VAT = GetDecimal(rs, 15, 25),
							IsSubscription = GetInt32(rs, 16) == 1,
							SubscriptionStartDate = GetDateTime(rs, 17),
							SubscriptionEndDate = GetDateTime(rs, 18),
							DateHidden = GetInt32(rs, 19) == 1,
							IsHeader = GetInt32(rs, 20) == 1
						}
					);
				}
			}

//			query = @"
			//SELECT i.Status,
			//    i.Id,
			//    i.Number
			//FROM InvoiceTimebook it
			//INNER JOIN Invoice i ON i.Id = it.InvoiceId AND it.CustomerTimebookId = @CustomerTimebookId";
//			foreach (var t in timebooks)
//			{
//				int status = 0;
//				using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerTimebookId", t.Id)))
//				{
//					if (rs.Read())
//					{
//						status = GetInt32(rs, 0);
//						t.InvoiceTimebook = new InvoiceTimebook
//						{
//							Invoice = new Invoice {
//								Id = GetInt32(rs, 1),
//								Number = GetString(rs, 2)
//							}
//						};
//					}
//				}
//				t.Status = status;
//			}
			AssignInvoiceTimebooks(timebooks);
			timebooks = timebooks.OrderBy(x => x.Status).ThenByDescending(x => x.IsSubscription).ThenByDescending(x => x.Date).ToList();
			return timebooks;
		}
		
		void AssignInvoiceTimebooks(IList<CustomerTimebook> timebooks)
		{
			string query = @"
SELECT i.Status,
    i.Id,
    i.Number
FROM InvoiceTimebook it
INNER JOIN Invoice i ON i.Id = it.InvoiceId AND it.CustomerTimebookId = @CustomerTimebookId";
			foreach (var t in timebooks) {
				int status = 0;
				using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerTimebookId", t.Id))) {
					if (rs.Read()) {
						status = GetInt32(rs, 0);
						t.InvoiceTimebook = new InvoiceTimebook {
							Invoice = new Invoice {
								Id = GetInt32(rs, 1),
								Number = GetString(rs, 2)
							}
						};
					}
				}
				t.Status = status;
			}
		}
		
		public IList<CustomerTimebook> FindSubscriptionTimebooks(int customerId)
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
    t.Inactive,
    t.InternalComments,
    t.VAT,
    t.IsSubscription,
    t.SubscriptionStartDate,
    t.SubscriptionEndDate,
    t.DateHidden
FROM CustomerTimebook t
LEFT OUTER JOIN CustomerContact c ON c.Id = t.CustomerContactId
INNER JOIN Item i ON i.Id = t.ItemId
INNER JOIN UNit u ON u.Id = i.UnitId
WHERE t.CustomerId = @CustomerId
AND t.IsSubscription = 1
ORDER BY Status, t.Date DESC, t.Id DESC"
			);
			var timebooks = new List<CustomerTimebook>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					timebooks.Add(
						new CustomerTimebook {
							Contact = new CustomerContact {
								Id = GetInt32(rs, 0),
								Name = GetString(rs, 6)
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
							Inactive = GetInt32(rs, 13) == 1,
							InternalComments = GetString(rs, 14),
							VAT = GetDecimal(rs, 15, 25),
							IsSubscription = GetInt32(rs, 16) == 1,
							SubscriptionStartDate = GetDateTime(rs, 17),
							SubscriptionEndDate = GetDateTime(rs, 18),
							DateHidden = GetInt32(rs, 19) == 1
						}
					);
				}
			}

			query = @"
SELECT i.Status,
    i.Id,
    i.Number
FROM InvoiceTimebook it
INNER JOIN Invoice i ON i.Id = it.InvoiceId AND it.CustomerTimebookId = @CustomerTimebookId";
			foreach (var t in timebooks)
			{
				int status = 0;
				using (var rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerTimebookId", t.Id)))
				{
					if (rs.Read())
					{
						status = GetInt32(rs, 0);
						t.InvoiceTimebook = new InvoiceTimebook
						{
							Invoice = new Invoice {
								Id = GetInt32(rs, 1),
								Number = GetString(rs, 2)
							}
						};
					}
				}
				t.Status = status;
			}
			timebooks = timebooks.OrderBy(x => x.Status).ThenByDescending(x => x.IsSubscription).ThenByDescending(x => x.Date).ToList();
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
	Type,
    Title,
    PurchaseOrderNumber
FROM CustomerContact
WHERE CustomerId = @CustomerId
ORDER BY Type"
			);
			var contacts = new List<CustomerContact>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CustomerId", customerId))) {
				while (rs.Read()) {
					contacts.Add(
						new CustomerContact {
							Name = GetString(rs, 0),
							Phone = GetString(rs, 1),
							Mobile = GetString(rs, 2),
							Email = GetString(rs, 3),
							Id = GetInt32(rs, 4),
							Inactive = GetInt32(rs, 5) == 1,
							Type = GetInt32(rs, 6),
							Title = GetString(rs, 7),
							PurchaseOrderNumber = GetString(rs, 8)
						}
					);
				}
			}
			return contacts;
		}

		public IList<Customer> FindNonSubscribersByCompany(int companyId)
		{
			string query = string.Format(
				@"
SELECT c.Id,
    c.Name,
    c.Number,
    c.Phone,
    c.Email,
    NULL, --c.Inactive,
    c.HasSubscription,
    c.Status,
    c.ContactPersonId
FROM Customer c
--WHERE (c.HasSubscription != 1 OR c.HasSubscription IS NULL)
WHERE ISNULL(c.HasSubscription, 0) = 0
AND ISNULL(c.Status, 0) IN (0, 1)
AND c.CompanyId = @CompanyId
ORDER BY ISNULL(c.Status, 0), --c.Inactive,
c.Name"
			);
			var customers = new List<Customer>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CompanyId", companyId)))
			{
				while (rs.Read())
				{
					customers.Add(
						new Customer
						{
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Number = GetString(rs, 2),
							Phone = GetString(rs, 3),
							Email = GetString(rs, 4),
//							Inactive = GetInt32(rs, 5) == 1,
							Status = GetInt32(rs, 7),
//							ContactPerson = new CustomerContact { Id = GetInt32(rs, 8) }
						}
					);
				}
			}
			return customers;
		}
		
		

		public IList<Customer> FindDeletedCustomersByCompany(int companyId)
		{
			string query = string.Format(
				@"
SELECT c.Id,
    c.Name,
    c.Number,
    c.Phone,
    c.Email,
    NULL, --c.Inactive,
    c.HasSubscription,
    c.Status,
    c.ContactPersonId
FROM Customer c
WHERE c.Status = @Status
AND c.CompanyId = @CompanyId
ORDER BY c.Name"
			);
			var customers = new List<Customer>();
			using (SqlDataReader rs = ExecuteReader(
				query, "invoicing",
				new SqlParameter("@CompanyId", companyId),
				new SqlParameter("@Status", Customer.DELETED)
			))
			{
				while (rs.Read())
				{
					customers.Add(
						new Customer
						{
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Number = GetString(rs, 2),
							Phone = GetString(rs, 3),
							Email = GetString(rs, 4),
//							Inactive = GetInt32(rs, 5) == 1,
							HasSubscription = GetInt32(rs, 6) == 1,
							Status = GetInt32(rs, 7),
//							ContactPerson = new CustomerContact { Id = GetInt32(rs, 8) }
						}
					);
				}
			}
			return customers;
		}

		public void MoveTimebook(int customerId, int timebookId)
		{
			string query = string.Format(
				@"
UPDATE CustomerTimebook SET CustomerId = @CustomerId
WHERE Id = @Id"
			);
			ExecuteNonQuery(
				query, "invoicing",
				new SqlParameter("@CustomerId", customerId),
				new SqlParameter("@Id", timebookId)
			);
		}

		public IList<Customer> FindSubscribersByCompany(int companyId)
		{
			string query = string.Format(
				@"
SELECT c.Id,
    c.Name,
    c.Number,
    c.Phone,
    c.Email,
    NULL, --c.Inactive,
    i.Id,
    i.Name,
    u.Id,
    u.Name,
    i.Price,
    c.Status,
    c.ContactPersonId,
    c.HasSubscription
FROM Customer c
INNER JOIN Item i on i.Id = c.SubscriptionItemId
INNER JOIN Unit u on u.Id = i.UnitId
WHERE c.HasSubscription = 1
AND ISNULL(c.Status, 0) IN (0, 1)
AND c.CompanyId = @CompanyId
ORDER BY ISNULL(c.Status, 0), --c.Inactive,
c.Name"
			);
			var customers = new List<Customer>();
			using (SqlDataReader rs = ExecuteReader(query, "invoicing", new SqlParameter("@CompanyId", companyId)))
			{
				while (rs.Read())
				{
					customers.Add(
						new Customer
						{
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Number = GetString(rs, 2),
							Phone = GetString(rs, 3),
							Email = GetString(rs, 4),
//							Inactive = GetInt32(rs, 5) == 1,
							SubscriptionItem = new Item
							{
								Id = GetInt32(rs, 6),
								Name = GetString(rs, 7),
								Unit = new Unit
								{
									Id = GetInt32(rs, 8),
									Name = GetString(rs, 9)
								},
								Price = GetDecimal(rs, 10)
							},
							Status = GetInt32(rs, 11),
//							ContactPerson = new CustomerContact { Id = GetInt32(rs, 12) },
							HasSubscription = GetInt32(rs, 13) == 1
						}
					);
				}
			}
			return customers;
		}

		public IList<Customer> FindActiveSubscribersByCompany(int companyId, DateTime startDate, DateTime endDate)
		{
			string query = string.Format(
				@"
SELECT c.Id,
    c.Name,
    c.Number,
    c.Phone,
    c.Email,
    NULL, --c.Inactive,
    i.Id,
    i.Name,
    u.Id,
    u.Name,
    i.Price,
    c.HasSubscription,
    c.SubscriptionStartDate,
    c.SubscriptionEndDate,
    c.SubscriptionHasEndDate,
    c.Status
FROM Customer c
INNER JOIN Item i on i.Id = c.SubscriptionItemId
INNER JOIN Unit u on u.Id = i.UnitId
WHERE c.HasSubscription = 1
AND ISNULL(c.Status, 0) = 0
AND c.CompanyId = @CompanyId
AND (
    c.SubscriptionStartDate BETWEEN @StartDate AND @EndDate
    OR c.SubscriptionStartDate <= @StartDate
)
ORDER BY c.Status,
c.Name"
			);
			var customers = new List<Customer>();
			using (SqlDataReader rs = ExecuteReader(
				query,
				"invoicing",
				new SqlParameter("@CompanyId", companyId),
				new SqlParameter("@StartDate", startDate),
				new SqlParameter("@EndDate", endDate)
			))
			{
				while (rs.Read()) {
					customers.Add(
						new Customer {
							Id = GetInt32(rs, 0),
							Name = GetString(rs, 1),
							Number = GetString(rs, 2),
							Phone = GetString(rs, 3),
							Email = GetString(rs, 4),
							SubscriptionItem = new Item {
								Id = GetInt32(rs, 6),
								Name = GetString(rs, 7),
								Unit = new Unit {
									Id = GetInt32(rs, 8),
									Name = GetString(rs, 9)
								},
								Price = GetDecimal(rs, 10)
							},
							HasSubscription = GetInt32(rs, 11) == 1,
							SubscriptionStartDate = GetDateTime(rs, 12),
							SubscriptionEndDate = GetDateTime(rs, 13),
							SubscriptionHasEndDate = GetInt32(rs, 14) == 1,
							Status = GetInt32(rs, 15)
						}
					);
				}
			}
			foreach (var c in customers) {
				c.Timebooks = FindTimebooks(c.Id);
			}
			return customers;
		}
	}
}
