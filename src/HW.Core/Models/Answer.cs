using System;
using System.Collections.Generic;
using HW.Core.Helpers;
using HW.Core.Util;

namespace HW.Core.Models
{
	public interface IMinMax
	{
		float Min { get; set; }
		float Max { get; set; }
	}
	
	public interface IAnswer
	{
		IList<IValue> Values { get; set; }
//		HWList GetIntValues();
		HWList GetDoubleValues();
	}
	
	public interface IValue
	{
//		int ValueInt { get; set; }
		decimal ValueDecimal { get; set; }
		string ValueText { get; set; }
		
		double ValueDouble { get; set; }
	}
	
	public class Answer : BaseModel, IMinMax, IAnswer
	{
		float min = 0;
		float max = 100;
		
		public Answer()
		{
			Values = new List<IValue>();
		}
		
		public ProjectRound ProjectRound { get; set; }
		public int ProjectRoundID { get; set; }
		public ProjectRoundUnit ProjectRoundUnit { get; set; }
		public int ProjectRoundUnitID { get; set; }
		public ProjectRoundUser ProjectRoundUser { get; set; }
		public int ProjectRoundUserID { get; set; }
		public Language Language { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public IList<IValue> Values { get; set; }
		public int CurrentPage { get; set; }
		
		public float Average { get; set; }
		public int DummyValue1 { get; set; } // TODO: This is used by dbo.cf_yearWeek and related methods
		public int DummyValue2 { get; set; }
		public int DummyValue3 { get; set; }
		
		public float Max {
//			get { return max > 100 ? 100 : max; }
			get { return max; }
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
		
		public void AddValue(IValue value)
		{
			Values.Add(value);
		}
		
//		public HWList GetIntValues()
//		{
//			List<double> n = new List<double>();
//			foreach (var v in Values) {
//				n.Add((double)v.ValueInt);
//			}
//			return new HWList(n);
//		}
//		
		public HWList GetDoubleValues()
		{
			List<double> n = new List<double>();
			foreach (var v in Values) {
				n.Add(v.ValueDouble);
			}
			return new HWList(n);
		}
	}
	
	public class AnswerValue : BaseModel, IValue
	{
		public Answer Answer { get; set; }
		public Question Question { get; set; }
		public Option Option { get; set; }
//		public int ValueInt { get; set; }
		public decimal ValueDecimal { get; set; }
		public DateTime ValueDateTime { get; set; }
		public DateTime Created { get; set; }
		public string ValueText { get; set; }
		public string ValueTextJapaneseUnicode { get; set; }
		
		public double ValueDouble { get; set; }
	}
}
