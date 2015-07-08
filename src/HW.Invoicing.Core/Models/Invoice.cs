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
	public class InvoiceExporter
	{
		public string Type {
			get { return "application/pdf"; }
		}

		public string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=\"{0}.pdf\";", file);
		}

		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
		public void Export2(Invoice invoice, string existingFileName)
		{
			string newFile = @"new.pdf";
			PdfReader reader = new PdfReader(existingFileName);
			PdfStamper stamper = new PdfStamper(reader, new FileStream(newFile, FileMode.Create));
			AcroFields form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
			form.SetField("Text1", invoice.Customer.Number);
			form.SetField("Text2", invoice.Number);
			form.SetField("Text3", invoice.Date.Value.ToString("yyyy-MM-dd"));
			form.SetField("Text4", invoice.MaturityDate.Value.ToString("yyyy-MM-dd"));
			
			form.SetField("Text5", invoice.Customer.YourReferencePerson);
			form.SetField("Text6", invoice.Customer.OurReferencePerson);
			
			form.SetField("Text6B", invoice.Customer.ToString());
			
			form.SetField("Text10b", invoice.SubTotal.ToString());
			form.SetField("Text13", invoice.TotalAmount.ToString());
			
			string items = "";
			string quantities = "";
			string units = "";
			string prices = "";
			string amounts = "";
			foreach (var t in invoice.Timebooks) {
				items += t.ToString() + "\n\n";
				quantities += t.Timebook.Quantity.ToString() + "\n\n";
				units += t.Timebook.Item.Unit.Name + "\n\n";
				prices += t.Timebook.Price.ToString() + "\n\n";
				amounts += t.Timebook.Amount.ToString() + "\n\n";
			}
			form.SetField("Text7", items);
			form.SetField("Text8", quantities);
			form.SetField("Text9", units);
			form.SetField("Text9b", prices);
			form.SetField("Text9c", amounts);

			if (invoice.VATs.ContainsKey(25))
			{
				form.SetField("Text11b", 25.ToString());
				form.SetField("Text12", invoice.VATs[25].ToString());
			}

			// "Flatten" the form so it wont be editable/usable anymore
			stamper.FormFlattening = true;

			// You can also specify fields to be flattened, which
			// leaves the rest of the form still be editable/usable
			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}

			stamper.Close();
			reader.Close();
		}
		
		public MemoryStream Export(Invoice invoice, string existingFileName)
		{
			MemoryStream output = new MemoryStream();

			var existingFileStream = new FileStream(existingFileName, FileMode.Open);
			var reader = new PdfReader(existingFileStream);
			var stamper = new PdfStamper(reader, output) {
//				FormFlattening = true,
				FreeTextFlattening = true
			};
			var form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
			form.SetField("Text1", invoice.Customer.Number);
			form.SetField("Text2", invoice.Number);
			form.SetField("Text3", invoice.Date.Value.ToString("yyyy-MM-dd"));
			form.SetField("Text4", invoice.MaturityDate.Value.ToString("yyyy-MM-dd"));
			
			form.SetField("Text5", invoice.Customer.YourReferencePerson);
			form.SetField("Text6", invoice.Customer.OurReferencePerson);
			
			form.SetField("Text6B", invoice.Customer.ToString());
			
			form.SetField("Text10b", invoice.SubTotal.ToString());
			form.SetField("Text13", invoice.TotalAmount.ToString());
			
			string items = "";
			string quantities = "";
			string units = "";
			string prices = "";
			string amounts = "";
			foreach (var t in invoice.Timebooks) {
				items += t.ToString() + "\n\n";
				quantities += t.Timebook.Quantity.ToString() + "\n\n";
				units += t.Timebook.Item.Unit.Name + "\n\n";
				prices += t.Timebook.Price.ToString() + "\n\n";
				amounts += t.Timebook.Amount.ToString() + "\n\n";
			}
			form.SetField("Text7", items);
			form.SetField("Text8", quantities);
			form.SetField("Text9", units);
			form.SetField("Text9b", prices);
			form.SetField("Text9c", amounts);

			if (invoice.VATs.ContainsKey(25))
			{
				form.SetField("Text11b", 25.ToString());
				form.SetField("Text12", invoice.VATs[25].ToString());
			}

			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}
//			reader.Close();
//			stamper.Close();

			return output;
		}

		byte[] FlattenPdfFormToBytes(PdfReader reader)
		{
			var output = new MemoryStream();
			var stamper = new PdfStamper(reader, output) { FormFlattening = true };
			stamper.Close();
			return output.ToArray();
		}
	}
	
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
