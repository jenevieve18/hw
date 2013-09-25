using System;

namespace HW.Core.Models
{
	public class Diary : BaseModel
	{
		public virtual string Note { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual User User { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual DateTime Deleted { get; set; }
		public virtual int Mood { get; set; }
	}
}
