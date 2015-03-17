﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
    public partial class PasswordActivation : System.Web.UI.Page
    {
        SqlSponsorAdminRepository r = new SqlSponsorAdminRepository();
        protected int lid;
        
        bool HasUniqueKey {
        	get { return UniqueKey != null && UniqueKey != ""; }
        }
        
        string UniqueKey {
        	get { return Request.QueryString["KEY"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        	HtmlHelper.RedirectIf(!HasUniqueKey, "default.aspx", true);
        	
            lid = ConvertHelper.ToInt32(Session["lid"], 1);
        }

        protected void buttonActivate_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.Text != textBoxConfirmPassword.Text) {
                labelErrorMessage.Text = "Password does not match";
            } else if (textBoxPassword.Text.Length < 8) {
                labelErrorMessage.Text = "Password must be at least 8 characters";
            } else {
                r.SavePassword(Db.HashMd5(textBoxPassword.Text), UniqueKey);
                Response.Redirect("default.aspx", true);
            }
        }
    }
}