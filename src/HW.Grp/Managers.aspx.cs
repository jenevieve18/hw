using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Managers : System.Web.UI.Page
	{
		protected IList<SponsorAdmin> sponsorAdmins;
		protected SqlManagerFunctionRepository managerRepository = new SqlManagerFunctionRepository();
		protected int sort;
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
        protected int lid;
        
        bool ForDelete {
        	get { return SponsorAdminIDToBeDeleted != -1; }
        }
        
        int SponsorAdminIDToBeDeleted {
        	get { return ConvertHelper.ToInt32(Request.QueryString["Delete"], -1); }
        }
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			sponsorRepository.SaveSponsorAdminSessionFunction(Convert.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Managers, DateTime.Now);
//			int sponsorID = Convert.ToInt32(Session["SponsorID"]);
			int sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			int sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			
			HtmlHelper.RedirectIf(!new SqlSponsorAdminRepository().SponsorAdminHasAccess(sponsorAdminID, ManagerFunction.Managers), "default.aspx", true);
			
            lid = ConvertHelper.ToInt32(Session["lid"], 1);
			sort = ConvertHelper.ToInt32(Request.QueryString["sort"]);
//			bool delete = Request.QueryString["Delete"] != null;
//			if (delete) {
			if (ForDelete) {
//				int sponsorAdminIDToBeDeleted = Convert.ToInt32(Request.QueryString["Delete"]);
//				sponsorRepository.UpdateDeletedAdmin(sponsorID, sponsorAdminIDToBeDeleted);
				sponsorRepository.UpdateDeletedAdmin(sponsorID, SponsorAdminIDToBeDeleted);
			}
//			if (sponsorID != 0) {
//				int sponsorAdminID = Session["SponsorAdminID"] != null ? Convert.ToInt32(Session["SponsorAdminID"]) : -1;
			
			sponsorAdmins = sponsorRepository.FindAdminBySponsor(sponsorID, sponsorAdminID, sort == 0 ? "ASC" : "DESC");
//			} else {
//				Response.Redirect("default.aspx", true);
//			}
		}
	}
}