using System;
using System.IO;
using System.Xml.Serialization;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Core.Services;

namespace HW.Toolbelt
{
	class Program
	{
		public static void Main(string[] args)
		{
//			lalala(args);
			if (args.Length == 1) {
				string command = args[0];
				if (command == "upload-exercises") {
					var e = Exercises.Deserialize(@"exercises.xml");
					e.Upload();
				}
			}
		}
		
		static void lalala(string[] args)
		{
			if (args.Length == 4) {
				string command = args[0];
				string html = args[3];
				string js = @"assets\exercises\js\temp\" + args[3] + ".js";
				
				int exerciseID = ConvertHelper.ToInt32(args[1]);
				int langID = ConvertHelper.ToInt32(args[2]);
				
				html += langID == 1 ? "-en" : "";
				html += ".html";
				
				if (command == "update-exercise") {
					string dir = Directory.GetCurrentDirectory();
					string htmlFile = Path.Combine(dir, html);
					string jsFile = Path.Combine(dir, js);
					
					try {
						var content = ReadContent(htmlFile);
						var script = "";
						try {
							script = ReadContent(jsFile);
						} catch (Exception) {
							Console.WriteLine(@"Unable to upload ""{0}"".", jsFile);
						}
						
						Console.WriteLine(@"Running ""update-exercise"" command");
						Console.WriteLine("Waiting...");
						
						var s = new ExerciseService();
						
						var e = s.ReadExercise(exerciseID, langID);
						if (e != null) {
							var el = e.CurrentLanguage;
							if (el != null) {
								int exerciseVariantLangID = s.GetExerciseVariantLangID(exerciseID, langID);
								
								s.UpdateExerciseVariangLanguageContent(content, exerciseVariantLangID);
								Console.WriteLine(@">>> File ""{0}"" uploaded.", htmlFile);
								s.UpdateExerciseScript(script, e.Id);
								Console.WriteLine(@">>> File ""{0}"" uploaded.", jsFile);
								
								Console.WriteLine();
								Console.WriteLine(@"""{0}"" updated.", el.ExerciseName);
							} else {
								Console.WriteLine("No current language ID: {0} for exercise with ID: {1}", langID, exerciseID);
							}
						} else {
							Console.WriteLine(@"Unable to find exercise with ID: {0} and language ID: {1}.", exerciseID, langID);
						}
					} catch (Exception ex) {
						Console.WriteLine(ex.Message);
					}
				}
			}
		}
		
		public static string ReadContent(string path)
		{
			string content = "";
			try {
				content = new StreamReader(path).ReadToEnd();
			} catch (Exception ex) {
				throw ex;
			}
			return content;
		}
	}
	
	public class BaseSerializable<T>
	{
		public BaseSerializable()
		{
		}
		
		public static T Deserialize(string filename)
		{
			return Deserialize(new StreamReader(filename));
		}
		
		public static T Deserialize(TextReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			T type = (T)serializer.Deserialize(reader);
			reader.Close();
			return type;
		}
	}
	
	public class Exercises : BaseSerializable<Exercises>
	{
		[XmlElement("Exercise")] public ExerciseElement[] Items { get; set; }
		
		public void Upload()
		{
			Console.WriteLine(@"Running ""upload-exercises"" command.");
			Console.WriteLine("Waiting...");
			Console.WriteLine();
			var s = new ExerciseService();
			
			foreach (var i in Items) {
				int exerciseID = ConvertHelper.ToInt32(i.ID);
				int langID = ConvertHelper.ToInt32(i.LangID);
				
				int exerciseVariantLangID = s.GetExerciseVariantLangID(exerciseID, langID);
				
				var e = s.ReadExercise(exerciseID, langID);
				if (e != null) {
					if (e.CurrentLanguage != null) {
						Console.WriteLine(@"Updating ""{0}"" content", e.CurrentLanguage.ExerciseName);
						string content = Program.ReadContent(Path.Combine(Directory.GetCurrentDirectory(), i.HTML));
						s.UpdateExerciseVariangLanguageContent(content, exerciseVariantLangID);
						Console.WriteLine("Updated content");
						Console.WriteLine();
					}
					if (i.HasJS) {
						Console.WriteLine(@"Updating ""{0}"" script", e.CurrentLanguage.ExerciseName);
						string script = Program.ReadContent(Path.Combine(Directory.GetCurrentDirectory(), i.JS));
						s.UpdateExerciseScript(script, exerciseID);
						Console.WriteLine("Updated script");
						Console.WriteLine();
					}
				}
			}
		}
	}
	
	public class ExerciseElement
	{
		[XmlAttribute("ID")] public string ID { get; set; }
		[XmlAttribute("LangID")] public string LangID { get; set; }
		[XmlAttribute("HTML")] public string HTML { get; set; }
		[XmlAttribute("JS")] public string JS { get; set; }
		
		public bool HasHTML {
			get { return HTML != ""; }
		}
		
		public bool HasJS {
			get { return JS != ""; }
		}
		
		public override string ToString()
		{
			return string.Format("ID: {0}, HTML: {1}, JS: {2}", ID, HTML, JS);
		}
	}
}