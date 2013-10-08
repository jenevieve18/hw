using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;

namespace HW.Adm
{
    public partial class rssSetupSource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Request.QueryString["SID"] != null)
            {
                SqlDataReader rs = Db.rs("SELECT source, sourceShort, favourite FROM NewsSource WHERE sourceID = " + Convert.ToInt32(Request.QueryString["SID"]), "newsSqlConnection");
                if (rs.Read())
                {
                    source.Text = rs.GetString(0);
                    sourceShort.Text = rs.GetString(1);
                    Favourite.Checked = (!rs.IsDBNull(2) && rs.GetInt32(2) == 1);
                }
                rs.Close();
            }

            Save.Click += new EventHandler(Save_Click);
        }

        void Save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["SID"] != null)
            {
                Db.exec("UPDATE NewsSource SET source = '" + source.Text.Replace("'", "''") + "',sourceShort = '" + sourceShort.Text.Replace("'", "''") + "', favourite = " + (Favourite.Checked ? "1" : "0") + " WHERE sourceID = " + Convert.ToInt32(Request.QueryString["SID"]), "newsSqlConnection");
            }
            else
            {
                Db.exec("INSERT INTO NewsSource (source, sourceShort, favourite) VALUES ('" + source.Text.Replace("'", "''") + "', '" + source.Text.Replace("'", "''") + "'," + (Favourite.Checked ? "1" : "0") + ")", "newsSqlConnection");
            }

            Response.Redirect("rss.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
        }
    }
}