//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using NUnit.Framework;

namespace HW.Tests.Repositories
{
	[TestFixture]
	public class RepositoryFactoryTests
	{
		SqlRepositoryFactory f;
		
		[SetUp]
		public void Setup()
		{
			f = new SqlRepositoryFactory();
		}
		
		[Test]
		public void TestValues()
		{
			Assert.IsInstanceOfType(typeof(IAnswerRepository), f.CreateAnswerRepository());
			Assert.IsInstanceOfType(typeof(IDepartmentRepository), f.CreateDepartmentRepository());
			Assert.IsInstanceOfType(typeof(IExerciseRepository), f.CreateExerciseRepository());
			Assert.IsInstanceOfType(typeof(IIndexRepository), f.CreateIndexRepository());
			Assert.IsInstanceOfType(typeof(ILanguageRepository), f.CreateLanguageRepository());
			Assert.IsInstanceOfType(typeof(IManagerFunctionRepository), f.CreateManagerFunctionRepository());
			Assert.IsInstanceOfType(typeof(IOptionRepository), f.CreateOptionRepository());
			Assert.IsInstanceOfType(typeof(IProjectRepository), f.CreateProjectRepository());
			Assert.IsInstanceOfType(typeof(IQuestionRepository), f.CreateQuestionRepository());
			Assert.IsInstanceOfType(typeof(IReportRepository), f.CreateReportRepository());
			Assert.IsInstanceOfType(typeof(ISponsorRepository), f.CreateSponsorRepository());
			Assert.IsInstanceOfType(typeof(IUserRepository), f.CreateUserRepository());
		}
	}
}
