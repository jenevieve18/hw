using System;
	
namespace HW.EForm.Core.Models
{
	public class PlotTypeLang
	{
		public PlotTypeLang()
		{
		}
		
		public int PlotTypeLangID { get; set; }
		public int PlotTypeID { get; set; }
		public int LangID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortName { get; set; }
		public int SupportsMultipleSeries { get; set; }

	}
}
