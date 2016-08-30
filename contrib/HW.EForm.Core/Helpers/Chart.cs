// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Helpers
{
	public class Chart
	{
		public string Title { get; set; }
		public string Subtitle { get; set; }
		public List<string> Categories { get; set; }
		public string XAxisTitle { get; set; }
		public string YAxisTitle { get; set; }
		public List<Series> Series { get; set; }
		
		public Chart()
		{
			Series = new List<Series>();
			Categories = new List<string>();
		}
	}
	
	public class Series
	{
		public string Name { get; set; }
		public List<double> Data { get; set; }
		
		public Series(string name, List<double> data)
		{
			this.Name = name;
			this.Data = data;
		}
	}
}
