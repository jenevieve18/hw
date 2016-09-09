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
	public partial class FeedbackShow2 : System.Web.UI.Page
	{
//		FeedbackService s = new FeedbackService(new SqlFeedbackRepository(),
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
		FeedbackService s = ServiceFactory.CreateFeedbackService();
		protected Feedback feedback;
		protected int projectRoundID;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["ManagerID"] == null, "default.aspx");
			int feedbackID = ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]);
			projectRoundID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]);
			int projectRoundUnitID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundUnitID"]);
			int langID = 1;
			
			Show(feedbackID, projectRoundID, projectRoundUnitID, langID);
		}

		public void Show(int feedbackID, int projectRoundID, int projectRoundUnitID, int langID)
		{
			feedback = s.ReadFeedbackWithAnswers2(feedbackID, projectRoundID, new int[] { projectRoundUnitID }, langID);
		}
	}
}