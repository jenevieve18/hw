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
            labelMessage.Text = R.Str("user.forgot.password");
            labelSub.Text = R.Str("user.forgot.message");
        }

        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (service.UserResetPassword(textBoxEmailAddress.Text, 2))
            {
                Response.Redirect("LinkSent.aspx");
            }
            else
            {
                labelMessage.Text = R.Str("user.forgot.wrong");
                labelMessage.ForeColor = System.Drawing.Color.Red;
                labelSub.Text = R.Str("user.forgot.wrongMessage");
            }
        }
    }
}