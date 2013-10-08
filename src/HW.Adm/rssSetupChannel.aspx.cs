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
    public partial class rssSetupChannel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NewsCategoryID.Items.Add(new ListItem("< NOT SET >", "NULL"));
                SqlDataReader rs = Db.rs("SELECT NewsCategoryID, NewsCategory FROM NewsCategory ORDER BY NewsCategory", "newsSqlConnection");
                while (rs.Read())
                {
                    NewsCategoryID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();
                rs = Db.rs("SELECT sourceID, source FROM NewsSource ORDER BY Source", "newsSqlConnection");
                while (rs.Read())
                {
                    sourceID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();

                if (Request.QueryString["CID"] != null)
                {
                    rs = Db.rs("SELECT sourceID, feed, langID, pause, NewsCategoryID, internal FROM NewsChannel WHERE channelID = " + Convert.ToInt32(Request.QueryString["CID"]), "newsSqlConnection");
                    if (rs.Read())
                    {
                        sourceID.SelectedValue = rs.GetInt32(0).ToString();
                        feed.Text = rs.GetString(1);
                        langID.SelectedValue = rs.GetInt32(2).ToString();
                        Pause.Text = (!rs.IsDBNull(3) ? rs.GetDateTime(3).AddHours(1).ToString("yyyy-MM-dd HH:mm") : "");
                        NewsCategoryID.SelectedValue = (!rs.IsDBNull(4) ? rs.GetInt32(4).ToString() : "NULL");
                        Internal.Text = rs.GetString(5);
                    }
                    rs.Close();
                }
            }

            Save.Click += new EventHandler(Save_Click);
        }

        void Save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["CID"] != null)
            {
                Db.exec("UPDATE NewsChannel SET sourceID = " + sourceID.SelectedValue + ", feed = '" + feed.Text.Replace("'", "''") + "', internal = '" + Internal.Text.Replace("'", "''") + "', langID = " + langID.SelectedValue + ", pause = " + (Pause.Text != "" ? "'" + Convert.ToDateTime(Pause.Text).AddHours(-1).ToString("yyyy-MM-dd HH:mm") + "'" : "NULL") + ", NewsCategoryID = " + NewsCategoryID.SelectedValue + " WHERE channelID = " + Convert.ToInt32(Request.QueryString["CID"]), "newsSqlConnection");
            }
            else
            {
                Db.exec("INSERT INTO NewsChannel (sourceID, internal, feed, langID, pause, NewsCategoryID) VALUES (" + sourceID.SelectedValue + ",'" + Internal.Text.Replace("'", "''") + "','" + feed.Text.Replace("'", "''") + "', " + langID.SelectedValue + ", " + (Pause.Text != "" ? "'" + Convert.ToDateTime(Pause.Text).AddHours(-1).ToString("yyyy-MM-dd HH:mm") + "'" : "NULL") + ", " + NewsCategoryID.SelectedValue + ")", "newsSqlConnection");
            }

            Response.Redirect("rss.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
        }
    }
}