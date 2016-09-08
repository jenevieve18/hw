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
	public partial class FeedbackQuestionImage : System.Web.UI.Page
	{
//		FeedbackService s = new FeedbackService();
		FeedbackService s = new FeedbackService(new SqlFeedbackRepository(),
		                                        new SqlFeedbackQuestionRepository(),
		                                        new SqlQuestionRepository(),
		                                        new SqlQuestionOptionRepository(),
		                                        new SqlQuestionLangRepository(),
		                                        new SqlWeightedQuestionOptionRepository(),
		                                        new SqlOptionRepository(),
		                                        new SqlOptionComponentsRepository(),
		                                        new SqlOptionComponentRepository(),
		                                        new SqlOptionComponentLangRepository(),
		                                        new SqlProjectRoundUnitRepository(),
		                                        new SqlAnswerValueRepository());
		protected Feedback feedback;
		protected int feedbackID;
		protected int projectRoundID;
		protected int projectRoundUnitID;
		
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
			feedback = s.ReadFeedbackWithAnswers(feedbackID, projectRoundID, new int[] { projectRoundUnitID }, langID);
		}
	}
}