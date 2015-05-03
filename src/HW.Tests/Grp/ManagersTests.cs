using System;
using HW.Core;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class ManagersTests
	{
		HW.Grp.Managers v;
		ISponsorRepository r;
		
		[SetUp]
		public void Setup()
		{
			AppContext.SetRepositoryFactory(new RepositoryFactoryStub());
			
			v = new HW.Grp.Managers();
			r = new SponsorRepositoryStub();
		}
		
		[Test]
		public void SaveSession()
		{
			v.SaveAdminSession(-1, -1);
		}
		
		[Test]
		public void TestHasAccess()
		{
			v.HasAccess(1, 1);
		}
		
		[Test]
		public void TestTryDelete()
		{
			v.TryDelete(-1, -1);
			v.TryDelete(1, 1);
		}
		
		[Test]
		public void TestListSponsorAdmins()
		{
			v.SponsorAdmins = r.FindAdminBySponsor(1, 1, "ASC");
			Assert.AreEqual(3, v.SponsorAdmins.Count);
		}
	}
}
