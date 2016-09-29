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
			Values = values;
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
		public IList<AnswerValue> Values { get; set; }
		
		public void AddValue(AnswerValue v)
		{
			v.Answer = this;
			Values.Add(v);
		}
		
		public void AddValues(IList<AnswerValue> values)
		{
			foreach (var v in values) {
				AddValue(v);
			}
		}
	}
}
