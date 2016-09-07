using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminExerciseRepository : BaseSqlRepository<SponsorAdminExercise>
	{
		public SqlSponsorAdminExerciseRepository()
		{
		}
		
		public override void Save(SponsorAdminExercise sponsorAdminExercise)
		{
			string query = @"
INSERT INTO SponsorAdminExercise(
	SponsorAdminExerciseID, 
	Date, 
	SponsorAdminID, 
	ExerciseVariantLangID
)
VALUES(
	@SponsorAdminExerciseID, 
	@Date, 
	@SponsorAdminID, 
	@ExerciseVariantLangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExercise.SponsorAdminExerciseID),
				new SqlParameter("@Date", sponsorAdminExercise.Date),
				new SqlParameter("@SponsorAdminID", sponsorAdminExercise.SponsorAdminID),
				new SqlParameter("@ExerciseVariantLangID", sponsorAdminExercise.ExerciseVariantLangID)
			);
		}
		
		public override void Update(SponsorAdminExercise sponsorAdminExercise, int id)
		{
			string query = @"
UPDATE SponsorAdminExercise SET
	SponsorAdminExerciseID = @SponsorAdminExerciseID,
	Date = @Date,
	SponsorAdminID = @SponsorAdminID,
	ExerciseVariantLangID = @ExerciseVariantLangID
WHERE SponsorAdminExerciseID = @SponsorAdminExerciseID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExercise.SponsorAdminExerciseID),
				new SqlParameter("@Date", sponsorAdminExercise.Date),
				new SqlParameter("@SponsorAdminID", sponsorAdminExercise.SponsorAdminID),
				new SqlParameter("@ExerciseVariantLangID", sponsorAdminExercise.ExerciseVariantLangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminExercise
WHERE SponsorAdminExerciseID = @SponsorAdminExerciseID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExerciseID", id)
			);
		}
		
		public override SponsorAdminExercise Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminExerciseID, 
	Date, 
	SponsorAdminID, 
	ExerciseVariantLangID
FROM SponsorAdminExercise
WHERE SponsorAdminExerciseID = @SponsorAdminExerciseID";
			SponsorAdminExercise sponsorAdminExercise = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminExerciseID", id))) {
				if (rs.Read()) {
					sponsorAdminExercise = new SponsorAdminExercise {
						SponsorAdminExerciseID = GetInt32(rs, 0),
						Date = GetDateTime(rs, 1),
						SponsorAdminID = GetInt32(rs, 2),
						ExerciseVariantLangID = GetInt32(rs, 3)
					};
				}
			}
			return sponsorAdminExercise;
		}
		
		public override IList<SponsorAdminExercise> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminExerciseID, 
	Date, 
	SponsorAdminID, 
	ExerciseVariantLangID
FROM SponsorAdminExercise";
			var sponsorAdminExercises = new List<SponsorAdminExercise>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminExercises.Add(new SponsorAdminExercise {
						SponsorAdminExerciseID = GetInt32(rs, 0),
						Date = GetDateTime(rs, 1),
						SponsorAdminID = GetInt32(rs, 2),
						ExerciseVariantLangID = GetInt32(rs, 3)
					});
				}
			}
			return sponsorAdminExercises;
		}
	}
}
