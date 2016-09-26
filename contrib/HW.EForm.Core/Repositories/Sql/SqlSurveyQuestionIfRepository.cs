using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyQuestionIfRepository : BaseSqlRepository<SurveyQuestionIf>
	{
		public SqlSurveyQuestionIfRepository()
		{
		}
		
		public override void Save(SurveyQuestionIf surveyQuestionIf)
		{
			string query = @"
INSERT INTO SurveyQuestionIf(
	SurveyQuestionIfID, 
	SurveyID, 
	SurveyQuestionID, 
	QuestionID, 
	OptionID, 
	OptionComponentID, 
	ConditionAnd
)
VALUES(
	@SurveyQuestionIfID, 
	@SurveyID, 
	@SurveyQuestionID, 
	@QuestionID, 
	@OptionID, 
	@OptionComponentID, 
	@ConditionAnd
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionIfID", surveyQuestionIf.SurveyQuestionIfID),
				new SqlParameter("@SurveyID", surveyQuestionIf.SurveyID),
				new SqlParameter("@SurveyQuestionID", surveyQuestionIf.SurveyQuestionID),
				new SqlParameter("@QuestionID", surveyQuestionIf.QuestionID),
				new SqlParameter("@OptionID", surveyQuestionIf.OptionID),
				new SqlParameter("@OptionComponentID", surveyQuestionIf.OptionComponentID),
				new SqlParameter("@ConditionAnd", surveyQuestionIf.ConditionAnd)
			);
		}
		
		public override void Update(SurveyQuestionIf surveyQuestionIf, int id)
		{
			string query = @"
UPDATE SurveyQuestionIf SET
	SurveyQuestionIfID = @SurveyQuestionIfID,
	SurveyID = @SurveyID,
	SurveyQuestionID = @SurveyQuestionID,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	OptionComponentID = @OptionComponentID,
	ConditionAnd = @ConditionAnd
WHERE SurveyQuestionIfID = @SurveyQuestionIfID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionIfID", surveyQuestionIf.SurveyQuestionIfID),
				new SqlParameter("@SurveyID", surveyQuestionIf.SurveyID),
				new SqlParameter("@SurveyQuestionID", surveyQuestionIf.SurveyQuestionID),
				new SqlParameter("@QuestionID", surveyQuestionIf.QuestionID),
				new SqlParameter("@OptionID", surveyQuestionIf.OptionID),
				new SqlParameter("@OptionComponentID", surveyQuestionIf.OptionComponentID),
				new SqlParameter("@ConditionAnd", surveyQuestionIf.ConditionAnd)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SurveyQuestionIf
WHERE SurveyQuestionIfID = @SurveyQuestionIfID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyQuestionIfID", id)
			);
		}
		
		public override SurveyQuestionIf Read(int id)
		{
			string query = @"
SELECT 	SurveyQuestionIfID, 
	SurveyID, 
	SurveyQuestionID, 
	QuestionID, 
	OptionID, 
	OptionComponentID, 
	ConditionAnd
FROM SurveyQuestionIf
WHERE SurveyQuestionIfID = @SurveyQuestionIfID";
			SurveyQuestionIf surveyQuestionIf = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyQuestionIfID", id))) {
				if (rs.Read()) {
					surveyQuestionIf = new SurveyQuestionIf {
						SurveyQuestionIfID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						SurveyQuestionID = GetInt32(rs, 2),
						QuestionID = GetInt32(rs, 3),
						OptionID = GetInt32(rs, 4),
						OptionComponentID = GetInt32(rs, 5),
						ConditionAnd = GetInt32(rs, 6)
					};
				}
			}
			return surveyQuestionIf;
		}
		
		public override IList<SurveyQuestionIf> FindAll()
		{
			string query = @"
SELECT 	SurveyQuestionIfID, 
	SurveyID, 
	SurveyQuestionID, 
	QuestionID, 
	OptionID, 
	OptionComponentID, 
	ConditionAnd
FROM SurveyQuestionIf";
			var surveyQuestionIfs = new List<SurveyQuestionIf>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveyQuestionIfs.Add(new SurveyQuestionIf {
						SurveyQuestionIfID = GetInt32(rs, 0),
						SurveyID = GetInt32(rs, 1),
						SurveyQuestionID = GetInt32(rs, 2),
						QuestionID = GetInt32(rs, 3),
						OptionID = GetInt32(rs, 4),
						OptionComponentID = GetInt32(rs, 5),
						ConditionAnd = GetInt32(rs, 6)
					});
				}
			}
			return surveyQuestionIfs;
		}
	}
}
