// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Report.Tests.Repositories
{
	public class RepositoryStubFactory : IRepositoryFactory
	{
		public RepositoryStubFactory()
		{
		}
		
		public IFeedbackRepository CreateFeedbackRepository()
		{
			return new FeedbackRepositoryStub();
		}
		
		public IFeedbackQuestionRepository CreateFeedbackQuestionRepository()
		{
			return new FeedbackQuestionRepositoryStub();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new QuestionRepositoryStub();
		}
		
		public IQuestionOptionRepository CreateQuestionOptionRepository()
		{
			return new QuestionOptionRepositoryStub();
		}
		
		public IQuestionLangRepository CreateQuestionLangRepository()
		{
			return new QuestionLangRepositoryStub();
		}
		
		public IWeightedQuestionOptionRepository CreateWeightedQuestionOptionRepository()
		{
			return new WeightedQuestionOptionRepositoryStub();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			return new OptionRepositoryStub();
		}
		
		public IOptionComponentsRepository CreateOptionComponentsRepository()
		{
			return new OptionComponentsRepositoryStub();
		}
		
		public IOptionComponentRepository CreateOptionComponentRepository()
		{
			return new OptionComponentRepositoryStub();
		}
		
		public IOptionComponentLangRepository CreateOptionComponentLangRepository()
		{
			return new OptionComponentLangRepositoryStub();
		}
		
		public IProjectRoundUnitRepository CreateProjectRoundUnitRepository()
		{
			return new ProjectRoundUnitRepositoryStub();
		}
		
		public IAnswerValueRepository CreateAnswerValueRepository()
		{
			return new AnswerValueRepositoryStub();
		}
	}
}
