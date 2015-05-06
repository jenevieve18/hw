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
    public partial class news : System.Web.UI.Page
    {
        private string teaser = "", title = "";

        public string getTeaser()
        {
            return teaser;
        }
        public string getTitle()
        {
            return title;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Db.checkAndLogin();

            ClientScript.RegisterClientScriptBlock(this.GetType(),"UPDATE_FORM_ACTION", "<script language=\"javascript\">document.forms[0].action = '/' + document.forms[0].action.substr(document.forms[0].action.lastIndexOf('/')+1);</script>");

            System.Text.StringBuilder buf = new System.Text.StringBuilder();

            bool bodySameAsTeaser = false; 
            string url = "";

            SqlDataReader rs = Db.rs("SELECT " +
                "n.NewsID, " +
                "n.DT, " +
                "cl.NewsCategory, " +
                "n.Headline, " +
                "n.Body, " +
                "ISNULL(n.ImageID,0), " +               // 5
                "t.Filename + '.' + t.Ext AS ifn, " +
                "n.LinkText, " +
                "n.Link, " +
                "n.LinkLangID, " +
                "c.NewsCategoryShort, " +               // 10
                "n.Teaser, " +
                "n.DirectFromFeed " +
                "FROM News n " +
                "LEFT OUTER JOIN NewsCategory c ON n.NewsCategoryID = c.NewsCategoryID " +
                "LEFT OUTER JOIN NewsCategoryLang cl ON c.NewsCategoryID = cl.NewsCategoryID AND cl.LangID = " + Convert.ToInt32(HttpContext.Current.Session["LID"]) + " " +
                "LEFT OUTER JOIN NewsImage t ON n.ImageID = t.NewsImageID " +
                "WHERE n.Deleted IS NULL AND n.Published IS NOT NULL AND n.NewsID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["NID"]), "newsSqlConnection");
            if (rs.Read())
            {
                Db.exec("INSERT INTO NewsRead (NewsID, SessionID) VALUES (" + rs.GetInt32(0) + "," + Convert.ToInt32(HttpContext.Current.Session["SessionID"]) + ")", "newsSqlConnection");

   				buf.Append("<h1>" + rs.GetString(3) + "</h1>");
                buf.Append("<div class=\"taxonomy\">");
                if(!rs.IsDBNull(2))
                {
                    buf.Append(rs.GetString(2).ToUpper() + " / ");
                }
                switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                {
                    case 1: { buf.Append(rs.GetDateTime(1).ToString("yyyy-MM-dd")); break; }
                    case 2: { buf.Append(rs.GetDateTime(1).ToString("MMM dd, yyyy")); break; }
                }
                buf.Append("</div>");
                buf.Append("<div class=\"content\">");

                if (rs.GetInt32(5) != 0)
				{
                    buf.Append("<div class=\"imgcontainer\"><img src=\"/img/news/" + rs.GetInt32(5) + "/" + rs.GetString(6) + "\" alt=\"" + HttpUtility.HtmlEncode(rs.GetString(3)) + "\" /></div>");
				}

                if(!rs.IsDBNull(2))
                {
                    buf.Append("<div class=\"category " + rs.GetString(10) + "\">&nbsp;</div>");
                }

                buf.Append("<p>" + rs.GetString(4).Replace("\r\n", "&nbsp;</p><p>") + "</p>");

                //buf.Append("<a href=\"/" + (HttpContext.Current.Session["CategorySupplied"] != null && Convert.ToBoolean(HttpContext.Current.Session["CategorySupplied"]) && HttpContext.Current.Request.QueryString["NCID"] != null && !rs.IsDBNull(10) ? "news/" + rs.GetString(10) : "") + "\" class=\"more external\"> ");
                //switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                //{
                //    case 1: { buf.Append("Tillbaka"); break; }
                //    case 2: { buf.Append("Back"); break; }
                //}
                //buf.Append("</a>");

                if(!rs.IsDBNull(8))
                {
                    //buf.Append("&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;");
                    buf.Append("<a href=\"" + rs.GetString(8) + "\" class=\"more external\"> ");
                    switch (Convert.ToInt32(HttpContext.Current.Session["LID"]))
                    {
                        //case 1: { buf.Append(rs.GetString(7)); break; }
                        case 1: { buf.Append("Till artikeln"); break; }
                        case 2: { buf.Append("To the article"); break; }
                    } 
                    buf.Append("</a>");
                }

                title = rs.GetString(3).Replace("\r","").Replace("\n","").Replace("\"","");
                teaser = rs.GetString(11).Replace("\r", "").Replace("\n", "").Replace("\"", "");

                bodySameAsTeaser = (!rs.IsDBNull(12) || rs.GetString(11) == rs.GetString(4));
                url = (!rs.IsDBNull(8) ? rs.GetString(8) : "");
            }
            rs.Close();

            buf.Append("</div>	<!-- end .content-->");

            if (bodySameAsTeaser && url != "")
            {
                HttpContext.Current.Response.Redirect(url, true);
            }

            article.Text = buf.ToString();
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