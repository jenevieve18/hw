// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using NUnit.Framework;

namespace HW.EForm.Report.Tests.Repositories
{
	[TestFixture]
	public class QuestionRepositoryTests
	{
		[Test]
		public void TestMethod()
		{
		}
	}
	
	public class QuestionRepositoryStub : BaseRepositoryStub<Question>, IQuestionRepository
	{
		List<Question> questions = new List<Question>();
		
		public QuestionRepositoryStub()
		{
			for (int i = 1; i <= 100; i++) {
				questions.Add(new Question { QuestionID = i, Internal = "Question" + i });
			}
		}
		
		public override Question Read(int id)
		{
			return questions.Find(x => x.QuestionID == id);
		}
		
		public override IList<Question> FindAll()
		{
			return questions;
		}
	}
	
	public class QuestionOptionRepositoryStub : BaseRepositoryStub<QuestionOption>, IQuestionOptionRepository
	{
		public QuestionOptionRepositoryStub()
		{
			for (int i = 1; i <= 11; i++) {
				items.Add(new QuestionOption { QuestionID = i, OptionID = 1 });
			}
		}
		
		public IList<QuestionOption> FindByQuestion(int questionID)
		{
			return items.FindAll(x => x.QuestionID == questionID);
		}
		
		public IList<QuestionOption> FindByQuestionAndOption(int questionID, int optionID)
		{
			return items.FindAll(x => x.QuestionID == questionID && x.OptionID == optionID);
		}
	}
	
	public class QuestionLangRepositoryStub : BaseRepositoryStub<QuestionLang>, IQuestionLangRepository
	{
		public QuestionLangRepositoryStub()
		{
			for (int i = 1; i <= 100; i++) {
				for (int j = 1; j <= 2; j++) {
					items.Add(new QuestionLang { QuestionID = i, LangID = j, Question = "Question" + i + "Lang" + j });
				}
			}
		}
		
		public IList<QuestionLang> FindByQuestion(int questionID)
		{
			return items.FindAll(x => x.QuestionID == questionID);
		}
	}
	
	public class BaseRepositoryStub<T> : IBaseRepository<T>
	{
		protected List<T> items = new List<T>();
		
		public BaseRepositoryStub()
		{
		}
		
		public virtual void Save(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Update(T t, int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual void Delete(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual IList<T> FindAll()
		{
			return items;
		}
	}
}
