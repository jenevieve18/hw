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

public partial class rssSetupNewsCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack && HttpContext.Current.Request.QueryString["SID"] != null)
        {
            SqlDataReader rs = Db.rs("SELECT NewsCategory, NewsCategoryShort, OnlyDirectFromFeed FROM NewsCategory WHERE NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]), "newsSqlConnection");
            if (rs.Read())
            {
                NewsCategory.Text = rs.GetString(0);
                NewsCategoryShort.Text = rs.GetString(1);
                OnlyDirectFromFeed.Checked = (!rs.IsDBNull(2) && rs.GetInt32(2) == 1);
            }
            rs.Close();
        }

        Save.Click += new EventHandler(Save_Click);
    }

    void Save_Click(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.QueryString["SID"] != null)
        {
            Db.exec("UPDATE NewsCategory SET NewsCategory = '" + NewsCategory.Text.Replace("'", "''") + "', NewsCategoryShort = '" + NewsCategoryShort.Text.Replace("'", "''") + "', OnlyDirectFromFeed = " + (OnlyDirectFromFeed.Checked ? "1" : "0") + " WHERE NewsCategoryID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["SID"]), "newsSqlConnection");
        }
        else
        {
            Db.exec("INSERT INTO NewsCategory (NewsCategory, NewsCategoryShort, OnlyDirectFromFeed) VALUES ('" + NewsCategory.Text.Replace("'", "''") + "', '" + NewsCategoryShort.Text.Replace("'", "''") + "'," + (OnlyDirectFromFeed.Checked ? "1" : "0") + ")", "newsSqlConnection");
        }

        HttpContext.Current.Response.Redirect("rss.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
    }


}
