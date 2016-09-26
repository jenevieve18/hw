// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;

namespace HW.EForm.Core.Repositories
{
	public interface IRepositoryFactory
	{
		IFeedbackRepository CreateFeedbackRepository();
		IFeedbackQuestionRepository CreateFeedbackQuestionRepository();
		IQuestionRepository CreateQuestionRepository();
		IQuestionOptionRepository CreateQuestionOptionRepository();
		IQuestionLangRepository CreateQuestionLangRepository();
		IWeightedQuestionOptionRepository CreateWeightedQuestionOptionRepository();
		IOptionRepository CreateOptionRepository();
		IOptionComponentsRepository CreateOptionComponentsRepository();
		IOptionComponentRepository CreateOptionComponentRepository();
		IOptionComponentLangRepository CreateOptionComponentLangRepository();
		IProjectRoundUnitRepository CreateProjectRoundUnitRepository();
		IAnswerValueRepository CreateAnswerValueRepository();
	}
}
