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
	public class FeedbackTests
	{
		FeedbackService s = new FeedbackService();
		
		[Test]
		public void TestReadFeedback()
		{
			var f = s.ReadFeedback(1);
			foreach (var fq in f.Questions) {
				Console.WriteLine("Question: {0}", fq.Question.GetLanguage(1).Question);
				foreach (var qo in fq.Question.Options) {
					Console.WriteLine("\tOption: {0}", qo.Option.Internal);
					foreach (var oc in qo.Option.Components) {
						Console.WriteLine("\t\tComponent: {0}", oc.OptionComponent.Internal);
					}
				}
			}
		}
	}
}
