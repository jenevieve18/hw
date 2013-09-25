using System;
using System.Collections.Generic;

namespace HW.Core.Models
{
	public class Question : BaseModel
	{
		public Question()
		{
			Answers = new List<Answer>();
		}
		
		public virtual QuestionCategory Category { get; set; }
		public virtual QuestionContainer Container { get; set; }
		public virtual string VariableName { get; set; }
		public virtual IList<QuestionLanguage> Languages { get; set; }
		public virtual IList<Answer> Answers { get; set; }
		public virtual string Internal { get; set; }
	}
	
	public class QuestionCategory : BaseModel
	{
		public virtual string Internal { get; set; }
	}
	
	public class QuestionContainer : BaseModel
	{
		public virtual string Container { get; set; }
	}
	
	public class QuestionCategoryLanguage : BaseModel
	{
		public virtual QuestionCategory Category { get; set; }
		public virtual Language Language { get; set; }
	}
	
	public class QuestionCategoryQuestion : BaseModel
	{
		public virtual QuestionCategory Category { get; set; }
		public virtual Question Question { get; set; }
	}
	
	public class QuestionLanguage : BaseModel
	{
		public virtual Question Question { get; set;}
		public virtual Language Language { get; set; }
		public virtual string QuestionText { get; set; }
	}
	
	public class QuestionOption : BaseModel
	{
		public virtual Question Question { get; set; }
		public virtual Option Option { get; set; }
	}
	
	public class QuestionOptionRange : BaseModel
	{
		public virtual DateTime Start { get; set; }
		public virtual DateTime End { get; set; }
		public virtual decimal LowValue { get; set; }
		public virtual decimal HighValue { get; set; }
	}
	
	public class WeightedQuestionOption : BaseIndex
	{
		public virtual Question Question { get; set; }
		public virtual Option Option { get; set; }
		public virtual IList<WeightedQuestionOptionLanguage> Languages { get; set; }
		public virtual string Internal { get; set; }
	}
	
	public class WeightedQuestionOptionLanguage : BaseModel
	{
		public virtual WeightedQuestionOption Option { get; set; }
		public virtual Language Language { get; set; }
		public virtual string Question { get; set; }
	}
	
	public class BackgroundQuestion : BaseModel
	{
		public virtual string Internal { get; set; }
		public virtual int Type { get; set; }
		public virtual string DefaultValue { get; set; }
		public virtual int Comparison { get; set; }
		public virtual string Variable { get; set; }
		public virtual int Restricted { get; set; }
		public virtual IList<BackgroundQuestionLanguage> Languages { get; set; }
		public virtual IList<BackgroundAnswer> Answers { get; set; }
	}
	
	public class BackgroundQuestionLanguage : BaseModel
	{
		public virtual Language Language { get; set; }
		public virtual BackgroundQuestion BackgroundQuestion { get; set; }
		public virtual string Question { get; set; }
	}
	
	public class BackgroundQuestionVisibility : BaseModel
	{
		public virtual BackgroundQuestion Question { get; set; }
		public virtual BackgroundAnswer Answer { get; set; }
		public virtual BackgroundQuestion Child { get; set; }
	}
}
