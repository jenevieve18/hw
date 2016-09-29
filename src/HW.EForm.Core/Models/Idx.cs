using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Index
	{
		public Index()
		{
			Parts = new List<IndexPart>();
		}
		
		public int IdxID { get; set; }
		public string Internal { get; set; }
		public int RequiredAnswerCount { get; set; }
		public bool AllPartsRequired { get; set; }
		public int MaxVal { get; set; }
		public int SortOrder { get; set; }
		public int TargetVal { get; set; }
		public int YellowLow { get; set; }
		public int GreenLow { get; set; }
		public int GreenHigh { get; set; }
		public int YellowHigh { get; set; }
		public int CX { get; set; }
		public string Description { get; set; }
		public IList<IndexPart> Parts { get; set; }
		
		public void AddPart(IndexPart part)
		{
			part.Index = this;
			Parts.Add(part);
		}
	}
}
