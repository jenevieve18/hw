//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateQuestionRepository : BaseNHibernateRepository<Question>, IQuestionRepository
	{
		public NHibernateQuestionRepository()
		{
		}
		
		public BackgroundQuestion ReadBackgroundQuestion(int bqid)
		{
			return NHibernateHelper.OpenSession("healthWatch").Load<BackgroundQuestion>(bqid);
		}
		
		public IList<BackgroundQuestion> FindAllBackgroundQuestions()
		{
			return NHibernateHelper.OpenSession("healthWatch").CreateCriteria(typeof(BackgroundQuestion)).List<BackgroundQuestion>();
		}
		
		public IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			throw new NotImplementedException();
		}
		
		public IList<BackgroundQuestion> FindLikeBackgroundQuestions(string bqID)
		{
			throw new NotImplementedException();
		}
		
		public IList<BackgroundQuestion> FindBackgroundQuestionsWithAnswers(string query, int count)
		{
			throw new NotImplementedException();
		}
		
		public void SaveOrUpdateBackgroundQuestion(BackgroundQuestion q)
		{
			SaveOrUpdate<BackgroundQuestion>(q, "healthWatch");
		}
	}
}
