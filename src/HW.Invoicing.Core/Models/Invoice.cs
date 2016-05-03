using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Invoicing.Core.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using HW.Core.Helpers;

namespace HW.Invoicing.Core.Models
{
	public class Invoice : BaseModel
	{
		public const int INVOICED = 1;
		public const int PAID = 2;
		public const int OPEN = 0;

		DateTime? date;
		public DateTime? MaturityDate { get; set; }
		public Customer Customer { get; set; }
		public IList<InvoiceTimebook> Timebooks { get; set; }
		public string Comments { get; set; }
		public string Number { get; set; }
		public int Status { get; set; }
		public string InternalComments { get; set; }
		public bool Exported { get; set; }
		public Company Company { get; set; }
		
//		public string YourReferencePerson { get; set; }
		public string OurReferencePerson { get; set; }
//		public string PurchaseOrderNumber { get; set; }
		
		public CustomerContact CustomerContact { get; set; }
		
		public string GetContactReferenceNumber()
		{
			return CustomerContact != null ? CustomerContact.PurchaseOrderNumber : "";
		}
		
		public string GetContactName()
		{
			return CustomerContact != null ? CustomerContact.Name : "";
		}
		
		public Invoice()
		{
			Date = DateTime.Now;
			Timebooks = new List<InvoiceTimebook>();
		}
		
		public Nullable<DateTime> Date {
			get { return date; }
			set { date = value; MaturityDate = date.Value.AddDays(30); }
		}
		
		public bool IsPaid {
			get { return Status == PAID; }
		}

		public string GetStatus()
		{
			switch (Status) {
					case PAID: return "<span class='label label-success'>PAID</span>";
					default: return "<span class='label label-danger'>NOT PAID</span>";
			}
		}

		public IDictionary<decimal, decimal> VATs
		{
			get {
				var v = new Dictionary<decimal, decimal>();
				foreach (var t in Timebooks) {
					if (v.ContainsKey(t.Timebook.VAT)) {
						v[t.Timebook.VAT] += t.Timebook.VATAmount;
					} else {
						v[t.Timebook.VAT] = t.Timebook.VATAmount;
					}
				}
				return v;
			}
		}

		public decimal TotalVAT
		{
			get {
				decimal t = 0;
				foreach (var x in Timebooks) {
					t += (x.Timebook.VAT / 100) * x.Timebook.Amount;
				}
				return t;
			}
		}

		public decimal SubTotal
		{
			get {
				return Timebooks.Sum(x => x.Timebook.Amount);
			}
		}
		
		public decimal TotalAmount {
			get {
				return SubTotal + TotalVAT;
			}
		}

		public override void Validate()
		{
			base.Validate();
			AddErrorIf(Timebooks.Count <= 0, "There should be at least one timebook in an invoice.");
		}

		public void AddTimebook(string[] timebooks, string[] sortOrders)
		{
			if (timebooks != null) {
                int i = 0;
				foreach (var t in timebooks) {
                    var sortOrder = ConvertHelper.ToInt32(sortOrders[i]);
					var s = t.Split('|');
					if (s.Length > 1) {
						AddTimebook(
							new InvoiceTimebook {
								Timebook = new CustomerTimebook {
									Id = ConvertHelper.ToInt32(s[0]),
									Customer = new Customer { Id = ConvertHelper.ToInt32(s[1]) },
									Item = new Item { Id = ConvertHelper.ToInt32(s[2]) },
									Price = ConvertHelper.ToDecimal(s[3]),
									VAT = ConvertHelper.ToDecimal(s[4]),
									Quantity = ConvertHelper.ToDecimal(s[5]),
									Comments = s[6]
								},
                                SortOrder = sortOrder
							}
						);
					} else {
						AddTimebook(ConvertHelper.ToInt32(t), sortOrder);
					}
                    i++;
				}
			}
		}
		
		public void AddTimebook(int id, int sortOrder)
		{
			AddTimebook(new InvoiceTimebook { Timebook = new CustomerTimebook { Id = id }, SortOrder = sortOrder });
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
        public int SortOrder { get; set; }
		
		public override string ToString()
		{
			return Timebook.ToString();
		}
	}
}
