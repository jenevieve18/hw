using System;
	
namespace HW.Core2.Models
{
	public class UserProjectRoundUserAnswer
	{
		public int UserProjectRoundUserAnswerID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public string AnswerKey { get; set; }
		public DateTime? DT { get; set; }
		public int UserProfileID { get; set; }
		public int AnswerID { get; set; }

		public UserProjectRoundUserAnswer()
		{
		}
	}
}
