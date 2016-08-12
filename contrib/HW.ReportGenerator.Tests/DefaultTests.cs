/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 8/11/2016
 * Time: 12:37 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;

namespace HW.ReportGenerator.Tests
{
	[TestFixture]
	public class DefaultTests
	{
		Default v = new Default();
		
		[Test]
		public void TestMethod()
		{
			v.Login("demo", "password");
		}
	}
}
