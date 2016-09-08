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
			questions.Add(new Question { QuestionID = 1, Internal = "sex" });
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
		public IList<QuestionOption> FindByQuestion(int questionID)
		{
			throw new NotImplementedException();
		}
		
		public IList<QuestionOption> FindByQuestionAndOption(int questionID, int optionID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class QuestionLangRepositoryStub : BaseRepositoryStub<QuestionLang>, IQuestionLangRepository
	{
		public QuestionLangRepositoryStub()
		{
			items.Add(new QuestionLang { QuestionID = 1, LangID = 1, Question = "" });
			items.Add(new QuestionLang { QuestionID = 1, LangID = 2, Question = "" });
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
