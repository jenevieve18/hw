using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class CustomerAgreementClosed : System.Web.UI.Page
    {
        SqlCompanyRepository cr = new SqlCompanyRepository();
        SqlCustomerRepository r = new SqlCustomerRepository();
        protected Company company;

        protected void Page_Load(object sender, EventArgs e)
        {
            int companyId = ConvertHelper.ToInt32(Request.QueryString["CompanyId"]);
            company = cr.Read(companyId);
        }
    }
}