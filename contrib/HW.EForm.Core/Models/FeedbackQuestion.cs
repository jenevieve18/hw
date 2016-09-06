using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class FeedbackQuestion
	{
		public int FeedbackQuestionID { get; set; }
		public int FeedbackID { get; set; }
		public int QuestionID { get; set; }

		public Question Question { get; set; }
		public Feedback Feedback { get; set; }
		
		public Chart ToChart(bool hasBackground)
		{
			var c = new Chart { Title = Question.GetLanguage(1).Question, HasBackground = hasBackground };
			if (Question.WeightedQuestionOption != null) {
				c.PlotBands.Add(new PlotBand { From = 0, To = Question.WeightedQuestionOption.YellowLow, Color = "rgb(255,168,168)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.YellowLow, To = Question.WeightedQuestionOption.GreenLow, Color = "rgb(255,254,190)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.GreenLow, To = Question.WeightedQuestionOption.GreenHigh, Color = "rgb(204,255,187)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.GreenHigh, To = Question.WeightedQuestionOption.YellowHigh, Color = "rgb(255,254,190)" });
				c.PlotBands.Add(new PlotBand { From = Question.WeightedQuestionOption.YellowHigh, To = Question.WeightedQuestionOption.YellowHigh < 100 ? 100 : 101, Color = "rgb(255,254,190)" });
			}
			foreach (var qo in Question.Options) {
				if (qo.Option.IsSlider) {
					foreach (var pru in ProjectRoundUnits) {
						c.Categories.Add(pru.Unit);
					}
				} else {
					foreach (var oc in qo.Option.Components) {
						c.Categories.Add(oc.OptionComponent.GetLanguage(1).Text);
					}
				}
			}
			foreach (var pru in ProjectRoundUnits) {
				var d = new List<double>();
				foreach (var qo in pru.Options) {
					if (qo.Option.IsSlider) {
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
		
		public IList<ProjectRoundUnit> ProjectRoundUnits { get; set; }
	}
}
