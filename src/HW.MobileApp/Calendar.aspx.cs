using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Calendar : System.Web.UI.Page
    {

        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.Calendar[] calendar;
        protected int lang;
        protected string token = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();
            lang = int.Parse(Session["languageId"].ToString());

            DateTime fromDT = new DateTime(2000, 1, 1);
            
            calendar = service.CalendarEnum(new HWService.CalendarEnumRequest(token,fromDT, DateTime.Now.AddDays(1), lang, 10)).CalendarEnumResult;
        }
    }
}