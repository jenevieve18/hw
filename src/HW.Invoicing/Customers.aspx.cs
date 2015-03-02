using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Customers : System.Web.UI.Page
    {
    	SqlCustomerRepository r = new SqlCustomerRepository();
    	protected IList<Customer> customers;
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	customers = r.FindAll();
        }
    }
}