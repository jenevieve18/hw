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

namespace HW.Invoicing.Core.Helpers
{
	public class HCGEAgreementExporter
	{
//		public MemoryStream Export(CustomerAgreement agreement, string templateFileName, string calibriFont)
//		{
//			MemoryStream output = new MemoryStream();
//			
//			var templateFileStream = new FileStream(templateFileName, FileMode.Open);
//			var reader = new PdfReader(templateFileStream);
//			var stamper = new PdfStamper(reader, output) {};
//			var form = stamper.AcroFields;
//			var fieldKeys = form.Fields.Keys;
//			
//			form.SetField("Avtalsnr", agreement.Id.ToString("000"));
//			form.SetField("Kund", agreement.Customer.ToString());
//			form.SetField("Datum", agreement.Date.Value.ToString("yyyy-MM-dd"));
//			// Speltid
//			form.SetField("Föreläsningstitel", agreement.LectureTitle);
//			// Plats
//			form.SetField("Mobil", agreement.Mobile);
//			form.SetField("Ersättning", agreement.Compensation.ToString());
//			form.SetField("Faktureringsadress", agreement.BillingAddress);
//			form.SetField("Övrig information", agreement.OtherInformation);
//			form.SetField("Ort & Datum 1", string.Format("{0}, {1}", agreement.ContactPlaceSigned, agreement.ContactDateSigned));
//			form.SetField("Namnförtydligande", agreement.CustomerToString());
//			
//			stamper.FormFlattening = true;
//			foreach (var s in form.Fields.Keys)
//			{
//				stamper.PartialFormFlattening(s);
//			}
//			
//			stamper.Writer.CloseStream = false;
//			stamper.Close();
//			reader.Close();
//			
//			return output;
//		}
		
		public MemoryStream Export(CustomerAgreement a, string templateFileName, string calibriFont)
		{
			MemoryStream output = new MemoryStream();
			
			var templateFileStream = new FileStream(templateFileName, FileMode.Open);
			var reader = new PdfReader(templateFileStream);
			var stamper = new PdfStamper(reader, output) {};
			var form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
			form.SetField("Number1", a.Id.ToString("000"));
			form.SetField("Number2", a.Id.ToString("000"));
			form.SetField("Number3", a.Id.ToString("000"));
			
			form.SetField("Customer", string.Format("{0}", a.Customer.InvoiceAddress));
			form.SetField("LectureTitle", a.LectureTitle);
			
			string dates = "";
			string timefroms = "";
			string timetos = "";
			string  places = "";
			foreach (var d in a.DateTimeAndPlaces) {
				dates += d.Date.Value.ToString("yyyy-MM-dd") + "\n";
				timefroms += d.TimeFrom + "\n";
				timetos += d.TimeTo + "\n";
				places += d.Address + "\n";
			}
			form.SetField("Dates", dates);
			form.SetField("TimeFroms", timefroms);
			form.SetField("TimeTos", timetos);
			form.SetField("Places", places);

			form.SetField("ContactPerson", a.Contact);
			form.SetField("Mobile", a.Mobile);
			form.SetField("Email", a.Email);
			form.SetField("Compensation", a.Compensation.ToString());
			form.SetField("OtherInformation", a.OtherInformation);
			
			var ds = a.ContactDateSigned != null ? a.ContactDateSigned.Value.ToString("yyyy-MM-dd") : "";
			form.SetField("ContactPlaceAndDateSigned", string.Format("{0} den {1}", a.ContactPlaceSigned, ds));
			ds = a.DateSigned != null ? a.DateSigned.Value.ToString("yyyy-MM-dd") : "";
			form.SetField("DateSigned", string.Format("Stockholm den {0}", ds));
			form.SetField("ContactNameTitleAndCompany", string.Format("{0}, {1}\n{2}", a.ContactName, a.ContactTitle, a.ContactCompany));
			
			stamper.FormFlattening = true;
			foreach (var s in form.Fields.Keys)
			{
				stamper.PartialFormFlattening(s);
			}
			
			stamper.Writer.CloseStream = false;
			stamper.Close();
			reader.Close();
			
			return output;
		}
	}
}
