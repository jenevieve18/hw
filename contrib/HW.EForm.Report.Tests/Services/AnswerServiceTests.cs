// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Services;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Services
{
	[TestFixture]
	public class AnswerServiceTests
	{
		AnswerService s = new AnswerService();
		
		[Test]
		public void TestReadAnswer()
		{
			var a = s.ReadAnswer(214);
			Console.WriteLine(a.AnswerKey);
			foreach (var av in a.Values) {
				Console.WriteLine("\tValueInt: {0}, Question: {1}, Option: {2}", av.ValueInt, av.Question.Internal, av.Option.Internal);
			}
		}
		
		[Test]
		public void TestMethod()
		{
		}
	}
}
