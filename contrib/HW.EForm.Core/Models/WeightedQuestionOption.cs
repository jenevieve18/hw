using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class WeightedQuestionOption
	{
		public WeightedQuestionOption()
		{
			Languages = new List<WeightedQuestionOptionLang>();
		}
		
		public int WeightedQuestionOptionID { get; set; }
		public string Internal { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public int TargetVal { get; set; }
		public int YellowLow { get; set; }
		public int GreenLow { get; set; }
		public int GreenHigh { get; set; }
		public int YellowHigh { get; set; }
		public int SortOrder { get; set; }
		
		public IList<WeightedQuestionOptionLang> Languages { get; set; }
		
		public void AddLanguage(WeightedQuestionOptionLang lang)
		{
//			lang.WeightedQuestionOption = this;
			Languages.Add(lang);
		}
	}
}
