//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Report : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public enum ReportType
	{
		One = 1,
		Two = 2,
		Three = 3,
		Eight = 8,
		Nine = 9
	}
	
	public class ReportLanguage : BaseModel
	{
		public Language Language { get; set; }
		public Option Option { get; set; }
		public Question Question { get; set; }
	}
	
	public class ReportPart : BaseModel
	{
		public Report Report { get; set; }
		public int Type { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int PartLevel { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public IList<ReportPartComponent> Components { get; set; }
	}
	
	public class ReportPartComponent : BaseModel
	{
		public ReportPart ReportPart { get; set; }
		public Index Index { get; set; }
		public int SortOrder { get; set; }
		public WeightedQuestionOption QuestionOption { get; set; }
	}
	
	public class ReportPartLanguage : BaseModel
	{
		public ReportPart ReportPart { get; set; }
		public Language Language { get; set; }
		public string Subject { get; set; }
		public string Header { get; set; }
		public string Footer { get; set; }
	}
}
