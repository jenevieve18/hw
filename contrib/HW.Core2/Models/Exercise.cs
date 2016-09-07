using System;
	
namespace HW.Core2.Models
{
	public class Exercise
	{
		public int ExerciseID { get; set; }
		public int ExerciseAreaID { get; set; }
		public int ExerciseSortOrder { get; set; }
		public string ExerciseImg { get; set; }
		public int RequiredUserLevel { get; set; }
		public int Minutes { get; set; }
		public int ExerciseCategoryID { get; set; }
		public int PrintOnBottom { get; set; }
		public string ReplacementHead { get; set; }
		public int Status { get; set; }
		public string Script { get; set; }

		public Exercise()
		{
		}
	}
}
