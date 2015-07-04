using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Dashboard : System.Web.UI.Page
    {
    	SqlUserRepository r = new SqlUserRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");
        }
    }
}