using System;
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
        public string InvoiceLogo { get; set; }
        public string InvoiceTemplate { get; set; }
        public string Terms { get; set; }
        public string Signature { get; set; }
        public string AgreementEmailText { get; set; }
        public string AgreementEmailSubject { get; set; }
        public string OrganizationNumber { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}", Name, Address, OrganizationNumber);
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

        public bool HasInvoiceLogo
        {
            get { return InvoiceLogo != null && InvoiceLogo != "";  }
        }
		
		public Company()
		{
		}
	}
}
