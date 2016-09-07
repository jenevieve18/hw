using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseTypeLangRepository : BaseSqlRepository<ExerciseTypeLang>
	{
		public SqlExerciseTypeLangRepository()
		{
		}
		
		public override void Save(ExerciseTypeLang exerciseTypeLang)
		{
			string query = @"
INSERT INTO ExerciseTypeLang(
	ExerciseTypeLangID, 
	ExerciseTypeID, 
	ExerciseType, 
	ExerciseSubtype, 
	Lang
)
VALUES(
	@ExerciseTypeLangID, 
	@ExerciseTypeID, 
	@ExerciseType, 
	@ExerciseSubtype, 
	@Lang
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseTypeLangID", exerciseTypeLang.ExerciseTypeLangID),
				new SqlParameter("@ExerciseTypeID", exerciseTypeLang.ExerciseTypeID),
				new SqlParameter("@ExerciseType", exerciseTypeLang.ExerciseType),
				new SqlParameter("@ExerciseSubtype", exerciseTypeLang.ExerciseSubtype),
				new SqlParameter("@Lang", exerciseTypeLang.Lang)
			);
		}
		
		public override void Update(ExerciseTypeLang exerciseTypeLang, int id)
		{
			string query = @"
UPDATE ExerciseTypeLang SET
	ExerciseTypeLangID = @ExerciseTypeLangID,
	ExerciseTypeID = @ExerciseTypeID,
	ExerciseType = @ExerciseType,
	ExerciseSubtype = @ExerciseSubtype,
	Lang = @Lang
WHERE ExerciseTypeLangID = @ExerciseTypeLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseTypeLangID", exerciseTypeLang.ExerciseTypeLangID),
				new SqlParameter("@ExerciseTypeID", exerciseTypeLang.ExerciseTypeID),
				new SqlParameter("@ExerciseType", exerciseTypeLang.ExerciseType),
				new SqlParameter("@ExerciseSubtype", exerciseTypeLang.ExerciseSubtype),
				new SqlParameter("@Lang", exerciseTypeLang.Lang)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseTypeLang
WHERE ExerciseTypeLangID = @ExerciseTypeLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseTypeLangID", id)
			);
		}
		
		public override ExerciseTypeLang Read(int id)
		{
			string query = @"
SELECT 	ExerciseTypeLangID, 
	ExerciseTypeID, 
	ExerciseType, 
	ExerciseSubtype, 
	Lang
FROM ExerciseTypeLang
WHERE ExerciseTypeLangID = @ExerciseTypeLangID";
			ExerciseTypeLang exerciseTypeLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseTypeLangID", id))) {
				if (rs.Read()) {
					exerciseTypeLang = new ExerciseTypeLang {
						ExerciseTypeLangID = GetInt32(rs, 0),
						ExerciseTypeID = GetInt32(rs, 1),
						ExerciseType = GetString(rs, 2),
						ExerciseSubtype = GetString(rs, 3),
						Lang = GetInt32(rs, 4)
					};
				}
			}
			return exerciseTypeLang;
		}
		
		public override IList<ExerciseTypeLang> FindAll()
		{
			string query = @"
SELECT 	ExerciseTypeLangID, 
	ExerciseTypeID, 
	ExerciseType, 
	ExerciseSubtype, 
	Lang
FROM ExerciseTypeLang";
			var exerciseTypeLangs = new List<ExerciseTypeLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseTypeLangs.Add(new ExerciseTypeLang {
						ExerciseTypeLangID = GetInt32(rs, 0),
						ExerciseTypeID = GetInt32(rs, 1),
						ExerciseType = GetString(rs, 2),
						ExerciseSubtype = GetString(rs, 3),
						Lang = GetInt32(rs, 4)
					});
				}
			}
			return exerciseTypeLangs;
		}
	}
}
