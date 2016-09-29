using System;
	
namespace HW.EForm.Core.Models
{
	public class FeedbackTemplate
	{
		public FeedbackTemplate()
		{
		}
		
		public int FeedbackTemplateID { get; set; }
		public string FeedbackTemplateText { get; set; }
		public string OrgPH { get; set; }
		public string DeptPH { get; set; }
		public string DatePH { get; set; }
		public string Slide { get; set; }
		public string DefaultSlide { get; set; }
		public string DefaultHeaderPH { get; set; }
		public string DefaultBottomPH { get; set; }
		public string BG { get; set; }
		public int DefaultImgPos { get; set; }
		public string CountSlide { get; set; }
		public string CountPH { get; set; }
		public string CountTxt { get; set; }
		public int NoFontScale { get; set; }

	}
}
