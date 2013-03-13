using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HWgrp.Web
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            buttonSave.Click += new EventHandler(Save_Click);
            if (Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) <= 0) {
                labelMessage.Text = "Super administrators cannot change password. Please contact support@healthwatch.se!";
                buttonSave.Visible = false;
                textBoxPassword.Visible = false;
                Txt.Visible = false;
            }
        }

        void Save_Click(object sender, EventArgs e)
        {
			if (textBoxPassword.Text.Length > 1) {
				Db2.exec("UPDATE SponsorAdmin SET Pas = '" + textBoxPassword.Text.Replace("'", "''") + "' WHERE SponsorAdminID = " + Convert.ToInt32(Session["SponsorAdminID"]));
				labelMessage.Text = "New password saved!";
            } else {
				labelMessage.Text = "Password too short!";
            }
        }
    }
}