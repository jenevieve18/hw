using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorExerciseDataInputRepository : BaseSqlRepository<SponsorExerciseDataInput>
	{
		public SqlSponsorExerciseDataInputRepository()
		{
		}
		
		public override void Save(SponsorExerciseDataInput sponsorExerciseDataInput)
		{
			string query = @"
INSERT INTO SponsorExerciseDataInput(
	SponsorExerciseDataInputID, 
	Content, 
	SponsorID, 
	Order, 
	ExerciseVariantLangID
)
VALUES(
	@SponsorExerciseDataInputID, 
	@Content, 
	@SponsorID, 
	@Order, 
	@ExerciseVariantLangID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExerciseDataInputID", sponsorExerciseDataInput.SponsorExerciseDataInputID),
				new SqlParameter("@Content", sponsorExerciseDataInput.Content),
				new SqlParameter("@SponsorID", sponsorExerciseDataInput.SponsorID),
				new SqlParameter("@Order", sponsorExerciseDataInput.Order),
				new SqlParameter("@ExerciseVariantLangID", sponsorExerciseDataInput.ExerciseVariantLangID)
			);
		}
		
		public override void Update(SponsorExerciseDataInput sponsorExerciseDataInput, int id)
		{
			string query = @"
UPDATE SponsorExerciseDataInput SET
	SponsorExerciseDataInputID = @SponsorExerciseDataInputID,
	Content = @Content,
	SponsorID = @SponsorID,
	Order = @Order,
	ExerciseVariantLangID = @ExerciseVariantLangID
WHERE SponsorExerciseDataInputID = @SponsorExerciseDataInputID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExerciseDataInputID", sponsorExerciseDataInput.SponsorExerciseDataInputID),
				new SqlParameter("@Content", sponsorExerciseDataInput.Content),
				new SqlParameter("@SponsorID", sponsorExerciseDataInput.SponsorID),
				new SqlParameter("@Order", sponsorExerciseDataInput.Order),
				new SqlParameter("@ExerciseVariantLangID", sponsorExerciseDataInput.ExerciseVariantLangID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorExerciseDataInput
WHERE SponsorExerciseDataInputID = @SponsorExerciseDataInputID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExerciseDataInputID", id)
			);
		}
		
		public override SponsorExerciseDataInput Read(int id)
		{
			string query = @"
SELECT 	SponsorExerciseDataInputID, 
	Content, 
	SponsorID, 
	Order, 
	ExerciseVariantLangID
FROM SponsorExerciseDataInput
WHERE SponsorExerciseDataInputID = @SponsorExerciseDataInputID";
			SponsorExerciseDataInput sponsorExerciseDataInput = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorExerciseDataInputID", id))) {
				if (rs.Read()) {
					sponsorExerciseDataInput = new SponsorExerciseDataInput {
						SponsorExerciseDataInputID = GetInt32(rs, 0),
						Content = GetString(rs, 1),
						SponsorID = GetInt32(rs, 2),
						Order = GetInt32(rs, 3),
						ExerciseVariantLangID = GetInt32(rs, 4)
					};
				}
			}
			return sponsorExerciseDataInput;
		}
		
		public override IList<SponsorExerciseDataInput> FindAll()
		{
			string query = @"
SELECT 	SponsorExerciseDataInputID, 
	Content, 
	SponsorID, 
	Order, 
	ExerciseVariantLangID
FROM SponsorExerciseDataInput";
			var sponsorExerciseDataInputs = new List<SponsorExerciseDataInput>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorExerciseDataInputs.Add(new SponsorExerciseDataInput {
						SponsorExerciseDataInputID = GetInt32(rs, 0),
						Content = GetString(rs, 1),
						SponsorID = GetInt32(rs, 2),
						Order = GetInt32(rs, 3),
						ExerciseVariantLangID = GetInt32(rs, 4)
					});
				}
			}
			return sponsorExerciseDataInputs;
		}
	}
}
