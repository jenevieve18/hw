using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlBARepository : BaseSqlRepository<BA>
	{
		public SqlBARepository()
		{
		}
		
		public override void Save(BA bA)
		{
			string query = @"
INSERT INTO BA(
	BAID, 
	BQID, 
	Internal, 
	SortOrder, 
	Value
)
VALUES(
	@BAID, 
	@BQID, 
	@Internal, 
	@SortOrder, 
	@Value
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BAID", bA.BAID),
				new SqlParameter("@BQID", bA.BQID),
				new SqlParameter("@Internal", bA.Internal),
				new SqlParameter("@SortOrder", bA.SortOrder),
				new SqlParameter("@Value", bA.Value)
			);
		}
		
		public override void Update(BA bA, int id)
		{
			string query = @"
UPDATE BA SET
	BAID = @BAID,
	BQID = @BQID,
	Internal = @Internal,
	SortOrder = @SortOrder,
	Value = @Value
WHERE BAID = @BAID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BAID", bA.BAID),
				new SqlParameter("@BQID", bA.BQID),
				new SqlParameter("@Internal", bA.Internal),
				new SqlParameter("@SortOrder", bA.SortOrder),
				new SqlParameter("@Value", bA.Value)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM BA
WHERE BAID = @BAID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BAID", id)
			);
		}
		
		public override BA Read(int id)
		{
			string query = @"
SELECT 	BAID, 
	BQID, 
	Internal, 
	SortOrder, 
	Value
FROM BA
WHERE BAID = @BAID";
			BA bA = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@BAID", id))) {
				if (rs.Read()) {
					bA = new BA {
						BAID = GetInt32(rs, 0),
						BQID = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						SortOrder = GetInt32(rs, 3),
						Value = GetInt32(rs, 4)
					};
				}
			}
			return bA;
		}
		
		public override IList<BA> FindAll()
		{
			string query = @"
SELECT 	BAID, 
	BQID, 
	Internal, 
	SortOrder, 
	Value
FROM BA";
			var bAs = new List<BA>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					bAs.Add(new BA {
						BAID = GetInt32(rs, 0),
						BQID = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						SortOrder = GetInt32(rs, 3),
						Value = GetInt32(rs, 4)
					});
				}
			}
			return bAs;
		}
	}
}
