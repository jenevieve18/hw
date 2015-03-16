using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class PasswordActivation : System.Web.UI.Page
	{
		SqlSponsorAdminRepository r = new SqlSponsorAdminRepository();
		string uid;
		
		protected void Page_Load(object sender, EventArgs e)
		{
			uid = Request.QueryString["KEY"];
		}

		protected void buttonActivate_Click(object sender, EventArgs e)
		{
			if (textBoxPassword.Text != textBoxConfirmPassword.Text) {
				labelErrorMessage.Text = "Password does not match";
			} else if (textBoxPassword.Text.Length < 8) {
				labelErrorMessage.Text = "Password must be at least 8 characters";
			} else {
				r.SavePassword(Db.HashMD5(textBoxPassword.Text), uid);
			}
		}
	}
}