using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlExerciseVariantLangRepository : BaseSqlRepository<ExerciseVariantLang>
	{
		public SqlExerciseVariantLangRepository()
		{
		}
		
		public override void Save(ExerciseVariantLang exerciseVariantLang)
		{
			string query = @"
INSERT INTO ExerciseVariantLang(
	ExerciseVariantLangID, 
	ExerciseVariantID, 
	ExerciseFile, 
	ExerciseFileSize, 
	ExerciseContent, 
	ExerciseWindowX, 
	ExerciseWindowY, 
	Lang
)
VALUES(
	@ExerciseVariantLangID, 
	@ExerciseVariantID, 
	@ExerciseFile, 
	@ExerciseFileSize, 
	@ExerciseContent, 
	@ExerciseWindowX, 
	@ExerciseWindowY, 
	@Lang
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseVariantLangID", exerciseVariantLang.ExerciseVariantLangID),
				new SqlParameter("@ExerciseVariantID", exerciseVariantLang.ExerciseVariantID),
				new SqlParameter("@ExerciseFile", exerciseVariantLang.ExerciseFile),
				new SqlParameter("@ExerciseFileSize", exerciseVariantLang.ExerciseFileSize),
				new SqlParameter("@ExerciseContent", exerciseVariantLang.ExerciseContent),
				new SqlParameter("@ExerciseWindowX", exerciseVariantLang.ExerciseWindowX),
				new SqlParameter("@ExerciseWindowY", exerciseVariantLang.ExerciseWindowY),
				new SqlParameter("@Lang", exerciseVariantLang.Lang)
			);
		}
		
		public override void Update(ExerciseVariantLang exerciseVariantLang, int id)
		{
			string query = @"
UPDATE ExerciseVariantLang SET
	ExerciseVariantLangID = @ExerciseVariantLangID,
	ExerciseVariantID = @ExerciseVariantID,
	ExerciseFile = @ExerciseFile,
	ExerciseFileSize = @ExerciseFileSize,
	ExerciseContent = @ExerciseContent,
	ExerciseWindowX = @ExerciseWindowX,
	ExerciseWindowY = @ExerciseWindowY,
	Lang = @Lang
WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseVariantLangID", exerciseVariantLang.ExerciseVariantLangID),
				new SqlParameter("@ExerciseVariantID", exerciseVariantLang.ExerciseVariantID),
				new SqlParameter("@ExerciseFile", exerciseVariantLang.ExerciseFile),
				new SqlParameter("@ExerciseFileSize", exerciseVariantLang.ExerciseFileSize),
				new SqlParameter("@ExerciseContent", exerciseVariantLang.ExerciseContent),
				new SqlParameter("@ExerciseWindowX", exerciseVariantLang.ExerciseWindowX),
				new SqlParameter("@ExerciseWindowY", exerciseVariantLang.ExerciseWindowY),
				new SqlParameter("@Lang", exerciseVariantLang.Lang)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ExerciseVariantLang
WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ExerciseVariantLangID", id)
			);
		}
		
		public override ExerciseVariantLang Read(int id)
		{
			string query = @"
SELECT 	ExerciseVariantLangID, 
	ExerciseVariantID, 
	ExerciseFile, 
	ExerciseFileSize, 
	ExerciseContent, 
	ExerciseWindowX, 
	ExerciseWindowY, 
	Lang
FROM ExerciseVariantLang
WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
			ExerciseVariantLang exerciseVariantLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ExerciseVariantLangID", id))) {
				if (rs.Read()) {
					exerciseVariantLang = new ExerciseVariantLang {
						ExerciseVariantLangID = GetInt32(rs, 0),
						ExerciseVariantID = GetInt32(rs, 1),
						ExerciseFile = GetString(rs, 2),
						ExerciseFileSize = GetInt32(rs, 3),
						ExerciseContent = GetString(rs, 4),
						ExerciseWindowX = GetInt32(rs, 5),
						ExerciseWindowY = GetInt32(rs, 6),
						Lang = GetInt32(rs, 7)
					};
				}
			}
			return exerciseVariantLang;
		}
		
		public override IList<ExerciseVariantLang> FindAll()
		{
			string query = @"
SELECT 	ExerciseVariantLangID, 
	ExerciseVariantID, 
	ExerciseFile, 
	ExerciseFileSize, 
	ExerciseContent, 
	ExerciseWindowX, 
	ExerciseWindowY, 
	Lang
FROM ExerciseVariantLang";
			var exerciseVariantLangs = new List<ExerciseVariantLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					exerciseVariantLangs.Add(new ExerciseVariantLang {
						ExerciseVariantLangID = GetInt32(rs, 0),
						ExerciseVariantID = GetInt32(rs, 1),
						ExerciseFile = GetString(rs, 2),
						ExerciseFileSize = GetInt32(rs, 3),
						ExerciseContent = GetString(rs, 4),
						ExerciseWindowX = GetInt32(rs, 5),
						ExerciseWindowY = GetInt32(rs, 6),
						Lang = GetInt32(rs, 7)
					});
				}
			}
			return exerciseVariantLangs;
		}
	}
}
