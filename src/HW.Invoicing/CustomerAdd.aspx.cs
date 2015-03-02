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
    public partial class CustomerAdd : System.Web.UI.Page
    {
    	SqlCustomerRepository r = new SqlCustomerRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var c = new Customer {
        		Name = textBoxName.Text
        	};
        	r.Save(c);
        	Response.Redirect("customers.aspx");
        }
    }
}