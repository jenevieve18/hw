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
using HW.Core.Services;

namespace HW.Grp
{
	public partial class Managers : System.Web.UI.Page
	{
		protected IList<SponsorAdmin> sponsorAdmins;
		protected int lid;
		protected int sort;
		ManagerService service = new ManagerService(
			AppContext.GetRepositoryFactory().CreateManagerFunctionRepository(),
			AppContext.GetRepositoryFactory().CreateSponsorRepository(),
			AppContext.GetRepositoryFactory().CreateSponsorAdminRepository()
		);
		
		public IList<SponsorAdmin> SponsorAdmins {
			get { return sponsorAdmins; }
			set { sponsorAdmins = value; }
		}
		
		public void TryDelete(int sponsorID, int sponsorAdminIDToBeDeleted)
		{
			if (sponsorAdminIDToBeDeleted != -1) {
				service.UpdateDeletedAdmin(sponsorID, sponsorAdminIDToBeDeleted);
			}
		}
		
		public void SaveAdminSession(int sponsorAdminSessionID, int functionID)
		{
			service.SaveSponsorAdminSessionFunction(sponsorAdminSessionID, functionID, DateTime.Now);
		}
		
		public bool HasAccess(int sponsorAdminID, int functionID)
		{
			return service.SponsorAdminHasAccess(sponsorAdminID, functionID);
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(Session["SponsorID"] == null, "default.aspx", true);
			
			SaveAdminSession(ConvertHelper.ToInt32(Session["SponsorAdminSessionID"]), ManagerFunction.Managers);
			
			int sponsorID = ConvertHelper.ToInt32(Session["SponsorID"]);
			int sponsorAdminID = ConvertHelper.ToInt32(Session["SponsorAdminID"], -1);
			
			HtmlHelper.RedirectIf(!HasAccess(sponsorAdminID, ManagerFunction.Managers), "default.aspx", true);
			
			lid = ConvertHelper.ToInt32(Session["lid"], 1);
			sort = ConvertHelper.ToInt32(Request.QueryString["sort"]);
			
			TryDelete(sponsorID, ConvertHelper.ToInt32(Request.QueryString["Delete"], -1));
			
			SponsorAdmins = service.FindAdminBySponsor(sponsorID, sponsorAdminID, sort, lid);
		}
	}
}