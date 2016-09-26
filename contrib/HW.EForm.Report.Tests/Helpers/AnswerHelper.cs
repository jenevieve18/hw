// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.EForm.Core.Models;

namespace HW.EForm.Report.Tests.Helpers
{
	public static class AnswerHelper
	{
		public static double GetMean(this Answer answer)
		{
			return GetTotalValueInt(answer) / answer.AnswerValues.Count;
		}
		
		static double GetTotalValueInt(this Answer answer)
		{
			double total = 0;
			foreach (var v in answer.AnswerValues) {
				total += v.ValueInt;
			}
			return total;
		}
	}
}
