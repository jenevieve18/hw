using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Project
	{
		public Project()
		{
			Surveys = new List<ProjectSurvey>();
			Rounds = new List<ProjectRound>();
		}
		
		public int ProjectID { get; set; }
		public string Internal { get; set; }
		public string Name { get; set; }
		public string AppURL { get; set; }
		public IList<ProjectSurvey> Surveys { get; set; }
		public IList<ProjectRound> Rounds { get; set; }
		
		public void AddSurvey(Survey survey)
		{
			AddSurvey(new ProjectSurvey { Survey = survey });
		}
		
		public void AddSurvey(ProjectSurvey survey)
		{
			survey.Project = this;
			Surveys.Add(survey);
		}
		
		public void AddRound(ProjectRound round)
		{
			round.Project = this;
			Rounds.Add(round);
		}
	}
}
