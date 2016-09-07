using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminSessionRepository : BaseSqlRepository<SponsorAdminSession>
	{
		public SqlSponsorAdminSessionRepository()
		{
		}
		
		public override void Save(SponsorAdminSession sponsorAdminSession)
		{
			string query = @"
INSERT INTO SponsorAdminSession(
	SponsorAdminID, 
	DT, 
	SponsorAdminSessionID, 
	EndDT
)
VALUES(
	@SponsorAdminID, 
	@DT, 
	@SponsorAdminSessionID, 
	@EndDT
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", sponsorAdminSession.SponsorAdminID),
				new SqlParameter("@DT", sponsorAdminSession.DT),
				new SqlParameter("@SponsorAdminSessionID", sponsorAdminSession.SponsorAdminSessionID),
				new SqlParameter("@EndDT", sponsorAdminSession.EndDT)
			);
		}
		
		public override void Update(SponsorAdminSession sponsorAdminSession, int id)
		{
			string query = @"
UPDATE SponsorAdminSession SET
	SponsorAdminID = @SponsorAdminID,
	DT = @DT,
	SponsorAdminSessionID = @SponsorAdminSessionID,
	EndDT = @EndDT
WHERE SponsorAdminSessionID = @SponsorAdminSessionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminID", sponsorAdminSession.SponsorAdminID),
				new SqlParameter("@DT", sponsorAdminSession.DT),
				new SqlParameter("@SponsorAdminSessionID", sponsorAdminSession.SponsorAdminSessionID),
				new SqlParameter("@EndDT", sponsorAdminSession.EndDT)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminSession
WHERE SponsorAdminSessionID = @SponsorAdminSessionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminSessionID", id)
			);
		}
		
		public override SponsorAdminSession Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminID, 
	DT, 
	SponsorAdminSessionID, 
	EndDT
FROM SponsorAdminSession
WHERE SponsorAdminSessionID = @SponsorAdminSessionID";
			SponsorAdminSession sponsorAdminSession = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminSessionID", id))) {
				if (rs.Read()) {
					sponsorAdminSession = new SponsorAdminSession {
						SponsorAdminID = GetInt32(rs, 0),
						DT = GetString(rs, 1),
						SponsorAdminSessionID = GetInt32(rs, 2),
						EndDT = GetString(rs, 3)
					};
				}
			}
			return sponsorAdminSession;
		}
		
		public override IList<SponsorAdminSession> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminID, 
	DT, 
	SponsorAdminSessionID, 
	EndDT
FROM SponsorAdminSession";
			var sponsorAdminSessions = new List<SponsorAdminSession>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminSessions.Add(new SponsorAdminSession {
						SponsorAdminID = GetInt32(rs, 0),
						DT = GetString(rs, 1),
						SponsorAdminSessionID = GetInt32(rs, 2),
						EndDT = GetString(rs, 3)
					});
				}
			}
			return sponsorAdminSessions;
		}
	}
}
