using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlAdminNewRepository : BaseSqlRepository<AdminNew>
	{
		public SqlAdminNewRepository()
		{
		}
		
		public override void Save(AdminNew adminNew)
		{
			string query = @"
INSERT INTO AdminNews(
	AdminNewsID, 
	DT, 
	News
)
VALUES(
	@AdminNewsID, 
	@DT, 
	@News
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AdminNewsID", adminNew.AdminNewsID),
				new SqlParameter("@DT", adminNew.DT),
				new SqlParameter("@News", adminNew.News)
			);
		}
		
		public override void Update(AdminNew adminNew, int id)
		{
			string query = @"
UPDATE AdminNews SET
	AdminNewsID = @AdminNewsID,
	DT = @DT,
	News = @News
WHERE AdminNewID = @AdminNewID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AdminNewsID", adminNew.AdminNewsID),
				new SqlParameter("@DT", adminNew.DT),
				new SqlParameter("@News", adminNew.News)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM AdminNews
WHERE AdminNewID = @AdminNewID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AdminNewID", id)
			);
		}
		
		public override AdminNew Read(int id)
		{
			string query = @"
SELECT 	AdminNewsID, 
	DT, 
	News
FROM AdminNews
WHERE AdminNewID = @AdminNewID";
			AdminNew adminNew = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@AdminNewID", id))) {
				if (rs.Read()) {
					adminNew = new AdminNew {
						AdminNewsID = GetInt32(rs, 0),
						DT = GetString(rs, 1),
						News = GetString(rs, 2)
					};
				}
			}
			return adminNew;
		}
		
		public override IList<AdminNew> FindAll()
		{
			string query = @"
SELECT 	AdminNewsID, 
	DT, 
	News
FROM AdminNews";
			var adminNews = new List<AdminNew>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					adminNews.Add(new AdminNew {
						AdminNewsID = GetInt32(rs, 0),
						DT = GetString(rs, 1),
						News = GetString(rs, 2)
					});
				}
			}
			return adminNews;
		}
	}
}
