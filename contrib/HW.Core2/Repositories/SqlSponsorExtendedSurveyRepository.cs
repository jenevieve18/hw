using System;
using HW.Core2.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.Core2.Repositories
{
	public class SqlSponsorExtendedSurveyRepository : BaseSqlRepository<SponsorExtendedSurvey>
	{
		public SqlSponsorExtendedSurveyRepository()
		{
		}
		
		public override void Save(SponsorExtendedSurvey sponsorExtendedSurvey)
		{
			string query = @"
INSERT INTO SponsorExtendedSurvey(
	SponsorExtendedSurveyID, 
	SponsorID, 
	ProjectRoundID, 
	Internal, 
	EformFeedbackID, 
	RequiredUserCount, 
	PreviousProjectRoundID, 
	RoundText, 
	EmailSubject, 
	EmailBody, 
	EmailLastSent, 
	IndividualFeedbackID, 
	IndividualFeedbackEmailSubject, 
	IndividualFeedbackEmailBody, 
	WarnIfMissingQID, 
	ExtraEmailSubject, 
	ExtraEmailBody, 
	FinishedEmailSubject, 
	FinishedEmailBody, 
	FinishedLastSent, 
	Total, 
	Answers
)
VALUES(
	@SponsorExtendedSurveyID, 
	@SponsorID, 
	@ProjectRoundID, 
	@Internal, 
	@EformFeedbackID, 
	@RequiredUserCount, 
	@PreviousProjectRoundID, 
	@RoundText, 
	@EmailSubject, 
	@EmailBody, 
	@EmailLastSent, 
	@IndividualFeedbackID, 
	@IndividualFeedbackEmailSubject, 
	@IndividualFeedbackEmailBody, 
	@WarnIfMissingQID, 
	@ExtraEmailSubject, 
	@ExtraEmailBody, 
	@FinishedEmailSubject, 
	@FinishedEmailBody, 
	@FinishedLastSent, 
	@Total, 
	@Answers
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurvey.SponsorExtendedSurveyID),
				new SqlParameter("@SponsorID", sponsorExtendedSurvey.SponsorID),
				new SqlParameter("@ProjectRoundID", sponsorExtendedSurvey.ProjectRoundID),
				new SqlParameter("@Internal", sponsorExtendedSurvey.Internal),
				new SqlParameter("@EformFeedbackID", sponsorExtendedSurvey.EformFeedbackID),
				new SqlParameter("@RequiredUserCount", sponsorExtendedSurvey.RequiredUserCount),
				new SqlParameter("@PreviousProjectRoundID", sponsorExtendedSurvey.PreviousProjectRoundID),
				new SqlParameter("@RoundText", sponsorExtendedSurvey.RoundText),
				new SqlParameter("@EmailSubject", sponsorExtendedSurvey.EmailSubject),
				new SqlParameter("@EmailBody", sponsorExtendedSurvey.EmailBody),
				new SqlParameter("@EmailLastSent", sponsorExtendedSurvey.EmailLastSent),
				new SqlParameter("@IndividualFeedbackID", sponsorExtendedSurvey.IndividualFeedbackID),
				new SqlParameter("@IndividualFeedbackEmailSubject", sponsorExtendedSurvey.IndividualFeedbackEmailSubject),
				new SqlParameter("@IndividualFeedbackEmailBody", sponsorExtendedSurvey.IndividualFeedbackEmailBody),
				new SqlParameter("@WarnIfMissingQID", sponsorExtendedSurvey.WarnIfMissingQID),
				new SqlParameter("@ExtraEmailSubject", sponsorExtendedSurvey.ExtraEmailSubject),
				new SqlParameter("@ExtraEmailBody", sponsorExtendedSurvey.ExtraEmailBody),
				new SqlParameter("@FinishedEmailSubject", sponsorExtendedSurvey.FinishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", sponsorExtendedSurvey.FinishedEmailBody),
				new SqlParameter("@FinishedLastSent", sponsorExtendedSurvey.FinishedLastSent),
				new SqlParameter("@Total", sponsorExtendedSurvey.Total),
				new SqlParameter("@Answers", sponsorExtendedSurvey.Answers)
			);
		}
		
		public override void Update(SponsorExtendedSurvey sponsorExtendedSurvey, int id)
		{
			string query = @"
UPDATE SponsorExtendedSurvey SET
	SponsorExtendedSurveyID = @SponsorExtendedSurveyID,
	SponsorID = @SponsorID,
	ProjectRoundID = @ProjectRoundID,
	Internal = @Internal,
	EformFeedbackID = @EformFeedbackID,
	RequiredUserCount = @RequiredUserCount,
	PreviousProjectRoundID = @PreviousProjectRoundID,
	RoundText = @RoundText,
	EmailSubject = @EmailSubject,
	EmailBody = @EmailBody,
	EmailLastSent = @EmailLastSent,
	IndividualFeedbackID = @IndividualFeedbackID,
	IndividualFeedbackEmailSubject = @IndividualFeedbackEmailSubject,
	IndividualFeedbackEmailBody = @IndividualFeedbackEmailBody,
	WarnIfMissingQID = @WarnIfMissingQID,
	ExtraEmailSubject = @ExtraEmailSubject,
	ExtraEmailBody = @ExtraEmailBody,
	FinishedEmailSubject = @FinishedEmailSubject,
	FinishedEmailBody = @FinishedEmailBody,
	FinishedLastSent = @FinishedLastSent,
	Total = @Total,
	Answers = @Answers
WHERE SponsorExtendedSurveyID = @SponsorExtendedSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyID", sponsorExtendedSurvey.SponsorExtendedSurveyID),
				new SqlParameter("@SponsorID", sponsorExtendedSurvey.SponsorID),
				new SqlParameter("@ProjectRoundID", sponsorExtendedSurvey.ProjectRoundID),
				new SqlParameter("@Internal", sponsorExtendedSurvey.Internal),
				new SqlParameter("@EformFeedbackID", sponsorExtendedSurvey.EformFeedbackID),
				new SqlParameter("@RequiredUserCount", sponsorExtendedSurvey.RequiredUserCount),
				new SqlParameter("@PreviousProjectRoundID", sponsorExtendedSurvey.PreviousProjectRoundID),
				new SqlParameter("@RoundText", sponsorExtendedSurvey.RoundText),
				new SqlParameter("@EmailSubject", sponsorExtendedSurvey.EmailSubject),
				new SqlParameter("@EmailBody", sponsorExtendedSurvey.EmailBody),
				new SqlParameter("@EmailLastSent", sponsorExtendedSurvey.EmailLastSent),
				new SqlParameter("@IndividualFeedbackID", sponsorExtendedSurvey.IndividualFeedbackID),
				new SqlParameter("@IndividualFeedbackEmailSubject", sponsorExtendedSurvey.IndividualFeedbackEmailSubject),
				new SqlParameter("@IndividualFeedbackEmailBody", sponsorExtendedSurvey.IndividualFeedbackEmailBody),
				new SqlParameter("@WarnIfMissingQID", sponsorExtendedSurvey.WarnIfMissingQID),
				new SqlParameter("@ExtraEmailSubject", sponsorExtendedSurvey.ExtraEmailSubject),
				new SqlParameter("@ExtraEmailBody", sponsorExtendedSurvey.ExtraEmailBody),
				new SqlParameter("@FinishedEmailSubject", sponsorExtendedSurvey.FinishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", sponsorExtendedSurvey.FinishedEmailBody),
				new SqlParameter("@FinishedLastSent", sponsorExtendedSurvey.FinishedLastSent),
				new SqlParameter("@Total", sponsorExtendedSurvey.Total),
				new SqlParameter("@Answers", sponsorExtendedSurvey.Answers)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM SponsorExtendedSurvey
WHERE SponsorExtendedSurveyID = @SponsorExtendedSurveyID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@SponsorExtendedSurveyID", id)
			);
		}
		
		public override SponsorExtendedSurvey Read(int id)
		{
			string query = @"
SELECT 	SponsorExtendedSurveyID, 
	SponsorID, 
	ProjectRoundID, 
	Internal, 
	EformFeedbackID, 
	RequiredUserCount, 
	PreviousProjectRoundID, 
	RoundText, 
	EmailSubject, 
	EmailBody, 
	EmailLastSent, 
	IndividualFeedbackID, 
	IndividualFeedbackEmailSubject, 
	IndividualFeedbackEmailBody, 
	WarnIfMissingQID, 
	ExtraEmailSubject, 
	ExtraEmailBody, 
	FinishedEmailSubject, 
	FinishedEmailBody, 
	FinishedLastSent, 
	Total, 
	Answers
FROM SponsorExtendedSurvey
WHERE SponsorExtendedSurveyID = @SponsorExtendedSurveyID";
			SponsorExtendedSurvey sponsorExtendedSurvey = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@SponsorExtendedSurveyID", id))) {
				if (rs.Read()) {
					sponsorExtendedSurvey = new SponsorExtendedSurvey {
						SponsorExtendedSurveyID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundID = GetInt32(rs, 2),
						Internal = GetString(rs, 3),
						EformFeedbackID = GetInt32(rs, 4),
						RequiredUserCount = GetInt32(rs, 5),
						PreviousProjectRoundID = GetInt32(rs, 6),
						RoundText = GetString(rs, 7),
						EmailSubject = GetString(rs, 8),
						EmailBody = GetString(rs, 9),
						EmailLastSent = GetString(rs, 10),
						IndividualFeedbackID = GetInt32(rs, 11),
						IndividualFeedbackEmailSubject = GetString(rs, 12),
						IndividualFeedbackEmailBody = GetString(rs, 13),
						WarnIfMissingQID = GetInt32(rs, 14),
						ExtraEmailSubject = GetString(rs, 15),
						ExtraEmailBody = GetString(rs, 16),
						FinishedEmailSubject = GetString(rs, 17),
						FinishedEmailBody = GetString(rs, 18),
						FinishedLastSent = GetString(rs, 19),
						Total = GetInt32(rs, 20),
						Answers = GetInt32(rs, 21)
					};
				}
			}
			return sponsorExtendedSurvey;
		}
		
		public override IList<SponsorExtendedSurvey> FindAll()
		{
			string query = @"
SELECT 	SponsorExtendedSurveyID, 
	SponsorID, 
	ProjectRoundID, 
	Internal, 
	EformFeedbackID, 
	RequiredUserCount, 
	PreviousProjectRoundID, 
	RoundText, 
	EmailSubject, 
	EmailBody, 
	EmailLastSent, 
	IndividualFeedbackID, 
	IndividualFeedbackEmailSubject, 
	IndividualFeedbackEmailBody, 
	WarnIfMissingQID, 
	ExtraEmailSubject, 
	ExtraEmailBody, 
	FinishedEmailSubject, 
	FinishedEmailBody, 
	FinishedLastSent, 
	Total, 
	Answers
FROM SponsorExtendedSurvey";
			var sponsorExtendedSurveys = new List<SponsorExtendedSurvey>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					sponsorExtendedSurveys.Add(new SponsorExtendedSurvey {
						SponsorExtendedSurveyID = GetInt32(rs, 0),
						SponsorID = GetInt32(rs, 1),
						ProjectRoundID = GetInt32(rs, 2),
						Internal = GetString(rs, 3),
						EformFeedbackID = GetInt32(rs, 4),
						RequiredUserCount = GetInt32(rs, 5),
						PreviousProjectRoundID = GetInt32(rs, 6),
						RoundText = GetString(rs, 7),
						EmailSubject = GetString(rs, 8),
						EmailBody = GetString(rs, 9),
						EmailLastSent = GetString(rs, 10),
						IndividualFeedbackID = GetInt32(rs, 11),
						IndividualFeedbackEmailSubject = GetString(rs, 12),
						IndividualFeedbackEmailBody = GetString(rs, 13),
						WarnIfMissingQID = GetInt32(rs, 14),
						ExtraEmailSubject = GetString(rs, 15),
						ExtraEmailBody = GetString(rs, 16),
						FinishedEmailSubject = GetString(rs, 17),
						FinishedEmailBody = GetString(rs, 18),
						FinishedLastSent = GetString(rs, 19),
						Total = GetInt32(rs, 20),
						Answers = GetInt32(rs, 21)
					});
				}
			}
			return sponsorExtendedSurveys;
		}
	}
}
