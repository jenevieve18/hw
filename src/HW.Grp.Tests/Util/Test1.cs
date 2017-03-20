
using System;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace HW.Grp.Tests.Util
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			Console.WriteLine(Guid.NewGuid().ToString().Replace("-", ""));
		}
	}
}