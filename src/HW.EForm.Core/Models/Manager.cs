using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Manager
	{
		public Manager()
		{
			Rounds = new List<ManagerProjectRound>();
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
		public IList<ManagerProjectRound> Rounds { get; set; }
		
		public void AddRound(ManagerProjectRound round)
		{
			round.Manager = this;
			Rounds.Add(round);
		}
	}
}
