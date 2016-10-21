using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using HW.Core.Util.Graphs;
using OfficeOpenXml.Style;

namespace HW.Core.Util.Exporters
{
	public interface IExcelWriter
	{
		void WriteCell(int row, int col, object value, Color bgColor, ExcelBorderStyle border);
		
		void WriteCell(int row, int col, object value, Color fgColor, Color bgColor, int size, ExcelBorderStyle border, bool merge, int mergeCount);
		
		void WriteCell(int row, int col, object value, ExcelBorderStyle border);
		
		void EndWrite();
	}
	
	public class ExcelStatsExporter : AbstractExporter
	{
		bool hasAnswerKey;
		bool hasGrouping;
		ReportPart r;
		ReportService service;
		IList<IReportPart> parts;
		
		public ExcelStatsExporter(ReportPart r)
		{
			this.r = r;
		}
		
		public ExcelStatsExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, ReportPart r)
		{
			this.service = service;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.r = r;
		}
		
		public ExcelStatsExporter(ReportService service, bool hasAnswerKey, bool hasGrouping, IList<IReportPart> parts)
		{
			this.service = service;
			this.hasAnswerKey = hasAnswerKey;
			this.hasGrouping = hasGrouping;
			this.parts = parts;
		}
		
		public override string Type {
			get { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
		}
		
		public override string GetContentDisposition(string file)
		{
			return string.Format("attachment;filename=\"HealthWatch {0} {1}.xlsx\";", file, DateTime.Now.ToString("yyyyMMdd"));
		}
		
		public override string ContentDisposition2 {
			get { return string.Format("attachment;filename=\"HealthWatch Survey {0}.xlsx\";", DateTime.Now.ToString("yyyyMMdd")); }
		}
		
		public override object Export(string url, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			MemoryStream output = new MemoryStream();
			var f = service.GetGraphFactory(hasAnswerKey);
			ExcelWriter w = new ExcelWriter(output);
			int i = 0;
			w.WriteCell(i, 0, r.SelectedReportPartLang.Subject, Color.Black, Color.AliceBlue, 16, ExcelBorderStyle.Thin);
			int j = i;
			f.ForMerge += delegate(object sender, MergeEventArgs e) {
				w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
			};
			i++;
			f.CellWrite += delegate(object sender, ExcelCellEventArgs e) { OnCellWrite(e); };
			
			f.CreateGraphForExcelWriter(r, langID, projectRoundUnit, dateFrom, dateTo, groupBy, hasGrouping, plot, grouping, sponsorAdmin, sponsor, departmentIDs, w, ref i);
			
			w.EndWrite();
			return output;
		}
		
		public override object ExportAll(int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int groupBy, int plot, int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs)
		{
			MemoryStream output = new MemoryStream();
			ExcelWriter w = new ExcelWriter(output);
			int i = 0;
			foreach (var p in parts) {
				if (p is SponsorProject) {
					var f = new ForStepCount(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository(), new SqlMeasureRepository());
					w.WriteCell(new ExcelCell { Row = i, Column = 0, Value = p.Subject, BackgroundColor = Color.AliceBlue, FontSize = 16, BorderStyle = ExcelBorderStyle.Thin});
					int j = i;
					f.ForMerge += delegate(object sender, MergeEventArgs e) {
						w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
					};
					i++;
					
					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
					f.CreateGraphForExcelWriter(r, langID, projectRoundUnit, dateFrom, dateTo, groupBy, hasGrouping, plot, grouping, sponsorAdmin, sponsor, departmentIDs, w, ref i);
				} else {
					var f = service.GetGraphFactory(hasAnswerKey);
					w.WriteCell(new ExcelCell { Row = i, Column = 0, Value = p.Subject, BackgroundColor = Color.AliceBlue, FontSize = 16, BorderStyle = ExcelBorderStyle.Thin});
					int j = i;
					f.ForMerge += delegate(object sender, MergeEventArgs e) {
						w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
					};
					i++;
					f.CellWrite += delegate(object sender, ExcelCellEventArgs e) { OnCellWrite(e); };
					
					ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
					f.CreateGraphForExcelWriter(r, langID, projectRoundUnit, dateFrom, dateTo, groupBy, hasGrouping, plot, grouping, sponsorAdmin, sponsor, departmentIDs, w, ref i);
				}
			}
			w.EndWrite();
			return output;
		}
		
		public override object SuperExport(string url, string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, int rpid, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			MemoryStream output = new MemoryStream();
			var f = new GroupStatsGraphFactory(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository());
			ExcelWriter w = new ExcelWriter(output);
			int i = 0;
			w.WriteCell(i, 0, r.SelectedReportPartLang.Subject, Color.Black, Color.AliceBlue, 16, ExcelBorderStyle.Thin);
			int j = i;
			f.ForMerge += delegate(object sender, MergeEventArgs e) {
				w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
			};
			f.CellWrite += delegate(object sender, ExcelCellEventArgs e) { OnCellWrite(e); };
			i++;
			f.CreateSuperGraphForExcelWriter(rnds1, rnds2, rndsd1, rndsd2, pid1, pid2, n, rpid, yearFrom, yearTo, r1, r2, langID, ref i, plot, w);
			w.EndWrite();
			return output;
		}
		
		public override object SuperExportAll(string rnds1, string rnds2, string rndsd1, string rndsd2, string pid1, string pid2, string n, string yearFrom, string yearTo, string r1, string r2, int langID, int plot)
		{
			MemoryStream output = new MemoryStream();
			ExcelWriter w = new ExcelWriter(output);
			int i = 0;
			foreach (var p in parts) {
				ReportPart r = service.ReadReportPart(p.ReportPart.Id, langID);
				var f = new GroupStatsGraphFactory(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository());
				w.WriteCell(new ExcelCell { Row = i, Column = 0, Value = r.SelectedReportPartLang.Subject, BackgroundColor = Color.AliceBlue, FontSize = 16, BorderStyle = ExcelBorderStyle.Thin});
				int j = i;
				f.ForMerge += delegate(object sender, MergeEventArgs e) {
					w.Merge(j, 0, j, e.WeeksCount, ExcelBorderStyle.Thin);
				};
				f.CellWrite += delegate(object sender, ExcelCellEventArgs e) { OnCellWrite(e); };
				i++;
				f.CreateSuperGraphForExcelWriter(rnds1, rnds2, rndsd1, rndsd2, pid1, pid2, n, r.Id, yearFrom, yearTo, r1, r2, langID, ref i, plot, w);
			}
			w.EndWrite();
			return output;
		}
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
		
		public event EventHandler<ExcelCellEventArgs> CellWrite;
		
		protected virtual void OnCellWrite(ExcelCellEventArgs e)
		{
			if (CellWrite != null) {
				CellWrite(this, e);
			}
		}
		
		public virtual void ToExcel(List<IDepartment> departments, Dictionary<string, List<IAnswer>> weeks, ExcelWriter writer, ref int i)
		{
		}
	}
	
