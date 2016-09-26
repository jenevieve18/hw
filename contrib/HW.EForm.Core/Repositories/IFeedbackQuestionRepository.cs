using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Repositories
{
	public interface IFeedbackQuestionRepository : IBaseRepository<FeedbackQuestion>
	{
		IList<FeedbackQuestion> FindByFeedback(int feedbackID);
		IList<FeedbackQuestion> FindByQuestions(int feedbackID, int[] questionIDs);
	}
}
