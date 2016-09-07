using System;
	
namespace HW.Core2.Models
{
	public class SponsorProject
	{
		public int SponsorProjectID { get; set; }
		public int SponsorID { get; set; }
		public DateTime? StartDT { get; set; }
		public DateTime? EndDT { get; set; }
		public string ProjectName { get; set; }

		public SponsorProject()
		{
		}
	}
}
