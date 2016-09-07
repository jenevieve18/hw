using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlWiseLangRepository : BaseSqlRepository<WiseLang>
	{
		public SqlWiseLangRepository()
		{
		}
		
		public override void Save(WiseLang wiseLang)
		{
			string query = @"
INSERT INTO WiseLang(
	WiseLangID, 
	WiseID, 
	LangID, 
	Wise, 
	WiseBy
)
VALUES(
	@WiseLangID, 
	@WiseID, 
	@LangID, 
	@Wise, 
	@WiseBy
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WiseLangID", wiseLang.WiseLangID),
				new SqlParameter("@WiseID", wiseLang.WiseID),
				new SqlParameter("@LangID", wiseLang.LangID),
				new SqlParameter("@Wise", wiseLang.Wise),
				new SqlParameter("@WiseBy", wiseLang.WiseBy)
			);
		}
		
		public override void Update(WiseLang wiseLang, int id)
		{
			string query = @"
UPDATE WiseLang SET
	WiseLangID = @WiseLangID,
	WiseID = @WiseID,
	LangID = @LangID,
	Wise = @Wise,
	WiseBy = @WiseBy
WHERE WiseLangID = @WiseLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WiseLangID", wiseLang.WiseLangID),
				new SqlParameter("@WiseID", wiseLang.WiseID),
				new SqlParameter("@LangID", wiseLang.LangID),
				new SqlParameter("@Wise", wiseLang.Wise),
				new SqlParameter("@WiseBy", wiseLang.WiseBy)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM WiseLang
WHERE WiseLangID = @WiseLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WiseLangID", id)
			);
		}
		
		public override WiseLang Read(int id)
		{
			string query = @"
SELECT 	WiseLangID, 
	WiseID, 
	LangID, 
	Wise, 
	WiseBy
FROM WiseLang
WHERE WiseLangID = @WiseLangID";
			WiseLang wiseLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@WiseLangID", id))) {
				if (rs.Read()) {
					wiseLang = new WiseLang {
						WiseLangID = GetInt32(rs, 0),
						WiseID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Wise = GetString(rs, 3),
						WiseBy = GetString(rs, 4)
					};
				}
			}
			return wiseLang;
		}
		
		public override IList<WiseLang> FindAll()
		{
			string query = @"
SELECT 	WiseLangID, 
	WiseID, 
	LangID, 
	Wise, 
	WiseBy
FROM WiseLang";
			var wiseLangs = new List<WiseLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					wiseLangs.Add(new WiseLang {
						WiseLangID = GetInt32(rs, 0),
						WiseID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Wise = GetString(rs, 3),
						WiseBy = GetString(rs, 4)
					});
				}
			}
			return wiseLangs;
		}
	}
}
