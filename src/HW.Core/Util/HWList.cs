using System;
using System.Collections.Generic;
using HW.Core.Services;

namespace HW.Core.Util
{
	public interface IHWList
	{
		double Mean { get; }
		double LowerWhisker { get; }
		double UpperWhisker { get; }
		double LowerBox { get; }
		double UpperBox { get; }
		double Median { get; }
	}
	
	public class HWList : List<double>, IHWList
	{
		List<double> data;

		public HWList(params double[] data) : this(new List<double>(data))
		{
		}

		public HWList(IEnumerable<double> data)
		{
			this.data = new List<double>(data);
			this.data.Sort();
		}
		
		public double Max {
			get { return data[data.Count - 1]; }
		}
		
		public double Min {
			get { return data[0]; }
		}

		public List<double> Data {
			get { return data; }
		}

		public double LowerWhisker {
			get { return data[0]; }
		}
		
		public double NerdLowerWhisker {
			get { return Math.Max(LowerWhisker, LowerBox - 1.5 * (UpperBox - LowerBox)); }
		}

		public double UpperWhisker {
			get { return data[data.Count - 1]; }
		}
		
		public double NerdUpperWhisker {
			get { return Math.Min(UpperWhisker, UpperBox + 1.5 * (UpperBox - LowerBox)); }
		}

		public double LowerBox {
			get {
				var rangeData = data.GetRange(0, Math.Max(1, data.Count / 2));
				return GetMedian(rangeData);
			}
		}

		public double UpperBox {
			get {
				int lower = data.Count % 2 != 0 && data.Count > 1 ? data.Count / 2 + 1 : data.Count / 2;
				var rangeData = data.GetRange(lower, Math.Max(1, data.Count / 2));
				return GetMedian(rangeData);
			}
		}
		
		public double ConfidenceInterval {
			get { return StandardDeviation * 1.96; }
		}
		
		public double StandardDeviation {
			get {
				double total = 0;
				foreach (var n in data) {
					total += Math.Pow(n - Mean, 2);
				}
				return Math.Sqrt(total / data.Count);
			}
		}

		public double Mean {
			get {
				double sum = 0;
				foreach (var d in data) {
					sum += d;
				}
				return sum / data.Count;
			}
		}

		public double Median {
			get { return GetMedian(data); }
		}

//		public object[] YValues {
//			get { return new object[] { LowerWhisker, UpperWhisker, LowerBox, UpperBox, Mean, Median }; }
//		}

		public override string ToString()
		{
			return string.Format("[Lower Whisker: {0}, Lower Box: {1}, Median: {2}, Upper Box: {3}, Upper Whisker: {4}, Mean: {5}]", LowerWhisker, LowerBox, Median, UpperBox, UpperWhisker, Mean);
		}
		
		public string ToStr()
		{
			string s = "[";
			int i = 0;
			foreach (var d in data) {
				s += d.ToString();
				if (i++ < data.Count - 1) {
					s += ",";
				}
			}
			s += "]";
			return s;
		}

		double GetMedian(List<double> data)
		{
			if (data.Count % 2 != 0) {
				return data[data.Count / 2];
			} else {
				double upper = data[data.Count / 2];
				double lower = data[data.Count / 2 - 1];
				return (lower + upper) / 2;
			}
		}
	}
}
