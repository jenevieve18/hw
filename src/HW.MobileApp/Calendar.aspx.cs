using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HW.MobileApp
{
    public partial class Calendar : System.Web.UI.Page
    {

        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected HWService.Calendar[] calendar;
        
        protected string token = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["token"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else token = Session["token"].ToString();
            int lang = int.Parse(Session["languageId"].ToString());

            int month = DateTime.Now.Month-1;
            int year = DateTime.Now.Year;
            if (month == 1){
                month = 12;
                year -= 1;
            }
            calendar = service.CalendarEnum(new HWService.CalendarEnumRequest(token, new DateTime(year, month, 1), DateTime.Now, lang, 10)).CalendarEnumResult;
        }
    }
}