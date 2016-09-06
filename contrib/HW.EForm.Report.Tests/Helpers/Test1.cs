// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.IO;
using System.Net;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using NUnit.Framework;
using RestSharp;

namespace HW.EForm.Report.Tests.Helpers
{
	[TestFixture]
	public class Test1
	{
		string options;
		
		[SetUp]
		public void Setup()
		{
			options = @"{
  chart: {
    type: 'boxplot'
  },
  credits: {
    enabled: false,
  },
  title: {
    text: 'Hur sov du i natt?'
  },
  legend: {
    enabled: false
  },
  xAxis: {
    categories: ['Unit97',],
    title: {
      text: ''
    }
  },
  yAxis: {
    min: 0,
    max: 100,
    tickInterval: 10,
    title: {
      text: ''
    },
    
    plotBands: [{
      from: 0,
      to: 50,
      color: 'rgb(255,168,168)',
    }, {
      from: 50,
      to: 60,
      color: 'rgb(255,254,190)',
    }, {
      from: 60,
      to: 101,
      color: 'rgb(204,255,187)',
    }, {
      from: 101,
      to: 101,
      color: 'rgb(255,254,190)',
    }, {
      from: 101,
      to: 101,
      color: 'rgb(255,168,168)',
    }],
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
  series: [{
    name: '',
    pointWidth: 40,
    data: [[27.0,54.0,72.0,83.0,94.0],]
  }]
}";
		}
		
		[Test]
		public void TestMethod()
		{
			using (WordprocessingDocument package = WordprocessingDocument.Create("test.docx", WordprocessingDocumentType.Document))
			{
				package.AddMainDocumentPart();

				package.MainDocumentPart.Document =
					new DocumentFormat.OpenXml.Wordprocessing.Document(
						new DocumentFormat.OpenXml.Wordprocessing.Body(
							new DocumentFormat.OpenXml.Wordprocessing.Paragraph(
								new DocumentFormat.OpenXml.Wordprocessing.Run(
									new DocumentFormat.OpenXml.Wordprocessing.Text("Hello World!")))));

				package.MainDocumentPart.Document.Save();
			}
		}
		
		[Test]
		public void b()
		{
			var client = new RestClient("http://export.highcharts.com");

			StringBuilder json = new StringBuilder(options);

			var request = new RestRequest("/", Method.POST);
			request.AddHeader("Content-Type", "multipart/form-data");
			request.AddParameter("content", "options");
			request.AddParameter("options", json);
			request.AddParameter("constr", "Chart");
			request.AddParameter("type", "image/png");
			var response = (RestResponse)client.Execute(request);
			Console.WriteLine(Convert.ToBase64String(response.RawBytes));
		}
	}
}
