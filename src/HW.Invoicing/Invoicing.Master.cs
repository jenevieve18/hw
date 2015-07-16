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
        SqlCompanyRepository r = new SqlCompanyRepository();
        protected Company company;

        protected void Page_Load(object sender, EventArgs e)
        {
            int companyId = ConvertHelper.ToInt32(Session["CompanyId"]);
            //company = r.Read(companyId);
        }
    }
}