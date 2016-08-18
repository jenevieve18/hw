using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Project
	{
		public int ProjectID { get; set; }
		public string Internal { get; set; }
		public string Name { get; set; }
		public string AppURL { get; set; }

		public Project()
		{
		}
		
		public IList<ProjectRound> Rounds { get; set; }
		public IList<ProjectSurvey> Surveys { get; set; }
	}
}
