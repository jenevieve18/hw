using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Survey : BaseModel
	{
		public Survey()
		{
			Questions = new List<SurveyQuestion>();
		}
		
		public int SurveyID { get; set; }
		public string Internal { get; set; }
		public Guid SurveyKey { get; set; }
		public string Copyright { get; set; }
		public int FlipFlopBg { get; set; }
		public int NoTime { get; set; }
		public int ClearQuestions { get; set; }
		public int TwoColumns { get; set; }
		public IList<SurveyQuestion> Questions { get; set; }
		
		public void AddQuestion(Question question)
		{
			AddQuestion(new SurveyQuestion { Question = question });
		}
		
		public void AddQuestion(SurveyQuestion question)
		{
			question.Survey = this;
			Questions.Add(question);
		}
	}
	
	public class SurveyLang : BaseModel
	{
		public int SurveyLangID { get; set; }
		public int SurveyID { get; set; }
		public int LangID { get; set; }
	}
	
	public class SurveyQuestion
	{
		public SurveyQuestion()
		{
		}
		
		public int SurveyQuestionID { get; set; }
		public int SurveyID { get; set; }
		public Survey Survey { get; set; }
		public int QuestionID { get; set; }
		public Question Question { get; set; }
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
	}
}
