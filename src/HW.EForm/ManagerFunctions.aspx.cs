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
	public partial class ManagerFunctions : System.Web.UI.Page
	{
		IManagerFunctionRepository r = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		protected IList<ManagerFunction> functions;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			functions = r.FindAll();
		}
	}
}