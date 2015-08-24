using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class CustomerAgreementThanks : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();
        protected Company company;

        protected void Page_Load(object sender, EventArgs e)
        {
            company = r.Read(ConvertHelper.ToInt32(Request["CompanyId"]));
            if (company == null)
            {
                Response.Redirect("companies.aspx");
            }
        }
    }
}