using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserSessionRepository : BaseSqlRepository<UserSession>
	{
		public SqlUserSessionRepository()
		{
		}
		
		public override void Save(UserSession userSession)
		{
			string query = @"
INSERT INTO UserSession(
	UserSessionID, 
	UserHostAddress, 
	UserAgent, 
	LangID
)
VALUES(
	@UserSessionID, 
	@UserHostAddress, 
	@UserAgent, 
	@LangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSessionID", userSession.UserSessionID),
				new SqlParameter("@UserHostAddress", userSession.UserHostAddress),
				new SqlParameter("@UserAgent", userSession.UserAgent),
				new SqlParameter("@LangID", userSession.LangID)
			);
		}
		
		public override void Update(UserSession userSession, int id)
		{
			string query = @"
UPDATE UserSession SET
	UserSessionID = @UserSessionID,
	UserHostAddress = @UserHostAddress,
	UserAgent = @UserAgent,
	LangID = @LangID
WHERE UserSessionID = @UserSessionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSessionID", userSession.UserSessionID),
				new SqlParameter("@UserHostAddress", userSession.UserHostAddress),
				new SqlParameter("@UserAgent", userSession.UserAgent),
				new SqlParameter("@LangID", userSession.LangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserSession
WHERE UserSessionID = @UserSessionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserSessionID", id)
			);
		}
		
		public override UserSession Read(int id)
		{
			string query = @"
SELECT 	UserSessionID, 
	UserHostAddress, 
	UserAgent, 
	LangID
FROM UserSession
WHERE UserSessionID = @UserSessionID";
			UserSession userSession = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserSessionID", id))) {
				if (rs.Read()) {
					userSession = new UserSession {
						UserSessionID = GetInt32(rs, 0),
						UserHostAddress = GetString(rs, 1),
						UserAgent = GetString(rs, 2),
						LangID = GetInt32(rs, 3)
					};
				}
			}
			return userSession;
		}
		
		public override IList<UserSession> FindAll()
		{
			string query = @"
SELECT 	UserSessionID, 
	UserHostAddress, 
	UserAgent, 
	LangID
FROM UserSession";
			var userSessions = new List<UserSession>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userSessions.Add(new UserSession {
						UserSessionID = GetInt32(rs, 0),
						UserHostAddress = GetString(rs, 1),
						UserAgent = GetString(rs, 2),
						LangID = GetInt32(rs, 3)
					});
				}
			}
			return userSessions;
		}
	}
}
