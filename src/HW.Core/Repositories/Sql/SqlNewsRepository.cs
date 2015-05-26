
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlNewsRepository : BaseSqlRepository<News>, INewsRepository
	{
		public SqlNewsRepository()
		{
		}
		
		public IList<NewsCategory> FindCategories()
		{
			string query = string.Format(
				@"
SELECT nc.NewsCategoryID,
	nc.NewsCategory,
	(SELECT COUNT(*) FROM News x WHERE x.NewsCategoryID = nc.NewsCategoryID AND DATEADD(m,1,x.DT) > GETDATE()),
	(SELECT COUNT(*) FROM News x WHERE x.NewsCategoryID = nc.NewsCategoryID),
	OnlyDirectFromFeed
FROM NewsCategory nc ORDER BY nc.NewsCategory"
			);
			var categories = new List<NewsCategory>();
			using (SqlDataReader rs = Db.rs(query, "newsSqlConnection")) {
				while (rs.Read()) {
					var c = new NewsCategory {
						Id = GetInt32(rs, 0),
						Name = GetString(rs, 1),
						SomeNews = new List<News>(GetInt32(rs, 2)),
						News = new List<News>(GetInt32(rs, 3)),
						OnlyDirectFromFeed = GetInt32(rs, 4)
					};
					categories.Add(c);
				}
			}
			return categories;
		}
		
		public IList<NewsSource> FindSources()
		{
			string query = string.Format(
				@"
SELECT ns.sourceID,
	ns.sourceShort,
	ns.Favourite
FROM NewsSource ns ORDER BY ns.sourceShort"
			);
			var sources = new List<NewsSource>();
			using (SqlDataReader rs = Db.rs(query, "newsSqlConnection")) {
				while (rs.Read()) {
					var s = new NewsSource {
						Id = GetInt32(rs, 0),
						SourceShort = GetString(rs, 1),
						Favourite = GetInt32(rs, 2)
					};
					sources.Add(s);
				}
			}
			return sources;
		}
		
		public IList<NewsChannel> FindChannels()
		{
			string query = string.Format(
				@"
SELECT nc.channelID,
    ns.sourceShort,
    nc.internal,
    nc.LangID,
    (SELECT COUNT(*) FROM NewsRSS r WHERE r.channelID = nc.channelID AND r.Deleted = 0),
    (SELECT COUNT(*) FROM NewsRSS r2 WHERE r2.channelID = nc.channelID AND r2.Deleted = 1),
    nc.pause
FROM NewsChannel nc
INNER JOIN NewsSource ns ON nc.sourceID = ns.sourceID
ORDER BY ns.sourceShort, nc.internal"
			);
			var channels = new List<NewsChannel>();
			using (SqlDataReader rs = Db.rs(query, "newsSqlConnection")) {
				while (rs.Read()) {
					var c = new NewsChannel {
						Id = GetInt32(rs, 0),
						Source = new NewsSource {
							SourceShort = GetString(rs, 1),
						},
						Internal = GetString(rs, 2),
						Language = new Language {
							Id = GetInt32(rs, 3),
						},
						UndeletedRSS = new List<NewsRSS>(GetInt32(rs, 4)),
						DeletedRSS = new List<NewsRSS>(GetInt32(rs, 5)),
						Pause = GetDateTime(rs, 6)
					};
					channels.Add(c);
				}
			}
			return channels;
		}
		
		public IList<AdminNews> FindTop3AdminNews()
		{
			string query = string.Format(
				@"
SELECT TOP 3 n.AdminNewsID,
	n.DT,
	n.News
FROM AdminNews n
ORDER BY n.DT DESC"
			);
			var news = new List<AdminNews>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var c = new AdminNews {
						Id = GetInt32(rs, 0),
						Date = GetDateTime(rs, 1),
						News = GetString(rs, 2)
					};
					news.Add(c);
				}
			}
			return news;
		}
	}
}
