using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyQuestionOptionComponentLangRepository : BaseSqlRepository<SurveyQuestionOptionComponentLang>
	{
		public SqlSurveyQuestionOptionComponentLangRepository()
		{
		}
		
		public override void Save(SurveyQuestionOptionComponentLang surveyQuestionOptionComponentLang)
		{
			string query = @"
INSERT INTO SurveyQuestionOptionComponentLang(
	SurveyQuestionOptionComponentLangID, 
	SurveyQuestionOptionID, 
	OptionComponentID, 
	LangID, 
	Text, 
	OnClick, 
	TextJapaneseUnicode, 
	OnClickJapaneseUnicode
)
VALUES(
	@SurveyQuestionOptionComponentLangID, 
	@SurveyQuestionOptionID, 
	@OptionComponentID, 
	@LangID, 
	@Text, 
	@OnClick, 
	@TextJapaneseUnicode, 
	@OnClickJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionComponentLangID", surveyQuestionOptionComponentLang.SurveyQuestionOptionComponentLangID),
				new SqlParameter("@SurveyQuestionOptionID", surveyQuestionOptionComponentLang.SurveyQuestionOptionID),
				new SqlParameter("@OptionComponentID", surveyQuestionOptionComponentLang.OptionComponentID),
				new SqlParameter("@LangID", surveyQuestionOptionComponentLang.LangID),
				new SqlParameter("@Text", surveyQuestionOptionComponentLang.Text),
				new SqlParameter("@OnClick", surveyQuestionOptionComponentLang.OnClick),
				new SqlParameter("@TextJapaneseUnicode", surveyQuestionOptionComponentLang.TextJapaneseUnicode),
				new SqlParameter("@OnClickJapaneseUnicode", surveyQuestionOptionComponentLang.OnClickJapaneseUnicode)
			);
		}
		
		public override void Update(SurveyQuestionOptionComponentLang surveyQuestionOptionComponentLang, int id)
		{
			string query = @"
UPDATE SurveyQuestionOptionComponentLang SET
	SurveyQuestionOptionComponentLangID = @SurveyQuestionOptionComponentLangID,
	SurveyQuestionOptionID = @SurveyQuestionOptionID,
	OptionComponentID = @OptionComponentID,
	LangID = @LangID,
	Text = @Text,
	OnClick = @OnClick,
	TextJapaneseUnicode = @TextJapaneseUnicode,
	OnClickJapaneseUnicode = @OnClickJapaneseUnicode
WHERE SurveyQuestionOptionComponentLangID = @SurveyQuestionOptionComponentLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionComponentLangID", surveyQuestionOptionComponentLang.SurveyQuestionOptionComponentLangID),
				new SqlParameter("@SurveyQuestionOptionID", surveyQuestionOptionComponentLang.SurveyQuestionOptionID),
				new SqlParameter("@OptionComponentID", surveyQuestionOptionComponentLang.OptionComponentID),
				new SqlParameter("@LangID", surveyQuestionOptionComponentLang.LangID),
				new SqlParameter("@Text", surveyQuestionOptionComponentLang.Text),
				new SqlParameter("@OnClick", surveyQuestionOptionComponentLang.OnClick),
				new SqlParameter("@TextJapaneseUnicode", surveyQuestionOptionComponentLang.TextJapaneseUnicode),
				new SqlParameter("@OnClickJapaneseUnicode", surveyQuestionOptionComponentLang.OnClickJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyQuestionOptionComponentLang
WHERE SurveyQuestionOptionComponentLangID = @SurveyQuestionOptionComponentLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionOptionComponentLangID", id)
			);
		}
		
		public override SurveyQuestionOptionComponentLang Read(int id)
		{
			string query = @"
SELECT 	SurveyQuestionOptionComponentLangID, 
	SurveyQuestionOptionID, 
	OptionComponentID, 
	LangID, 
	Text, 
	OnClick, 
	TextJapaneseUnicode, 
	OnClickJapaneseUnicode
FROM SurveyQuestionOptionComponentLang
WHERE SurveyQuestionOptionComponentLangID = @SurveyQuestionOptionComponentLangID";
			SurveyQuestionOptionComponentLang surveyQuestionOptionComponentLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyQuestionOptionComponentLangID", id))) {
				if (rs.Read()) {
					surveyQuestionOptionComponentLang = new SurveyQuestionOptionComponentLang {
						SurveyQuestionOptionComponentLangID = GetInt32(rs, 0),
						SurveyQuestionOptionID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						LangID = GetInt32(rs, 3),
						Text = GetString(rs, 4),
						OnClick = GetString(rs, 5),
						TextJapaneseUnicode = GetString(rs, 6),
						OnClickJapaneseUnicode = GetString(rs, 7)
					};
				}
			}
			return surveyQuestionOptionComponentLang;
		}
		
		public override IList<SurveyQuestionOptionComponentLang> FindAll()
		{
			string query = @"
SELECT 	SurveyQuestionOptionComponentLangID, 
	SurveyQuestionOptionID, 
	OptionComponentID, 
	LangID, 
	Text, 
	OnClick, 
	TextJapaneseUnicode, 
	OnClickJapaneseUnicode
FROM SurveyQuestionOptionComponentLang";
			var surveyQuestionOptionComponentLangs = new List<SurveyQuestionOptionComponentLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyQuestionOptionComponentLangs.Add(new SurveyQuestionOptionComponentLang {
						SurveyQuestionOptionComponentLangID = GetInt32(rs, 0),
						SurveyQuestionOptionID = GetInt32(rs, 1),
						OptionComponentID = GetInt32(rs, 2),
						LangID = GetInt32(rs, 3),
						Text = GetString(rs, 4),
						OnClick = GetString(rs, 5),
						TextJapaneseUnicode = GetString(rs, 6),
						OnClickJapaneseUnicode = GetString(rs, 7)
					});
				}
			}
			return surveyQuestionOptionComponentLangs;
		}
	}
}
