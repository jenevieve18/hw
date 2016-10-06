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
	public class PresentationDocumentStatsExporter : AbstractExporter
	{
		ReportPart r;
		IList<IReportPart> parts;
		ReportService service;
		GeneratedClass gc = new GeneratedClass();
		
		public PresentationDocumentStatsExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public PresentationDocumentStatsExporter(ReportService service, IList<IReportPart> parts)
		{
			this.service = service;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/vnd.openxmlformats-officedocument.presentationml.presentation"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=\"HealthWatch {0} {1}.pptx\";", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=\"HealthWatch Survey {0}.pptx\";", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
//		public override object Export(string url, int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		public override object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) {
					e.Url = url;
					OnUrlSet(e);
				};
//				r.CurrentLanguage.ReportPart = r; // HACK: Report part should be assigned to the language because upon querying from database it's not set.
//				gc.CreateParts(package, new List<IReportPart>(new ReportPartLang[] { r.CurrentLanguage }));
				gc.CreateParts(package, new List<IReportPart>(new ReportPartLang[] { r.SelectedReportPartLang }));
			}
			return output;
		}
		
//		public override object ExportAll(int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm)
		public override object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
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
		
		public override object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			MemoryStream output = new MemoryStream();
			using (PresentationDocument package = PresentationDocument.Create(output, PresentationDocumentType.Presentation)) {
				gc.UrlSet += delegate(object sender, ReportPartEventArgs e) { e.Url =  url; };
//				r.CurrentLanguage.ReportPart = r; // HACK: Report part should be assigned to the language because upon querying from database it's not set.
//				gc.CreateParts(package, new List<IReportPart>(new ReportPartLang[] { r.CurrentLanguage }));
				gc.CreateParts(package, new List<IReportPart>(new ReportPartLang[] { r.SelectedReportPartLang }));
			}
			return output;
		}
		
		public override object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
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
