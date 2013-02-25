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
		public string Image { get; set; }
		public ExerciseCategory Category { get; set; }
		public ExerciseArea Area { get; set; }
		public int SortOrder { get; set; }
		public int Minutes { get; set; }
		public int RequiredUserLevel { get; set; }
		public string ReplacementHead { get; set; }
		public IList<ExerciseLanguage> Languages { get; set; }
		
		public ExerciseLanguage CurrentLanguage { get; set; }
		public ExerciseAreaLanguage CurrentArea { get; set; }
		public ExerciseCategoryLanguage CurrentCategory { get; set; }
		public ExerciseVariantLanguage CurrentVariant { get; set; }
		public ExerciseTypeLanguage CurrentType { get; set; }
	}
	
	public class ExerciseArea : BaseModel
	{
		public string Image { get; set; }
		public int SortOrder { get; set; }
		public IList<ExerciseAreaLanguage> Languages { get; set; }
	}
	
	public class ExerciseAreaLanguage : BaseModel
	{
		public ExerciseArea Area { get; set; }
		public Language Language { get; set; }
		public string AreaName { get; set; }
	}
	
	public class ExerciseCategory : BaseModel
	{
		public int SortOrder { get; set; }
		public IList<ExerciseCategoryLanguage> Languages { get; set; }
	}
	
	public class ExerciseCategoryLanguage : BaseModel
	{
		public Language Language { get; set; }
		public ExerciseCategory Category { get; set; }
		public string CategoryName { get; set; }
	}
	
	public class ExerciseLanguage : BaseModel
	{
		public Exercise Exercise { get; set; }
		public string ExerciseName { get; set; }
		public string Time { get; set; }
		public string Teaser { get; set; }
		public Language Language { get; set; }
		public bool IsNew { get; set; }
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
