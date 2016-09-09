using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
	public partial class FeedbackShow3 : System.Web.UI.Page
	{
//		FeedbackService feedbackService = new FeedbackService(new SqlFeedbackRepository(),
//		                                        new SqlFeedbackQuestionRepository(),
//		                                        new SqlQuestionRepository(),
//		                                        new SqlQuestionOptionRepository(),
//		                                        new SqlQuestionLangRepository(),
//		                                        new SqlWeightedQuestionOptionRepository(),
//		                                        new SqlOptionRepository(),
//		                                        new SqlOptionComponentsRepository(),
//		                                        new SqlOptionComponentRepository(),
//		                                        new SqlOptionComponentLangRepository(),
//		                                        new SqlProjectRoundUnitRepository(),
//		                                        new SqlAnswerValueRepository());
		FeedbackService feedbackService = ServiceFactory.CreateFeedbackService();
		QuestionService questionService = new QuestionService();
		
		protected Feedback feedback;
		protected int feedbackID;
		protected int projectRoundID;
		protected int projectRoundUnitID;
		
		protected List<HighchartsChart> charts = new List<HighchartsChart>();

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["ManagerID"] == null, "default.aspx");
			feedbackID = ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]);
			projectRoundID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]);
			projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundUnitID"]);
			int langID = 1;
			
            Show(feedbackID, projectRoundID, projectRoundUnitID, langID);
		}

		public void Show(int feedbackID, int projectRoundID, int projectRoundUnitID, int langID)
		{
			feedback = feedbackService.ReadFeedbackWithAnswers2(feedbackID, projectRoundID, new int[] { projectRoundUnitID }, langID);
			
			charts.Add(HighchartsBoxplot.GetHighchartsChart(9, feedback.ToChart(false)));
			foreach (var fq in feedback.Questions) {
				charts.Add(HighchartsBoxplot.GetHighchartsChart(fq.Question.Options[0].Option.OptionType, fq.Question.ToChart(true)));
			}
			
//			AddQuestions(GetIndex1Questions(), projectRoundID, projectRoundUnitID);
			
			var painQuestions = new GroupedQuestions();
			painQuestions.Questions.AddRange(questionService.ReadQuestion2(1689, projectRoundID, new int[] { projectRoundUnitID }).ToQuestions());
			painQuestions.Questions.AddRange(questionService.ReadQuestion2(1690, projectRoundID, new int[] { projectRoundUnitID }).ToQuestions());
			painQuestions.Questions.AddRange(questionService.ReadQuestion2(1691, projectRoundID, new int[] { projectRoundUnitID }).ToQuestions());
			painQuestions.Questions.AddRange(questionService.ReadQuestion2(1692, projectRoundID, new int[] { projectRoundUnitID }).ToQuestions());
			
//			Console.WriteLine(HighchartsChart.GetHighchartsChart(9, painQuestions.ToChart(true)));
			charts.Add(HighchartsChart.GetHighchartsChart(9, painQuestions.ToChart(true)));
		}
		
		void AddQuestions(int[] questionIDs, int projectRoundID, int projectRoundUnitID)
		{
			var questions = questionService.FindQuestion(questionIDs, projectRoundID, new int[] { projectRoundUnitID });
			foreach (var q in questions) {
				charts.Add(HighchartsBoxplot.GetHighchartsChart(q.Options[0].Option.OptionType, q.ToChart(true)));
			}
		}
		
		int[] GetIndex1Questions()
		{
			return new int[] { 380, 381, 382, 383, 384, 459, 460, 461, 462, 463 };
		}

        int[] GetIndex3Questions()
        {
            return new int[] { 459, 460, 461, 462, 463 };
        }

        int[] GetIndex5Questions()
        {
            return new int[] { 387, 388, 389, 390, 391 };
        }
	}
}