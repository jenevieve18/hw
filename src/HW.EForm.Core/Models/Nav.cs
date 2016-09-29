using System;
	
namespace HW.EForm.Core.Models
{
	public class Nav
	{
		public Nav()
		{
		}
		
		public int NavID { get; set; }
		public string NavURL { get; set; }
		public string NavText { get; set; }
		public int SortOrder { get; set; }

	}
}
