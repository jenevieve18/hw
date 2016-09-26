using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.EForm.Core.Repositories
{
	public interface IQuestionLangRepository : IBaseRepository<QuestionLang>
	{
		IList<QuestionLang> FindByQuestion(int questionID);
	}
}
