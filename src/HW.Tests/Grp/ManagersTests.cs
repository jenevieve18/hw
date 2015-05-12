using System;
using HW.Core;
using HW.Core.Repositories;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class ManagersTests
	{
		HW.Grp.Managers v;
//		ISponsorRepository r;
		
		[SetUp]
		public void Setup()
		{
//			AppContext.SetRepositoryFactory(new RepositoryFactoryStub());
			
			v = new HW.Grp.Managers();
			
			v = new HW.Grp.Managers(new ManagerService(new ManagerFunctionRepositoryStub(), new SponsorRepositoryStub(), new SponsorAdminRepositoryStub()));
//			r = new SponsorRepositoryStub();
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
		public void TestDelete()
		{
			v.Delete(-1, -1);
			v.Delete(1, 1);
		}
		
		[Test]
		public void TestIndex()
		{
//			v.SponsorAdmins = r.FindAdminBySponsor(1, 1, "ASC");
			v.Index(1, 1, 1, 1);
//			Assert.AreEqual(3, v.SponsorAdmins.Count);
		}
	}
}
