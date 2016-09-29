using System;
	
namespace HW.EForm.Core.Models
{
	public class Answer
	{
		public Answer()
		{
		}
		
		public int AnswerID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public string StartDT { get; set; }
		public string EndDT { get; set; }
		public Guid AnswerKey { get; set; }
		public int ExtendedFirst { get; set; }
		public int CurrentPage { get; set; }
		public int FeedbackAlert { get; set; }

	}
}
