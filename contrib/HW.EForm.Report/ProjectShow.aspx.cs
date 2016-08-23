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
    public partial class ProjectShow : System.Web.UI.Page
    {
    	ProjectService s = new ProjectService();
    	protected Project project;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            Show(ConvertHelper.ToInt32(Request.QueryString["ProjectID"]));
        }
        
        public void Show(int projectID)
        {
        	project = s.ReadProject(projectID, ConvertHelper.ToInt32(Session["ManagerID"]));
        }
    }
}