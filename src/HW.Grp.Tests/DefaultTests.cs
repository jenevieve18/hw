/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using HW.Core.Repositories;
using NUnit.Framework;

namespace HW.Grp.Tests
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
