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
    public partial class ItemDeactivate : System.Web.UI.Page
    {
    	SqlItemRepository r = new SqlItemRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
        	r.Deactivate(id);
        	Response.Redirect("items.aspx");
        }
    }
}