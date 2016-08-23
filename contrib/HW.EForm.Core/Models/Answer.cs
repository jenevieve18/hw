using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Answer
	{
		public int AnswerID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public DateTime? StartDT { get; set; }
		public DateTime? EndDT { get; set; }
		public Guid AnswerKey { get; set; }
		public int ExtendedFirst { get; set; }
		public int CurrentPage { get; set; }
		public int FeedbackAlert { get; set; }

		public Answer()
		{
		}
		
		public IList<AnswerValue> Values { get; set; }
	}
}
