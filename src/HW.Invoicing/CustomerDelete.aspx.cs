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
    public partial class CustomerDelete : System.Web.UI.Page
    {
    	ICustomerRepository r;
    	
    	public CustomerDelete() : this(new SqlCustomerRepository())
    	{
    	}
    	
    	public CustomerDelete(ICustomerRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Delete(int id)
    	{
    		r.Delete(id);
        	Response.Redirect("customers.aspx");
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

        	Delete(ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
        }
    }
}