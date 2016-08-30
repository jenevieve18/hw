// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Views
{
	[TestFixture]
	public class DefaultTests
	{
		Default v = new Default();
		
		[Test]
		public void TestLogin()
		{
			v.Login("info1@eform.se", "password1");
		}
	}
}
