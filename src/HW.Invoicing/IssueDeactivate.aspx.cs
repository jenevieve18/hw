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
    public partial class IssueDeactivate : System.Web.UI.Page
    {
    	SqlIssueRepository r = new SqlIssueRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

        	int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
        	r.Deactivate(id);
            Response.Redirect("issues.aspx");
        }
    }
}