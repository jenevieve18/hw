using System;
	
namespace HW.EForm.Core.Models
{
	public class OptionComponents
	{
		public OptionComponents()
		{
		}
		
		public int OptionComponentsID { get; set; }
		public int OptionComponentID { get; set; }
		public int OptionID { get; set; }
		public int ExportValue { get; set; }
		public int SortOrder { get; set; }

		public OptionComponent OptionComponent { get; set; }
	}
}
