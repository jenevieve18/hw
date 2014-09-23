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
            language = 2;
            if (Session["languageId"]!=null)
                language = int.Parse(Session["languageId"].ToString());
            else if (Session["newslanguageid"] != null) language = int.Parse(Session["newslanguageid"].ToString());
        }
    }
}