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
		
		public Company()
		{
		}
	}
}
