using System;
using System.Collections.Generic;
using System.Web;
using HW.Core;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class DefaultTests
	{
		HW.Grp.Default v;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Default();

			v = new HW.Grp.Default(new SponsorRepositoryStub(), new NewsRepositoryStub());
		}
		
		[Test]
		public void TestIndex()
		{
			v.Index();
		}
		
		[Test]
		public void TestLogin()
		{
//			p.Login(null, null, null, "", "Usr1", "Pas1");
		}
		
		[Test]
		[Ignore("Can't test session for now. Prolly in the future.")]
		public void TestLogout()
		{
//			p.Logout();
		}
	}
}
