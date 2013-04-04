//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Manager : BaseModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
	}
	
	public class ManagerFunction : BaseModel
	{
		public string Function { get; set; }
		public string URL { get; set; }
		public string Expl { get; set; }
	}
}
