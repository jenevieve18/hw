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
//			get { return "application/octet-stream"; }
			get { return "application/vnd.openxmlformats-officedocument.presentationml.presentation"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=\"HealthWatch {0} {1}.pptx\";", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=\"HealthWatch Survey {0}.pptx\";", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
//		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			MemoryStream output = new MemoryStream();
//			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
//				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) { e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot, fm, tm); };
//				r.CurrentLanguage.ReportPart = r; // HACK: Report part should be assigned to the language because upon querying from database it's not set.
//				gc.CreateParts(package, new List<ReportPartLanguage>(new ReportPartLanguage[] { r.CurrentLanguage }));
//			}
//			return output;
//		}
//		
//		public override object ExportAll(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			MemoryStream output = new MemoryStream();
//			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
//				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) { e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot, fm, tm); };
//				gc.CreateParts(package, parts);
//			}
//			return output;
//		}
		
//		public override object Export(string url)
		public override object Export(string url, int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
//				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) { e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot, fm, tm); };
				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) {
					OnUrlSet(e);
//					e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot, fm, tm);
				};
				r.CurrentLanguage.ReportPart = r; // HACK: Report part should be assigned to the language because upon querying from database it's not set.
				gc.CreateParts(package, new List<ReportPartLanguage>(new ReportPartLanguage[] { r.CurrentLanguage }));
			}
			return output;
		}
		
//		public override object ExportAll(int langID)
		public override object ExportAll(int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
//				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) { e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot, fm, tm); };
				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) {
					OnUrlSet(e);
//					e.Url =  GetUrl(path, langID, fy, ty, spons, sid, gb, e.ReportPart.Id, pruid, gid, grpng, plot, fm, tm);
				};
				gc.CreateParts(package, parts);
			}
			return output;
		}
		
		public override object SuperExport(string url)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) { e.Url =  url; };
				r.CurrentLanguage.ReportPart = r; // HACK: Report part should be assigned to the language because upon querying from database it's not set.
				gc.CreateParts(package, new List<ReportPartLanguage>(new ReportPartLanguage[] { r.CurrentLanguage }));
			}
			return output;
		}
		
		public override object SuperExportAll(int langID)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) {
					OnUrlSet(e);
				};
				gc.CreateParts(package, parts);
			}
			return output;
		}
	}
}
