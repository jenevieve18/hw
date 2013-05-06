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
	public partial class ReportsAdd : System.Web.UI.Page
	{
		IReportRepository r = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var p = new Report {
					Internal = Request["Internal"]
				};
				r.SaveOrUpdate(p);
				Response.Redirect("Reports.aspx");
			}
		}
	}
}