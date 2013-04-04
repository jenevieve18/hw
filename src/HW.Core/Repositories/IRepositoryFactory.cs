//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IRepositoryFactory
	{
		IExerciseRepository CreateExerciseRepository();
		
		ILanguageRepository CreateLanguageRepository();
		
		IDepartmentRepository CreateDepartmentRepository();
		
		IProjectRepository CreateProjectRepository();
		
		ISponsorRepository CreateSponsorRepository();
		
		IReportRepository CreateReportRepository();
		
		IAnswerRepository CreateAnswerRepository();
		
		IOptionRepository CreateOptionRepository();
		
		IIndexRepository CreateIndexRepository();
		
		IQuestionRepository CreateQuestionRepository();
		
		IManagerFunctionRepository CreateManagerFunctionRepository();
		
		IUserRepository CreateUserRepository();
	}
	
	public class RepositoryFactoryStub : IRepositoryFactory
	{
		public IExerciseRepository CreateExerciseRepository()
		{
			return new ExerciseRepositoryStub();
		}
		
		public ILanguageRepository CreateLanguageRepository()
		{
			return new LanguageRepositoryStub();
		}
		
		public IDepartmentRepository CreateDepartmentRepository()
		{
			return new DepartmentRepositoryStub();
		}
		
		public IProjectRepository CreateProjectRepository()
		{
			return new ProjectRepositoryStub();
		}
		
		public ISponsorRepository CreateSponsorRepository()
		{
			return new SponsorRepositoryStub();
		}
		
		public IReportRepository CreateReportRepository()
		{
			return new ReportRepositoryStub();
		}
		
		public IAnswerRepository CreateAnswerRepository()
		{
			return new AnswerRepositoryStub();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			return new OptionRepositoryStub();
		}
		
		public IIndexRepository CreateIndexRepository()
		{
			return new IndexRepositoryStub();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new QuestionRepositoryStub();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new ManagerFunctionRepositoryStub();
		}
		
		public IUserRepository CreateUserRepository()
		{
			return new UserRepositoryStub();
		}
	}
}
