using System;
	
namespace HW.Core2.Models
{
	public class UserSession
	{
		public int UserSessionID { get; set; }
		public string UserHostAddress { get; set; }
		public string UserAgent { get; set; }
		public int LangID { get; set; }

		public UserSession()
		{
		}
	}
}
