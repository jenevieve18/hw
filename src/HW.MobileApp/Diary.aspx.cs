using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HW.MobileApp
{
    public partial class Diary : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected string token = "";
        protected int lang;
        protected DateTime setdate;
        protected HWService.Calendar calendar;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();

             lang = int.Parse(Session["languageId"].ToString());
             

             if (Request.QueryString["date"] != null) {
                    var temp = Request.QueryString["date"];
                    int year = int.Parse(temp.Substring(0,4));
                    int month = int.Parse(temp.Substring(5, 2));
                    int day = int.Parse(temp.Substring(8, 2));
                    int hour = int.Parse(temp.Substring(11, 2));
                    int min = int.Parse(temp.Substring(14, 2));
                    int sec = int.Parse(temp.Substring(17, 2));
                    setdate = new DateTime(year,month,day,hour,min,sec);
                    dateset.Value = setdate.ToString("yyyy-MM-ddThh:mm:ss");                     
             }
            
            
            
        }

        protected void saveBtnClick(object sender, EventArgs e)
        {
            string date = dateset.Value;
            
            int year = int.Parse(date.Substring(0,4));
            int month = int.Parse(date.Substring(5,2));
            int day = int.Parse(date.Substring(8,2));
            
            var mood = HWService.Mood.NotSet;

            if(moody.Value == "DontKnow") 
                mood = HWService.Mood.DontKnow;
            else if (moody.Value == "Unhappy")
                 mood = HWService.Mood.Unhappy;
            else if (moody.Value == "Neutral")
                 mood = HWService.Mood.Neutral;
            else if (moody.Value == "Happy")
                 mood = HWService.Mood.Happy;


            if (service.CalendarUpdate(token, mood, diarynote.Text, new DateTime(year, month, day), 10))
            {
                Response.Redirect("Calendar.aspx");
            }
        }

        protected void activitylink_Click(object sender, EventArgs e)
        {
            if (dateset.Value != "")
                Response.Redirect("ActivityMeasurement.aspx?datetime=" + dateset.Value);
            else
                Response.Redirect("ActivityMeasurement.aspx?datetime=" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + "");    
        }


    }
}