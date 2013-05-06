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
	public partial class ReportPartLanguagesEdit : System.Web.UI.Page
	{
		IReportRepository r = AppContext.GetRepositoryFactory().CreateReportRepository();
		protected ReportPartLanguage part;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["ReportPartLangID"]);
			part = r.ReadReportPartLanguage(id);
			if (Request["Submit"] != null) {
				part.Subject = Request.Form["Subject"];
				part.Header = Request.Form["Header"];
				part.Footer = Request.Form["Footer"];
				r.SaveOrUpdate<ReportPartLanguage>(part);
				Response.Redirect("ReportPartsShow.aspx?ReportPartID=" + part.ReportPart.Id);
			}
		}
	}
}