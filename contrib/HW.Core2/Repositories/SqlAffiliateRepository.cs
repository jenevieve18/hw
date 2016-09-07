using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlAffiliateRepository : BaseSqlRepository<Affiliate>
	{
		public SqlAffiliateRepository()
		{
		}
		
		public override void Save(Affiliate affiliate)
		{
			string query = @"
INSERT INTO Affiliate(
	AffiliateID, 
	Affiliate
)
VALUES(
	@AffiliateID, 
	@Affiliate
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AffiliateID", affiliate.AffiliateID),
				new SqlParameter("@Affiliate", affiliate.AffiliateText)
			);
		}
		
		public override void Update(Affiliate affiliate, int id)
		{
			string query = @"
UPDATE Affiliate SET
	AffiliateID = @AffiliateID,
	Affiliate = @Affiliate
WHERE AffiliateID = @AffiliateID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AffiliateID", affiliate.AffiliateID),
				new SqlParameter("@Affiliate", affiliate.AffiliateText)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM Affiliate
WHERE AffiliateID = @AffiliateID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@AffiliateID", id)
			);
		}
		
		public override Affiliate Read(int id)
		{
			string query = @"
SELECT 	AffiliateID, 
	Affiliate
FROM Affiliate
WHERE AffiliateID = @AffiliateID";
			Affiliate affiliate = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@AffiliateID", id))) {
				if (rs.Read()) {
					affiliate = new Affiliate {
						AffiliateID = GetInt32(rs, 0),
						AffiliateText = GetString(rs, 1)
					};
				}
			}
			return affiliate;
		}
		
		public override IList<Affiliate> FindAll()
		{
			string query = @"
SELECT 	AffiliateID, 
	Affiliate
FROM Affiliate";
			var affiliates = new List<Affiliate>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					affiliates.Add(new Affiliate {
						AffiliateID = GetInt32(rs, 0),
						AffiliateText = GetString(rs, 1)
					});
				}
			}
			return affiliates;
		}
	}
}
