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
	public partial class ReportPartsShow : System.Web.UI.Page
	{
		IReportRepository rr = AppContext.GetRepositoryFactory().CreateReportRepository();
		ILanguageRepository lr = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		protected ReportPart part;
		protected IList<Language> languages;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(Request["ReportPartID"]);
			part = rr.ReadReportPart(id, 1);
			languages = lr.FindAll();
		}
	}
}