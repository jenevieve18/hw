using System;
	
namespace HW.Core2.Models
{
	public class ExerciseLang
	{
		public int ExerciseLangID { get; set; }
		public int ExerciseID { get; set; }
		public string Exercise { get; set; }
		public string ExerciseTime { get; set; }
		public string ExerciseTeaser { get; set; }
		public int Lang { get; set; }
		public bool New { get; set; }
		public string ExerciseContent { get; set; }

		public ExerciseLang()
		{
		}
	}
}
