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
//		public IList<InvoiceItem> Items { get; set; }
//		public IList<InvoicePayment> Payments { get; set; }
        public IList<InvoiceTimebook> Timebooks { get; set; }
		
		public Invoice()
		{
            Date = DateTime.Now;
//			Items = new List<InvoiceItem>();
//			Payments = new List<InvoicePayment>();
            Timebooks = new List<InvoiceTimebook>();
		}
		
//		public void AddItem(int id, double quantity, double price)
//		{
//			AddItem(new Item { Id = id }, quantity, price);
//		}
//		
//		public void AddItem(Item item, double quantity, double price)
//		{
//			AddItem(new InvoiceItem { Item = item });
//		}
//		
//		public void AddItem(InvoiceItem item)
//		{
//			item.Invoice = this;
//			Items.Add(item);
//		}
//		
//		public double AmountDue {
//			get { return TotalAmount - Payments.Sum(x => x.Amount); }
//		}
		
		public decimal TotalAmount {
			get { return Timebooks.Sum(x => x.Timebook.Amount); }
		}
		
		public void AddTimebook(int id)
		{
			AddTimebook(new InvoiceTimebook { Timebook = { Id = id }});
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
	
//	public class InvoiceItem : BaseModel
//	{
//		public Invoice Invoice { get; set; }
//		public Item Item { get; set; }
//		public double Quantity { get; set; }
//		public double Price { get; set; }
//		public double Amount {
//			get { return Quantity * Price; }
//		}
//	}
//	
//	public class InvoicePayment : BaseModel
//	{
//		public Invoice Invoice { get; set; }
//		public DateTime Date { get; set; }
//		public double Amount { get; set; }
//	}
}
