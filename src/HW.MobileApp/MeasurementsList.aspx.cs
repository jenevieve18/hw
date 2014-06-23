using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class MeasurementsList : System.Web.UI.Page
    {

        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.MeasureCategory[] category;
        protected string datetime;
        protected void Page_Load(object sender, EventArgs e)
        {
            string token="";
            if (Session["token"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else token = Session["token"].ToString();

            int lang = int.Parse(Session["languageId"].ToString());

            category = service.MeasureCategoryEnum(new HWService.MeasureCategoryEnumRequest(token, 0, lang, 10)).MeasureCategoryEnumResult;
            datetime = Request.QueryString["datetime"];

        }
    }
}