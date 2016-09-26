using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class OptionType
	{
		public const int SingleChoice = 1;
		public const int FreeText = 2;
		public const int MultiChoice = 3;
		public const int Numeric = 4;
		public const int VAS = 9;
	}
	
	public class Option : BaseModel
	{
		public int OptionType { get; set; }
		public int OptionPlacement { get; set; }
		public string Variablename { get; set; }
		public string Internal { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int InnerWidth { get; set; }
		public int OptionContainerID { get; set; }
		public string BgColor { get; set; }
		public int RangeLow { get; set; }
		public int RangeHigh { get; set; }
		
		public IList<OptionComponents> Components { get; set; }
		public OptionContainer Container { get; set; }
		public virtual OptionComponentLanguage CurrentComponent { get; set; }
	}
	
	public class OptionComponents : BaseModel
	{
		public int OptionComponentsID { get; set; }
		public int OptionComponentID { get; set; }
		public int OptionID { get; set; }
		public int ExportValue { get; set; }
		public int SortOrder { get; set; }
		
		public OptionComponent Component { get; set; }
	}
	
	public class OptionContainer : BaseModel
	{
		public int OptionContainerID { get; set; }
		public string Container { get; set; }
	}
	
	public class OptionComponent : BaseModel
	{
		public int OptionComponentID { get; set; }
		public int ExportValue { get; set; }
		public string Internal { get; set; }
		public int OptionComponentContainerID { get; set; }
		
		public IList<OptionComponentLanguage> Languages { get; set; }
		public OptionComponentContainer Container { get; set; }
		public OptionComponentLanguage CurrentLanguage { get; set; }
	}
	
	public class OptionComponentContainer : BaseModel
	{
		public string Container { get; set; }
	}
	
	public class OptionComponentLanguage : BaseModel
	{
		public int OptionComponentLangID { get; set; }
		public int OptionComponentID { get; set; }
		public int LangID { get; set; }
		public string Text { get; set; }
		public string TextJapaneseUnicode { get; set; }
		
		public OptionComponent Component { get; set; }
		public Language Language { get; set; }
	}
}
