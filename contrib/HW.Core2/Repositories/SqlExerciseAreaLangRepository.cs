using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseAreaLangRepository : BaseSqlRepository<ExerciseAreaLang>
	{
		public SqlExerciseAreaLangRepository()
		{
		}
		
		public override void Save(ExerciseAreaLang exerciseAreaLang)
		{
			string query = @"
INSERT INTO ExerciseAreaLang(
	ExerciseAreaLangID, 
	ExerciseAreaID, 
	ExerciseArea, 
	Lang
)
VALUES(
	@ExerciseAreaLangID, 
	@ExerciseAreaID, 
	@ExerciseArea, 
	@Lang
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseAreaLangID", exerciseAreaLang.ExerciseAreaLangID),
				new SqlParameter("@ExerciseAreaID", exerciseAreaLang.ExerciseAreaID),
				new SqlParameter("@ExerciseArea", exerciseAreaLang.ExerciseArea),
				new SqlParameter("@Lang", exerciseAreaLang.Lang)
			);
		}
		
		public override void Update(ExerciseAreaLang exerciseAreaLang, int id)
		{
			string query = @"
UPDATE ExerciseAreaLang SET
	ExerciseAreaLangID = @ExerciseAreaLangID,
	ExerciseAreaID = @ExerciseAreaID,
	ExerciseArea = @ExerciseArea,
	Lang = @Lang
WHERE ExerciseAreaLangID = @ExerciseAreaLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseAreaLangID", exerciseAreaLang.ExerciseAreaLangID),
				new SqlParameter("@ExerciseAreaID", exerciseAreaLang.ExerciseAreaID),
				new SqlParameter("@ExerciseArea", exerciseAreaLang.ExerciseArea),
				new SqlParameter("@Lang", exerciseAreaLang.Lang)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseAreaLang
WHERE ExerciseAreaLangID = @ExerciseAreaLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseAreaLangID", id)
			);
		}
		
		public override ExerciseAreaLang Read(int id)
		{
			string query = @"
SELECT 	ExerciseAreaLangID, 
	ExerciseAreaID, 
	ExerciseArea, 
	Lang
FROM ExerciseAreaLang
WHERE ExerciseAreaLangID = @ExerciseAreaLangID";
			ExerciseAreaLang exerciseAreaLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseAreaLangID", id))) {
				if (rs.Read()) {
					exerciseAreaLang = new ExerciseAreaLang {
						ExerciseAreaLangID = GetInt32(rs, 0),
						ExerciseAreaID = GetInt32(rs, 1),
						ExerciseArea = GetString(rs, 2),
						Lang = GetInt32(rs, 3)
					};
				}
			}
			return exerciseAreaLang;
		}
		
		public override IList<ExerciseAreaLang> FindAll()
		{
			string query = @"
SELECT 	ExerciseAreaLangID, 
	ExerciseAreaID, 
	ExerciseArea, 
	Lang
FROM ExerciseAreaLang";
			var exerciseAreaLangs = new List<ExerciseAreaLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseAreaLangs.Add(new ExerciseAreaLang {
						ExerciseAreaLangID = GetInt32(rs, 0),
						ExerciseAreaID = GetInt32(rs, 1),
						ExerciseArea = GetString(rs, 2),
						Lang = GetInt32(rs, 3)
					});
				}
			}
			return exerciseAreaLangs;
		}
	}
}
