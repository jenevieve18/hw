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
        	int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
        	r.Delete(id);
        	Response.Redirect("issues.aspx");
        }
    }
}