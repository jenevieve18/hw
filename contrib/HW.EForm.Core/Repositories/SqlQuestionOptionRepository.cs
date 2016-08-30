using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionOptionRepository : BaseSqlRepository<QuestionOption>
	{
		public SqlQuestionOptionRepository()
		{
		}
		
		public override void Save(QuestionOption questionOption)
		{
			string query = @"
INSERT INTO QuestionOption(
	QuestionOptionID, 
	QuestionID, 
	OptionID, 
	OptionPlacement, 
	SortOrder, 
	Variablename, 
	Forced, 
	Hide
)
VALUES(
	@QuestionOptionID, 
	@QuestionID, 
	@OptionID, 
	@OptionPlacement, 
	@SortOrder, 
	@Variablename, 
	@Forced, 
	@Hide
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionOptionID", questionOption.QuestionOptionID),
				new SqlParameter("@QuestionID", questionOption.QuestionID),
				new SqlParameter("@OptionID", questionOption.OptionID),
				new SqlParameter("@OptionPlacement", questionOption.OptionPlacement),
				new SqlParameter("@SortOrder", questionOption.SortOrder),
				new SqlParameter("@Variablename", questionOption.Variablename),
				new SqlParameter("@Forced", questionOption.Forced),
				new SqlParameter("@Hide", questionOption.Hide)
			);
		}
		
		public override void Update(QuestionOption questionOption, int id)
		{
			string query = @"
UPDATE QuestionOption SET
	QuestionOptionID = @QuestionOptionID,
	QuestionID = @QuestionID,
	OptionID = @OptionID,
	OptionPlacement = @OptionPlacement,
	SortOrder = @SortOrder,
	Variablename = @Variablename,
	Forced = @Forced,
	Hide = @Hide
WHERE QuestionOptionID = @QuestionOptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionOptionID", questionOption.QuestionOptionID),
				new SqlParameter("@QuestionID", questionOption.QuestionID),
				new SqlParameter("@OptionID", questionOption.OptionID),
				new SqlParameter("@OptionPlacement", questionOption.OptionPlacement),
				new SqlParameter("@SortOrder", questionOption.SortOrder),
				new SqlParameter("@Variablename", questionOption.Variablename),
				new SqlParameter("@Forced", questionOption.Forced),
				new SqlParameter("@Hide", questionOption.Hide)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionOption
WHERE QuestionOptionID = @QuestionOptionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionOptionID", id)
			);
		}
		
		public override QuestionOption Read(int id)
		{
			string query = @"
SELECT 	QuestionOptionID, 
	QuestionID, 
	OptionID, 
	OptionPlacement, 
	SortOrder, 
	Variablename, 
	Forced, 
	Hide
FROM QuestionOption
WHERE QuestionOptionID = @QuestionOptionID";
			QuestionOption questionOption = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionOptionID", id))) {
				if (rs.Read()) {
					questionOption = new QuestionOption {
						QuestionOptionID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						OptionPlacement = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						Variablename = GetString(rs, 5),
						Forced = GetInt32(rs, 6),
						Hide = GetInt32(rs, 7)
					};
				}
			}
			return questionOption;
		}
		
		public override IList<QuestionOption> FindAll()
		{
			string query = @"
SELECT 	QuestionOptionID, 
	QuestionID, 
	OptionID, 
	OptionPlacement, 
	SortOrder, 
	Variablename, 
	Forced, 
	Hide
FROM QuestionOption";
			var questionOptions = new List<QuestionOption>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionOptions.Add(new QuestionOption {
						QuestionOptionID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						OptionPlacement = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						Variablename = GetString(rs, 5),
						Forced = GetInt32(rs, 6),
						Hide = GetInt32(rs, 7)
					});
				}
			}
			return questionOptions;
		}
		
		public IList<QuestionOption> FindByQuestion(int questionID)
		{
			string query = @"
SELECT 	QuestionOptionID, 
	QuestionID, 
	OptionID, 
	OptionPlacement, 
	SortOrder, 
	Variablename, 
	Forced, 
	Hide
FROM QuestionOption
WHERE QuestionID = @QuestionID";
			var questionOptions = new List<QuestionOption>();
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", questionID))) {
				while (rs.Read()) {
					questionOptions.Add(new QuestionOption {
						QuestionOptionID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						OptionPlacement = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						Variablename = GetString(rs, 5),
						Forced = GetInt32(rs, 6),
						Hide = GetInt32(rs, 7)
					});
				}
			}
			return questionOptions;
		}
		
		public IList<QuestionOption> FindByQuestion(int questionID, int[] projectRoundUnitIDs)
		{
			string projectRoundUnitQuery = "";
			var parameters = new List<SqlParameter>();
			if (projectRoundUnitIDs.Length > 0) {
				projectRoundUnitQuery += "AND a.ProjectRoundUnitID IN (";
				int i = 1;
				foreach (var u in projectRoundUnitIDs) {
					projectRoundUnitQuery += "@ProjectRoundUnitID" + u;
					projectRoundUnitQuery += i++ < projectRoundUnitIDs.Length ? ", " : "";
					parameters.Add(new SqlParameter("@ProjectRoundUnitID" + u, u));
				}
				projectRoundUnitQuery += ")";
			}
			string query = string.Format(@"
SELECT 	qo.QuestionOptionID, 
	qo.QuestionID, 
	qo.OptionID, 
	qo.OptionPlacement, 
	qo.SortOrder, 
	qo.Variablename, 
	qo.Forced, 
	qo.Hide
FROM QuestionOption qo
WHERE qo.QuestionID = @QuestionID
AND EXISTS (
	SELECT 1 FROM Answer a
	INNER JOIN AnswerValue av ON av.AnswerID = a.AnswerID
	WHERE av.QuestionID = qo.QuestionID
		AND av.OptionID = qo.OptionID
		{0}
)", projectRoundUnitQuery);
			var questionOptions = new List<QuestionOption>();
			parameters.Add(new SqlParameter("@QuestionID", questionID));
			using (var rs = ExecuteReader(query, parameters.ToArray())) {
				while (rs.Read()) {
					questionOptions.Add(new QuestionOption {
						QuestionOptionID = GetInt32(rs, 0),
						QuestionID = GetInt32(rs, 1),
						OptionID = GetInt32(rs, 2),
						OptionPlacement = GetInt32(rs, 3),
						SortOrder = GetInt32(rs, 4),
						Variablename = GetString(rs, 5),
						Forced = GetInt32(rs, 6),
						Hide = GetInt32(rs, 7)
					});
				}
			}
			return questionOptions;
		}
	}
}
