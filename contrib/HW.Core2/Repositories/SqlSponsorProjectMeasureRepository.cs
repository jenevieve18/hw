using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorProjectMeasureRepository : BaseSqlRepository<SponsorProjectMeasure>
	{
		public SqlSponsorProjectMeasureRepository()
		{
		}
		
		public override void Save(SponsorProjectMeasure sponsorProjectMeasure)
		{
			string query = @"
INSERT INTO SponsorProjectMeasure(
	SponsorProjectMeasureID, 
	SponsorProjectID, 
	MeasureID
)
VALUES(
	@SponsorProjectMeasureID, 
	@SponsorProjectID, 
	@MeasureID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectMeasureID", sponsorProjectMeasure.SponsorProjectMeasureID),
				new SqlParameter("@SponsorProjectID", sponsorProjectMeasure.SponsorProjectID),
				new SqlParameter("@MeasureID", sponsorProjectMeasure.MeasureID)
			);
		}
		
		public override void Update(SponsorProjectMeasure sponsorProjectMeasure, int id)
		{
			string query = @"
UPDATE SponsorProjectMeasure SET
	SponsorProjectMeasureID = @SponsorProjectMeasureID,
	SponsorProjectID = @SponsorProjectID,
	MeasureID = @MeasureID
WHERE SponsorProjectMeasureID = @SponsorProjectMeasureID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectMeasureID", sponsorProjectMeasure.SponsorProjectMeasureID),
				new SqlParameter("@SponsorProjectID", sponsorProjectMeasure.SponsorProjectID),
				new SqlParameter("@MeasureID", sponsorProjectMeasure.MeasureID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorProjectMeasure
WHERE SponsorProjectMeasureID = @SponsorProjectMeasureID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorProjectMeasureID", id)
			);
		}
		
		public override SponsorProjectMeasure Read(int id)
		{
			string query = @"
SELECT 	SponsorProjectMeasureID, 
	SponsorProjectID, 
	MeasureID
FROM SponsorProjectMeasure
WHERE SponsorProjectMeasureID = @SponsorProjectMeasureID";
			SponsorProjectMeasure sponsorProjectMeasure = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorProjectMeasureID", id))) {
				if (rs.Read()) {
					sponsorProjectMeasure = new SponsorProjectMeasure {
						SponsorProjectMeasureID = GetInt32(rs, 0),
						SponsorProjectID = GetInt32(rs, 1),
						MeasureID = GetInt32(rs, 2)
					};
				}
			}
			return sponsorProjectMeasure;
		}
		
		public override IList<SponsorProjectMeasure> FindAll()
		{
			string query = @"
SELECT 	SponsorProjectMeasureID, 
	SponsorProjectID, 
	MeasureID
FROM SponsorProjectMeasure";
			var sponsorProjectMeasures = new List<SponsorProjectMeasure>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorProjectMeasures.Add(new SponsorProjectMeasure {
						SponsorProjectMeasureID = GetInt32(rs, 0),
						SponsorProjectID = GetInt32(rs, 1),
						MeasureID = GetInt32(rs, 2)
					});
				}
			}
			return sponsorProjectMeasures;
		}
	}
}
