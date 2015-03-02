using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HW.Invoicing.Core.Models
{
	public class Invoice : BaseModel
	{
		public DateTime? Date { get; set; }
		public Customer Customer { get; set; }
		public IList<InvoiceItem> Items { get; set; }
	}
	
	public class InvoiceItem : BaseModel
	{
		public Invoice Invoice { get; set; }
		public Item Item { get; set; }
		public double Quantity { get; set; }
		public double Price { get; set; }
		public double Amount {
			get { return Quantity * Price; }
		}
	}
}
