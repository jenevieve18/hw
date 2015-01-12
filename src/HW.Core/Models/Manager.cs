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
		
		public const int Organization = 1;
		public const int Statistics = 2;
		public const int Messages = 3;
		public const int Managers = 4;
		public const int TEST = 6;
		public const int Exercises = 7;
		
		public override string ToString()
		{
			return Function;
		}
	}
	
	public class ManagerFunctionLang : BaseModel
	{
		public virtual string Function { get; set; }
		public virtual string URL { get; set; }
		public virtual string Expl { get; set; }
	}
}
