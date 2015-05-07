using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Invoicing.Core.Models;

namespace HW.Invoicing
{
    public partial class UserAdd : System.Web.UI.Page
    {
    	IUserRepository r;
    	
    	public UserAdd() : this(new SqlUserRepository())
    	{
    	}
    	
    	public UserAdd(IUserRepository r)
    	{
    		this.r = r;
    	}
    	
    	public void Add()
    	{
    		var u = new User {
                Name = textBoxName.Text,
                Password = textBoxPassword.Text
        	};
            r.Save(u);
            Response.Redirect("users.aspx");
    	}
    	
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
        	Add();
        }
    }
}