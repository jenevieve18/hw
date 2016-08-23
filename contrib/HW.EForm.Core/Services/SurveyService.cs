// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class SurveyService
	{
		SqlSurveyRepository sr = new SqlSurveyRepository();
		SqlSurveyQuestionRepository sqr = new SqlSurveyQuestionRepository();
		SqlSurveyQuestionOptionRepository sqor = new SqlSurveyQuestionOptionRepository();
		
		SqlQuestionRepository qr = new SqlQuestionRepository();
		SqlQuestionOptionRepository qor = new SqlQuestionOptionRepository();
		
		SqlOptionRepository or = new SqlOptionRepository();
		
		public SurveyService()
		{
		}
		
		public Survey ReadSurvey(int surveyID)
		{
			var s = sr.Read(surveyID);
			s.Questions = sqr.FindBySurvey(surveyID);
			foreach (var q in s.Questions) {
				q.Question = qr.Read(q.QuestionID);
				q.Options = sqor.lalala(q.SurveyQuestionID);
				foreach (var o in q.Options) {
					o.QuestionOption = qor.Read(o.QuestionOptionID);
					o.QuestionOption.Option = or.Read(o.QuestionOption.OptionID);
				}
			}
			return s;
		}
	}
}
