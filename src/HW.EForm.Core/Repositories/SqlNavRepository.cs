using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlNavRepository : BaseSqlRepository<Nav>
	{
		public SqlNavRepository()
		{
		}
		
		public override void Save(Nav nav)
		{
			string query = @"
INSERT INTO Nav(
	NavID, 
	NavURL, 
	NavText, 
	SortOrder
)
VALUES(
	@NavID, 
	@NavURL, 
	@NavText, 
	@SortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@NavID", nav.NavID),
				new SqlParameter("@NavURL", nav.NavURL),
				new SqlParameter("@NavText", nav.NavText),
				new SqlParameter("@SortOrder", nav.SortOrder)
			);
		}
		
		public override void Update(Nav nav, int id)
		{
			string query = @"
UPDATE Nav SET
	NavID = @NavID,
	NavURL = @NavURL,
	NavText = @NavText,
	SortOrder = @SortOrder
WHERE NavID = @NavID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@NavID", nav.NavID),
				new SqlParameter("@NavURL", nav.NavURL),
				new SqlParameter("@NavText", nav.NavText),
				new SqlParameter("@SortOrder", nav.SortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Nav
WHERE NavID = @NavID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@NavID", id)
			);
		}
		
		public override Nav Read(int id)
		{
			string query = @"
SELECT 	NavID, 
	NavURL, 
	NavText, 
	SortOrder
FROM Nav
WHERE NavID = @NavID";
			Nav nav = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@NavID", id))) {
				if (rs.Read()) {
					nav = new Nav {
						NavID = GetInt32(rs, 0),
						NavURL = GetString(rs, 1),
						NavText = GetString(rs, 2),
						SortOrder = GetInt32(rs, 3)
					};
				}
			}
			return nav;
		}
		
		public override IList<Nav> FindAll()
		{
			string query = @"
SELECT 	NavID, 
	NavURL, 
	NavText, 
	SortOrder
FROM Nav";
			var navs = new List<Nav>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					navs.Add(new Nav {
						NavID = GetInt32(rs, 0),
						NavURL = GetString(rs, 1),
						NavText = GetString(rs, 2),
						SortOrder = GetInt32(rs, 3)
					});
				}
			}
			return navs;
		}
	}
}
