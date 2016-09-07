using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlProfileComparisonBQRepository : BaseSqlRepository<ProfileComparisonBQ>
	{
		public SqlProfileComparisonBQRepository()
		{
		}
		
		public override void Save(ProfileComparisonBQ profileComparisonBQ)
		{
			string query = @"
INSERT INTO ProfileComparisonBQ(
	ProfileComparisonBQID, 
	BQID, 
	ValueInt, 
	ProfileComparisonID
)
VALUES(
	@ProfileComparisonBQID, 
	@BQID, 
	@ValueInt, 
	@ProfileComparisonID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProfileComparisonBQID", profileComparisonBQ.ProfileComparisonBQID),
				new SqlParameter("@BQID", profileComparisonBQ.BQID),
				new SqlParameter("@ValueInt", profileComparisonBQ.ValueInt),
				new SqlParameter("@ProfileComparisonID", profileComparisonBQ.ProfileComparisonID)
			);
		}
		
		public override void Update(ProfileComparisonBQ profileComparisonBQ, int id)
		{
			string query = @"
UPDATE ProfileComparisonBQ SET
	ProfileComparisonBQID = @ProfileComparisonBQID,
	BQID = @BQID,
	ValueInt = @ValueInt,
	ProfileComparisonID = @ProfileComparisonID
WHERE ProfileComparisonBQID = @ProfileComparisonBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProfileComparisonBQID", profileComparisonBQ.ProfileComparisonBQID),
				new SqlParameter("@BQID", profileComparisonBQ.BQID),
				new SqlParameter("@ValueInt", profileComparisonBQ.ValueInt),
				new SqlParameter("@ProfileComparisonID", profileComparisonBQ.ProfileComparisonID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProfileComparisonBQ
WHERE ProfileComparisonBQID = @ProfileComparisonBQID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProfileComparisonBQID", id)
			);
		}
		
		public override ProfileComparisonBQ Read(int id)
		{
			string query = @"
SELECT 	ProfileComparisonBQID, 
	BQID, 
	ValueInt, 
	ProfileComparisonID
FROM ProfileComparisonBQ
WHERE ProfileComparisonBQID = @ProfileComparisonBQID";
			ProfileComparisonBQ profileComparisonBQ = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProfileComparisonBQID", id))) {
				if (rs.Read()) {
					profileComparisonBQ = new ProfileComparisonBQ {
						ProfileComparisonBQID = GetInt32(rs, 0),
						BQID = GetInt32(rs, 1),
						ValueInt = GetInt32(rs, 2),
						ProfileComparisonID = GetInt32(rs, 3)
					};
				}
			}
			return profileComparisonBQ;
		}
		
		public override IList<ProfileComparisonBQ> FindAll()
		{
			string query = @"
SELECT 	ProfileComparisonBQID, 
	BQID, 
	ValueInt, 
	ProfileComparisonID
FROM ProfileComparisonBQ";
			var profileComparisonBQs = new List<ProfileComparisonBQ>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					profileComparisonBQs.Add(new ProfileComparisonBQ {
						ProfileComparisonBQID = GetInt32(rs, 0),
						BQID = GetInt32(rs, 1),
						ValueInt = GetInt32(rs, 2),
						ProfileComparisonID = GetInt32(rs, 3)
					});
				}
			}
			return profileComparisonBQs;
		}
	}
}
