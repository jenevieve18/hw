﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Core.Models;

namespace HW.Grp
{
	public partial class PasswordActivation : System.Web.UI.Page
	{
		SqlSponsorAdminRepository r = new SqlSponsorAdminRepository();
		protected int lid;
        //protected SponsorAdmin admin;
		
		bool HasUniqueKey {
			get { return UniqueKey != null && UniqueKey != ""; }
		}
		
		string UniqueKey {
			get { return Request.QueryString["KEY"]; }
		}
		
		bool Saved {
			get { return Request.QueryString["Saved"] != null; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			HtmlHelper.RedirectIf(!HasUniqueKey, "default.aspx", true);
			HtmlHelper.RedirectIf(!r.SponsorAdminUniqueKeyExists(UniqueKey), "default.aspx", true);

            if (r.SponsorAdminUniqueKeyUsed(UniqueKey))
            {
                Response.Redirect("default.aspx");
            }

            lid = ConvertHelper.ToInt32(Session["lid"], 1);

            //admin = r.ReadSponsorByUniqueKey(UniqueKey);
            //HtmlHelper.RedirectIf(admin == null, "default.aspx", true);
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            buttonActivate.Text = R.Str(lid, "password.activate", "Activate Password");
			if (Saved) {
				string script = string.Format("<script>openWindow('{0}')</script>", R.Str(lid, "password.saved", "New password saved!"));
				ClientScript.RegisterStartupScript(this.GetType(), "SENT", script);
			}
		}

		protected void buttonActivate_Click(object sender, EventArgs e)
		{
			if (textBoxPassword.Text != textBoxConfirmPassword.Text) {
				labelErrorMessage.Text = R.Str(lid, "password.nomatch", "Passwords do not match!");
			} else if (textBoxPassword.Text.Length < 8) {
				labelErrorMessage.Text = R.Str(lid, "password.short", "Password too short! It needs to be at least 8 characters.");
			} else {
				r.SavePassword(Db.HashMd5(textBoxPassword.Text), UniqueKey);
                //r.SavePassword(Db.HashMd5(textBoxPassword.Text), Guid.NewGuid().ToString(), admin.Id);
				//Response.Redirect(string.Format("passwordactivation.aspx?KEY={0}&Saved=true", UniqueKey), true);
                Response.Redirect("default.aspx");
			}
		}
	}
}