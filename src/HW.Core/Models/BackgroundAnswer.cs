using System;
using System.Collections.Generic;
using HW.Core.Helpers;

namespace HW.Core.Models
{
	public class BackgroundAnswer : BaseModel
	{
		public BackgroundQuestion BackgroundQuestion { get; set; }
		public string Internal { get; set; }
		public int SortOrder { get; set; }
		public int Value { get; set; }
		public IList<BackgroundAnswerLanguage> Languages { get; set; }
	}
	
	public class BackgroundAnswerLanguage : BaseModel
	{
		public Language Language { get; set; }
		public BackgroundAnswer Answer { get; set; }
	}
}
