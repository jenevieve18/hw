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
	public partial class UserEdit : System.Web.UI.Page
	{
		SqlUserRepository r = new SqlUserRepository();
		
		public UserEdit()
        {
		}
		
		public void Edit(int id)
		{
			if (IsPostBack) {
				var d = new User {
					Username = textBoxUsername.Text,
					Password = textBoxPassword.Text,
                    Color = textBoxColor.Text
				};
				r.Update(d, id);
				Response.Redirect("users.aspx");
			}
			var u = r.Read(id);
			if (u != null) {
				textBoxUsername.Text = u.Username;
				//textBoxPassword.Text = u.Password;
                textBoxPassword.Attributes["value"] = u.Password;
                textBoxColor.Text = u.Color;
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["UserId"] == null, string.Format("login.aspx?r={0}", HttpUtility.UrlEncode(Request.Url.PathAndQuery)));
            
            //textBoxPassword.Attributes["type"] = "password";
			Edit(ConvertHelper.ToInt32(Request.QueryString["UserID"]));
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			Edit(ConvertHelper.ToInt32(Request.QueryString["UserID"]));
		}
	}
}