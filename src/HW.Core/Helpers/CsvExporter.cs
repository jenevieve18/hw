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
	
	public class CsvExporter : IExporter
	{
		bool hasAnswerKey;
		bool hasGrouping;
		object disabled;
		int width;
		int height;
		string background;
		ReportPart r;
		string key;
		ReportService service;
		IList<ReportPartLanguage> parts;
		
		public CsvExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key)
		{
			this.service = service;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.disabled = disabled;
			this.width = width;
			this.height = height;
			this.background = background;
			this.r = r;
			this.key = key;
		}
		
		public CsvExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, IList<ReportPartLanguage> parts, string key)
		{
			this.service = service;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.disabled = disabled;
			this.width = width;
			this.height = height;
			this.background = background;
			this.parts = parts;
			this.key = key;
		}
		
		public string Type {
			get { return "text/csv"; }
		}
		
		public bool HasContentDisposition {
			get { return ContentDisposition.Length > 0; }
		}
		
		public string ContentDisposition {
			get { return "attachment;filename=Report.csv"; }
		}
		
//		public object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path, int distribution)
		public object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path)
		{
			var f = service.GetGraphFactory(hasAnswerKey);
//			return f.CreateGraph2(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled, distribution);
			return f.CreateGraph2(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled);
		}
		
//		public object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path, int distribution)
		public object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, string plot, string path)
		{
			StringBuilder s = new StringBuilder();
			foreach (var p in parts) {
				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
				var f = service.GetGraphFactory(hasAnswerKey);
//				string x = f.CreateGraph2(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled, distribution);
				string x = f.CreateGraph2(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled);
				s.AppendLine(x);
				s.AppendLine();
			}
			return s.ToString();
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
				foreach (var x in weeks[w]) {
					if (x.Values.Count > 0) {
						content.Append(AddComma(x.GetIntValues().Mean.ToString()));
						content.Append(AddComma(x.Values.Count.ToString()));
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
				foreach (var x in weeks[w]) {
					if (x.Values.Count > 0) {
						var v = x.GetIntValues();
						content.Append(AddComma(v.Mean.ToString()));
						content.Append(AddComma(v.ConfidenceInterval.ToString()));
						content.Append(AddComma(x.Values.Count.ToString()));
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
				foreach (var x in weeks[w]) {
					if (x.Values.Count > 0) {
						var v = x.GetIntValues();
						content.Append(AddComma(v.Mean.ToString()));
						content.Append(AddComma(v.ConfidenceInterval.ToString()));
						content.Append(AddComma(x.Values.Count.ToString()));
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
				foreach (var x in weeks[w]) {
					if (x.Values.Count > 0) {
						content.Append(AddComma(x.GetIntValues().Median.ToString()));
						content.Append(AddComma(x.Values.Count.ToString()));
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
