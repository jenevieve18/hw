using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HW.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace ExerciseDownloader
{
	class Program
	{
		static void Main(string[] args)
		{
            string dir = "exercises";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var r = new SqlExerciseRepository();
            var exercises = r.FindByAreaAndCategory(0, 0, 0, 2);
            Console.WriteLine("Fetched {0} exercises from the database.", exercises.Count);
            int i = 0;
            foreach (var e in exercises)
            {
                Console.Write("{0}. Writing {1}...", ++i, e.CurrentLanguage.ExerciseName);
                try
                {
                    string file = Path.Combine(dir, e.CurrentVariant.Id + " - " + e.CurrentLanguage.ExerciseName + ".html");
                    using (var w = new StreamWriter(FileHelper.SanitizeFileName(file), false, Encoding.UTF8))
                    {
                        w.WriteLine(e.CurrentVariant.Content);
                    }
                    Console.WriteLine("	[OK]");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("	[FAILED]");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
		}
	}
}
