using System;
	
namespace HW.Core2.Models
{
	public class ExerciseMiracle
	{
		public int ExerciseMiracleID { get; set; }
		public int UserID { get; set; }
		public DateTime? DateTime { get; set; }
		public DateTime? DateTimeChanged { get; set; }
		public string Miracle { get; set; }
		public bool AllowPublish { get; set; }
		public bool Published { get; set; }

		public ExerciseMiracle()
		{
		}
	}
}
