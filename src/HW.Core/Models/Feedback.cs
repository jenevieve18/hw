using System;

namespace HW.Core.Models
{
	public class Feedback : BaseModel
	{
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }
		public int SurveyID { get; set; }
		public int Compare { get; set; }
		public int FeedbackTemplateID { get; set; }
		public int NoHardcodedIdxs { get; set; }
	}
	
	public class FeedbackQuestion : BaseModel
	{
		public int FeedbackQuestionID { get; set; }
		public int FeedbackID { get; set; }
		public int QuestionID { get; set; }
		public int Additional { get; set; }
		public int OptionID { get; set; }
		public int PartOfChart { get; set; }
		public int FeedbackTemplatePageID { get; set; }
		public int IdxID { get; set; }
		public int HardcodedIdx { get; set; }
		public Feedback Feedback { get; set; }
		public Question Question { get; set; }
	}
}
