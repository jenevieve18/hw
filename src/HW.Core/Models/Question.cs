//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

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
		
		public QuestionCategory Category { get; set; }
		public string VariableName { get; set; }
		public IList<QuestionLanguage> Languages { get; set; }
		public IList<Answer> Answers { get; set; }
	}
	
	public class QuestionCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class QuestionCategoryLanguage : BaseModel
	{
		public QuestionCategory Category { get; set; }
		public Language Language { get; set; }
	}
	
	public class QuestionCategoryQuestion : BaseModel
	{
		public QuestionCategory Category { get; set; }
		public Question Question { get; set; }
	}
	
	public class QuestionLanguage : BaseModel
	{
		public Question Question { get; set;}
		public Language Language { get; set; }
	}
	
	public class QuestionOption : BaseModel
	{
		public Question Question { get; set; }
		public Option Option { get; set; }
	}
	
	public class QuestionOptionRange : BaseModel
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public decimal LowValue { get; set; }
		public decimal HighValue { get; set; }
	}
	
	public class WeightedQuestionOption : BaseIndex
	{
		public Question Question { get; set; }
		public Option Option { get; set; }
		public IList<WeightedQuestionOptionLanguage> Languages { get; set; }
	}
	
	public class WeightedQuestionOptionLanguage : BaseModel
	{
		public WeightedQuestionOption Option { get; set; }
		public Language Language { get; set; }
		public string Question { get; set; }
	}
	
	public class BackgroundQuestion : BaseModel
	{
		public string Internal { get; set; }
		public int Type { get; set; }
		public string DefaultValue { get; set; }
		public int Comparison { get; set; }
		public string Variable { get; set; }
		public IList<BackgroundQuestionLanguage> Languages { get; set; }
		public int Restricted { get; set; }
		public IList<BackgroundAnswer> Answers { get; set; }
	}
	
	public class BackgroundQuestionLanguage : BaseModel
	{
		public Language Language { get; set; }
		public BackgroundQuestion Question { get; set; }
	}
	
	public class BackgroundQuestionVisibility : BaseModel
	{
		public BackgroundQuestion Question { get; set; }
		public BackgroundAnswer Answer { get; set; }
		public BackgroundQuestion Child { get; set; }
	}
}
