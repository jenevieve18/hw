//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
		
		public object Export(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			string url = string.Format(
				@"{12}reportImage.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&STDEV={5}&DIST={13}&GB={6}&RPID={7}&PRUID={8}&GID={9}&GRPNG={10}&Plot={11}",
				langID,
				fy,
				ty,
				SPONS,
				SID,
				0, // TODO: No use for standard deviation. Current use is extra point or "distribution".
				GB,
				r.Id,
				PRUID,
				GID,
				GRPNG,
				plot,
				path,
				distribution
			);
			doc.Add(new Chunk(r.CurrentLanguage.Subject));
			iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
			jpg.ScaleToFit(500f, 500f);
			doc.Add(jpg);
			doc.Close();
			return output;
		}
		
		public object Export2(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution)
		{
			Document doc = new Document();
			var output = new MemoryStream();
			PdfWriter writer = PdfWriter.GetInstance(doc, output);
			doc.Open();
			
			foreach (var p in parts) {
				string url = string.Format(
					@"{12}reportImage.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&STDEV={5}&DIST={13}&GB={6}&RPID={7}&PRUID={8}&GID={9}&GRPNG={10}&Plot={11}",
					langID,
					fy,
					ty,
					SPONS,
					SID,
					0, // TODO: No use for standard deviation. Current use is extra point or "distribution".
					GB,
					p.ReportPart.Id,
					PRUID,
					GID,
					GRPNG,
					plot,
					path,
					distribution
				);
				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
				doc.Add(new Chunk(r.CurrentLanguage.Subject));
				iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(new Uri(url));
				jpg.ScaleToFit(500f, 500f);
				doc.Add(jpg);
			}
			doc.Close();
			return output;
		}
	}
}
