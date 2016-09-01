// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Helpers
{
	public abstract class HighchartsChart
	{
		public Chart Chart { get; set; }
		
		public HighchartsChart(Chart chart)
		{
			this.Chart = chart;
		}
		
		public static HighchartsChart GetHighchartsChart(int optionType, Chart chart)
		{
			switch (optionType) {
					case 9: return new HighchartsBoxplot(chart);
					default: return new HighchartsColumnChart(chart);
			}
		}
	}
	
	public class HighchartsBoxplot : HighchartsChart
	{
		public HighchartsBoxplot(Chart chart) : base(chart)
		{
		}
		
		public override string ToString()
		{
			string str = @"
{
  chart: {
    type: 'boxplot'
  },
  credits: {
    enabled: false,
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
    min: 0,
    max: 100,
    tickInterval: 10,
    title: {
      text: '__YAXIS_TITLE__'
    },
    plotBands: [{
      from: 0,
      to: 30,
      color: 'rgb(255,168,168)',
    }, {
      from: 30,
      to: 60,
      color: 'rgb(255,254,190)',
    }, {
      from: 60,
      to: 100,
      color: 'rgb(204,255,187)',
    }]
  },
  plotOptions: {
    boxplot: {
      fillColor: '#4467e9',
      color: 'black',
      medianColor: 'black',
      medianWidth: 1,
      whiskerColor: 'black',
      whiskerWidth: 1
    }
  },
  series: [{
    name: '',
    data: [__DATA__]
  }]
}";
			str = str.Replace("__TITLE__", Chart.Title);
			str = str.Replace("__XAXIS_TITLE__", Chart.XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", Chart.YAxisTitle);
			string categories = "";
			foreach (var c in Chart.Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			string data = "";
			foreach (var s in Chart.Series) {
				if (s.Data.Count > 0) {
					data += new HWList(s.Data).ToString() + ",";
				} else {
					data += "[],";
				}
			}
			str = str.Replace("__DATA__", data);
			return str;
		}
	}
	
	public class HighchartsColumnChart : HighchartsChart
	{
		public HighchartsColumnChart(Chart chart) : base(chart)
		{
		}
		
		public override string ToString()
		{
			string str = @"{
  chart: {
    type: 'column'
  },
  credits: {
    enabled: false
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
    max: 100,
    tickInterval: 10,
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
  series: [__SERIES__]
}";
			str = str.Replace("__TITLE__", Chart.Title);
			str = str.Replace("__SUBTITLE__", Chart.Subtitle);
			str = str.Replace("__XAXIS_TITLE__", Chart.XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", Chart.YAxisTitle);
			string categories = "";
			foreach (var c in Chart.Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			string series = "";
			foreach (var s in Chart.Series) {
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
	
	public class HighchartsLineChart : HighchartsChart
	{
		public HighchartsLineChart(Chart chart) : base(chart)
		{
		}
		
		public override string ToString()
		{
			string str = @"
{
  title: {
    text: '__TITLE__',
    x: -20 //center
  },
  credits: {
  	enabled: false,
  },
  subtitle: {
    text: '__SUBTITLE__',
    x: -20
  },
  xAxis: {
    categories: [__CATEGORIES__]
  },
  yAxis: {
    min: 0,
    max: 100,
    tickInterval: 10,
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
			str = str.Replace("__TITLE__", Chart.Title);
			str = str.Replace("__SUBTITLE__", Chart.Subtitle);
			str = str.Replace("__XAXIS_TITLE__", Chart.XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", Chart.YAxisTitle);
			string categories = "";
			foreach (var c in Chart.Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			string series = "";
			foreach (var s in Chart.Series) {
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
