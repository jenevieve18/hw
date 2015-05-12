using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class StatsTests
	{
		HW.Grp.Stats v;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Stats();
			
			v = new HW.Grp.Stats(new ProjectRepositoryStub(), new SponsorRepositoryStub(), new DepartmentRepositoryStub(), new ReportRepositoryStub(), new PlotTypeRepositoryStub());
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
