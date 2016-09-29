using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IAnswerRepository : IBaseRepository<Answer>
	{
//		void UpdateAnswer(int projectRoundUnitID, int projectRoundUserID);
//		
//		Answer ReadByKey(string key);
//		
//		Answer ReadByQuestionAndOption(int answerID, int questionID, int optionID);
//		
//		Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString);
//		
//		Answer ReadMinMax(string groupBy, int questionID, int optionID, int yearFrom, int yearTO, string sortString);
//
//		int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString);
//		
//		int CountByProject(int projectRoundUserID, int yearFrom, int yearTo);
//		
//		int CountByDate(int yearFrom, int yearTo, string sortString);
//		
//		IList<Answer> FindByProjectRound(int projectRoundID);
//		
//		IList<Answer> FindByQuestionAndOptionGrouped(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString);
//		
//		IList<Answer> FindByQuestionAndOptionJoinedAndGrouped(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo);
//		
//		IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo);
//		
//		IList<BackgroundAnswer> FindBackgroundAnswers(int bqID);
//		
//		IList<Answer> FindByQuestionAndOptionWithYearSpan(int questionID, int optionID, int yearFrom, int yearTo);
	}
	
	public class AnswerRepositoryStub : BaseRepositoryStub<Answer>, IAnswerRepository
	{
		public AnswerRepositoryStub()
		{
			data.Add(new Answer { Id = 1, ProjectRoundUserID = 1, ProjectRoundUnitID = 1 });
		}
		
		public IList<BackgroundAnswer> FindBackgroundAnswers(int bqID)
		{
			var answers = new List<BackgroundAnswer>();
			for (int i = 0; i < 10; i++) {
				var a = new BackgroundAnswer {
					Id = i,
					Internal = "Internal " + i
				};
				answers.Add(a);
			}
			return answers;
		}
		
		public void UpdateAnswer(int projectRoundUnitID, int projectRoundUserID)
		{
		}
		
		public IList<Answer> FindByProjectRound(int projectRoundID)
		{
			throw new NotImplementedException();
		}
		
		public IList<Answer> FindByQuestionAndOptionGrouped(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 2; i++) {
				var a = new Answer {
					DT = 1,
					AverageV = r.Next(20, 100),
					CountV = 10,
					StandardDeviation = 11.3f
				};
				answers.Add(a);
			}
			return answers;
		}
		
		static int x = 1;
		Random r = new Random();
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped(string @join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 10; i++) {
				var a = new Answer {
					DT = 1,
					AverageV = r.Next(0, 100),
					CountV = 10,
					StandardDeviation = 11.3f
				};
				answers.Add(a);
			}
			x += 2;
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string @join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 10; i++) {
				var a = new Answer {
					DT = 1,
					AverageV = r.Next(0, 100),
					CountV = 10,
					StandardDeviation = 11.3f
				};
//				a.Values = new List<AnswerValue>();
				a.Values = new List<IValue>();
				for (int j = 0; j < 10; j++) {
					a.Values.Add(new AnswerValue { ValueDecimal = r.Next(0, 100), ValueInt = r.Next(0, 100) });
				}
				answers.Add(a);
			}
			x += 2;
			return answers;
		}
		
		public Answer ReadByKey(string key)
		{
			return new Answer() {
				Id = 1,
				ProjectRoundUser = new ProjectRoundUser { Id = 1 }
			};
		}
		
		public int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString)
		{
			return 10;
		}
		
		public int CountByProject(int projectRoundUserID, int yearFrom, int yearTo)
		{
			return 10;
		}
		
		public IList<Answer> FindByQuestionAndOptionWithYearSpan(int questionID, int optionID, int yearFrom, int yearTo)
		{
			var answers = new List<Answer>();
			for (int i = 0; i < 10; i++) {
				var a = new Answer() {
					SomeString = "Some String " + i,
					Average = 44f
				};
				answers.Add(a);
			}
			return answers;
		}
		
		public Answer ReadByQuestionAndOption(int answerID, int questionID, int optionID)
		{
			var a = new Answer();
//			a.Values = new List<AnswerValue>(
			a.Values = new List<IValue>(
				new AnswerValue[] {
					new AnswerValue { Answer = a, ValueInt = 10 }
				}
			);
			return a;
		}
		
		public Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString)
		{
			return new Answer() {
				DummyValue1 = 10,
				DummyValue2 = 10,
				DummyValue3 = 100
			};
		}
		
		public Answer ReadMinMax(string groupBy, int questionID, int optionID, int yearFrom, int yearTO, string sortString)
		{
			return new Answer() {
				Max = 12,
				Min = 100
			};
		}
		
		public int CountByDate(int yearFrom, int yearTo, string sortString)
		{
			return 10;
		}
	}
}
