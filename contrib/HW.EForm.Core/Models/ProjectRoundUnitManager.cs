using System;
	
namespace HW.EForm.Core.Models
{
	public class ProjectRoundUnitManager
	{
		public int ProjectRoundUnitManagerID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundUserID { get; set; }

		public ProjectRoundUnitManager()
		{
		}
		
		public ProjectRoundUser User { get; set; }
	}
}