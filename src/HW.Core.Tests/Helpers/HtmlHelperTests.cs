using System;
using HW.Core.Helpers;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class HtmlHelperTests
	{
		[Test]
		public void TestAnchor()
		{
			Assert.AreEqual("<a href='exercise.aspx?SORT=2&EAID=7&ECID=2#filter' class='active'>Random</a>", HtmlHelper.Anchor("Random", "exercise.aspx?SORT=2&EAID=7&ECID=2#filter", "class='active'"));
			Assert.AreEqual("<a href='exercise.aspx?SORT=2&EAID=7&ECID=2#filter' class='active' target='_blank'>Random</a>", HtmlHelper.Anchor("Random", "exercise.aspx?SORT=2&EAID=7&ECID=2#filter", "class='active' target='_blank'"));
		}
		
		[Test]
		public void TestAnchorSpan()
		{
			Assert.AreEqual("<a href='exercise.aspx?SORT=2&EAID=7&ECID=2#filter' class='active'><span>Random</span></a>", HtmlHelper.AnchorSpan("Random", "exercise.aspx?SORT=2&EAID=7&ECID=2#filter", "class='active'"));
			Assert.AreEqual("<a href='exercise.aspx?SORT=2&EAID=7&ECID=2#filter' class='active' target='_blank'><span>Random</span></a>", HtmlHelper.AnchorSpan("Random", "exercise.aspx?SORT=2&EAID=7&ECID=2#filter", "class='active' target='_blank'"));
		}
	}
}
