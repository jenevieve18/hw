using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class PdfWriterTests
	{
		[Test]
		public void TestMethod()
		{
			string file = @"test.pdf";
			Document doc = new Document();
			PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(file, FileMode.Create));
			doc.Open();
			string url = "http://localhost:34472/reportImage.aspx?LangID=1&FY=2012&TY=2013&SAID=0&SID=1&Anonymized=1&STDEV=0&GB=7&RPID=1&PRUID=1&GID=0,8&GRPNG=1&Plot=LinePlot";
			Image jpg = Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
		}
	}
}
