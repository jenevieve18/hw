using System;
	
namespace HW.EForm.Core.Models
{
	public class OptionComponent
	{
		public OptionComponent()
		{
		}
		
		public int OptionComponentID { get; set; }
		public int ExportValue { get; set; }
		public string Internal { get; set; }
		public int OptionComponentContainerID { get; set; }

	}
}
