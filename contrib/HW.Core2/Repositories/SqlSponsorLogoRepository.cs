using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorLogoRepository : BaseSqlRepository<SponsorLogo>
	{
		public SqlSponsorLogoRepository()
		{
		}
		
		public override void Save(SponsorLogo sponsorLogo)
		{
			string query = @"
INSERT INTO SponsorLogo(
	SponsorLogoID, 
	SponsorID, 
	URL
)
VALUES(
	@SponsorLogoID, 
	@SponsorID, 
	@URL
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorLogoID", sponsorLogo.SponsorLogoID),
				new SqlParameter("@SponsorID", sponsorLogo.SponsorID),
				new SqlParameter("@URL", sponsorLogo.URL)
			);
		}
		
		public override void Update(SponsorLogo sponsorLogo, int id)
		{
			string query = @"
UPDATE SponsorLogo SET
	SponsorLogoID = @SponsorLogoID,
	SponsorID = @SponsorID,
	URL = @URL
WHERE SponsorLogoID = @SponsorLogoID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorLogoID", sponsorLogo.SponsorLogoID),
				new SqlParameter("@SponsorID", sponsorLogo.SponsorID),
				new SqlParameter("@URL", sponsorLogo.URL)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorLogo
WHERE SponsorLogoID = @SponsorLogoID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorLogoID", id)
			);
		}
		
		public override SponsorLogo Read(int id)
		{
			string query = @"
SELECT 	SponsorLogoID, 
	SponsorID, 
	URL
FROM SponsorLogo
WHERE SponsorLogoID = @SponsorLogoID";
			SponsorLogo sponsorLogo = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorLogoID", id))) {
				if (rs.Read()) {
					sponsorLogo = new SponsorLogo {
						SponsorLogoID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						URL = GetString(rs, 2)
					};
				}
			}
			return sponsorLogo;
		}
		
		public override IList<SponsorLogo> FindAll()
		{
			string query = @"
SELECT 	SponsorLogoID, 
	SponsorID, 
	URL
FROM SponsorLogo";
			var sponsorLogos = new List<SponsorLogo>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorLogos.Add(new SponsorLogo {
						SponsorLogoID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						URL = GetString(rs, 2)
					});
				}
			}
			return sponsorLogos;
		}
	}
}
