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
	public partial class ReportsEdit : System.Web.UI.Page
	{
		IReportRepository r = AppContext.GetRepositoryFactory().CreateReportRepository();
		protected Report report;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["ReportID"]);
			report = r.Read(id);
		}
	}
}