using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected int lang;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");

            lang = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
            if (Session["languageId"] == null)
                Session.Add("languageId", lang);
            else Session["languageId"] = lang;

            
        } 
    }
}