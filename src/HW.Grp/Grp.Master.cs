using System;
using System.Collections.Generic;
using System.Linq;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Grp : System.Web.UI.MasterPage
	{
		protected Sponsor sponsor;
		protected IList<ManagerFunctionLang> functions;
		protected int lid;
//		protected bool swedish;
		protected int sponsorAdminID;
		protected string sponsorName;
		protected bool super;
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlManagerFunctionRepository managerFunctionRepository = new SqlManagerFunctionRepository();

		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorID = Convert.ToInt32(Session["SponsorID"]);
			sponsor = sponsorRepository.ReadSponsor2(sponsorID);

			sponsorName = Session["Name"] != null ? Session["Name"].ToString() : "";
			sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;

			super = Request.Url.AbsolutePath.Contains("super");

            lid = ConvertHelper.ToInt32(Session["lid"], 1);
			functions = managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID, lid);

//			lid = Request["LID"] != null ? Convert.ToInt32(Request["LID"]) : 0;

//			swedish = Session["LID"] != null ? Convert.ToInt32(Session["LID"]) != 0 : false;
//			LanguageFactory.SetCurrentCulture2(lid);
		}
	}
}