using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionCategoryLangRepository : BaseSqlRepository<QuestionCategoryLang>
	{
		public SqlQuestionCategoryLangRepository()
		{
		}
		
		public override void Save(QuestionCategoryLang questionCategoryLang)
		{
			string query = @"
INSERT INTO QuestionCategoryLang(
	QuestionCategoryLangID, 
	QuestionCategoryID, 
	LangID, 
	QuestionCategory, 
	QuestionCategoryJapaneseUnicode
)
VALUES(
	@QuestionCategoryLangID, 
	@QuestionCategoryID, 
	@LangID, 
	@QuestionCategory, 
	@QuestionCategoryJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryLangID", questionCategoryLang.QuestionCategoryLangID),
				new SqlParameter("@QuestionCategoryID", questionCategoryLang.QuestionCategoryID),
				new SqlParameter("@LangID", questionCategoryLang.LangID),
				new SqlParameter("@QuestionCategory", questionCategoryLang.QuestionCategory),
				new SqlParameter("@QuestionCategoryJapaneseUnicode", questionCategoryLang.QuestionCategoryJapaneseUnicode)
			);
		}
		
		public override void Update(QuestionCategoryLang questionCategoryLang, int id)
		{
			string query = @"
UPDATE QuestionCategoryLang SET
	QuestionCategoryLangID = @QuestionCategoryLangID,
	QuestionCategoryID = @QuestionCategoryID,
	LangID = @LangID,
	QuestionCategory = @QuestionCategory,
	QuestionCategoryJapaneseUnicode = @QuestionCategoryJapaneseUnicode
WHERE QuestionCategoryLangID = @QuestionCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryLangID", questionCategoryLang.QuestionCategoryLangID),
				new SqlParameter("@QuestionCategoryID", questionCategoryLang.QuestionCategoryID),
				new SqlParameter("@LangID", questionCategoryLang.LangID),
				new SqlParameter("@QuestionCategory", questionCategoryLang.QuestionCategory),
				new SqlParameter("@QuestionCategoryJapaneseUnicode", questionCategoryLang.QuestionCategoryJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionCategoryLang
WHERE QuestionCategoryLangID = @QuestionCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryLangID", id)
			);
		}
		
		public override QuestionCategoryLang Read(int id)
		{
			string query = @"
SELECT 	QuestionCategoryLangID, 
	QuestionCategoryID, 
	LangID, 
	QuestionCategory, 
	QuestionCategoryJapaneseUnicode
FROM QuestionCategoryLang
WHERE QuestionCategoryLangID = @QuestionCategoryLangID";
			QuestionCategoryLang questionCategoryLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionCategoryLangID", id))) {
				if (rs.Read()) {
					questionCategoryLang = new QuestionCategoryLang {
						QuestionCategoryLangID = GetInt32(rs, 0),
						QuestionCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						QuestionCategory = GetString(rs, 3),
						QuestionCategoryJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return questionCategoryLang;
		}
		
		public override IList<QuestionCategoryLang> FindAll()
		{
			string query = @"
SELECT 	QuestionCategoryLangID, 
	QuestionCategoryID, 
	LangID, 
	QuestionCategory, 
	QuestionCategoryJapaneseUnicode
FROM QuestionCategoryLang";
			var questionCategoryLangs = new List<QuestionCategoryLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionCategoryLangs.Add(new QuestionCategoryLang {
						QuestionCategoryLangID = GetInt32(rs, 0),
						QuestionCategoryID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						QuestionCategory = GetString(rs, 3),
						QuestionCategoryJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return questionCategoryLangs;
		}
	}
}
