using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class NewsCategoriesList : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.News[] news;
        protected int categ;
        protected string head;
        protected void Page_Load(object sender, EventArgs e)
        {
            int lang = 2;
            if (Session["newslanguageid"] != null)
            {
                lang = int.Parse(Session["newslanguageid"].ToString());
            }
            categ = 0;
            if (Request.QueryString["ncid"] != null)
            {
                categ = int.Parse(Request.QueryString["ncid"]);
            }
            news = service.NewsEnum(new HWService.NewsEnumRequest(0, 0, 10, lang, true, categ)).NewsEnumResult;

            if (news.Length > 1) head = news[0].newsCategory;
        }
    }
}