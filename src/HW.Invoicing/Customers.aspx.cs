using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Customers : System.Web.UI.Page
    {
    	ICustomerRepository r;
    	protected IList<Customer> customers;
    	
    	public Customers() : this(new SqlCustomerRepository())
    	{
    	}
    	
    	public Customers(ICustomerRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Index()
    	{
    		Page_Load(this, null);
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	HtmlHelper.RedirectIf(Session["UserId"] == null, "default.aspx");
        	
        	customers = r.FindAll();
        }
    }
}