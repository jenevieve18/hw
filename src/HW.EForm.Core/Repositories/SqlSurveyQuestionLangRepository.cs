using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyQuestionLangRepository : BaseSqlRepository<SurveyQuestionLang>
	{
		public SqlSurveyQuestionLangRepository()
		{
		}
		
		public override void Save(SurveyQuestionLang surveyQuestionLang)
		{
			string query = @"
INSERT INTO SurveyQuestionLang(
	SurveyQuestionLangID, 
	SurveyQuestionID, 
	LangID, 
	Question, 
	QuestionJapaneseUnicode
)
VALUES(
	@SurveyQuestionLangID, 
	@SurveyQuestionID, 
	@LangID, 
	@Question, 
	@QuestionJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionLangID", surveyQuestionLang.SurveyQuestionLangID),
				new SqlParameter("@SurveyQuestionID", surveyQuestionLang.SurveyQuestionID),
				new SqlParameter("@LangID", surveyQuestionLang.LangID),
				new SqlParameter("@Question", surveyQuestionLang.Question),
				new SqlParameter("@QuestionJapaneseUnicode", surveyQuestionLang.QuestionJapaneseUnicode)
			);
		}
		
		public override void Update(SurveyQuestionLang surveyQuestionLang, int id)
		{
			string query = @"
UPDATE SurveyQuestionLang SET
	SurveyQuestionLangID = @SurveyQuestionLangID,
	SurveyQuestionID = @SurveyQuestionID,
	LangID = @LangID,
	Question = @Question,
	QuestionJapaneseUnicode = @QuestionJapaneseUnicode
WHERE SurveyQuestionLangID = @SurveyQuestionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionLangID", surveyQuestionLang.SurveyQuestionLangID),
				new SqlParameter("@SurveyQuestionID", surveyQuestionLang.SurveyQuestionID),
				new SqlParameter("@LangID", surveyQuestionLang.LangID),
				new SqlParameter("@Question", surveyQuestionLang.Question),
				new SqlParameter("@QuestionJapaneseUnicode", surveyQuestionLang.QuestionJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyQuestionLang
WHERE SurveyQuestionLangID = @SurveyQuestionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionLangID", id)
			);
		}
		
		public override SurveyQuestionLang Read(int id)
		{
			string query = @"
SELECT 	SurveyQuestionLangID, 
	SurveyQuestionID, 
	LangID, 
	Question, 
	QuestionJapaneseUnicode
FROM SurveyQuestionLang
WHERE SurveyQuestionLangID = @SurveyQuestionLangID";
			SurveyQuestionLang surveyQuestionLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyQuestionLangID", id))) {
				if (rs.Read()) {
					surveyQuestionLang = new SurveyQuestionLang {
						SurveyQuestionLangID = GetInt32(rs, 0),
						SurveyQuestionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						QuestionJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return surveyQuestionLang;
		}
		
		public override IList<SurveyQuestionLang> FindAll()
		{
			string query = @"
SELECT 	SurveyQuestionLangID, 
	SurveyQuestionID, 
	LangID, 
	Question, 
	QuestionJapaneseUnicode
FROM SurveyQuestionLang";
			var surveyQuestionLangs = new List<SurveyQuestionLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyQuestionLangs.Add(new SurveyQuestionLang {
						SurveyQuestionLangID = GetInt32(rs, 0),
						SurveyQuestionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Question = GetString(rs, 3),
						QuestionJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return surveyQuestionLangs;
		}
	}
}
