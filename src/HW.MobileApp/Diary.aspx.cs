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
        
        protected HWService.Calendar calendar;

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();
            lang = int.Parse(Session["languageId"].ToString());

            if (!Page.IsPostBack)
            {
                ddlDate();
                
                if (Request.QueryString["date"] != null)
                {
                    var temp = Request.QueryString["date"];
                    int year = int.Parse(temp.Substring(0, 4));
                    int month = int.Parse(temp.Substring(5, 2));
                    int day = int.Parse(temp.Substring(8, 2));
                   
                    dropDownListDateDay.SelectedValue = day.ToString("D2");
                    dropDownListDateMonth.SelectedValue = month.ToString("D2");
                    dropDownListDateYear.SelectedValue = year.ToString();
                }
                if(!unsavedValues())
                setFields();
                
            }
            
            
        }

        protected bool unsavedValues()
        {
            if (Session["note"] != null)
            {
                textBoxNote.Text = Session["note"].ToString();
                Session.Remove("note");
            }
            if (Session["mood"] != null)
            {
                var mood = Session["mood"].ToString();
                if (mood == "DontKnow") { rdbDontKnow.Checked = true; }
                else if (mood == "Happy") { rdbHappy.Checked = true; }
                else if (mood == "Neutral") { rdbNeutral.Checked = true; }
                else if (mood == "Unhappy") { rdbUnhappy.Checked = true; }

                Session.Remove("mood");
                return true;
            }
            return false;
        }

        protected void setFields()
        {
            textBoxNote.Text = "";
            rdbDontKnow.Checked = false; rdbHappy.Checked = false; rdbNeutral.Checked = false; rdbUnhappy.Checked = false;

            calendar = service.CalendarEnum(new HWService.CalendarEnumRequest(token, selectedDate(), selectedDate(), lang, 10)).CalendarEnumResult.DefaultIfEmpty(null).First();
            if (calendar != null)
            {
                textBoxNote.Text = calendar.note;

                if (calendar.mood == HWService.Mood.DontKnow)    { rdbDontKnow.Checked = true;  }
                else if (calendar.mood == HWService.Mood.Happy)  { rdbHappy.Checked = true;     }
                else if (calendar.mood == HWService.Mood.Neutral){ rdbNeutral.Checked = true;   }
                else if (calendar.mood == HWService.Mood.Unhappy){ rdbUnhappy.Checked = true;   }
            }
            
        }

        protected void ddlDate()
        {
            dropDownListDateDay.DataSource = Enumerable.Range(1, 31).Select(i => i.ToString("D2"));
            dropDownListDateYear.DataSource = Enumerable.Range(1990, DateTime.Now.Year - 1989);
            dropDownListDateDay.DataBind();
            dropDownListDateYear.DataBind();
            dropDownListDateDay.SelectedValue = DateTime.Now.Day.ToString("D2");
            dropDownListDateMonth.SelectedValue = DateTime.Now.Month.ToString("D2"); ;
            dropDownListDateYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected DateTime selectedDate()
        {
            return new DateTime(int.Parse(dropDownListDateYear.SelectedValue), int.Parse(dropDownListDateMonth.SelectedValue), int.Parse(dropDownListDateDay.SelectedValue));
        }

        protected HWService.Mood selectedMood()
        {
            if (rdbDontKnow.Checked) { return HWService.Mood.DontKnow; }
            else if (rdbHappy.Checked) { return HWService.Mood.Happy; }
            else if (rdbNeutral.Checked) { return HWService.Mood.Neutral; }
            else if (rdbUnhappy.Checked) { return HWService.Mood.Unhappy; }
            else { return HWService.Mood.NotSet; }
        }

        protected void saveBtnClick(object sender, EventArgs e)
        {
            HWService.Mood mood = selectedMood();
    

            if (service.CalendarUpdate(token, mood, textBoxNote.Text, selectedDate(), 10))
            {
                Response.Redirect("Calendar.aspx");
                
            }
        }

        
        protected void activitylink_Click(object sender, EventArgs e)
        {
            Session.Add("note", textBoxNote.Text);
            Session.Add("mood", selectedMood().ToString());
            Response.Redirect("ActivityMeasurement.aspx?datetime=" + selectedDate().ToString("yyyy-MM-ddTHH:mm:ss"));
        }

        protected void dropDownListDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFields();
        }


    }
}