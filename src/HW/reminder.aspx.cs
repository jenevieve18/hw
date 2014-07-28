using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.FromHW;

namespace HW
{
    public partial class reminder : System.Web.UI.Page
    {
        public int sponsorLoginDays = 0;
        public string email = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                HttpContext.Current.Response.Redirect("inactivity.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            if (!IsPostBack)
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 2:
                        reminderType.Items.FindByValue("0").Text = "Never";
                        reminderType.Items.FindByValue("1").Text = "Regularly";
                        reminderType.Items.FindByValue("2").Text = "At inactivity";
                        reminderRepeat.Items.FindByValue("1").Text = "Daily";
                        reminderRepeat.Items.FindByValue("2").Text = "Weekly";
                        reminderRepeat.Items.FindByValue("3").Text = "Monthly";
                        reminderRepeatDay.Items.FindByValue("1").Text = "Monday";
                        reminderRepeatDay.Items.FindByValue("2").Text = "Tuesday";
                        reminderRepeatDay.Items.FindByValue("3").Text = "Wednesday";
                        reminderRepeatDay.Items.FindByValue("4").Text = "Thursday";
                        reminderRepeatDay.Items.FindByValue("5").Text = "Friday";
                        reminderRepeatDay.Items.FindByValue("6").Text = "Saturday";
                        reminderRepeatDay.Items.FindByValue("7").Text = "Sunday";
                        reminderRepeatWeekDay.Items.FindByValue("1").Text = "mondays";
                        reminderRepeatWeekDay.Items.FindByValue("2").Text = "tuesdays";
                        reminderRepeatWeekDay.Items.FindByValue("3").Text = "wednesdays";
                        reminderRepeatWeekDay.Items.FindByValue("4").Text = "thursdays";
                        reminderRepeatWeekDay.Items.FindByValue("5").Text = "fridays";
                        reminderRepeatWeekDay.Items.FindByValue("6").Text = "saturdays";
                        reminderRepeatWeekDay.Items.FindByValue("7").Text = "sundays";
                        reminderRepeatWeek.Items.FindByValue("1").Text = "every week";
                        reminderRepeatWeek.Items.FindByValue("2").Text = "every other week";
                        reminderRepeatWeek.Items.FindByValue("3").Text = "every third week";
                        reminderRepeatMonthWeek.Items.FindByValue("1").Text = "first";
                        reminderRepeatMonthWeek.Items.FindByValue("2").Text = "second";
                        reminderRepeatMonthWeek.Items.FindByValue("3").Text = "third";
                        reminderRepeatMonthWeek.Items.FindByValue("4").Text = "fourth";
                        reminderRepeatMonthDay.Items.FindByValue("1").Text = "monday";
                        reminderRepeatMonthDay.Items.FindByValue("2").Text = "tuesday";
                        reminderRepeatMonthDay.Items.FindByValue("3").Text = "wednesday";
                        reminderRepeatMonthDay.Items.FindByValue("4").Text = "thursday";
                        reminderRepeatMonthDay.Items.FindByValue("5").Text = "friday";
                        reminderRepeatMonthDay.Items.FindByValue("6").Text = "saturday";
                        reminderRepeatMonthDay.Items.FindByValue("7").Text = "sunday";
                        reminderRepeatMonth.Items.FindByValue("1").Text = "in every";
                        reminderRepeatMonth.Items.FindByValue("2").Text = "in every other";
                        reminderRepeatMonth.Items.FindByValue("3").Text = "in every thrid";
                        reminderRepeatMonth.Items.FindByValue("6").Text = "in every sixth";
                        reminderInactivityPeriod.Items.FindByValue("1").Text = "day/days";
                        reminderInactivityPeriod.Items.FindByValue("7").Text = "week/weeks";
                        reminderInactivityPeriod.Items.FindByValue("30").Text = "month/months";
                        reminderLink.Items.FindByValue("0").Text = "No";
                        reminderLink.Items.FindByValue("1").Text = "Yes, with a link that is the same every time and can be bookmarked";
                        reminderLink.Items.FindByValue("2").Text = "Yes, with a link that is only valid for one login (differs every time; recommended)";
                        submit.Text = "Save";
                        break;
                }
            }

