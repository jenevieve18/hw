using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseStatRepository : BaseSqlRepository<ExerciseStat>
	{
		public SqlExerciseStatRepository()
		{
		}
		
		public override void Save(ExerciseStat exerciseStat)
		{
			string query = @"
INSERT INTO ExerciseStats(
	ExerciseStatsID, 
	ExerciseVariantLangID, 
	UserID, 
	DateTime, 
	UserProfileID
)
VALUES(
	@ExerciseStatsID, 
	@ExerciseVariantLangID, 
	@UserID, 
	@DateTime, 
	@UserProfileID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseStatsID", exerciseStat.ExerciseStatsID),
				new SqlParameter("@ExerciseVariantLangID", exerciseStat.ExerciseVariantLangID),
				new SqlParameter("@UserID", exerciseStat.UserID),
				new SqlParameter("@DateTime", exerciseStat.DateTime),
				new SqlParameter("@UserProfileID", exerciseStat.UserProfileID)
			);
		}
		
		public override void Update(ExerciseStat exerciseStat, int id)
		{
			string query = @"
UPDATE ExerciseStats SET
	ExerciseStatsID = @ExerciseStatsID,
	ExerciseVariantLangID = @ExerciseVariantLangID,
	UserID = @UserID,
	DateTime = @DateTime,
	UserProfileID = @UserProfileID
WHERE ExerciseStatID = @ExerciseStatID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseStatsID", exerciseStat.ExerciseStatsID),
				new SqlParameter("@ExerciseVariantLangID", exerciseStat.ExerciseVariantLangID),
				new SqlParameter("@UserID", exerciseStat.UserID),
				new SqlParameter("@DateTime", exerciseStat.DateTime),
				new SqlParameter("@UserProfileID", exerciseStat.UserProfileID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseStats
WHERE ExerciseStatID = @ExerciseStatID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseStatID", id)
			);
		}
		
		public override ExerciseStat Read(int id)
		{
			string query = @"
SELECT 	ExerciseStatsID, 
	ExerciseVariantLangID, 
	UserID, 
	DateTime, 
	UserProfileID
FROM ExerciseStats
WHERE ExerciseStatID = @ExerciseStatID";
			ExerciseStat exerciseStat = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseStatID", id))) {
				if (rs.Read()) {
					exerciseStat = new ExerciseStat {
						ExerciseStatsID = GetInt32(rs, 0),
						ExerciseVariantLangID = GetInt32(rs, 1),
						UserID = GetInt32(rs, 2),
						DateTime = GetDateTime(rs, 3),
						UserProfileID = GetInt32(rs, 4)
					};
				}
			}
			return exerciseStat;
		}
		
		public override IList<ExerciseStat> FindAll()
		{
			string query = @"
SELECT 	ExerciseStatsID, 
	ExerciseVariantLangID, 
	UserID, 
	DateTime, 
	UserProfileID
FROM ExerciseStats";
			var exerciseStats = new List<ExerciseStat>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseStats.Add(new ExerciseStat {
						ExerciseStatsID = GetInt32(rs, 0),
						ExerciseVariantLangID = GetInt32(rs, 1),
						UserID = GetInt32(rs, 2),
						DateTime = GetDateTime(rs, 3),
						UserProfileID = GetInt32(rs, 4)
					});
				}
			}
			return exerciseStats;
		}
	}
}
