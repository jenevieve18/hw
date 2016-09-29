using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Manager : BaseModel
	{
		public Manager()
		{
			ProjectRounds = new List<ManagerProjectRound>();
		}
		
		public int ManagerID { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public int AddUser { get; set; }
		public int SeeAnswer { get; set; }
		public int ExpandAll { get; set; }
		public int UseExternalID { get; set; }
		public int SeeFeedback { get; set; }
		public int HasFeedback { get; set; }
		public int SeeUnit { get; set; }
		public int SeeTerminated { get; set; }
		public int SeeSurvey { get; set; }

		public IList<ManagerProjectRound> ProjectRounds { get; set; }
		
		public void AddProjectRound(ProjectRound round)
		{
			AddProjectRound(new ManagerProjectRound { ProjectRound = round });
		}
		
		public void AddProjectRound(ManagerProjectRound round)
		{
			round.Manager = this;
			ProjectRounds.Add(round);
		}
	}
}
