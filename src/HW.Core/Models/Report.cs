using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public enum ReportType
	{
		One = 1,
		Two = 2,
		Three = 3,
		Eight = 8,
		Nine = 9
	}
	
	public class Report : BaseModel
	{
		public string Internal { get; set; }
		public Guid ReportKey { get; set; }
		public IList<ReportPart> Parts { get; set; }
		public IList<ReportLanguage> Languages { get; set; }
		public IList<ProjectRound> ProjectRounds { get; set; }
	}
	
	public class ReportLanguage : BaseModel
	{
		public Report Report { get; set; }
		public Language Language { get; set; }
		public Option Option { get; set; }
		public Question Question { get; set; }
		public string Feedback { get; set; }
	}
	
	public class ReportPart : BaseModel
	{
		public Report Report { get; set; }
		public string Internal { get; set; }
		public int Type { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int PartLevel { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public List<ReportPartComponent> Components { get; set; }
		public IList<ReportPartLanguage> Languages { get; set; }
		
		public ReportPartLanguage CurrentLanguage { get; set; }
	}
	
	public class ReportPartComponent : BaseModel
	{
		public ReportPart ReportPart { get; set; }
		public Index Index { get; set; }
		public int SortOrder { get; set; }
		public WeightedQuestionOption WeightedQuestionOption { get; set; }
	}
	
	public class ReportPartLanguage : BaseModel, IReportPart
	{
		public ReportPart ReportPart { get; set; }
		public Language Language { get; set; }
		public string Subject { get; set; }
		public string Header { get; set; }
		public string Footer { get; set; }
	}
	
	public interface IReportPart : IBaseModel
	{
		string Subject { get; set; }
		string Header { get; set; }
		
		ReportPart ReportPart { get; set; }
	}
}
