﻿using System;
using System.IO;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Toolbelt
{
	class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length == 3) {
				string command = args[0];
				string html = args[2] + ".html";
				string js = @"assets\exercises\js\temp\" + args[2] + ".js";
				
				int exerciseVariantLangID = ConvertHelper.ToInt32(args[1]);
				
				if (command == "update-exercise") {
					string dir = Directory.GetCurrentDirectory();
					string htmlFile = Path.Combine(dir, html);
					string jsFile = Path.Combine(dir, js);
					
					var content = ReadContent(htmlFile);
					var script = ReadContent(jsFile);
					
					Console.WriteLine(@"Running ""update-exercise"" command");
					Console.WriteLine("Waiting...");
					var s = new ExerciseService();
					
					var evl = s.ReadExerciseVariantLanguage(exerciseVariantLangID);
					var ev = evl.Variant;
					var e = ev.Exercise;
					var el = e.CurrentLanguage;
					
					Console.Write(@"Update ""{0}""? (y/n) ", el.ExerciseName);
					if (Console.ReadLine() == "y") {
						s.UpdateExerciseVariangLanguageContent(content, exerciseVariantLangID);
						Console.WriteLine(@">>> File ""{0}"" uploaded.", htmlFile);
						s.UpdateExerciseScript(script, e.Id);
						Console.WriteLine(@">>> File ""{0}"" uploaded.", jsFile);
					}
					
					Console.WriteLine();
					Console.WriteLine(@"""{0}"" updated.", el.ExerciseName);
				}
			}
		}
		
		static string ReadContent(string path)
		{
			string content = "";
			try {
				content = new StreamReader(path).ReadToEnd();
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
			}
			return content;
		}
	}
}