using System;
	
namespace HW.EForm.Core.Models
{
	public class AnswerValue
	{
		int valueInt;
		
		public AnswerValue()
		{
		}
		
		public int Value { get; set; }
		public int AnswerID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public decimal ValueDecimal { get; set; }
		public DateTime? ValueDateTime { get; set; }
		public DateTime? CreatedDateTime { get; set; }
		public int CreatedSessionID { get; set; }
		public int DeletedSessionID { get; set; }
		public string ValueText { get; set; }
		public string ValueTextJapaneseUnicode { get; set; }
		public Question Question { get; set; }
		public OptionComponent OptionComponent { get; set; }
		public IndexPartComponent IndexPartComponent { get; set; }
		public bool HasIndexPartComponent {
			get { return IndexPartComponent != null; }
		}
		public bool HasOptionComponent {
			get { return OptionComponent != null; }
		}
		public int ValueInt {
			get {
				if (HasOptionComponent) {
					return OptionComponent.ExportValue;
				} else if (HasIndexPartComponent) {
					return IndexPartComponent.Val;
				}
				return valueInt;
			}
			set { valueInt = value; }
		}
		public Answer Answer { get; set; }
		public Option Option { get; set; }
	}
}
