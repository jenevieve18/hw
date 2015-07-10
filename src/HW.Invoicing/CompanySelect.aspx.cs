using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class CompanySelect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            Session["CompanyId"] = id;
            Response.Redirect("dashboard.aspx");
        }
    }
}