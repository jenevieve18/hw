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
		public MemoryStream Export(CustomerAgreement agreement, string templateFileName, string calibriFont)
		{
			MemoryStream output = new MemoryStream();
			
			var templateFileStream = new FileStream(templateFileName, FileMode.Open);
			var reader = new PdfReader(templateFileStream);
			var stamper = new PdfStamper(reader, output) {};
			var form = stamper.AcroFields;
			var fieldKeys = form.Fields.Keys;
			
//			foreach (var k in fieldKeys) {
//				form.SetField(k, k);
//			}
			
			form.SetField("Avtalsnr", agreement.Id.ToString("000"));
			form.SetField("Kund", agreement.Customer.ToString());
			form.SetField("Datum", agreement.Date.Value.ToString("yyyy-MM-dd"));
			// Speltid
			form.SetField("Föreläsningstitel", agreement.LectureTitle);
			// Plats
			form.SetField("Mobil", agreement.Mobile);
			form.SetField("Ersättning", agreement.Compensation);
			form.SetField("Faktureringsadress", agreement.BillingAddress);
			form.SetField("Övrig information", agreement.OtherInformation);
			form.SetField("Ort & Datum 1", string.Format("{0}, {1}", agreement.PlaceSigned, agreement.DateSigned));
			form.SetField("Namnförtydligande", agreement.CustomerToString());
			
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
