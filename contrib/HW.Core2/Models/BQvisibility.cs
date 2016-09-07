using System;
	
namespace HW.Core2.Models
{
	public class BQvisibility
	{
		public int BQvisibilityID { get; set; }
		public int ChildBQID { get; set; }
		public int BQID { get; set; }
		public int BAID { get; set; }

		public BQvisibility()
		{
		}
	}
}
