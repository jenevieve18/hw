using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseTypeRepository : BaseSqlRepository<ExerciseType>
	{
		public SqlExerciseTypeRepository()
		{
		}
		
		public override void Save(ExerciseType exerciseType)
		{
			string query = @"
INSERT INTO ExerciseType(
	ExerciseTypeID, 
	ExerciseTypeSortOrder
)
VALUES(
	@ExerciseTypeID, 
	@ExerciseTypeSortOrder
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseTypeID", exerciseType.ExerciseTypeID),
				new SqlParameter("@ExerciseTypeSortOrder", exerciseType.ExerciseTypeSortOrder)
			);
		}
		
		public override void Update(ExerciseType exerciseType, int id)
		{
			string query = @"
UPDATE ExerciseType SET
	ExerciseTypeID = @ExerciseTypeID,
	ExerciseTypeSortOrder = @ExerciseTypeSortOrder
WHERE ExerciseTypeID = @ExerciseTypeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseTypeID", exerciseType.ExerciseTypeID),
				new SqlParameter("@ExerciseTypeSortOrder", exerciseType.ExerciseTypeSortOrder)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseType
WHERE ExerciseTypeID = @ExerciseTypeID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseTypeID", id)
			);
		}
		
		public override ExerciseType Read(int id)
		{
			string query = @"
SELECT 	ExerciseTypeID, 
	ExerciseTypeSortOrder
FROM ExerciseType
WHERE ExerciseTypeID = @ExerciseTypeID";
			ExerciseType exerciseType = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseTypeID", id))) {
				if (rs.Read()) {
					exerciseType = new ExerciseType {
						ExerciseTypeID = GetInt32(rs, 0),
						ExerciseTypeSortOrder = GetInt32(rs, 1)
					};
				}
			}
			return exerciseType;
		}
		
		public override IList<ExerciseType> FindAll()
		{
			string query = @"
SELECT 	ExerciseTypeID, 
	ExerciseTypeSortOrder
FROM ExerciseType";
			var exerciseTypes = new List<ExerciseType>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseTypes.Add(new ExerciseType {
						ExerciseTypeID = GetInt32(rs, 0),
						ExerciseTypeSortOrder = GetInt32(rs, 1)
					});
				}
			}
			return exerciseTypes;
		}
	}
}
