using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class FeedbackQuestion
	{
		public int FeedbackQuestionID { get; set; }
		public int FeedbackID { get; set; }
		public int QuestionID { get; set; }
        public int Additional { get; set; }
		public int OptionID { get; set; }
		public int PartOfChart { get; set; }
		public Question Question { get; set; }
		public Feedback Feedback { get; set; }
		public Option Option { get; set; }
		public bool IsPartOfChart {
			get { return PartOfChart != 0; }
		}
		public bool HasQuestion {
			get { return QuestionID < 0 || Question != null; }
		}
		public bool HasOption {
			get { return Option != null; }
		}
		public int FeedbackTemplatePageID { get; set; }
		public int IdxID { get; set; }
		public int HardcodedIdx { get; set; }
		public Index Index { get; set; }
		public bool HasIndex {
			get { return Index != null; }
		}
	}
}
