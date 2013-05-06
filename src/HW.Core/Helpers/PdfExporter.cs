//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

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
	public class PdfExporter : IExporter
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
		
		public string Type {
			get { return "application/pdf"; }
		}
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public string ContentDisposition {
			get { return ""; }
		}
		
//		public object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path, int distribution)
		public object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
//			string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot, distribution);
			string url = GetUrl(path, langID, fy, ty, spons, sid, gb, r.Id, pruid, gid, grpng, plot);
			doc.Add(new Chunk(r.CurrentLanguage.Subject));
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			return output;
		}
		
//		public object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path, int distribution)
		public object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			int i = 0;
			foreach (var p in parts) {
//				string url = GetUrl(path, langID, fy, ty, spons, sid, gb, p.ReportPart.Id, pruid, gid, grpng, plot, distribution);
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
		
//		string GetUrl(string path, int langID, int fy, int ty, int spons, int sid, int gb, int rpid, int pruid, string gid, int grpng, string plot, int distribution)
		string GetUrl(string path, int langID, int fy, int ty, int spons, int sid, int gb, int rpid, int pruid, string gid, int grpng, string plot)
		{
			P p = new P(path, "reportImage.aspx");
			p.Q.Add("LangID", langID);
			p.Q.Add("FY", fy);
			p.Q.Add("TY", ty);
			p.Q.Add("SAID", spons);
			p.Q.Add("SID", sid);
			p.Q.Add("GB", gb);
			p.Q.Add("RPID", rpid);
			p.Q.Add("PRUID", pruid);
			p.Q.Add("GID", gid);
			p.Q.Add("GRPNG", grpng);
			p.Q.Add("PLOT", plot);
//			p.Q.Add("DIST", distribution);
			return p.ToString();
		}
	}
}
