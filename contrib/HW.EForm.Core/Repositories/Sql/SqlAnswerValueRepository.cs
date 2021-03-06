using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.EForm.Core.Models;

namespace HW.EForm.Core.Repositories
{
	public class SqlAnswerValueRepository : BaseSqlRepository<AnswerValue>, IAnswerValueRepository
	{
		public SqlAnswerValueRepository()
		{
		}
		
		public override void Save(AnswerValue answerValue)
		{
			string query = @"
INSERT INTO AnswerValue(
	AnswerValue,
	AnswerID,
	QuestionID,
	OptionID,
	ValueInt,
	ValueDecimal,
	ValueDateTime,
	CreatedDateTime,
	CreatedSessionID,
	DeletedSessionID,
	ValueText,
	ValueTextJapaneseUnicode
)
VALUES(
	@AnswerValue,
	@AnswerID,
	@QuestionID,
	@OptionID,
	@ValueInt,
	@ValueDecimal,
	@ValueDateTime,
	@CreatedDateTime,
	@CreatedSessionID,
	@DeletedSessionID,
	@ValueText,
	@ValueTextJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AnswerValue", answerValue.Value),
				new SqlParameter("@AnswerID", answerValue.AnswerID),
				new SqlParameter("@QuestionID", answerValue.QuestionID),
				new SqlParameter("@OptionID", answerValue.OptionID),
				new SqlParameter("@ValueInt", answerValue.ValueInt),
				new SqlParameter("@ValueDecimal", answerValue.ValueDecimal),
				new SqlParameter("@ValueDateTime", answerValue.ValueDateTime),
				new SqlParameter("@CreatedDateTime", answerValue.CreatedDateTime),
				new SqlParameter("@CreatedSessionID", answerValue.CreatedSessionID),
				new SqlParameter("@DeletedSessionID", answerValue.DeletedSessionID),
				new SqlParameter("@ValueText", answerValue.ValueText),
				new SqlParameter("@ValueTextJapaneseUnicode", answerValue.ValueTextJapaneseUnicode)
			);
		}
		
		public override void Update(AnswerValue answerValue, int id)
		{
			string query = @"
UPDATE AnswerValue SET
	AnswerValue = @AnswerValue,
	AnswerID = @AnswerID,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	ValueInt = @ValueInt,
	ValueDecimal = @ValueDecimal,
	ValueDateTime = @ValueDateTime,
	CreatedDateTime = @CreatedDateTime,
	CreatedSessionID = @CreatedSessionID,
	DeletedSessionID = @DeletedSessionID,
	ValueText = @ValueText,
	ValueTextJapaneseUnicode = @ValueTextJapaneseUnicode
WHERE AnswerValueID = @AnswerValueID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AnswerValue", answerValue.Value),
				new SqlParameter("@AnswerID", answerValue.AnswerID),
				new SqlParameter("@QuestionID", answerValue.QuestionID),
				new SqlParameter("@OptionID", answerValue.OptionID),
				new SqlParameter("@ValueInt", answerValue.ValueInt),
				new SqlParameter("@ValueDecimal", answerValue.ValueDecimal),
				new SqlParameter("@ValueDateTime", answerValue.ValueDateTime),
				new SqlParameter("@CreatedDateTime", answerValue.CreatedDateTime),
				new SqlParameter("@CreatedSessionID", answerValue.CreatedSessionID),
				new SqlParameter("@DeletedSessionID", answerValue.DeletedSessionID),
				new SqlParameter("@ValueText", answerValue.ValueText),
				new SqlParameter("@ValueTextJapaneseUnicode", answerValue.ValueTextJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM AnswerValue
WHERE AnswerValueID = @AnswerValueID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AnswerValueID", id)
			);
		}
		
