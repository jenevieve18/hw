//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Exercise : BaseModel
	{
		public virtual string Image { get; set; }
		public virtual ExerciseCategory Category { get; set; }
		public virtual ExerciseArea Area { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual int Minutes { get; set; }
		public virtual int RequiredUserLevel { get; set; }
		public virtual string ReplacementHead { get; set; }
		public virtual IList<ExerciseLanguage> Languages { get; set; }
		
		public virtual ExerciseLanguage CurrentLanguage { get; set; }
		public virtual ExerciseAreaLanguage CurrentArea { get; set; }
		public virtual ExerciseCategoryLanguage CurrentCategory { get; set; }
		public virtual ExerciseVariantLanguage CurrentVariant { get; set; }
		public virtual ExerciseTypeLanguage CurrentType { get; set; }
		
		public override string ToString()
		{
			return string.Format("{0} - {1}", CurrentArea.AreaName, CurrentCategory.CategoryName);
		}
	}
	
	public class ExerciseArea : BaseModel
	{
		public virtual string Image { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual IList<ExerciseAreaLanguage> Languages { get; set; }
	}
	
	public class ExerciseAreaLanguage : BaseModel
	{
		public virtual ExerciseArea Area { get; set; }
		public virtual Language Language { get; set; }
		public virtual string AreaName { get; set; }
		
		public override string ToString()
		{
			return AreaName;
		}
	}
	
	public class ExerciseCategory : BaseModel
	{
		public virtual int SortOrder { get; set; }
		public virtual IList<ExerciseCategoryLanguage> Languages { get; set; }
	}
	
	public class ExerciseCategoryLanguage : BaseModel
	{
		public Language Language { get; set; }
		public ExerciseCategory Category { get; set; }
		public string CategoryName { get; set; }
		
		public override string ToString()
		{
			return CategoryName;
		}
	}
	
	public class ExerciseLanguage : BaseModel
	{
		public virtual Exercise Exercise { get; set; }
		public virtual string ExerciseName { get; set; }
		public virtual string Time { get; set; }
		public virtual string Teaser { get; set; }
		public virtual Language Language { get; set; }
		public virtual bool IsNew { get; set; }
	}
	
	public class ExerciseMiracle : BaseModel
	{
		public User User { get; set; }
		public DateTime Time { get; set; }
		public DateTime TimeChanged { get; set; }
		public string MiracleDescription { get; set; }
		public bool AllowPublished { get; set; }
		public bool Published { get; set; }
	}
	
	public class ExerciseStats : BaseModel
	{
		public User User { get; set; }
		public ExerciseVariantLanguage VariantLanguage { get; set; }
		public DateTime Date { get; set; }
		public UserProfile UserProfile { get; set; }
	}
	
	public class ExerciseType : BaseModel
	{
		public int SortOrder { get; set; }
		public IList<ExerciseTypeLanguage> Languages { get; set; }
	}
	
	public class ExerciseTypeLanguage : BaseModel
	{
		public ExerciseType Type { get; set; }
		public Language Language { get; set; }
		public string TypeName { get; set; }
		public string SubTypeName { get; set; }
	}
	
	public class ExerciseVariant : BaseModel
	{
		public Exercise Exercise { get; set; }
		public ExerciseType Type { get; set; }
		public IList<ExerciseVariantLanguage> Languages { get; set; }
	}
	
	public class ExerciseVariantLanguage : BaseModel
	{
		public ExerciseVariant Variant { get; set; }
		public string File { get; set; }
		public int Size { get; set; }
		public string Content { get; set; }
		public int ExerciseWindowX { get; set; }
		public int ExerciseWindowY { get; set; }
	}
}
