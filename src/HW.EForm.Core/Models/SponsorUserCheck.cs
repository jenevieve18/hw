using System;
	
namespace HW.EForm.Core.Models
{
	public class SponsorUserCheck
	{
		public SponsorUserCheck()
		{
		}
		
		public int SponsorUserCheckID { get; set; }
		public int UserCheckNr { get; set; }
		public int SponsorID { get; set; }
		public string Txt { get; set; }

	}
}
