using System;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class StatsTests
	{
		StatsPage v;
		
		[SetUp]
		public void Setup()
		{
			v = new StatsPage();
		}
		
		[Test]
		public void TestMethod()
		{
		}
		
		class StatsPage : HW.Grp.Stats
		{
		}
	}
}
