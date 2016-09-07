using System;
	
namespace HW.Core2.Models
{
	public class SuperSponsorLang
	{
		public int SuperSponsorLangID { get; set; }
		public int SuperSponsorID { get; set; }
		public int LangID { get; set; }
		public string Slogan { get; set; }
		public string Header { get; set; }

		public SuperSponsorLang()
		{
		}
	}
}
