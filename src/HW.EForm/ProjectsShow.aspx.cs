﻿using System;
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
	public partial class ProjectsShow : System.Web.UI.Page
	{
		IProjectRepository r = AppContext.GetRepositoryFactory().CreateProjectRepository();
		protected Project project;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["ProjectID"]);
			project = r.Read(id);
		}
	}
}