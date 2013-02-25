//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Navigation : BaseModel
	{
		public string URL { get; set; }
		public string Text { get; set; }
		public int SortOrder { get; set; }
	}
}
