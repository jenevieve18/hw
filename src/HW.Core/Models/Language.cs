using System;
using System.Globalization;
using System.Threading;

namespace HW.Core.Models
{
	public class Language : BaseModel
	{
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
			return lid == 1 ? "medelvärde" : "mean value";
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
//			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
//			switch (lid) {
//					case 0: return "Välj område";
//					case 1: return "Choose area";
//					default: throw new NotSupportedException();
//			}
//		}
		
//		public static string GetChooseCategory(int lid)
//		{
//			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
//			switch (lid) {
//					case 0: return "Välj kategori";
//					case 1: return "Choose category";
//					default: throw new NotSupportedException();
//			}
//		}
		
//		public static string GetSortingOrder(int lid, int bx)
//		{
//			// FIXME: Why 0 on Swedish? See setculture where Swedish is 1.
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
