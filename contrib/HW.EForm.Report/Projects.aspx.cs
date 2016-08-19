using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.EForm.Core.Helpers;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;
using HW.EForm.Core.Services;

namespace HW.EForm.Report
{
    public partial class Projects : System.Web.UI.Page
    {
    	protected IList<Project> projects;
    	ProjectService s = new ProjectService();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	Index();
        }
        
        public void Index()
        {
        	projects = s.FindByManager(ConvertHelper.ToInt32(Session["ManagerID"]));
        }
    }
}