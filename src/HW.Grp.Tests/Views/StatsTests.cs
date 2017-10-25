/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using System.Web;
using NUnit.Framework;

namespace HW.Grp.Tests.Views
{
	[TestFixture]
	public class StatsTests
	{
		HW.Grp.Stats v;
		
		[SetUp]
		public void Setup()
		{
//			v = new HW.Grp.Stats();
//			v = new HW.Grp.Stats(WebRequest.Create("https://dev-grp.healthwatch.se") as HttpRequest);
			
			//v = new HW.Grp.Stats(new ProjectRepositoryStub(), new SponsorRepositoryStub(), new DepartmentRepositoryStub(), new ReportRepositoryStub(), new PlotTypeRepositoryStub());
		}
		
		[Test]
		public void TestSaveAdminSession()
		{
			//v.SaveAdminSession(1, 1, DateTime.Now);
		}
		
		[Test]
		public void TestIndex()
		{
			v.Index(1, 1);
		}
		
		[Test]
		public void TestGetSponsorDefaultPlotType()
		{
			Assert.AreEqual(4, GetSponsorDefaultPlotType(true, 4, 0));
			Assert.AreEqual(4, GetSponsorDefaultPlotType(true, 4, 1));
			Assert.AreEqual(4, GetSponsorDefaultPlotType(true, 4, 2));
			
			Assert.AreEqual(0, GetSponsorDefaultPlotType(false, 4, 0));
			Assert.AreEqual(0, GetSponsorDefaultPlotType(false, 4, 1));
			Assert.AreEqual(0, GetSponsorDefaultPlotType(false, 4, 2));
			
			Assert.AreEqual(0, GetSponsorDefaultPlotType(true, 4, 3));
			
			Assert.AreEqual(1, GetSponsorDefaultPlotType(true, 1, 3));
			Assert.AreEqual(2, GetSponsorDefaultPlotType(true, 2, 3));
			Assert.AreEqual(3, GetSponsorDefaultPlotType(true, 3, 3));
		}
		
		public int GetSponsorDefaultPlotType(bool forSingleSeries, int defaultPlotType, int grouping)
		{
			if (grouping == 3 && defaultPlotType > 3) {
				return 0;
			} else if (grouping <= 2 && !forSingleSeries) {
				return 0;
			} else {
				return defaultPlotType;
			}
		}
	}
}
