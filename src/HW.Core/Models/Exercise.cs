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
		public virtual IList<ExerciseVariant> Variants { get; set; }
		
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
		public virtual Language Language { get; set; }
		public virtual ExerciseCategory Category { get; set; }
		public virtual string CategoryName { get; set; }
		
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
		public virtual User User { get; set; }
		public virtual DateTime Time { get; set; }
		public virtual DateTime TimeChanged { get; set; }
		public virtual string MiracleDescription { get; set; }
		public virtual bool AllowPublished { get; set; }
		public virtual bool Published { get; set; }
	}
	
	public class ExerciseStats : BaseModel
	{
		public virtual User User { get; set; }
		public virtual ExerciseVariantLanguage VariantLanguage { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual UserProfile UserProfile { get; set; }
	}
	
	public class ExerciseType : BaseModel
	{
		public virtual int SortOrder { get; set; }
		public virtual IList<ExerciseTypeLanguage> Languages { get; set; }
	}
	
	public class ExerciseTypeLanguage : BaseModel
	{
		public virtual ExerciseType Type { get; set; }
		public virtual Language Language { get; set; }
		public virtual string TypeName { get; set; }
		public virtual string SubTypeName { get; set; }
	}
	
	public class ExerciseVariant : BaseModel
	{
		public virtual Exercise Exercise { get; set; }
		public virtual ExerciseType Type { get; set; }
		public virtual IList<ExerciseVariantLanguage> Languages { get; set; }
	}
	
	public class ExerciseVariantLanguage : BaseModel
	{
		public virtual ExerciseVariant Variant { get; set; }
		public virtual string File { get; set; }
		public virtual int Size { get; set; }
		public virtual string Content { get; set; }
		public virtual int ExerciseWindowX { get; set; }
		public virtual int ExerciseWindowY { get; set; }
		public virtual Language Language { get; set; }
	}
}
