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
		IUserRepository r;
		
		public UserEdit() : this(new SqlUserRepository())
		{
		}
		
		public UserEdit(IUserRepository r)
		{
			this.r = r;
		}
		
		public void Edit(int id)
		{
			if (IsPostBack) {
				var d = new User {
					Name = textBoxName.Text,
					Password = textBoxPassword.Text
				};
				r.Update(d, id);
				Response.Redirect("users.aspx");
			}
			var u = r.Read(id);
			if (u != null) {
				textBoxName.Text = u.Name;
				textBoxPassword.Text = u.Password;
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
			Edit(ConvertHelper.ToInt32(Request.QueryString["UserID"]));
		}

		protected void buttonSave_Click(object sender, EventArgs e)
		{
			Edit(ConvertHelper.ToInt32(Request.QueryString["UserID"]));
		}
	}
}