using System;
	
namespace HW.EForm.Core.Models
{
	public class FeedbackQuestion
	{
		public int FeedbackQuestionID { get; set; }
		public int FeedbackID { get; set; }
		public int QuestionID { get; set; }

		public FeedbackQuestion()
		{
		}
		
		public Question Question { get; set; }
	}
}
