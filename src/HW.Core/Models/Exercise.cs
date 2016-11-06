using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Exercise : BaseModel
	{
		public Exercise()
		{
			Languages = new List<ExerciseLanguage>();
		}
		
        public string Script { get; set; }
        public int Status { get; set; }
		public string Image { get; set; }
		public ExerciseCategory Category { get; set; }
		public ExerciseArea Area { get; set; }
		public int SortOrder { get; set; }
		public int Minutes { get; set; }
		public int RequiredUserLevel { get; set; }
		public string ReplacementHead { get; set; }
		public IList<ExerciseLanguage> Languages { get; set; }
		public IList<ExerciseVariant> Variants { get; set; }
		public bool PrintOnBottom { get; set; }
		
		public string AreaCategoryName {
			get {
				return string.Format("{0}{1}", Area.AreaName, Category.CategoryName != "" ? " - " + Category.CategoryName : "");
			}
		}
		
		public virtual ExerciseLanguage SelectedLanguage {
			get {
				if (Languages != null && Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		public ExerciseLanguage CurrentLanguage { get; set; }
		public ExerciseVariantLanguage CurrentVariant { get; set; }
		public ExerciseTypeLanguage CurrentType { get; set; }
		
		public void AddLanguage(string name, string time, string teaser, int langID)
		{
			var l = new ExerciseLanguage {
				ExerciseName = name,
				Time = time,
				Teaser = teaser,
				Language = new Language { Id = langID }
			};
			AddLanguage(l);
		}
		
		public void AddLanguage(ExerciseLanguage l)
		{
			l.Exercise = this;
			Languages.Add(l);
		}
	}
	
	public class ExerciseArea : BaseModel
	{
		public ExerciseArea()
		{
		}
		
		public ExerciseArea(int id, params ExerciseAreaLanguage[] languages)
		{
			this.Id = id;
			this.Languages = new List<ExerciseAreaLanguage>(languages);
		}
		
		public string Image { get; set; }
		public int SortOrder { get; set; }
		public IList<ExerciseAreaLanguage> Languages { get; set; }
		
		public ExerciseAreaLanguage SelectedLanguage {
			get {
				if (Languages != null && Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		public string AreaName {
			get {
				if (SelectedLanguage != null) {
					return SelectedLanguage.AreaName;
				}
				return "";
			}
		}
	}
	
	public class ExerciseAreaLanguage : BaseModel
	{
		public ExerciseAreaLanguage()
		{
		}
		
		public ExerciseAreaLanguage(string name)
		{
			this.AreaName = name;
		}
		
		public ExerciseArea Area { get; set; }
		public Language Language { get; set; }
		public string AreaName { get; set; }
		
		public override string ToString()
		{
			return AreaName;
		}
	}
	
	public class ExerciseCategory : BaseModel
	{
		public ExerciseCategory(params ExerciseCategoryLanguage[] languages)
		{
			this.Languages = new List<ExerciseCategoryLanguage>(languages);
		}
		
		public int SortOrder { get; set; }
		public IList<ExerciseCategoryLanguage> Languages { get; set; }
		
		public ExerciseCategoryLanguage SelectedLanguage {
			get {
				if (Languages != null && Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		public string CategoryName {
			get {
				if (SelectedLanguage != null) {
					return SelectedLanguage.CategoryName;
				}
				return "";
			}
		}
	}
	
	public class ExerciseCategoryLanguage : BaseModel
	{
		public ExerciseCategoryLanguage()
		{
		}
		
		public ExerciseCategoryLanguage(string name)
		{
			this.CategoryName = name;
		}
		
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
		public Exercise Exercise { get; set; }
		public string ExerciseName { get; set; }
		public string Time { get; set; }
		public string Teaser { get; set; }
		public string Content { get; set; }
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
		
		public ExerciseTypeLanguage SelectedLanguage {
			get {
				if (Languages != null && Languages.Count > 0) {
					return Languages[0];
				}
				return null;
			}
		}
		
		public bool HasContent()
		{
			return Id == Text || Id == Pdf;
		}
		
		public const int Text = 1;
		public const int Animation = 2;
		public const int AnimationMute = 3;
		public const int AnimationNonStop = 4;
		public const int Pdf = 5;
		public const int Sortable = 6;
		
		public bool IsText()
		{
			return Id == Text;
		}
	}
	
	public class ExerciseTypeLanguage : BaseModel
	{
		public ExerciseType Type { get; set; }
		public Language Language { get; set; }
		public string TypeName { get; set; }
		public string SubTypeName { get; set; }
		
		bool HasSubTypeName {
			get { return SubTypeName != null && SubTypeName != ""; }
		}
		
		string GetSubTypeName()
		{
			if (HasSubTypeName) {
				return string.Format(" ({0})", SubTypeName);
			}
			return "";
		}
		
		public override string ToString()
		{
			return string.Format("{0} {1}", TypeName, GetSubTypeName());
		}
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
		public Language Language { get; set; }
	}
	
//	public class ExerciseDataInput : BaseModel
//	{
//		public virtual Exercise Exercise { get; set; }
//	}
//	
//	public class ExerciseDataInputLang : BaseModel
//	{
//		public virtual ExerciseDataInput ExerciseDataInput { get; set; }
//		public virtual Language Language { get; set; }
//		public virtual string Content { get; set; }
//	}
}
