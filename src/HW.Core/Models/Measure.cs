//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Measure : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual MeasureCategory Category { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual string Description { get; set; }
	}
	
	public class MeasureCategory : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual MeasureType Type { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual IList<MeasureCategoryLanguage> Languages { get; set; }
	}
	
	public class MeasureCategoryLanguage : BaseModel
	{
		public virtual MeasureCategory Category { get; set; }
		public virtual Measure Measure { get; set; }
		public virtual Language Language { get; set; }
	}
	
	public class MeasureComponent : BaseModel
	{
		public virtual Measure Measure { get; set; }
		public virtual string Component { get; set; }
		public virtual IList<MeasureComponentLanguage> Languages { get; set; }
	}
	
	public class MeasureComponentLanguage : BaseModel
	{
		public virtual MeasureComponent Component { get; set; }
		public virtual Language Language { get; set; }
		public virtual string ComponentName { get; set; }
		public virtual string Unit { get; set; }
	}
	
	public class MeasureComponentPart : BaseModel
	{
		public virtual MeasureComponent Component { get; set; }
		public virtual int Part { get; set; }
		public virtual int SortOrder { get; set; }
	}
	
	public class MeasureLanguage : BaseModel
	{
		public virtual Measure Measure { get; set; }
		public virtual Language Language { get; set; }
		public virtual string MeasureName { get; set; }
	}
	
	public class MeasureType : BaseModel
	{
		public virtual string Name { get; set; }
		public virtual bool Active { get; set; }
		public virtual int SortOrder { get; set; }
	}
	
	public class MeasureTypeLanguage : BaseModel
	{
		public virtual Measure Measure { get; set; }
		public virtual Language Language { get; set; }
		public virtual string TypeName { get; set; }
	}
}
