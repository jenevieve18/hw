using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Repositories
{
	public interface IQuestionOptionRepository : IBaseRepository<QuestionOption>
	{
		IList<QuestionOption> FindByQuestion(int questionID);
		IList<QuestionOption> FindByQuestionAndOption(int questionID, int optionID);
	}
}
