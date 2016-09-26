// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Data.Odbc;
using NUnit.Framework;

namespace HW.EForm.Tests
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			OdbcConnection c = new OdbcConnection("Driver={SQL Server};Server=myServerAddress;Database=myDataBase;Trusted_Connection=Yes;");
		}
	}
}