		public override AnswerValue Read(int id)
		{
			string query = @"
SELECT 	AnswerValue,
	AnswerID,
	QuestionID,
	OptionID,
	ValueInt,
	ValueDecimal,
	ValueDateTime,
	CreatedDateTime,
	CreatedSessionID,
	DeletedSessionID,
	ValueText,
	ValueTextJapaneseUnicode
FROM AnswerValue
WHERE AnswerValueID = @AnswerValueID";
			AnswerValue answerValue = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@AnswerValueID", id))) {
				if (rs.Read()) {
					answerValue = new AnswerValue {
						Value = GetInt32(rs, 0),
						AnswerID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2),
						OptionID = GetInt32(rs, 3),
						ValueInt = GetInt32(rs, 4),
						ValueDecimal = GetDecimal(rs, 5),
						ValueDateTime = GetDateTime(rs, 6),
						CreatedDateTime = GetDateTime(rs, 7),
						CreatedSessionID = GetInt32(rs, 8),
						DeletedSessionID = GetInt32(rs, 9),
						ValueText = GetString(rs, 10),
						ValueTextJapaneseUnicode = GetString(rs, 11)
					};
				}
			}
			return answerValue;
		}
		
		public override IList<AnswerValue> FindAll()
		{
			string query = @"
SELECT 	AnswerValue,
	AnswerID,
	QuestionID,
	OptionID,
	ValueInt,
	ValueDecimal,
	ValueDateTime,
	CreatedDateTime,
	CreatedSessionID,
	DeletedSessionID,
	ValueText,
	ValueTextJapaneseUnicode
FROM AnswerValue";
			var answerValues = new List<AnswerValue>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					answerValues.Add(
						new AnswerValue {
							Value = GetInt32(rs, 0),
							AnswerID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							OptionID = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							ValueDecimal = GetDecimal(rs, 5),
							ValueDateTime = GetDateTime(rs, 6),
							CreatedDateTime = GetDateTime(rs, 7),
							CreatedSessionID = GetInt32(rs, 8),
							DeletedSessionID = GetInt32(rs, 9),
							ValueText = GetString(rs, 10),
							ValueTextJapaneseUnicode = GetString(rs, 11)
						}
					);
				}
			}
			return answerValues;
		}
		
		public IList<AnswerValue> FindByAnswer(int answerID)
		{
			string query = @"
SELECT 	AnswerValue,
	AnswerID,
	QuestionID,
	OptionID,
	ValueInt,
	ValueDecimal,
	ValueDateTime,
	CreatedDateTime,
	CreatedSessionID,
	DeletedSessionID,
	ValueText,
	ValueTextJapaneseUnicode
FROM AnswerValue
WHERE AnswerID = @AnswerID";
			var answerValues = new List<AnswerValue>();
			using (var rs = ExecuteReader(query, new SqlParameter("@AnswerID", answerID))) {
				while (rs.Read()) {
					answerValues.Add(
						new AnswerValue {
							Value = GetInt32(rs, 0),
							AnswerID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							OptionID = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							ValueDecimal = GetDecimal(rs, 5),
							ValueDateTime = GetDateTime(rs, 6),
							CreatedDateTime = GetDateTime(rs, 7),
							CreatedSessionID = GetInt32(rs, 8),
							DeletedSessionID = GetInt32(rs, 9),
							ValueText = GetString(rs, 10),
							ValueTextJapaneseUnicode = GetString(rs, 11)
						}
					);
				}
			}
			return answerValues;
		}
		
		public IList<AnswerValue> FindByQuestion(int questionID)
		{
			string query = @"
SELECT 	AnswerValue,
	AnswerID,
	QuestionID,
	OptionID,
	ValueInt,
	ValueDecimal,
	ValueDateTime,
	CreatedDateTime,
	CreatedSessionID,
	DeletedSessionID,
	ValueText,
	ValueTextJapaneseUnicode
FROM AnswerValue
WHERE QuestionID = @QuestionID";
			var answerValues = new List<AnswerValue>();
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID))) {
				while (rs.Read()) {
					answerValues.Add(
						new AnswerValue {
							Value = GetInt32(rs, 0),
							AnswerID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							OptionID = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							ValueDecimal = GetDecimal(rs, 5),
							ValueDateTime = GetDateTime(rs, 6),
							CreatedDateTime = GetDateTime(rs, 7),
							CreatedSessionID = GetInt32(rs, 8),
							DeletedSessionID = GetInt32(rs, 9),
							ValueText = GetString(rs, 10),
							ValueTextJapaneseUnicode = GetString(rs, 11)
						}
					);
				}
			}
			return answerValues;
		}
		
		public IList<AnswerValue> FindByQuestionOptionsAndUnit(int questionID, IList<QuestionOption> options, int projectRoundID, int projectRoundUnitID)
		{
			string optionQuery = "";
			List<SqlParameter> parameters = new List<SqlParameter>();
			if (options.Count > 0) {
				optionQuery = "AND av.OptionID IN (";
				int i = 1;
				foreach (var o in options) {
					optionQuery += "@OptionID" + o.OptionID;
					optionQuery += i++ < options.Count ? ", " : "";
					parameters.Add(new SqlParameter("@OptionID" + o.OptionID, o.OptionID));
				}
				optionQuery += ")";
			}
			
			string query = string.Format(
				@"
SELECT 	av.AnswerValue,
	av.AnswerID,
	av.QuestionID,
	av.OptionID,
	av.ValueInt,
	av.ValueDecimal,
	av.ValueDateTime,
	av.CreatedDateTime,
	av.CreatedSessionID,
	av.DeletedSessionID,
	av.ValueText,
	av.ValueTextJapaneseUnicode
FROM AnswerValue av
INNER JOIN Answer a ON a.AnswerID = av.AnswerID
	AND a.ProjectRoundID = @ProjectRoundID
	AND a.ProjectRoundUnitID = @ProjectRoundUnitID
WHERE av.QuestionID = @QuestionID
{0}",
				optionQuery
			);
			var answerValues = new List<AnswerValue>();
			parameters.Add(new SqlParameter("@QuestionID", questionID));
			parameters.Add(new SqlParameter("@ProjectRoundID", projectRoundID));
			parameters.Add(new SqlParameter("@ProjectRoundUnitID", projectRoundUnitID));
			using (var rs = ExecuteReader(
				query,
				parameters.ToArray())) {
				while (rs.Read()) {
					answerValues.Add(
						new AnswerValue {
							Value = GetInt32(rs, 0),
							AnswerID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							OptionID = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							ValueDecimal = GetDecimal(rs, 5),
							ValueDateTime = GetDateTime(rs, 6),
							CreatedDateTime = GetDateTime(rs, 7),
							CreatedSessionID = GetInt32(rs, 8),
							DeletedSessionID = GetInt32(rs, 9),
							ValueText = GetString(rs, 10),
							ValueTextJapaneseUnicode = GetString(rs, 11)
						}
					);
				}
			}
			return answerValues;
		}
		
