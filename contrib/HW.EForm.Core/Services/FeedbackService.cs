// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class FeedbackService
	{
		SqlFeedbackRepository feedbackRepo = new SqlFeedbackRepository();
		SqlFeedbackQuestionRepository feedbackQuestionRepo = new SqlFeedbackQuestionRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		SqlQuestionOptionRepository questionOptionRepo = new SqlQuestionOptionRepository();
		SqlQuestionLangRepository questionLangRepo = new SqlQuestionLangRepository();
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		SqlOptionComponentsRepository optionComponentsRepo = new SqlOptionComponentsRepository();
		SqlOptionComponentRepository optionComponentRepo = new SqlOptionComponentRepository();
		
		public FeedbackService()
		{
		}
		
		public Feedback ReadFeedback(int feedbackID)
		{
			var f = feedbackRepo.Read(feedbackID);
			if (f != null) {
				f.Questions = feedbackQuestionRepo.FindByFeedback(feedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = questionRepo.Read(fq.QuestionID);
					fq.Question.Languages = questionLangRepo.FindByQuestion(fq.QuestionID);
					fq.Question.Options = questionOptionRepo.FindByQuestion(fq.QuestionID);
					foreach (var qo in fq.Question.Options) {
						qo.Option = optionRepo.Read(qo.OptionID);
						qo.Option.Components = optionComponentsRepo.FindByOption(qo.OptionID);
						foreach (var oc in qo.Option.Components) {
							oc.OptionComponent = optionComponentRepo.Read(oc.OptionComponentID);
						}
					}
				}
			}
			return f;
		}
	}
}
