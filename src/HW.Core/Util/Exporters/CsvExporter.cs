using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace HW.Core.Util.Exporters
{
	public interface ICsv
	{
		string ToCsv(List<Department> departments, Dictionary<string, List<Answer>> weeks);
	}
	
	public abstract class BaseCsv : ICsv
	{
		public abstract string ToCsv(List<Department> departments, Dictionary<string, List<Answer>> weeks);
		
		protected string AddComma(string s)
		{
			return "\"" + s + "\"" + ",";
		}
	}
	
	public class CsvStatsExporter : AbstractExporter
	{
		bool hasAnswerKey;
		bool hasGrouping;
		ReportPart reportPart;
		ReportService service;
		IList<IReportPart> reportParts;
		
		public CsvStatsExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, ReportPart reportPart)
		{
			this.service = service;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.reportPart = reportPart;
		}
		
		public CsvStatsExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, IList<IReportPart> reportParts)
		{
			this.service = service;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.reportParts = reportParts;
		}
		
		public override string Type {
			get { return "text/csv"; }
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=HealthWatch Survey {0}.csv", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=HealthWatch {0} {1}.csv", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
//		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			var f = service.GetGraphFactory(hasAnswerKey);
//			return f.CreateGraph2(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled, sponsorMinUserCountToDisclose, fm, tm);
//		}
//		
//		public override object ExportAll(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose, int fm, int tm)
//		{
//			StringBuilder s = new StringBuilder();
//			foreach (var p in parts) {
//				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
//				var f = service.GetGraphFactory(hasAnswerKey);
//				string x = f.CreateGraph2(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled, sponsorMinUserCountToDisclose, fm, tm);
//				s.AppendLine(x);
//				s.AppendLine();
//			}
//			return s.ToString();
//		}
		
		public override object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			throw new NotImplementedException();
		}
		
		public override object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			throw new NotImplementedException();
		}
		
		public override object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			throw new NotImplementedException();
		}
		
		public override object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			throw new NotImplementedException();
		}
	}
	
	public class LineCsv : BaseCsv
	{
		public override string ToCsv(List<Department> departments, Dictionary<string, List<Answer>> weeks)
		{
			StringBuilder content = new StringBuilder();
			StringBuilder header1 = new StringBuilder(",");
			StringBuilder header2 = new StringBuilder(",");
			foreach (var d in departments) {
				header1.Append(AddComma(d.Name));
				header1.Append(",");
				header2.Append(AddComma("Mean"));
				header2.Append(AddComma("Count"));
			}
			header1.AppendLine();
			header2.AppendLine();
			foreach (var w in weeks.Keys) {
				content.Append(AddComma(w));
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						content.Append(AddComma(a.GetDoubleValues().Mean.ToString()));
						content.Append(AddComma(a.Values.Count.ToString()));
					} else {
						content.Append(",,");
					}
				}
				content.AppendLine();
			}
			header1.Append(header2);
			header1.Append(content);
			return header1.ToString();
		}
	}
	
	public class ConfidenceIntervalLineCsv : BaseCsv
	{
		public override string ToCsv(List<Department> departments, Dictionary<string, List<Answer>> weeks)
		{
			StringBuilder content = new StringBuilder();
			StringBuilder header1 = new StringBuilder(",");
			StringBuilder header2 = new StringBuilder(",");
			foreach (var d in departments) {
				header1.Append(AddComma(d.Name));
				header1.Append(",");
				header1.Append(",");
				header2.Append(AddComma("Mean"));
				header2.Append(AddComma("Confidence Interval"));
				header2.Append(AddComma("Count"));
			}
			header1.AppendLine();
			header2.AppendLine();
			foreach (var w in weeks.Keys) {
				content.Append(AddComma(w));
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						var v = a.GetDoubleValues();
						content.Append(AddComma(v.Mean.ToString()));
						content.Append(AddComma(v.ConfidenceInterval.ToString()));
						content.Append(AddComma(a.Values.Count.ToString()));
					} else {
						content.Append(",,,");
					}
				}
				content.AppendLine();
			}
			header1.Append(header2);
			header1.Append(content);
			return header1.ToString();
		}
	}
	
	public class StandardDeviationLineCsv : BaseCsv
	{
		public override string ToCsv(List<Department> departments, Dictionary<string, List<Answer>> weeks)
		{
			StringBuilder content = new StringBuilder();
			StringBuilder header1 = new StringBuilder(",");
			StringBuilder header2 = new StringBuilder(",");
			foreach (var d in departments) {
				header1.Append(AddComma(d.Name));
				header1.Append(",");
				header1.Append(",");
				header2.Append(AddComma("Mean"));
				header2.Append(AddComma("Standard Deviation"));
				header2.Append(AddComma("Count"));
			}
			header1.AppendLine();
			header2.AppendLine();
			foreach (var w in weeks.Keys) {
				content.Append(AddComma(w));
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						var v = a.GetDoubleValues();
						content.Append(AddComma(v.Mean.ToString()));
						content.Append(AddComma(v.ConfidenceInterval.ToString()));
						content.Append(AddComma(a.Values.Count.ToString()));
					} else {
						content.Append(",,");
					}
				}
				content.AppendLine();
			}
			header1.Append(header2);
			header1.Append(content);
			return header1.ToString();
		}
	}
	
	public class BoxPlotCsv : BaseCsv
	{
		public override string ToCsv(List<Department> departments, Dictionary<string, List<Answer>> weeks)
		{
			StringBuilder content = new StringBuilder();
			StringBuilder header1 = new StringBuilder(",");
			StringBuilder header2 = new StringBuilder(",");
			foreach (var d in departments) {
				header1.Append(AddComma(d.Name));
				header1.Append(",");
				header2.Append(AddComma("Median"));
				header2.Append(AddComma("Count"));
			}
			header1.AppendLine();
			header2.AppendLine();
			foreach (var w in weeks.Keys) {
				content.Append(AddComma(w));
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						content.Append(AddComma(a.GetDoubleValues().Median.ToString()));
						content.Append(AddComma(a.Values.Count.ToString()));
					} else {
						content.Append(",,");
					}
				}
				content.AppendLine();
			}
			header1.Append(header2);
			header1.Append(content);
			return header1.ToString();
		}
	}
}
