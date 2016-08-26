using System;
using System.Collections.Generic;
using HW.EForm.Core.Helpers;

namespace HW.EForm.Core.Models
{
	public class Question
	{
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

		public Question()
		{
			Languages = new List<QuestionLang>();
			Options = new List<QuestionOption>();
		}
		
		public IList<QuestionOption> Options { get; set; }
		public IList<QuestionLang> Languages { get; set; }
		public IList<AnswerValue> AnswerValues { get; set; }
		
		public QuestionLang GetLanguage(int langID)
		{
			foreach (var l in Languages) {
				if (l.LangID == langID) {
					return l;
				}
			}
			return null;
		}
		
//		public void AddLanguage(int langID, string question)
//		{
//			Languages.Add(new QuestionLang(langID, question));
//		}
//		
//		public void AddOption(Option option)
//		{
//			Options.Add(new QuestionOption(option));
//		}
		
		public Chart lalala()
		{
			var c = new ColumnChart { Title = GetLanguage(1).Question };
			foreach (var qo in Options) {
				foreach (var oc in qo.Option.Components) {
					c.Categories.Add(oc.Component.Internal);
				}
			}
			return c;
		}
		
		public Chart ToChart()
		{
			var c = new LineChart { Title = GetLanguage(1).Question };
			c.Categories = new List<string>(new[] { "Jan", "Feb" });
			foreach (var o in Options) {
				var d = new List<double>();
				foreach (var x in o.Option.AnswerValues) {
					d.Add(x.GetValueInt());
				}
				c.Series.Add(new Series("test", d));
			}
			return c;
		}
	}
}
