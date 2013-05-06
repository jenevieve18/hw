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
	public partial class ReportPartsAdd : System.Web.UI.Page
	{
		IReportRepository r = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var p = new ReportPart {
					Report = new Report { Id = Convert.ToInt32(Request["ReportID"]) },
					Internal = Request["Internal"]
				};
				r.SaveOrUpdateReportPart(p);
				Response.Redirect("ReportsShow.aspx?ReportID=" + p.Report.Id);
			}
		}
	}
}