﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class Settings : System.Web.UI.Page
	{
		SqlSponsorRepository sponsorRepository = new SqlSponsorRepository();
//		protected int lid;
		
		SqlUserRepository userRepository = new SqlUserRepository();
//		protected int lid = Language.ENGLISH;
		protected int lid = LanguageFactory.GetLanguageID(HttpContext.Current.Request);

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Save.Text = R.Str(lid, "save", "Save");
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{
            HtmlHelper.RedirectIf(Session["IPAddress"].ToString() != "Not RealmIdentifier", "default.aspx", true);
            HtmlHelper.RedirectIf(Session["SponsorAdminID"] == null, "default.aspx", true);
			
//			lid = ConvertHelper.ToInt32(Session["lid"], 2);
			var userSession = userRepository.ReadUserSession(Request.UserHostAddress, Request.UserAgent);
			if (userSession != null) {
				lid = userSession.Lang;
			}
			Save.Click += new EventHandler(Save_Click);
			if (Convert.ToInt32(Session["SponsorAdminID"]) <= 0) {
				Message.Text = R.Str(lid, "admin.error", "Super administrators cannot change password. Please contact support@healthwatch.se!");
				Save.Visible = false;
				Password.Visible = false;
				Txt.Visible = false;
			}
		}

		void Save_Click(object sender, EventArgs e)
		{
			if (Password.Text.Length > 8) {
				sponsorRepository.UpdateSponsorAdminPassword(Db.HashMd5(Password.Text), Convert.ToInt32(Session["SponsorAdminID"]));
				Message.Text = R.Str(lid, "password.saved", "New password saved!");
			} else {
				Message.Text = R.Str(lid, "password.short", "Password too short!");
			}
		}
	}
}