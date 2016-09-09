// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Helpers
{
	public class Chart
	{
		public Chart()
		{
			Series = new List<Series>();
			Categories = new List<string>();
			PlotBands = new List<PlotBand>();
		}
		
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public List<string> Categories { get; set; }
		public string XAxisTitle { get; set; }
		public string YAxisTitle { get; set; }
		public List<Series> Series { get; set; }
		public bool HasBackground { get; set; }
		public List<PlotBand> PlotBands { get; set; }
		public bool HasPlotBands {
			get { return PlotBands.Count > 0; }
		}
	}
	
	public class Series
	{
		public Series(string name, List<double> data)
		{
			this.Name = name;
			this.Data = data;
		}
		
		public string Name { get; set; }
		public List<double> Data { get; set; }
	}
	
	public class PlotBand
	{
		public double From { get; set; }
		public double To { get; set; }
		public string Color { get; set; }
	}
}
