using System;
	
namespace HW.EForm.Core.Models
{
	public class AnswerValue
	{
		public int Value { get; set; }
		public int AnswerID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int ValueInt { get; set; }
		public decimal ValueDecimal { get; set; }
		public DateTime? ValueDateTime { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public int CreatedSessionID { get; set; }
		public int DeletedSessionID { get; set; }
		public string ValueText { get; set; }
		public string ValueTextJapaneseUnicode { get; set; }

		public AnswerValue()
		{
		}
		
		public Question Question { get; set; }
		public Option Option { get; set; }
	}
}
