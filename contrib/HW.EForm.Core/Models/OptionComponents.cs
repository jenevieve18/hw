using System;
	
namespace HW.EForm.Core.Models
{
	public class OptionComponents
	{
		public int OptionComponentsID { get; set; }
		public int OptionComponentID { get; set; }
		public int OptionID { get; set; }
		public int ExportValue { get; set; }
		public int SortOrder { get; set; }

		public OptionComponents()
		{
		}
		
		public OptionComponent Component { get; set; }
	}
}
