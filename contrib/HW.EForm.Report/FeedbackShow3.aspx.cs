using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
	public partial class FeedbackShow3 : System.Web.UI.Page
	{
		FeedbackService feedbackService = new FeedbackService();
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
			Show(feedbackID, projectRoundID, projectRoundUnitID);
		}

		public void Show(int feedbackID, int projectRoundID, int projectRoundUnitID)
		{
			feedback = feedbackService.ReadFeedback2(feedbackID, projectRoundID, new int[] { projectRoundUnitID });
			
			charts.Add(HighchartsBoxplot.GetHighchartsChart(9, feedback.ToChart(false)));
			foreach (var fq in feedback.Questions) {
				charts.Add(HighchartsBoxplot.GetHighchartsChart(fq.Question.Options[0].Option.OptionType, fq.ToChart(true)));
			}
			
			var questions = questionService.FindQuestion(new int[] { 380, 381, 382, 383, 384, 459, 460, 461, 462, 463 }, 13, new int[] { 97 });
			foreach (var q in questions) {
				charts.Add(HighchartsBoxplot.GetHighchartsChart(q.Options[0].Option.OptionType, q.ToChart(true)));
			}
		}
	}
}