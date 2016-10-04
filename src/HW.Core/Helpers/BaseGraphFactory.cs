using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Helpers
{
	public interface IGraphFactory
	{
//		ExtendedGraph CreateGraph(string key, ReportPart reportPart, int langID, int projectRoundUnitID, int yearFrom, int yearTo, int groupBy, bool hasGrouping, int plot, int width, int height, string bg, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo);
		ExtendedGraph CreateGraph(string key, ReportPart reportPart, int langID, int projectRoundUnitID, DateTime dateFrom, DateTime dateTo, int groupBy, bool hasGrouping, int plot, int width, int height, string bg, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose);

		void CreateGraphForExcelWriter(ReportPart reportPart, int langID, int projectRoundUnitID, int yearFrom, int yearTo, int groupBy, bool hasGrouping, int plot, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, ExcelWriter writer, ref int index, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo);
		
		event EventHandler<MergeEventArgs> ForMerge;
		
		event EventHandler<ExcelCellEventArgs> CellWrite;
	}
	
	public abstract class BaseGraphFactory : IGraphFactory
	{
		public static List<string> GetBottomStrings(int minDT, int maxDT, int groupBy)
		{
			int j = 0;
			var strings = new List<string>();
			for (int i = minDT; i <= maxDT; i++) {
				j++;
				string v = GroupStatsGraphFactory.GetBottomString(groupBy, i, j, "");
//				DrawBottomString(v, j);
				strings.Add(v);
			}
			return strings;
		}
		
		public static string GetBottomString(int groupBy, int i, int dx, string str)
		{
			switch (groupBy) {
				case 1:
					{
						int d = i;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = string.Format("v{0}, {1}{2}", w, d / 52, str);
						return v;
					}
				case 2:
					{
						int d = i * 2;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = string.Format("v{0}-{1}, {2}{3}", w - 1, w, (d - ((d - 1) % 52)) / 52, str);
						return v;
					}
				case 3:
					{
						int d = i;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
						return v;
					}
				case 4:
					{
						int d = i * 3;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = string.Format("Q{0}, {1}{2}", w / 3, (d - w) / 12, str);
						return v;
					}
				case 5:
					{
						int d = i * 6;
						int w = d % 12;
						if (w == 0) {
							w = 12;
						}
						string v = string.Format("{0}/{1}{2}", (d - w) / 12, w / 6, str);
						return v;
					}
				case 6:
					{
						string v = i.ToString() + str;
						return v;
					}
				case 7:
					{
						int d = i * 2;
						int w = d % 52;
						if (w == 0) {
							w = 52;
						}
						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
						return v;
					}
				default:
					throw new NotSupportedException();
			}
		}
		
		public event EventHandler<ExcelCellEventArgs> CellWrite;
		
		protected virtual void OnCellWrite(ExcelCellEventArgs e)
		{
			if (CellWrite != null) {
				CellWrite(this, e);
			}
		}
		
		public event EventHandler<MergeEventArgs> ForMerge;
		
		protected virtual void OnForMerge(MergeEventArgs e)
		{
			if (ForMerge != null) {
				ForMerge(this, e);
			}
		}
		
//		public abstract ExtendedGraph CreateGraph(string key, ReportPart reportPart, int langID, int projectRoundUnitID, int yearFrom, int yearTo, int groupBy, bool hasGrouping, int plot, int width, int height, string bg, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo);
		public abstract ExtendedGraph CreateGraph(string key, ReportPart reportPart, int langID, int projectRoundUnitID, DateTime dateFrom, DateTime dateTo, int groupBy, bool hasGrouping, int plot, int width, int height, string bg, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose);
		
		public abstract void CreateGraphForExcelWriter(ReportPart reportPart, int langID, int projectRoundUnitID, int yearFrom, int yearTo, int groupBy, bool hasGrouping, int plot, int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, ExcelWriter writer, ref int index, int sponsorMinUserCountToDisclose, int monthFrom, int monthTo);
	}
}
