using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Core.Models
{
	public interface IBaseModel
	{
		int Id { get; set; }
	}
	
	public class BaseModel : IBaseModel
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
		
		public virtual void Validate()
        {
            Errors.Clear();
        }
		
		public virtual void AddErrorIf(bool condition, string message)
		{
			if (condition) {
				Errors.Add(message);
			}
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
		
		public string ToHtmlUl()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("<ul>");
			foreach (var s in this) {
				sb.AppendLine("<li>" + s + "</li>");
			}
			sb.AppendLine("</ul>");
			return sb.ToString();
		}
	}
}
