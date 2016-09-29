using System;
	
namespace HW.EForm.Core.Models
{
	public class FeedbackQuestion
	{
		public FeedbackQuestion()
		{
		}
		
		public int FeedbackQuestionID { get; set; }
		public int FeedbackID { get; set; }
		public Feedback Feedback { get; set; }
		public int QuestionID { get; set; }
		public QuestionOption QuestionOption { get; set; }
		public int Additional { get; set; }
		public int OptionID { get; set; }
		public int PartOfChart { get; set; }
		public int FeedbackTemplatePageID { get; set; }
		public int IdxID { get; set; }
		public Index Index { get; set; }
		public int HardcodedIdx { get; set; }
	}
}