//		public IList<AnswerValue> FindByQuestionOptionsAndUnits(int questionID, IList<QuestionOption> options, int projectRoundID, IList<ProjectRoundUnit> projectRoundUnits)
		public IList<AnswerValue> FindByQuestionOptionsAndUnits(int questionID, int[] optionIDs, int projectRoundID, int[] projectRoundUnitIDs)
		{
			string optionQuery = "";
			List<SqlParameter> parameters = new List<SqlParameter>();
//			if (options.Count > 0) {
			if (optionIDs.Length > 0) {
				optionQuery += "AND av.OptionID IN (";
				int i = 1;
//				foreach (var o in options) {
//					optionQuery += "@OptionID" + o.OptionID;
//					optionQuery += i++ < options.Count ? ", " : "";
//					parameters.Add(new SqlParameter("@OptionID" + o.OptionID, o.OptionID));
				foreach (var optionID in optionIDs) {
					optionQuery += "@OptionID" + optionID;
					optionQuery += i++ < optionIDs.Length ? ", " : "";
					parameters.Add(new SqlParameter("@OptionID" + optionID, optionID));
				}
				optionQuery += ")";
			}
			
			string projectRoundUnitQuery = "";
//			if (projectRoundUnits.Count > 0) {
			if (projectRoundUnitIDs.Length > 0) {
				projectRoundUnitQuery += "AND a.ProjectRoundUnitID IN (";
				int i = 1;
//				foreach (var pru in projectRoundUnits) {
//					projectRoundUnitQuery += "@ProjectRoundUnitID" + pru.ProjectRoundUnitID;
//					projectRoundUnitQuery += i++ < projectRoundUnits.Count ? ", " : "";
//					parameters.Add(new SqlParameter("@ProjectRoundUnitID" + pru.ProjectRoundUnitID, pru.ProjectRoundUnitID));
				foreach (var pru in projectRoundUnitIDs) {
					projectRoundUnitQuery += "@ProjectRoundUnitID" + pru;
					projectRoundUnitQuery += i++ < projectRoundUnitIDs.Length ? ", " : "";
					parameters.Add(new SqlParameter("@ProjectRoundUnitID" + pru, pru));
				}
				projectRoundUnitQuery += ")";
			}
			
			string query = string.Format(
				@"
SELECT 	av.AnswerValue,
	av.AnswerID,
	av.QuestionID,
	av.OptionID,
	av.ValueInt,
	av.ValueDecimal,
	av.ValueDateTime,
	av.CreatedDateTime,
	av.CreatedSessionID,
	av.DeletedSessionID,
	av.ValueText,
	av.ValueTextJapaneseUnicode
FROM AnswerValue av
INNER JOIN Answer a ON a.AnswerID = av.AnswerID
	AND a.ProjectRoundID = @ProjectRoundID
	{1}
WHERE av.QuestionID = @QuestionID
{0}",
				optionQuery,
				projectRoundUnitQuery
			);
			var answerValues = new List<AnswerValue>();
			parameters.Add(new SqlParameter("@QuestionID", questionID));
			parameters.Add(new SqlParameter("@ProjectRoundID", projectRoundID));
			using (var rs = ExecuteReader(
				query,
				parameters.ToArray())) {
				while (rs.Read()) {
					answerValues.Add(
						new AnswerValue {
							Value = GetInt32(rs, 0),
							AnswerID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							OptionID = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							ValueDecimal = GetDecimal(rs, 5),
							ValueDateTime = GetDateTime(rs, 6),
							CreatedDateTime = GetDateTime(rs, 7),
							CreatedSessionID = GetInt32(rs, 8),
							DeletedSessionID = GetInt32(rs, 9),
							ValueText = GetString(rs, 10),
							ValueTextJapaneseUnicode = GetString(rs, 11)
						}
					);
				}
			}
			return answerValues;
		}
		
		public IList<AnswerValue> FindByQuestionOptionAndUnit(int questionID, int optionID, int projectRoundID, int projectRoundUnitID)
		{
			string query = @"
SELECT 	av.AnswerValue,
	av.AnswerID,
	av.QuestionID,
	av.OptionID,
	av.ValueInt,
	av.ValueDecimal,
	av.ValueDateTime,
	av.CreatedDateTime,
	av.CreatedSessionID,
	av.DeletedSessionID,
	av.ValueText,
	av.ValueTextJapaneseUnicode
FROM AnswerValue av
INNER JOIN Answer a ON a.AnswerID = av.AnswerID
	AND a.ProjectRoundID = @ProjectRoundID
	AND a.ProjectRoundUnitID = @ProjectRoundUnitID
WHERE av.QuestionID = @QuestionID
AND av.OptionID = @OptionID";
			var answerValues = new List<AnswerValue>();
			using (var rs = ExecuteReader(
				query,
				new SqlParameter("@QuestionID", questionID),
				new SqlParameter("@ProjectRoundID", projectRoundID),
				new SqlParameter("@ProjectRoundUnitID", projectRoundUnitID),
				new SqlParameter("@OptionID", optionID))) {
				while (rs.Read()) {
					answerValues.Add(
						new AnswerValue {
							Value = GetInt32(rs, 0),
							AnswerID = GetInt32(rs, 1),
							QuestionID = GetInt32(rs, 2),
							OptionID = GetInt32(rs, 3),
							ValueInt = GetInt32(rs, 4),
							ValueDecimal = GetDecimal(rs, 5),
							ValueDateTime = GetDateTime(rs, 6),
							CreatedDateTime = GetDateTime(rs, 7),
							CreatedSessionID = GetInt32(rs, 8),
							DeletedSessionID = GetInt32(rs, 9),
							ValueText = GetString(rs, 10),
							ValueTextJapaneseUnicode = GetString(rs, 11)
						}
					);
				}
			}
			return answerValues;
		}
	}
}
