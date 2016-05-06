using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
//		protected int lid;
		protected int sponsorAdminID;
		protected string sponsorName;
		protected bool super;
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		SqlManagerFunctionRepository managerFunctionRepository = new SqlManagerFunctionRepository();
		SqlUserRepository userRepository = new SqlUserRepository();
//		protected int lid = Language.ENGLISH;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

		protected void Page_Load(object sender, EventArgs e)
		{
			int sponsorID = Convert.ToInt32(Session["SponsorID"]);
			sponsor = sponsorRepository.ReadSponsor2(sponsorID);

			sponsorName = Session["Name"] != null ? Session["Name"].ToString() : "";
			sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;

			super = Request.Url.AbsolutePath.Contains("super");

//			lid = ConvertHelper.ToInt32(Session["lid"], 2);
			var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			functions = managerFunctionRepository.FindBySponsorAdmin(sponsorAdminID, lid);
		}
	}
}