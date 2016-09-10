using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class Question
	{
		QuestionLang selectedQuestionLang;
		int selectedQuestionLangID;
		
		public Question()
		{
			Languages = new List<QuestionLang>();
			Options = new List<QuestionOption>();
			ProjectRoundUnits = new List<ProjectRoundUnit>();
		}
		
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
		
		public bool AllOptionsAreTheSame
		{
			get {
				int type = 0;
				int i = 0;
				foreach (var qo in Options) {
					if (i == 0) {
						type = qo.Option.OptionType;
					} else {
						if (type != qo.Option.OptionType) {
							return false;
						}
					}
					i++;
				}
				return true;
			}
		}
		
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
		
		public bool ContainsProjectRoundUnit(int projectRoundUnitID)
		{
			return GetProjectRoundUnit(projectRoundUnitID) != null;
		}
		
		public ProjectRoundUnit GetProjectRoundUnit(int projectRoundUnitID)
		{
			foreach (var pru in ProjectRoundUnits) {
				if (pru.ProjectRoundUnitID == projectRoundUnitID) {
					return pru;
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
		
		public override string ToString()
		{
			if (SelectedQuestionLang != null) {
				return SelectedQuestionLang.Question;
			} else {
				return Internal;
			}
		}
	}
}
