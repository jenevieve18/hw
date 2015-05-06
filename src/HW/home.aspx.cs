using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace healthWatch
{
	/// <summary>
	/// Summary description for home.
	/// </summary>
	public partial class home : System.Web.UI.Page
    {
        private string title = "", desc = "Verktyg för självhjälp samt nyheter inom hälsa, stress, sömn och diabetes. Self-help tools and news within health, stress, sleep and diabetes.";
        public bool categorySupplied = false;

        public string getTitle()
        {
            return title;
        }

        public string getDesc()
        {
            return desc;
        }
        public string splash()
        {
            return "<a href=\"/register.aspx\"><img src=\"/images/stock" + Convert.ToInt32(HttpContext.Current.Session["LID"]) + ".jpg\" width=\"228\" height=\"300\" alt=\"Stock\"></a>";
        }
        
        public string rightNow()
        {
            return Db.rightNow(0, Convert.ToInt32(HttpContext.Current.Session["LID"]));
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (HttpContext.Current.Request.QueryString["LID"] != null && HttpContext.Current.Session["ForceLID"] == null)
            {
                int LID = Convert.ToInt32("0" + HttpContext.Current.Request.QueryString["LID"]);
                if (Convert.ToInt32(HttpContext.Current.Session["LID"]) != LID && (LID == 1 || LID == 2))
                {
                    if (HttpContext.Current.Session["UserID"] != null && Convert.ToInt32(HttpContext.Current.Session["UserID"]) != 0)
                    {
                        Db.exec("UPDATE [User] SET LID = " + LID + " WHERE UserID = " + Convert.ToInt32(HttpContext.Current.Session["UserID"]));
                    }
                    HttpContext.Current.Session["LID"] = LID;
                    HttpContext.Current.Response.Cookies.Remove("HW");
                    HttpContext.Current.Response.Cookies["HW"]["LID"] = LID.ToString();
                    HttpContext.Current.Response.Cookies["HW"].Expires = DateTime.Now.AddYears(1);
                    if (HttpContext.Current.Request.QueryString["Goto"] != null && HttpContext.Current.Request.QueryString["Goto"].ToString() != "")
                    {
                        string redir = HttpContext.Current.Request.QueryString["Goto"].ToString();
                        string rawUrl = HttpContext.Current.Request.RawUrl.ToLower();
                        rawUrl = rawUrl.Substring(rawUrl.LastIndexOf("/") + 1);
                        if (rawUrl.IndexOf("aspx") >= 0)
                        {
                            rawUrl = rawUrl.Substring(0, rawUrl.IndexOf("aspx") + 4);
                        }
                        if (redir != rawUrl)
                        {
                            HttpContext.Current.Response.Redirect(redir + (redir.IndexOf("aspx") >= 0 ? "?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() : ""), true);
                        }
                    }
                }
            }

			Db.checkAndLogin();

            ClientScript.RegisterClientScriptBlock(this.GetType(), "UPDATE_FORM_ACTION", "<script language=\"javascript\">document.forms[0].action = '/' + document.forms[0].action.substr(document.forms[0].action.lastIndexOf('/')+1);</script>");
		}

        int cxL = 0;
        int cxR = 0;
        int cx = 0;
        
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            left.Text = "";
            right.Text = "";

            categorySupplied = (HttpContext.Current.Request.QueryString["NCID"] != null);
            HttpContext.Current.Session["CategorySupplied"] = categorySupplied;
            bool onlyDirectFromFeed = false;

            SqlDataReader rs;

            if (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1)
            {
                onlyDirectFromFeed = true;
            }
            else if (categorySupplied)
            {
                rs = Db.rs("SELECT OnlyDirectFromFeed " +
                    "FROM NewsCategory " +
                    "WHERE NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]), "newsSqlConnection");
                if (rs.Read())
                {
                    onlyDirectFromFeed = !rs.IsDBNull(0);
                }
                rs.Close();
            }

            rs = Db.rs("SELECT " +
                 "n.NewsID, " +                          // 0
                 "n.DT, " +                              // 1
                 "cl.NewsCategory, " +                   // 2
                 "n.Headline, " +                        // 3
                 "n.Teaser, " +                          // 4
                 "n.TeaserImageID, " +                   // 5
                 "t.Filename + '.' + t.Ext AS ifn, " +   // 6
                 "n.DirectFromFeed, " +                  // 7
                 "c.OnlyDirectFromFeed, " +              // 8
                 "n.HeadlineShort, " +                   // 9
                 "c.NewsCategoryShort, " +               // 10
                 "n.Body, " +
                 "n.DirectFromFeed, " +
                 "n.Link " +
                 "FROM News n " +
                 "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                 "LEFT OUTER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                 "LEFT OUTER JOIN NewsImage t ON n.TeaserImageID = t.NewsImageID " +
                 "WHERE n.Published IS NOT NULL " +
                 "AND n.Deleted IS NULL " +
                 (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND n.LinkLangID = 1 " : "") +
                 "AND n.DT >= '" + DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("'", "") + "' " +
                 "" + (categorySupplied ? "AND n.NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) + " " : "AND n.OnlyInCategory IS NULL ") + "" +
                 "ORDER BY n.DT DESC", "newsSqlConnection");
            while (rs.Read())
            {
                if (cx == 0 && categorySupplied)
                {
                    title = rs.GetString(2);
                    desc = rs.GetString(2);
                }
                cx++;
                if ((!onlyDirectFromFeed && rs.IsDBNull(7)) || (onlyDirectFromFeed && cx % 2 == 1))
                {
                    cxL++;
                }
                else
                {
                    cxR++;
                }

                renderTeaser((!onlyDirectFromFeed && rs.IsDBNull(7)) || (onlyDirectFromFeed && cx % 2 == 1), rs.GetInt32(0), rs.GetDateTime(1), (rs.IsDBNull(2) ? "" : rs.GetString(2)), rs.GetString(3), rs.GetString(4), (rs.IsDBNull(5) ? 0 : rs.GetInt32(5)), (rs.IsDBNull(6) ? "" : rs.GetString(6)), onlyDirectFromFeed, categorySupplied, rs.GetString(9), (!rs.IsDBNull(10) ? rs.GetString(10) : ""), (!rs.IsDBNull(13) && rs.GetString(13) != "") && (rs.GetString(4) == rs.GetString(11) || !rs.IsDBNull(12)));
            }
            rs.Close();

            if (onlyDirectFromFeed)
            {
                if (cx < 50)
                {
                    rs = Db.rs("SELECT TOP " + (50 - cx) + " " +
                        "n.NewsID, " +                          // 0
                        "n.DT, " +                              // 1
                        "cl.NewsCategory, " +                   // 2
                        "n.Headline, " +                        // 3
                        "n.Teaser, " +                          // 4
                        "n.TeaserImageID, " +                   // 5
                        "t.Filename + '.' + t.Ext AS ifn, " +   // 6
                        "n.DirectFromFeed, " +                  // 7
                        "c.OnlyDirectFromFeed, " +              // 8
                        "n.HeadlineShort, " +                   // 9
                        "c.NewsCategoryShort, " +               // 10
                        "n.Body, " +
                        "n.DirectFromFeed, " +
                        "n.Link " +
                        "FROM News n " +
                        "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                        "LEFT OUTER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "LEFT OUTER JOIN NewsImage t ON n.TeaserImageID = t.NewsImageID " +
                        "WHERE n.Published IS NOT NULL " +
                        "AND n.Deleted IS NULL " +
                        (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND n.LinkLangID = 1 " : "") +
                        "AND n.DT < '" + DateTime.Now.Date.ToString("yyyy-MM-dd").Replace("'", "") + "' " +
                        "" + (categorySupplied ? "AND n.NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) + " " : "AND n.OnlyInCategory IS NULL ") + "" +
                        "ORDER BY n.DT DESC", "newsSqlConnection");
                    while (rs.Read())
                    {
                        if (cx == 0 && categorySupplied)
                        {
                            title = rs.GetString(2);
                            desc = rs.GetString(2);
                        }
                        cx++;
                        if ((!onlyDirectFromFeed && rs.IsDBNull(7)) || (onlyDirectFromFeed && cx % 2 == 1))
                        {
                            cxL++;
                        }
                        else
                        {
                            cxR++;
                        }

                        renderTeaser((!onlyDirectFromFeed && rs.IsDBNull(7)) || (onlyDirectFromFeed && cx % 2 == 1), rs.GetInt32(0), rs.GetDateTime(1), (rs.IsDBNull(2) ? "" : rs.GetString(2)), rs.GetString(3), rs.GetString(4), (rs.IsDBNull(5) ? 0 : rs.GetInt32(5)), (rs.IsDBNull(6) ? "" : rs.GetString(6)), onlyDirectFromFeed, categorySupplied, rs.GetString(9), (!rs.IsDBNull(10) ? rs.GetString(10) : ""), (!rs.IsDBNull(13) && rs.GetString(13) != "") && (rs.GetString(4) == rs.GetString(11) || !rs.IsDBNull(12)));
                    }
                    rs.Close();
                }
            }
            else
            {
                if (cxL < 25)
                {
                    rs = Db.rs("SELECT TOP " + (25 - cxL) + " " +
                        "n.NewsID, " +
                        "n.DT, " +
                        "cl.NewsCategory, " +
                        "n.Headline, " +
                        "n.Teaser, " +
                        "n.TeaserImageID, " +
                        "t.Filename + '.' + t.Ext AS ifn, " +   // 6
                        "n.DirectFromFeed, " +                  // 7
                        "c.OnlyDirectFromFeed, " +              // 8
                        "n.HeadlineShort, " +                   // 9
                        "c.NewsCategoryShort, " +               // 10
                        "n.Body, " +
                        "n.DirectFromFeed, " +
                        "n.Link " +
                        "FROM News n " +
                        "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                        "LEFT OUTER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "LEFT OUTER JOIN NewsImage t ON n.TeaserImageID = t.NewsImageID " +
                        "WHERE n.Deleted IS NULL " +
                        "AND n.DirectFromFeed IS NULL " +
                        "AND n.Published IS NOT NULL " +
                        (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND n.LinkLangID = 1 " : "") +
                        "AND n.DT < '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' " +
                        "" + (categorySupplied ? "AND n.NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) + " " : "AND n.OnlyInCategory IS NULL ") + "" +
                        "ORDER BY n.DT DESC", "newsSqlConnection");
                    while (rs.Read())
                    {
                        if (cx == 0 && categorySupplied)
                        {
                            title = rs.GetString(2);
                            desc = rs.GetString(2);
                        }
                        cx++;
                        cxL++;
                        renderTeaser(true, rs.GetInt32(0), rs.GetDateTime(1), (rs.IsDBNull(2) ? "" : rs.GetString(2)), rs.GetString(3), rs.GetString(4), (rs.IsDBNull(5) ? 0 : rs.GetInt32(5)), (rs.IsDBNull(6) ? "" : rs.GetString(6)), onlyDirectFromFeed, categorySupplied, rs.GetString(9), (!rs.IsDBNull(10) ? rs.GetString(10) : ""), (!rs.IsDBNull(13) && rs.GetString(13) != "") && (rs.GetString(4) == rs.GetString(11) || !rs.IsDBNull(12)));
                    }
                    rs.Close();
                }

                if (cxR < 25)
                {
                    rs = Db.rs("SELECT TOP " + (25 - cxR) + " " +
                        "n.NewsID, " +                          // 0
                        "n.DT, " +                              // 1
                        "cl.NewsCategory, " +                   // 2
                        "n.Headline, " +                        // 3
                        "n.Teaser, " +                          // 4
                        "n.TeaserImageID, " +                   // 5
                        "t.Filename + '.' + t.Ext AS ifn, " +   // 6
                        "n.DirectFromFeed, " +                  // 7
                        "c.OnlyDirectFromFeed, " +              // 8
                        "n.HeadlineShort, " +                   // 9
                        "c.NewsCategoryShort, " +               // 10
                        "n.Body, " +
                        "n.DirectFromFeed, " +
                        "n.Link " +
                        "FROM News n " +
                        "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                        "LEFT OUTER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                        "LEFT OUTER JOIN NewsImage t ON n.TeaserImageID = t.NewsImageID " +
                        "WHERE n.Deleted IS NULL " +
                        "AND n.DirectFromFeed IS NOT NULL " +
                        "AND n.Published IS NOT NULL " +
                        (Convert.ToInt32(HttpContext.Current.Session["LID"]) != 1 ? "AND n.LinkLangID = 1 " : "") +
                        "AND n.DT < '" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "' " +
                        "" + (categorySupplied ? "AND n.NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NCID"]) + " " : "AND n.OnlyInCategory IS NULL ") + "" +
                        "ORDER BY n.DT DESC", "newsSqlConnection");
                    while (rs.Read())
                    {
                        if (cx == 0 && categorySupplied)
                        {
                            title = rs.GetString(2);
                            desc = rs.GetString(2);
                        }
                        cx++;
                        cxR++;
                        renderTeaser(false, rs.GetInt32(0), rs.GetDateTime(1), (rs.IsDBNull(2) ? "" : rs.GetString(2)), rs.GetString(3), rs.GetString(4), (rs.IsDBNull(5) ? 0 : rs.GetInt32(5)), (rs.IsDBNull(6) ? "" : rs.GetString(6)), onlyDirectFromFeed, categorySupplied, rs.GetString(9), (!rs.IsDBNull(10) ? rs.GetString(10) : ""), (!rs.IsDBNull(13) && rs.GetString(13) != "") && (rs.GetString(4) == rs.GetString(11) || !rs.IsDBNull(12)));
                    }
                    rs.Close();
                }
            }
        }
        private void renderTeaser(bool toleft, int newsID, DateTime dt, string newsCategory, string headLine, string teaser, int teaserImageID, string teaserImageFile, bool allDirectFromFeed, bool inCategory, string url, string newsCategoryShort, bool directToArticle)
        {
            string buf = "<div class=\"article" + (toleft && cxL == 1 || !toleft && cxR == 1 ? " first" : (toleft && cxL == 25 || !toleft && cxR == 25 ? " last" : "")) + "\">";

            buf += "<h1>" + headLine + "</h1>";
            buf += "<div class=\"taxonomy\">";
            buf += (newsCategory != "" ? newsCategory.ToUpper() + " / " : "");
            //if (toleft && !allDirectFromFeed)
            if (!directToArticle)
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { buf += "AKTUELLT"; break; }
                    case 2: { buf += "NEWS"; break; }
                }
            }
            else
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { buf += "DIREKT FRÅN NÄTET"; break; }
                    case 2: { buf += "FROM THE NET"; break; }
                }
            } 
            buf += " / <span class=\"date\">";
			switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
            {
                case 1: { buf += dt.ToString("yyyy-MM-dd"); break; }
                case 2: { buf += dt.ToString("MMM dd, yyyy"); break; }
            }
            buf += "</span></div><div class=\"content\">";
            if (teaserImageID != 0)
            {
                buf += "<img src=\"/img/news/" + teaserImageID + "/" + teaserImageFile + "\" alt=\"" + HttpUtility.HtmlEncode(headLine) + "\" />";
            }
            buf += (newsCategoryShort != "" ? "<div class=\"category " + newsCategoryShort + "\">&nbsp;</div>" : "");
            buf += "<p>" + teaser.Replace("\r\n", "<br/>") + "</p>";
            buf += "<a title=\"" + headLine + "\" class=\"more" + (directToArticle ? " external" : "") + "\" href=\"/news/" + (newsCategoryShort != "" ? newsCategoryShort + "/" : "") + url + "\">";
            if (directToArticle)
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { buf += " Till artikeln"; break; }
                    case 2: { buf += " To the article"; break; }
                }
            }
            else
            {
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { buf += " Läs mer"; break; }
                    case 2: { buf += " Read more"; break; }
                }
            }
            buf += "</a></div><!-- end .content-->";

            buf += "</div><!-- end .article" + (toleft && cxL == 1 || !toleft && cxR == 1 ? ".first" : (toleft && cxL == 25 || !toleft && cxR == 25 ? ".last" : "")) + " -->";

            if (!toleft)
            {
                right.Text += buf;
            }
            else
            {
                left.Text += buf;
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
