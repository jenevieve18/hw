using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Survey
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
}