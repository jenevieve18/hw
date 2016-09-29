using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class BackgroundQuestion : BaseModel
	{
		public string Internal { get; set; }
		public int Type { get; set; }
		public string DefaultValue { get; set; }
		public int Comparison { get; set; }
		public string Variable { get; set; }
		public bool Restricted { get; set; }
		public IList<BackgroundQuestionLanguage> Languages { get; set; }
		public IList<BackgroundAnswer> Answers { get; set; }
		public string InternalAggregate { get; set; }
	}
	
	public class BackgroundQuestionLanguage : BaseModel
	{
		public Language Language { get; set; }
		public BackgroundQuestion BackgroundQuestion { get; set; }
		public string Question { get; set; }
	}
	
	public class BackgroundQuestionVisibility : BaseModel
	{
		public BackgroundQuestion Question { get; set; }
		public BackgroundAnswer Answer { get; set; }
		public BackgroundQuestion Child { get; set; }
	}
}
