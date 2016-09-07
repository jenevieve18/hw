using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseLangRepository : BaseSqlRepository<ExerciseLang>
	{
		public SqlExerciseLangRepository()
		{
		}
		
		public override void Save(ExerciseLang exerciseLang)
		{
			string query = @"
INSERT INTO ExerciseLang(
	ExerciseLangID, 
	ExerciseID, 
	Exercise, 
	ExerciseTime, 
	ExerciseTeaser, 
	Lang, 
	New, 
	ExerciseContent
)
VALUES(
	@ExerciseLangID, 
	@ExerciseID, 
	@Exercise, 
	@ExerciseTime, 
	@ExerciseTeaser, 
	@Lang, 
	@New, 
	@ExerciseContent
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseLangID", exerciseLang.ExerciseLangID),
				new SqlParameter("@ExerciseID", exerciseLang.ExerciseID),
				new SqlParameter("@Exercise", exerciseLang.Exercise),
				new SqlParameter("@ExerciseTime", exerciseLang.ExerciseTime),
				new SqlParameter("@ExerciseTeaser", exerciseLang.ExerciseTeaser),
				new SqlParameter("@Lang", exerciseLang.Lang),
				new SqlParameter("@New", exerciseLang.New),
				new SqlParameter("@ExerciseContent", exerciseLang.ExerciseContent)
			);
		}
		
		public override void Update(ExerciseLang exerciseLang, int id)
		{
			string query = @"
UPDATE ExerciseLang SET
	ExerciseLangID = @ExerciseLangID,
	ExerciseID = @ExerciseID,
	Exercise = @Exercise,
	ExerciseTime = @ExerciseTime,
	ExerciseTeaser = @ExerciseTeaser,
	Lang = @Lang,
	New = @New,
	ExerciseContent = @ExerciseContent
WHERE ExerciseLangID = @ExerciseLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseLangID", exerciseLang.ExerciseLangID),
				new SqlParameter("@ExerciseID", exerciseLang.ExerciseID),
				new SqlParameter("@Exercise", exerciseLang.Exercise),
				new SqlParameter("@ExerciseTime", exerciseLang.ExerciseTime),
				new SqlParameter("@ExerciseTeaser", exerciseLang.ExerciseTeaser),
				new SqlParameter("@Lang", exerciseLang.Lang),
				new SqlParameter("@New", exerciseLang.New),
				new SqlParameter("@ExerciseContent", exerciseLang.ExerciseContent)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseLang
WHERE ExerciseLangID = @ExerciseLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseLangID", id)
			);
		}
		
		public override ExerciseLang Read(int id)
		{
			string query = @"
SELECT 	ExerciseLangID, 
	ExerciseID, 
	Exercise, 
	ExerciseTime, 
	ExerciseTeaser, 
	Lang, 
	New, 
	ExerciseContent
FROM ExerciseLang
WHERE ExerciseLangID = @ExerciseLangID";
			ExerciseLang exerciseLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseLangID", id))) {
				if (rs.Read()) {
					exerciseLang = new ExerciseLang {
						ExerciseLangID = GetInt32(rs, 0),
						ExerciseID = GetInt32(rs, 1),
						Exercise = GetString(rs, 2),
						ExerciseTime = GetString(rs, 3),
						ExerciseTeaser = GetString(rs, 4),
						Lang = GetInt32(rs, 5),
						New = GetBoolean(rs, 6),
						ExerciseContent = GetString(rs, 7)
					};
				}
			}
			return exerciseLang;
		}
		
		public override IList<ExerciseLang> FindAll()
		{
			string query = @"
SELECT 	ExerciseLangID, 
	ExerciseID, 
	Exercise, 
	ExerciseTime, 
	ExerciseTeaser, 
	Lang, 
	New, 
	ExerciseContent
FROM ExerciseLang";
			var exerciseLangs = new List<ExerciseLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseLangs.Add(new ExerciseLang {
						ExerciseLangID = GetInt32(rs, 0),
						ExerciseID = GetInt32(rs, 1),
						Exercise = GetString(rs, 2),
						ExerciseTime = GetString(rs, 3),
						ExerciseTeaser = GetString(rs, 4),
						Lang = GetInt32(rs, 5),
						New = GetBoolean(rs, 6),
						ExerciseContent = GetString(rs, 7)
					});
				}
			}
			return exerciseLangs;
		}
	}
}
