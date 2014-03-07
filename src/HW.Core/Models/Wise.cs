using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Wise : BaseModel
	{
		public virtual DateTime LastShown { get; set; }
		public virtual IList<WiseLanguage> Languages { get; set; }
	}
	
	public class WiseLanguage : BaseModel
	{
		public virtual Wise Wise { get; set; }
		public virtual Language Language { get; set; }
		public virtual string WiseName { get; set; }
		public virtual string Owner { get; set; }
	}
	
	public struct WordsOfWisdom
	{
		public string words;
		public string author;
	}
}
