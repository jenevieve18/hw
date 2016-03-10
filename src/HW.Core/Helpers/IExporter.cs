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
	public class ExportFactory
	{
		public static readonly string Pdf = "pdf";
		public static readonly string Csv = "csv";
		public static readonly string WordDocument = "docx";
//		public static readonly string SpreadsheetDocument = "xlsx";
		public static readonly string PresentationDocument = "pptx";
		public static readonly string Excel = "xls";

//		public static IExporter GetExporter(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key, string template)
		public static IExporter GetExporter(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, ReportPart r, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(r);
			} else if (type == Csv) {
//				return new CsvExporter(service, hasAnswerKey, hasGrouping, width, height, background, r);
				return new CsvExporter(service, hasAnswerKey, hasGrouping, r);
			} else if (type == Excel) {
//				return new ExcelExporter(service, hasAnswerKey, hasGrouping, width, height, background, r, key);
				return new ExcelExporter(service, hasAnswerKey, hasGrouping, r);
			} else if (type == WordDocument) {
//				return new WordDocumentExporter2(r);
				return new DocXExporter(r, template);
//			} else if (type == SpreadsheetDocument) {
//				return new SpreadsheetDocumentExporter(r);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(r);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetExporterAll(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, IList<ReportPartLanguage> parts, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(service, parts);
			} else if (type == Csv) {
//				return new CsvExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, parts, key);
				return new CsvExporter(service, hasAnswerKey, hasGrouping, parts);
			} else if (type == Excel) {
//				return new ExcelExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, parts, key);
				return new ExcelExporter(service, hasAnswerKey, hasGrouping, parts);
			} else if (type == WordDocument) {
//				return new WordDocumentExporter2(service, parts);
				return new DocXExporter(service, parts, template);
//			} else if (type == SpreadsheetDocument) {
//				return new SpreadsheetDocumentExporter(service, parts);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(service, parts);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetSuperExporter(string type, ReportPart r, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(r);
			} else if (type == Excel) {
				return new ExcelExporter(r);
			} else if (type == WordDocument) {
				return new DocXExporter(r, template);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(r);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetSuperExporterAll(string type, ReportService service, IList<ReportPartLanguage> parts, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(service, parts);
			} else if (type == Excel) {
//				return new ExcelExporter(service, hasAnswerKey, hasGrouping, parts);
				return new ExcelExporter(service, false, false, parts);
			} else if (type == WordDocument) {
				return new DocXExporter(service, parts, template);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(service, parts);
			} else {
				throw new NotSupportedException();
			}
		}
	}
	
	public interface IExporter
	{
		string Type { get; }
		
		bool HasContentDisposition(string file);
		
		string GetContentDisposition(string file);
		
		string ContentDisposition2 { get; }
		
		bool HasContentDisposition2 { get; }
		
//		object Export(string url);
		object Export(string url, int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		object ExportAll(int langID);
		object ExportAll(int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
//		
//		object ExportAll(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		object SuperExport(string url);
		object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
		
//		object SuperExportAll(int langID);
		object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
		
		event EventHandler<ReportPartEventArgs> UrlSet;
		
//		event EventHandler<ExcelWriterEventArgs> GraphCreate;
	}
	
	
	public abstract class AbstractExporter : IExporter
	{
		public abstract string Type { get; }
		
		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
//		public event EventHandler<ExcelWriterEventArgs> GraphCreate;
//		
//		protected virtual void OnGraphCreate(ExcelWriterEventArgs e)
//		{
//			if (GraphCreate != null) {
//				GraphCreate(this, e);
//			}
//		}
		
		public event EventHandler<ReportPartEventArgs> UrlSet;
		
		protected virtual void OnUrlSet(ReportPartEventArgs e)
		{
			if (UrlSet != null) {
				UrlSet(this, e);
			}
		}
		
		public abstract string GetContentDisposition(string file);
		
		public bool HasContentDisposition2 {
			get { return ContentDisposition2.Length > 0; }
		}
		
		public abstract string ContentDisposition2 { get; }
		
//		public abstract object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
//		
//		public abstract object ExportAll(int gb, int fy, int ty, int langID, int pruid, int GRPNG, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		public abstract object Export(string url);
		public abstract object Export(string url, int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		public abstract object ExportAll(int langID);
		public abstract object ExportAll(int langID, int pruid, int fy, int ty, int gb, int plot, int grpng, int spons, int sid, string gid, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		public abstract object SuperExport(string url);
		public abstract object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
		
//		public abstract object SuperExportAll(int langID);
		public abstract object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);

//		protected string GetUrl(string path, int langID, int fy, int ty, int spons, int sid, int gb, int rpid, int pruid, string gid, int grpng, int plot, int fm, int tm)
//		{
//			P p = new P(path, "reportImage.aspx");
//			p.Q.Add("LangID", langID);
//			p.Q.Add("FY", fy);
//			p.Q.Add("TY", ty);
//			p.Q.Add("FM", fm);
//			p.Q.Add("TM", tm);
//			p.Q.Add("SAID", spons);
//			p.Q.Add("SID", sid);
//			p.Q.Add("GB", gb);
//			p.Q.Add("RPID", rpid);
//			p.Q.Add("PRUID", pruid);
//			p.Q.Add("GID", gid);
//			p.Q.Add("GRPNG", grpng);
//			p.Q.Add("PLOT", plot);
//			return p.ToString();
//		}
	}
	
	public abstract class BinaryExporter : AbstractExporter
	{
		public override string Type {
			get { return "application/octet-stream"; }
		}
	}
	
	public class Distribution
	{
		public const int None = 0;
		public const int StandardDeviation = 1;
		public const int ConfidenceInterval = 2;
	}
}
