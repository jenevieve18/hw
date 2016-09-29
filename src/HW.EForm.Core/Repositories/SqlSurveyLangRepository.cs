using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyLangRepository : BaseSqlRepository<SurveyLang>
	{
		public SqlSurveyLangRepository()
		{
		}
		
		public override void Save(SurveyLang surveyLang)
		{
			string query = @"
INSERT INTO SurveyLang(
	SurveyLangID, 
	SurveyID, 
	LangID
)
VALUES(
	@SurveyLangID, 
	@SurveyID, 
	@LangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyLangID", surveyLang.SurveyLangID),
				new SqlParameter("@SurveyID", surveyLang.SurveyID),
				new SqlParameter("@LangID", surveyLang.LangID)
			);
		}
		
		public override void Update(SurveyLang surveyLang, int id)
		{
			string query = @"
UPDATE SurveyLang SET
	SurveyLangID = @SurveyLangID,
	SurveyID = @SurveyID,
	LangID = @LangID
WHERE SurveyLangID = @SurveyLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyLangID", surveyLang.SurveyLangID),
				new SqlParameter("@SurveyID", surveyLang.SurveyID),
				new SqlParameter("@LangID", surveyLang.LangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyLang
WHERE SurveyLangID = @SurveyLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyLangID", id)
			);
		}
		
		public override SurveyLang Read(int id)
		{
			string query = @"
SELECT 	SurveyLangID, 
	SurveyID, 
	LangID
FROM SurveyLang
WHERE SurveyLangID = @SurveyLangID";
			SurveyLang surveyLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyLangID", id))) {
				if (rs.Read()) {
					surveyLang = new SurveyLang {
						SurveyLangID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2)
					};
				}
			}
			return surveyLang;
		}
		
		public override IList<SurveyLang> FindAll()
		{
			string query = @"
SELECT 	SurveyLangID, 
	SurveyID, 
	LangID
FROM SurveyLang";
			var surveyLangs = new List<SurveyLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyLangs.Add(new SurveyLang {
						SurveyLangID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2)
					});
				}
			}
			return surveyLangs;
		}
	}
}
