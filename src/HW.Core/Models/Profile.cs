using System;

namespace HW.Core.Models
{
	public class ProfileComparison : BaseModel
	{
		public virtual string Hash { get; set; }
	}
	
	public class ProfileComparisonBackgroundQuestion : BaseModel
	{
		public virtual ProfileComparison Comparison { get; set; }
		public virtual BackgroundQuestion Question { get; set; }
		public virtual int Value { get; set; }
	}
}
