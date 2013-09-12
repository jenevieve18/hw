//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Core.Models
{
	public class BaseModel
	{
        public BaseModel()
        {
            Errors = new ErrorMessages();
        }

		public virtual int Id { get; set; }
		public virtual ErrorMessages Errors { get; set; }
		public virtual bool HasErrors {
			get { return Errors.Count > 0; }
		}
	}
	
	public class ErrorMessages : List<string>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (var s in this) {
				sb.AppendLine(s);
			}
			return sb.ToString();
		}
	}
}
