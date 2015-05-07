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
    public partial class CustomerEdit : System.Web.UI.Page
    {
    	ICustomerRepository r;
    	
    	public CustomerEdit() : this(new SqlCustomerRepository())
    	{
    	}
    	
    	public CustomerEdit(ICustomerRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Edit(int id)
    	{
    		Page_Load(this, null);
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	var id = ConvertHelper.ToInt32(Request.QueryString["CustomerID"]);
        	if (!IsPostBack) {
        		var c = r.Read(id);
        		if (c != null) {
        			textBoxName.Text = c.Name;
        		}
        	}
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var id = ConvertHelper.ToInt32(Request.QueryString["CustomerID"]);
        	var c = new Customer {
        		Name = textBoxName.Text
        	};
        	r.Update(c, id);
        	Response.Redirect("customers.aspx");
        }
    }
}