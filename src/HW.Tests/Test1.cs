//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Tests
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			string s = @"52312	90
52312	86
52312	53
52312	50
52313	79
52313	92
52313	72
52313	62
52313	72
52313	76
52313	28
52313	36
52313	91
52313	100
52313	62
52313	59
52313	38
52313	80
52313	83
52313	73
52313	82
52314	84
52314	57
52314	69
52314	59
52314	100
52314	51
52314	82
52314	64
52315	49
52315	49
52315	30
52315	71
52315	100
52315	62
52315	41
52315	73
52315	39
52316	45
52316	41
52316	51
52316	100
52316	70
52316	50
52316	87
52316	80
52316	53
52317	12
52317	53
52317	48
52317	100
52317	21
52317	42
52317	73
52317	80
52317	54
52318	64
52318	76
52318	100
52318	35
52318	56
52318	81
52318	67
52318	59
52319	67
52319	28
52319	53
52319	63
52319	72
52319	100
52319	32
52319	85
52319	34
52319	59
52320	47
52320	69
52320	50
52320	82
52320	100
52320	28
52320	83
52320	83
52321	70
52321	37
52321	94
52321	54
52321	90
52321	81
52321	58
52321	74
52322	88
52322	58
52322	89
52322	75
52322	79
52323	87
52323	62
52323	84
52323	87
52324	85
52324	50
52324	67
52324	77
52325	85
52325	89
52325	61
52325	68
52327	92
52328	93
52328	54
52328	84
52328	72
52328	71
52329	93
52329	52
52329	84
52329	69
52329	76
52329	74
52330	91
52330	54
52331	50
52331	72
52331	64
52332	78
52333	59";
			var l = new List<X>();
			foreach (var i in s.Split('\n')) {
				var j = i.Split('\t');
				l.Add(new X { DT = Convert.ToInt32(j[0].Trim()), V = Convert.ToInt32(j[1].Trim()) });
			}
			foreach (var i in l) {
				Console.WriteLine(i);
			}
			bool done = false;
			var answers = new List<Answer>();
			int k = 0;
			while (!done) {
				var a = new Answer();
				int dt;
				do {
					dt = l[k].DT;
					a.Values.Add(new AnswerValue { ValueInt = l[k].V });
					k++;
					done = k == l.Count;
				} while (!done && l[k].DT == dt);
				answers.Add(a);
			}
			foreach (var a in answers) {
				Console.WriteLine("Count: " + a.Values.Count);
//				foreach (var v in a.Values) {
//					Console.WriteLine("\t" + v.ValueInt);
//				}
			}
		}
		
		class X
		{
			public int DT { get; set; }
			public int V { get; set; }
			public override string ToString()
			{
				return string.Format("DT: {0}, V: {1}", DT, V);
			}
		}
	}
}
