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
    public partial class FeedbackAdd : System.Web.UI.Page
    {
    	FeedbackService s =  new FeedbackService();
    	
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            var f = new Feedback {
        		FeedbackText = textBoxFeedback.Text
            };
        	s.SaveFeedback(f);
            Response.Redirect("feedbacks.aspx");
        }
    }
}