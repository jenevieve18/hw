using System;
	
namespace HW.EForm.Core.Models
{
	public class ManagerProjectRoundUnit
	{
		public int ManagerProjectRoundUnitID { get; set; }
		public int ManagerID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ProjectRoundUnitID { get; set; }

		public ManagerProjectRoundUnit()
		{
		}
		
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
	}
}