using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Invoicing.Core.Repositories.Sql;
using HW.Core.Helpers;

namespace HW.Invoicing
{
    public partial class Login : System.Web.UI.Page
    {
    	SqlUserRepository r = new SqlUserRepository();
    	protected string errorMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null && Request.Cookies["RememberMe"] != null)
                {
                    textBoxName.Text = Request.Cookies["UserName"].Value;
                    textBoxPassword.Text = Request.Cookies["Password"].Value;
                    CheckBoxRememberMe.Checked = ConvertHelper.ToBoolean(Request.Cookies["RememberMe"].Value);

                    textBoxName.Attributes.Add("value", Request.Cookies["UserName"].Value);
                    textBoxPassword.Attributes.Add("value", Request.Cookies["Password"].Value);
                }
            }
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            if (CheckBoxRememberMe.Checked)
            {
                Response.Cookies["UserName"].Expires = Response.Cookies["Password"].Expires = Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(30);
            }
            else
            {
                Response.Cookies["UserName"].Expires = Response.Cookies["Password"].Expires = Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);
            }
            Response.Cookies["UserName"].Value = textBoxName.Text.Trim();
            Response.Cookies["Password"].Value = textBoxPassword.Text.Trim();
            Response.Cookies["RememberMe"].Value = CheckBoxRememberMe.Checked.ToString();

            var u = r.ReadByNameAndPassword(textBoxName.Text, textBoxPassword.Text);
            if (u != null)
            {
                Session["UserID"] = u.Id;
                Session["UserName"] = u.Name;
                Response.Redirect("dashboard.aspx");
            }
            else
            {
                errorMessage = "Invalid user name or password, please try again!";
            }
        }
    }
}