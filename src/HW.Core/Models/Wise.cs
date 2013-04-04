//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Wise : BaseModel
	{
		public DateTime LastShown { get; set; }
		public IList<WiseLanguage> Languages { get; set; }
	}
	
	public class WiseLanguage : BaseModel
	{
		public Wise Wise { get; set; }
		public Language Language { get; set; }
		public string WiseName { get; set; }
		public string Owner { get; set; }
	}
}
