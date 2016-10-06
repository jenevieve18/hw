using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Models
{
	public class PlotType : BaseModel
	{
		public string Name { get; set; }
		public string Description { get; set; }
		
		public const int Line = 1;
		public const int LineSD = 2;
		public const int LineSDWithCI = 3;
		public const int BoxPlotMinMax = 4;
		public const int BoxPlot = 5;
		public const int Verbose = 7;
		public const int Bar = 6;
		
		public static string GetString(int plot)
		{
			switch (plot) {
					case Line: return "Line";
					case LineSD: return "Line (± SD)";
					case LineSDWithCI: return "Line (± 1.96 SD)";
					case BoxPlotMinMax: return "BoxPlot (Min/Max)";
					case BoxPlot: return "BoxPlot (Tukey)";
					default: throw new NotSupportedException();
			}
		}
	}
	
	public class PlotTypeLanguage : BaseModel
	{
		public PlotType PlotType { get; set; }
		public Language Language { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortName { get; set; }
		public bool SupportsMultipleSeries { get; set; }
	}
}
