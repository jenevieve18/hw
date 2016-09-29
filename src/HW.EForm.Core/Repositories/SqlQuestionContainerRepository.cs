using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionContainerRepository : BaseSqlRepository<QuestionContainer>
	{
		public SqlQuestionContainerRepository()
		{
		}
		
		public override void Save(QuestionContainer questionContainer)
		{
			string query = @"
INSERT INTO QuestionContainer(
	QuestionContainerID, 
	QuestionContainer
)
VALUES(
	@QuestionContainerID, 
	@QuestionContainer
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionContainerID", questionContainer.QuestionContainerID),
				new SqlParameter("@QuestionContainer", questionContainer.QuestionContainerText)
			);
		}
		
		public override void Update(QuestionContainer questionContainer, int id)
		{
			string query = @"
UPDATE QuestionContainer SET
	QuestionContainerID = @QuestionContainerID,
	QuestionContainer = @QuestionContainer
WHERE QuestionContainerID = @QuestionContainerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionContainerID", questionContainer.QuestionContainerID),
				new SqlParameter("@QuestionContainer", questionContainer.QuestionContainerText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionContainer
WHERE QuestionContainerID = @QuestionContainerID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionContainerID", id)
			);
		}
		
		public override QuestionContainer Read(int id)
		{
			string query = @"
SELECT 	QuestionContainerID, 
	QuestionContainer
FROM QuestionContainer
WHERE QuestionContainerID = @QuestionContainerID";
			QuestionContainer questionContainer = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionContainerID", id))) {
				if (rs.Read()) {
					questionContainer = new QuestionContainer {
						QuestionContainerID = GetInt32(rs, 0),
						QuestionContainerText = GetString(rs, 1)
					};
				}
			}
			return questionContainer;
		}
		
		public override IList<QuestionContainer> FindAll()
		{
			string query = @"
SELECT 	QuestionContainerID, 
	QuestionContainer
FROM QuestionContainer";
			var questionContainers = new List<QuestionContainer>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionContainers.Add(new QuestionContainer {
						QuestionContainerID = GetInt32(rs, 0),
						QuestionContainerText = GetString(rs, 1)
					});
				}
			}
			return questionContainers;
		}
	}
}
