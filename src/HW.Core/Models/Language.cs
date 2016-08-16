using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace HW.Core.Models
{
	public class Language : BaseModel
	{
		public const int SWEDISH = 1;
		public const int ENGLISH = 2;
		public const int GERMAN = 4;
		
		public virtual string Name { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
	
	public class LanguageFactory
	{
		public static string GetMeanText(int lid)
		{
//			return lid == 1 ? "medelvärde" : "mean value";
			switch (lid) {
				case 1: return "medelvärde";
			case 4: return "medelvärde";
		default: return "mean value";
			}
		}
		
		public static int GetLanguageID(HttpRequest request)
		{
			var c = ResolveCulture(request);
//			return c.Name == "sv-SE" ? Language.SWEDISH : Language.ENGLISH;
			if (c.Name == "sv-SE") {
				return Language.SWEDISH;
			} else if (c.Name == "de-DE") {
				return Language.GERMAN;
			} else {
				return Language.ENGLISH;
			}
		}
		
		public static CultureInfo ResolveCulture(HttpRequest request)
		{
			string[] languages = request.UserLanguages;
			
			if (languages == null || languages.Length == 0) {
				return null;
			}
			
			try {
				string language = languages[0].ToLowerInvariant().Trim();
				return CultureInfo.CreateSpecificCulture(language);
			} catch (ArgumentException) {
				return null;
			}
		}
		
//		public static string GetGroupExercise(int lid)
//		{
//			switch (lid) {
//					case 0: return "Grupp-<br/>övningar";
//					case 1: return "Group-<br/>exercises";
//					default: throw new NotSupportedException();
//			}
//		}
		
//		public static string GetChooseArea(int lid)
//		{
//			switch (lid) {
//					case 0: return "Välj område";
//					case 1: return "Choose area";
//					default: throw new NotSupportedException();
//			}
//		}
		
//		public static string GetChooseCategory(int lid)
//		{
//			switch (lid) {
//					case 0: return "Välj kategori";
//					case 1: return "Choose category";
//					default: throw new NotSupportedException();
//			}
//		}
		
//		public static string GetSortingOrder(int lid, int bx)
//		{
//			switch (lid) {
//					case 0: return bx + " övningar - Sortering:";
//					case 1: return bx + " exercises - Order:";
//					default: throw new NotSupportedException();
//			}
//		}
		
		public static string GetLegend(int lid)
		{
			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
			switch (lid) {
					case 0: return ""; // TODO: Why?
					case 1: return ""; // TODO: Why?
					default: throw new NotSupportedException();
			}
		}
		
		public static void SetCurrentCulture(int lid)
		{
			switch (lid) {
					case 1: Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE"); break;
                    case 2: Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); break;
                    case 4: Thread.CurrentThread.CurrentCulture = new CultureInfo("de-DE"); break;
					default: throw new NotSupportedException();
			}
		}

//		public static void SetCurrentCulture2(int lid)
//		{
//			switch (lid)
//			{
//				case 0: Thread.CurrentThread.CurrentCulture = new CultureInfo("sv-SE"); break;
//				case 1: Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); break;
//				default: throw new NotSupportedException();
//			}
//		}
	}
}
