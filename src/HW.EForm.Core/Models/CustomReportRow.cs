using System;
	
namespace HW.EForm.Core.Models
{
	public class CustomReportRow
	{
		public CustomReportRow()
		{
		}
		
		public int CustomReportRowID { get; set; }
		public int CustomReportID { get; set; }
		public string Before { get; set; }
		public string Editable { get; set; }
		public string After { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

	}
}
