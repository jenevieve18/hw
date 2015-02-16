using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.FromHW;

namespace HW
{
    public partial class exerciseShow : System.Web.UI.Page
    {
        protected string replacementHead = "";
        protected string headerText = "";
        protected string logos = "";
        protected int LID = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["LID"] != null)
            {
                LID = Convert.ToInt32(HttpContext.Current.Session["LID"]);
            }
            if (HttpContext.Current.Request.QueryString["LID"] != null)
            {
                LID = Convert.ToInt32(HttpContext.Current.Request.QueryString["LID"]);
            }

            int UID = 0, UPID = 0;
            if (HttpContext.Current.Request.QueryString["AUID"] != null)
            {
                UID = -Convert.ToInt32(HttpContext.Current.Request.QueryString["AUID"]);
                if (HttpContext.Current.Request.QueryString["SID"] != null)
                {
                    SqlDataReader r = Db.rs("SELECT " +
                        "s.Sponsor, " +
                        "ss.SuperSponsorID, " +
                        "ssl.Header " +
                        "FROM Sponsor s " +
                        "INNER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID " +
                        "LEFT OUTER JOIN SuperSponsorLang ssl ON ss.SuperSponsorID = ssl.SuperSponsorID AND ssl.LangID = 1 " +
                        "WHERE s.SponsorID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]));
                    if (r.Read())
                    {
                        if (!r.IsDBNull(1))
                        {
                            logos += "<img src=\"img/partner/" + r.GetInt32(1) + ".gif\"/>";
                        }
                        if (!r.IsDBNull(2))
                        {
                            headerText += " - " + r.GetString(2);
                        }
                    }
                    r.Close();
                }
            }
            else if (HttpContext.Current.Session["UserID"] != null)
            {
                UID = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                if (Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSOR" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])]) > 0 && HttpContext.Current.Application["SUPERSPONSORHEAD" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])] != null)
                {
                    headerText += " - " + HttpContext.Current.Application["SUPERSPONSORHEAD" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "LANG" + Convert.ToInt32(HttpContext.Current.Session["LID"])];
                }
                if (Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSOR" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])]) > 0)
                {
                    logos += "<img src=\"img/partner/" + Convert.ToInt32(HttpContext.Current.Application["SUPERSPONSOR" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"])]) + ".gif\"/>";
                }
            }
            if (HttpContext.Current.Session["UserProfileID"] != null)
            {
                UPID = Convert.ToInt32(HttpContext.Current.Session["UserProfileID"]);
            }

            if (UID == 0 || HttpContext.Current.Request.QueryString["ExerciseVariantLangID"] == null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "CLOSE_WINDOW", "<script language=\"JavaScript\">window.close();</script>");
            }
            else
            {
                if (!IsPostBack)
                {
                    Db.exec("INSERT INTO [ExerciseStats] (" +
                        "ExerciseVariantLangID, " +
                        "UserID, " +
                        "UserProfileID" +
                        ") VALUES (" +
                        "" + Convert.ToInt32(HttpContext.Current.Request.QueryString["ExerciseVariantLangID"]) + "," +
                        "" + UID + "," +
                        "" + UPID + "" +
                        ")");
                }

                string redir = "";

                SqlDataReader rs = Db.rs("SELECT " +
                    "el.Exercise, " +
                    "evl.ExerciseFile, " +
                    "et.ExerciseTypeID, " +
                    "evl.ExerciseContent, " +
                    "e.PrintOnBottom, " +
                    "e.ReplacementHead " +
                    "FROM [ExerciseVariantLang] evl " +
                    "INNER JOIN [ExerciseVariant] ev ON evl.ExerciseVariantID = ev.ExerciseVariantID " +
                    "INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID " +
                    "INNER JOIN [ExerciseLang] el ON ev.ExerciseID = el.ExerciseID AND el.Lang = evl.Lang " +
                    "INNER JOIN [Exercise] e ON el.ExerciseID = e.ExerciseID " +
                    "WHERE evl.ExerciseVariantLangID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["ExerciseVariantLangID"]));
                if (rs.Read())
                {
                    if (!rs.IsDBNull(5))
                    {
                        replacementHead = rs.GetString(5);
                    }
                    if ((rs.GetInt32(2) == 1 || rs.GetInt32(2) == 5) && !rs.IsDBNull(1))
                    {
                        redir = rs.GetString(1);
                    }
                    else if (rs.GetInt32(2) == 1)
                    {
                        exercise.Controls.Add(new LiteralControl("<h1>" + rs.GetString(0) + "</h1>" + rs.GetString(3)));
                    }
                    else
                    {
                        exercise.Controls.Add(new LiteralControl("<script type=\"text/javascript\">" +
                            "AC_FL_RunContent( 'codebase','https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0','width','550','height','400','src','exercise/" + rs.GetString(1).Replace(".swf", "") + "','quality','high','pluginspage','https://www.macromedia.com/go/getflashplayer','movie','exercise/" + rs.GetString(1).Replace(".swf", "") + "' );" +
                            "</script><noscript><object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"https://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"550\" height=\"400\">" +
                            "<param name=\"movie\" value=\"exercise/" + rs.GetString(1) + "\" />" +
                            "<param name=\"quality\" value=\"high\" />" +
                            "<embed src=\"exercise/" + rs.GetString(1) + "\" quality=\"high\" pluginspage=\"https://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"550\" height=\"400\"></embed>" +
                            "</object></noscript>"));
                    }
                    if (!rs.IsDBNull(4))
                    {
                        exercise.Controls.Add(new LiteralControl("<a href=\"#\" id=\"printBtn2\" onclick=\"window.print();return false;\" class=\"print\">"));

                        switch ((HttpContext.Current.Session["LID"] == null ? 2 : Convert.ToInt32(HttpContext.Current.Session["LID"])))
                        {
                            case 1: exercise.Controls.Add(new LiteralControl("Skriv ut")); break;
                            case 2: exercise.Controls.Add(new LiteralControl("Print")); break;
                        }
                        exercise.Controls.Add(new LiteralControl("</a>"));
                    }
                }
                rs.Close();

                if (redir != "")
                {
                    HttpContext.Current.Response.Redirect("exercise/" + redir, true);
                }
            }
        }
    }
}