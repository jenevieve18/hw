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
        protected int lang;
        protected void Page_Load(object sender, EventArgs e)
        {
            lang=2;
            if (Session["token"] != null)
            {
                lang = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
            }
            else if (Session["newslanguageid"] != null)
            {
                lang = int.Parse(Session["newslanguageid"].ToString());
            }
            
            news = service.NewsEnum(new HWService.NewsEnumRequest(0, 0, 10, lang, true, 0)).NewsEnumResult;
        }
    }
}