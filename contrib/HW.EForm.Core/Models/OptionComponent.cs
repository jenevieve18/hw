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

		public OptionComponent()
		{
		}
		
		public OptionComponent(string @internal)
		{
			this.Internal = @internal;
		}
		
		IList<OptionComponentLang> languages;
		
		public IList<OptionComponentLang> Languages {
			get {
				if (languages == null) {
					OnLanguagesSet(null);
				}
				return languages;
			}
			set { languages = value; }
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
		
		public event EventHandler LanguagesSet;
		
		protected virtual void OnLanguagesSet(EventArgs e)
		{
			if (LanguagesSet != null) {
				LanguagesSet(this, e);
			}
		}
	}
}
