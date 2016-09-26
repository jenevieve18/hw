using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class IndexPart
	{
		public IndexPart()
		{
			Components = new List<IndexPartComponent>();
		}
		
		public int IdxPartID { get; set; }
		public int IdxID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int OtherIdxID { get; set; }
		public int Multiple { get; set; }
		public IList<IndexPartComponent> Components { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public bool HasQuestion {
			get { return Question != null; }
		}
		public bool HasOption {
			get { return Option != null; }
		}
	}
}
