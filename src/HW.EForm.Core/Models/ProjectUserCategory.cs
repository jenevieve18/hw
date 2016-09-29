using System;
	
namespace HW.EForm.Core.Models
{
	public class ProjectUserCategory
	{
		public ProjectUserCategory()
		{
		}
		
		public int ProjectUserCategoryID { get; set; }
		public int ProjectID { get; set; }
		public int UserCategoryID { get; set; }

	}
}
