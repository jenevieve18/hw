using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Models
{
	public class Link : BaseModel
	{
		public string Name { get; set; }
		public string Url { get; set; }
		public bool ForSubscription { get; set; }
		
		public static Link GetLink(int id)
		{
			foreach (var l in GetLinks()) {
				if (id == l.Id) {
					return l;
				}
			}
			return null;
		}
		
		public static List<Link> GetLinks()
		{
			return new List<Link>(
				new Link[] {
					new Link { Id = 1, Name = "Customers", Url = "customers.aspx" },
					new Link { Id = 2, Name = "Subscription Timebooks", Url = "customertimebooksubscriptionadd.aspx", ForSubscription = true },
					new Link { Id = 3, Name = "Invoices", Url = "invoices.aspx" },
					new Link { Id = 4, Name = "Items", Url = "items.aspx" },
					new Link { Id = 5, Name = "Units", Url = "units.aspx" },
//					new Link { Id = 6, Name = "Users", Url = "users.aspx" },
					new Link { Id = 7, Name = "Collaborators", Url = "collaborators.aspx" },
					new Link { Id = 8, Name = "Issues", Url = "issues.aspx" }
				}
			);
		}
	}
	
	public class Customer : BaseModel
	{
		public const int ACTIVE = 0;
		public const int INACTIVE = 1;
		public const int DELETED = 2;
		
		public Company Company { get; set; }
		public bool HasSubscription { get; set; }
		public bool SubscriptionHasEndDate { get; set; }
		public Item SubscriptionItem { get; set; }
		public DateTime? SubscriptionStartDate { get; set; }
		public DateTime? SubscriptionEndDate { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Number { get; set; }
		public string InvoiceAddress { get; set; }
        public string InvoiceEmail { get; set; }
        public string InvoiceEmailCC { get; set; }
		public string PostalAddress { get; set; }
		public string PurchaseOrderNumber { get; set; }
//		public string YourReferencePerson { get; set; }
        public CustomerContact ContactPerson { get; set; }
		public string OurReferencePerson { get; set; }
//		public bool Inactive { get; set; }
		public Language Language { get; set; }
		public Currency Currency { get; set; }
		public int Status { get; set; }
		
		public bool IsInactive {
			get { return Status == INACTIVE; }
		}
		
		public bool IsDeleted {
			get { return Status == DELETED; }
		}

		public bool HasOpenSubscriptionTimebooks
		{
			get { return OpenSubscriptionTimebooks.Count > 0;  }
		}

		public bool HasLatestSubscriptionTimebook
		{
			get { return LatestSubscriptionTimebook != null; }
		}

		public CustomerTimebook LatestSubscriptionTimebook
		{
			get {
				if (HasSubscriptionTimebooks) {
					return SubscriptionTimebooks[0];
				}
				return null;
			}
		}

		public bool HasSubscriptionTimebooks
		{
			get { return SubscriptionTimebooks.Count > 0;  }
		}

		public IList<CustomerTimebook> SubscriptionTimebooks
		{
			get {
				return Timebooks.OrderByDescending(x => x.SubscriptionStartDate).Where(x => x.IsSubscription).Select(x => x).ToList();
			}
		}

		public IList<CustomerTimebook> OpenSubscriptionTimebooks
		{
			get {
				return Timebooks.OrderByDescending(x => x.SubscriptionStartDate).Where(x => x.IsSubscription && x.IsOpen).Select(x => x).ToList();
			}
		}

		public IList<CustomerTimebook> Timebooks { get; set; }

		public IList<CustomerContact> Contacts { get; set; }

		public CustomerContact FirstPrimaryContact
		{
			get {
				if (HasPrimaryContacts) {
					return PrimaryContacts[0];
				}
				return null;
			}
		}

		public CustomerContact SecondaryContact
		{
			get {
				if (HasSecondaryContacts) {
					return SecondaryContacts[0];
				}
				return null;
			}
		}

		public bool HasPrimaryContacts
		{
			get { return PrimaryContacts.Count > 0; }
		}

		public bool HasSecondaryContacts
		{
			get { return SecondaryContacts.Count > 0; }
		}

		public IList<CustomerContact> PrimaryContacts
		{
			get {
				return (from c in Contacts where c.Type == 1 select c).ToList();
			}
		}

		public IList<CustomerContact> SecondaryContacts
		{
			get {
				return (from c in Contacts where c.Type == 2 select c).ToList();
			}
		}
		
		public string GetName()
		{
			if (Name.Length > 30) {
				return Name.Substring(0, 30) + "...";
			} else {
				return Name;
			}
		}

		public string GetSubscriptionStartAndEndDate()
		{
			return SubscriptionStartDate.Value.ToString("yyyy.MM.dd") + (SubscriptionHasEndDate ? " - " + SubscriptionEndDate.Value.ToString("yyyy.MM.dd") : "");
		}

		public string GetSubscriptionStartDate()
		{
			return SubscriptionStartDate.Value.ToString("yyyy.MM.dd");
		}

		public string GetSubscriptionEndDate()
		{
			return SubscriptionEndDate.Value.ToString("yyyy.MM.dd");
		}

		public string GetSubscriptionEndDateString()
		{
			if (SubscriptionHasEndDate) {
				return SubscriptionEndDate.Value.ToString("yyyy-MM-dd");
			} else {
				return "";
			}
		}

		public string GetLatestSubscriptionTimebookEndDateLabel()
		{
			if (HasLatestSubscriptionTimebook) {
				return string.Format("<span class='label label-success'>{0}</span>", LatestSubscriptionTimebook.SubscriptionEndDate.Value.ToString("MMM dd"));
			} else {
				return "<span class='label label-default'>None</span>";
			}
		}

		public string GetLatestSubscriptionTimebookComments(DateTime startDate, DateTime endDate, string generatedComments)
		{
			if (HasLatestSubscriptionTimebook) {
				var t = SubscriptionTimebooks[0];
				if (t.IsInvoiced || t.IsPaid) {
					return string.Format("Subscription fee for HealthWatch.se {0} - {1}", startDate.ToString("yyyy.MM.dd"), endDate.ToString("yyyy.MM.dd"));
				} else {
					return SubscriptionTimebooks[0].Comments;
				}
			} else {
				return string.Format("Subscription fee for HealthWatch.se {0} - {1}", startDate.ToString("yyyy.MM.dd"), endDate.ToString("yyyy.MM.dd"));
			}
		}

		public decimal GetLatestSubscriptionTimebookQuantity(DateTime startDate, DateTime endDate)
		{
			if (HasLatestSubscriptionTimebook) {
				var t = SubscriptionTimebooks[0];
				if (t.IsInvoiced || t.IsPaid) {
					return (decimal)DateHelper.MonthDiff(startDate, endDate);
				} else {
					return SubscriptionTimebooks[0].Quantity;
				}
			} else {
				if (SubscriptionHasEndDate) {
					return (decimal)DateHelper.MonthDiff(startDate, endDate);
				} else {
					return (decimal)DateHelper.MonthDiff(startDate, endDate);
				}
			}
		}
		
		public bool IsInitial(DateTime d)
		{
			return d.Date == SubscriptionStartDate.Value.Date;
//			if (HasSubscriptionTimebooks) {
//				var t = SubscriptionTimebooks[0];
//				if (t.IsInvoiced) {
//					return false;
//				} else {
//
//				}
//			} else {
//				return true;
//			}
		}

		public DateTime GetLatestSubscriptionTimebookStartDate(DateTime d)
		{
			if (HasSubscriptionTimebooks) {
				var t = SubscriptionTimebooks[0];
				if (t.IsInvoiced || t.IsPaid) {
					return t.SubscriptionEndDate.Value.AddDays(1);
				} else {
					return t.SubscriptionStartDate.Value;
				}
			} else {
				return SubscriptionStartDate.Value;
			}
		}

		public DateTime GetLatestSubscriptionTimebookEndDate(DateTime endDate)
		{
			if (HasLatestSubscriptionTimebook) {
				var t = SubscriptionTimebooks[0];
				if (t.IsInvoiced || t.IsPaid) {
					if (SubscriptionHasEndDate) {
						return SubscriptionEndDate.Value;
					} else {
						//return endDate.AddMonths(1);
						return t.SubscriptionEndDate.Value.AddMonths(1);
					}
				} else {
					return t.SubscriptionEndDate.Value;
				}
			} else {
				if (SubscriptionHasEndDate) {
					return SubscriptionEndDate.Value;
				} else {
					return endDate;
				}
			}
		}

		public int GetLatestSubscriptionTimebookId()
		{
			if (HasLatestSubscriptionTimebook) {
				var t = SubscriptionTimebooks[0];
				if (t.IsInvoiced || t.IsPaid) {
					return 0;
				} else {
					return LatestSubscriptionTimebook.Id;
				}
			} else {
				return 0;
			}
		}

		public string GetSubscriptionTimebookAvailability(DateTime d)
		{
			if (HasSubscriptionTimebook(d)) {
				var t = SubscriptionTimebooks[0];
				if (t.IsInvoiced || t.IsPaid) {
					return "";
				} else {
					return " class='danger'";
				}
			}
			else {
				return "";
			}
		}

		public bool CantCreateTimebook(DateTime d)
		{
			return d.Date < SubscriptionStartDate.Value.Date;
		}

		public bool HasSubscriptionTimebook(DateTime d)
		{
			bool found = false;
			if (HasSubscriptionTimebooks) {
				found = true;
			}
			return found;
		}

//		public static List<Currency> GetCurrencies()
//		{
//			return new List<Currency>(
//				new[] {
//					new Currency { Name = "Pound Sterling", Code = "GBP" },
//					new Currency { Name = "US Dollar", Code = "USD" }
//				}
//			);
//		}

		public override string ToString()
		{
			return string.Format("{0}", InvoiceAddress);
		}

		public override void Validate()
		{
			base.Validate();
			Errors.Clear();
//			AddErrorIf(Number == "", "Customer number shouldn't be empty.");
			AddErrorIf(Name == "", "Customer name shouldn't be empty.");
//			AddErrorIf(InvoiceAddress == "", "Invoice address shouldn't be empty.");
//			AddErrorIf(YourReferencePerson == "", "Your reference person shouldn't be empty.");
//			AddErrorIf(OurReferencePerson == "", "Our reference person shouldn't be empty.");
		}
	}
	
	public class CustomerItem : BaseModel
	{
		public Customer Customer { get; set; }
		public Item Item { get; set; }
		public decimal Price { get; set; }
		public bool Inactive { get; set; }
		public int SortOrder { get; set; }
	}
	
	public class CustomerContact : BaseModel
	{
		public Customer Customer { get; set; }
		public string Name { get; set; }
		public string Title { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public bool Inactive { get; set; }
		public int Type { get; set; }
		public string PurchaseOrderNumber { get; set; }

		public override void Validate()
		{
			base.Validate();
			AddErrorIf(Name == "", "Contact person name shouldn't be empty.");
		}
		
		public string GetContactType()
		{
			switch (Type) {
					case 1: return "<span class='label label-success'>Primary</span>";
					case 2: return "<span class='label label-warning'>Secondary</span>";
					default: return "<span class='label label-default'>Other</span>";
			}
		}

        public override string ToString()
        {
            string p = PurchaseOrderNumber != null && PurchaseOrderNumber != "" ? " (" + PurchaseOrderNumber + ")" : "";
            return string.Format("{0}{1}", Name, p);
        }
	}
	
	public class CustomerNotes : BaseModel
	{
		public Customer Customer { get; set; }
		public string Notes { get; set; }
		public DateTime? CreatedAt { get; set; }
		public User CreatedBy { get; set; }
		public bool Inactive { get; set; }
	}

	public class CustomerAgreement : BaseModel
	{
		public DateTime? Date { get; set; }
		public Customer Customer { get; set; }
		public string Lecturer { get; set; }
		public string LectureTitle { get; set; }
		public string Contact { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public decimal Compensation { get; set; }
		public string PaymentTerms { get; set; }
		public string BillingAddress { get; set; }
		public string OtherInformation { get; set; }

		public string ContactPlaceSigned { get; set; }
		public DateTime? ContactDateSigned { get; set; }
		public string ContactName { get; set; }
		public string ContactTitle { get; set; }
		public string ContactCompany { get; set; }

		public DateTime? DateSigned { get; set; }

		public bool IsClosed { get; set; }
		public List<CustomerAgreementDateTimeAndPlace> DateTimeAndPlaces { get; set; }
		
		public CustomerAgreement()
		{
			DateTimeAndPlaces = new List<CustomerAgreementDateTimeAndPlace>();
		}
		
		public string CustomerToString()
		{
			return string.Format("{0}, {1} {2}", ContactName, ContactTitle, ContactCompany);
		}
		
		public override void Validate()
		{
			base.Validate();
//			AddErrorIf(Lecturer == "", "Lecturer should not be empty.");
//			AddErrorIf(LectureTitle == "", "Lecture title should not be empty.");
//			AddErrorIf(Contact == "", "Contact should not be empty.");
		}
	}
	
	public class CustomerAgreementDateTimeAndPlace : BaseModel
	{
		public CustomerAgreement CustomerAgreement { get; set; }
		public DateTime? Date { get; set; }
		public string TimeFrom { get; set; }
		public string TimeTo { get; set; }
		public string Runtime { get; set; }
		public string Address { get; set; }
	}
	
	public class CustomerTimebook : BaseModel
	{
		public const int INVOICED = 1;
		public const int PAID = 2;
		public const int OPEN = 0;

		public bool IsSubscription { get; set; }
		public DateTime? SubscriptionStartDate { get; set; }
		public DateTime? SubscriptionEndDate { get; set; }
		public Customer Customer { get; set; }
		public DateTime? Date { get; set; }
		public bool DateHidden { get; set; }
		public CustomerContact Contact { get; set; }
		public Item Item { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public string Consultant { get; set; }
		public string Comments { get; set; }
		public string Department { get; set; }
		public bool Inactive { get; set; }
		public int Status { get; set; }
		public string InternalComments { get; set; }
		public bool IsHeader { get; set; }
		public bool HasInternalComments {
			get { return InternalComments != null && InternalComments != ""; }
		}
		public decimal VAT { get; set; }
		public InvoiceTimebook InvoiceTimebook { get; set; }
		public decimal VATAmount
		{
			get { return Amount * VAT / 100;  }
		}
		public decimal Amount {
			get {
				return Price * Quantity;
			}
		}
		public string GetDepartmentAndContact()
		{
			string s = "";
			s += StrHelper.Str(Department != null && Department != "", Department + "<br>", "");
			s += StrHelper.Str(Contact.Name != "", "<i>" + Contact.Name + "</i>", "");
			return s;
		}
        public string GetDepartmentAndContact2()
        {
            string s = "";
            s += StrHelper.Str(Department != null && Department != "", Department, "");
            s += StrHelper.Str(Contact.Name != null && Contact.Name != "", " - " + Contact.Name, "");
            return s;
        }

		public override string ToString()
		{
			string c = Consultant != null && Consultant != "" ? string.Format(" ({0})", Consultant) : "";
			string d = Date != null && !DateHidden ? Date.Value.ToString("yyyy-MM-dd") + " " : "";
			return string.Format("{2}{0}{1}", Comments, c, d);
		}

		public bool IsPaid
		{
			get { return Status == PAID;  }
		}
		
		public bool IsOpen {
			get { return Status == OPEN; }
		}

		public bool IsInvoiced
		{
			get { return Status == INVOICED; }
		}

		public void ValidateSubscription()
		{
			base.Validate();
			AddErrorIf(Comments == "", "Comments shouldn't be empty.");
		}

		public override void Validate()
		{
			base.Validate();
			AddErrorIf(Comments == "", "Comments shouldn't be empty.");
			AddErrorIf(Price <= 0, "Price should be greater than zero.");
			//AddErrorIf(VAT <= 0, "VAT should be greater than zero.");
		}

		public string GetStatus()
		{
			switch (Status)
			{
					case INVOICED: return string.Format("<span class='label label-warning'>INVOICED</span><br><span class='label label-warning'>{0}</span>", HtmlHelper.Anchor(InvoiceTimebook.Invoice.Number, "invoiceshow.aspx?Id=" + InvoiceTimebook.Invoice.Id));
					case PAID: return string.Format("<span class='label label-success'>PAID</span><br><span class='label label-success'>{0}</span>", HtmlHelper.Anchor(InvoiceTimebook.Invoice.Number, "invoiceshow.aspx?Id=" + InvoiceTimebook.Invoice.Id));
					default: return "<span class='label label-default'>OPEN</span>";
			}
		}
	}
}
