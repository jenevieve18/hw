using System;
	
namespace HW.EForm.Core.Models
{
	public class ProjectRoundQO
	{
		public ProjectRoundQO()
		{
		}
		
		public int ProjectRoundQOID { get; set; }
		public int ProjectRoundID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int SortOrder { get; set; }

	}
}
