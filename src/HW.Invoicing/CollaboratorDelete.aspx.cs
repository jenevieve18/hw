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
    public partial class CollaboratorDelete : System.Web.UI.Page
    {
    	SqlUserRepository r = new SqlUserRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));

            int id = ConvertHelper.ToInt32(Request.QueryString["UserID"]);
            r.Delete(id);
        	Response.Redirect("collaborators.aspx");
        }
    }
}