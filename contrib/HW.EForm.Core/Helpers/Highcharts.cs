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
	
	public class Boxplot : Chart
	{
		public override string ToString()
		{
			string str = @"
{
	chart: {
	    type: 'boxplot'
	},
	title: {
	    text: '__TITLE__'
	},
	legend: {
	    enabled: false
	},
	xAxis: {
	    categories: [__CATEGORIES__],
	    title: {
	        text: '__XAXIS_TITLE__'
	    }
	},
	yAxis: {
	    title: {
	        text: '__YAXIS_TITLE__'
	    },
	},
	series: [__SERIES__]
}";
			str = str.Replace("__TITLE__", Title);
			str = str.Replace("__XAXIS_TITLE__", XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", YAxisTitle);
			string categories = "";
			foreach (var c in Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			string series = "";
			foreach (var s in Series) {
				string data = "";
				foreach (var d in s.Data) {
					data += d + ",";
				}
				series += string.Format("{{name: '{1}', data: [[{0}]]}}", data, s.Name);
			}
			str = str.Replace("__SERIES__", series);
			return str;
		}
	}
	
	public class ColumnChart : Chart
	{
		public override string ToString()
		{
			string str = @"{
  chart: {
    type: 'column'
  },
  title: {
    text: '__TITLE__'
  },
  subtitle: {
    text: '__SUBTITLE__'
  },
  xAxis: {
    categories: [__CATEGORIES__],
    crosshair: true
  },
  yAxis: {
    min: 0,
    title: {
      text: ''
    }
  },
  tooltip: {
    headerFormat: '<span style=""font-size:10px"">{point.key}</span><table>',
    pointFormat: '<tr><td style=""color:{series.color};padding:0"">{series.name}: </td>' +
      '<td style=""padding:0""><b>{point.y:.1f} mm</b></td></tr>',
    footerFormat: '</table>',
    shared: true,
    useHTML: true
  },
  plotOptions: {
    column: {
      pointPadding: 0.2,
      borderWidth: 0
    }
  },
  series: [{__SERIES__}]
}";
			str = str.Replace("__TITLE__", Title);
			str = str.Replace("__SUBTITLE__", Subtitle);
			str = str.Replace("__XAXIS_TITLE__", XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", YAxisTitle);
			string categories = "";
			foreach (var c in Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			string series = "";
			foreach (var s in Series) {
				string data = "";
				foreach (var d in s.Data) {
					data += d + ",";
				}
				series += string.Format("{{name: '{1}', data: [{0}]}},", data, s.Name);
			}
			str = str.Replace("__SERIES__", series);
			return str;
		}
	}
	
	public class LineChart : Chart
	{
		public override string ToString()
		{
			string str = @"
{
  title: {
    text: '__TITLE__',
    x: -20 //center
  },
  subtitle: {
    text: '__SUBTITLE__',
    x: -20
  },
  xAxis: {
    categories: [__CATEGORIES__]
  },
  yAxis: {
    title: {
      text: '__YAXIS_TITLE__'
    },
    plotLines: [{
      value: 0,
      width: 1,
      color: '#808080'
    }]
  },
  tooltip: {
      valueSuffix: '°C'
  },
  legend: {
      layout: 'vertical',
      align: 'right',
      verticalAlign: 'middle',
      borderWidth: 0
  },
  series: [__SERIES__]
}";
			str = str.Replace("__TITLE__", Title);
			str = str.Replace("__SUBTITLE__", Subtitle);
			str = str.Replace("__XAXIS_TITLE__", XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", YAxisTitle);
			string categories = "";
			foreach (var c in Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			string series = "";
			foreach (var s in Series) {
				string data = "";
				foreach (var d in s.Data) {
					data += d + ",";
				}
				series += string.Format("{{name: '{1}', data: [{0}]}},", data, s.Name);
			}
			str = str.Replace("__SERIES__", series);
			return str;
		}
	}
}
