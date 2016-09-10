using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

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
		
		public Survey Survey { get; set; }
		public IList<FeedbackQuestion> Questions { get; set; }

//		public Dictionary<int, List<FeedbackQuestion>> GetGroupedQuestions()
//		{
//			var groups = new Dictionary<int, List<FeedbackQuestion>>();
//			foreach (var fq in Questions) {
//				if (!groups.ContainsKey(fq.PartOfChart)) {
//					groups.Add(fq.PartOfChart, new List<FeedbackQuestion>());
//				}
//				var @group = groups[fq.PartOfChart];
//				@group.Add(fq);
//			}
//			return groups;
//		}
		
		List<int> chartIDs = new List<int>();
		public bool HasGroupedChart(int chartID)
		{
			return chartIDs.Contains(chartID);
		}
		
		public IList<FeedbackQuestion> GetQuestions(int chartID)
		{
			chartIDs.Add(chartID);
			return (Questions as List<FeedbackQuestion>).FindAll(x => x.PartOfChart == chartID);
		}
		
		public void AddQuestion(Question q)
		{
			AddQuestion(new FeedbackQuestion { Question = q });
		}
		
		public void AddQuestion(FeedbackQuestion fq)
		{
			fq.Feedback = this;
			Questions.Add(fq);
		}
	}
}
