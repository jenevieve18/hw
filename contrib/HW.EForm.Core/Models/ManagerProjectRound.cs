using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ManagerProjectRound
	{
		public int ManagerProjectRoundID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ManagerID { get; set; }
		public int ShowAllUnits { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public Guid MPRK { get; set; }

		public ManagerProjectRound()
		{
		}
		
		public ProjectRound ProjectRound { get; set; }
		public IList<ManagerProjectRoundUnit> Units { get; set; }
	}
}
