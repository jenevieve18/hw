using System;
	
namespace HW.EForm.Core.Models
{
	public class ReportPartComponent
	{
		public ReportPartComponent()
		{
		}
		
		public int ReportPartComponentID { get; set; }
		public int ReportPartID { get; set; }
		public ReportPart ReportPart { get; set; }
		public int IdxID { get; set; }
		public Index Index { get; set; }
		public int WeightedQuestionOptionID { get; set; }
		public int SortOrder { get; set; }

		public bool HasIndex {
			get { return Index != null; }
		}
	}
}
