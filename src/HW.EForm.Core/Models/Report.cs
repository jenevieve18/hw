using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class Report
	{
		public Report()
		{
			Parts = new List<ReportPart>();
		}
		
		public int ReportID { get; set; }
		public string Internal { get; set; }
		public Guid ReportKey { get; set; }
		public IList<ReportPart> Parts { get; set; }
		
		public void AddPart(ReportPart part)
		{
			part.Report = this;
			Parts.Add(part);
		}
		
		public int CountQuestions()
		{
			int questions = 0;
			foreach (var p in Parts) {
				if (p.HasQuestionOption) {
					questions++;
				}
			}
			return questions;
		}
		
		public int CountIndexes()
		{
			int indexes = 0;
			foreach (var p in Parts) {
				if (p.HasComponents && p.FirstComponent.HasIndex) {
					indexes++;
				}
			}
			return indexes;
		}
		
		public int CountWeightedQuestionOptions()
		{
			int questions = 0;
			foreach (var p in Parts) {
				if (p.HasComponents && p.FirstComponent.HasWeightedQuestionOption) {
					questions++;
				}
			}
			return questions;
		}
	}
}