	public class ExcelCellEventArgs : EventArgs
	{
		public ExcelCell ExcelCell { get; set; }
		public string ValueKey { get; set; }
		public ExcelWriter Writer { get; set; }
	}
	
	public class LineExcel : AbstractExcel
	{
		public override void ToExcel(List<IDepartment> departments, Dictionary<string, List<IAnswer>> weeks, ExcelWriter writer, ref int i)
		{
			OnCellWrite(new ExcelCellEventArgs { ValueKey = "timeframe", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center, AutoFit = true });
				j++;
			}
			i++;
			j = 1;
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "department", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			foreach (var w in weeks.Keys) {
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "mean", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = j, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center }});
				j++;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
//			j = 1;
//			i = k;
//			foreach (var w in weeks.Keys) {
//				foreach (var a in weeks[w]) {
//					if (a.Values.Count > 0) {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.GetDoubleValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//					} else {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
//					}
//					i++;
//				}
//				j++;
//				i = k;
//			}
//			j = 1;
			i = k;
			foreach (var d in departments) {
				j = 1;
				foreach (var a in d.Answers) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.GetDoubleValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
					}
//					i++;
					j++;
				}
//				j++;
//				i = k;
				i++;
			}
//			i += departments.Count + 2;
			i += 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count });
		}
	}
	
	public class ConfidenceIntervalLineExcel : AbstractExcel
	{
		public override void ToExcel(List<IDepartment> departments, Dictionary<string, List<IAnswer>> weeks, ExcelWriter writer, ref int i)
		{
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "timeframe", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin}});
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.Merge(i, j, i, j + 2, ExcelBorderStyle.Thin);
				j += 3;
			}
			i++;
			j = 1;
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "department", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "mean", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = j + 1, Value = "Mean", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center }});
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = "± 1.96 SD", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center, AutoFit = true });
				j += 3;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
