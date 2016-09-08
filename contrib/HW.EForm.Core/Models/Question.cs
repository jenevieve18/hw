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
		public IList<QuestionOption> Options { get; set; }
		public IList<QuestionLang> Languages { get; set; }
		public WeightedQuestionOption WeightedQuestionOption { get; set; }
        public QuestionOption FirstOption {
            get {
                if (Options.Count > 0) {
                    return Options[0];
                }
                return null;
            }
        }
		public QuestionLang FirstLanguage {
			get {
				if (Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
//		public int SelectedQuestionLangID { get; set; }
		int selectedQuestionLangID;
		public int SelectedQuestionLangID {
			get { return selectedQuestionLangID; }
			set {
				selectedQuestionLangID = value;
				foreach (var qo in Options) {
					foreach (var qc in qo.Option.Components) {
						qc.OptionComponent.SelectedOptionComponentLangID = value;
					}
				}
			}
		}
		QuestionLang selectedQuestionLang;
		public QuestionLang SelectedQuestionLang {
			get {
				if (selectedQuestionLang == null) {
					selectedQuestionLang = GetLanguage(SelectedQuestionLangID);
				}
				if (selectedQuestionLang == null && FirstLanguage != null) {
					selectedQuestionLang = FirstLanguage;
				}
				return selectedQuestionLang;
			}
			set { selectedQuestionLang = value; }
		}
		public IList<ProjectRoundUnit> ProjectRoundUnits { get; set; }
		public bool HasOnlyOneOption {
			get { return Options.Count == 1; }
		}
		public bool HasMultipleOptions {
			get { return Options.Count > 1; }
		}

		public Question()
		{
			Languages = new List<QuestionLang>();
			Options = new List<QuestionOption>();
			ProjectRoundUnits = new List<ProjectRoundUnit>();
		}
		
//		public Question(int questionID, string @internal)
//		{
//			this.QuestionID = questionID;
//			this.Internal = @internal;
//		}
		
		public void AddLanguage(int langID, string question)
		{
			AddLanguage(new QuestionLang { LangID = langID, Question = question });
		}
		
		public void AddLanguage(QuestionLang ql)
		{
			Languages.Add(ql);
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
		
		public void AddOption(Option o)
		{
			AddOption(new QuestionOption { Option = o });
		}
		
		public void AddOption(QuestionOption qo)
		{
			Options.Add(qo);
		}
		
		public IList<Question> ToQuestions()
		{
			var questions = new List<Question>();
			foreach (var qo in Options) {
				var q = new Question();
				q.Languages = new List<QuestionLang>(
					new [] {
						new QuestionLang { LangID = 1, Question = qo.Option.Components[0].OptionComponent.GetLanguage(1).Text }
					}
				);
				q.Options.Add(qo);
				q.ProjectRoundUnits = ProjectRoundUnits;
				foreach (var pru in q.ProjectRoundUnits) {
					pru.Options = q.Options;
				}
				questions.Add(q);
			}
			return questions;
		}
		
//		public List<Chart> ToCharts(bool hasBackground)
//		{
//			var charts = new List<Chart>();
//			
//			foreach (var qo in Options) {
//				var c = new Chart { Title = GetLanguage(1).Question, HasBackground = hasBackground };
//				if (WeightedQuestionOption != null) {
//					c.PlotBands.Add(new PlotBand { From = 0, To = WeightedQuestionOption.YellowLow, Color = "rgb(255,168,168)" });
//					c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.YellowLow, To = WeightedQuestionOption.GreenLow, Color = "rgb(255,254,190)" });
//					c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.GreenLow, To = WeightedQuestionOption.GreenHigh, Color = "rgb(204,255,187)" });
//					c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.GreenHigh, To = WeightedQuestionOption.YellowHigh, Color = "rgb(255,254,190)" });
//					c.PlotBands.Add(new PlotBand { From = WeightedQuestionOption.YellowHigh, To = WeightedQuestionOption.YellowHigh < 100 ? 100 : 101, Color = "rgb(255,254,190)" });
//				}
//				if (qo.Option.IsVAS) {
//					foreach (var pru in ProjectRoundUnits) {
//						c.Categories.Add(pru.Unit);
//					}
//				} else {
//					foreach (var oc in qo.Option.Components) {
//						c.Categories.Add(oc.OptionComponent.GetLanguage(1).Text);
//					}
//				}
//				foreach (var pru in ProjectRoundUnits) {
//					var d = new List<double>();
//					foreach (var qo2 in pru.Options) {
//						if (qo2.Option.IsVAS && qo2.OptionID == qo.OptionID) {
//							foreach (var oc in qo2.Option.Components) {
//								foreach (var av in oc.OptionComponent.AnswerValues) {
//									d.Add(av.ValueInt);
//								}
//							}
//						} else if (qo2.OptionID == qo.OptionID) {
//							foreach (var oc in qo2.Option.Components) {
//								d.Add(oc.OptionComponent.AnswerValues.Count / (double)pru.AnswerValues.Count * 100);
//							}
//						}
//					}
//					c.Series.Add(new Series(pru.Unit, d));
//				}
//				charts.Add(c);
//			}
//			
//			return charts;
//		}
	}
	
	
	
	
	
	
	
	
	public class GroupedQuestions
	{
		public List<Question> Questions { get; set; }
		
		public GroupedQuestions()
		{
			Questions = new System.Collections.Generic.List<Question>();
		}
	}
}
