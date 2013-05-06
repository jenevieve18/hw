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
	public partial class ReportPartLanguagesAdd : System.Web.UI.Page
	{
		IReportRepository rr = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var p = new ReportPartLanguage {
					ReportPart = new ReportPart { Id = Convert.ToInt32(Request["ReportPartID"]) },
					Language = new Language { Id = Convert.ToInt32(Request["Language"]) },
					Subject = Request["Subject"]
				};
				rr.SaveOrUpdateReportPartLanguage(p);
				Response.Redirect("ReportPartsShow.aspx?ReportPartID=" + p.ReportPart.Id);
			}
		}
	}
}