using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing.Core.Models
{
	public class Invoice : BaseModel
	{
		public DateTime? Date { get; set; }
		public Customer Customer { get; set; }
        public IList<InvoiceTimebook> Timebooks { get; set; }
        public string Comments { get; set; }
        public string Number { get; set; }
		
		public Invoice()
		{
            Date = DateTime.Now;
            Timebooks = new List<InvoiceTimebook>();
		}
		
		public decimal TotalAmount {
			get { return Timebooks.Sum(x => x.Timebook.Amount); }
		}
		
		public void AddTimebook(int id)
		{
			AddTimebook(new InvoiceTimebook { Timebook = new CustomerTimebook { Id = id }});
		}
		
		public void AddTimebook(InvoiceTimebook t)
		{
			Timebooks.Add(t);
		}
	}

    public class InvoiceTimebook : BaseModel
    {
        public Invoice Invoice { get; set; }
        public CustomerTimebook Timebook { get; set; }
    }
}
