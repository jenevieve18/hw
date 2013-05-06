using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.EForm
{
	public partial class ProjectsAdd : System.Web.UI.Page
	{
		IProjectRepository r = AppContext.GetRepositoryFactory().CreateProjectRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var p = new Project {
					Internal = Request["Internal"]
				};
				r.SaveOrUpdate(p);
				Response.Redirect("Projects.aspx");
			}
		}
	}
}