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

namespace healthWatch
{
    public partial class _404 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.RawUrl.EndsWith("/news"))
            {
                //This needed?
                //HttpContext.Current.Response.StatusCode = 200;
                HttpContext.Current.Server.Transfer("/home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.EndsWith("/myhealth"))
            {
                HttpContext.Current.Server.Transfer("/myhealth.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.EndsWith("/about"))
            {
                HttpContext.Current.Server.Transfer("/about.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.EndsWith("/news"))
            {
                HttpContext.Current.Server.Transfer("/home.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.EndsWith("/faq"))
            {
                HttpContext.Current.Server.Transfer("/faq.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.EndsWith("/contact"))
            {
                HttpContext.Current.Server.Transfer("/contact.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.EndsWith("/code"))
            {
                HttpContext.Current.Server.Transfer("/code.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.IndexOf("/code/") >= 0)
            {
                string code = "";
                try
                {
                    code = "&Code=" + HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.IndexOf("/code/") + 6);
                }
                catch (Exception) { }
                HttpContext.Current.Server.Transfer("/code.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + code, true);
            }
            else if (HttpContext.Current.Request.RawUrl.IndexOf("/news/") >= 0)
            {
                string news = HttpContext.Current.Request.RawUrl;
                news = news.Substring(news.IndexOf("/news/") + 6);
                string category = "";
                if (news.IndexOf("/") >= 0)
                {
                    category = news.Substring(0, news.IndexOf("/"));
                    news = news.Substring(news.IndexOf("/") + 1);
                }
                string url = "";
                SqlDataReader rs = Db.rs("SELECT TOP 1 " +
                    "n.NewsID, " +
                    "nc.NewsCategoryID " +
                    "FROM News n " +
                    "LEFT OUTER JOIN NewsCategory nc ON n.NewsCategoryID = nc.NewsCategoryID " +
                    "WHERE n.HeadlineShort = '" + news.Replace("'", "") + "' " +
                    "ORDER BY n.NewsID DESC", "newsSqlConnection");
                if (rs.Read())
                {
                    url = "news.aspx?NID=" + rs.GetInt32(0) + (!rs.IsDBNull(1) && category != "" ? "&NCID=" + rs.GetInt32(1) : "");
                }
                rs.Close();
                if (url == "")
                {
                    rs = Db.rs("SELECT NewsCategoryID FROM NewsCategory WHERE NewsCategoryShort = '" + news.Replace("'", "") + "'", "newsSqlConnection");
                    if (rs.Read())
                    {
                        url = "home.aspx?NCID=" + rs.GetInt32(0);
                    }
                    rs.Close();
                }
                if (url == "")
                {
                    HttpContext.Current.Response.Redirect("/error.aspx", true);
                }
                else
                {
                    HttpContext.Current.Server.Transfer("/" + url + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
                }
            }
            else if (HttpContext.Current.Request.RawUrl.IndexOf("/i/") >= 0)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
                {
                    Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL AND SessionID = " + Convert.ToInt32(HttpContext.Current.Session["SessionID"]));
                    HttpContext.Current.Session.Abandon();
                }
                HttpContext.Current.Session["SponsorInviteID"] = -1;
                string url = "/register.aspx";
                try
                {
                    string invite = HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.IndexOf("/i/") + 3);
                    SqlDataReader rs = Db.rs("SELECT " +
                        "i.SponsorInviteID, " +     // 0
                        "i.SponsorID, " +
                        "i.DepartmentID, " +
                        "LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8), " +
                        "i.Email, " +
                        "s.LID, " +                 // 5
                        "s.InfoText, " +
                        "s.ConsentText, " +
                        "s.ForceLID " +
                        "FROM SponsorInvite i " +
                        "INNER JOIN Sponsor s ON i.SponsorID = s.SponsorID " +
                        "WHERE i.SponsorInviteID = " + Convert.ToInt32(invite.Substring(8)) + " " +
                        "AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),i.InvitationKey),'-',''),8)) = '" + invite.Substring(0, 8).Replace("'", "").ToLower() + "'");
                    if (rs.Read())
                    {
                        if (!rs.IsDBNull(5) || !rs.IsDBNull(8))
                        {
                            HttpContext.Current.Session["LID"] = (rs.IsDBNull(8) ? rs.GetInt32(5) : rs.GetInt32(8));
                            HttpContext.Current.Response.Cookies["HW"]["LID"] = (rs.IsDBNull(8) ? rs.GetInt32(5) : rs.GetInt32(8)).ToString();
                            HttpContext.Current.Response.Cookies["HW"].Expires = DateTime.Now.AddYears(1);
                        }
                        if (!rs.IsDBNull(8))
                        {
                            HttpContext.Current.Session["ForceLID"] = rs.GetInt32(8);
                        }
                        HttpContext.Current.Session["SponsorInviteID"] = rs.GetInt32(0);
                        Db.loadSponsor(rs.GetInt32(1).ToString(), rs.GetString(3));
                        HttpContext.Current.Session["DepartmentID"] = (rs.IsDBNull(2) ? "NULL" : rs.GetInt32(2).ToString());
                        HttpContext.Current.Session["Email"] = (rs.IsDBNull(4) ? "" : rs.GetString(4));
                        if (!rs.IsDBNull(6))
                        {
                            url = "/sponsorInformation.aspx";
                        }
                        else if (!rs.IsDBNull(7))
                        {
                            url = "/sponsorConsent.aspx";
                        }
                    }
                    rs.Close();
                }
                catch (Exception) { }
                HttpContext.Current.Response.Redirect(url + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Request.RawUrl.IndexOf("/c/") >= 0)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
                {
                    Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL AND SessionID = " + Convert.ToInt32(HttpContext.Current.Session["SessionID"]));
                    HttpContext.Current.Session.Abandon();
                }
                int userID = 0;
                try
                {
                    string user = HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.IndexOf("/c/") + 3);
                    SqlDataReader rs = Db.rs("SELECT " +
                        "u.UserID, " +
                        "u.ReminderLink " +
                        "FROM [User] u " +
                        "WHERE u.UserID = " + Convert.ToInt32(user.Substring(12)) + " " +
                        "AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)) = '" + user.Substring(0, 12).Replace("'", "").ToLower() + "'");
                    if (rs.Read())
                    {
                        userID = rs.GetInt32(0);
                        if (rs.GetInt32(1) == 2)
                        {
                            Db.exec("UPDATE [User] SET UserKey = NEWID() WHERE UserID = " + userID);
                        }
                    }
                    rs.Close();
                }
                catch (Exception) { }
                if (userID != 0)
                {
                    Db.checkAndLogin(userID);
                }
                HttpContext.Current.Response.Redirect("/invalidLogin.aspx", true);
            }
            else if (HttpContext.Current.Request.RawUrl.IndexOf("/a/") >= 0)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
                {
                    Db.exec("UPDATE [Session] SET EndDT = GETDATE() WHERE EndDT IS NULL AND SessionID = " + Convert.ToInt32(HttpContext.Current.Session["SessionID"]));
                    HttpContext.Current.Session.Abandon();
                }
                int userID = 0;
                try
                {
                    string user = HttpContext.Current.Request.RawUrl.Substring(HttpContext.Current.Request.RawUrl.IndexOf("/a/") + 3);
                    SqlDataReader rs = Db.rs("SELECT " +
                        "u.UserID " +
                        "FROM [User] u " +
                        "WHERE u.UserID = " + Convert.ToInt32(user.Substring(12)) + " " +
                        "AND LOWER(LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)) = '" + user.Substring(0, 12).Replace("'", "").ToLower() + "'");
                    if (rs.Read())
                    {
                        userID = rs.GetInt32(0);
                    }
                    rs.Close();
                }
                catch (Exception) { }
                if (userID != 0)
                {
                    Db.checkAndLogin(userID, false);
                }
            }
            HttpContext.Current.Response.Redirect("/error.aspx", true);
        }
    }
}