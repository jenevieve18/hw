using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Answer
	{
		public int AnswerID { get; set; }
		public int ProjectRoundID { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundUserID { get; set; }
		public DateTime? StartDT { get; set; }
		public DateTime? EndDT { get; set; }
		public Guid AnswerKey { get; set; }
		public int ExtendedFirst { get; set; }
		public int CurrentPage { get; set; }
		public int FeedbackAlert { get; set; }

		public Answer()
		{
		}
		
		public IList<AnswerValue> Values { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		
		ProjectRoundUser projectRoundUser;
		
		public ProjectRoundUser ProjectRoundUser {
			get {
				if (projectRoundUser == null) {
					OnProjectRoundUserGet(null);
				}
				return projectRoundUser;
			}
			set { projectRoundUser = value; }
		}
		
		public event EventHandler ProjectRoundUserGet;
		
		protected virtual void OnProjectRoundUserGet(EventArgs e)
		{
			if (ProjectRoundUserGet != null) {
				ProjectRoundUserGet(this, e);
			}
		}
	}
}
