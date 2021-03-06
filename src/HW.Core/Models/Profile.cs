﻿using System;

namespace HW.Core.Models
{
	public class ProfileComparison : BaseModel
	{
		public string Hash { get; set; }
	}
	
	public class ProfileComparisonBackgroundQuestion : BaseModel
	{
		public ProfileComparison Comparison { get; set; }
		public BackgroundQuestion Question { get; set; }
		public int Value { get; set; }
	}
}
