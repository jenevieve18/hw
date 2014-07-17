﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Login : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["token"] != null)
            {
                if (Request.Cookies["token"].Value != null)
                {
                    Session.Add("token", Request.Cookies["token"].Value);
                    service.UserExtendToken(Request.Cookies["token"].Value, 10);
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }

        protected void LoginButtonClick(object sender, EventArgs e)
        {
            var u = service.UserLogin(textBoxUsername.Text, textBoxPassword.Text, 10);
            if (u.token != null && u.token != "")
            {
                Session.Add("token", u.token);
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                labelMessage.Text = "Sorry, incorrect login details.";
            }
        }
    }
}