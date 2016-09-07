using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserProfileBQRepository : BaseSqlRepository<UserProfileBQ>
	{
		public SqlUserProfileBQRepository()
		{
		}
		
		public override void Save(UserProfileBQ userProfileBQ)
		{
			string query = @"
INSERT INTO UserProfileBQ(
	UserBQID, 
	UserProfileID, 
	BQID, 
	ValueInt, 
	ValueText, 
	ValueDate
)
VALUES(
	@UserBQID, 
	@UserProfileID, 
	@BQID, 
	@ValueInt, 
	@ValueText, 
	@ValueDate
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserBQID", userProfileBQ.UserBQID),
				new SqlParameter("@UserProfileID", userProfileBQ.UserProfileID),
				new SqlParameter("@BQID", userProfileBQ.BQID),
				new SqlParameter("@ValueInt", userProfileBQ.ValueInt),
				new SqlParameter("@ValueText", userProfileBQ.ValueText),
				new SqlParameter("@ValueDate", userProfileBQ.ValueDate)
			);
		}
		
		public override void Update(UserProfileBQ userProfileBQ, int id)
		{
			string query = @"
UPDATE UserProfileBQ SET
	UserBQID = @UserBQID,
	UserProfileID = @UserProfileID,
	BQID = @BQID,
	ValueInt = @ValueInt,
	ValueText = @ValueText,
	ValueDate = @ValueDate
WHERE UserProfileBQID = @UserProfileBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserBQID", userProfileBQ.UserBQID),
				new SqlParameter("@UserProfileID", userProfileBQ.UserProfileID),
				new SqlParameter("@BQID", userProfileBQ.BQID),
				new SqlParameter("@ValueInt", userProfileBQ.ValueInt),
				new SqlParameter("@ValueText", userProfileBQ.ValueText),
				new SqlParameter("@ValueDate", userProfileBQ.ValueDate)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserProfileBQ
WHERE UserProfileBQID = @UserProfileBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProfileBQID", id)
			);
		}
		
		public override UserProfileBQ Read(int id)
		{
			string query = @"
SELECT 	UserBQID, 
	UserProfileID, 
	BQID, 
	ValueInt, 
	ValueText, 
	ValueDate
FROM UserProfileBQ
WHERE UserProfileBQID = @UserProfileBQID";
			UserProfileBQ userProfileBQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserProfileBQID", id))) {
				if (rs.Read()) {
					userProfileBQ = new UserProfileBQ {
						UserBQID = GetInt32(rs, 0),
						UserProfileID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						ValueInt = GetInt32(rs, 3),
						ValueText = GetString(rs, 4),
						ValueDate = GetDateTime(rs, 5)
					};
				}
			}
			return userProfileBQ;
		}
		
		public override IList<UserProfileBQ> FindAll()
		{
			string query = @"
SELECT 	UserBQID, 
	UserProfileID, 
	BQID, 
	ValueInt, 
	ValueText, 
	ValueDate
FROM UserProfileBQ";
			var userProfileBQs = new List<UserProfileBQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userProfileBQs.Add(new UserProfileBQ {
						UserBQID = GetInt32(rs, 0),
						UserProfileID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						ValueInt = GetInt32(rs, 3),
						ValueText = GetString(rs, 4),
						ValueDate = GetDateTime(rs, 5)
					});
				}
			}
			return userProfileBQs;
		}
	}
}
