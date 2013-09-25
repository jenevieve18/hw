using System;

namespace HW.Core.Models
{
	public class Manager : BaseModel
	{
		public virtual string Email { get; set; }
		public virtual string Password { get; set; }
		public virtual string Name { get; set; }
		public virtual string Phone { get; set; }
	}
	
	public class ManagerFunction : BaseModel
	{
		public virtual string Function { get; set; }
		public virtual string URL { get; set; }
		public virtual string Expl { get; set; }
		
		public override string ToString()
		{
			return Function;
		}
	}
}
