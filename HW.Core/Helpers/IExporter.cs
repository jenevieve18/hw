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
	public class ExportFactory
	{
		public static readonly string Pdf = "pdf";
		public static readonly string Csv = "csv";
		public static readonly string WordDocument = "docx";
		public static readonly string PresentationDocument = "pptx";
		
		public static IExporter GetExporter(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key)
		{
			if (type == Pdf) {
				return new PdfExporter(r);
			} else if (type == Csv) {
				return new CsvExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, r, key);
			} else if (type == WordDocument) {
				return new WordDocumentExporter(r);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter();
			} else {
				throw new NotSupportedException();
			}
		}
		
		public static IExporter GetExporter2(ReportService service, string type, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, IList<ReportPartLanguage> parts, string key)
		{
			if (type == Pdf) {
				return new PdfExporter(service, parts);
			} else if (type == Csv) {
				return new CsvExporter(service, hasAnswerKey, hasGrouping, disabled, width, height, background, parts, key);
			} else if (type == WordDocument) {
				return new WordDocumentExporter(service, parts);
			} else if (type == PresentationDocument) {
				return new PresentationDocumentExporter();
			} else {
				throw new NotSupportedException();
			}
		}
	}
	
	public interface IExporter
	{
		string Type { get; }
		
		bool HasContentDisposition { get; }
		
		string ContentDisposition { get; }
		
		object Export(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution);
		
		object Export2(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution);
	}
	
	public abstract class AbstractExporter : IExporter
	{
		public abstract string Type { get; }
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public abstract string ContentDisposition { get; }
		
		public abstract object Export(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution);
		
		public abstract object Export2(int GB, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution);
	}
	
	public class Distribution
	{
		public const int None = 0;
		public const int StandardDeviation = 1;
		public const int ConfidenceInterval = 2;
	}
}
