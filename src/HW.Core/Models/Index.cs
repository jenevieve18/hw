using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public interface IIndex
	{
		int TargetValue { get; set; }
		int YellowLow { get; set; }
		int GreenLow { get; set; }
		int GreenHigh { get; set; }
		int YellowHigh { get; set; }
	}
	
	public class BaseIndex : BaseModel, IIndex
	{
		public int TargetValue { get; set; }
		public int YellowLow { get; set; }
		public int GreenLow { get; set; }
		public int GreenHigh { get; set; }
		public int YellowHigh { get; set; }
		
		public BaseIndex()
		{
		}
		
		public BaseIndex(int targetValue, int yellowLow, int greenLow, int greenHigh, int yellowHigh)
		{
			this.TargetValue = targetValue;
			this.YellowLow = yellowLow;
			this.GreenLow = greenLow;
			this.GreenHigh = greenHigh;
			this.YellowHigh = yellowHigh;
		}
		
		public int GetColor(float x)
		{
			if (YellowLow >= 0 && YellowLow <= 100 && x >= YellowLow) {
				return 1;
			} else if (GreenLow >= 0 && GreenLow <= 100 && x >= GreenLow) {
				return 0;
			} else if (GreenHigh >= 0 && GreenHigh <= 100 && x >= GreenHigh) {
				return 1;
			} else if (YellowHigh >= 0 && YellowHigh <= 100 && x >= YellowHigh) {
				return 2;
			} else {
				return 2;
			}
		}
	}
	
	public class Index : BaseIndex
	{
		public string Internal { get; set; }
		public int RequiredAnswerCount { get; set; }
		public bool AllPartsRequired { get; set; }
		public int MaxValue { get; set; }
		public List<IndexPart> Parts { get; set; }
		public IList<IndexLanguage> Languages { get; set; }
		
		public float AverageAX { get; set; }
		public int CountDX { get; set; }
	}
	
	public class IndexPart : BaseModel
	{
		public int Multiple { get; set; }
		public Index OtherIndex { get; set; }
	}
	
	public class IndexLanguage : BaseModel
	{
		public Index Index { get; set; }
		public Language Language { get; set; }
		public string IndexName { get; set;}
	}
}
