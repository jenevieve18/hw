using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class Invoicing : System.Web.UI.MasterPage
    {
        SqlCompanyRepository cr = new SqlCompanyRepository();
        SqlUserRepository ur = new SqlUserRepository();
        protected Company company;
        protected IList<Company> companies;
        protected User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            company = cr.Read(companyId);
            companies = cr.FindAll();
            
            int userId = ConvertHelper.ToInt32(Session["UserId"]);
            user = ur.Read(userId);
        }
    }
}