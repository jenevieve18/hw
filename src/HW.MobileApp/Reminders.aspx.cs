using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;


namespace HW.MobileApp
{
    public partial class Reminders : System.Web.UI.Page
    {
        protected HWService.ServiceSoap service = new HWService.ServiceSoapClient();
        protected string token; 

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlHelper.RedirectIf(Session["token"] == null, "Login.aspx");
            token = Session["token"].ToString();

            if (!Page.IsPostBack)
            {
                populateDropDownTime();

                HWService.Reminder reminder = service.UserGetReminder(token, 10);

                dropDownListReminder.SelectedValue = reminder.type.ToString();
                dropDownListInactivityTime.SelectedValue = reminder.sendAtHour.ToString();
                dropDownListInactivityCount.SelectedValue = reminder.inactivityCount.ToString();
                dropDownListInactivityPeriod.SelectedValue = reminder.inactivityPeriod.ToString();

                dropDownListRegularSchedule.SelectedValue = reminder.regularity.ToString();
                dropDownListRegularTime.SelectedValue = reminder.sendAtHour.ToString();

                cbMonday.Checked = reminder.regularityDailyMonday;
                cbTuesday.Checked = reminder.regularityDailyTuesday;
                cbWednesday.Checked = reminder.regularityDailyWednesday;
                cbThursday.Checked = reminder.regularityDailyThursday;
                cbFriday.Checked = reminder.regularityDailyFriday;
                cbSaturday.Checked = reminder.regularityDailySaturday;
                cbSunday.Checked = reminder.regularityDailySunday;
                dropDownListWeeklyDay.SelectedValue = reminder.regularityWeeklyDay.ToString();
                dropDownListWeeklyForEvery.SelectedValue = reminder.regularityWeeklyEvery.ToString();
                dropDownListMonthlyDayNo.SelectedValue = reminder.regularityMonthlyWeekNr.ToString();
                dropDownListMonthlyDay.SelectedValue = reminder.regularityMonthlyDay.ToString();
                dropDownListMonthlyOfEvery.SelectedValue = reminder.regularityMonthlyEvery.ToString();

                dropDownListReminder_SelectedIndexChanged(sender, e);
            }
        }

        protected void saveChangesBtn_Click(object sender, EventArgs e)
        {
            HWService.Reminder reminder = service.UserGetReminder(token, 10);

            reminder.type = int.Parse(dropDownListReminder.SelectedValue);
            
            reminder.inactivityCount = int.Parse(dropDownListInactivityCount.SelectedValue);
            reminder.inactivityPeriod = int.Parse(dropDownListInactivityPeriod.SelectedValue);

            if(reminder.type == 2)
                reminder.sendAtHour = int.Parse(dropDownListInactivityTime.SelectedValue);
            else if (reminder.type == 1)
                reminder.sendAtHour = int.Parse(dropDownListRegularTime.SelectedValue);

            reminder.regularity = int.Parse(dropDownListRegularSchedule.SelectedValue);
            reminder.regularityDailyMonday= cbMonday.Checked;
            reminder.regularityDailyTuesday= cbTuesday.Checked   ;
            reminder.regularityDailyWednesday= cbWednesday.Checked;
            reminder.regularityDailyThursday= cbThursday.Checked ;
            reminder.regularityDailyFriday= cbFriday.Checked     ;
            reminder.regularityDailySaturday= cbSaturday.Checked  ;
            reminder.regularityDailySunday = cbSunday.Checked;
            
            reminder.regularityWeeklyDay = int.Parse(dropDownListWeeklyDay.SelectedValue);
            reminder.regularityWeeklyEvery= int.Parse(dropDownListWeeklyForEvery.SelectedValue);
            reminder.regularityMonthlyWeekNr= int.Parse(dropDownListMonthlyDayNo.SelectedValue);
            reminder.regularityMonthlyDay = int.Parse(dropDownListMonthlyDay.SelectedValue);
            reminder.regularityMonthlyEvery = int.Parse(dropDownListMonthlyOfEvery.SelectedValue);

            if(service.UserSetReminder(token,reminder,10))
                Response.Redirect("Settings.aspx");
        }

        private void populateDropDownTime()
        {
            for (int i = 6; i <= 22; i++)
            {
                dropDownListRegularTime.Items.Add(new ListItem(i.ToString("D2") + ":00", i.ToString()));
                dropDownListInactivityTime.Items.Add(new ListItem(i.ToString("D2")+":00", i.ToString()));
            }
            dropDownListRegularTime.DataBind();
            dropDownListInactivityTime.DataBind();
        }

        protected void dropDownListReminder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownListReminder.SelectedValue == "1")
            {
                divReminderRegularly.Style["display"] = "block";
                divReminderInactivity.Style["display"] = "none";
            }
            else if (dropDownListReminder.SelectedValue == "2")
            {
                divReminderRegularly.Style["display"] = "none";
                divReminderInactivity.Style["display"] = "block";
            }
            else if (dropDownListReminder.SelectedValue == "0")
            {
                divReminderRegularly.Style["display"] = "none";
                divReminderInactivity.Style["display"] = "none";
            }
            dropDownListSchedule_SelectedIndexChanged(sender, e);
        }

        protected void dropDownListSchedule_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropDownListRegularSchedule.SelectedValue == "1")
            { 
                divScheduleDaily.Style["display"] = "block";
                divScheduleMonthly.Style["display"] = "none";
                divScheduleWeekly.Style["display"] = "none";
            }
            else if (dropDownListRegularSchedule.SelectedValue == "2")
            {
                divScheduleDaily.Style["display"] = "none";
                divScheduleMonthly.Style["display"] = "none";
                divScheduleWeekly.Style["display"] = "block";
            }
            else if (dropDownListRegularSchedule.SelectedValue == "3")
            {
                divScheduleDaily.Style["display"] = "none";
                divScheduleMonthly.Style["display"] = "block";
                divScheduleWeekly.Style["display"] = "none";
            }
        }
    }
}