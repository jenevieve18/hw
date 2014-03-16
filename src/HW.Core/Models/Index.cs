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
		public virtual int TargetValue { get; set; }
		public virtual int YellowLow { get; set; }
		public virtual int GreenLow { get; set; }
		public virtual int GreenHigh { get; set; }
		public virtual int YellowHigh { get; set; }
		
		public virtual int GetColor(float x)
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
		public virtual string Internal { get; set; }
		public virtual int RequiredAnswerCount { get; set; }
		public virtual bool AllPartsRequired { get; set; }
		public virtual int MaxValue { get; set; }
		public virtual List<IndexPart> Parts { get; set; }
		public virtual IList<IndexLanguage> Languages { get; set; }
		
		public virtual float AverageAX { get; set; }
		public virtual int CountDX { get; set; }
	}
	
	public class IndexPart : BaseModel
	{
		public virtual int Multiple { get; set; }
		public virtual Index OtherIndex { get; set; }
	}
	
	public class IndexLanguage : BaseModel
	{
		public virtual Index Index { get; set; }
		public virtual Language Language { get; set; }
		public virtual string IndexName { get; set;}
	}
}
