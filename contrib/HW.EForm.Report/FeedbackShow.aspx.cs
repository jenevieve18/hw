﻿using System;
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
    public partial class FeedbackShow : System.Web.UI.Page
    {
    	FeedbackService s = new FeedbackService();
    	protected Feedback feedback;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	HtmlHelper.RedirectIf(Session["ManagerID"] == null, "default.aspx");
        	Show(
                ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]),
                ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]),
                ConvertHelper.ToInt32(Request.QueryString["ProjectRoundUnitID"])
            );
        }

        public void Show(int feedbackID, int projectRoundID, int projectRoundUnitID)
        {
        	feedback = s.ReadFeedback2(feedbackID, projectRoundID, new int[] { projectRoundUnitID });
        }
    }
}