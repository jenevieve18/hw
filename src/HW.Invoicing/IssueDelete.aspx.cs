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
    public partial class IssueDelete : System.Web.UI.Page
    {
    	SqlIssueRepository r = new SqlIssueRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

        	int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
        	r.Delete(id);
        	Response.Redirect("issues.aspx");
        }
    }
}