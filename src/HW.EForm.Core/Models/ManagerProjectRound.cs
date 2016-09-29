using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ManagerProjectRound
	{
		public ManagerProjectRound()
		{
			Units = new List<ManagerProjectRoundUnit>();
		}
		
		public int ManagerProjectRoundID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ManagerID { get; set; }
		public Manager Manager { get; set; }
		public int ShowAllUnits { get; set; }
		public string EmailSubject { get; set; }
		public string EmailBody { get; set; }
		public Guid MPRK { get; set; }
		public IList<ManagerProjectRoundUnit> Units { get; set; }
		
		public void AddUnit(ProjectRoundUnit unit)
		{
			AddUnit(new ManagerProjectRoundUnit { ProjectRoundUnit = unit });
		}
		
		public void AddUnit(ManagerProjectRoundUnit unit)
		{
			Units.Add(unit);
		}
	}
}
