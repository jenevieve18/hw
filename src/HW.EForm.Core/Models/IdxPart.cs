using System;
	
namespace HW.EForm.Core.Models
{
	public class IndexPart
	{
		public IndexPart()
		{
		}
		
		public int IdxPartID { get; set; }
		public int IdxID { get; set; }
		public Index Index { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int OtherIdxID { get; set; }
		public int Multiple { get; set; }
		public QuestionOption QuestionOption { get; set; }
	}
}
