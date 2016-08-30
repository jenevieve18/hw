// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Helpers
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
//				return GetMeanMedian(data.GetRange(0, data.Count / 2));
				return GetMeanMedian(data.GetRange(0, Math.Max(1, data.Count / 2)));
//				double q = 0.25 * (data.Count);
//				if (q % 1 == 0) {
//					return data[(int)q];
//				} else {
//					int index = (int)q;
//					double a = data[index - 1];
//					double b = data[index];
//					return a + 0.25 * (b - a);
//				}
			}
		}

		public double UpperBox {
			get {
//				int lower = data.Count % 2 != 0 ? data.Count / 2 + 1 : data.Count / 2;
//				return GetMeanMedian(data.GetRange(lower, data.Count / 2));
				int lower = data.Count % 2 != 0 && data.Count > 1 ? data.Count / 2 + 1 : data.Count / 2;
				return GetMeanMedian(data.GetRange(lower, Math.Max(1, data.Count / 2)));
//				double q = 0.75 * (data.Count);
//				if (q % 1 == 0) {
//					return data[(int)q];
//				} else {
//					int index = (int)q;
//					double a = data[index];
//					double b = data[index + 1];
//					return a + 0.5 * (b - a);
//				}
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
			get { return GetMeanMedian(data); }
		}

		public object[] YValues {
			get { return new object[] { LowerWhisker, UpperWhisker, LowerBox, UpperBox, Mean, Median }; }
		}

		public override string ToString()
		{
//			return string.Format("[Lower Whisker: {0}, Lower Box: {1}, Median: {2}, Upper Box: {3}, Upper Whisker: {4}, Mean: {5}]", LowerWhisker, LowerBox, Median, UpperBox, UpperWhisker, Mean);
			return string.Format("[{0},{1},{2},{3},{4}]", LowerWhisker, LowerBox, Median, UpperBox, UpperWhisker);
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

		double GetMeanMedian(List<double> data)
		{
//			LoggingService.Info(ToStr());
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
