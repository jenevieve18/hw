﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected int language;
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Default.aspx");
            string token = Session["token"].ToString();
            language = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
            HttpCookie cToken = new HttpCookie("token");
            cToken.Value = token;
            cToken.Expires = DateTime.Now.AddMonths(5);
            Response.Cookies.Add(cToken);
        }
    }
}