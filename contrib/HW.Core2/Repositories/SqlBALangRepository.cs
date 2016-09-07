using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlBALangRepository : BaseSqlRepository<BALang>
	{
		public SqlBALangRepository()
		{
		}
		
		public override void Save(BALang bALang)
		{
			string query = @"
INSERT INTO BALang(
	BALangID, 
	BAID, 
	LangID, 
	BA
)
VALUES(
	@BALangID, 
	@BAID, 
	@LangID, 
	@BA
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BALangID", bALang.BALangID),
				new SqlParameter("@BAID", bALang.BAID),
				new SqlParameter("@LangID", bALang.LangID),
				new SqlParameter("@BA", bALang.BA)
			);
		}
		
		public override void Update(BALang bALang, int id)
		{
			string query = @"
UPDATE BALang SET
	BALangID = @BALangID,
	BAID = @BAID,
	LangID = @LangID,
	BA = @BA
WHERE BALangID = @BALangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BALangID", bALang.BALangID),
				new SqlParameter("@BAID", bALang.BAID),
				new SqlParameter("@LangID", bALang.LangID),
				new SqlParameter("@BA", bALang.BA)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM BALang
WHERE BALangID = @BALangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@BALangID", id)
			);
		}
		
		public override BALang Read(int id)
		{
			string query = @"
SELECT 	BALangID, 
	BAID, 
	LangID, 
	BA
FROM BALang
WHERE BALangID = @BALangID";
			BALang bALang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@BALangID", id))) {
				if (rs.Read()) {
					bALang = new BALang {
						BALangID = GetInt32(rs, 0),
						BAID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						BA = GetString(rs, 3)
					};
				}
			}
			return bALang;
		}
		
		public override IList<BALang> FindAll()
		{
			string query = @"
SELECT 	BALangID, 
	BAID, 
	LangID, 
	BA
FROM BALang";
			var bALangs = new List<BALang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					bALangs.Add(new BALang {
						BALangID = GetInt32(rs, 0),
						BAID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						BA = GetString(rs, 3)
					});
				}
			}
			return bALangs;
		}
	}
}
