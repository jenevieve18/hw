using System;
	
namespace HW.Core2.Models
{
	public class ExerciseCategoryLang
	{
		public int ExerciseCategoryLangID { get; set; }
		public int ExerciseCategoryID { get; set; }
		public string ExerciseCategory { get; set; }
		public int Lang { get; set; }

		public ExerciseCategoryLang()
		{
		}
	}
}
