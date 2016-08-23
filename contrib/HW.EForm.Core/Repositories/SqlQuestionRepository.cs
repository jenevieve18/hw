using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionRepository : BaseSqlRepository<Question>
	{
		public SqlQuestionRepository()
		{
		}
		
		public override void Save(Question question)
		{
			string query = @"
INSERT INTO Question(
	QuestionID, 
	VariableName, 
	OptionsPlacement, 
	FontFamily, 
	FontSize, 
	FontDecoration, 
	FontColor, 
	Underlined, 
	QuestionContainerID, 
	Internal, 
	Box
)
VALUES(
	@QuestionID, 
	@VariableName, 
	@OptionsPlacement, 
	@FontFamily, 
	@FontSize, 
	@FontDecoration, 
	@FontColor, 
	@Underlined, 
	@QuestionContainerID, 
	@Internal, 
	@Box
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionID", question.QuestionID),
				new SqlParameter("@VariableName", question.VariableName),
				new SqlParameter("@OptionsPlacement", question.OptionsPlacement),
				new SqlParameter("@FontFamily", question.FontFamily),
				new SqlParameter("@FontSize", question.FontSize),
				new SqlParameter("@FontDecoration", question.FontDecoration),
				new SqlParameter("@FontColor", question.FontColor),
				new SqlParameter("@Underlined", question.Underlined),
				new SqlParameter("@QuestionContainerID", question.QuestionContainerID),
				new SqlParameter("@Internal", question.Internal),
				new SqlParameter("@Box", question.Box)
			);
		}
		
		public override void Update(Question question, int id)
		{
			string query = @"
UPDATE Question SET
	QuestionID = @QuestionID,
	VariableName = @VariableName,
	OptionsPlacement = @OptionsPlacement,
	FontFamily = @FontFamily,
	FontSize = @FontSize,
	FontDecoration = @FontDecoration,
	FontColor = @FontColor,
	Underlined = @Underlined,
	QuestionContainerID = @QuestionContainerID,
	Internal = @Internal,
	Box = @Box
WHERE QuestionID = @QuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionID", question.QuestionID),
				new SqlParameter("@VariableName", question.VariableName),
				new SqlParameter("@OptionsPlacement", question.OptionsPlacement),
				new SqlParameter("@FontFamily", question.FontFamily),
				new SqlParameter("@FontSize", question.FontSize),
				new SqlParameter("@FontDecoration", question.FontDecoration),
				new SqlParameter("@FontColor", question.FontColor),
				new SqlParameter("@Underlined", question.Underlined),
				new SqlParameter("@QuestionContainerID", question.QuestionContainerID),
				new SqlParameter("@Internal", question.Internal),
				new SqlParameter("@Box", question.Box)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Question
WHERE QuestionID = @QuestionID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionID", id)
			);
		}
		
		public override Question Read(int id)
		{
			string query = @"
SELECT 	QuestionID, 
	VariableName, 
	OptionsPlacement, 
	FontFamily, 
	FontSize, 
	FontDecoration, 
	FontColor, 
	Underlined, 
	QuestionContainerID, 
	Internal, 
	Box
FROM Question
WHERE QuestionID = @QuestionID";
			Question question = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionID", id))) {
				if (rs.Read()) {
					question = new Question {
						QuestionID = GetInt32(rs, 0),
						VariableName = GetString(rs, 1),
						OptionsPlacement = GetInt32(rs, 2),
						FontFamily = GetInt32(rs, 3),
						FontSize = GetInt32(rs, 4),
						FontDecoration = GetInt32(rs, 5),
						FontColor = GetString(rs, 6),
						Underlined = GetInt32(rs, 7),
						QuestionContainerID = GetInt32(rs, 8),
						Internal = GetString(rs, 9),
						Box = GetInt32(rs, 10)
					};
				}
			}
			return question;
		}
		
		public override IList<Question> FindAll()
		{
			string query = @"
SELECT 	QuestionID, 
	VariableName, 
	OptionsPlacement, 
	FontFamily, 
	FontSize, 
	FontDecoration, 
	FontColor, 
	Underlined, 
	QuestionContainerID, 
	Internal, 
	Box
FROM Question";
			var questions = new List<Question>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questions.Add(new Question {
						QuestionID = GetInt32(rs, 0),
						VariableName = GetString(rs, 1),
						OptionsPlacement = GetInt32(rs, 2),
						FontFamily = GetInt32(rs, 3),
						FontSize = GetInt32(rs, 4),
						FontDecoration = GetInt32(rs, 5),
						FontColor = GetString(rs, 6),
						Underlined = GetInt32(rs, 7),
						QuestionContainerID = GetInt32(rs, 8),
						Internal = GetString(rs, 9),
						Box = GetInt32(rs, 10)
					});
				}
			}
			return questions;
		}
	}
}
