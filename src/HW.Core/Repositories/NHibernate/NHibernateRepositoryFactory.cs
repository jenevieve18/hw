//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateRepositoryFactory : IRepositoryFactory
	{
		public NHibernateRepositoryFactory()
		{
		}
		
		public IExerciseRepository CreateExerciseRepository()
		{
			return new NHibernateExerciseRepository();
		}
		
		public ILanguageRepository CreateLanguageRepository()
		{
			return new NHibernateLanguageRepository();
		}
		
		public IDepartmentRepository CreateDepartmentRepository()
		{
			return new NHibernateDepartmentRepository();
		}
		
		public IProjectRepository CreateProjectRepository()
		{
			return new NHibernateProjectRepository();
		}
		
		public ISponsorRepository CreateSponsorRepository()
		{
			return new NHibernateSponsorRepository();
		}
		
		public IReportRepository CreateReportRepository()
		{
			return new NHibernateReportRepository();
		}
		
		public IAnswerRepository CreateAnswerRepository()
		{
			throw new NotImplementedException();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			throw new NotImplementedException();
		}
		
		public IIndexRepository CreateIndexRepository()
		{
			throw new NotImplementedException();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new NHibernateQuestionRepository();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new NHibernateManagerFunctionRepository();
		}
		
		public IUserRepository CreateUserRepository()
		{
			return new NHibernateUserRepository();
		}
	}
}
