using System;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Tests.Grp
{
	[TestFixture]
	public class MessagesTests
	{
		HW.Grp.Messages v;
		
		[SetUp]
		public void Setup()
		{
			v = new HW.Grp.Messages();
		}
		
		[Test]
		public void TestSetSponsor()
		{
//			v.SetSponsor(new Sponsor(), false, false);
		}
	}
}
