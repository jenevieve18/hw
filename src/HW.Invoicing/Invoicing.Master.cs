using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
	public partial class Invoicing : System.Web.UI.MasterPage
	{
		UserService service = new UserService();
		protected Company company;
		protected User user;

		protected void Page_Load(object sender, EventArgs e)
		{
			int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
//			company = cr.Read(companyId);
			company = service.ReadCompany(companyId);
			
			int userId = ConvertHelper.ToInt32(Session["UserId"]);
			
//			user = ur.Read(userId);
//			user.Companies = ur.FindCompanies(userId);
			user = service.ReadUser(userId);
		}
	}
}