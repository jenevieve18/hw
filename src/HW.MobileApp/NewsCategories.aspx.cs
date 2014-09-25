using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class NewsCategories : System.Web.UI.Page
    {
        HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.NewsCategory[] categories;
        protected int lang;

        protected void Page_Load(object sender, EventArgs e)
        {
            lang = 2;
            if (Session["token"] != null)
            {
                lang = service.UserGetInfo(Session["token"].ToString(), 20).languageID;
            }
            else if (Session["newslanguageid"] != null)
            {
                lang = int.Parse(Session["newslanguageid"].ToString());
            }
            categories = service.NewsCategories(new HWService.NewsCategoriesRequest(1, lang, true)).NewsCategoriesResult;
            
            if (lang == 1)
                btnSwe.CssClass = "ui-btn-active";
            else if (lang == 2)
                btnInt.CssClass = "ui-btn-active";
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            btnInt.Text = R.Str(lang, "news.categories.international");
            btnSwe.Text = R.Str(lang, "news.categories.swedish");
        }

        protected void toEnglish(object sender, EventArgs e)
        {
            if (Session["newslanguageid"] != null)
            {
                if (Session["newslanguageid"].ToString() != "2") {
                    Session["newslanguageid"] = 2;
                    Response.Redirect("NewsCategories.aspx");
                }
            }
        }

        protected void toSwedish(object sender, EventArgs e)
        {
            if (Session["newslanguageid"] == null) Session.Add("newslanguageid", 1);
            else Session["newslanguageid"] = 1;
            Response.Redirect("NewsCategories.aspx");
        }
    }
}