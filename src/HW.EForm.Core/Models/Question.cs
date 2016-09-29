using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Question
	{
		public Question(Option option)
		{
			Options = new List<QuestionOption>();
			AddOption(option);
		}
		
		public Question()
		{
			Options = new List<QuestionOption>();
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
		public int LimitWidth { get; set; }
		public string FillRemainderWithBgColor { get; set; }
		public int Niner { get; set; }
		public IList<QuestionOption> Options { get; set; }
		
		public bool HasOnlyOneOption {
			get { return Options.Count == 1; }
		}
		
		public bool HasMultipleOptions {
			get { return Options.Count > 1; }
		}
		
		public bool HasOptions {
			get { return Options.Count > 0; }
		}
		
		public QuestionOption FirstOption {
			get {
				if (HasOptions) {
					return Options[0];
				}
				return null;
			}
		}
		
		public void AddOption(Option option)
		{
			AddOption(new QuestionOption { Option = option });
		}
		
		public void AddOption(QuestionOption option)
		{
			option.Question = this;
			Options.Add(option);
		}
	}
}
