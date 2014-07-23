using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class ActivityMeasurement : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected DateTime date;
        protected HWService.Event[] activities = null;
        HWService.Calendar calendar;
        protected string token;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            
            token = Session["token"].ToString();

            if (Request.QueryString["datetime"] != null)
            {
                var temp = Request.QueryString["datetime"];
                int year = int.Parse(temp.Substring(0, 4));
                int month = int.Parse(temp.Substring(5, 2));
                int day = int.Parse(temp.Substring(8, 2));
                int hrs = int.Parse(temp.Substring(11, 2));
                int min = int.Parse(temp.Substring(14, 2));
                int sec = int.Parse(temp.Substring(17, 2));
                
                date = new DateTime(year, month, day, hrs, min, sec);
                
            }
            int lang = int.Parse(Session["languageId"].ToString());

            foreach (var c in service.CalendarEnum(new HWService.CalendarEnumRequest(token, date, date, lang, 10)).CalendarEnumResult)
            {
                calendar = c;
            }
            if(calendar!=null)
            activities = calendar.events;            
        }

        protected void deleteActivity(object sender, EventArgs e){
            string[] delimiter = {"$*#"};
            string[] value = hdEventId.Value.Split(delimiter, StringSplitOptions.None);
            if (value.Length > 1 )
            {
                if (value[1] != "")
                {
                    if (service.UserDeleteFormInstance(token, value[1], int.Parse(value[0]), 10))
                        Response.Redirect("ActivityMeasurement.aspx?datetime=" + date.ToString("yyyy-MM-ddTHH:mm:ss"));
                }
                else
                {
                    if (service.UserDeleteMeasure(token, int.Parse(value[0]), 10))
                        Response.Redirect("ActivityMeasurement.aspx?datetime=" + date.ToString("yyyy-MM-ddTHH:mm:ss"));
                }

            }
            
                
            
        }
    }
}