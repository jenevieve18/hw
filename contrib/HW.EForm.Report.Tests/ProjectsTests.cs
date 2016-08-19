// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using NUnit.Framework;

namespace HW.EForm.Report.Tests
{
	[TestFixture]
	public class ProjectsTests
	{
		Projects v = new Projects();
		
		[Test]
		public void TestIndex()
		{
			v.Index();
		}
	}
}
