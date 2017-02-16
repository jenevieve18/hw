using System;
using HW.Core.Helpers;

namespace ExerciseUploader
{
	class Program
	{
		public static void Main(string[] args)
		{
			var exercises = Exercises.Deserialize(@"../../../src/HW.Grp/exercises/exercises.xml");
			Console.WriteLine(exercises);
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}