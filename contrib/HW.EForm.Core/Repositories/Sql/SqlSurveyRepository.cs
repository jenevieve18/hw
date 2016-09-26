using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlSurveyRepository : BaseSqlRepository<Survey>
	{
		public SqlSurveyRepository()
		{
		}
		
		public override void Save(Survey survey)
		{
			string query = @"
INSERT INTO Survey(
	SurveyID, 
	Internal, 
	SurveyKey, 
	Copyright, 
	FlipFlopBg, 
	NoTime, 
	ClearQuestions, 
	TwoColumns
)
VALUES(
	@SurveyID, 
	@Internal, 
	@SurveyKey, 
	@Copyright, 
	@FlipFlopBg, 
	@NoTime, 
	@ClearQuestions, 
	@TwoColumns
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyID", survey.SurveyID),
				new SqlParameter("@Internal", survey.Internal),
				new SqlParameter("@SurveyKey", survey.SurveyKey),
				new SqlParameter("@Copyright", survey.Copyright),
				new SqlParameter("@FlipFlopBg", survey.FlipFlopBg),
				new SqlParameter("@NoTime", survey.NoTime),
				new SqlParameter("@ClearQuestions", survey.ClearQuestions),
				new SqlParameter("@TwoColumns", survey.TwoColumns)
			);
		}
		
		public override void Update(Survey survey, int id)
		{
			string query = @"
UPDATE Survey SET
	SurveyID = @SurveyID,
	Internal = @Internal,
	SurveyKey = @SurveyKey,
	Copyright = @Copyright,
	FlipFlopBg = @FlipFlopBg,
	NoTime = @NoTime,
	ClearQuestions = @ClearQuestions,
	TwoColumns = @TwoColumns
WHERE SurveyID = @SurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyID", survey.SurveyID),
				new SqlParameter("@Internal", survey.Internal),
				new SqlParameter("@SurveyKey", survey.SurveyKey),
				new SqlParameter("@Copyright", survey.Copyright),
				new SqlParameter("@FlipFlopBg", survey.FlipFlopBg),
				new SqlParameter("@NoTime", survey.NoTime),
				new SqlParameter("@ClearQuestions", survey.ClearQuestions),
				new SqlParameter("@TwoColumns", survey.TwoColumns)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Survey
WHERE SurveyID = @SurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SurveyID", id)
			);
		}
		
		public override Survey Read(int id)
		{
			string query = @"
SELECT 	SurveyID, 
	Internal, 
	SurveyKey, 
	Copyright, 
	FlipFlopBg, 
	NoTime, 
	ClearQuestions, 
	TwoColumns
FROM Survey
WHERE SurveyID = @SurveyID";
			Survey survey = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SurveyID", id))) {
				if (rs.Read()) {
					survey = new Survey {
						SurveyID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						SurveyKey = GetGuid(rs, 2),
						Copyright = GetString(rs, 3),
						FlipFlopBg = GetInt32(rs, 4),
						NoTime = GetInt32(rs, 5),
						ClearQuestions = GetInt32(rs, 6),
						TwoColumns = GetInt32(rs, 7)
					};
				}
			}
			return survey;
		}
		
		public override IList<Survey> FindAll()
		{
			string query = @"
SELECT 	SurveyID, 
	Internal, 
	SurveyKey, 
	Copyright, 
	FlipFlopBg, 
	NoTime, 
	ClearQuestions, 
	TwoColumns
FROM Survey";
			var surveys = new List<Survey>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					surveys.Add(new Survey {
						SurveyID = GetInt32(rs, 0),
						Internal = GetString(rs, 1),
						SurveyKey = GetGuid(rs, 2),
						Copyright = GetString(rs, 3),
						FlipFlopBg = GetInt32(rs, 4),
						NoTime = GetInt32(rs, 5),
						ClearQuestions = GetInt32(rs, 6),
						TwoColumns = GetInt32(rs, 7)
					});
				}
			}
			return surveys;
		}
	}
}
