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
	public partial class SponsorsAdd : System.Web.UI.Page
	{
		ISponsorRepository r = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var s = new Sponsor {
					Name = Request["Name"]
				};
				r.SaveOrUpdate(s);
				Response.Redirect("Sponsors.aspx");
			}
		}
	}
}