﻿using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public enum ReportType
	{
		One = 1,
		Index = 2,
		Three = 3,
		Eight = 8,
		Nine = 9
	}
	
	public class Report : BaseModel
	{
		public Report()
		{
			Parts = new List<ReportPart>();
		}
		
		public int ReportID { get; set; }
		public string Internal { get; set; }
		public Guid ReportKey { get; set; }
		
		public IList<ReportPart> Parts { get; set; }
		public IList<ReportLanguage> Languages { get; set; }
		public IList<ProjectRound> ProjectRounds { get; set; }
		
		public void AddPart(ReportPart p)
		{
			p.Report = this;
			Parts.Add(p);
		}
	}
	
	public class ReportLanguage : BaseModel
	{
		public int ReportID { get; set; }
		public int LangID { get; set; }
		public string Feedback { get; set; }
		public string FeedbackJapaneseUnicode { get; set; }
		
		public Report Report { get; set; }
		public Language Language { get; set; }
		public Option Option { get; set; }
		public Question Question { get; set; }
	}
	
	public class ReportPart : BaseModel
	{
		ReportPartLang currentLanguage;
		
		public ReportPart()
		{
			Languages = new List<ReportPartLang>();
			Components = new List<ReportPartComponent>();
		}
		
		public int ReportPartID { get; set; }
		public int ReportID { get; set; }
		public string Internal { get; set; }
		public int Type { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int SortOrder { get; set; }
		public int PartLevel { get; set; }
		public int GroupingQuestionID { get; set; }
		public int GroupingOptionID { get; set; }
		public Report Report { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public List<ReportPartComponent> Components { get; set; }
		public IList<ReportPartLang> Languages { get; set; }
		public int SelectedReportPartLangID { get; set; }
		
		public bool HasQuestion {
			get { return Question != null; }
		}
		
		public ReportPartComponent FirstComponent {
			get {
				if (Components.Count > 0) {
					return Components[0];
				}
				return null;
			}
		}
		
		public bool HasComponents {
			get { return Components.Count > 0; }
		}
		
		public ReportPartLang CurrentLanguage {
			get { return currentLanguage; }
			set {
				currentLanguage = value;
				currentLanguage.ReportPart = this;
			}
		}
		
		public ReportPartLang FirstLanguage {
			get {
				if (Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		ReportPartLang selectedReportPartLang;
		
		public ReportPartLang SelectedReportPartLang {
			get {
				if (selectedReportPartLang == null) {
					selectedReportPartLang = GetLanguage(SelectedReportPartLangID);
				}
				if (selectedReportPartLang == null && FirstLanguage != null) {
					selectedReportPartLang = FirstLanguage;
				}
				return selectedReportPartLang;
			}
			set { selectedReportPartLang = value; }
		}
		
		ReportPartLang GetLanguage(int langID)
		{
			foreach (var l in Languages) {
				if (l.LangID == langID) {
					return l;
				}
			}
			return null;
		}
	}
	
	public class ReportPartEventArgs : EventArgs
	{
		public string Url { get; set; }
		public ReportPart ReportPart { get; set; }
		
		public ReportPartEventArgs(ReportPart r)
		{
			this.ReportPart = r;
		}
	}
	
	public class ReportPartComponent : BaseModel
	{
		public int ReportPartComponentID { get; set; }
		public int ReportPartID { get; set; }
		public int IdxID { get; set; }
		public int WeightedQuestionOptionID { get; set; }
		public int SortOrder { get; set; }
		public ReportPart ReportPart { get; set; }
		public Index Index { get; set; }
		public WeightedQuestionOption WeightedQuestionOption { get; set; }
		
		public bool HasIndex {
			get { return Index != null; }
		}
		
		public bool HasWeightedQuestionOption {
			get { return WeightedQuestionOption != null; }
		}
	}
	
	public class ReportPartLang : BaseModel, IReportPart
	{
		public int ReportPartLangID { get; set; }
		public int ReportPartID { get; set; }
		public int LangID { get; set; }
		public string Subject { get; set; }
		public string Header { get; set; }
		public string Footer { get; set; }
		public string AltText { get; set; }
		public string SubjectJapaneseUnicode { get; set; }
		public string HeaderJapaneseUnicode { get; set; }
		public string FooterJapaneseUnicode { get; set; }
		public string AltTextJapaneseUnicode { get; set; }
		public ReportPart ReportPart { get; set; }
		public Language Language { get; set; }
	}
	
	public interface IReportPart : IBaseModel
	{
		string Subject { get; set; }
		string Header { get; set; }
		ReportPart ReportPart { get; set; }
	}
}
