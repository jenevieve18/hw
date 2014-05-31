using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class News : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.News[] news;

        protected void Page_Load(object sender, EventArgs e)
        {
            news = service.NewsEnum(new HWService.NewsEnumRequest(0, 0, 10, 1, true, 0)).NewsEnumResult;
        }
    }
}