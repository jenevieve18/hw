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
    public partial class CustomerContactDelete : System.Web.UI.Page
    {
    	SqlCustomerRepository r = new SqlCustomerRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	int id = ConvertHelper.ToInt32(Request.QueryString["Id"]);
        	int customerId = ConvertHelper.ToInt32(Request.QueryString["CustomerId"]);
        	r.DeleteContact(id);
            Response.Redirect("customeredit.aspx?Id=" + customerId);
        }
    }
}