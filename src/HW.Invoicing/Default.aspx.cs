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
            if (CheckBoxRememberMe.Checked)
            {
                Response.Cookies["UserName"].Expires = Response.Cookies["Password"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["UserName"].Expires = Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Cookies["UserName"].Value = textBoxName.Text.Trim();
            Response.Cookies["Password"].Value = textBoxPassword.Text.Trim();

        	var u = r.ReadByNameAndPassword(textBoxName.Text, textBoxPassword.Text);
            if (u != null) {
        		Session["UserID"] = u.Id;
                Session["UserName"] = u.Name;
                Response.Redirect("dashboard.aspx");
            } else {
                errorMessage = "Invalid user name or password, please try again!";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null) {
                    textBoxName.Text = Request.Cookies["UserName"].Value;
                    textBoxPassword.Text = Request.Cookies["Password"].Value;
                    textBoxName.Attributes.Add("value", Request.Cookies["UserName"].Value);
                    textBoxPassword.Attributes.Add("value", Request.Cookies["Password"].Value);
                }
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
        	Login();
        }
    }
}