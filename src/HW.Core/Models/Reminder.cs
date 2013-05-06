﻿//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;

namespace HW.Core.Models
{
	public class Reminder : BaseModel
	{
		public virtual User User { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string Subject { get; set; }
		public virtual string Body { get; set; }
	}
}
