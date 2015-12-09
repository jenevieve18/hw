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
		SqlUserRepository ur = new SqlUserRepository();
		SqlCompanyRepository cr = new SqlCompanyRepository();
		protected string errorMessage;

		protected void Page_Load(object sender, EventArgs e)
		{
			var url = Request.QueryString["r"] != null ? HttpUtility.UrlDecode(Request.QueryString["r"]) : "dashboard.aspx";
			HtmlHelper.RedirectIf(Session["UserId"] != null, url);

			if (!IsPostBack)
			{
				if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null && Request.Cookies["RememberMe"] != null)
				{
					textBoxLoginName.Text = Request.Cookies["UserName"].Value;
					textBoxLoginPassword.Text = Request.Cookies["Password"].Value;
					checkBoxRememberMe.Checked = ConvertHelper.ToBoolean(Request.Cookies["RememberMe"].Value);

					textBoxLoginName.Attributes.Add("value", Request.Cookies["UserName"].Value);
					textBoxLoginPassword.Attributes.Add("value", Request.Cookies["Password"].Value);
				}
			}
		}

		protected void buttonLogin_Click(object sender, EventArgs e)
		{
			if (checkBoxRememberMe.Checked)
			{
				Response.Cookies["UserName"].Expires = Response.Cookies["Password"].Expires = Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(30);
			}
			else
			{
				Response.Cookies["UserName"].Expires = Response.Cookies["Password"].Expires = Response.Cookies["RememberMe"].Expires = DateTime.Now.AddDays(-1);
			}
			Response.Cookies["UserName"].Value = textBoxLoginName.Text.Trim();
			Response.Cookies["Password"].Value = textBoxLoginPassword.Text.Trim();
			Response.Cookies["RememberMe"].Value = checkBoxRememberMe.Checked.ToString();

			var u = ur.ReadByNameAndPassword(textBoxLoginName.Text, textBoxLoginPassword.Text);
			if (u != null)
			{
//				var c = cr.ReadSelectedCompanyByUser(u.Id);
//				var c = cr.ReadSelectedCompanyByUser2(u);
				var c = ur.ReadSelectedCompany(u);
				if (c != null)
				{
					Session["CompanyId"] = c.Id;
					Session["CompanyName"] = c.Name;
				}
				Session["UserID"] = u.Id;
				Session["UserName"] = u.Username;
				Session["UserRealName"] = u.Name;
				if (Request.QueryString["r"] != null)
				{
					Response.Redirect(HttpUtility.UrlDecode(Request.QueryString["r"]));
				}
				else
				{
					Response.Redirect("dashboard.aspx");
				}
			}
			else
			{
				errorMessage = "Invalid user name or password, please try again!";
			}
		}
	}
}