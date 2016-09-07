using System;
	
namespace HW.Core2.Models
{
	public class Session
	{
		public int SessionID { get; set; }
		public string Referrer { get; set; }
		public string DT { get; set; }
		public string UserAgent { get; set; }
		public int UserID { get; set; }
		public string IP { get; set; }
		public string EndDT { get; set; }
		public string Host { get; set; }
		public string Site { get; set; }
		public bool AutoEnded { get; set; }

		public Session()
		{
		}
	}
}
