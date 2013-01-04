//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using NUnit.Framework;

namespace HW.Tests.Models
{
	[TestFixture]
	public class DbTests
	{
		[Test]
		public void TestHeader()
		{
			string s = Db.header();
		}
	}
}
