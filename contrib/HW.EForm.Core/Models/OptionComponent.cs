using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class OptionComponent
	{
		public int OptionComponentID { get; set; }
		public int ExportValue { get; set; }
		public string Internal { get; set; }
		public int OptionComponentContainerID { get; set; }

		public OptionComponent() : this("")
		{
		}
		
		public OptionComponent(string @internal)
		{
			this.Internal = @internal;
			Languages = new List<OptionComponentLang>();
		}
		
		public IList<AnswerValue> AnswerValues { get; set; }
		
		public void AddLanguage(int langID, string text)
		{
			AddLanguage(new OptionComponentLang { LangID = langID, Text = text});
		}
		
		public void AddLanguage(OptionComponentLang language)
		{
			Languages.Add(language);
		}
		
		public IList<OptionComponentLang> Languages { get; set; }
		
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
