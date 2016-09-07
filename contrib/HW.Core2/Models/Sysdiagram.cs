using System;
	
namespace HW.Core2.Models
{
	public class Sysdiagram
	{
		public string Name { get; set; }
		public int PrincipalId { get; set; }
		public int DiagramId { get; set; }
		public int Version { get; set; }
		public string Definition { get; set; }

		public Sysdiagram()
		{
		}
	}
}
