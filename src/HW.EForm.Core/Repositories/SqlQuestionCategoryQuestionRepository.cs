using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionCategoryQuestionRepository : BaseSqlRepository<QuestionCategoryQuestion>
	{
		public SqlQuestionCategoryQuestionRepository()
		{
		}
		
		public override void Save(QuestionCategoryQuestion questionCategoryQuestion)
		{
			string query = @"
INSERT INTO QuestionCategoryQuestion(
	QuestionCategoryQuestionID, 
	QuestionCategoryID, 
	QuestionID
)
VALUES(
	@QuestionCategoryQuestionID, 
	@QuestionCategoryID, 
	@QuestionID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryQuestionID", questionCategoryQuestion.QuestionCategoryQuestionID),
				new SqlParameter("@QuestionCategoryID", questionCategoryQuestion.QuestionCategoryID),
				new SqlParameter("@QuestionID", questionCategoryQuestion.QuestionID)
			);
		}
		
		public override void Update(QuestionCategoryQuestion questionCategoryQuestion, int id)
		{
			string query = @"
UPDATE QuestionCategoryQuestion SET
	QuestionCategoryQuestionID = @QuestionCategoryQuestionID,
	QuestionCategoryID = @QuestionCategoryID,
	QuestionID = @QuestionID
WHERE QuestionCategoryQuestionID = @QuestionCategoryQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryQuestionID", questionCategoryQuestion.QuestionCategoryQuestionID),
				new SqlParameter("@QuestionCategoryID", questionCategoryQuestion.QuestionCategoryID),
				new SqlParameter("@QuestionID", questionCategoryQuestion.QuestionID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionCategoryQuestion
WHERE QuestionCategoryQuestionID = @QuestionCategoryQuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryQuestionID", id)
			);
		}
		
		public override QuestionCategoryQuestion Read(int id)
		{
			string query = @"
SELECT 	QuestionCategoryQuestionID, 
	QuestionCategoryID, 
	QuestionID
FROM QuestionCategoryQuestion
WHERE QuestionCategoryQuestionID = @QuestionCategoryQuestionID";
			QuestionCategoryQuestion questionCategoryQuestion = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionCategoryQuestionID", id))) {
				if (rs.Read()) {
					questionCategoryQuestion = new QuestionCategoryQuestion {
						QuestionCategoryQuestionID = GetInt32(rs, 0),
						QuestionCategoryID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2)
					};
				}
			}
			return questionCategoryQuestion;
		}
		
		public override IList<QuestionCategoryQuestion> FindAll()
		{
			string query = @"
SELECT 	QuestionCategoryQuestionID, 
	QuestionCategoryID, 
	QuestionID
FROM QuestionCategoryQuestion";
			var questionCategoryQuestions = new List<QuestionCategoryQuestion>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionCategoryQuestions.Add(new QuestionCategoryQuestion {
						QuestionCategoryQuestionID = GetInt32(rs, 0),
						QuestionCategoryID = GetInt32(rs, 1),
						QuestionID = GetInt32(rs, 2)
					});
				}
			}
			return questionCategoryQuestions;
		}
	}
}
