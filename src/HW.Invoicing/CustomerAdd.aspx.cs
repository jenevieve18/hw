using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CustomerAdd : System.Web.UI.Page
    {
    	ICustomerRepository r;
    	
    	public CustomerAdd() : this(new SqlCustomerRepository())
    	{
    	}
    	
    	public CustomerAdd(ICustomerRepository r)
    	{
    		this.r = r;
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        
        public void Add()
        {
        	buttonSave_Click(this, null);
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