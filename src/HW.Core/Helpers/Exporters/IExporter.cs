using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HW.Core.Helpers.Exporters;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core.Helpers
{
    public interface IExporter
    {
        string Type { get; }

        bool HasContentDisposition(string file);

        string GetContentDisposition(string file);

        string ContentDisposition2 { get; }

        bool HasContentDisposition2 { get; }

        object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);

        object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);

        object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);

        object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);

        event EventHandler<ReportPartEventArgs> UrlSet;

        event EventHandler<ExcelCellEventArgs> CellWrite;
    }

	public class ExportFactory
	{
		public static readonly string Pdf = "pdf";
		public static readonly string Csv = "csv";
		public static readonly string WordDocument = "docx";
		public static readonly string PresentationDocument = "pptx";
		public static readonly string Excel = "xls";

		public static IExporter GetExporter(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, ReportPart r, string template)
		{
			if (type == Pdf) {
				return new PdfStatsExporter(r);
			} else if (type == Csv) {
				return new CsvStatsExporter(service, hasAnswerKey, hasGrouping, r);
			} else if (type == Excel) {
				return new ExcelStatsExporter(service, hasAnswerKey, hasGrouping, r);
			} else if (type == WordDocument) {
				return new DocXStatsExporter(r, template);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentStatsExporter(r);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetExporterAll(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, IList<IReportPart> parts, string template)
		{
			if (type == Pdf) {
				return new PdfStatsExporter(service, parts);
			} else if (type == Csv) {
				return new CsvStatsExporter(service, hasAnswerKey, hasGrouping, parts);
			} else if (type == Excel) {
				return new ExcelStatsExporter(service, hasAnswerKey, hasGrouping, parts);
			} else if (type == WordDocument) {
				return new DocXStatsExporter(service, parts, template);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentStatsExporter(service, parts);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetSuperExporter(string type, ReportPart r, string template)
		{
			if (type == Pdf) {
				return new PdfStatsExporter(r);
			} else if (type == Excel) {
				return new ExcelStatsExporter(r);
			} else if (type == WordDocument) {
				return new DocXStatsExporter(r, template);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentStatsExporter(r);
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetSuperExporterAll(string type, ReportService service, IList<IReportPart> parts, string template)
		{
			if (type == Pdf) {
				return new PdfStatsExporter(service, parts);
			} else if (type == Excel) {
				return new ExcelStatsExporter(service, false, false, parts);
			} else if (type == WordDocument) {
				return new DocXStatsExporter(service, parts, template);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentStatsExporter(service, parts);
			} else {
				throw new NotSupportedException();
			}
		}
	}
	
	public abstract class AbstractExporter : IExporter
	{
		public abstract string Type { get; }
		
		public bool HasContentDisposition(string file)
		{
			return GetContentDisposition(file).Length > 0;
		}
		
		public event EventHandler<ExcelCellEventArgs> CellWrite;
		
		protected virtual void OnCellWrite(ExcelCellEventArgs e)
		{
			if (CellWrite != null) {
				CellWrite(this, e);
			}
		}
		
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
		
		public abstract object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);
		
		public abstract object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs);
		
		public abstract object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
		
		public abstract object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot);
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
