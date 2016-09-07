using System;
	
namespace HW.Core2.Models
{
	public class UserToken
	{
		public Guid Token { get; set; }
		public int UserID { get; set; }
		public DateTime? Expires { get; set; }
		public string SessionID { get; set; }

		public UserToken()
		{
		}
	}
}
