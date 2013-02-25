//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Feedback : BaseModel
	{
		public string Notes { get; set; }
	}
	
	public class FeedbackQuestion : BaseModel
	{
		public Feedback Feedback { get; set; }
		public Question Question { get; set; }
	}
}
