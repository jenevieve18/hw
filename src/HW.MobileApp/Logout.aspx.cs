using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            if (Request.Cookies["token"] != null)
            {
                Response.Cookies["token"].Value = null;
                Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1);

            }*/
            
            Session.RemoveAll();
            Response.Redirect("Default.aspx");

        }
    }
}