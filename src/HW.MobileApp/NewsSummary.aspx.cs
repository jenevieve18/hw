using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class NewsSummary : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.News news;
        protected string back;
        protected int lang;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["ncid"] != null)
            {
                back = "href='NewsCategoriesList.aspx?ncid="+Request.QueryString["ncid"]+"'";
            }
            
            if (Request.QueryString["nid"] != null)
            {
                if (Session["token"] != null)
                {
                    lang = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
                }
                if (Session["newslanguageid"] != null) lang = int.Parse(Session["newslanguageid"].ToString());
                news = service.NewsDetail(int.Parse(Request.QueryString["nid"]), lang );
            }
            else Response.Redirect("News.aspx");

            
            
        }
    }
}