//			j = 1;
//			i = k;
//			foreach (var w in weeks.Keys) {
//				foreach (var a in weeks[w]) {
//					if (a.Values.Count > 0) {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetDoubleValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = a.GetDoubleValues().ConfidenceInterval, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//					} else {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1 });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2 });
//					}
//					i++;
//				}
//				j += 3;
//				i = k;
//			}
//			j = 1;
			i = k;
			foreach (var d in departments) {
				j = 1;
				foreach (var a in d.Answers) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetDoubleValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = a.GetDoubleValues().ConfidenceInterval, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1 });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2 });
					}
//					i++;
					j += 3;
				}
//				j += 3;
//				i = k;
				i++;
			}
//			i += departments.Count + 2;
			i += 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count * 3 });
		}
	}
	
	public class StandardDeviationLineExcel : AbstractExcel
	{
		public override void ToExcel(List<IDepartment> departments, Dictionary<string, List<IAnswer>> weeks, ExcelWriter writer, ref int i)
		{
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "timeframe", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin}});
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.Merge(i, j, i, j + 2, ExcelBorderStyle.Thin);
				j += 3;
			}
			i++;
			j = 1;
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "department", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "mean", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = j + 1, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center }});
				writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = "SD", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				j += 3;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
//			j = 1;
//			i = k;
//			foreach (var w in weeks.Keys) {
//				foreach (var a in weeks[w]) {
//					if (a.Values.Count > 0) {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetDoubleValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = a.GetDoubleValues().StandardDeviation, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//					} else {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, BorderStyle = ExcelBorderStyle.Thin });
//					}
//					i++;
//				}
//				j += 3;
//				i = k;
//			}
//			j = 1;
			i = k;
			foreach (var d in departments) {
				j = 1;
				foreach (var a in d.Answers) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetDoubleValues().Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, Value = a.GetDoubleValues().StandardDeviation, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 2, BorderStyle = ExcelBorderStyle.Thin });
					}
//					i++;
					j += 3;
				}
//				j += 3;
//				i = k;
				i++;
			}
//			i += departments.Count + 2;
			i += 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count * 3 });
		}
	}
	
	public class BoxPlotExcel : AbstractExcel
	{
		public override void ToExcel(List<IDepartment> departments, Dictionary<string, List<IAnswer>> weeks, ExcelWriter writer, ref int i)
		{
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "timeframe", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			int j = 1;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				writer.Merge(i, j, i, j + 1, ExcelBorderStyle.Thin);
				j += 2;
			}
			i++;
			j = 1;
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "department", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "median", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = j + 1, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center }});
				j += 2;
			}
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				i++;
			}
//			j = 1;
//			i = k;
//			foreach (var w in weeks.Keys) {
//				foreach (var a in weeks[w]) {
//					if (a.Values.Count > 0) {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetDoubleValues().Median, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//					} else {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, BorderStyle = ExcelBorderStyle.Thin });
//					}
//					i++;
//				}
//				j += 2;
//				i = k;
//			}
//			j = 1;
			i = k;
			foreach (var d in departments) {
				j = 1;
				foreach (var a in d.Answers) {
					if (a.Values.Count > 0) {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, Value = a.GetDoubleValues().Median, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
					} else {
						writer.WriteCell(new ExcelCell { Row = i, Column = j, BorderStyle = ExcelBorderStyle.Thin });
						writer.WriteCell(new ExcelCell { Row = i, Column = j + 1, BorderStyle = ExcelBorderStyle.Thin });
					}
//					i++;
					j += 2;
				}
//				j += 2;
//				i = k;
				i++;
			}
