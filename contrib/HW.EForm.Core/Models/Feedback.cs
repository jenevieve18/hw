using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Feedback
	{
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }

		public Feedback()
		{
		}
		
		public IList<FeedbackQuestion> Questions { get; set; }
		
//		IList<FeedbackQuestion> questions;
//		
//		public IList<FeedbackQuestion> Questions {
//			get {
//				if (questions == null) {
//					OnQuestionsGet(null);
//				}
//				return questions;
//			}
//			set { questions = value; }
//		}
//		
//		public event EventHandler QuestionsGet;
//		
//		protected virtual void OnQuestionsGet(EventArgs e)
//		{
//			if (QuestionsGet != null) {
//				QuestionsGet(this, e);
//			}
//		}
	}
}
