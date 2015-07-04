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
using HW.Invoicing.Core.Services;

namespace HW.Invoicing
{
    public partial class Customers : System.Web.UI.Page
    {
    	protected IList<Customer> subscribers;
        protected IList<Customer> nonSubscribers;
        CustomerService s = new CustomerService(new SqlCustomerRepository());
    	
    	public Customers()
    	{
    	}
    	
    	public void Index()
    	{
    		Page_Load(this, null);
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx?r=customers.aspx");

            subscribers = s.FindSubscribers();
            nonSubscribers = s.FindNonSubscribers();
        }
    }
}