using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Login : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "Login to a better life.";
            
            if (Request.Cookies["token"] != null)
            {
                if (Request.Cookies["token"].Value != null)
                {
                    Session.Add("token", Request.Cookies["token"].Value);
                    if(service.UserExtendToken(Request.Cookies["token"].Value,120))
                        Response.Redirect("Dashboard.aspx");
                    
                }
            }
            HtmlHelper.RedirectIf(Session["token"] != null, "Dashboard.aspx");
            
        }

        protected void LoginButtonClick(object sender, EventArgs e)
        {
            var u = service.UserLogin(textBoxUsername.Text, textBoxPassword.Text, 120);
            if (u.token != null && u.token != "")
            {
                Session.Add("token", u.token);
                Response.Redirect("Dashboard.aspx");
            }
            else
            {
                labelMessage.Text = "Sorry, incorrect login details.";
                labelMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}