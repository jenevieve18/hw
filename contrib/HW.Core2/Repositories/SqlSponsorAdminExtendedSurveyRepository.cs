using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorAdminExtendedSurveyRepository : BaseSqlRepository<SponsorAdminExtendedSurvey>
	{
		public SqlSponsorAdminExtendedSurveyRepository()
		{
		}
		
		public override void Save(SponsorAdminExtendedSurvey sponsorAdminExtendedSurvey)
		{
			string query = @"
INSERT INTO SponsorAdminExtendedSurvey(
	SponsorAdminExtendedSurveyID, 
	SponsorAdminID, 
	EmailSubject, 
	EmailBody, 
	FinishedEmailSubject, 
	FinishedEmailBody, 
	EmailLastSent, 
	FinishedLastSent, 
	SponsorExtendedSurveyID
)
VALUES(
	@SponsorAdminExtendedSurveyID, 
	@SponsorAdminID, 
	@EmailSubject, 
	@EmailBody, 
	@FinishedEmailSubject, 
	@FinishedEmailBody, 
	@EmailLastSent, 
	@FinishedLastSent, 
	@SponsorExtendedSurveyID
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExtendedSurveyID", sponsorAdminExtendedSurvey.SponsorAdminExtendedSurveyID),
				new SqlParameter("@SponsorAdminID", sponsorAdminExtendedSurvey.SponsorAdminID),
				new SqlParameter("@EmailSubject", sponsorAdminExtendedSurvey.EmailSubject),
				new SqlParameter("@EmailBody", sponsorAdminExtendedSurvey.EmailBody),
				new SqlParameter("@FinishedEmailSubject", sponsorAdminExtendedSurvey.FinishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", sponsorAdminExtendedSurvey.FinishedEmailBody),
				new SqlParameter("@EmailLastSent", sponsorAdminExtendedSurvey.EmailLastSent),
				new SqlParameter("@FinishedLastSent", sponsorAdminExtendedSurvey.FinishedLastSent),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorAdminExtendedSurvey.SponsorExtendedSurveyID)
			);
		}
		
		public override void Update(SponsorAdminExtendedSurvey sponsorAdminExtendedSurvey, int id)
		{
			string query = @"
UPDATE SponsorAdminExtendedSurvey SET
	SponsorAdminExtendedSurveyID = @SponsorAdminExtendedSurveyID,
	SponsorAdminID = @SponsorAdminID,
	EmailSubject = @EmailSubject,
	EmailBody = @EmailBody,
	FinishedEmailSubject = @FinishedEmailSubject,
	FinishedEmailBody = @FinishedEmailBody,
	EmailLastSent = @EmailLastSent,
	FinishedLastSent = @FinishedLastSent,
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID
WHERE SponsorAdminExtendedSurveyID = @SponsorAdminExtendedSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExtendedSurveyID", sponsorAdminExtendedSurvey.SponsorAdminExtendedSurveyID),
				new SqlParameter("@SponsorAdminID", sponsorAdminExtendedSurvey.SponsorAdminID),
				new SqlParameter("@EmailSubject", sponsorAdminExtendedSurvey.EmailSubject),
				new SqlParameter("@EmailBody", sponsorAdminExtendedSurvey.EmailBody),
				new SqlParameter("@FinishedEmailSubject", sponsorAdminExtendedSurvey.FinishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", sponsorAdminExtendedSurvey.FinishedEmailBody),
				new SqlParameter("@EmailLastSent", sponsorAdminExtendedSurvey.EmailLastSent),
				new SqlParameter("@FinishedLastSent", sponsorAdminExtendedSurvey.FinishedLastSent),
				new SqlParameter("@SponsorExtendedSurveyID", sponsorAdminExtendedSurvey.SponsorExtendedSurveyID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorAdminExtendedSurvey
WHERE SponsorAdminExtendedSurveyID = @SponsorAdminExtendedSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorAdminExtendedSurveyID", id)
			);
		}
		
		public override SponsorAdminExtendedSurvey Read(int id)
		{
			string query = @"
SELECT 	SponsorAdminExtendedSurveyID, 
	SponsorAdminID, 
	EmailSubject, 
	EmailBody, 
	FinishedEmailSubject, 
	FinishedEmailBody, 
	EmailLastSent, 
	FinishedLastSent, 
	SponsorExtendedSurveyID
FROM SponsorAdminExtendedSurvey
WHERE SponsorAdminExtendedSurveyID = @SponsorAdminExtendedSurveyID";
			SponsorAdminExtendedSurvey sponsorAdminExtendedSurvey = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorAdminExtendedSurveyID", id))) {
				if (rs.Read()) {
					sponsorAdminExtendedSurvey = new SponsorAdminExtendedSurvey {
						SponsorAdminExtendedSurveyID = GetInt32(rs, 0),
						SponsorAdminID = GetInt32(rs, 1),
						EmailSubject = GetString(rs, 2),
						EmailBody = GetString(rs, 3),
						FinishedEmailSubject = GetString(rs, 4),
						FinishedEmailBody = GetString(rs, 5),
						EmailLastSent = GetDateTime(rs, 6),
						FinishedLastSent = GetDateTime(rs, 7),
						SponsorExtendedSurveyID = GetInt32(rs, 8)
					};
				}
			}
			return sponsorAdminExtendedSurvey;
		}
		
		public override IList<SponsorAdminExtendedSurvey> FindAll()
		{
			string query = @"
SELECT 	SponsorAdminExtendedSurveyID, 
	SponsorAdminID, 
	EmailSubject, 
	EmailBody, 
	FinishedEmailSubject, 
	FinishedEmailBody, 
	EmailLastSent, 
	FinishedLastSent, 
	SponsorExtendedSurveyID
FROM SponsorAdminExtendedSurvey";
			var sponsorAdminExtendedSurveys = new List<SponsorAdminExtendedSurvey>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorAdminExtendedSurveys.Add(new SponsorAdminExtendedSurvey {
						SponsorAdminExtendedSurveyID = GetInt32(rs, 0),
						SponsorAdminID = GetInt32(rs, 1),
						EmailSubject = GetString(rs, 2),
						EmailBody = GetString(rs, 3),
						FinishedEmailSubject = GetString(rs, 4),
						FinishedEmailBody = GetString(rs, 5),
						EmailLastSent = GetDateTime(rs, 6),
						FinishedLastSent = GetDateTime(rs, 7),
						SponsorExtendedSurveyID = GetInt32(rs, 8)
					});
				}
			}
			return sponsorAdminExtendedSurveys;
		}
	}
}
