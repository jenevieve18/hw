using System;
	
namespace HW.EForm.Core.Models
{
	public class FeedbackTemplatePage
	{
		public FeedbackTemplatePage()
		{
		}
		
		public int FeedbackTemplatePageID { get; set; }
		public int FeedbackTemplateID { get; set; }
		public string Slide { get; set; }
		public string HeaderPH { get; set; }
		public string BottomPH { get; set; }
		public int ImgPos { get; set; }
		public string Description { get; set; }
		public int DoubleImg { get; set; }

	}
}
