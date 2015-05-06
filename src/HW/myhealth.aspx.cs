using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace healthWatch
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