using System;
using System.Collections.Generic;
using System.Web;
using HW.Core;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class DefaultTests
	{
		HW.Grp.Default p;
		
		[SetUp]
		public void Setup()
		{
			AppContext.SetRepositoryFactory(new SqlRepositoryFactory());
			
			p = new HW.Grp.Default();
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
