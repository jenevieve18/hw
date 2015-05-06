using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class messages : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Save.Click += new EventHandler(Save_Click);
		RecalculateReminders.Click += new EventHandler(RecalculateReminders_Click);

        if (!IsPostBack)
        {
            SqlDataReader rs = Db.rs("SELECT " +
                "ReminderEmail, " +
                "ReminderMessage, " +
                "ReminderSubject " +
                "FROM SystemSettings " +
                "WHERE SystemID = 1");
            if (rs.Read())
            {
                ReminderEmail.Text = (rs.IsDBNull(0) ? "" : rs.GetString(0));
                ReminderMessage.Text = (rs.IsDBNull(1) ? "" : rs.GetString(1));
                ReminderSubject.Text = (rs.IsDBNull(2) ? "" : rs.GetString(2));
            }
            rs.Close();
        }
    }

	void RecalculateReminders_Click(object sender, EventArgs e)
	{
		SqlDataReader rs = Db.rs("SELECT " +
			"u.UserID, " +
			"u.ReminderType, " +
			"u.ReminderSettings, " +
			"u.ReminderLastSent, " +
			"(SELECT TOP 1 DT FROM Session s WHERE s.UserID = u.UserID ORDER BY SessionID DESC) AS LastLogin " +
			"FROM [User] u WHERE u.Reminder IS NOT NULL AND u.Reminder > 0 AND u.ReminderType IS NOT NULL");
		while (rs.Read())
		{
			DateTime lastSent = (rs.IsDBNull(3) ? DateTime.Now : rs.GetDateTime(3));
			DateTime lastLogin = (rs.IsDBNull(4) ? DateTime.Now : rs.GetDateTime(4));
			Db.exec("UPDATE [User] SET ReminderNextSend = '" + nextReminderSend(rs.GetInt32(1), rs.GetString(2).Split(':'), lastLogin, lastSent) + "' WHERE UserID = " + rs.GetInt32(0));
		}
		rs.Close();
	}

    void Save_Click(object sender, EventArgs e)
    {
        Db.exec("UPDATE SystemSettings SET " +
            "ReminderEmail = '" + ReminderEmail.Text.Replace("'", "''") + "', " +
            "ReminderMessage = '" + ReminderMessage.Text.Replace("'", "''") + "', " +
            "ReminderSubject = '" + ReminderSubject.Text.Replace("'", "''") + "' " +
            "WHERE SystemID = 1");

        HttpContext.Current.Response.Redirect("messages.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
    }

	private string nextReminderSend(int type, string[] settings, DateTime lastLogin, DateTime lastSend)
	{
		DateTime nextPossibleReminderSend = lastSend.Date.AddHours(Convert.ToInt32(settings[0]));
		while (nextPossibleReminderSend <= DateTime.Now.AddMinutes(30))
		{
			nextPossibleReminderSend = nextPossibleReminderSend.AddDays(1);
		}
		DateTime nextReminderSend = nextPossibleReminderSend.AddYears(10);

		try
		{
			switch (type)
			{
				case 1:
					System.DayOfWeek[] dayOfWeek = { System.DayOfWeek.Monday, System.DayOfWeek.Tuesday, System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday, System.DayOfWeek.Saturday, System.DayOfWeek.Sunday };

					switch (Convert.ToInt32(settings[1]))
					{
						case 1:
							#region Weekday
							{
								string[] days = settings[2].Split(',');
								foreach (string day in days)
								{
									DateTime tmp = nextPossibleReminderSend;
									while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(day) - 1])
									{
										tmp = tmp.AddDays(1);
									}
									if (tmp < nextReminderSend)
									{
										nextReminderSend = tmp;
									}
								}
								break;
							}
							#endregion
						case 2:
							#region Week
							{
								nextReminderSend = nextPossibleReminderSend.AddDays(7 * (Convert.ToInt32(settings[3]) - 1));
								while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[2]) - 1])
								{
									nextReminderSend = nextReminderSend.AddDays(1);
								}
								break;
							}
							#endregion
						case 3:
							#region Month
							{
								DateTime tmp = nextPossibleReminderSend.AddDays(-nextPossibleReminderSend.Day);
								int i = 0;
								while (tmp.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
								{
									tmp = tmp.AddDays(1);
									if (tmp.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
									{
										i++;
									}
								}
								nextReminderSend = nextPossibleReminderSend.AddMonths((Convert.ToInt32(settings[4]) - 1));
								if (tmp < nextPossibleReminderSend)
								{
									// Has allready occurred this month
									nextReminderSend = nextReminderSend.AddMonths(1);
								}
								nextReminderSend = nextReminderSend.AddDays(-nextReminderSend.Day);
								i = 0;
								while (nextReminderSend.DayOfWeek != dayOfWeek[Convert.ToInt32(settings[3]) - 1] || i != Convert.ToInt32(settings[2]))
								{
									nextReminderSend = nextReminderSend.AddDays(1);
									if (nextReminderSend.DayOfWeek == dayOfWeek[Convert.ToInt32(settings[3]) - 1])
									{
										i++;
									}
								}
								break;
							}
							#endregion
					}
					break;
				case 2:
					nextReminderSend = lastLogin.Date.AddHours(Convert.ToInt32(settings[0])).AddDays(Convert.ToInt32(settings[1]) * Convert.ToInt32(settings[2]));
					while (nextReminderSend < nextPossibleReminderSend)
					{
						nextReminderSend = nextReminderSend.AddDays(7);
					}
					break;
			}
		}
		catch (Exception)
		{
			nextReminderSend = nextPossibleReminderSend.AddYears(10);
		}

		return nextReminderSend.ToString("yyyy-MM-dd HH:mm");
	}
}
