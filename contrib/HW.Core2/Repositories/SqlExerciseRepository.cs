using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseRepository : BaseSqlRepository<Exercise>
	{
		public SqlExerciseRepository()
		{
		}
		
		public override void Save(Exercise exercise)
		{
			string query = @"
INSERT INTO Exercise(
	ExerciseID, 
	ExerciseAreaID, 
	ExerciseSortOrder, 
	ExerciseImg, 
	RequiredUserLevel, 
	Minutes, 
	ExerciseCategoryID, 
	PrintOnBottom, 
	ReplacementHead, 
	Status, 
	Script
)
VALUES(
	@ExerciseID, 
	@ExerciseAreaID, 
	@ExerciseSortOrder, 
	@ExerciseImg, 
	@RequiredUserLevel, 
	@Minutes, 
	@ExerciseCategoryID, 
	@PrintOnBottom, 
	@ReplacementHead, 
	@Status, 
	@Script
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseID", exercise.ExerciseID),
				new SqlParameter("@ExerciseAreaID", exercise.ExerciseAreaID),
				new SqlParameter("@ExerciseSortOrder", exercise.ExerciseSortOrder),
				new SqlParameter("@ExerciseImg", exercise.ExerciseImg),
				new SqlParameter("@RequiredUserLevel", exercise.RequiredUserLevel),
				new SqlParameter("@Minutes", exercise.Minutes),
				new SqlParameter("@ExerciseCategoryID", exercise.ExerciseCategoryID),
				new SqlParameter("@PrintOnBottom", exercise.PrintOnBottom),
				new SqlParameter("@ReplacementHead", exercise.ReplacementHead),
				new SqlParameter("@Status", exercise.Status),
				new SqlParameter("@Script", exercise.Script)
			);
		}
		
		public override void Update(Exercise exercise, int id)
		{
			string query = @"
UPDATE Exercise SET
	ExerciseID = @ExerciseID,
	ExerciseAreaID = @ExerciseAreaID,
	ExerciseSortOrder = @ExerciseSortOrder,
	ExerciseImg = @ExerciseImg,
	RequiredUserLevel = @RequiredUserLevel,
	Minutes = @Minutes,
	ExerciseCategoryID = @ExerciseCategoryID,
	PrintOnBottom = @PrintOnBottom,
	ReplacementHead = @ReplacementHead,
	Status = @Status,
	Script = @Script
WHERE ExerciseID = @ExerciseID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseID", exercise.ExerciseID),
				new SqlParameter("@ExerciseAreaID", exercise.ExerciseAreaID),
				new SqlParameter("@ExerciseSortOrder", exercise.ExerciseSortOrder),
				new SqlParameter("@ExerciseImg", exercise.ExerciseImg),
				new SqlParameter("@RequiredUserLevel", exercise.RequiredUserLevel),
				new SqlParameter("@Minutes", exercise.Minutes),
				new SqlParameter("@ExerciseCategoryID", exercise.ExerciseCategoryID),
				new SqlParameter("@PrintOnBottom", exercise.PrintOnBottom),
				new SqlParameter("@ReplacementHead", exercise.ReplacementHead),
				new SqlParameter("@Status", exercise.Status),
				new SqlParameter("@Script", exercise.Script)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Exercise
WHERE ExerciseID = @ExerciseID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseID", id)
			);
		}
		
		public override Exercise Read(int id)
		{
			string query = @"
SELECT 	ExerciseID, 
	ExerciseAreaID, 
	ExerciseSortOrder, 
	ExerciseImg, 
	RequiredUserLevel, 
	Minutes, 
	ExerciseCategoryID, 
	PrintOnBottom, 
	ReplacementHead, 
	Status, 
	Script
FROM Exercise
WHERE ExerciseID = @ExerciseID";
			Exercise exercise = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseID", id))) {
				if (rs.Read()) {
					exercise = new Exercise {
						ExerciseID = GetInt32(rs, 0),
						ExerciseAreaID = GetInt32(rs, 1),
						ExerciseSortOrder = GetInt32(rs, 2),
						ExerciseImg = GetString(rs, 3),
						RequiredUserLevel = GetInt32(rs, 4),
						Minutes = GetInt32(rs, 5),
						ExerciseCategoryID = GetInt32(rs, 6),
						PrintOnBottom = GetInt32(rs, 7),
						ReplacementHead = GetString(rs, 8),
						Status = GetInt32(rs, 9),
						Script = GetString(rs, 10)
					};
				}
			}
			return exercise;
		}
		
		public override IList<Exercise> FindAll()
		{
			string query = @"
SELECT 	ExerciseID, 
	ExerciseAreaID, 
	ExerciseSortOrder, 
	ExerciseImg, 
	RequiredUserLevel, 
	Minutes, 
	ExerciseCategoryID, 
	PrintOnBottom, 
	ReplacementHead, 
	Status, 
	Script
FROM Exercise";
			var exercises = new List<Exercise>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exercises.Add(new Exercise {
						ExerciseID = GetInt32(rs, 0),
						ExerciseAreaID = GetInt32(rs, 1),
						ExerciseSortOrder = GetInt32(rs, 2),
						ExerciseImg = GetString(rs, 3),
						RequiredUserLevel = GetInt32(rs, 4),
						Minutes = GetInt32(rs, 5),
						ExerciseCategoryID = GetInt32(rs, 6),
						PrintOnBottom = GetInt32(rs, 7),
						ReplacementHead = GetString(rs, 8),
						Status = GetInt32(rs, 9),
						Script = GetString(rs, 10)
					});
				}
			}
			return exercises;
		}
	}
}
