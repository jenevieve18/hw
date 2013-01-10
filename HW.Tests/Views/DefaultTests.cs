//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Web;
using HW.Core;
using HWgrp;
using NUnit.Framework;

namespace HW.Tests.Views
{
	[TestFixture]
	public class DefaultTests
	{
		Default p;
		
		[SetUp]
		public void Setup()
		{
			AppContext.SetRepositoryFactory(new SqlRepositoryFactory());
			p = new Default();
		}
		
		[Test]
		public void TestLogin()
		{
			p.Login(null, null, null, "", "Usr1", "Pas1");
		}
		
		[Test]
		[Ignore("Can't test session for now. Prolly in the future.")]
		public void TestLogout()
		{
			p.Logout();
		}
	}
}
