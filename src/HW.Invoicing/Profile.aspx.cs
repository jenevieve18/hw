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
    public partial class Profile : System.Web.UI.Page
    {
    	SqlUserRepository r = new SqlUserRepository();
    	
    	public Profile()
    	{
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, "login.aspx");

            Update(ConvertHelper.ToInt32(Session["UserID"]));
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	Update(ConvertHelper.ToInt32(Session["UserID"]));
        }

        public void Update(int id)
        {
        	if (IsPostBack) {
        		var d = new User {
        			Name = textBoxName.Text,
        			Password = textBoxPassword.Text
        		};
        		r.Update(d, id);
                Session["UserName"] = d.Name;
                Response.Redirect("dashboard.aspx");
        	}
        	var u = r.Read(id);
        	if (u != null) {
        		textBoxName.Text = u.Name;
                textBoxPassword.Text = u.Password;
        	}
        }
    }
}