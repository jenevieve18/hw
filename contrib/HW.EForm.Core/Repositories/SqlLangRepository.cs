using HW.EForm.Core.Models;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlLangRepository : BaseSqlRepository<Language>
	{
		public SqlLangRepository()
		{
		}
		
		public override void Save(Language lang)
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
				new SqlParameter("@Lang", lang.LanguageName),
				new SqlParameter("@LangJapaneseUnicode", lang.LangJapaneseUnicode)
			);
		}
		
		public override void Update(Language lang, int id)
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
				new SqlParameter("@Lang", lang.LanguageName),
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
		
		public override Language Read(int id)
		{
			string query = @"
SELECT 	LangID, 
	Lang, 
	LangJapaneseUnicode
FROM Lang
WHERE LangID = @LangID";
			Language lang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@LangID", id))) {
				if (rs.Read()) {
					lang = new Language {
						LangID = GetInt32(rs, 0),
						LanguageName = GetString(rs, 1),
						LangJapaneseUnicode = GetString(rs, 2)
					};
				}
			}
			return lang;
		}
		
		public override IList<Language> FindAll()
		{
			string query = @"
SELECT 	LangID, 
	Lang, 
	LangJapaneseUnicode
FROM Lang";
			var langs = new List<Language>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					langs.Add(new Language {
						LangID = GetInt32(rs, 0),
						LanguageName = GetString(rs, 1),
						LangJapaneseUnicode = GetString(rs, 2)
					});
				}
			}
			return langs;
		}
	}
}
