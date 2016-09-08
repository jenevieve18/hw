using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class Feedback
	{
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }
		public int SurveyID { get; set; }
		public int Compare { get; set; }
		public int FeedbackTemplateID { get; set; }
		public int NoHardcodedIdxs { get; set; }
		
		public Survey Survey { get; set; }
		public IList<FeedbackQuestion> Questions { get; set; }

		public Feedback()
		{
			Questions = new List<FeedbackQuestion>();
		}
		
		public Dictionary<int, List<FeedbackQuestion>> GetGroupedQuestions()
		{
			var groups = new Dictionary<int, List<FeedbackQuestion>>();
			foreach (var fq in Questions) {
				if (!groups.ContainsKey(fq.PartOfChart)) {
					groups.Add(fq.PartOfChart, new List<FeedbackQuestion>());
				}
				var @group = groups[fq.PartOfChart];
				@group.Add(fq);
			}
			return groups;
		}
		
		public void AddQuestion(Question q)
		{
			AddQuestion(new FeedbackQuestion { Question = q });
		}
		
		public void AddQuestion(FeedbackQuestion fq)
		{
			Questions.Add(fq);
		}
	}
}
