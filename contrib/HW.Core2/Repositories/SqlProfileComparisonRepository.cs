using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlProfileComparisonRepository : BaseSqlRepository<ProfileComparison>
	{
		public SqlProfileComparisonRepository()
		{
		}
		
		public override void Save(ProfileComparison profileComparison)
		{
			string query = @"
INSERT INTO ProfileComparison(
	ProfileComparisonID, 
	Hash
)
VALUES(
	@ProfileComparisonID, 
	@Hash
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProfileComparisonID", profileComparison.ProfileComparisonID),
				new SqlParameter("@Hash", profileComparison.Hash)
			);
		}
		
		public override void Update(ProfileComparison profileComparison, int id)
		{
			string query = @"
UPDATE ProfileComparison SET
	ProfileComparisonID = @ProfileComparisonID,
	Hash = @Hash
WHERE ProfileComparisonID = @ProfileComparisonID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProfileComparisonID", profileComparison.ProfileComparisonID),
				new SqlParameter("@Hash", profileComparison.Hash)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProfileComparison
WHERE ProfileComparisonID = @ProfileComparisonID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProfileComparisonID", id)
			);
		}
		
		public override ProfileComparison Read(int id)
		{
			string query = @"
SELECT 	ProfileComparisonID, 
	Hash
FROM ProfileComparison
WHERE ProfileComparisonID = @ProfileComparisonID";
			ProfileComparison profileComparison = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProfileComparisonID", id))) {
				if (rs.Read()) {
					profileComparison = new ProfileComparison {
						ProfileComparisonID = GetInt32(rs, 0),
						Hash = GetString(rs, 1)
					};
				}
			}
			return profileComparison;
		}
		
		public override IList<ProfileComparison> FindAll()
		{
			string query = @"
SELECT 	ProfileComparisonID, 
	Hash
FROM ProfileComparison";
			var profileComparisons = new List<ProfileComparison>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					profileComparisons.Add(new ProfileComparison {
						ProfileComparisonID = GetInt32(rs, 0),
						Hash = GetString(rs, 1)
					});
				}
			}
			return profileComparisons;
		}
	}
}
