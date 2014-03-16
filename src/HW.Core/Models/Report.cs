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
		public virtual string Internal { get; set; }
		public virtual Guid ReportKey { get; set; }
		public virtual IList<ReportPart> Parts { get; set; }
		public virtual IList<ReportLanguage> Languages { get; set; }
		public virtual IList<ProjectRound> ProjectRounds { get; set; }
	}
	
	public class ReportLanguage : BaseModel
	{
		public virtual Report Report { get; set; }
		public virtual Language Language { get; set; }
		public virtual Option Option { get; set; }
		public virtual Question Question { get; set; }
		public virtual string Feedback { get; set; }
	}
	
	public class ReportPart : BaseModel
	{
		public virtual Report Report { get; set; }
		public virtual string Internal { get; set; }
		public virtual int Type { get; set; }
		public virtual int RequiredAnswerCount { get; set; }
		public virtual int PartLevel { get; set; }
		public virtual Question Question { get; set; }
		public virtual Option Option { get; set; }
		public virtual List<ReportPartComponent> Components { get; set; }
		public virtual IList<ReportPartLanguage> Languages { get; set; }
		
		public virtual ReportPartLanguage CurrentLanguage { get; set; }
	}
	
	public class ReportPartComponent : BaseModel
	{
		public virtual ReportPart ReportPart { get; set; }
		public virtual Index Index { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual WeightedQuestionOption QuestionOption { get; set; }
	}
	
	public class ReportPartLanguage : BaseModel
	{
		public virtual ReportPart ReportPart { get; set; }
		public virtual Language Language { get; set; }
		public virtual string Subject { get; set; }
		public virtual string Header { get; set; }
		public virtual string Footer { get; set; }
	}
}
