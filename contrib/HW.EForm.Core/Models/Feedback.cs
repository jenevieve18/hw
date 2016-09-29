using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class Feedback
	{
		List<int> chartIDs = new List<int>();
		
		public Feedback()
		{
			Questions = new List<FeedbackQuestion>();
		}
		
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }
		public int SurveyID { get; set; }
		public int Compare { get; set; }
		public int FeedbackTemplateID { get; set; }
		public int NoHardcodedIdxs { get; set; }
		public Survey Survey { get; set; }
		public IList<FeedbackQuestion> Questions { get; set; }
		
		public int CountQuestions()
		{
			int questions = 0;
			foreach (var q in Questions) {
				if (q.HasQuestion) {
					questions ++;
				}
			}
			return questions;
		}
		
		public int CountIndexes()
		{
			int indexes = 0;
			foreach (var q in Questions) {
				if (q.HasIndex) {
					indexes ++;
				}
			}
			return indexes;
		}
		
		public IList<Chart> Charts {
			get {
				var charts = new List<Chart>();
				foreach (var fq in Questions) {
					if (fq.IsPartOfChart && !HasGroupedChart(fq.PartOfChart)) {
						charts.Add(GetQuestions(fq.PartOfChart).ToChart());
					} else if (!fq.IsPartOfChart) {
						charts.Add(fq.Question.ToChart());
					}
				}
				return charts;
			}
		}
		
		public bool HasGroupedChart(int chartID)
		{
			return chartIDs.Contains(chartID);
		}
		
		public IList<FeedbackQuestion> GetQuestions(int chartID)
		{
			chartIDs.Add(chartID);
			return (Questions as List<FeedbackQuestion>).FindAll(x => x.PartOfChart == chartID);
		}
		
		public void AddQuestion(Index index)
		{
			AddQuestion(new FeedbackQuestion { Index = index });
		}
		
		public void AddQuestion(Question question)
		{
			AddQuestion(new FeedbackQuestion { Question = question });
		}
		
		public void AddQuestion(FeedbackQuestion fq)
		{
			fq.Feedback = this;
			Questions.Add(fq);
		}
	}
}
