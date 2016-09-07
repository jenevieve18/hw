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

		public Feedback()
		{
		}
		
		public IList<FeedbackQuestion> Questions { get; set; }
		
//		public IList<FeedbackQuestion> GroupedQuestions { get; set; }
//		
//		public void GroupQuestions(int[] questionIDs)
//		{
//			GroupedQuestions = new List<FeedbackQuestion>(Questions);
//			foreach (var fq in GroupedQuestions) {
//				
//			}
//		}
//		
//		public IList<FeedbackQuestion> lalala(int[] questionIDs)
//		{
//			var x = new List<FeedbackQuestion>();
//			foreach (var questionID in questionIDs) {
//				var fq = GetFeedbackQuestion(Questions, questionID);
//				x.Add(fq);
//			}
//			return x;
//		}
//		
//		FeedbackQuestion GetFeedbackQuestion(IList<FeedbackQuestion> questions, int questionID)
//		{
//			foreach (var fq in questions) {
//				if (fq.QuestionID == questionID) {
//					return fq;
//				}
//			}
//			return null;
//		}
	}
}
