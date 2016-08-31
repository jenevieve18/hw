using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Models;
using HW.EForm.Core.Services;

namespace HW.EForm2
{
    public partial class Feedbacks : System.Web.UI.Page
    {
    	FeedbackService s = new FeedbackService();
    	protected IList<Feedback> feedbacks;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	feedbacks = s.FindAllFeedbacks();
        }
    }
}