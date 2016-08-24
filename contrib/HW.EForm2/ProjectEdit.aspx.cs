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
		ProjectService s = new ProjectService();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			project = s.ReadProject(ConvertHelper.ToInt32(Request.QueryString["ProjectID"]));
			if (project != null) {
				textBoxInternal.Text = project.Internal;
				textBoxName.Text = project.Name;
				foreach (var x in project.Surveys) {
					dropDownListSurvey.Items.Add(x.Survey.ToString());
				}
			}
		}
	}
}