//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlRepositoryFactory : IRepositoryFactory
	{
		public ILanguageRepository CreateLanguageRepository()
		{
			return new SqlLanguageRepository();
		}
		
		public IDepartmentRepository CreateDepartmentRepository()
		{
			return new SqlDepartmentRepository();
		}
		
		public IProjectRepository CreateProjectRepository()
		{
			return new SqlProjectRepository();
		}
		
		public ISponsorRepository CreateSponsorRepository()
		{
			return new SqlSponsorRepository();
		}
		
		public IReportRepository CreateReportRepository()
		{
			return new SqlReportRepository();
		}
		
		public IAnswerRepository CreateAnswerRepository()
		{
			return new SqlAnswerRepository();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			return new SqlOptionRepository();
		}
		
		public IIndexRepository CreateIndexRepository()
		{
			return new SqlIndexRepository();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new SqlQuestionRepository();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new SqlManagerFunctionRepository();
		}
		
		public IExerciseRepository CreateExerciseRepository()
		{
			return new SqlExerciseRepository();
		}
		
		public IUserRepository CreateUserRepository()
		{
			return new SqlUserRepository();
		}
	}
}
