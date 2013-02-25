//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Diary : BaseModel
	{
		public string Note { get; set; }
		public DateTime Date { get; set; }
		public User User { get; set; }
		public DateTime Created { get; set; }
		public DateTime Deleted { get; set; }
		public int Mood { get; set; }
	}
}
