using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseAreaRepository : BaseSqlRepository<ExerciseArea>
	{
		public SqlExerciseAreaRepository()
		{
		}
		
		public override void Save(ExerciseArea exerciseArea)
		{
			string query = @"
INSERT INTO ExerciseArea(
	ExerciseAreaID, 
	ExerciseAreaSortOrder, 
	ExerciseAreaImg
)
VALUES(
	@ExerciseAreaID, 
	@ExerciseAreaSortOrder, 
	@ExerciseAreaImg
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseAreaID", exerciseArea.ExerciseAreaID),
				new SqlParameter("@ExerciseAreaSortOrder", exerciseArea.ExerciseAreaSortOrder),
				new SqlParameter("@ExerciseAreaImg", exerciseArea.ExerciseAreaImg)
			);
		}
		
		public override void Update(ExerciseArea exerciseArea, int id)
		{
			string query = @"
UPDATE ExerciseArea SET
	ExerciseAreaID = @ExerciseAreaID,
	ExerciseAreaSortOrder = @ExerciseAreaSortOrder,
	ExerciseAreaImg = @ExerciseAreaImg
WHERE ExerciseAreaID = @ExerciseAreaID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseAreaID", exerciseArea.ExerciseAreaID),
				new SqlParameter("@ExerciseAreaSortOrder", exerciseArea.ExerciseAreaSortOrder),
				new SqlParameter("@ExerciseAreaImg", exerciseArea.ExerciseAreaImg)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseArea
WHERE ExerciseAreaID = @ExerciseAreaID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseAreaID", id)
			);
		}
		
		public override ExerciseArea Read(int id)
		{
			string query = @"
SELECT 	ExerciseAreaID, 
	ExerciseAreaSortOrder, 
	ExerciseAreaImg
FROM ExerciseArea
WHERE ExerciseAreaID = @ExerciseAreaID";
			ExerciseArea exerciseArea = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseAreaID", id))) {
				if (rs.Read()) {
					exerciseArea = new ExerciseArea {
						ExerciseAreaID = GetInt32(rs, 0),
						ExerciseAreaSortOrder = GetInt32(rs, 1),
						ExerciseAreaImg = GetString(rs, 2)
					};
				}
			}
			return exerciseArea;
		}
		
		public override IList<ExerciseArea> FindAll()
		{
			string query = @"
SELECT 	ExerciseAreaID, 
	ExerciseAreaSortOrder, 
	ExerciseAreaImg
FROM ExerciseArea";
			var exerciseAreas = new List<ExerciseArea>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseAreas.Add(new ExerciseArea {
						ExerciseAreaID = GetInt32(rs, 0),
						ExerciseAreaSortOrder = GetInt32(rs, 1),
						ExerciseAreaImg = GetString(rs, 2)
					});
				}
			}
			return exerciseAreas;
		}
	}
}
