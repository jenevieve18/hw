// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class SurveyService
	{
		SqlSurveyRepository ssr = new SqlSurveyRepository();
		SqlSurveyQuestionRepository ssqr = new SqlSurveyQuestionRepository();
		SqlSurveyQuestionOptionRepository ssqor = new SqlSurveyQuestionOptionRepository();
		
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		SqlQuestionOptionRepository sqor = new SqlQuestionOptionRepository();
		
		SqlOptionRepository sor = new SqlOptionRepository();
		
		public SurveyService()
		{
		}

        public IList<Survey> FindAllSurveys()
        {
            return ssr.FindAll();
        }
		
		public Survey ReadSurvey(int surveyID)
		{
			var s = ssr.Read(surveyID);
			s.Questions = ssqr.FindBySurvey(surveyID);
			foreach (var q in s.Questions) {
				q.Question = sqr.Read(q.QuestionID);
				q.Options = ssqor.lalala(q.SurveyQuestionID);
				foreach (var o in q.Options) {
					o.QuestionOption = sqor.Read(o.QuestionOptionID);
					o.QuestionOption.Option = sor.Read(o.QuestionOption.OptionID);
				}
			}
			return s;
		}
	}
}
