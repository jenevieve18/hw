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
    	FeedbackService feedbackService = new FeedbackService();
        QuestionService questionService = new QuestionService();
    	protected Feedback feedback;
        int feedbackID;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            feedbackID = ConvertHelper.ToInt32(Request.QueryString["FeedbackID"]);
        	feedback = feedbackService.ReadFeedback(feedbackID);
            var questions = questionService.FindAllQuestions();
            foreach (var q in questions)
            {
                dropDownListQuestions.Items.Add(new ListItem(q.Internal, q.QuestionID.ToString()));
            }
        }

        protected void buttonAddQuestion_Click(object sender, EventArgs e)
        {
            var fq = new FeedbackQuestion { 
                FeedbackID = feedbackID,
                QuestionID = ConvertHelper.ToInt32(dropDownListQuestions.SelectedValue)
            };
            feedbackService.SaveFeedbackQuestion(fq);
        }
    }
}