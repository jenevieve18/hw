using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserTokenRepository : BaseSqlRepository<UserToken>
	{
		public SqlUserTokenRepository()
		{
		}
		
		public override void Save(UserToken userToken)
		{
			string query = @"
INSERT INTO UserToken(
	UserToken, 
	UserID, 
	Expires, 
	SessionID
)
VALUES(
	@UserToken, 
	@UserID, 
	@Expires, 
	@SessionID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserToken", userToken.Token),
				new SqlParameter("@UserID", userToken.UserID),
				new SqlParameter("@Expires", userToken.Expires),
				new SqlParameter("@SessionID", userToken.SessionID)
			);
		}
		
		public override void Update(UserToken userToken, int id)
		{
			string query = @"
UPDATE UserToken SET
	UserToken = @UserToken,
	UserID = @UserID,
	Expires = @Expires,
	SessionID = @SessionID
WHERE UserTokenID = @UserTokenID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserToken", userToken.Token),
				new SqlParameter("@UserID", userToken.UserID),
				new SqlParameter("@Expires", userToken.Expires),
				new SqlParameter("@SessionID", userToken.SessionID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserToken
WHERE UserTokenID = @UserTokenID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserTokenID", id)
			);
		}
		
		public override UserToken Read(int id)
		{
			string query = @"
SELECT 	UserToken, 
	UserID, 
	Expires, 
	SessionID
FROM UserToken
WHERE UserTokenID = @UserTokenID";
			UserToken userToken = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserTokenID", id))) {
				if (rs.Read()) {
					userToken = new UserToken {
						Token = GetGuid(rs, 0),
						UserID = GetInt32(rs, 1),
						Expires = GetDateTime(rs, 2),
						SessionID = GetString(rs, 3)
					};
				}
			}
			return userToken;
		}
		
		public override IList<UserToken> FindAll()
		{
			string query = @"
SELECT 	UserToken, 
	UserID, 
	Expires, 
	SessionID
FROM UserToken";
			var userTokens = new List<UserToken>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userTokens.Add(new UserToken {
						Token = GetGuid(rs, 0),
						UserID = GetInt32(rs, 1),
						Expires = GetDateTime(rs, 2),
						SessionID = GetString(rs, 3)
					});
				}
			}
			return userTokens;
		}
	}
}
