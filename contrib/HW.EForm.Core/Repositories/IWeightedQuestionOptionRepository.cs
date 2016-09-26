using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Repositories
{
	public interface IWeightedQuestionOptionRepository : IBaseRepository<WeightedQuestionOption>
	{
		WeightedQuestionOption ReadByQuestion(int questionID);
		WeightedQuestionOption ReadByQuestionAndOption(int questionID, int optionID);
	}
}
