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
	public partial class ProjectEdit : System.Web.UI.Page
	{
		protected Project project;
		ProjectService projectService = new ProjectService();
		SurveyService surveyService = new SurveyService();
		FeedbackService feedbackService = new FeedbackService();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			Show(ConvertHelper.ToInt32(Request.QueryString["ProjectID"]));
		}
		
		public void Show(int projectID)
		{
			project = projectService.ReadProject(projectID);
			if (project != null) {
				textBoxInternal.Text = project.Internal;
				textBoxName.Text = project.Name;
				foreach (var s in surveyService.FindAllSurveys()) {
					dropDownListSurvey.Items.Add(s.ToString());
				}
			}
		}
	}
}