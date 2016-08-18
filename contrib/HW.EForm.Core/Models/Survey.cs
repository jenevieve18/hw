using System;
	
namespace HW.EForm.Core.Models
{
	public class Survey
	{
		public int SurveyID { get; set; }
		public string Internal { get; set; }
		public Guid SurveyKey { get; set; }
		public string Copyright { get; set; }
		public int FlipFlopBg { get; set; }
		public int NoTime { get; set; }
		public int ClearQuestions { get; set; }
		public int TwoColumns { get; set; }

		public Survey()
		{
		}
	}
}
