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
		Option option;
		Answer answer;
		
		public OptionComponent OptionComponent { get; set; }
		
		public int GetValueInt()
		{
			if (Option != null && !Option.IsVAS) {
				return Option.GetComponent(ValueInt).ExportValue;
			}
			return ValueInt;
		}
		
		public Answer Answer {
			get {
				if (answer == null) {
					OnAnswerGet(null);
				}
				return answer;
			}
			set { answer = value; }
		}
		
		public Option Option {
			get {
				if (option == null) {
					OnOptionGet(null);
				}
				return option;
			}
			set { option = value; }
		}
		
		public event EventHandler OptionGet;
		public event EventHandler AnswerGet;
		
		protected virtual void OnAnswerGet(EventArgs e)
		{
			if (AnswerGet != null) {
				AnswerGet(this, e);
			}
		}
		
		protected virtual void OnOptionGet(EventArgs e)
		{
			if (OptionGet != null) {
				OptionGet(this, e);
			}
		}
	}
}