//			i += departments.Count + 2;
			i += 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count * 2 });
		}
	}
	
	public class EverythingExcel : AbstractExcel
	{
		public override void ToExcel(List<IDepartment> departments, Dictionary<string, List<IAnswer>> weeks, ExcelWriter writer, ref int i)
		{
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "timeframe", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Right }});
			writer.Merge(i, 0, i, 1, ExcelBorderStyle.Thin);
			int j = 2;
			foreach (var w in weeks.Keys) {
				writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = w, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center, AutoFit = true, VerticalAlignment = ExcelVerticalAlignment.Center });
				writer.Merge(i, j, i + 1, j, ExcelBorderStyle.Thin);
				j++;
			}
			i++;
			j = 1;
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "department", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 0, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			OnCellWrite(new ExcelCellEventArgs{ ValueKey = "variable", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 1, ForegroundColor = Color.White, BackgroundColor = Color.SteelBlue, BorderStyle = ExcelBorderStyle.Thin }});
			i++;
			int k = i;
			foreach (var d in departments) {
				writer.WriteCell(new ExcelCell { Row = i, Column = 0, Value = d.Name, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "mean", Writer = writer, ExcelCell = new ExcelCell { Row = i, Column = 1, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true }});
				
				writer.WriteCell(new ExcelCell { Row = i + 1, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 1, Column = 1, Value = "±SD", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 2, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 2, Column = 1, Value = "±SD*1.96", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 3, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "quartile.lower", Writer = writer, ExcelCell = new ExcelCell { Row = i + 3, Column = 1, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin, AutoFit = true }});
				
				writer.WriteCell(new ExcelCell { Row = i + 4, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "median.q2", Writer = writer, ExcelCell = new ExcelCell { Row = i + 4, Column = 1, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin }});
				
				writer.WriteCell(new ExcelCell { Row = i + 5, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				OnCellWrite(new ExcelCellEventArgs{ ValueKey = "quartile.upper", Writer = writer, ExcelCell = new ExcelCell { Row = i + 5, Column = 1, BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin }});
				
				writer.WriteCell(new ExcelCell { Row = i + 6, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 6, Column = 1, Value = "n", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 7, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 7, Column = 1, Value = "Min", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				writer.WriteCell(new ExcelCell { Row = i + 8, Column = 0, Value = "", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				writer.WriteCell(new ExcelCell { Row = i + 8, Column = 1, Value = "Max", BackgroundColor = Color.SkyBlue, BorderStyle = ExcelBorderStyle.Thin });
				
				i += 9;
			}
//			j = 2;
			i = k;
//			foreach (var w in weeks.Keys) {
//				foreach (var a in weeks[w]) {
//					if (a.Values.Count > 0) {
//						var v = a.GetDoubleValues();
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = v.Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 1, Column = j, Value = v.StandardDeviation, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 2, Column = j, Value = v.ConfidenceInterval, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 3, Column = j, Value = v.LowerBox, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 4, Column = j, Value = v.Median, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 5, Column = j, Value = v.UpperBox, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 6, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 7, Column = j, Value = v.LowerWhisker, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//						writer.WriteCell(new ExcelCell { Row = i + 8, Column = j, Value = v.UpperWhisker, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
//					} else {
//						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 1, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 2, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 3, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 4, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 5, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 6, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 7, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//						writer.WriteCell(new ExcelCell { Row = i + 8, Column = j, Value = "", BorderStyle = ExcelBorderStyle.Thin });
//					}
//					i += 9;
//				}
//				j += 1;
//				i = k;
//			}
			foreach (var d in departments) {
				j = 2;
				foreach (var a in d.Answers) {
					if (a.Values.Count > 0) {
						var v = a.GetDoubleValues();
						writer.WriteCell(new ExcelCell { Row = i, Column = j, Value = v.Mean, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 1, Column = j, Value = v.StandardDeviation, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 2, Column = j, Value = v.ConfidenceInterval, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 3, Column = j, Value = v.LowerBox, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 4, Column = j, Value = v.Median, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 5, Column = j, Value = v.UpperBox, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
						writer.WriteCell(new ExcelCell { Row = i + 6, Column = j, Value = a.Values.Count, BorderStyle = ExcelBorderStyle.Thin, HorizontalAlignment = ExcelHorizontalAlignment.Center });
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
					j += 1;
				}
				i += 9;
			}
//			i += (departments.Count * 9) + 2;
			i += 2;
			OnForMerge(new MergeEventArgs { WeeksCount = weeks.Count + 1 });
		}
	}
}
