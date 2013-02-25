﻿//	<file>
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
		public ProjectRound ProjectRound { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
		public Language Language { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public IList<AnswerValue> Values { get; set; }
		
		public HWList GetIntValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add((double)v.ValueInt);
			}
			return new HWList(n);
		}
		
		public float Average { get; set; }
		public int DummyValue1 { get; set; } // TODO: This is used by dbo.cf_yearWeek and related methods
		public int DummyValue2 { get; set; }
		public int DummyValue3 { get; set; }
		public float Max {
			get { return max > 100 ? 100 : max; }
			set { max = value; }
		}
		public float Min {
			get { return min < 0 ? 0 : min; }
			set { min = value; }
		}
		public string SomeString { get; set; } // TODO: From cf_yearMonthDay(a.EndDT) function
		public int DT { get; set; } // TODO: From cf_yearWeek(a.EndDT) function
		public float AverageV { get; set; }
		public int CountV { get; set; }
		public float StandardDeviation { get; set; }
		
		public double LowerWhisker { get; set; }
		public double UpperWhisker { get; set; }
		public double LowerBox { get; set; }
		public double UpperBox { get; set; }
		public double Median { get; set; }
	}
	
	public class AnswerValue : BaseModel
	{
		public Answer Answer { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
		public int ValueInt { get; set; }
		public decimal ValueDecimal { get; set; }
		public DateTime ValueDateTime { get; set; }
		public DateTime Created { get; set; }
		public string ValueText { get; set; }
		public string ValueTextJapaneseUnicode { get; set; }
	}
	
	public class BackgroundAnswer : BaseModel
	{
		public string Internal { get; set; }
		public int SortOrder { get; set; }
		public int Value { get; set; }
		public IList<BackgroundAnswerLanguage> Languages { get; set; }
	}
	
	public class BackgroundAnswerLanguage : BaseModel
	{
		public Language Language { get; set; }
		public BackgroundAnswer Answer { get; set; }
	}
}
