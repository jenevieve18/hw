using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class UserAdd : System.Web.UI.Page
    {
    	SqlUserRepository r = new SqlUserRepository();
    	
    	public UserAdd()
    	{
    	}
    	
    	public void Add()
    	{
    		var u = new User {
                Username = textBoxUsername.Text,
                Name = textBoxName.Text,
                Password = textBoxPassword.Text,
                Color = textBoxColor.Text
        	};
            r.Save(u);
            Response.Redirect("users.aspx");
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	Add();
        }
    }
}