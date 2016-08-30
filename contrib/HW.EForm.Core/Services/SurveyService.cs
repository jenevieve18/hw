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
		SqlSurveyRepository surveyRepo = new SqlSurveyRepository();
		SqlSurveyQuestionRepository surveyQuestionRepo = new SqlSurveyQuestionRepository();
		SqlSurveyQuestionOptionRepository surveyQuestionOptionRepo = new SqlSurveyQuestionOptionRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		
		public SurveyService()
		{
		}

        public IList<Survey> FindAllSurveys()
        {
            return surveyRepo.FindAll();
        }
		
		public Survey ReadSurvey(int surveyID)
		{
			var s = surveyRepo.Read(surveyID);
			s.Questions = surveyQuestionRepo.FindBySurvey(surveyID);
			foreach (var q in s.Questions) {
				q.Question = questionRepo.Read(q.QuestionID);
				q.Options = surveyQuestionOptionRepo.FindBySurveyQuestion(q.SurveyQuestionID);
				foreach (var o in q.Options) {
					o.QuestionOption = questionOptionRepo.Read(o.QuestionOptionID);
					o.QuestionOption.Option = optionRepo.Read(o.QuestionOption.OptionID);
				}
			}
			return s;
		}
	}
}
