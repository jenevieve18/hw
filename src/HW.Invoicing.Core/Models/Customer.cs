using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Models
{
    public class Currency : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, Code);
        }
    }

	public class Customer : BaseModel
	{
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
        public string PostalAddress { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string YourReferencePerson { get; set; }
        public string OurReferencePerson { get; set; }
        public bool Inactive { get; set; }
        public Language Language { get; set; }

        public static List<Currency> GetCurrencies()
        {
            return new List<Currency>(
                new[] {
                    new Currency { Name = "Pound Sterling", Code = "GBP" },
                    new Currency { Name = "US Dollar", Code = "USD" }
                }
            );
        }

        public override string ToString()
        {
            return string.Format("{0}\n{1}", Name, InvoiceAddress);
        }

        public override void Validate()
        {
            base.Validate();
            Errors.Clear();
            AddErrorIf(Number == "", "Customer number shouldn't be empty.");
            AddErrorIf(Name == "", "Customer name shouldn't be empty.");
            AddErrorIf(PostalAddress == "", "Postal address shouldn't be empty.");
            AddErrorIf(InvoiceAddress == "", "Invoice address shouldn't be empty.");
            AddErrorIf(YourReferencePerson == "", "Your reference person shouldn't be empty.");
            AddErrorIf(OurReferencePerson == "", "Our reference person shouldn't be empty.");
        }

        public IList<CustomerContact> Contacts { get; set; }

        public CustomerContact FirstPrimaryContact
        {
            get
            {
                if (HasPrimaryContacts)
                {
                    return PrimaryContacts[0];
                }
                return null;
            }
        }

        public CustomerContact SecondaryContact
        {
            get {
                if (HasSecondaryContacts)
                {
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
	}
	
	public class Unit : BaseModel
	{
		public string Name { get; set; }
		public bool Inactive { get; set; }
	}

    public class Language : BaseModel
    {
        public string Name { get; set; }
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
		public string Contact { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public string Email { get; set; }
		public bool Inactive { get; set; }
		public int Type { get; set; }

        public override void Validate()
        {
            base.Validate();
            AddErrorIf(Contact == "", "Contact person name shouldn't be empty.");
        }
		
		public string GetContactType()
		{
			switch (Type) {
					case 1: return "<span class='label label-success'>Primary</span>";
					case 2: return "<span class='label label-warning'>Secondary</span>";
					default: return "<span class='label label-default'>Other</span>";
			}
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
	
	public class CustomerTimebook : BaseModel
	{
        public const int INVOICED = 1;
        public const int PAID = 2;

        public bool IsSubscription { get; set; }
        public DateTime? SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionEndDate { get; set; }
        public Customer Customer { get; set; }
        public DateTime? Date { get; set; }
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

        public override string ToString()
        {
            if (Contact != null && Contact.Id != 0)
            {
                return string.Format("{0} ({1})", Comments, Consultant);
            }
            else
            {
                return Item.Name;
            }
        }

        public bool IsPaid
        {
            get { return Status == 2;  }
        }

        public override void Validate()
        {
            base.Validate();
            AddErrorIf(Department == "", "Department shouldn't be empty.");
            AddErrorIf(Consultant == "", "Consultant shouldn't be empty.");
            AddErrorIf(Comments == "", "Comments shouldn't be empty.");
            AddErrorIf(Price <= 0, "Price should be greater than zero.");
            AddErrorIf(VAT <= 0, "VAT should be greater than zero.");
        }

        public string GetStatus()
        {
            switch (Status)
            {
                case INVOICED: return string.Format("<span class='label label-warning'>INVOICED {0}</span>", HtmlHelper.Anchor(InvoiceTimebook.Invoice.Number, "invoiceshow.aspx?Id=" + InvoiceTimebook.Invoice.Id));
                case PAID: return "<span class='label label-success'>PAID</span>";
                default: return "<span class='label label-default'>OPEN</span>";
            }
        }
	}
}
