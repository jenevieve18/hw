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
		
		
		public int QuestionID { get; set; }
		public string VariableName { get; set; }
		public int OptionsPlacement { get; set; }
		public int FontFamily { get; set; }
		public int FontSize { get; set; }
		public int FontDecoration { get; set; }
		public string FontColor { get; set; }
		public int Underlined { get; set; }
		public int QuestionContainerID { get; set; }
		public string Internal { get; set; }
		public int Box { get; set; }
		public int LimitWidth { get; set; }
		public string FillRemainderWithBgColor { get; set; }
		public int Niner { get; set; }
		
		public QuestionCategory Category { get; set; }
		public QuestionContainer Container { get; set; }
		public IList<QuestionLanguage> Languages { get; set; }
		public IList<Answer> Answers { get; set; }
	}
	
	public class QuestionCategory : BaseModel
	{
		public string Internal { get; set; }
	}
	
	public class QuestionContainer : BaseModel
	{
		public string Container { get; set; }
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
		public string QuestionText { get; set; }
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
		public string Internal { get; set; }
	}
	
	public class WeightedQuestionOptionLanguage : BaseModel
	{
		public WeightedQuestionOption Option { get; set; }
		public Language Language { get; set; }
		public string Question { get; set; }
	}
}
