using System;
	
namespace HW.Core2.Models
{
	public class SponsorAdminExerciseDataInput
	{
		public int SponsorAdminExerciseDataInputID { get; set; }
		public string Content { get; set; }
		public int SponsorAdminExerciseID { get; set; }
		public int Order { get; set; }

		public SponsorAdminExerciseDataInput()
		{
		}
	}
}
