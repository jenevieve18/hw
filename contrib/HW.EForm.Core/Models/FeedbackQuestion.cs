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
		
		public Chart ToChart()
		{
			var c = new Chart { Title = Question.Internal };
			foreach (var qo in Question.Options) {
				if (qo.Option.IsSlider) {
					foreach (var pru in ProjectRoundUnits) {
						c.Categories.Add(pru.Unit);
					}
				} else {
					foreach (var oc in qo.Option.Components) {
						c.Categories.Add(oc.OptionComponent.Internal);
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
