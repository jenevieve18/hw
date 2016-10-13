using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core.Util.Exporters
{
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
}
