using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Services;
using OfficeOpenXml.Style;

namespace HW.Core.Helpers
{
	public class ExcelExporter : AbstractExporter
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
		
		public ExcelExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, ReportPart r, string key)
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
		
		public ExcelExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, object disabled, int width, int height, string background, IList<ReportPartLanguage> parts, string key)
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
		
		public override string Type {
			get { return "application/octet-stream"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=HealthWatch {0} {1}.xlsx;", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=HealthWatch Survey {0}.xlsx;", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override object Export(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose)
		{
			MemoryStream output = new MemoryStream();
			var f = service.GetGraphFactory(hasAnswerKey);
			ExcelWriter w = new ExcelWriter(output);
			int i = 0;
			w.WriteCell(i, 0, r.CurrentLanguage.Subject, Color.Black, Color.AliceBlue, 16, ExcelBorderStyle.Thin);
			int j = i;
			f.ForMerge += delegate(object sender, MergeEventArgs e) {
				w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
			};
			i++;
			f.CreateGraph3(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled, w, ref i, sponsorMinUserCountToDisclose);
			w.EndWrite();
			return output;
		}
		
		public override object Export2(int gb, int fy, int ty, int langID, int pruid, int grpng, int spons, int sid, string gid, int plot, string path, int sponsorMinUserCountToDisclose)
		{
			MemoryStream output = new MemoryStream();
			ExcelWriter w = new ExcelWriter(output);
			int i = 0;
			foreach (var p in parts) {
				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
				var f = service.GetGraphFactory(hasAnswerKey);
				w.WriteCell(new ExcelCell { Row = i, Column = 0, Value = r.CurrentLanguage.Subject, BackgroundColor = Color.AliceBlue, FontSize = 16, BorderStyle = ExcelBorderStyle.Thin});
				int j = i;
				f.ForMerge += delegate(object sender, MergeEventArgs e) {
					w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
				};
				i++;
				f.CreateGraph3(key, r, langID, pruid, fy, ty, gb, hasGrouping, plot, grpng, spons, sid, gid, disabled, w, ref i, sponsorMinUserCountToDisclose);
				
			}
			w.EndWrite();
			return output;
		}
	}
	
	public interface IExcelWriter
	{
		void WriteCell(int row, int col, object value, Color bgColor, ExcelBorderStyle border);
		
		void WriteCell(int row, int col, object value, Color fgColor, Color bgColor, int size, ExcelBorderStyle border, bool merge, int mergeCount);
		
		void WriteCell(int row, int col, object value, ExcelBorderStyle border);
		
		void EndWrite();
	}
	
	public class AbstractExcel
	{
		public event EventHandler<MergeEventArgs> ForMerge;
		
		protected virtual void OnForMerge(MergeEventArgs e)
		{
			if (ForMerge != null) {
				ForMerge(this, e);
			}
		}
	}
	
	public class LineExcel : AbstractExcel
	{
		public void ToExcel(List<Department> departments, Dictionary<string, List<Answer>> weeks, ExcelWriter writer, ref int i)
		{
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Timeframe", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center, AutoFit = true });
				j++;
			}
			i++;
			j = 1;
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Department", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "Mean", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				j++;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
			j = 1;
			i = k;
			foreach (var w in weeks.Keys) {
				foreach (var x in weeks[w]) {
					if (x.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = x.GetIntValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
					}
					i++;
				}
				j++;
				i = k;
			}
			i += departments.Count + 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count });
		}
	}
	
	public class ConfidenceIntervalLineExcel : AbstractExcel
	{
		public void ToExcel(List<Department> departments, Dictionary<string, List<Answer>> weeks, ExcelWriter writer, ref int i)
		{
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Timeframe", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin});
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.Merge(i, j, i, j + 2, ExcelBorderStyle.Thin);
				j += 3;
			}
			i++;
			j = 1;
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Department", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = "Mean", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = "± 1.96 SD", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center, AutoFit = true });
				j += 3;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
			j = 1;
			i = k;
			foreach (var w in weeks.Keys) {
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetIntValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = a.GetIntValues().ConfidenceInterval, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1 });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2 });
					}
					i++;
				}
				j += 3;
				i = k;
			}
			i += departments.Count + 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count * 3 });
		}
	}
	
	public class StandardDeviationLineExcel : AbstractExcel
	{
		public void ToExcel(List<Department> departments, Dictionary<string, List<Answer>> weeks, ExcelWriter writer, ref int i)
		{
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Timeframe", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin});
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.Merge(i, j, i, j + 2, ExcelBorderStyle.Thin);
				j += 3;
			}
			i++;
			j = 1;
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Department", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = "Mean", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = "SD", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				j += 3;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
			j = 1;
			i = k;
			foreach (var w in weeks.Keys) {
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetIntValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = a.GetIntValues().StandardDeviation, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, BorderStyle = ExcelBorderStyle.Thin });
					}
					i++;
				}
				j += 3;
				i = k;
			}
			i += departments.Count + 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count * 3 });
		}
	}
	
	public class BoxPlotExcel : AbstractExcel
	{
		public void ToExcel(List<Department> departments, Dictionary<string, List<Answer>> weeks, ExcelWriter writer, ref int i)
		{
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Timeframe", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.Merge(i, j, i, j + 1, ExcelBorderStyle.Thin);
				j += 2;
			}
			i++;
			j = 1;
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Department", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = "Median", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				j += 2;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
			j = 1;
			i = k;
			foreach (var w in weeks.Keys) {
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetIntValues().Median, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, BorderStyle = ExcelBorderStyle.Thin });
					}
					i++;
				}
				j += 2;
				i = k;
			}
			i += departments.Count + 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count * 2 });
		}
	}
	
	public class EverythingExcel : AbstractExcel
	{
		public void ToExcel(List<Department> departments, Dictionary<string, List<Answer>> weeks, ExcelWriter writer, ref int i)
		{
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Timeframe", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Right });
			writer.Merge(i, 0, i, 1, ExcelBorderStyle.Thin);
			int j = 2;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center, AutoFit = true, VerticalAlignment = ExcelVerticalAlignment.Center });
				writer.Merge(i, j, i + 1, j, ExcelBorderStyle.Thin);
				j++;
			}
			i++;
			j = 1;
			writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = "Department", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			writer.WriteCell(new ExcelCell { Row = i, Column = 1, Value = "Variable", ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin });
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				writer.WriteCell(new ExcelCell { Row = i, Column = 1, Value = "Mean", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				
				writer.WriteCell(new ExcelCell { Row = i + 1, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 1, Column = 1, Value = "±SD", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 2, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 2, Column = 1, Value = "±SD*1.96", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 3, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 3, Column = 1, Value = "Lower quartile (Q1)", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				
				writer.WriteCell(new ExcelCell { Row = i + 4, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 4, Column = 1, Value = "Median (Q2)", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 5, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 5, Column = 1, Value = "Upper quartile (Q3)", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 6, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 6, Column = 1, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 7, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 7, Column = 1, Value = "Min", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 8, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 8, Column = 1, Value = "Max", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				i += 9;
			}
			j = 2;
			i = k;
			foreach (var w in weeks.Keys) {
				foreach (var a in weeks[w]) {
					if (a.Values.Count > 0) {
						var v = a.GetIntValues();
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = v.Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 1, Column = j, Value = v.StandardDeviation, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 2, Column = j, Value = v.ConfidenceInterval, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 3, Column = j, Value = v.LowerBox, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 4, Column = j, Value = v.Median, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 5, Column = j, Value = v.UpperBox, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 6, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 7, Column = j, Value = a.Min, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 8, Column = j, Value = a.Max, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 7, Column = j, Value = v.LowerWhisker, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 8, Column = j, Value = v.UpperWhisker, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 1, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 2, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 3, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 4, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 5, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 6, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 7, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i + 8, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
					}
					i += 9;
				}
				j += 1;
				i = k;
			}
			i += (departments.Count * 9) + 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count + 1 });
		}
	}
}
