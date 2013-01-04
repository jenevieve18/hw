﻿//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class OptionRepositoryTests
	{
		SqlOptionRepository r;
		
		[SetUp]
		public void Setup()
		{
			r = new SqlOptionRepository();
		}
		
		[Test]
		public void TestCountByOption()
		{
			r.CountByOption(1);
		}
		
		[Test]
		public void TestFindComponentsByLanguage()
		{
			r.FindComponentsByLanguage(33, 1);
		}
	}
}
