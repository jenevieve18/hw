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
//			var a = s.ReadAnswer(214);
			var a = s.ReadAnswer(9534);
			Console.WriteLine("Email: {0}, AnswerKey: {1}", a.HasProjectRoundUser ? a.ProjectRoundUser.Email : "", a.AnswerKey);
			foreach (var av in a.Values) {
				Console.WriteLine("\tValueInt: {0}, QuestionID: {3}, Question: {1}, Option: {2}", av.ValueInt, av.Question.Internal, av.Option.Internal, av.QuestionID);
			}
		}
	}
}
