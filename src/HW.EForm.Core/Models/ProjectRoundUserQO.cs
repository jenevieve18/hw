using System;
	
namespace HW.EForm.Core.Models
{
	public class ProjectRoundUserQO
	{
		public ProjectRoundUserQO()
		{
		}
		
		public int ProjectRoundUserQOID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public string Answer { get; set; }

	}
}
