using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.Grp3
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            buttonLogin.Click += buttonLogin_Click;
        }

        void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Org.aspx");
        }
    }
}