using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HWgrp.Web
{
	public partial class Site : System.Web.UI.MasterPage
	{
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IManagerFunctionRepository managerFunctionRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		protected Sponsor sponsor;
		protected IList<ManagerFunction> functions;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			sponsor = sponsorRepository.X(sponsorID);
			
			int sponsorAdminID = HttpContext.Current.Session["SponsorAdminID"] != null ? Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) : -1;
			functions = managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID);
		}
	}
}