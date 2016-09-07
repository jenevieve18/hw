using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseCategoryLangRepository : BaseSqlRepository<ExerciseCategoryLang>
	{
		public SqlExerciseCategoryLangRepository()
		{
		}
		
		public override void Save(ExerciseCategoryLang exerciseCategoryLang)
		{
			string query = @"
INSERT INTO ExerciseCategoryLang(
	ExerciseCategoryLangID, 
	ExerciseCategoryID, 
	ExerciseCategory, 
	Lang
)
VALUES(
	@ExerciseCategoryLangID, 
	@ExerciseCategoryID, 
	@ExerciseCategory, 
	@Lang
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseCategoryLangID", exerciseCategoryLang.ExerciseCategoryLangID),
				new SqlParameter("@ExerciseCategoryID", exerciseCategoryLang.ExerciseCategoryID),
				new SqlParameter("@ExerciseCategory", exerciseCategoryLang.ExerciseCategory),
				new SqlParameter("@Lang", exerciseCategoryLang.Lang)
			);
		}
		
		public override void Update(ExerciseCategoryLang exerciseCategoryLang, int id)
		{
			string query = @"
UPDATE ExerciseCategoryLang SET
	ExerciseCategoryLangID = @ExerciseCategoryLangID,
	ExerciseCategoryID = @ExerciseCategoryID,
	ExerciseCategory = @ExerciseCategory,
	Lang = @Lang
WHERE ExerciseCategoryLangID = @ExerciseCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseCategoryLangID", exerciseCategoryLang.ExerciseCategoryLangID),
				new SqlParameter("@ExerciseCategoryID", exerciseCategoryLang.ExerciseCategoryID),
				new SqlParameter("@ExerciseCategory", exerciseCategoryLang.ExerciseCategory),
				new SqlParameter("@Lang", exerciseCategoryLang.Lang)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseCategoryLang
WHERE ExerciseCategoryLangID = @ExerciseCategoryLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseCategoryLangID", id)
			);
		}
		
		public override ExerciseCategoryLang Read(int id)
		{
			string query = @"
SELECT 	ExerciseCategoryLangID, 
	ExerciseCategoryID, 
	ExerciseCategory, 
	Lang
FROM ExerciseCategoryLang
WHERE ExerciseCategoryLangID = @ExerciseCategoryLangID";
			ExerciseCategoryLang exerciseCategoryLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseCategoryLangID", id))) {
				if (rs.Read()) {
					exerciseCategoryLang = new ExerciseCategoryLang {
						ExerciseCategoryLangID = GetInt32(rs, 0),
						ExerciseCategoryID = GetInt32(rs, 1),
						ExerciseCategory = GetString(rs, 2),
						Lang = GetInt32(rs, 3)
					};
				}
			}
			return exerciseCategoryLang;
		}
		
		public override IList<ExerciseCategoryLang> FindAll()
		{
			string query = @"
SELECT 	ExerciseCategoryLangID, 
	ExerciseCategoryID, 
	ExerciseCategory, 
	Lang
FROM ExerciseCategoryLang";
			var exerciseCategoryLangs = new List<ExerciseCategoryLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseCategoryLangs.Add(new ExerciseCategoryLang {
						ExerciseCategoryLangID = GetInt32(rs, 0),
						ExerciseCategoryID = GetInt32(rs, 1),
						ExerciseCategory = GetString(rs, 2),
						Lang = GetInt32(rs, 3)
					});
				}
			}
			return exerciseCategoryLangs;
		}
	}
}
