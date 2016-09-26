using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Repositories
{
	public interface IAnswerValueRepository : IBaseRepository<AnswerValue>
	{
		IList<AnswerValue> FindByQuestionOptionsAndUnit(int questionID, IList<QuestionOption> options, int projectRoundID, int projectRoundUnitID);
	}
}
