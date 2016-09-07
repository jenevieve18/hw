using System;
	
namespace HW.Core2.Models
{
	public class ExerciseVariant
	{
		public int ExerciseVariantID { get; set; }
		public int ExerciseID { get; set; }
		public int ExerciseTypeID { get; set; }

		public ExerciseVariant()
		{
		}
	}
}
