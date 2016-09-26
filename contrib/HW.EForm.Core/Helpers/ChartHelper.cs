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
		public static Chart ToChart(IList<ProjectRoundUnit> units, QuestionOption questionOption)
		{
			var c = new Chart {};
			foreach (var oc in questionOption.Option.Components) {
				c.Categories.Add(oc.OptionComponent.Internal);
			}
			foreach (var u in units) {
				var seriesData = new List<List<double>>();
				var d = new List<double>();
				foreach (var oc in questionOption.Option.Components) {
					var values = questionOption.GetAnswerValues(u.ProjectRoundUnitID, oc.OptionComponentID);
					double v = (double)values.Count / questionOption.AnswerValues.Count * 100;
					d.Add(v);
				}
				seriesData.Add(d);
				c.Series.Add(new Series(u.Unit, seriesData));
			}
			return c;
		}
		
		public static Chart ToChart(this IList<FeedbackQuestion> questions)
		{
			if (AreVASQuestions(questions)) {
				return GetVASChart(questions);
			} else {
				return GetSingleChoiceChart(questions);
			}
		}
		
		public static Chart ToChart(this Question question)
		{
			if (question.HasOnlyOneOption && question.FirstOption.Option.IsVAS) {
				return GetVASChart(question);
			} else {
				return GetSingleChoiceChart(question);
			}
		}
		
		static Chart GetSingleChoiceChart(this IList<FeedbackQuestion> questions)
		{
			var c = new Chart { ID = Guid.NewGuid().ToString(), Title = "", Type = Chart.Column };
			foreach (var fq in questions) {
				if (fq.Question.HasOnlyOneOption && fq.Question.FirstOption.Option.HasOnlyOneComponent) {
					c.Categories.Add(fq.Question.FirstOption.Option.FirstComponent.OptionComponent.SelectedOptionComponentLang.Text);
				}
			}
			IList<ProjectRoundUnit> projectRoundUnits = new List<ProjectRoundUnit>();
			if (questions.Count > 0) {
				projectRoundUnits = questions[0].Question.ProjectRoundUnits;
			}
			foreach (var pru in projectRoundUnits) {
				var d = new List<double>();
				foreach (var fq in questions) {
					var questionPru = fq.Question.GetProjectRoundUnit(pru.ProjectRoundUnitID);
					foreach (var qo in questionPru.Options) {
						foreach (var oc in qo.Option.Components) {
							d.Add(oc.OptionComponent.AnswerValues.Count / (double)pru.AnswerValues.Count * 100);
						}
					}
				}
				var d2 = new List<List<double>>();
				d2.Add(d);
				c.Series.Add(new Series(pru.Unit, d2));
			}
			return c;
		}
		
		static Chart GetVASChart(this IList<FeedbackQuestion> questions)
		{
			var c = new Chart { ID = Guid.NewGuid().ToString(), Title = "", Type = Chart.BoxPlot };
			foreach (var fq in questions) {
				c.Categories.Add(fq.Question.SelectedQuestionLang.Question);
			}
			IList<ProjectRoundUnit> projectRoundUnits = new List<ProjectRoundUnit>();
			if (questions.Count > 0) {
				projectRoundUnits = questions[0].Question.ProjectRoundUnits;
			}
			foreach (var pru in projectRoundUnits) {
				var d2 = new List<List<double>>();
				foreach (var fq in questions) {
					var questionPru = fq.Question.GetProjectRoundUnit(pru.ProjectRoundUnitID);
					var d = new List<double>();
					if (questionPru.HasOnlyOneOption) {
						foreach (var oc in questionPru.FirstOption.Option.Components) {
							foreach (var av in oc.OptionComponent.AnswerValues) {
								d.Add(av.ValueInt);
							}
						}
					}
					d2.Add(d);
				}
				c.Series.Add(new Series(pru.Unit, d2));
			}
			return c;
		}
		
		static bool AreVASQuestions(this IList<FeedbackQuestion> questions)
		{
			foreach (var q in questions) {
				if (q.Question.HasOnlyOneOption && !q.Question.FirstOption.Option.IsVAS) {
					return false;
				}
			}
			return true;
		}
		
		static Chart GetVASChart(this Question question)
		{
			var c = new Chart { ID = question.QuestionID.ToString(), Title = "", Type = Chart.BoxPlot };
			if (question.HasOnlyOneOption) {
				var qo = question.FirstOption;
				if (qo.HasWeightedQuestionOption) {
					c.PlotBands.Add(new PlotBand { From = 0, To = qo.WeightedQuestionOption.YellowLow, Color = "rgb(255,168,168)" });
					c.PlotBands.Add(new PlotBand { From = qo.WeightedQuestionOption.YellowLow, To = qo.WeightedQuestionOption.GreenLow, Color = "rgb(255,254,190)" });
					c.PlotBands.Add(new PlotBand { From = qo.WeightedQuestionOption.GreenLow, To = qo.WeightedQuestionOption.GreenHigh, Color = "rgb(204,255,187)" });
					c.PlotBands.Add(new PlotBand { From = qo.WeightedQuestionOption.GreenHigh, To = qo.WeightedQuestionOption.YellowHigh, Color = "rgb(255,254,190)" });
					c.PlotBands.Add(new PlotBand { From = qo.WeightedQuestionOption.YellowHigh, To = qo.WeightedQuestionOption.YellowHigh < 100 ? 100 : 101, Color = "rgb(255,254,190)" });
				}
				foreach (var pru in question.ProjectRoundUnits) {
					c.Categories.Add(pru.Unit);
				}
				foreach (var pru in question.ProjectRoundUnits) {
					var d2 = new List<List<double>>();
					if (pru.HasOnlyOneOption) {
						var d = new List<double>();
						foreach (var oc in pru.FirstOption.Option.Components) {
							foreach (var av in oc.OptionComponent.AnswerValues) {
								d.Add(av.ValueInt);
							}
						}
						d2.Add(d);
						c.Series.Add(new Series(pru.Unit, d2));
					}
				}
			}
			return c;
		}
		
		static Chart GetSingleChoiceChart(this Question question)
		{
			var c = new Chart { ID = question.QuestionID.ToString(), Title = question.SelectedQuestionLang.Question, Type = Chart.Column };
			foreach (var qo in question.Options) {
				foreach (var oc in qo.Option.Components) {
					c.Categories.Add(oc.OptionComponent.SelectedOptionComponentLang.Text);
				}
			}
			foreach (var pru in question.ProjectRoundUnits) {
				var d = new List<double>();
				foreach (var qo in pru.Options) {
					foreach (var oc in qo.Option.Components) {
						d.Add(oc.OptionComponent.AnswerValues.Count / (double)pru.AnswerValues.Count * 100);
					}
				}
				var d2 = new List<List<double>>();
				d2.Add(d);
				c.Series.Add(new Series(pru.Unit, d2));
			}
			return c;
		}
	}
}
