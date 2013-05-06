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
	public partial class Projects : System.Web.UI.Page
	{
		IProjectRepository r = AppContext.GetRepositoryFactory().CreateProjectRepository();
		protected IList<Project> projects;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			projects = r.FindAll();
		}
	}
}