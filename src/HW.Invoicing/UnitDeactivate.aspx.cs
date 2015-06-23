using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class UnitDeactivate : System.Web.UI.Page
    {
        SqlUnitRepository r = new SqlUnitRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
            r.Deactivate(id);
            Response.Redirect("units.aspx");
        }
    }
}