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
		public static readonly string SpreadsheetDocument = "xlsx";
		public static readonly string PresentationDocument = "pptx";
		public static readonly string Excel = "xls";

		public static IExporter GetExporter(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(r);
			} else if (type == Csv) {
				return new CsvExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, r, key);
			} else if (type == Excel) {
				return new ExcelExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, r, key);
			} else if (type == WordDocument) {
//				return new WordDocumentExporter2(r);
				return new DocXExporter(r, template);
			} else if (type == SpreadsheetDocument) {
				return new SpreadsheetDocumentExporter(r);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(r);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetExporter2(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, IList<ReportPartLanguage> parts, string key, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(service, parts);
			} else if (type == Csv) {
				return new CsvExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, parts, key);
			} else if (type == Excel) {
				return new ExcelExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, parts, key);
			} else if (type == WordDocument) {
//				return new WordDocumentExporter2(service, parts);
				return new DocXExporter(service, parts, template);
			} else if (type == SpreadsheetDocument) {
				return new SpreadsheetDocumentExporter(service, parts);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(service, parts);
			} else {
				throw new NotSupportedException();
			}
		}
		
//		public static IExporter GetSuperExporter(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key, string template)
		public static IExporter GetSuperExporter(string type, ReportPart r, string template)
		{
			if (type == Pdf) {
				return new PdfExporter(r);
//			} else if (type == Csv) {
//				return new CsvExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, r, key);
//			} else if (type == Excel) {
//				return new ExcelExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, r, key);
			} else if (type == WordDocument) {
				return new DocXExporter(r, template);
			} else if (type == SpreadsheetDocument) {
				return new SpreadsheetDocumentExporter(r);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter(r);
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
//		
//		object Export2(string url, int langID);
		
		object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		
		object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		object SuperExport(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		object SuperExport(string url);
		
		object SuperExport2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
	}
	
	public abstract class AbstractExporter : IExporter
	{
		public abstract string Type { get; }
		
		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
		public abstract string GetContentDisposition(string file);
		
		public bool HasContentDisposition2 {
			get { return ContentDisposition2.Length > 0; }
		}
		
		public abstract string ContentDisposition2 { get; }
		
		public abstract object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		
		public abstract object Export2(int gb, int fy, int ty, int langID, int pruid, int GRPNG, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		public abstract object Export(string url);
//		
//		public abstract object Export2(string url, int langID);
		
//		public abstract object SuperExport(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);
		public abstract object SuperExport(string url);
		
		public abstract object SuperExport2(int gb, int fy, int ty, int langID, int pruid, int GRPNG, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm);

		protected string GetUrl(string path, int langID, int fy, int ty, int spons, int sid, int gb, int rpid, int pruid, string gid, int grpng, int plot, int fm, int tm)
		{
			P p = new P(path, "reportImage.aspx");
			p.Q.Add("LangID", langID);
			p.Q.Add("FY", fy);
			p.Q.Add("TY", ty);
			p.Q.Add("FM", fm);
			p.Q.Add("TM", tm);
			p.Q.Add("SAID", spons);
			p.Q.Add("SID", sid);
			p.Q.Add("GB", gb);
			p.Q.Add("RPID", rpid);
			p.Q.Add("PRUID", pruid);
			p.Q.Add("GID", gid);
			p.Q.Add("GRPNG", grpng);
			p.Q.Add("PLOT", plot);
			return p.ToString();
		}
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
