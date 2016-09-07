using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlBQvisibilityRepository : BaseSqlRepository<BQvisibility>
	{
		public SqlBQvisibilityRepository()
		{
		}
		
		public override void Save(BQvisibility bQvisibility)
		{
			string query = @"
INSERT INTO BQvisibility(
	BQvisibilityID, 
	ChildBQID, 
	BQID, 
	BAID
)
VALUES(
	@BQvisibilityID, 
	@ChildBQID, 
	@BQID, 
	@BAID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQvisibilityID", bQvisibility.BQvisibilityID),
				new SqlParameter("@ChildBQID", bQvisibility.ChildBQID),
				new SqlParameter("@BQID", bQvisibility.BQID),
				new SqlParameter("@BAID", bQvisibility.BAID)
			);
		}
		
		public override void Update(BQvisibility bQvisibility, int id)
		{
			string query = @"
UPDATE BQvisibility SET
	BQvisibilityID = @BQvisibilityID,
	ChildBQID = @ChildBQID,
	BQID = @BQID,
	BAID = @BAID
WHERE BQvisibilityID = @BQvisibilityID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQvisibilityID", bQvisibility.BQvisibilityID),
				new SqlParameter("@ChildBQID", bQvisibility.ChildBQID),
				new SqlParameter("@BQID", bQvisibility.BQID),
				new SqlParameter("@BAID", bQvisibility.BAID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM BQvisibility
WHERE BQvisibilityID = @BQvisibilityID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQvisibilityID", id)
			);
		}
		
		public override BQvisibility Read(int id)
		{
			string query = @"
SELECT 	BQvisibilityID, 
	ChildBQID, 
	BQID, 
	BAID
FROM BQvisibility
WHERE BQvisibilityID = @BQvisibilityID";
			BQvisibility bQvisibility = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@BQvisibilityID", id))) {
				if (rs.Read()) {
					bQvisibility = new BQvisibility {
						BQvisibilityID = GetInt32(rs, 0),
						ChildBQID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						BAID = GetInt32(rs, 3)
					};
				}
			}
			return bQvisibility;
		}
		
		public override IList<BQvisibility> FindAll()
		{
			string query = @"
SELECT 	BQvisibilityID, 
	ChildBQID, 
	BQID, 
	BAID
FROM BQvisibility";
			var bQvisibilitys = new List<BQvisibility>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					bQvisibilitys.Add(new BQvisibility {
						BQvisibilityID = GetInt32(rs, 0),
						ChildBQID = GetInt32(rs, 1),
						BQID = GetInt32(rs, 2),
						BAID = GetInt32(rs, 3)
					});
				}
			}
			return bQvisibilitys;
		}
	}
}
