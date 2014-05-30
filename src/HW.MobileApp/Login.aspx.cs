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
            if (IsPostBack)
            {
                var u = service.UserLogin(textBoxUsername.Text, textBoxPassword.Text, 10);
                if (u.token != null && u.token != "")
                {
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    labelMessage.Text = "Sorry, incorrect login details.";
                }
            }
        }
    }
}