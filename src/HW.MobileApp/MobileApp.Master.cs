using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class MobileApp : System.Web.UI.MasterPage
    {
        protected int language;
        protected void Page_Load(object sender, EventArgs e)
        {
            language = int.Parse(Session["languageId"].ToString());
        }
    }
}