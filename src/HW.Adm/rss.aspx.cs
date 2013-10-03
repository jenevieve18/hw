using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
	public partial class rss : System.Web.UI.Page
	{
		SqlNewsRepository repository = new SqlNewsRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
//			string query = string.Format(
//				@"
//SELECT nc.channelID,
//    ns.sourceShort,
//    nc.internal,
//    nc.LangID,
//    (SELECT COUNT(*) FROM NewsRSS r WHERE r.channelID = nc.channelID AND r.Deleted = 0),
//    (SELECT COUNT(*) FROM NewsRSS r2 WHERE r2.channelID = nc.channelID AND r2.Deleted = 1),
//    nc.pause
//FROM NewsChannel nc INNER JOIN NewsSource ns ON nc.sourceID = ns.sourceID ORDER BY ns.sourceShort, nc.internal"
//			);
//			SqlDataReader rs = Db.rs(query, "newsSqlConnection");
//			while (rs.Read())
			foreach (var c in repository.FindChannels())
			{
//				Channel.Text += "<img src=\"img/LangID_" + rs.GetInt32(3) + ".gif\" border=\"0\"> <A HREF=\"rssSetupChannel.aspx?CID=" + rs.GetInt32(0) + "\" TITLE=\"" + rs.GetString(1) + "\">" + rs.GetString(2) + "</A> (" + rs.GetInt32(4) + "/" + rs.GetInt32(5) + ")" + (!rs.IsDBNull(6) && rs.GetDateTime(6).AddHours(1) > DateTime.Now ? " <B>PAUSE</B>" : "") + "<br/>";
				Channel.Text += "<img src=\"img/LangID_" + c.Language.Id + ".gif\" border=\"0\"> <A HREF=\"rssSetupChannel.aspx?CID=" + c.Id + "\" TITLE=\"" + c.Source.SourceShort + "\">" + c.Internal + "</A> (" + c.UndeletedRSS.Count + "/" + c.DeletedRSS.Count + ")" + (c.Pause != null && c.Pause.Value.AddHours(1) > DateTime.Now ? " <B>PAUSE</B>" : "") + "<br/>";
			}
//			rs.Close();

//			string quer = string.Format(
//				@"
//SELECT ns.sourceID, 
//	ns.sourceShort, 
//	ns.Favourite 
//FROM NewsSource ns ORDER BY ns.sourceShort"
//			);
//			SqlDataReader rs = Db.rs(query, "newsSqlConnection");
//			while (rs.Read())
			foreach (var s in repository.FindSources())
			{
//				Source.Text += "<A HREF=\"rssSetupSource.aspx?SID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>" + (!rs.IsDBNull(2) && rs.GetInt32(2) == 1 ? " <B>FAV</B>" : "") + "<br/>";
				Source.Text += "<A HREF=\"rssSetupSource.aspx?SID=" + s.Id + "\">" + s.SourceShort + "</A>" + (s.Favourite == 1 ? " <B>FAV</B>" : "") + "<br/>";
			}
//			rs.Close();

//			string query = string.Format(
//				@"
//SELECT nc.NewsCategoryID, 
//	nc.NewsCategory, 
//	(SELECT COUNT(*) FROM News x WHERE x.NewsCategoryID = nc.NewsCategoryID AND DATEADD(m,1,x.DT) > GETDATE()), 
//	(SELECT COUNT(*) FROM News x WHERE x.NewsCategoryID = nc.NewsCategoryID), 
//	OnlyDirectFromFeed 
//FROM NewsCategory nc ORDER BY nc.NewsCategory"
//			);
//			SqlDataReader rs = Db.rs(query, "newsSqlConnection");
//			while (rs.Read())
foreach (var c in repository.FindCategories())
			{
//				NewsCategory.Text += "<A " + (rs.GetInt32(2) > 0 ? "STYLE=\"font-weight:bold;\" " : "") + "HREF=\"rssSetupNewsCategory.aspx?SID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A> (" + rs.GetInt32(2) + "/" + rs.GetInt32(3) + ")" + (!rs.IsDBNull(4) && rs.GetInt32(4) == 1 ? " <B>DIR</B>" : "") + "<br/>";
	NewsCategory.Text += "<A " + (c.SomeNews.Count > 0 ? "STYLE=\"font-weight:bold;\" " : "") + "HREF=\"rssSetupNewsCategory.aspx?SID=" + c.Id + "\">" + c.Name + "</A> (" + c.SomeNews.Count + "/" + c.News.Count + ")" + (c.OnlyDirectFromFeed == 1 ? " <B>DIR</B>" : "") + "<br/>";
			}
//			rs.Close();
		}
	}
}