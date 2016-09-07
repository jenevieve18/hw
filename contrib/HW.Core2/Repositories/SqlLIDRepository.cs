using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlLIDRepository : BaseSqlRepository<LID>
	{
		public SqlLIDRepository()
		{
		}
		
		public override void Save(LID lID)
		{
			string query = @"
INSERT INTO LID(
	LID, 
	Language
)
VALUES(
	@LID, 
	@Language
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LID", lID.LID),
				new SqlParameter("@Language", lID.Language)
			);
		}
		
		public override void Update(LID lID, int id)
		{
			string query = @"
UPDATE LID SET
	LID = @LID,
	Language = @Language
WHERE LIDID = @LIDID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LID", lID.LID),
				new SqlParameter("@Language", lID.Language)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM LID
WHERE LIDID = @LIDID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LIDID", id)
			);
		}
		
		public override LID Read(int id)
		{
			string query = @"
SELECT 	LID, 
	Language
FROM LID
WHERE LIDID = @LIDID";
			LID lID = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@LIDID", id))) {
				if (rs.Read()) {
					lID = new LID {
						LID = GetInt32(rs, 0),
						Language = GetString(rs, 1)
					};
				}
			}
			return lID;
		}
		
		public override IList<LID> FindAll()
		{
			string query = @"
SELECT 	LID, 
	Language
FROM LID";
			var lIDs = new List<LID>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					lIDs.Add(new LID {
						LID = GetInt32(rs, 0),
						Language = GetString(rs, 1)
					});
				}
			}
			return lIDs;
		}
	}
}
