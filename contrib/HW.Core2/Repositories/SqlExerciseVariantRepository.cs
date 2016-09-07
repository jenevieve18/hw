using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseVariantRepository : BaseSqlRepository<ExerciseVariant>
	{
		public SqlExerciseVariantRepository()
		{
		}
		
		public override void Save(ExerciseVariant exerciseVariant)
		{
			string query = @"
INSERT INTO ExerciseVariant(
	ExerciseVariantID, 
	ExerciseID, 
	ExerciseTypeID
)
VALUES(
	@ExerciseVariantID, 
	@ExerciseID, 
	@ExerciseTypeID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseVariantID", exerciseVariant.ExerciseVariantID),
				new SqlParameter("@ExerciseID", exerciseVariant.ExerciseID),
				new SqlParameter("@ExerciseTypeID", exerciseVariant.ExerciseTypeID)
			);
		}
		
		public override void Update(ExerciseVariant exerciseVariant, int id)
		{
			string query = @"
UPDATE ExerciseVariant SET
	ExerciseVariantID = @ExerciseVariantID,
	ExerciseID = @ExerciseID,
	ExerciseTypeID = @ExerciseTypeID
WHERE ExerciseVariantID = @ExerciseVariantID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseVariantID", exerciseVariant.ExerciseVariantID),
				new SqlParameter("@ExerciseID", exerciseVariant.ExerciseID),
				new SqlParameter("@ExerciseTypeID", exerciseVariant.ExerciseTypeID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseVariant
WHERE ExerciseVariantID = @ExerciseVariantID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseVariantID", id)
			);
		}
		
		public override ExerciseVariant Read(int id)
		{
			string query = @"
SELECT 	ExerciseVariantID, 
	ExerciseID, 
	ExerciseTypeID
FROM ExerciseVariant
WHERE ExerciseVariantID = @ExerciseVariantID";
			ExerciseVariant exerciseVariant = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseVariantID", id))) {
				if (rs.Read()) {
					exerciseVariant = new ExerciseVariant {
						ExerciseVariantID = GetInt32(rs, 0),
						ExerciseID = GetInt32(rs, 1),
						ExerciseTypeID = GetInt32(rs, 2)
					};
				}
			}
			return exerciseVariant;
		}
		
		public override IList<ExerciseVariant> FindAll()
		{
			string query = @"
SELECT 	ExerciseVariantID, 
	ExerciseID, 
	ExerciseTypeID
FROM ExerciseVariant";
			var exerciseVariants = new List<ExerciseVariant>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseVariants.Add(new ExerciseVariant {
						ExerciseVariantID = GetInt32(rs, 0),
						ExerciseID = GetInt32(rs, 1),
						ExerciseTypeID = GetInt32(rs, 2)
					});
				}
			}
			return exerciseVariants;
		}
	}
}
