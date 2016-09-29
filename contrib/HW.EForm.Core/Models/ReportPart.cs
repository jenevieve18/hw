using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ReportPart
	{
		public ReportPart()
		{
			Components = new List<ReportPartComponent>();
		}
		
		public int ReportPartID { get; set; }
		public int ReportID { get; set; }
		public Report Report { get; set; }
		public string Internal { get; set; }
		public int Type { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int RequiredAnswerCount { get; set; }
		public int SortOrder { get; set; }
		public int PartLevel { get; set; }
		public int GroupingQuestionID { get; set; }
		public int GroupingOptionID { get; set; }
		public Question Question { get; set; }
		
		public bool HasComponents {
			get { return Components != null && Components.Count > 0; }
		}
		
		public bool HasQuestion {
			get { return Question != null; }
		}
		
		public IList<ReportPartComponent> Components { get; set; }
		
		public void AddComponent(ReportPartComponent component)
		{
			component.ReportPart = this;
			Components.Add(component);
		}
	}
}
