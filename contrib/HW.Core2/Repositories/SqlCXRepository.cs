using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlCXRepository : BaseSqlRepository<CX>
	{
		public SqlCXRepository()
		{
		}
		
		public override void Save(CX cX)
		{
			string query = @"
INSERT INTO CX(
	CXID
)
VALUES(
	@CXID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CXID", cX.CXID)
			);
		}
		
		public override void Update(CX cX, int id)
		{
			string query = @"
UPDATE CX SET
	CXID = @CXID
WHERE CXID = @CXID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CXID", cX.CXID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM CX
WHERE CXID = @CXID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@CXID", id)
			);
		}
		
		public override CX Read(int id)
		{
			string query = @"
SELECT 	CXID
FROM CX
WHERE CXID = @CXID";
			CX cX = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@CXID", id))) {
				if (rs.Read()) {
					cX = new CX {
						CXID = GetInt32(rs, 0)
					};
				}
			}
			return cX;
		}
		
		public override IList<CX> FindAll()
		{
			string query = @"
SELECT 	CXID
FROM CX";
			var cXs = new List<CX>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					cXs.Add(new CX {
						CXID = GetInt32(rs, 0)
					});
				}
			}
			return cXs;
		}
	}
}
