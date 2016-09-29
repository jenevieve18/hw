using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlDebugRepository : BaseSqlRepository<Debug>
	{
		public SqlDebugRepository()
		{
		}
		
		public override void Save(Debug debug)
		{
			string query = @"
INSERT INTO Debug(
	DebugID, 
	DebugTxt, 
	DT
)
VALUES(
	@DebugID, 
	@DebugTxt, 
	@DT
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DebugID", debug.DebugID),
				new SqlParameter("@DebugTxt", debug.DebugTxt),
				new SqlParameter("@DT", debug.DT)
			);
		}
		
		public override void Update(Debug debug, int id)
		{
			string query = @"
UPDATE Debug SET
	DebugID = @DebugID,
	DebugTxt = @DebugTxt,
	DT = @DT
WHERE DebugID = @DebugID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DebugID", debug.DebugID),
				new SqlParameter("@DebugTxt", debug.DebugTxt),
				new SqlParameter("@DT", debug.DT)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Debug
WHERE DebugID = @DebugID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@DebugID", id)
			);
		}
		
		public override Debug Read(int id)
		{
			string query = @"
SELECT 	DebugID, 
	DebugTxt, 
	DT
FROM Debug
WHERE DebugID = @DebugID";
			Debug debug = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@DebugID", id))) {
				if (rs.Read()) {
					debug = new Debug {
						DebugID = GetInt32(rs, 0),
						DebugTxt = GetString(rs, 1),
						DT = GetString(rs, 2)
					};
				}
			}
			return debug;
		}
		
		public override IList<Debug> FindAll()
		{
			string query = @"
SELECT 	DebugID, 
	DebugTxt, 
	DT
FROM Debug";
			var debugs = new List<Debug>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					debugs.Add(new Debug {
						DebugID = GetInt32(rs, 0),
						DebugTxt = GetString(rs, 1),
						DT = GetString(rs, 2)
					});
				}
			}
			return debugs;
		}
	}
}
