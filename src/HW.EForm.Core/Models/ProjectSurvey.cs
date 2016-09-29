using System;

namespace HW.EForm.Core.Models
{
	public class ProjectSurvey
	{
		public ProjectSurvey()
		{
		}
		
		public int ProjectSurveyID { get; set; }
		public int ProjectID { get; set; }
		public Project Project { get; set; }
		public int SurveyID { get; set; }
		public Survey Survey { get; set; }
	}
}
