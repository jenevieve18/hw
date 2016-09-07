using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorInviteRepository : BaseSqlRepository<SponsorInvite>
	{
		public SqlSponsorInviteRepository()
		{
		}
		
		public override void Save(SponsorInvite sponsorInvite)
		{
			string query = @"
INSERT INTO SponsorInvite(
	SponsorInviteID, 
	SponsorID, 
	DepartmentID, 
	Email, 
	UserID, 
	Sent, 
	InvitationKey, 
	Consent, 
	Stopped, 
	StoppedReason, 
	PreviewExtendedSurveys, 
	StoppedPercent
)
VALUES(
	@SponsorInviteID, 
	@SponsorID, 
	@DepartmentID, 
	@Email, 
	@UserID, 
	@Sent, 
	@InvitationKey, 
	@Consent, 
	@Stopped, 
	@StoppedReason, 
	@PreviewExtendedSurveys, 
	@StoppedPercent
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorInviteID", sponsorInvite.SponsorInviteID),
				new SqlParameter("@SponsorID", sponsorInvite.SponsorID),
				new SqlParameter("@DepartmentID", sponsorInvite.DepartmentID),
				new SqlParameter("@Email", sponsorInvite.Email),
				new SqlParameter("@UserID", sponsorInvite.UserID),
				new SqlParameter("@Sent", sponsorInvite.Sent),
				new SqlParameter("@InvitationKey", sponsorInvite.InvitationKey),
				new SqlParameter("@Consent", sponsorInvite.Consent),
				new SqlParameter("@Stopped", sponsorInvite.Stopped),
				new SqlParameter("@StoppedReason", sponsorInvite.StoppedReason),
				new SqlParameter("@PreviewExtendedSurveys", sponsorInvite.PreviewExtendedSurveys),
				new SqlParameter("@StoppedPercent", sponsorInvite.StoppedPercent)
			);
		}
		
		public override void Update(SponsorInvite sponsorInvite, int id)
		{
			string query = @"
UPDATE SponsorInvite SET
	SponsorInviteID = @SponsorInviteID,
	SponsorID = @SponsorID,
	DepartmentID = @DepartmentID,
	Email = @Email,
	UserID = @UserID,
	Sent = @Sent,
	InvitationKey = @InvitationKey,
	Consent = @Consent,
	Stopped = @Stopped,
	StoppedReason = @StoppedReason,
	PreviewExtendedSurveys = @PreviewExtendedSurveys,
	StoppedPercent = @StoppedPercent
WHERE SponsorInviteID = @SponsorInviteID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorInviteID", sponsorInvite.SponsorInviteID),
				new SqlParameter("@SponsorID", sponsorInvite.SponsorID),
				new SqlParameter("@DepartmentID", sponsorInvite.DepartmentID),
				new SqlParameter("@Email", sponsorInvite.Email),
				new SqlParameter("@UserID", sponsorInvite.UserID),
				new SqlParameter("@Sent", sponsorInvite.Sent),
				new SqlParameter("@InvitationKey", sponsorInvite.InvitationKey),
				new SqlParameter("@Consent", sponsorInvite.Consent),
				new SqlParameter("@Stopped", sponsorInvite.Stopped),
				new SqlParameter("@StoppedReason", sponsorInvite.StoppedReason),
				new SqlParameter("@PreviewExtendedSurveys", sponsorInvite.PreviewExtendedSurveys),
				new SqlParameter("@StoppedPercent", sponsorInvite.StoppedPercent)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorInvite
WHERE SponsorInviteID = @SponsorInviteID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorInviteID", id)
			);
		}
		
		public override SponsorInvite Read(int id)
		{
			string query = @"
SELECT 	SponsorInviteID, 
	SponsorID, 
	DepartmentID, 
	Email, 
	UserID, 
	Sent, 
	InvitationKey, 
	Consent, 
	Stopped, 
	StoppedReason, 
	PreviewExtendedSurveys, 
	StoppedPercent
FROM SponsorInvite
WHERE SponsorInviteID = @SponsorInviteID";
			SponsorInvite sponsorInvite = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorInviteID", id))) {
				if (rs.Read()) {
					sponsorInvite = new SponsorInvite {
						SponsorInviteID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2),
						Email = GetString(rs, 3),
						UserID = GetInt32(rs, 4),
						Sent = GetDateTime(rs, 5),
						InvitationKey = GetGuid(rs, 6),
						Consent = GetDateTime(rs, 7),
						Stopped = GetDateTime(rs, 8),
						StoppedReason = GetInt32(rs, 9),
						PreviewExtendedSurveys = GetInt32(rs, 10),
						StoppedPercent = GetInt32(rs, 11)
					};
				}
			}
			return sponsorInvite;
		}
		
		public override IList<SponsorInvite> FindAll()
		{
			string query = @"
SELECT 	SponsorInviteID, 
	SponsorID, 
	DepartmentID, 
	Email, 
	UserID, 
	Sent, 
	InvitationKey, 
	Consent, 
	Stopped, 
	StoppedReason, 
	PreviewExtendedSurveys, 
	StoppedPercent
FROM SponsorInvite";
			var sponsorInvites = new List<SponsorInvite>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorInvites.Add(new SponsorInvite {
						SponsorInviteID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						DepartmentID = GetInt32(rs, 2),
						Email = GetString(rs, 3),
						UserID = GetInt32(rs, 4),
						Sent = GetDateTime(rs, 5),
						InvitationKey = GetGuid(rs, 6),
						Consent = GetDateTime(rs, 7),
						Stopped = GetDateTime(rs, 8),
						StoppedReason = GetInt32(rs, 9),
						PreviewExtendedSurveys = GetInt32(rs, 10),
						StoppedPercent = GetInt32(rs, 11)
					});
				}
			}
			return sponsorInvites;
		}
	}
}
