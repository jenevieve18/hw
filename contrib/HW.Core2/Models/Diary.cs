using System;
	
namespace HW.Core2.Models
{
	public class Diary
	{
		public int DiaryID { get; set; }
		public string DiaryNote { get; set; }
		public DateTime? DiaryDate { get; set; }
		public int UserID { get; set; }
		public DateTime? CreatedDT { get; set; }
		public DateTime? DeletedDT { get; set; }
		public int Mood { get; set; }

		public Diary()
		{
		}
	}
}
