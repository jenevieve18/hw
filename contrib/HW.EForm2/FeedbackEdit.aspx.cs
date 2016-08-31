using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class FeedbackEdit : System.Web.UI.Page
    {
    	FeedbackService s = new FeedbackService();
    	protected Feedback feedback;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	feedback = s.ReadFeedback(ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]));
        }
    }
}