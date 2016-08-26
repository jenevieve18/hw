using System;
	
namespace HW.EForm.Core.Models
{
	public class QuestionOption
	{
		public int QuestionOptionID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int OptionPlacement { get; set; }
		public int SortOrder { get; set; }
		public string Variablename { get; set; }
		public int Forced { get; set; }
		public int Hide { get; set; }
		public Option Option { get; set; }

		public QuestionOption()
		{
		}
		
		public QuestionOption(Option option)
		{
			this.Option = option;
		}
	}
}
