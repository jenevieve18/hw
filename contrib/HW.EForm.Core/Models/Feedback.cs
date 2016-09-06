using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class Feedback
	{
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }

		public Feedback()
		{
		}
		
		public IList<FeedbackQuestion> Questions { get; set; }
		
		
		
		public Chart ToChart(bool hasBackground)
		{
			var c = new Chart { Title = FeedbackText, HasBackground = hasBackground };
			foreach (var fq in Questions) {
				c.Categories.Add(fq.Question.GetLanguage(1).Question);
				var d = new List<double>();
				foreach (var qo in fq.Question.Options) {
					if (qo.Option.IsSlider) {
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
	}
}
