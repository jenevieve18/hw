using System;
	
namespace HW.EForm.Core.Models
{
	public class OptionComponents
	{
		public int OptionComponentsID { get; set; }
		public int OptionComponentID { get; set; }
		public int OptionID { get; set; }
		public int ExportValue { get; set; }
		public int SortOrder { get; set; }

		public OptionComponents()
		{
		}
		
//		public OptionComponents(int optionComponentID, string @internal, int langID, string text)
//		{
//			OptionComponentID = optionComponentID;
//			OptionComponent = new OptionComponent { OptionComponentID = optionComponentID, Internal = @internal };
//			OptionComponent.AddLanguage(langID, text);
//		}
		
		public OptionComponent OptionComponent { get; set; }
	}
}
