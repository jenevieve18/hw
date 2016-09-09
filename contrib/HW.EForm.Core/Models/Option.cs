using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Option
	{
		public Option()
		{
			Components = new List<OptionComponents>();
			AnswerValues = new List<AnswerValue>();
		}
		
		public int OptionID { get; set; }
		public int OptionType { get; set; }
		public int OptionPlacement { get; set; }
		public string Variablename { get; set; }
		public string Internal { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public int InnerWidth { get; set; }
		public int OptionContainerID { get; set; }
		public string BgColor { get; set; }
		public decimal RangeLow { get; set; }
		public decimal RangeHigh { get; set; }
		public IList<OptionComponents> Components { get; set; }
		public IList<AnswerValue> AnswerValues { get; set; }

		public bool IsSingleChoice {
			get { return OptionType == OptionTypes.SingleChoice; }
		}
		
		public bool IsNumeric {
			get { return OptionType == OptionTypes.Numeric; }
		}
		
		public bool IsVAS {
			get { return OptionType == OptionTypes.VAS; }
		}
		
		public bool IsMultiChoice {
			get { return OptionType == OptionTypes.MultiChoice; }
		}
		
		public bool IsFreeText {
			get { return OptionType == OptionTypes.FreeText; }
		}
		
		public void AddComponent(OptionComponent oc)
		{
			AddComponent(new OptionComponents { OptionComponent = oc });
		}
		
		public void AddComponent(OptionComponents ocs)
		{
			Components.Add(ocs);
		}
		
		public OptionComponents GetComponent(int optionComponentID)
		{
			foreach (var c in Components) {
				if (c.OptionComponentID == optionComponentID) {
					return c;
				}
			}
			return null;
		}
	}
	
	public class OptionTypes
	{
		public const int SingleChoice = 1;
		public const int FreeText = 2;
		public const int MultiChoice = 3;
		public const int Numeric = 4;
		public const int VAS = 9;
	}
}
