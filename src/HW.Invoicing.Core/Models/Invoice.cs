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
		public const int PAID = 2;

		DateTime? date;
		
		public Nullable<DateTime> Date {
			get { return date; }
			set { date = value; MaturityDate = date.Value.AddDays(30); }
		}
		public DateTime? MaturityDate { get; set; }
		public Customer Customer { get; set; }
		public IList<InvoiceTimebook> Timebooks { get; set; }
		public string Comments { get; set; }
		public string Number { get; set; }
		public int Status { get; set; }
		public string InternalComments { get; set; }
		public bool Exported { get; set; }

		public string GetStatus()
		{
			switch (Status)
			{
					case PAID: return "<span class='label label-success'>PAID</span>";
					default: return "<span class='label label-danger'>NOT PAID</span>";
			}
		}
		
		public Invoice()
		{
			Date = DateTime.Now;
			Timebooks = new List<InvoiceTimebook>();
		}

		public IDictionary<decimal, decimal> VATs
		{
			get {
				var v = new Dictionary<decimal, decimal>();
				foreach (var t in Timebooks)
				{
					if (v.ContainsKey(t.Timebook.VAT))
					{
						v[t.Timebook.VAT] += t.Timebook.VATAmount;
					}
					else
					{
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
				foreach (var x in Timebooks)
				{
					t += (x.Timebook.VAT / 100) * x.Timebook.Amount;
				}
				return t;
			}
		}

		public decimal SubTotal
		{
			get
			{
				return Timebooks.Sum(x => x.Timebook.Amount);
			}
		}
		
		public decimal TotalAmount {
			get {
				return SubTotal + TotalVAT;
			}
		}

		public void AddTimebook(string[] timebooks)
		{
			foreach (var t in timebooks)
			{
				var s = t.Split('|');
				if (s.Length > 1)
				{
					AddTimebook(
						new InvoiceTimebook
						{
							Timebook = new CustomerTimebook
							{
								Id = ConvertHelper.ToInt32(s[0]),
								Customer = new Customer { Id = ConvertHelper.ToInt32(s[1]) },
								Item = new Item { Id = ConvertHelper.ToInt32(s[2]) },
								Price = ConvertHelper.ToDecimal(s[3]),
								VAT = ConvertHelper.ToDecimal(s[4]),
								Quantity = ConvertHelper.ToDecimal(s[5]),
								Comments = s[6]
							}
						}
					);
				}
				else
				{
					AddTimebook(ConvertHelper.ToInt32(t));
				}
			}
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
		
		public override string ToString()
		{
			return Timebook.ToString();
		}
	}
}
