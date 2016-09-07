using System;
	
namespace HW.Core2.Models
{
	public class SponsorAdminExercise
	{
		public int SponsorAdminExerciseID { get; set; }
		public DateTime? Date { get; set; }
		public int SponsorAdminID { get; set; }
		public int ExerciseVariantLangID { get; set; }

		public SponsorAdminExercise()
		{
		}
	}
}
