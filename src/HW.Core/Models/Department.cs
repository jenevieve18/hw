//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Department : BaseModel
	{
		public Sponsor Sponsor { get; set; }
		public string Name { get; set; }
		public Department Parent { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public string ShortName { get; set; }
		public string AnonymizedName { get; set; }
		
		public int Depth { get; set; }
		public int Siblings { get; set; }
		public string TreeName { get; set; }
	}
}
