using System;
	
namespace HW.Core2.Models
{
	public class ExerciseStat
	{
		public int ExerciseStatsID { get; set; }
		public int ExerciseVariantLangID { get; set; }
		public int UserID { get; set; }
		public DateTime? DateTime { get; set; }
		public int UserProfileID { get; set; }

		public ExerciseStat()
		{
		}
	}
}
