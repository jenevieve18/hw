using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IQuestionRepository : IBaseRepository<Question>
	{
		void SaveOrUpdateBackgroundQuestion(BackgroundQuestion q);
		BackgroundQuestion ReadBackgroundQuestion(int bqid);
		IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID);
		IList<BackgroundQuestion> FindAllBackgroundQuestions();
		IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID);
		IList<BackgroundQuestion> FindBackgroundQuestionsWithAnswers(string query, int count);
	}
	
	public class QuestionRepositoryStub : BaseRepositoryStub<Question>, IQuestionRepository
	{
		public QuestionRepositoryStub()
		{
			data.Add(new Question { QuestionID = 1, Internal = "My work is meaningful" });
			data.Add(new Question { QuestionID = 2, Internal = "I learn new things and develop in my daily work" });
			data.Add(new Question { QuestionID = 3, Internal = "I look forward to going to work" });
			data.Add(new Question { QuestionID = 4, Internal = "My immediate superior shows appreciation for my efforts" });
			data.Add(new Question { QuestionID = 5, Internal = "My immediate superior shows confidence in me as an employee " });
			data.Add(new Question { QuestionID = 6, Internal = "My immediate superior gives me the ability to take responsibility in my work " });
			data.Add(new Question { QuestionID = 7, Internal = "I am familiar with the goals at my workplace" });
			data.Add(new Question { QuestionID = 8, Internal = "The goals at my workplace are monitored and evaluated in a good way" });
			data.Add(new Question { QuestionID = 9, Internal = "I know what is expected from me in my work " });
		}
		
		public override Question Read(int id)
		{
			return data.Find(x => x.QuestionID == id);
		}
		
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
		
		public IList<BackgroundQuestion> FindAllBackgroundQuestions()
		{
			return new List<BackgroundQuestion>(
				new BackgroundQuestion[] {
					new BackgroundQuestion { Id = 1, Internal = "Internal 1" },
					new BackgroundQuestion { Id = 2, Internal = "Internal 2" }
				}
			);
		}
		
		public BackgroundQuestion ReadBackgroundQuestion(int bqid)
		{
			throw new NotImplementedException();
		}
		
		public void SaveOrUpdateBackgroundQuestion(BackgroundQuestion q)
		{
			throw new NotImplementedException();
		}
	}
}
