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
			AnswerValues = new List<AnswerValue>();
		}
		
		public IList<QuestionOption> Options { get; set; }
		public IList<QuestionLang> Languages { get; set; }
		public IList<AnswerValue> AnswerValues { get; set; }
		
		public void SetAnswerValuesForComponents()
		{
			foreach (var qo in Options) {
				foreach (var oc in qo.Option.Components) {
					oc.OptionComponent.AnswerValues = GetAnswerValuesForComponent(oc.OptionComponentID);
				}
			}
		}
		
		List<AnswerValue> GetAnswerValuesForComponent(int optionComponentID)
		{
			var answerValues = new List<AnswerValue>();
			foreach (var av in AnswerValues) {
				if (av.ValueInt == optionComponentID) {
					answerValues.Add(av);
				}
			}
			return answerValues;
		}
		
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
		
		public void AddAnswerValue(Answer answer, int optionID, int valueInt)
		{
			AddAnswerValue(new AnswerValue { Answer = answer, OptionID = optionID, ValueInt = valueInt });
		}
		
		public void AddAnswerValue(AnswerValue av)
		{
			AnswerValues.Add(av);
		}
		
		public void AddLanguage(int langID, string question)
		{
			AddLanguage(new QuestionLang { LangID = langID, Question = question });
		}
		
		public void AddLanguage(QuestionLang lang)
		{
			Languages.Add(lang);
		}
	}
}
