﻿using System;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Core.Tests.Models
{
	[TestFixture]
	public class ProjectTests
	{
		[Test]
		public void TestMethod()
		{
			var p = new Project {};
			p.AddRound(new ProjectRound());
		}
	}
}
