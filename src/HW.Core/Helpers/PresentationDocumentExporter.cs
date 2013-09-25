//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml;
using A = DocumentFormat.OpenXml.Drawing;
using P14 = DocumentFormat.OpenXml.Office2010.PowerPoint;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;

using HW.Core.Models;
using HW.Core.Services;

namespace HW.Core.Helpers
{
	public class PresentationDocumentExporter : AbstractExporter
	{
		ReportPart r;
		IList<ReportPartLanguage> parts;
		ReportService service;
		GeneratedClass gc = new GeneratedClass();
		
		public PresentationDocumentExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public PresentationDocumentExporter(ReportService service, IList<ReportPartLanguage> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/octet-stream"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=HealthWatch {0} {1}.pptx;", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=HealthWatch Survey {0}.pptx;", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot,	string path)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.UrlSet += delegate(object sender, ReportPartLanguageEventArgs e) { e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot); };
				r.CurrentLanguage.ReportPart = r; // HACK: Report part should be assigned to the language because upon querying from database it's not set.
				gc.CreateParts(package, new List<ReportPartLanguage>(new ReportPartLanguage[] { r.CurrentLanguage }));
			}
			return output;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.UrlSet += delegate(object sender, ReportPartLanguageEventArgs e) { e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot); };
				gc.CreateParts(package, parts);
			}
			return output;
		}
	}
}
