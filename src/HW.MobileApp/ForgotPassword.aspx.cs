using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "Forgot your password?";
            labelSub.Text = "Enter your email address below, and we will send you a link that can be used to create a new password.";
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (service.UserResetPassword(textBoxEmailAddress.Text, 2))
            {
                Response.Redirect("LinkSent.aspx");
            }
            else
            {
                labelMessage.Text = "Something is wrong";
                labelMessage.ForeColor = System.Drawing.Color.Red;
                labelSub.Text = "There is no such user with that email address. Please enter the email address that is connected to your account";
            }
        }
    }
}