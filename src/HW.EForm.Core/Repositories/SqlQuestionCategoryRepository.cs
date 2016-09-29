using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlQuestionCategoryRepository : BaseSqlRepository<QuestionCategory>
	{
		public SqlQuestionCategoryRepository()
		{
		}
		
		public override void Save(QuestionCategory questionCategory)
		{
			string query = @"
INSERT INTO QuestionCategory(
	QuestionCategoryID, 
	Internal
)
VALUES(
	@QuestionCategoryID, 
	@Internal
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryID", questionCategory.QuestionCategoryID),
				new SqlParameter("@Internal", questionCategory.Internal)
			);
		}
		
		public override void Update(QuestionCategory questionCategory, int id)
		{
			string query = @"
UPDATE QuestionCategory SET
	QuestionCategoryID = @QuestionCategoryID,
	Internal = @Internal
WHERE QuestionCategoryID = @QuestionCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryID", questionCategory.QuestionCategoryID),
				new SqlParameter("@Internal", questionCategory.Internal)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM QuestionCategory
WHERE QuestionCategoryID = @QuestionCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@QuestionCategoryID", id)
			);
		}
		
		public override QuestionCategory Read(int id)
		{
			string query = @"
SELECT 	QuestionCategoryID, 
	Internal
FROM QuestionCategory
WHERE QuestionCategoryID = @QuestionCategoryID";
			QuestionCategory questionCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@QuestionCategoryID", id))) {
				if (rs.Read()) {
					questionCategory = new QuestionCategory {
						QuestionCategoryID = GetInt32(rs, 0),
						Internal = GetString(rs, 1)
					};
				}
			}
			return questionCategory;
		}
		
		public override IList<QuestionCategory> FindAll()
		{
			string query = @"
SELECT 	QuestionCategoryID, 
	Internal
FROM QuestionCategory";
			var questionCategorys = new List<QuestionCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					questionCategorys.Add(new QuestionCategory {
						QuestionCategoryID = GetInt32(rs, 0),
						Internal = GetString(rs, 1)
					});
				}
			}
			return questionCategorys;
		}
	}
}
