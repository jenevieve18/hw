using System;
	
namespace HW.EForm.Core.Models
{
	public class MailQueue
	{
		public MailQueue()
		{
		}
		
		public int MailQueueID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public string AdrTo { get; set; }
		public string AdrFrom { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public DateTime? Sent { get; set; }
		public int SendType { get; set; }
		public string ErrorDescription { get; set; }
		public string BodyJapaneseUnicode { get; set; }
		public string SubjectJapaneseUnicode { get; set; }
		public int LangID { get; set; }

	}
}
