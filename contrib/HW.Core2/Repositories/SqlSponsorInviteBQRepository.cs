using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorInviteBQRepository : BaseSqlRepository<SponsorInviteBQ>
	{
		public SqlSponsorInviteBQRepository()
		{
		}
		
		public override void Save(SponsorInviteBQ sponsorInviteBQ)
		{
			string query = @"
INSERT INTO SponsorInviteBQ(
	SponsorInviteBQID, 
	SponsorInviteID, 
	BQID, 
	BAID, 
	ValueInt, 
	ValueDate, 
	ValueText
)
VALUES(
	@SponsorInviteBQID, 
	@SponsorInviteID, 
	@BQID, 
	@BAID, 
	@ValueInt, 
	@ValueDate, 
	@ValueText
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorInviteBQID", sponsorInviteBQ.SponsorInviteBQID),
				new SqlParameter("@SponsorInviteID", sponsorInviteBQ.SponsorInviteID),
				new SqlParameter("@BQID", sponsorInviteBQ.BQID),
				new SqlParameter("@BAID", sponsorInviteBQ.BAID),
				new SqlParameter("@ValueInt", sponsorInviteBQ.ValueInt),
				new SqlParameter("@ValueDate", sponsorInviteBQ.ValueDate),
				new SqlParameter("@ValueText", sponsorInviteBQ.ValueText)
			);
		}
		
		public override void Update(SponsorInviteBQ sponsorInviteBQ, int id)
		{
			string query = @"
UPDATE SponsorInviteBQ SET
	SponsorInviteBQID = @SponsorInviteBQID,
	SponsorInviteID = @SponsorInviteID,
	BQID = @BQID,
	BAID = @BAID,
	ValueInt = @ValueInt,
	ValueDate = @ValueDate,
	ValueText = @ValueText
WHERE SponsorInviteBQID = @SponsorInviteBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorInviteBQID", sponsorInviteBQ.SponsorInviteBQID),
				new SqlParameter("@SponsorInviteID", sponsorInviteBQ.SponsorInviteID),
				new SqlParameter("@BQID", sponsorInviteBQ.BQID),
				new SqlParameter("@BAID", sponsorInviteBQ.BAID),
				new SqlParameter("@ValueInt", sponsorInviteBQ.ValueInt),
				new SqlParameter("@ValueDate", sponsorInviteBQ.ValueDate),
				new SqlParameter("@ValueText", sponsorInviteBQ.ValueText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorInviteBQ
WHERE SponsorInviteBQID = @SponsorInviteBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorInviteBQID", id)
			);
		}
		
		public override SponsorInviteBQ Read(int id)
		{
			string query = @"
SELECT 	SponsorInviteBQID, 
	SponsorInviteID, 
	BQID, 
	BAID, 
	ValueInt, 
	ValueDate, 
	ValueText
FROM SponsorInviteBQ
WHERE SponsorInviteBQID = @SponsorInviteBQID";
			SponsorInviteBQ sponsorInviteBQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorInviteBQID", id))) {
				if (rs.Read()) {
					sponsorInviteBQ = new SponsorInviteBQ {
						SponsorInviteBQID = GetInt32(rs, 0),
						SponsorInviteID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						BAID = GetInt32(rs, 3),
						ValueInt = GetInt32(rs, 4),
						ValueDate = GetDateTime(rs, 5),
						ValueText = GetString(rs, 6)
					};
				}
			}
			return sponsorInviteBQ;
		}
		
		public override IList<SponsorInviteBQ> FindAll()
		{
			string query = @"
SELECT 	SponsorInviteBQID, 
	SponsorInviteID, 
	BQID, 
	BAID, 
	ValueInt, 
	ValueDate, 
	ValueText
FROM SponsorInviteBQ";
			var sponsorInviteBQs = new List<SponsorInviteBQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorInviteBQs.Add(new SponsorInviteBQ {
						SponsorInviteBQID = GetInt32(rs, 0),
						SponsorInviteID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						BAID = GetInt32(rs, 3),
						ValueInt = GetInt32(rs, 4),
						ValueDate = GetDateTime(rs, 5),
						ValueText = GetString(rs, 6)
					});
				}
			}
			return sponsorInviteBQs;
		}
	}
}
