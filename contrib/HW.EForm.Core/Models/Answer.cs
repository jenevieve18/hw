using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Answer
	{
		public Answer() : this(new List<AnswerValue>())
		{
		}
		
		public Answer(IList<AnswerValue> values)
		{
			AnswerValues = values;
		}
		
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
		public ProjectRound ProjectRound { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
		public bool HasProjectRoundUser {
			get { return ProjectRoundUser != null; }
		}
		public IList<AnswerValue> AnswerValues { get; set; }
		
		public void AddAnswerValue(AnswerValue v)
		{
			v.Answer = this;
			AnswerValues.Add(v);
		}
		
		public void AddAnswerValues(IList<AnswerValue> values)
		{
			foreach (var v in values) {
				AddAnswerValue(v);
			}
		}
	}
}
