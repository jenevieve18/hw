using System;
using System.Collections.Generic;

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
		
		public Survey(Survey s)
		{
			this.SurveyID = s.SurveyID;
			this.Internal = s.Internal;
			this.SurveyKey = s.SurveyKey;
			this.Copyright = s.Copyright;
			this.FlipFlopBg = s.FlipFlopBg;
			this.NoTime = s.NoTime;
			this.ClearQuestions = s.ClearQuestions;
			this.TwoColumns = s.TwoColumns;
		}
		
		public IList<SurveyQuestion> Questions { get; set; }
		
		public override string ToString()
		{
			return string.Format("(SurveyID: {0}) {1}", SurveyID, Internal);
		}
	}
}
