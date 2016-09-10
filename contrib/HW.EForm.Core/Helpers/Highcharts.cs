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
		string guid;
		bool justScript;
		
		public HighchartsChart(Chart chart) : this(chart, false)
		{
		}
		
		public HighchartsChart(Chart chart, bool justScript)
		{
			this.Chart = chart;
			this.guid = Guid.NewGuid().ToString();
			this.justScript = justScript;
		}
		
		public Chart Chart { get; set; }
		
		public static HighchartsChart GetHighchartsChart(int optionType, Chart chart)
		{
			return GetHighchartsChart(optionType, chart, false);
		}
		
		public static HighchartsChart GetHighchartsChart(int optionType, Chart chart, bool justScript)
		{
			switch (optionType) {
					case 9: return new HighchartsBoxplot(chart, justScript);
					default: return new HighchartsColumnChart(chart, justScript);
			}
		}
		
		public abstract string GetOptions();
		
		public override string ToString()
		{
			string str = @"__HEADER__
<script>
$(function() {
	$('#container__GUID__').highcharts(__OPTIONS__);
});
</script>
<div id='container__GUID__'></div>
__BODYEND__";
			str = str.Replace("__OPTIONS__", GetOptions());
			str = str.Replace("__GUID__", Chart.ID);
			str = str.Replace("__HEADER__", justScript ? "" : GetHeader());
			str = str.Replace("__BODYEND__", justScript ? "" : GetBodyEnd());
			return str;
		}
		
		string GetHeader()
		{
			return @"<html>
<head>
<meta charset='utf-8'>
<script src='https://code.jquery.com/jquery-2.2.4.min.js'></script>
<script src='https://code.highcharts.com/highcharts.js'></script>
<script src='https://code.highcharts.com/highcharts-more.js'></script>
<script src='https://code.highcharts.com/modules/exporting.js'></script>
</head>
<body>";
		}
		
		string GetBodyEnd()
		{
			return @"
</body>";
		}
	}
	
	public class HighchartsBoxplot : HighchartsChart
	{
		public HighchartsBoxplot(Chart chart) : this(chart, false)
		{
		}
		
		public HighchartsBoxplot(Chart chart, bool justScript) : base(chart, justScript)
		{
		}
		
		public override string GetOptions()
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
    __PLOT_BANDS__
  },
  plotOptions: {
    boxplot: {
      fillColor: '#8BB9DE',
      color: 'black',
      medianColor: 'black',
      medianWidth: 2,
      whiskerColor: 'black',
      whiskerWidth: 2
    }
  },
  series: [__SERIES__]
}";
			str = str.Replace("__TITLE__", Chart.Title);
			str = str.Replace("__XAXIS_TITLE__", Chart.XAxisTitle);
			str = str.Replace("__YAXIS_TITLE__", Chart.YAxisTitle);
			
			string categories = "";
			foreach (var c in Chart.Categories) {
				categories += string.Format("'{0}',", c);
			}
			str = str.Replace("__CATEGORIES__", categories);
			
			string plotBands = "";
			if (Chart.HasPlotBands) {
				plotBands += "plotBands: [";
				foreach (var p in Chart.PlotBands) {
					plotBands += "{ from: " + p.From + ", to: " + p.To + ", color: '" + p.Color + "' }, ";
				}
				plotBands += "],";
			}
			str = str.Replace("__PLOT_BANDS__", plotBands);
			
			string series = "{ name: 'Observations', pointWidth: 40, data: [__DATA__], tooltip: { headerFormat: '<em>{point.key}</em><br/>' }}";
//			string data = "";
//			foreach (var s in Chart.Series) {
//				if (s.Data.Count > 0) {
//					data += new HWList(s.Data).ToString() + ",";
//				} else {
//					data += "[],";
//				}
//			}
			string data = "";
			foreach (var s in Chart.Series) {
				foreach (var d in s.Data) {
					if (d.Count > 0) {
						data += new HWList(d).ToString() + ",";
					} else {
						data += "[],";
					}
				}
			}
			series = series.Replace("__DATA__", data);
			
			str = str.Replace("__SERIES__", series);
			return str;
		}
	}
	
	public class HighchartsColumnChart : HighchartsChart
	{
		public HighchartsColumnChart(Chart chart) : this(chart, false)
		{
		}
		
		public HighchartsColumnChart(Chart chart, bool justScript) : base(chart, justScript)
		{
		}
		
		public override string GetOptions()
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
      '<td style=""padding:0""><b>{point.y:.1f}%</b></td></tr>',
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
				if (s.HasOnlyOneData) {
					foreach (var d in s.FirstData) {
						data += d + ",";
					}
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
		
		public override string GetOptions()
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
				if (s.HasOnlyOneData) {
					foreach (var d in s.FirstData) {
						data += d + ",";
					}
				}
				series += string.Format("{{name: '{1}', data: [{0}]}},", data, s.Name);
			}
			str = str.Replace("__SERIES__", series);
			return str;
		}
	}
}
