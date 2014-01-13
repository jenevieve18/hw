using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core.Helpers
{
	public class PdfExporter : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		
		public PdfExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public PdfExporter(ReportService service, IList<ReportPartLanguage> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/pdf"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return "";
		}
		
		public override string ContentDisposition2 {
			get { return ""; }
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
			doc.Add(new Chunk(r.CurrentLanguage.Subject));
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			return output;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			int i = 0;
			foreach (var p in parts) {
				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, p.ReportPart.Id, pruid, gid, grpng, plot);
				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
				doc.Add(new Chunk(r.CurrentLanguage.Subject));
				iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
				jpg.ScaleToFit(500f, 500f);
				doc.Add(jpg);
				
				if (i++ < parts.Count - 1) {
					doc.NewPage();
				}
			}
			doc.Close();
			return output;
		}
	}
}
