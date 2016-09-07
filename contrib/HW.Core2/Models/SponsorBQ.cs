using System;
	
namespace HW.Core2.Models
{
	public class SponsorBQ
	{
		public int SponsorBQID { get; set; }
		public int SponsorID { get; set; }
		public int BQID { get; set; }
		public int Forced { get; set; }
		public int SortOrder { get; set; }
		public int Hidden { get; set; }
		public int Fn { get; set; }
		public int InGrpAdmin { get; set; }
		public int IncludeInTreatmentReq { get; set; }
		public int Organize { get; set; }

		public SponsorBQ()
		{
		}
	}
}
