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
    public partial class sponsorConsent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "default.aspx";
            bool redir = false;
            if (HttpContext.Current.Session["SponsorInviteID"] != null)
            {
                SqlDataReader rs = Db.rs("SELECT " +
                    "s.ConsentText " +
                    "FROM SponsorInvite i " +
                    "INNER JOIN Sponsor s ON i.SponsorID = s.SponsorID " +
                    "WHERE i.SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]));
                if (rs.Read())
                {
                    string txt = rs.GetString(0);
                    if (txt.IndexOf("<YES>") >= 0)
                    {
                        txt = txt.Replace("<YES>", "<BUTTON ONCLICK=\"location.href='register.aspx?Consent=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';return false;\">");
                        txt = txt.Replace("</YES>", "</BUTTON>");
                    }
                    else
                    {
                        txt = txt + "<BUTTON ONCLICK=\"location.href='sponsorConsent.aspx?Consent=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';return false;\">Ja</BUTTON>";
                    }
                    if (txt.IndexOf("<NO>") >= 0)
                    {
                        txt = txt.Replace("<NO>", "<BUTTON ONCLICK=\"location.href='register.aspx?Consent=0&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';return false;\">");
                        txt = txt.Replace("</NO>", "</BUTTON>");
                    }
                    else
                    {
                        txt = txt + "<BUTTON ONCLICK=\"location.href='sponsorConsent.aspx?Consent=0&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "';return false;\">Nej</BUTTON>";
                    }

                    contents.Text = txt;
                }
                else
                {
                    redir = true;
                    url = "sponsorConsent.aspx";
                }
                rs.Close();
            }
            else
            {
                redir = true;
            }
            if (redir)
            {
                HttpContext.Current.Response.Redirect(url + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
        }
    }
}