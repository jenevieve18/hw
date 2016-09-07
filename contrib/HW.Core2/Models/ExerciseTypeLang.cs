using System;
	
namespace HW.Core2.Models
{
	public class ExerciseTypeLang
	{
		public int ExerciseTypeLangID { get; set; }
		public int ExerciseTypeID { get; set; }
		public string ExerciseType { get; set; }
		public string ExerciseSubtype { get; set; }
		public int Lang { get; set; }

		public ExerciseTypeLang()
		{
		}
	}
}
