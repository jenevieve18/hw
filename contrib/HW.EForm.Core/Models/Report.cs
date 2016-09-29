using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Report
	{
		ReportLang selectedReportLang;
		
		public Report()
		{
			Parts = new List<ReportPart>();
			Languages = new List<ReportLang>();
		}
		
		public int ReportID { get; set; }
		public string Internal { get; set; }
		public Guid ReportKey { get; set; }

		public IList<ReportPart> Parts { get; set; }
		public IList<ReportLang> Languages { get; set; }
		public int SelectedQuestionLangID { get; set; }
		
		public void AddPart(ReportPart part)
		{
			part.Report = this;
			Parts.Add(part);
		}
		
		public ReportLang FirstLanguage {
			get {
				if (Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		public ReportLang SelectedReportLang {
			get {
				if (selectedReportLang == null) {
					selectedReportLang = GetLanguage(SelectedQuestionLangID);
				}
				if (selectedReportLang == null && FirstLanguage != null) {
					selectedReportLang = FirstLanguage;
				}
				return selectedReportLang;
			}
			set { selectedReportLang = value; }
		}
		
		public ReportLang GetLanguage(int langID)
		{
			foreach (var l in Languages) {
				if (l.LangID == langID) {
					return l;
				}
			}
			return null;
		}
	}
}
