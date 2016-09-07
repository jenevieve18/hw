using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminExerciseDataInputRepository : BaseSqlRepository<SponsorAdminExerciseDataInput>
	{
		public SqlSponsorAdminExerciseDataInputRepository()
		{
		}
		
		public override void Save(SponsorAdminExerciseDataInput sponsorAdminExerciseDataInput)
		{
			string query = @"
INSERT INTO SponsorAdminExerciseDataInput(
	SponsorAdminExerciseDataInputID, 
	Content, 
	SponsorAdminExerciseID, 
	Order
)
VALUES(
	@SponsorAdminExerciseDataInputID, 
	@Content, 
	@SponsorAdminExerciseID, 
	@Order
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExerciseDataInputID", sponsorAdminExerciseDataInput.SponsorAdminExerciseDataInputID),
				new SqlParameter("@Content", sponsorAdminExerciseDataInput.Content),
				new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExerciseDataInput.SponsorAdminExerciseID),
				new SqlParameter("@Order", sponsorAdminExerciseDataInput.Order)
			);
		}
		
		public override void Update(SponsorAdminExerciseDataInput sponsorAdminExerciseDataInput, int id)
		{
			string query = @"
UPDATE SponsorAdminExerciseDataInput SET
	SponsorAdminExerciseDataInputID = @SponsorAdminExerciseDataInputID,
	Content = @Content,
	SponsorAdminExerciseID = @SponsorAdminExerciseID,
	Order = @Order
WHERE SponsorAdminExerciseDataInputID = @SponsorAdminExerciseDataInputID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExerciseDataInputID", sponsorAdminExerciseDataInput.SponsorAdminExerciseDataInputID),
				new SqlParameter("@Content", sponsorAdminExerciseDataInput.Content),
				new SqlParameter("@SponsorAdminExerciseID", sponsorAdminExerciseDataInput.SponsorAdminExerciseID),
				new SqlParameter("@Order", sponsorAdminExerciseDataInput.Order)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminExerciseDataInput
WHERE SponsorAdminExerciseDataInputID = @SponsorAdminExerciseDataInputID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExerciseDataInputID", id)
			);
		}
		
		public override SponsorAdminExerciseDataInput Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminExerciseDataInputID, 
	Content, 
	SponsorAdminExerciseID, 
	Order
FROM SponsorAdminExerciseDataInput
WHERE SponsorAdminExerciseDataInputID = @SponsorAdminExerciseDataInputID";
			SponsorAdminExerciseDataInput sponsorAdminExerciseDataInput = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminExerciseDataInputID", id))) {
				if (rs.Read()) {
					sponsorAdminExerciseDataInput = new SponsorAdminExerciseDataInput {
						SponsorAdminExerciseDataInputID = GetInt32(rs, 0),
						Content = GetString(rs, 1),
						SponsorAdminExerciseID = GetInt32(rs, 2),
						Order = GetInt32(rs, 3)
					};
				}
			}
			return sponsorAdminExerciseDataInput;
		}
		
		public override IList<SponsorAdminExerciseDataInput> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminExerciseDataInputID, 
	Content, 
	SponsorAdminExerciseID, 
	Order
FROM SponsorAdminExerciseDataInput";
			var sponsorAdminExerciseDataInputs = new List<SponsorAdminExerciseDataInput>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminExerciseDataInputs.Add(new SponsorAdminExerciseDataInput {
						SponsorAdminExerciseDataInputID = GetInt32(rs, 0),
						Content = GetString(rs, 1),
						SponsorAdminExerciseID = GetInt32(rs, 2),
						Order = GetInt32(rs, 3)
					});
				}
			}
			return sponsorAdminExerciseDataInputs;
		}
	}
}
