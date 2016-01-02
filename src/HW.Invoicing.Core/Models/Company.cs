using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Company : BaseModel
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string BankAccountNumber { get; set; }
		public string TIN { get; set; }
		public DateTime? FinancialMonthStart { get; set; }
		public DateTime? FinancialMonthEnd { get; set; }
		public string InvoicePrefix { get; set; }
		public bool HasSubscriber { get; set; }
		public string Terms { get; set; }
		public string Signature { get; set; }
		public string AgreementEmailText { get; set; }
		public string AgreementEmailSubject { get; set; }
		public string AgreementPrefix { get; set; }
		public string OrganizationNumber { get; set; }
		public string Email { get; set; }
		public string AgreementSignedEmailText { get; set; }
		public string AgreementSignedEmailSubject { get; set; }
		public string AgreementTemplate { get; set; }
		public User User { get; set; }
		public string Website { get; set; }
		
		public string InvoiceLogo { get; set; }
		public string InvoiceTemplate { get; set; }
		public int InvoiceExporter { get; set; }
		public string InvoiceEmail { get; set; }
		public string InvoiceEmailCC { get; set; }
		public string InvoiceEmailSubject { get; set; }
		public string InvoiceEmailText { get; set; }
		
		public IList<Link> Links { get; set; }
		
		public string GetWebsiteAndEmail()
		{
			string e = Email != null && Email != "" ? Email : "";
			string w = Website != null && Website != "" ? Website : "";
			if (w == "") {
				return Email;
			} else if (e == "") {
				return Website;
			} else {
				return string.Format("{0}, {1}", w, e);
			}
		}

		public bool HasSignature
		{
			get { return Signature != null && Signature != "";  }
		}
		
		public bool HasTerms
		{
			get { return Terms != null && Terms != ""; }
		}

		public bool HasInvoiceTemplate
		{
			get { return InvoiceTemplate != null && InvoiceTemplate != "";  }
		}
		
		public bool HasAgreementTemplate
		{
			get { return AgreementTemplate != null && AgreementTemplate != ""; }
		}

		public bool HasInvoiceLogo
		{
			get { return InvoiceLogo != null && InvoiceLogo != "";  }
		}

        public override string ToString()
        {
            return string.Format("{0}\n{1}\nOrg.nr {2}", Name, Address, OrganizationNumber);
        }

        public override void Validate()
        {
            base.Validate();
            AddErrorIf(Name == "", "Company name should not be empty.");
        }
	}
	
//	public class CompanyUser : BaseModel
//	{
//		public Company Company { get; set; }
//		public User User { get; set; }
//	}
}
