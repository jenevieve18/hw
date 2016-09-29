using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Feedback
	{
		public Feedback()
		{
			Questions = new List<FeedbackQuestion>();
		}
		
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }
		public int SurveyID { get; set; }
		public int Compare { get; set; }
		public int FeedbackTemplateID { get; set; }
		public int NoHardcodedIdxs { get; set; }
		public IList<FeedbackQuestion> Questions { get; set; }
		
		public void AddQuestion(Index index)
		{
			AddQuestion(new FeedbackQuestion { Index = index });
		}
		
		public void AddQuestion(QuestionOption question)
		{
			AddQuestion(new FeedbackQuestion { QuestionOption = question });
		}
		
		public void AddQuestion(FeedbackQuestion question)
		{
			question.Feedback = this;
			Questions.Add(question);
		}
	}
}
