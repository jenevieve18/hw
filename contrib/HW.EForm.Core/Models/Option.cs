using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Option
	{
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
		
		public Option() : this("")
		{
		}

		public Option(string @internal)
		{
			this.Internal = @internal;
			this.Components = new List<OptionComponents>();
		}
		
		public bool IsSlider {
			get { return OptionType == 9; }
		}
		
		public void AddComponent(OptionComponents component)
		{
			Components.Add(component);
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
	
	public enum OptionType
	{
		SingleChoice = 1,
		FreeText = 2,
		MultiChoice = 3,
		Numeric = 4,
		VAS = 9
	}
}
