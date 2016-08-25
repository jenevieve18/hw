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

		public Option()
		{
		}
		
		public IList<OptionComponents> Components;
	}
}
