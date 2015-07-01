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
		public void Export(Invoice invoice)
		{
			Document document = new Document(PageSize.A4);
			PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("Chap1002.pdf", FileMode.Open));
			
			document.Open();
			
			PdfContentByte cb = writer.DirectContent;
			
			BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
			cb.BeginText();
			cb.SetFontAndSize(bf, 12);
			
			cb.SetTextMatrix(100, 400);
			cb.ShowText("Text at position 100,400.");
			
			cb.EndText();
			document.Close();
		}
	}
	
	public class Invoice : BaseModel
	{
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
                case 2: return "<span class='label label-success'>PAID</span>";
                default: return "<span class='label label-danger'>NOT PAID</span>";
            }
        }
		
		public Invoice()
		{
			Date = DateTime.Now;
			Timebooks = new List<InvoiceTimebook>();
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
                AddTimebook(ConvertHelper.ToInt32(t));
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
	}
}
