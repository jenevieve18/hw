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
		protected int sortFirstName;
        protected int sortLastName;
//		ManagerService service = new ManagerService(
//			AppContext.GetRepositoryFactory().CreateManagerFunctionRepository(),
//			AppContext.GetRepositoryFactory().CreateSponsorRepository(),
//			AppContext.GetRepositoryFactory().CreateSponsorAdminRepository()
//		);
		ManagerService service;
        string sort;
		
		public Managers() : this(new ManagerService(new SqlManagerFunctionRepository(), new SqlSponsorRepository(), new SqlSponsorAdminRepository()))
		{
		}
		
		public Managers(ManagerService service)
		{
			this.service = service;
		}

		public void Delete(int sponsorID, int sponsorAdminIDToBeDeleted)
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
            
            sort = Request.QueryString["Sort"];
			sortFirstName = ConvertHelper.ToInt32(Request.QueryString["SortFirstName"]);
            sortLastName = ConvertHelper.ToInt32(Request.QueryString["SortLastName"]);
			
			Delete(sponsorID, ConvertHelper.ToInt32(Request.QueryString["Delete"], -1));
			
			Index(sponsorID, sponsorAdminID, sort, sortFirstName, sortLastName, lid);
		}
		
		public void Index(int sponsorID, int sponsorAdminID, string sort, int sortFirstName, int sortLastName, int lid)
		{
			sponsorAdmins = service.FindAdminBySponsor(sponsorID, sponsorAdminID, sort, sortFirstName, sortLastName, lid);
		}
	}
}