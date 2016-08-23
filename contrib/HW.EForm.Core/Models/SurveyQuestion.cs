using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class SurveyQuestion
	{
		public int SurveyQuestionID { get; set; }
		public int SurveyID { get; set; }
		public int QuestionID { get; set; }
		public int OptionsPlacement { get; set; }
		public string Variablename { get; set; }
		public int SortOrder { get; set; }
		public int NoCount { get; set; }
		public int RestartCount { get; set; }
		public int ExtendedFirst { get; set; }
		public int NoBreak { get; set; }
		public int BreakAfterQuestion { get; set; }
		public int PageBreakBeforeQuestion { get; set; }
		public int FontSize { get; set; }

		public SurveyQuestion()
		{
		}
		
		public Question Question { get; set; }
		public IList<SurveyQuestionOption> Options { get; set; }
	}
}
