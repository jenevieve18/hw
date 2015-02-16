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
    public partial class sponsorInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "default.aspx";
            bool redir = false;
            SqlDataReader rs = null;
            if (HttpContext.Current.Session["SponsorInviteID"] != null)
            {
                rs = Db.rs("SELECT " +
                    "s.InfoText " +
                    "FROM SponsorInvite i " +
                    "INNER JOIN Sponsor s ON i.SponsorID = s.SponsorID " +
                    "WHERE i.SponsorInviteID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorInviteID"]));
            }
            else if (HttpContext.Current.Request.QueryString["SponsorID"] != null)
            {
                rs = Db.rs("SELECT " +
                    "s.InfoText " +
                    "FROM Sponsor s " +
                    "WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorID"]));
            }
            if (rs != null)
            {
                if (rs.Read())
                {
                    string txt = rs.GetString(0);
                    if (txt.IndexOf("<CONT>") >= 0)
                    {
                        txt = txt.Replace("<CONT>", "<BUTTON ONCLICK=\"location.href='sponsorConsent.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (HttpContext.Current.Request.QueryString["SponsorID"] != null ? "&SponsorID=" + HttpContext.Current.Request.QueryString["SponsorID"] : "") + "';return false;\">");
                        txt = txt.Replace("</CONT>", "</BUTTON>");
                    }
                    else
                    {
                        txt = txt + "<BUTTON ONCLICK=\"location.href='sponsorConsent.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (HttpContext.Current.Request.QueryString["SponsorID"] != null ? "&SponsorID=" + HttpContext.Current.Request.QueryString["SponsorID"] : "") + "';return false;\">Fortsätt</BUTTON>";
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
                HttpContext.Current.Response.Redirect(url + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + (HttpContext.Current.Request.QueryString["SponsorID"] != null ? "&SponsorID=" + HttpContext.Current.Request.QueryString["SponsorID"] : ""), true);
            }
        }
    }
}