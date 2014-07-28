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
    public partial class rssImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, System.EventArgs e)
        {
            SqlDataReader rs = Db.rs("SELECT TOP 25 s.source, r.title, r.description, r.link, r.dt FROM NewsRSS r INNER JOIN NewsChannel c ON r.channelID = c.channelID INNER JOIN NewsSource s ON c.sourceID = s.sourceID WHERE r.deleted = 0 ORDER BY r.dt DESC");
            while (rs.Read())
            {
                HttpContext.Current.Response.Write("<span style=\"font-size:1.2em;font-weight:bold;\">" + rs.GetString(1) + "</span><br/>");
                HttpContext.Current.Response.Write(rs.GetString(2) + "<BR/>");
                HttpContext.Current.Response.Write("<A TARGET=\"_blank\" HREF=\"" + rs.GetString(3) + "\">Read more &gt;&gt;</A>");
                HttpContext.Current.Response.Write(" " + rs.GetString(0) + " [" + rs.GetDateTime(4).ToString("yyyy-MM-dd HH:mm") + "]<BR/><BR/>");
            }
            rs.Close();
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