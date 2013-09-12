//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Helpers;

namespace HW.Core.Models
{
	public interface IMinMax
	{
		float Min { get; set; }
		float Max { get; set; }
	}
	
	public class Answer : BaseModel, IMinMax
	{
		public Answer()
		{
			Values = new List<AnswerValue>();
		}
		
		float min = 0;
		float max = 100;
		public virtual ProjectRound ProjectRound { get; set; }
		public virtual ProjectRoundUnit ProjectRoundUnit { get; set; }
		public virtual ProjectRoundUser ProjectRoundUser { get; set; }
		public virtual Language Language { get; set; }
		public virtual DateTime StartDate { get; set; }
		public virtual DateTime EndDate { get; set; }
		public virtual IList<AnswerValue> Values { get; set; }
		
		public virtual HWList GetIntValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add((double)v.ValueInt);
			}
			return new HWList(n);
		}
		
		public virtual float Average { get; set; }
		public virtual int DummyValue1 { get; set; } // TODO: This is used by dbo.cf_yearWeek and related methods
		public virtual int DummyValue2 { get; set; }
		public virtual int DummyValue3 { get; set; }
		public virtual float Max {
			get { return max > 100 ? 100 : max; }
			set { max = value; }
		}
		public virtual float Min {
			get { return min < 0 ? 0 : min; }
			set { min = value; }
		}
		public virtual string SomeString { get; set; } // TODO: From cf_yearMonthDay(a.EndDT) function
		public virtual int DT { get; set; } // TODO: From cf_yearWeek(a.EndDT) function
		public virtual float AverageV { get; set; }
		public virtual int CountV { get; set; }
		public virtual float StandardDeviation { get; set; }
		
		public virtual double LowerWhisker { get; set; }
		public virtual double UpperWhisker { get; set; }
		public virtual double LowerBox { get; set; }
		public virtual double UpperBox { get; set; }
		public virtual double Median { get; set; }
	}
	
	public class AnswerValue : BaseModel
	{
		public virtual Answer Answer { get; set; }
		public virtual Question Question { get; set; }
		public virtual Option Option { get; set; }
		public virtual int ValueInt { get; set; }
		public virtual decimal ValueDecimal { get; set; }
		public virtual DateTime ValueDateTime { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual string ValueText { get; set; }
		public virtual string ValueTextJapaneseUnicode { get; set; }
	}
	
	public class BackgroundAnswer : BaseModel
	{
		public virtual BackgroundQuestion BackgroundQuestion { get; set; }
		public virtual string Internal { get; set; }
		public virtual int SortOrder { get; set; }
		public virtual int Value { get; set; }
		public virtual IList<BackgroundAnswerLanguage> Languages { get; set; }
	}
	
	public class BackgroundAnswerLanguage : BaseModel
	{
		public virtual Language Language { get; set; }
		public virtual BackgroundAnswer Answer { get; set; }
	}
}
