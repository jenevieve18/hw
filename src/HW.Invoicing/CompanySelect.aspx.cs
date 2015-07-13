using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CompanySelect : System.Web.UI.Page
    {
        SqlCompanyRepository r = new SqlCompanyRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            var c = r.Read(id);
            Session["CompanyId"] = c.Id;
            Session["CompanyName"] = c.Name;
            Response.Redirect("dashboard.aspx");
        }
    }
}