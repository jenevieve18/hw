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

public partial class rss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataReader rs = Db.rs("SELECT " +
            "nc.channelID, " +
            "ns.sourceShort, " +
            "nc.internal, " +
            "nc.LangID, " +
            "(SELECT COUNT(*) FROM NewsRSS r WHERE r.channelID = nc.channelID AND r.Deleted = 0), " +
            "(SELECT COUNT(*) FROM NewsRSS r2 WHERE r2.channelID = nc.channelID AND r2.Deleted = 1), " +
            "nc.pause " +
            "FROM NewsChannel nc INNER JOIN NewsSource ns ON nc.sourceID = ns.sourceID ORDER BY ns.sourceShort, nc.internal", "newsSqlConnection");
        while (rs.Read())
        {
            Channel.Text += "<img src=\"img/LangID_" + rs.GetInt32(3) + ".gif\" border=\"0\"> <A HREF=\"rssSetupChannel.aspx?CID=" + rs.GetInt32(0) + "\" TITLE=\"" + rs.GetString(1) + "\">" + rs.GetString(2) + "</A> (" + rs.GetInt32(4) + "/" + rs.GetInt32(5) + ")" + (!rs.IsDBNull(6) && rs.GetDateTime(6).AddHours(1) > DateTime.Now ? " <B>PAUSE</B>" : "") + "<br/>";
        }
        rs.Close();

        rs = Db.rs("SELECT ns.sourceID, ns.sourceShort, ns.Favourite FROM NewsSource ns ORDER BY ns.sourceShort", "newsSqlConnection");
        while (rs.Read())
        {
            Source.Text += "<A HREF=\"rssSetupSource.aspx?SID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>" + (!rs.IsDBNull(2) && rs.GetInt32(2) == 1 ? " <B>FAV</B>" : "") + "<br/>";
        }
        rs.Close();

        rs = Db.rs("SELECT " +
            "nc.NewsCategoryID, " +
            "nc.NewsCategory, " +
            "(SELECT COUNT(*) FROM News x WHERE x.NewsCategoryID = nc.NewsCategoryID AND DATEADD(m,1,x.DT) > GETDATE()), " +
            "(SELECT COUNT(*) FROM News x WHERE x.NewsCategoryID = nc.NewsCategoryID), " +
            "OnlyDirectFromFeed " +
            "FROM NewsCategory nc ORDER BY nc.NewsCategory", "newsSqlConnection");
        while (rs.Read())
        {
            NewsCategory.Text += "<A " + (rs.GetInt32(2) > 0 ? "STYLE=\"font-weight:bold;\" " : "") + "HREF=\"rssSetupNewsCategory.aspx?SID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A> (" + rs.GetInt32(2) + "/" + rs.GetInt32(3) + ")" + (!rs.IsDBNull(4) && rs.GetInt32(4) == 1 ? " <B>DIR</B>" : "") + "<br/>";
        }
        rs.Close();
    }
}
