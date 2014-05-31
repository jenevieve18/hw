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

        protected void Page_Load(object sender, EventArgs e)
        {
            categories = service.NewsCategories(new HWService.NewsCategoriesRequest(1, 1, true)).NewsCategoriesResult;
        }
    }
}