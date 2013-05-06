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
	public partial class Reports : System.Web.UI.Page
	{
		protected IList<Report> reports;
		IReportRepository r = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			reports = r.FindAll();
		}
	}
}