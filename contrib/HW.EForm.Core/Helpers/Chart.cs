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
		public const int Column = OptionTypes.SingleChoice;
		public const int BoxPlot = OptionTypes.VAS;
		
		public Chart()
		{
			Series = new List<Series>();
			Categories = new List<string>();
			PlotBands = new List<PlotBand>();
		}
		
		public string ID { get; set; }
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public List<string> Categories { get; set; }
		public string XAxisTitle { get; set; }
		public string YAxisTitle { get; set; }
		public List<Series> Series { get; set; }
		public List<PlotBand> PlotBands { get; set; }
		public bool HasPlotBands {
			get { return PlotBands.Count > 0; }
		}
		public int Type { get; set; }
	}
	
	public class Series
	{
		public Series(string name, List<List<double>> data)
		{
			this.Name = name;
			this.Data = data;
		}
		
		public string Name { get; set; }
		public List<List<double>> Data { get; set; }
		public bool HasOnlyOneData {
			get { return Data.Count == 1; }
		}
		public List<double> FirstData {
			get {
				if (Data.Count > 0) {
					return Data[0];
				}
				return null;
			}
		}
	}
	
	public class PlotBand
	{
		public double From { get; set; }
		public double To { get; set; }
		public string Color { get; set; }
	}
}
