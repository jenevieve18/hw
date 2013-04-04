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

		IList<BackgroundQuestion> FindBackgroundQuestionsWithAnswers(string query, int count);
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
		
		public IList<BackgroundQuestion> FindBackgroundQuestionsWithAnswers(string query, int count)
		{
			var questions = new List<BackgroundQuestion>();
			for (int i = 0; i < 10; i++) {
				var bq = new BackgroundQuestion { Id = i };
				var answers = new List<BackgroundAnswer>();
				for (int j = 0; j < count; j++) {
					answers.Add(
						new BackgroundAnswer {
							Id = j,
							Internal = "Internal" + j
						}
					);
				}
				bq.Answers = answers;
				questions.Add(bq);
			}
			return questions;
		}
	}
}
