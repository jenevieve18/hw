//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IQuestionRepository : IBaseRepository<Question>
	{
		IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID);
		
		IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID);
	}
	
	public class QuestionRepositoryStub : BaseRepositoryStub<Question>, IQuestionRepository
	{
		public IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			var questions = new List<BackgroundQuestion>();
			for (int i = 0; i < 10; i++) {
				var q = new BackgroundQuestion {
					Internal = "Internal " + i,
					Id = i,
					Type = 1
				};
				questions.Add(q);
			}
			return questions;
		}
		
		public IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID)
		{
			var questions = new List<BackgroundQuestion>();
			for (int i = 0; i < 10; i++) {
				var q = new BackgroundQuestion {
					Id = i,
					Internal = "Internal " + i
				};
				questions.Add(q);
			}
			return questions;
		}
	}
}
