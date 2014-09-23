using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class More : System.Web.UI.Page
    {
        protected int language;
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            language = 2;
            if (Session["token"] != null)
            {
                language = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
            }
        }
    }
}