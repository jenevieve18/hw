using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
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
	Lang, 
	LangJapaneseUnicode
)
VALUES(
	@LangID, 
	@Lang, 
	@LangJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LangID", lang.LangID),
				new SqlParameter("@Lang", lang.LangText),
				new SqlParameter("@LangJapaneseUnicode", lang.LangJapaneseUnicode)
			);
		}
		
		public override void Update(Lang lang, int id)
		{
			string query = @"
UPDATE Lang SET
	LangID = @LangID,
	Lang = @Lang,
	LangJapaneseUnicode = @LangJapaneseUnicode
WHERE LangID = @LangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@LangID", lang.LangID),
				new SqlParameter("@Lang", lang.LangText),
				new SqlParameter("@LangJapaneseUnicode", lang.LangJapaneseUnicode)
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
	Lang, 
	LangJapaneseUnicode
FROM Lang
WHERE LangID = @LangID";
			Lang lang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@LangID", id))) {
				if (rs.Read()) {
					lang = new Lang {
						LangID = GetInt32(rs, 0),
						LangText = GetString(rs, 1),
						LangJapaneseUnicode = GetString(rs, 2)
					};
				}
			}
			return lang;
		}
		
		public override IList<Lang> FindAll()
		{
			string query = @"
SELECT 	LangID, 
	Lang, 
	LangJapaneseUnicode
FROM Lang";
			var langs = new List<Lang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					langs.Add(new Lang {
						LangID = GetInt32(rs, 0),
						LangText = GetString(rs, 1),
						LangJapaneseUnicode = GetString(rs, 2)
					});
				}
			}
			return langs;
		}
	}
}
