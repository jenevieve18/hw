using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlLangRepository : BaseSqlRepository<Lang>
	{
		public SqlLangRepository()
		{
		}
		
		public override void Save(Lang lang)
		{
			string query = @"
INSERT INTO Lang(
	LangID, 
	Lang
)
VALUES(
	@LangID, 
	@Lang
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LangID", lang.LangID),
				new SqlParameter("@Lang", lang.Language)
			);
		}
		
		public override void Update(Lang lang, int id)
		{
			string query = @"
UPDATE Lang SET
	LangID = @LangID,
	Lang = @Lang
WHERE LangID = @LangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LangID", lang.LangID),
				new SqlParameter("@Lang", lang.Language)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Lang
WHERE LangID = @LangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LangID", id)
			);
		}
		
		public override Lang Read(int id)
		{
			string query = @"
SELECT 	LangID, 
	Lang
FROM Lang
WHERE LangID = @LangID";
			Lang lang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@LangID", id))) {
				if (rs.Read()) {
					lang = new Lang {
						LangID = GetInt32(rs, 0),
						Language = GetString(rs, 1)
					};
				}
			}
			return lang;
		}
		
		public override IList<Lang> FindAll()
		{
			string query = @"
SELECT 	LangID, 
	Lang
FROM Lang";
			var langs = new List<Lang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					langs.Add(new Lang {
						LangID = GetInt32(rs, 0),
						Language = GetString(rs, 1)
					});
				}
			}
			return langs;
		}
	}
}
