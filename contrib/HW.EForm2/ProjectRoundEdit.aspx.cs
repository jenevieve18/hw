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
    public partial class ProjectRoundEdit : System.Web.UI.Page
    {
        protected ProjectRound projectRound;
        ProjectService projectService = new ProjectService();
        SurveyService surveyService = new SurveyService();
        FeedbackService feedbackService = new FeedbackService();
        int projectRoundID;

        protected void Page_Load(object sender, EventArgs e)
        {
            projectRoundID = ConvertHelper.ToInt32(Request.QueryString["ProjectRoundID"]);
            projectRound = projectService.ReadProjectRound(projectRoundID);
            if (!IsPostBack) {
                if (projectRound != null) {
                    textBoxInternal.Text = projectRound.Internal;
                    foreach (var s in surveyService.FindAllSurveys()) {
                        dropDownListSurveys.Items.Add(new ListItem(s.ToString(), s.SurveyID.ToString()));
                    }
                    dropDownListSurveys.SelectedValue = projectRound.SurveyID.ToString();
				    foreach (var f in feedbackService.FindAllFeedbacks()) {
					    dropDownListFeedback.Items.Add(new ListItem(f.FeedbackText, f.FeedbackID.ToString()));
				    }
                    dropDownListFeedback.SelectedValue = projectRound.FeedbackID.ToString();
                }
            }
        }

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            var pr = new ProjectRound {
                Internal = textBoxInternal.Text,
                SurveyID = ConvertHelper.ToInt32(dropDownListSurveys.SelectedValue),
                FeedbackID = ConvertHelper.ToInt32(dropDownListFeedback.SelectedValue)
            };
            projectService.UpdateProjectRound(pr, projectRoundID);
        }
    }
}