using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories;
using HW.Invoicing.Core.Repositories.Sql;

namespace HW.Invoicing
{
    public partial class Default : System.Web.UI.Page
    {
        IUserRepository r;
        protected string errorMessage;
        
        public Default() : this(new SqlUserRepository())
        {
        }
        
        public Default(IUserRepository r)
        {
        	this.r = r;
        }
        
        public void Login()
        {
        	var u = r.ReadByNameAndPassword(textBoxName.Text, textBoxPassword.Text);
            if (u != null) {
                Response.Redirect("dashboard.aspx");
            } else {
                errorMessage = "Invalid user name or password, please try again!";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
        	Login();
        }
    }
}