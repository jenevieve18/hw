using System;
	
namespace HW.Core2.Models
{
	public class SponsorLang
	{
		public int SponsorLangID { get; set; }
		public int SponsorID { get; set; }
		public int LangID { get; set; }
		public string TreatmentOfferText { get; set; }
		public string TreatmentOfferIfNeededText { get; set; }
		public string AlternativeTreatmentOfferText { get; set; }

		public SponsorLang()
		{
		}
	}
}
