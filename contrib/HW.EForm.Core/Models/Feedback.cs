using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Feedback
	{
		public int FeedbackID { get; set; }
		public string FeedbackText { get; set; }

		public Feedback()
		{
		}
		
		public IList<FeedbackQuestion> Questions { get; set; }
		
		
		
//		public IList<ProjectRoundUnit> ProjectRoundUnits { get; set; }
	}
}
