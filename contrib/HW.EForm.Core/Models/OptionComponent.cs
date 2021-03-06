using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class OptionComponent
	{
		OptionComponentLang selectedOptionComponentLang;
		
		public OptionComponent()
		{
			Languages = new List<OptionComponentLang>();
		}
		
		public int OptionComponentID { get; set; }
		public int ExportValue { get; set; }
		public string Internal { get; set; }
		public int OptionComponentContainerID { get; set; }
		public OptionComponentContainer Container { get; set; }
		public IList<AnswerValue> AnswerValues { get; set; }
		public int SelectedOptionComponentLangID { get; set; }
		public IList<OptionComponentLang> Languages { get; set; }
		
		public OptionComponentLang FirstLanguage {
			get {
				if (Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		public OptionComponentLang SelectedOptionComponentLang {
			get {
				if (selectedOptionComponentLang == null) {
					selectedOptionComponentLang = GetLanguage(SelectedOptionComponentLangID);
				}
				if (selectedOptionComponentLang == null && FirstLanguage != null) {
					selectedOptionComponentLang = FirstLanguage;
				}
				return selectedOptionComponentLang;
			}
			set { selectedOptionComponentLang = value; }
		}
		
		public void AddLanguage(int langID, string text)
		{
			AddLanguage(new OptionComponentLang { LangID = 1, Text = text });
		}
		
		public void AddLanguage(OptionComponentLang ocl)
		{
			Languages.Add(ocl);
		}
		
		public OptionComponentLang GetLanguage(int langID)
		{
			foreach (var l in Languages) {
				if (l.LangID == langID) {
					return l;
				}
			}
			return null;
		}
	}
}
