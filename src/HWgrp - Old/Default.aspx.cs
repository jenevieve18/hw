using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool superAdmin = false, login = false;
        SqlDataReader r;

        if (HttpContext.Current.Request.Form["ANV"] != null && HttpContext.Current.Request.Form["LOS"] != null || HttpContext.Current.Request.QueryString["SKEY"] != null || HttpContext.Current.Request.QueryString["SAKEY"] != null)
        {
            login = true;
            r = Db.rs("SELECT " +
                "s.SponsorID, " +                                                                               // 0
                (HttpContext.Current.Request.QueryString["SKEY"] == null ? "sa.SponsorAdminID, " : "-1, ") +    // 1
                "s.Sponsor, " +                                                                                 // 2
                (HttpContext.Current.Request.QueryString["SKEY"] == null ? "sa.Anonymized, " : "NULL, ") +      // 3
                (HttpContext.Current.Request.QueryString["SKEY"] == null ? "sa.SeeUsers, " : (HttpContext.Current.Request.QueryString["SA"] != null ? "sas.SeeUsers, " : "1, ")) +           // 4
                "NULL, " +                                                                                      // 5
                (HttpContext.Current.Request.QueryString["SKEY"] == null ? "sa.ReadOnly, " : "NULL, ") +        // 6
                (HttpContext.Current.Request.QueryString["SKEY"] == null ? "ISNULL(sa.Name,sa.Usr) " : "'Internal administrator' ") +        // 7
                "FROM Sponsor s " +
                (HttpContext.Current.Request.Form["ANV"] != null && HttpContext.Current.Request.Form["LOS"] != null || HttpContext.Current.Request.QueryString["SAKEY"] != null ?
                    "INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID " +
                    (HttpContext.Current.Request.QueryString["SAKEY"] != null ?
                        "WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '" + HttpContext.Current.Request.QueryString["SAKEY"].Substring(0, 8).Replace("'", "") + "' " +
                        "AND s.SponsorID = " + HttpContext.Current.Request.QueryString["SAKEY"].Substring(8).Replace("'", "")
                        :
                        "WHERE sa.Usr = '" + HttpContext.Current.Request.Form["ANV"].Replace("'", "") + "' " +
                        "AND sa.Pas = '" + HttpContext.Current.Request.Form["LOS"].Replace("'", "") + "'")
                    :
                    (HttpContext.Current.Request.QueryString["SA"] != null ?
                        "INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SuperAdminID"]) + " "
                        :
                        ""
                    ) +
                    "WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '" + HttpContext.Current.Request.QueryString["SKEY"].Substring(0, 8).Replace("'", "") + "' " +
                    "AND s.SponsorID = " + HttpContext.Current.Request.QueryString["SKEY"].Substring(8).Replace("'", "")
                ) + " " +
                (HttpContext.Current.Request.QueryString["SKEY"] == null && HttpContext.Current.Request.QueryString["SAKEY"] == null ?
                "UNION ALL " +

                "SELECT " +
                "NULL, NULL, NULL, NULL, NULL, sa.SuperAdminID, NULL, sa.Username " +
                "FROM SuperAdmin sa " +
                "WHERE sa.Username = '" + HttpContext.Current.Request.Form["ANV"].Replace("'", "") + "' " +
                "AND sa.Password = '" + HttpContext.Current.Request.Form["LOS"].Replace("'", "") + "'"
                : ""));
            if (r.Read())
            {
                HttpContext.Current.Session["Name"] = r.GetString(7);
                if (!r.IsDBNull(5))
                {
                    HttpContext.Current.Session["SuperAdminID"] = r.GetInt32(5);
                    superAdmin = true;
                }
                else
                {
                    HttpContext.Current.Session["SponsorID"] = r.GetInt32(0);
                    HttpContext.Current.Session["SponsorAdminID"] = r.GetInt32(1);
                    HttpContext.Current.Session["Sponsor"] = r.GetString(2);
                    HttpContext.Current.Session["Anonymized"] = (r.IsDBNull(3) ? 0 : 1);
                    HttpContext.Current.Session["SeeUsers"] = (r.IsDBNull(4) ? 0 : 1);
                    HttpContext.Current.Session["ReadOnly"] = (r.IsDBNull(6) ? 0 : 1);
                }
            }
            r.Close();
        }

        if(login && (HttpContext.Current.Session["SponsorAdminID"] != null || HttpContext.Current.Session["SuperAdminID"] != null))
        {
            string firstUrl = "default.aspx?Logout=1&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
            r = Db.rs("SELECT TOP 1 " +
                "mf.ManagerFunction, " +
                "mf.URL, " +
                "mf.Expl " +
                "FROM ManagerFunction mf " +
                (HttpContext.Current.Session["SponsorAdminID"] != null && HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
                "INNER JOIN SponsorAdminFunction s ON s.ManagerFunctionID = mf.ManagerFunctionID " +
                "WHERE s.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] : ""));
            while (r.Read())
            {
                firstUrl = r.GetString(1) + "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next();
            }
            r.Close();
            if (superAdmin)
            {
                HttpContext.Current.Response.Redirect("superadmin.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
            }
            else if (HttpContext.Current.Session["SponsorID"] != null)
            {
                HttpContext.Current.Response.Redirect(firstUrl, true);
            }
        }
        else if (login && HttpContext.Current.Session["SponsorAdminID"] != null || HttpContext.Current.Request.QueryString["Logout"] != null)
		{
            HttpContext.Current.Session.Remove("SponsorID");
            HttpContext.Current.Session.Remove("SponsorAdminID");
            HttpContext.Current.Session.Remove("Sponsor");
            HttpContext.Current.Session.Remove("Anonymized");
            HttpContext.Current.Session.Remove("SeeUsers");
            HttpContext.Current.Session.Remove("ReadOnly");
			//HttpContext.Current.Session.Abandon();
			//HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);

            ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
        }
        else if (login && HttpContext.Current.Session["SuperAdminID"] != null || HttpContext.Current.Request.QueryString["SuperLogout"] != null)
        {
            HttpContext.Current.Session.Remove("SuperAdminID");

            ClientScript.RegisterStartupScript(this.GetType(), "CLOSE", "<script language=\"JavaScript\">window.close();</script>");
        }
    }
}
