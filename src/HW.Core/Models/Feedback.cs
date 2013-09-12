//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Feedback : BaseModel
	{
		public virtual string Notes { get; set; }
	}
	
	public class FeedbackQuestion : BaseModel
	{
		public virtual Feedback Feedback { get; set; }
		public virtual Question Question { get; set; }
	}
}
