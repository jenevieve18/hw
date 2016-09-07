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
    public partial class FeedbackQuestionImage : System.Web.UI.Page
    {
        FeedbackService s = new FeedbackService();
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
        	Show(feedbackID, projectRoundID, projectRoundUnitID);
        }

        public void Show(int feedbackID, int projectRoundID, int projectRoundUnitID)
        {
        	feedback = s.ReadFeedbackWithAnswers(feedbackID, projectRoundID, new int[] { projectRoundUnitID });
        }
    }
}