            bool guest = false;
            SqlDataReader rs = Db.rs("SELECT " +
                "u.Username, " +		// 0
                "s.LoginDays, " +		// 1
                "u.Reminder, " +		// 2
                "u.ReminderType, " +	// 3
                "u.ReminderLink, " +	// 4
                "u.ReminderSettings, " +// 5
                "u.Email " +			// 6
                "FROM [User] u " +
                "LEFT OUTER JOIN Sponsor s ON u.SponsorID = s.SponsorID " +
                "WHERE u.UserID = " + HttpContext.Current.Session["UserID"]);
            if (rs.Read())
            {
                guest = (rs.GetString(0).IndexOf("AUTO_CREATED_GUEST") >= 0);
                if (!guest)
                {
                    sponsorLoginDays = (!rs.IsDBNull(1) ? rs.GetInt32(1) : 0);
                    email = (!rs.IsDBNull(6) ? rs.GetString(6) : "[e-postadress saknas]");

                    if (!IsPostBack)
                    {
                        reminderType.Attributes["onclick"] += "self.updateReminder();";
                        reminderRepeat.Attributes["onclick"] += "self.updateReminder();";

                        if (!rs.IsDBNull(2))
                        {
                            // Reminder previously set

                            reminderType.SelectedValue = (rs.GetInt32(2) == 0 || rs.IsDBNull(3) ? "0" : rs.GetInt32(3).ToString());
                            reminderLink.SelectedValue = (rs.IsDBNull(4) ? "0" : rs.GetInt32(4).ToString());

                            string[] settings = (rs.IsDBNull(5) ? "" : rs.GetString(5)).Split(':');

                            switch (rs.IsDBNull(3) ? 0 : rs.GetInt32(3))
                            {
                                case 1:
                                    reminderHour.SelectedValue = settings[0];
                                    reminderRepeat.SelectedValue = settings[1];
                                    switch (Convert.ToInt32(settings[1]))
                                    {
                                        case 1:
                                            string[] days = settings[2].Split(',');
                                            for (int i = 1; i <= 7; i++)
                                            {
                                                reminderRepeatDay.Items.FindByValue(i.ToString()).Selected = false;
                                            }
                                            foreach (string day in days)
                                            {
                                                reminderRepeatDay.Items.FindByValue(day).Selected = true;
                                            }
                                            break;
                                        case 2:
                                            reminderRepeatWeekDay.SelectedValue = settings[2];
                                            reminderRepeatWeek.SelectedValue = settings[3];
                                            break;
                                        case 3:
                                            reminderRepeatMonthWeek.SelectedValue = settings[2];
                                            reminderRepeatMonthDay.SelectedValue = settings[3];
                                            reminderRepeatMonth.SelectedValue = settings[4];
                                            break;
                                    }
                                    break;
                                case 2:
                                    reminderHour.SelectedValue = settings[0];
                                    reminderInactivityCount.SelectedValue = settings[1];
                                    reminderInactivityPeriod.SelectedValue = settings[2];
                                    break;
                            }
                        }
                        else
                        {
                            reminderHour.SelectedValue = (new Random(unchecked((int)DateTime.Now.Ticks))).Next(8, 11).ToString();
                            for (int i = 1; i <= 7; i++)
                            {
                                reminderRepeatDay.Items.FindByValue(i.ToString()).Selected = false;
                            }
                            reminderRepeatDay.Items.FindByValue((new Random(unchecked((int)DateTime.Now.Ticks))).Next(1, 5).ToString()).Selected = true;
                        }
                    }
                    else
                    {
                        // Posted values
                    }
                }
            }
            rs.Close();

            if (guest)
            {
                HttpContext.Current.Response.Redirect("home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }

            submit.Click += new EventHandler(submit_Click);
        }

        private void submit_Click(object sender, EventArgs e)
        {
            if (reminderType.SelectedValue == "0")
            {
                Db.exec("UPDATE [User] SET " +
                    "Reminder = 0, " +
                    "ReminderLink = " + Convert.ToInt32(reminderLink.SelectedValue) + ", " +
                    "ReminderType = " + Convert.ToInt32(reminderType.SelectedValue) + ", " +
                    "ReminderNextSend = NULL " +
                    "WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
            }
            else
            {
                string settings = reminderHour.SelectedValue;
                switch (Convert.ToInt32(reminderType.SelectedValue))
                {
                    case 1:
                        settings += ":" + reminderRepeat.SelectedValue;
                        switch (Convert.ToInt32(reminderRepeat.SelectedValue))
                        {
                            case 1:
                                string days = "";
                                for (int i = 1; i <= 7; i++)
                                {
                                    if (reminderRepeatDay.Items.FindByValue(i.ToString()).Selected)
                                    {
                                        days += (days != "" ? "," : "") + i.ToString();
                                    }
                                }
                                settings += ":" + (days != "" ? days : "1");
                                break;
                            case 2:
                                settings += ":" + reminderRepeatWeekDay.SelectedValue;
                                settings += ":" + reminderRepeatWeek.SelectedValue;
                                break;
                            case 3:
                                settings += ":" + reminderRepeatMonthWeek.SelectedValue;
                                settings += ":" + reminderRepeatMonthDay.SelectedValue;
                                settings += ":" + reminderRepeatMonth.SelectedValue;
                                break;
                        }
                        break;
                    case 2:
                        settings += ":" + reminderInactivityCount.SelectedValue;
                        settings += ":" + reminderInactivityPeriod.SelectedValue;
                        break;

                }
                Db.exec("UPDATE [User] SET " +
                    (reminderLink.SelectedValue == "2" ? "UserKey = NEWID(), " : "") +
                    "Reminder = 1, " +
                    "ReminderLink = " + Convert.ToInt32(reminderLink.SelectedValue) + ", " +
                    "ReminderType = " + Convert.ToInt32(reminderType.SelectedValue) + ", " +
                    "ReminderSettings = '" + settings.Replace("'", "") + "', " +
                    "ReminderNextSend = '" + Db.nextReminderSend(Convert.ToInt32(reminderType.SelectedValue), settings.Split(':'), DateTime.Now, DateTime.Now) + "' " +
                    "WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
            }
            if (Convert.ToInt32(HttpContext.Current.Session["NoReminderSet"]) == 1)
            {
                HttpContext.Current.Session["NoReminderSet"] = null;
                HttpContext.Current.Response.Redirect("submit.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else
            {
                HttpContext.Current.Response.Redirect("calendar.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
        }
    }
}