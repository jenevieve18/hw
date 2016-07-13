/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;

namespace HW.Grp.Tests
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
			
            //v = new HW.Grp.Managers(new ManagerService(new ManagerFunctionRepositoryStub(), new SponsorRepositoryStub(), new SponsorAdminRepositoryStub()));
            v = new HW.Grp.Managers(new ManagerService(new SqlManagerFunctionRepository(), new SqlSponsorRepository(), new SqlSponsorAdminRepository()));
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
            //v.Index(1, 1, 1, 1);
//			Assert.AreEqual(3, v.SponsorAdmins.Count);
		}
	}
}
