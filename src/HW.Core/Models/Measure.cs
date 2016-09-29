using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Measure : BaseModel
	{
		public string Name { get; set; }
		public MeasureCategory Category { get; set; }
		public int SortOrder { get; set; }
		public string Description { get; set; }
		public IList<MeasureComponent> Components { get; set; }
		
		public Measure()
		{
			Components = new List<MeasureComponent>();
		}
	}
	
	public class MeasureCategory : BaseModel
	{
		public string Name { get; set; }
		public MeasureType Type { get; set; }
		public int SortOrder { get; set; }
		public IList<MeasureCategoryLanguage> Languages { get; set; }
	}
	
	public class MeasureCategoryLanguage : BaseModel
	{
		public MeasureCategory Category { get; set; }
		public Measure Measure { get; set; }
		public Language Language { get; set; }
	}
	
	public class MeasureComponent : BaseModel
	{
		public Measure Measure { get; set; }
		public string Component { get; set; }
		public IList<MeasureComponentLanguage> Languages { get; set; }
	}
	
	public class MeasureComponentLanguage : BaseModel
	{
		public MeasureComponent Component { get; set; }
		public Language Language { get; set; }
		public string ComponentName { get; set; }
		public string Unit { get; set; }
	}
	
	public class MeasureComponentPart : BaseModel
	{
		public MeasureComponent Component { get; set; }
		public int Part { get; set; }
		public int SortOrder { get; set; }
	}
	
	public class MeasureLanguage : BaseModel
	{
		public Measure Measure { get; set; }
		public Language Language { get; set; }
		public string MeasureName { get; set; }
	}
	
	public class MeasureType : BaseModel
	{
		public string Name { get; set; }
		public bool Active { get; set; }
		public int SortOrder { get; set; }
	}
	
	public class MeasureTypeLanguage : BaseModel
	{
		public Measure Measure { get; set; }
		public Language Language { get; set; }
		public string TypeName { get; set; }
	}
}
