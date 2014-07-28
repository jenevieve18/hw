using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW
{
    public partial class myhealth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Db.checkAndLogin();

            if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
            {
                HttpContext.Current.Response.Redirect("calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
        }
    }
}