using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseMiracleRepository : BaseSqlRepository<ExerciseMiracle>
	{
		public SqlExerciseMiracleRepository()
		{
		}
		
		public override void Save(ExerciseMiracle exerciseMiracle)
		{
			string query = @"
INSERT INTO ExerciseMiracle(
	ExerciseMiracleID, 
	UserID, 
	DateTime, 
	DateTimeChanged, 
	Miracle, 
	AllowPublish, 
	Published
)
VALUES(
	@ExerciseMiracleID, 
	@UserID, 
	@DateTime, 
	@DateTimeChanged, 
	@Miracle, 
	@AllowPublish, 
	@Published
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseMiracleID", exerciseMiracle.ExerciseMiracleID),
				new SqlParameter("@UserID", exerciseMiracle.UserID),
				new SqlParameter("@DateTime", exerciseMiracle.DateTime),
				new SqlParameter("@DateTimeChanged", exerciseMiracle.DateTimeChanged),
				new SqlParameter("@Miracle", exerciseMiracle.Miracle),
				new SqlParameter("@AllowPublish", exerciseMiracle.AllowPublish),
				new SqlParameter("@Published", exerciseMiracle.Published)
			);
		}
		
		public override void Update(ExerciseMiracle exerciseMiracle, int id)
		{
			string query = @"
UPDATE ExerciseMiracle SET
	ExerciseMiracleID = @ExerciseMiracleID,
	UserID = @UserID,
	DateTime = @DateTime,
	DateTimeChanged = @DateTimeChanged,
	Miracle = @Miracle,
	AllowPublish = @AllowPublish,
	Published = @Published
WHERE ExerciseMiracleID = @ExerciseMiracleID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseMiracleID", exerciseMiracle.ExerciseMiracleID),
				new SqlParameter("@UserID", exerciseMiracle.UserID),
				new SqlParameter("@DateTime", exerciseMiracle.DateTime),
				new SqlParameter("@DateTimeChanged", exerciseMiracle.DateTimeChanged),
				new SqlParameter("@Miracle", exerciseMiracle.Miracle),
				new SqlParameter("@AllowPublish", exerciseMiracle.AllowPublish),
				new SqlParameter("@Published", exerciseMiracle.Published)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseMiracle
WHERE ExerciseMiracleID = @ExerciseMiracleID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseMiracleID", id)
			);
		}
		
		public override ExerciseMiracle Read(int id)
		{
			string query = @"
SELECT 	ExerciseMiracleID, 
	UserID, 
	DateTime, 
	DateTimeChanged, 
	Miracle, 
	AllowPublish, 
	Published
FROM ExerciseMiracle
WHERE ExerciseMiracleID = @ExerciseMiracleID";
			ExerciseMiracle exerciseMiracle = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseMiracleID", id))) {
				if (rs.Read()) {
					exerciseMiracle = new ExerciseMiracle {
						ExerciseMiracleID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DateTime = GetDateTime(rs, 2),
						DateTimeChanged = GetDateTime(rs, 3),
						Miracle = GetString(rs, 4),
						AllowPublish = GetBoolean(rs, 5),
						Published = GetBoolean(rs, 6)
					};
				}
			}
			return exerciseMiracle;
		}
		
		public override IList<ExerciseMiracle> FindAll()
		{
			string query = @"
SELECT 	ExerciseMiracleID, 
	UserID, 
	DateTime, 
	DateTimeChanged, 
	Miracle, 
	AllowPublish, 
	Published
FROM ExerciseMiracle";
			var exerciseMiracles = new List<ExerciseMiracle>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseMiracles.Add(new ExerciseMiracle {
						ExerciseMiracleID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						DateTime = GetDateTime(rs, 2),
						DateTimeChanged = GetDateTime(rs, 3),
						Miracle = GetString(rs, 4),
						AllowPublish = GetBoolean(rs, 5),
						Published = GetBoolean(rs, 6)
					});
				}
			}
			return exerciseMiracles;
		}
	}
}
