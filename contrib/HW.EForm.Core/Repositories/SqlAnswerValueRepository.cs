using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlAnswerValueRepository : BaseSqlRepository<AnswerValue>
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
					answerValues.Add(new AnswerValue {
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
					});
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
					answerValues.Add(new AnswerValue {
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
					});
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
					answerValues.Add(new AnswerValue {
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
					});
				}
			}
			return answerValues;
		}
	}
}
