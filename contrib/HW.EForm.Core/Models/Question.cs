using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class Question
	{
		public int QuestionID { get; set; }
		public string VariableName { get; set; }
		public int OptionsPlacement { get; set; }
		public int FontFamily { get; set; }
		public int FontSize { get; set; }
		public int FontDecoration { get; set; }
		public string FontColor { get; set; }
		public int Underlined { get; set; }
		public int QuestionContainerID { get; set; }
		public string Internal { get; set; }
		public int Box { get; set; }

		public Question()
		{
			Languages = new List<QuestionLang>();
			Options = new List<QuestionOption>();
		}
		
		public IList<QuestionOption> Options { get; set; }
		public IList<QuestionLang> Languages { get; set; }
		public WeightedQuestionOption WeightedQuestionOption { get; set; }
		
		public void AddOption(QuestionOption option)
		{
			Options.Add(option);
		}
		
		public QuestionLang GetLanguage(int langID)
		{
			foreach (var l in Languages) {
				if (l.LangID == langID) {
					return l;
				}
			}
			return null;
		}
		
		
		
		
		
		public IList<ProjectRoundUnit> ProjectRoundUnits { get; set; }
		
		
		
		
		
		public Chart ToChart(bool hasBackground)
		{
			var c = new Chart { Title = GetLanguage(1).Question, HasBackground = hasBackground };
			if (WeightedQuestionOption != null) {
				c.PlotBands.Add(new PlotBand { From = 0, To = WeightedQuestionOption.YellowLow, Color = "rgb(255,168,168)" });
				c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.YellowLow, To = WeightedQuestionOption.GreenLow, Color = "rgb(255,254,190)" });
				c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.GreenLow, To = WeightedQuestionOption.GreenHigh, Color = "rgb(204,255,187)" });
				c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.GreenHigh, To = WeightedQuestionOption.YellowHigh, Color = "rgb(255,254,190)" });
				c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.YellowHigh, To = WeightedQuestionOption.YellowHigh < 100 ? 100 : 101, Color = "rgb(255,254,190)" });
			}
			foreach (var qo in Options) {
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
	}
}
