using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Project
	{
		public Project()
		{
			Rounds = new List<ProjectRound>();
			Surveys = new List<ProjectSurvey>();
		}
		
		public int ProjectID { get; set; }
		public string Internal { get; set; }
		public string Name { get; set; }
		public string AppURL { get; set; }
		
		public IList<ProjectRound> Rounds { get; set; }
		public IList<ProjectSurvey> Surveys { get; set; }

		public void AddSurvey(Survey s)
		{
			AddSurvey(new ProjectSurvey { Survey = s });
		}
		
		public void AddSurvey(ProjectSurvey s)
		{
			Surveys.Add(s);
		}
		
		public void AddRound(ProjectRound r)
		{
			Rounds.Add(r);
		}
	}
}
