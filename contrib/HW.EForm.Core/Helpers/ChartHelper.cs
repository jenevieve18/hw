// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Helpers
{
	public static class ChartHelper
	{
		public static Chart ToChart(this GroupedQuestions groupQuestions, bool hasBackground)
		{
			var c = new Chart { Title = "", HasBackground = hasBackground };
			foreach (var q in groupQuestions.Questions) {
				c.Categories.Add(q.GetLanguage(1).Question);
				var d = new List<double>();
				foreach (var qo in q.Options) {
					if (qo.Option.IsVAS) {
						foreach (var oc in qo.Option.Components) {
							foreach (var av in oc.OptionComponent.AnswerValues) {
								d.Add(av.ValueInt);
							}
						}
					}
				}
				c.Series.Add(new Series(q.GetLanguage(1).Question, d));
			}
			return c;
		}
		
		public static Chart ToChart(this Feedback feedback, bool hasBackground)
		{
			var c = new Chart { Title = feedback.FeedbackText, HasBackground = hasBackground };
			foreach (var fq in feedback.Questions) {
				c.Categories.Add(fq.Question.GetLanguage(1).Question);
				var d = new List<double>();
				foreach (var qo in fq.Question.Options) {
					if (qo.Option.IsVAS) {
						foreach (var oc in qo.Option.Components) {
							foreach (var av in oc.OptionComponent.AnswerValues) {
								d.Add(av.ValueInt);
							}
						}
					}
				}
				c.Series.Add(new Series(fq.Question.GetLanguage(1).Question, d));
			}
			return c;
		}
		
		public static Chart ToChart(this Question Question, bool hasBackground)
		{
//			var c = new Chart { Title = Question.GetLanguage(1).Question, HasBackground = hasBackground };
			var c = new Chart { Title = Question.SelectedQuestionLang.Question, HasBackground = hasBackground };
			if (Question.WeightedQuestionOption != null) {
				c.PlotBands.Add(new PlotBand { From = 0, To = Question.WeightedQuestionOption.YellowLow, Color = "rgb(255,168,168)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.YellowLow, To = Question.WeightedQuestionOption.GreenLow, Color = "rgb(255,254,190)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.GreenLow, To = Question.WeightedQuestionOption.GreenHigh, Color = "rgb(204,255,187)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.GreenHigh, To = Question.WeightedQuestionOption.YellowHigh, Color = "rgb(255,254,190)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.YellowHigh, To = Question.WeightedQuestionOption.YellowHigh < 100 ? 100 : 101, Color = "rgb(255,254,190)" });
			}
			foreach (var qo in Question.Options) {
				if (qo.Option.IsVAS) {
					foreach (var pru in Question.ProjectRoundUnits) {
						c.Categories.Add(pru.Unit);
					}
				} else {
					foreach (var oc in qo.Option.Components) {
//						c.Categories.Add(oc.OptionComponent.GetLanguage(1).Text);
						c.Categories.Add(oc.OptionComponent.SelectedOptionComponentLang.Text);
					}
				}
			}
			foreach (var pru in Question.ProjectRoundUnits) {
				var d = new List<double>();
				foreach (var qo in pru.Options) {
					if (qo.Option.IsVAS) {
						foreach (var oc in qo.Option.Components) {
							foreach (var av in oc.OptionComponent.AnswerValues) {
								d.Add(av.ValueInt);
							}
						}
					} else {
						foreach (var oc in qo.Option.Components) {
							d.Add(oc.OptionComponent.AnswerValues.Count / (double)pru.AnswerValues.Count * 100);
						}
					}
				}
				c.Series.Add(new Series(pru.Unit, d));
			}
			return c;
		}
	}
}
