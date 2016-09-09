using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class BaseModel
	{
		public BaseModel()
		{
			ErrorMessages = new ErrorMessages();
		}
		
		public ErrorMessages ErrorMessages { get; set; }
		
		public virtual void Validate()
		{
			ErrorMessages.Clear();
		}
	}
	
	public class ErrorMessages : List<string>
	{
	}
}
