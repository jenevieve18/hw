using System;
	
namespace HW.Core2.Models
{
	public class SponsorExerciseDataInput
	{
		public int SponsorExerciseDataInputID { get; set; }
		public string Content { get; set; }
		public int SponsorID { get; set; }
		public int Order { get; set; }
		public int ExerciseVariantLangID { get; set; }

		public SponsorExerciseDataInput()
		{
		}
	}
}
