using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlBQLangRepository : BaseSqlRepository<BQLang>
	{
		public SqlBQLangRepository()
		{
		}
		
		public override void Save(BQLang bQLang)
		{
			string query = @"
INSERT INTO BQLang(
	BQLangID, 
	BQID, 
	LangID, 
	BQ
)
VALUES(
	@BQLangID, 
	@BQID, 
	@LangID, 
	@BQ
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQLangID", bQLang.BQLangID),
				new SqlParameter("@BQID", bQLang.BQID),
				new SqlParameter("@LangID", bQLang.LangID),
				new SqlParameter("@BQ", bQLang.BQ)
			);
		}
		
		public override void Update(BQLang bQLang, int id)
		{
			string query = @"
UPDATE BQLang SET
	BQLangID = @BQLangID,
	BQID = @BQID,
	LangID = @LangID,
	BQ = @BQ
WHERE BQLangID = @BQLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQLangID", bQLang.BQLangID),
				new SqlParameter("@BQID", bQLang.BQID),
				new SqlParameter("@LangID", bQLang.LangID),
				new SqlParameter("@BQ", bQLang.BQ)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM BQLang
WHERE BQLangID = @BQLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BQLangID", id)
			);
		}
		
		public override BQLang Read(int id)
		{
			string query = @"
SELECT 	BQLangID, 
	BQID, 
	LangID, 
	BQ
FROM BQLang
WHERE BQLangID = @BQLangID";
			BQLang bQLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@BQLangID", id))) {
				if (rs.Read()) {
					bQLang = new BQLang {
						BQLangID = GetInt32(rs, 0),
						BQID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						BQ = GetString(rs, 3)
					};
				}
			}
			return bQLang;
		}
		
		public override IList<BQLang> FindAll()
		{
			string query = @"
SELECT 	BQLangID, 
	BQID, 
	LangID, 
	BQ
FROM BQLang";
			var bQLangs = new List<BQLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					bQLangs.Add(new BQLang {
						BQLangID = GetInt32(rs, 0),
						BQID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						BQ = GetString(rs, 3)
					});
				}
			}
			return bQLangs;
		}
	}
}
