using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseCategoryRepository : BaseSqlRepository<ExerciseCategory>
	{
		public SqlExerciseCategoryRepository()
		{
		}
		
		public override void Save(ExerciseCategory exerciseCategory)
		{
			string query = @"
INSERT INTO ExerciseCategory(
	ExerciseCategoryID, 
	ExerciseCategorySortOrder
)
VALUES(
	@ExerciseCategoryID, 
	@ExerciseCategorySortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseCategoryID", exerciseCategory.ExerciseCategoryID),
				new SqlParameter("@ExerciseCategorySortOrder", exerciseCategory.ExerciseCategorySortOrder)
			);
		}
		
		public override void Update(ExerciseCategory exerciseCategory, int id)
		{
			string query = @"
UPDATE ExerciseCategory SET
	ExerciseCategoryID = @ExerciseCategoryID,
	ExerciseCategorySortOrder = @ExerciseCategorySortOrder
WHERE ExerciseCategoryID = @ExerciseCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseCategoryID", exerciseCategory.ExerciseCategoryID),
				new SqlParameter("@ExerciseCategorySortOrder", exerciseCategory.ExerciseCategorySortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseCategory
WHERE ExerciseCategoryID = @ExerciseCategoryID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseCategoryID", id)
			);
		}
		
		public override ExerciseCategory Read(int id)
		{
			string query = @"
SELECT 	ExerciseCategoryID, 
	ExerciseCategorySortOrder
FROM ExerciseCategory
WHERE ExerciseCategoryID = @ExerciseCategoryID";
			ExerciseCategory exerciseCategory = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseCategoryID", id))) {
				if (rs.Read()) {
					exerciseCategory = new ExerciseCategory {
						ExerciseCategoryID = GetInt32(rs, 0),
						ExerciseCategorySortOrder = GetInt32(rs, 1)
					};
				}
			}
			return exerciseCategory;
		}
		
		public override IList<ExerciseCategory> FindAll()
		{
			string query = @"
SELECT 	ExerciseCategoryID, 
	ExerciseCategorySortOrder
FROM ExerciseCategory";
			var exerciseCategorys = new List<ExerciseCategory>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseCategorys.Add(new ExerciseCategory {
						ExerciseCategoryID = GetInt32(rs, 0),
						ExerciseCategorySortOrder = GetInt32(rs, 1)
					});
				}
			}
			return exerciseCategorys;
		}
	}
}
