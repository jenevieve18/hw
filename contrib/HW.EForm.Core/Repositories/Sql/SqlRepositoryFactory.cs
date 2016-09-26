// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;

namespace HW.EForm.Core.Repositories
{
	public class SqlRepositoryFactory : IRepositoryFactory
	{
		public SqlRepositoryFactory()
		{
		}
		
		public IFeedbackRepository CreateFeedbackRepository()
		{
			return new SqlFeedbackRepository();
		}
		
		public IFeedbackQuestionRepository CreateFeedbackQuestionRepository()
		{
			return new SqlFeedbackQuestionRepository();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new SqlQuestionRepository();
		}
		
		public IQuestionOptionRepository CreateQuestionOptionRepository()
		{
			return new SqlQuestionOptionRepository();
		}
		
		public IQuestionLangRepository CreateQuestionLangRepository()
		{
			return new SqlQuestionLangRepository();
		}
		
		public IWeightedQuestionOptionRepository CreateWeightedQuestionOptionRepository()
		{
			return new SqlWeightedQuestionOptionRepository();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			return new SqlOptionRepository();
		}
		
		public IOptionComponentsRepository CreateOptionComponentsRepository()
		{
			return new SqlOptionComponentsRepository();
		}
		
		public IOptionComponentRepository CreateOptionComponentRepository()
		{
			return new SqlOptionComponentRepository();
		}
		
		public IOptionComponentLangRepository CreateOptionComponentLangRepository()
		{
			return new SqlOptionComponentLangRepository();
		}
		
		public IProjectRoundUnitRepository CreateProjectRoundUnitRepository()
		{
			return new SqlProjectRoundUnitRepository();
		}
		
		public IAnswerValueRepository CreateAnswerValueRepository()
		{
			return new SqlAnswerValueRepository();
		}
	}
}
