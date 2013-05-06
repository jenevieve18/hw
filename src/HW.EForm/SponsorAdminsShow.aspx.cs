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
	public partial class SponsorAdminsShow : System.Web.UI.Page
	{
		ISponsorRepository sr = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IManagerFunctionRepository fr = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		IDepartmentRepository dr = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		protected SponsorAdmin admin;
		protected IList<ManagerFunction> functions;
		protected IList<Department> departments;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["SponsorAdminID"]);
			admin = sr.ReadSponsorAdmin(id);
			functions = fr.FindAll();
			departments = dr.FindAll();
		}
	}
}