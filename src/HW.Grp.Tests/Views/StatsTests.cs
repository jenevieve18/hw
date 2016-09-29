/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
			v = new HW.Grp.Stats();
			
			//v = new HW.Grp.Stats(new ProjectRepositoryStub(), new SponsorRepositoryStub(), new DepartmentRepositoryStub(), new ReportRepositoryStub(), new PlotTypeRepositoryStub());
		}
		
		[Test]
		public void TestSaveAdminSession()
		{
			v.SaveAdminSession(1, 1, DateTime.Now);
		}
		
		[Test]
		public void TestIndex()
		{
			v.Index(1, 1);
		}
	}
}
