using System;
	
namespace HW.Core2.Models
{
	public class UserProfileBQ
	{
		public int UserBQID { get; set; }
		public int UserProfileID { get; set; }
		public int BQID { get; set; }
		public int ValueInt { get; set; }
		public string ValueText { get; set; }
		public DateTime? ValueDate { get; set; }

		public UserProfileBQ()
		{
		}
	}
}
