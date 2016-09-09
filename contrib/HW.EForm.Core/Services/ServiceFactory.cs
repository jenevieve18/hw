// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Configuration;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class ServiceFactory
	{
		static IRepositoryFactory repoFactory;
		
		public static void SetRepositoryFactory(IRepositoryFactory repoFactory)
		{
			ServiceFactory.repoFactory = repoFactory;
		}
		
		public static FeedbackService CreateFeedbackService()
		{
//			return new FeedbackService(
//				repoFactory.CreateFeedbackRepository(),
//				repoFactory.CreateFeedbackQuestionRepository(),
//				repoFactory.CreateQuestionRepository(),
//				repoFactory.CreateQuestionOptionRepository(),
//				repoFactory.CreateQuestionLangRepository(),
//				repoFactory.CreateWeightedQuestionOptionRepository(),
//				repoFactory.CreateOptionRepository(),
//				repoFactory.CreateOptionComponentsRepository(),
//				repoFactory.CreateOptionComponentRepository(),
//				repoFactory.CreateOptionComponentLangRepository(),
//				repoFactory.CreateProjectRoundUnitRepository(),
//				repoFactory.CreateAnswerValueRepository()
//			);
			return new FeedbackService();
		}
	}
}
