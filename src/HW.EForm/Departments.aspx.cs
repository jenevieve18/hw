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
	public partial class Departments : System.Web.UI.Page
	{
		IDepartmentRepository r = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		protected IList<Department> departments;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			departments = r.FindAll();
		}
	}
}