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
		SqlFeedbackQuestionRepository sfqr = new SqlFeedbackQuestionRepository();
		
		SqlQuestionRepository sqr = new SqlQuestionRepository();
		
		public FeedbackService()
		{
		}
		
		public Feedback ReadFeedback(int feedbackID)
		{
			var f = feedbackRepo.Read(feedbackID);
			if (f != null) {
				f.Questions = sfqr.FindByFeedback(feedbackID);
				foreach (var fq in f.Questions) {
					fq.Question = sqr.Read(fq.QuestionID);
				}
			}
			return f;
		}
	}
}
