using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlUserProfileRepository : BaseSqlRepository<UserProfile>
	{
		public SqlUserProfileRepository()
		{
		}
		
		public override void Save(UserProfile userProfile)
		{
			string query = @"
INSERT INTO UserProfile(
	UserProfileID, 
	UserID, 
	SponsorID, 
	DepartmentID, 
	ProfileComparisonID, 
	Created
)
VALUES(
	@UserProfileID, 
	@UserID, 
	@SponsorID, 
	@DepartmentID, 
	@ProfileComparisonID, 
	@Created
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProfileID", userProfile.UserProfileID),
				new SqlParameter("@UserID", userProfile.UserID),
				new SqlParameter("@SponsorID", userProfile.SponsorID),
				new SqlParameter("@DepartmentID", userProfile.DepartmentID),
				new SqlParameter("@ProfileComparisonID", userProfile.ProfileComparisonID),
				new SqlParameter("@Created", userProfile.Created)
			);
		}
		
		public override void Update(UserProfile userProfile, int id)
		{
			string query = @"
UPDATE UserProfile SET
	UserProfileID = @UserProfileID,
	UserID = @UserID,
	SponsorID = @SponsorID,
	DepartmentID = @DepartmentID,
	ProfileComparisonID = @ProfileComparisonID,
	Created = @Created
WHERE UserProfileID = @UserProfileID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProfileID", userProfile.UserProfileID),
				new SqlParameter("@UserID", userProfile.UserID),
				new SqlParameter("@SponsorID", userProfile.SponsorID),
				new SqlParameter("@DepartmentID", userProfile.DepartmentID),
				new SqlParameter("@ProfileComparisonID", userProfile.ProfileComparisonID),
				new SqlParameter("@Created", userProfile.Created)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM UserProfile
WHERE UserProfileID = @UserProfileID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@UserProfileID", id)
			);
		}
		
		public override UserProfile Read(int id)
		{
			string query = @"
SELECT 	UserProfileID, 
	UserID, 
	SponsorID, 
	DepartmentID, 
	ProfileComparisonID, 
	Created
FROM UserProfile
WHERE UserProfileID = @UserProfileID";
			UserProfile userProfile = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@UserProfileID", id))) {
				if (rs.Read()) {
					userProfile = new UserProfile {
						UserProfileID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2),
						DepartmentID = GetInt32(rs, 3),
						ProfileComparisonID = GetInt32(rs, 4),
						Created = GetDateTime(rs, 5)
					};
				}
			}
			return userProfile;
		}
		
		public override IList<UserProfile> FindAll()
		{
			string query = @"
SELECT 	UserProfileID, 
	UserID, 
	SponsorID, 
	DepartmentID, 
	ProfileComparisonID, 
	Created
FROM UserProfile";
			var userProfiles = new List<UserProfile>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					userProfiles.Add(new UserProfile {
						UserProfileID = GetInt32(rs, 0),
						UserID = GetInt32(rs, 1),
						SponsorID = GetInt32(rs, 2),
						DepartmentID = GetInt32(rs, 3),
						ProfileComparisonID = GetInt32(rs, 4),
						Created = GetDateTime(rs, 5)
					});
				}
			}
			return userProfiles;
		}
	}
}
