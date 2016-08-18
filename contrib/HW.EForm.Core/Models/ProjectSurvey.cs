using System;
	
namespace HW.EForm.Core.Models
{
	public class ProjectSurvey
	{
		public int ProjectSurveyID { get; set; }
		public int ProjectID { get; set; }
		public int SurveyID { get; set; }

		public ProjectSurvey()
		{
		}
		
		public Survey Survey { get; set; }
	}
}
