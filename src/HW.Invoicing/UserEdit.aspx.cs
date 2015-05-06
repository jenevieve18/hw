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
    public partial class UserEdit : System.Web.UI.Page
    {
    	SqlUserRepository r = new SqlUserRepository();
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        	int id = ConvertHelper.ToInt32(Request.QueryString["UserID"]);
        	var u = r.Read(id);
        	if (!IsPostBack && u != null) {
        		textBoxName.Text = u.Name;
        		textBoxPassword.Text = u.Password;
        	}
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	var u = new User {
        		Name = textBoxName.Text,
        		Password = textBoxPassword.Text
        	};
        	r.Update(u, ConvertHelper.ToInt32(Request.QueryString["UserID"]));
        	Response.Redirect("users.aspx");
        }
    }
}