using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public interface IQuestionLangRepository : IBaseRepository<QuestionLang>
	{
		IList<QuestionLang> FindByQuestion(int questionID);
	}
	
	public class SqlQuestionLangRepository : BaseSqlRepository<QuestionLang>, IQuestionLangRepository
	{
		public SqlQuestionLangRepository()
		{
		}
		
		public override void Save(QuestionLang questionLang)
		{
			string query = @"
INSERT INTO QuestionLang(
	QuestionLangID, 
	QuestionID, 
	LangID, 
	Question, 
	QuestionShort, 
	QuestionArea, 
	QuestionJapaneseUnicode, 
	QuestionShortJapaneseUnicode, 
	QuestionAreaJapaneseUnicode
)
VALUES(
	@QuestionLangID, 
	@QuestionID, 
	@LangID, 
	@Question, 
	@QuestionShort, 
	@QuestionArea, 
	@QuestionJapaneseUnicode, 
	@QuestionShortJapaneseUnicode, 
	@QuestionAreaJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionLangID", questionLang.QuestionLangID),
				new SqlParameter("@QuestionID", questionLang.QuestionID),
				new SqlParameter("@LangID", questionLang.LangID),
				new SqlParameter("@Question", questionLang.Question),
				new SqlParameter("@QuestionShort", questionLang.QuestionShort),
				new SqlParameter("@QuestionArea", questionLang.QuestionArea),
				new SqlParameter("@QuestionJapaneseUnicode", questionLang.QuestionJapaneseUnicode),
				new SqlParameter("@QuestionShortJapaneseUnicode", questionLang.QuestionShortJapaneseUnicode),
				new SqlParameter("@QuestionAreaJapaneseUnicode", questionLang.QuestionAreaJapaneseUnicode)
			);
		}
		
		public override void Update(QuestionLang questionLang, int id)
		{
			string query = @"
UPDATE QuestionLang SET
	QuestionLangID = @QuestionLangID,
	QuestionID = @QuestionID,
	LangID = @LangID,
	Question = @Question,
	QuestionShort = @QuestionShort,
	QuestionArea = @QuestionArea,
	QuestionJapaneseUnicode = @QuestionJapaneseUnicode,
	QuestionShortJapaneseUnicode = @QuestionShortJapaneseUnicode,
	QuestionAreaJapaneseUnicode = @QuestionAreaJapaneseUnicode
WHERE QuestionLangID = @QuestionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionLangID", questionLang.QuestionLangID),
				new SqlParameter("@QuestionID", questionLang.QuestionID),
				new SqlParameter("@LangID", questionLang.LangID),
				new SqlParameter("@Question", questionLang.Question),
				new SqlParameter("@QuestionShort", questionLang.QuestionShort),
				new SqlParameter("@QuestionArea", questionLang.QuestionArea),
				new SqlParameter("@QuestionJapaneseUnicode", questionLang.QuestionJapaneseUnicode),
				new SqlParameter("@QuestionShortJapaneseUnicode", questionLang.QuestionShortJapaneseUnicode),
				new SqlParameter("@QuestionAreaJapaneseUnicode", questionLang.QuestionAreaJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionLang
WHERE QuestionLangID = @QuestionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionLangID", id)
			);
		}
		
		public override QuestionLang Read(int id)
		{
			string query = @"
SELECT 	QuestionLangID, 
	QuestionID, 
	LangID, 
	Question, 
	QuestionShort, 
	QuestionArea, 
	QuestionJapaneseUnicode, 
	QuestionShortJapaneseUnicode, 
	QuestionAreaJapaneseUnicode
FROM QuestionLang
WHERE QuestionLangID = @QuestionLangID";
			QuestionLang questionLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionLangID", id))) {
				if (rs.Read()) {
					questionLang = new QuestionLang {
						QuestionLangID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						QuestionShort = GetString(rs, 4),
						QuestionArea = GetString(rs, 5),
						QuestionJapaneseUnicode = GetString(rs, 6),
						QuestionShortJapaneseUnicode = GetString(rs, 7),
						QuestionAreaJapaneseUnicode = GetString(rs, 8)
					};
				}
			}
			return questionLang;
		}
		
		public QuestionLang ReadByQuestionAndLang(int questionID, int langID)
		{
			string query = @"
SELECT 	QuestionLangID, 
	QuestionID, 
	LangID, 
	Question, 
	QuestionShort, 
	QuestionArea, 
	QuestionJapaneseUnicode, 
	QuestionShortJapaneseUnicode, 
	QuestionAreaJapaneseUnicode
FROM QuestionLang
WHERE QuestionID = @QuestionID
AND LangID = @LangID";
			QuestionLang questionLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID), new SqlParameter("@LangID", langID))) {
				if (rs.Read()) {
					questionLang = new QuestionLang {
						QuestionLangID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						QuestionShort = GetString(rs, 4),
						QuestionArea = GetString(rs, 5),
						QuestionJapaneseUnicode = GetString(rs, 6),
						QuestionShortJapaneseUnicode = GetString(rs, 7),
						QuestionAreaJapaneseUnicode = GetString(rs, 8)
					};
				}
			}
			return questionLang;
		}
		
		public override IList<QuestionLang> FindAll()
		{
			string query = @"
SELECT 	QuestionLangID, 
	QuestionID, 
	LangID, 
	Question, 
	QuestionShort, 
	QuestionArea, 
	QuestionJapaneseUnicode, 
	QuestionShortJapaneseUnicode, 
	QuestionAreaJapaneseUnicode
FROM QuestionLang";
			var questionLangs = new List<QuestionLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionLangs.Add(new QuestionLang {
						QuestionLangID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						QuestionShort = GetString(rs, 4),
						QuestionArea = GetString(rs, 5),
						QuestionJapaneseUnicode = GetString(rs, 6),
						QuestionShortJapaneseUnicode = GetString(rs, 7),
						QuestionAreaJapaneseUnicode = GetString(rs, 8)
					});
				}
			}
			return questionLangs;
		}
		
		public IList<QuestionLang> FindByQuestion(int questionID)
		{
			string query = @"
SELECT 	QuestionLangID, 
	QuestionID, 
	LangID, 
	Question, 
	QuestionShort, 
	QuestionArea, 
	QuestionJapaneseUnicode, 
	QuestionShortJapaneseUnicode, 
	QuestionAreaJapaneseUnicode
FROM QuestionLang
WHERE QuestionID = @QuestionID";
			var questionLangs = new List<QuestionLang>();
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID))) {
				while (rs.Read()) {
					questionLangs.Add(new QuestionLang {
						QuestionLangID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						QuestionShort = GetString(rs, 4),
						QuestionArea = GetString(rs, 5),
						QuestionJapaneseUnicode = GetString(rs, 6),
						QuestionShortJapaneseUnicode = GetString(rs, 7),
						QuestionAreaJapaneseUnicode = GetString(rs, 8)
					});
				}
			}
			return questionLangs;
		}
	}
}
