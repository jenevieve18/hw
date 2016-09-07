using System;
	
namespace HW.Core2.Models
{
	public class ExerciseVariantLang
	{
		public int ExerciseVariantLangID { get; set; }
		public int ExerciseVariantID { get; set; }
		public string ExerciseFile { get; set; }
		public int ExerciseFileSize { get; set; }
		public string ExerciseContent { get; set; }
		public int ExerciseWindowX { get; set; }
		public int ExerciseWindowY { get; set; }
		public int Lang { get; set; }

		public ExerciseVariantLang()
		{
		}
	}
}
