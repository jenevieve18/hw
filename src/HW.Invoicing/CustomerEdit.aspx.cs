using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Models;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class CustomerEdit : System.Web.UI.Page
    {
    	SqlCustomerRepository r = new SqlCustomerRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	var id = ConvertHelper.ToInt32(Request.QueryString["CustomerID"]);
        	var c = r.Read(id);
        	if (!IsPostBack && c != null) {
        		textBoxName.Text = c.Name;
        	}
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var c = new Customer {
        		Name = textBoxName.Text
        	};
        	r.Update(c, ConvertHelper.ToInt32(Request.QueryString["CustomerID"]));
        	Response.Redirect("customers.aspx");
        }
    }
}