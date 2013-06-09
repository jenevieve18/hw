﻿//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email="jens.pettersson@healthwatch.se"/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Grp
{
	public partial class Site : System.Web.UI.MasterPage
	{
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IManagerFunctionRepository managerFunctionRepository = AppContext.GetRepositoryFactory().CreateManagerFunctionRepository();
		protected Sponsor sponsor;
		protected IList<ManagerFunction> functions;
		protected int lid;
		protected bool swedish;
		protected int sponsorAdminID;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorID = Convert.ToInt32(Session["SponsorID"]);
			sponsor = sponsorRepository.X(sponsorID);
			
			sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;
			functions = managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID);
			
			lid = Request["LID"] != null ? Convert.ToInt32(Request["LID"]) : 0;

			swedish = Session["LID"] != null ? Convert.ToInt32(Session["LID"]) != 0 : false;
			LanguageFactory.SetCurrentCulture2(lid);
		}
	}
}