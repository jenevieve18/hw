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
	public class QuestionServiceTests
	{
		QuestionService s = new QuestionService();
		
		[Test]
		public void TestReadQuestion()
		{
			var q = s.ReadQuestion(62);
			Console.WriteLine(q.Internal);
			foreach (var o in q.Options) {
				Console.WriteLine("  {0}", o.Option.Internal);
			}
		}
	}
}
