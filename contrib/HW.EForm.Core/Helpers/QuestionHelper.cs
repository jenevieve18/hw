// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Helpers
{
	public static class QuestionHelper
	{
		public static Chart ToChart(this Question question)
		{
			var c = new ColumnChart { Title = question.GetLanguage(1).Question };
			foreach (var qo in question.Options) {
				var d = new List<double>();
				foreach (var oc in qo.Option.Components) {
					c.Categories.Add(oc.OptionComponent.Internal);
					d.Add(oc.OptionComponent.AnswerValues.Count);
				}
				c.Series.Add(new Series("InternalShit", d));
			}
			return c;
		}
	}
}
