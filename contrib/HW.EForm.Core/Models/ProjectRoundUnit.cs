using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ProjectRoundUnit
	{
		public int ProjectRoundUnitID { get; set; }
		public int ProjectRoundID { get; set; }
		public string Unit { get; set; }
		public string ID { get; set; }
		public int ParentProjectRoundUnitID { get; set; }
		public int SortOrder { get; set; }
		public string SortString { get; set; }
		public int SurveyID { get; set; }
		public Guid UnitKey { get; set; }
		public int UserCount { get; set; }
		public int UnitCategoryID { get; set; }
		public bool CanHaveUsers { get; set; }
		public int ReportID { get; set; }
		public int Timeframe { get; set; }
		public int Yellow { get; set; }
		public int Green { get; set; }
		public string SurveyIntro { get; set; }
		public bool Terminated { get; set; }
		public int IndividualReportID { get; set; }
		public string UniqueID { get; set; }
		public int RequiredAnswerCount { get; set; }
		int langID;
		
		public int LangID {
			get {
				if (langID == 0 && ProjectRound != null) {
					return ProjectRound.LangID;
				}
				return langID;
			}
			set { langID = value; }
		}

		public ProjectRoundUnit()
		{
		}
		
		Survey survey;
		
		public Survey Survey {
			get {
				if (survey == null) {
					return ProjectRound.Survey;
				}
				return survey;
			}
			set { survey = value; }
		}
		public Report Report { get; set; }
		public ProjectRound ProjectRound { get; set; }
		public IList<ProjectRoundUnitManager> Managers { get; set; }
		
		IList<AnswerValue> answerValues;
		public IList<QuestionOption> Options { get; set; }
		
		public IList<AnswerValue> AnswerValues {
			get { return answerValues; }
			set { answerValues = value; SetAnswerValuesForComponents(); }
		}
		
		void SetAnswerValuesForComponents()
		{
			foreach (var qo in Options) {
				foreach (var oc in qo.Option.Components) {
					oc.OptionComponent.AnswerValues = GetAnswerValuesForComponent(oc.OptionComponentID);
				}
			}
		}
		
		List<AnswerValue> GetAnswerValuesForComponent(int optionComponentID)
		{
			var answerValues = new List<AnswerValue>();
			foreach (var av in AnswerValues) {
				if (av.ValueInt == optionComponentID) {
					answerValues.Add(av);
				}
			}
			return answerValues;
		}
	}
}
