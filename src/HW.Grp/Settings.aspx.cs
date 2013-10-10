using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Settings : System.Web.UI.Page
	{
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			Save.Click += new EventHandler(Save_Click);
			if (Convert.ToInt32(Session["SponsorAdminID"]) <= 0) {
				Message.Text = "Super administrators cannot change password. Please contact support@healthwatch.se!";
				Save.Visible = false;
				Password.Visible = false;
				Txt.Visible = false;
			}
		}

		void Save_Click(object sender, EventArgs e)
		{
			if (Password.Text.Length > 1) {
				sponsorRepository.UpdateSponsorAdminPassword(Password.Text.Replace("'", "''"), Convert.ToInt32(Session["SponsorAdminID"]));
				Message.Text = "New password saved!";
			} else {
				Message.Text = "Password too short!";
			}
		}
	}
}