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
		SqlQuestionLangRepository questionLangRepo = new SqlQuestionLangRepository();
		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		
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
			foreach (var sq in s.Questions) {
				sq.Question = questionRepo.Read(sq.QuestionID);
				sq.Question.Languages = questionLangRepo.FindByQuestion(sq.QuestionID);
				sq.Options = surveyQuestionOptionRepo.FindBySurveyQuestion(sq.SurveyQuestionID);
				foreach (var sqo in sq.Options) {
					sqo.QuestionOption = questionOptionRepo.Read(sqo.QuestionOptionID);
					sqo.QuestionOption.Option = optionRepo.Read(sqo.QuestionOption.OptionID);
					sqo.QuestionOption.Option.Components = optionComponentsRepo.FindByOption(sqo.QuestionOption.OptionID);
					foreach (var oc in sqo.QuestionOption.Option.Components) {
						oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
					}
				}
			}
			return s;
		}
	}
}
