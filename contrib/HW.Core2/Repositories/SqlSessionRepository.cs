using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSessionRepository : BaseSqlRepository<Session>
	{
		public SqlSessionRepository()
		{
		}
		
		public override void Save(Session session)
		{
			string query = @"
INSERT INTO Session(
	SessionID, 
	Referrer, 
	DT, 
	UserAgent, 
	UserID, 
	IP, 
	EndDT, 
	Host, 
	Site, 
	AutoEnded
)
VALUES(
	@SessionID, 
	@Referrer, 
	@DT, 
	@UserAgent, 
	@UserID, 
	@IP, 
	@EndDT, 
	@Host, 
	@Site, 
	@AutoEnded
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SessionID", session.SessionID),
				new SqlParameter("@Referrer", session.Referrer),
				new SqlParameter("@DT", session.DT),
				new SqlParameter("@UserAgent", session.UserAgent),
				new SqlParameter("@UserID", session.UserID),
				new SqlParameter("@IP", session.IP),
				new SqlParameter("@EndDT", session.EndDT),
				new SqlParameter("@Host", session.Host),
				new SqlParameter("@Site", session.Site),
				new SqlParameter("@AutoEnded", session.AutoEnded)
			);
		}
		
		public override void Update(Session session, int id)
		{
			string query = @"
UPDATE Session SET
	SessionID = @SessionID,
	Referrer = @Referrer,
	DT = @DT,
	UserAgent = @UserAgent,
	UserID = @UserID,
	IP = @IP,
	EndDT = @EndDT,
	Host = @Host,
	Site = @Site,
	AutoEnded = @AutoEnded
WHERE SessionID = @SessionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SessionID", session.SessionID),
				new SqlParameter("@Referrer", session.Referrer),
				new SqlParameter("@DT", session.DT),
				new SqlParameter("@UserAgent", session.UserAgent),
				new SqlParameter("@UserID", session.UserID),
				new SqlParameter("@IP", session.IP),
				new SqlParameter("@EndDT", session.EndDT),
				new SqlParameter("@Host", session.Host),
				new SqlParameter("@Site", session.Site),
				new SqlParameter("@AutoEnded", session.AutoEnded)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Session
WHERE SessionID = @SessionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SessionID", id)
			);
		}
		
		public override Session Read(int id)
		{
			string query = @"
SELECT 	SessionID, 
	Referrer, 
	DT, 
	UserAgent, 
	UserID, 
	IP, 
	EndDT, 
	Host, 
	Site, 
	AutoEnded
FROM Session
WHERE SessionID = @SessionID";
			Session session = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SessionID", id))) {
				if (rs.Read()) {
					session = new Session {
						SessionID = GetInt32(rs, 0),
						Referrer = GetString(rs, 1),
						DT = GetString(rs, 2),
						UserAgent = GetString(rs, 3),
						UserID = GetInt32(rs, 4),
						IP = GetString(rs, 5),
						EndDT = GetString(rs, 6),
						Host = GetString(rs, 7),
						Site = GetString(rs, 8),
						AutoEnded = GetBoolean(rs, 9)
					};
				}
			}
			return session;
		}
		
		public override IList<Session> FindAll()
		{
			string query = @"
SELECT 	SessionID, 
	Referrer, 
	DT, 
	UserAgent, 
	UserID, 
	IP, 
	EndDT, 
	Host, 
	Site, 
	AutoEnded
FROM Session";
			var sessions = new List<Session>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sessions.Add(new Session {
						SessionID = GetInt32(rs, 0),
						Referrer = GetString(rs, 1),
						DT = GetString(rs, 2),
						UserAgent = GetString(rs, 3),
						UserID = GetInt32(rs, 4),
						IP = GetString(rs, 5),
						EndDT = GetString(rs, 6),
						Host = GetString(rs, 7),
						Site = GetString(rs, 8),
						AutoEnded = GetBoolean(rs, 9)
					});
				}
			}
			return sessions;
		}
	}
}
