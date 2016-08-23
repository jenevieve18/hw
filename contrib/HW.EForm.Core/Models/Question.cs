using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Question
	{
		public int QuestionID { get; set; }
		public string VariableName { get; set; }
		public int OptionsPlacement { get; set; }
		public int FontFamily { get; set; }
		public int FontSize { get; set; }
		public int FontDecoration { get; set; }
		public string FontColor { get; set; }
		public int Underlined { get; set; }
		public int QuestionContainerID { get; set; }
		public string Internal { get; set; }
		public int Box { get; set; }

		public Question()
		{
		}
		
		public IList<QuestionOption> Options { get; set; }
	}
}
