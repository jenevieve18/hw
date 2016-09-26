using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class QuestionOption
	{
		public QuestionOption() : this(null)
		{
		}
		
		public QuestionOption(Option option)
		{
			this.Option = option;
			AnswerValues = new List<AnswerValue>();
		}
		
		public int QuestionOptionID { get; set; }
		public int QuestionID { get; set; }
		public int OptionID { get; set; }
		public int OptionPlacement { get; set; }
		public int SortOrder { get; set; }
		public string Variablename { get; set; }
		public int Forced { get; set; }
		public int Hide { get; set; }
		public Option Option { get; set; }
		public Question Question { get; set; }
		public WeightedQuestionOption WeightedQuestionOption { get; set; }
		public bool HasWeightedQuestionOption {
			get { return WeightedQuestionOption != null; }
		}
		
		public IList<AnswerValue> AnswerValues { get; set; }
		
		public void AddAnswerValue(AnswerValue av)
		{
			AnswerValues.Add(av);
		}
		
		public IList<AnswerValue> GetAnswerValues(int projectRoundUnitID)
		{
			var values = new List<AnswerValue>();
			foreach (var v in AnswerValues) {
				if (v.Answer.ProjectRoundUnit.ProjectRoundUnitID == projectRoundUnitID) {
					values.Add(v);
				}
			}
			return values;
		}
		
		public IList<AnswerValue> GetAnswerValues(int projectRoundUnitID, int optionComponentID)
		{
			var values = new List<AnswerValue>();
			foreach (var av in AnswerValues) {
				if (av.Answer.ProjectRoundUnit.ProjectRoundUnitID == projectRoundUnitID && av.OptionComponent.OptionComponentID == optionComponentID) {
					values.Add(av);
				}
			}
			return values;
		}
		
		double GetTotalExportValue()
		{
			double total = 0;
			foreach (var v in AnswerValues) {
				total += v.OptionComponent.ExportValue;
			}
			return total;
		}
		
		public double GetMean()
		{
			return GetTotalExportValue() / AnswerValues.Count;
		}
	}
}
