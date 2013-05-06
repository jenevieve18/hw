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
	public partial class SponsorAdminsAdd : System.Web.UI.Page
	{
		ISponsorRepository r = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request["Submit"] != null) {
				var a = new SponsorAdmin {
					Sponsor = new Sponsor { Id = Convert.ToInt32(Request["SponsorID"]) },
					Name = Request["Name"],
					Password = Request["Password"]
				};
				r.SaveOrUpdate(a);
				Response.Redirect("SponsorsShow.aspx?SponsorID=" + a.Sponsor.Id);
			}
		}
	}
}