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
			Console.WriteLine("QuestionID: {0}, Question: {1}", q.QuestionID, q.GetLanguage(1).Question);
			foreach (var o in q.Options) {
				Console.WriteLine("\tOptionID: {0}, Internal: {1}", o.OptionID, o.Option.Internal);
				foreach (var oc in o.Option.Components) {
					Console.WriteLine("\t\tOptionComponentID: {0}, Internal: {1}", oc.OptionComponentID, oc.OptionComponent.Internal);
				}
			}
		}
	}
}
