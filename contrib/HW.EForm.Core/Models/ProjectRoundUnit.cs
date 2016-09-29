using System;
using System.Collections.Generic;

namespace HW.EForm.Core.Models
{
	public class ProjectRoundUnit
	{
		int langID;
		Survey survey;
		IList<AnswerValue> answerValues;
		
		public ProjectRoundUnit()
		{
			Options = new List<QuestionOption>();
			Answers = new List<Answer>();
		}
		
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
		public Report Report { get; set; }
		public ProjectRound ProjectRound { get; set; }
		public IList<ProjectRoundUnitManager> Managers { get; set; }
		public IList<QuestionOption> Options { get; set; }
		public IList<Answer> Answers { get; set; }
		public bool HasOnlyOneOption {
			get { return Options.Count == 1; }
		}
		
		public int LangID {
			get {
				if (langID == 0 && ProjectRound != null) {
					return ProjectRound.LangID;
				}
				return langID;
			}
			set { langID = value; }
		}
		
		public Survey Survey {
			get {
				if (survey == null) {
					return ProjectRound.Survey;
				}
				return survey;
			}
			set { survey = value; }
		}
		
		public QuestionOption FirstOption {
			get {
				if (Options.Count > 0) {
					return Options[0];
				}
				return null;
			}
		}
		
		public IList<AnswerValue> AnswerValues {
			get { return answerValues; }
			set { answerValues = value; SetAnswerValuesForComponents(); }
		}
		
		public void AddAnswer(Answer a)
		{
			a.ProjectRoundUnit = this;
			Answers.Add(a);
		}
		
		public void AddOption(QuestionOption qo)
		{
			Options.Add(qo);
		}
		
		public IList<Question> GetQuestions()
		{
			var questions = new List<Question>();
			foreach (var a in Answers) {
				foreach (var av in a.Values) {
					if (!ExistsIn(questions, av.Question)) {
						questions.Add(av.Question);
					}
				}
			}
			return questions;
		}
		
		bool ExistsIn(IList<Question> questions, Question question)
		{
			foreach (var q in questions) {
				if (q.QuestionID == question.QuestionID) {
					return true;
				}
			}
			return false;
		}
		
		void SetAnswerValuesForComponents()
		{
			foreach (var qo in Options) {
				foreach (var oc in qo.Option.Components) {
					if (qo.Option.IsVAS) {
						oc.OptionComponent.AnswerValues = AnswerValues;
					} else {
						oc.OptionComponent.AnswerValues = GetAnswerValuesForComponent(oc.OptionComponentID);
					}
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
