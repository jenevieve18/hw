//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using HW.Core.Helpers;
using NUnit.Framework;
using w = DocumentFormat.OpenXml.Wordprocessing;

namespace HW.Tests
{
	[TestFixture]
	public class Test2
	{
		[Test]
		public void TestMethod()
		{
			Q q = new Q();
			q.Add("LangID", 1);
			q.Add("PLOT", "BOXPLOT");
			q.Add("PRUID", 1);
			q.Add("TEST", "");
			Console.WriteLine(q.ToString());
		}
	}
}
