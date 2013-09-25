using System;

namespace HW.Core.Models
{
	public class XUnit : BaseModel // TODO: XUnit because Unit is a Control in org.aspx
	{
	}
	
	public class UnitCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class UnitCategoryLanguage : BaseModel
	{
		public UnitCategory Category { get; set; }
		public Language Language { get; set; }
		public string CategoryName { get; set; }
		public string CategoryNameJapaneseUnicode { get; set; }
	}
}
