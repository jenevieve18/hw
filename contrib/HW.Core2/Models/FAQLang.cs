using System;
	
namespace HW.Core2.Models
{
	public class FAQLang
	{
		public int FAQLangID { get; set; }
		public int FAQID { get; set; }
		public int LangID { get; set; }
		public string Question { get; set; }
		public string Answer { get; set; }

		public FAQLang()
		{
		}
	}
}
