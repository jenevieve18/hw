using System;
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
            labelMessage.Text = R.Str("login.message", "Login to a better life.");
            if (Request.Cookies["token"] != null)
            {
                if (Request.Cookies["token"].Value != null)
                {
                    Session.Add("token", Request.Cookies["token"].Value);
                    if (service.UserExtendToken(Request.Cookies["token"].Value, 120))
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            textBoxUsername.Attributes["placeholder"] = R.Str("user.name");
            textBoxPassword.Attributes["placeholder"] = R.Str("user.password");
            buttonLogin.Text = R.Str("user.login");
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
                labelMessage.Text = R.Str("user.incorrectLogin", "Sorry, incorrect login details.");
                labelMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}