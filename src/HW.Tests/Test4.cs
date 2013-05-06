//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core.Repositories.NHibernate;
using NUnit.Framework;

namespace HW.Tests
{
	[TestFixture]
	public class Test4
	{
		[Test]
		public void TestMethod()
		{
			var q = new NHibernateQuestionRepository().ReadBackgroundQuestion(3);
			foreach (var l in q.Languages) {
				Console.WriteLine(l);
			}
		}
		
		[Test]
		public void a()
		{
			var s = new NHibernateSponsorRepository().Read(96);
			Console.WriteLine(s.ProjectRoundUnit.Name);
		}
		
		[Test]
		public void b()
		{
		}
	}
}
