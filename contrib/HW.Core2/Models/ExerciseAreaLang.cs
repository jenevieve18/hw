using System;
	
namespace HW.Core2.Models
{
	public class ExerciseAreaLang
	{
		public int ExerciseAreaLangID { get; set; }
		public int ExerciseAreaID { get; set; }
		public string ExerciseArea { get; set; }
		public int Lang { get; set; }

		public ExerciseAreaLang()
		{
		}
	}
}
