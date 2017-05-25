using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlProjectRoundRepository : BaseSqlRepository<ProjectRound>
	{
		public SqlProjectRoundRepository()
		{
		}
		
		public override void Save(ProjectRound projectRound)
		{
			string query = @"
INSERT INTO ProjectRound(
	ProjectRoundID, 
	ProjectID, 
	Internal, 
	Started, 
	Closed, 
	TransparencyLevel, 
	RepeatedEntry, 
	SurveyID, 
	LangID, 
	RoundKey, 
	EmailFromAddress, 
	ReminderInterval, 
	Layout, 
	SelfRegistration, 
	Timeframe, 
	Yellow, 
	Green, 
	IndividualReportID, 
	ExtendedSurveyID, 
	ReportID, 
	Logo, 
	UseCode, 
	ConfidentialIndividualReportID, 
	SendSurveyAsEmail, 
	SFTPhost, 
	SFTPpath, 
	SFTPuser, 
	SFTPpass, 
	SendSurveyAsPdfTo, 
	SendSurveyAsPdfToQ, 
	SendSurveyAsPdfToO, 
	AdHocReportCompareWithParent, 
	FeedBackID
)
VALUES(
	@ProjectRoundID, 
	@ProjectID, 
	@Internal, 
	@Started, 
	@Closed, 
	@TransparencyLevel, 
	@RepeatedEntry, 
	@SurveyID, 
	@LangID, 
	@RoundKey, 
	@EmailFromAddress, 
	@ReminderInterval, 
	@Layout, 
	@SelfRegistration, 
	@Timeframe, 
	@Yellow, 
	@Green, 
	@IndividualReportID, 
	@ExtendedSurveyID, 
	@ReportID, 
	@Logo, 
	@UseCode, 
	@ConfidentialIndividualReportID, 
	@SendSurveyAsEmail, 
	@SFTPhost, 
	@SFTPpath, 
	@SFTPuser, 
	@SFTPpass, 
	@SendSurveyAsPdfTo, 
	@SendSurveyAsPdfToQ, 
	@SendSurveyAsPdfToO, 
	@AdHocReportCompareWithParent, 
	@FeedBackID
)";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ProjectRoundID", projectRound.ProjectRoundID),
				new SqlParameter("@ProjectID", projectRound.ProjectID),
				new SqlParameter("@Internal", projectRound.Internal),
				new SqlParameter("@Started", projectRound.Started),
				new SqlParameter("@Closed", projectRound.Closed),
				new SqlParameter("@TransparencyLevel", projectRound.TransparencyLevel),
				new SqlParameter("@RepeatedEntry", projectRound.RepeatedEntry),
				new SqlParameter("@SurveyID", projectRound.SurveyID),
				new SqlParameter("@LangID", projectRound.LangID),
				new SqlParameter("@RoundKey", projectRound.RoundKey),
				new SqlParameter("@EmailFromAddress", projectRound.EmailFromAddress),
				new SqlParameter("@ReminderInterval", projectRound.ReminderInterval),
				new SqlParameter("@Layout", projectRound.Layout),
				new SqlParameter("@SelfRegistration", projectRound.SelfRegistration),
				new SqlParameter("@Timeframe", projectRound.Timeframe),
				new SqlParameter("@Yellow", projectRound.Yellow),
				new SqlParameter("@Green", projectRound.Green),
				new SqlParameter("@IndividualReportID", projectRound.IndividualReportID),
				new SqlParameter("@ExtendedSurveyID", projectRound.ExtendedSurveyID),
				new SqlParameter("@ReportID", projectRound.ReportID),
				new SqlParameter("@Logo", projectRound.Logo),
				new SqlParameter("@UseCode", projectRound.UseCode),
				new SqlParameter("@ConfidentialIndividualReportID", projectRound.ConfidentialIndividualReportID),
				new SqlParameter("@SendSurveyAsEmail", projectRound.SendSurveyAsEmail),
				new SqlParameter("@SFTPhost", projectRound.SFTPhost),
				new SqlParameter("@SFTPpath", projectRound.SFTPpath),
				new SqlParameter("@SFTPuser", projectRound.SFTPuser),
				new SqlParameter("@SFTPpass", projectRound.SFTPpass),
				new SqlParameter("@SendSurveyAsPdfTo", projectRound.SendSurveyAsPdfTo),
				new SqlParameter("@SendSurveyAsPdfToQ", projectRound.SendSurveyAsPdfToQ),
				new SqlParameter("@SendSurveyAsPdfToO", projectRound.SendSurveyAsPdfToO),
				new SqlParameter("@AdHocReportCompareWithParent", projectRound.AdHocReportCompareWithParent),
				new SqlParameter("@FeedBackID", projectRound.FeedBackID)
			);
		}
		
		public override void Update(ProjectRound projectRound, int id)
		{
			string query = @"
UPDATE ProjectRound SET
	ProjectRoundID = @ProjectRoundID,
	ProjectID = @ProjectID,
	Internal = @Internal,
	Started = @Started,
	Closed = @Closed,
	TransparencyLevel = @TransparencyLevel,
	RepeatedEntry = @RepeatedEntry,
	SurveyID = @SurveyID,
	LangID = @LangID,
	RoundKey = @RoundKey,
	EmailFromAddress = @EmailFromAddress,
	ReminderInterval = @ReminderInterval,
	Layout = @Layout,
	SelfRegistration = @SelfRegistration,
	Timeframe = @Timeframe,
	Yellow = @Yellow,
	Green = @Green,
	IndividualReportID = @IndividualReportID,
	ExtendedSurveyID = @ExtendedSurveyID,
	ReportID = @ReportID,
	Logo = @Logo,
	UseCode = @UseCode,
	ConfidentialIndividualReportID = @ConfidentialIndividualReportID,
	SendSurveyAsEmail = @SendSurveyAsEmail,
	SFTPhost = @SFTPhost,
	SFTPpath = @SFTPpath,
	SFTPuser = @SFTPuser,
	SFTPpass = @SFTPpass,
	SendSurveyAsPdfTo = @SendSurveyAsPdfTo,
	SendSurveyAsPdfToQ = @SendSurveyAsPdfToQ,
	SendSurveyAsPdfToO = @SendSurveyAsPdfToO,
	AdHocReportCompareWithParent = @AdHocReportCompareWithParent,
	FeedBackID = @FeedBackID
WHERE ProjectRoundID = @ProjectRoundID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ProjectRoundID", projectRound.ProjectRoundID),
				new SqlParameter("@ProjectID", projectRound.ProjectID),
				new SqlParameter("@Internal", projectRound.Internal),
				new SqlParameter("@Started", projectRound.Started),
				new SqlParameter("@Closed", projectRound.Closed),
				new SqlParameter("@TransparencyLevel", projectRound.TransparencyLevel),
				new SqlParameter("@RepeatedEntry", projectRound.RepeatedEntry),
				new SqlParameter("@SurveyID", projectRound.SurveyID),
				new SqlParameter("@LangID", projectRound.LangID),
				new SqlParameter("@RoundKey", projectRound.RoundKey),
				new SqlParameter("@EmailFromAddress", projectRound.EmailFromAddress),
				new SqlParameter("@ReminderInterval", projectRound.ReminderInterval),
				new SqlParameter("@Layout", projectRound.Layout),
				new SqlParameter("@SelfRegistration", projectRound.SelfRegistration),
				new SqlParameter("@Timeframe", projectRound.Timeframe),
				new SqlParameter("@Yellow", projectRound.Yellow),
				new SqlParameter("@Green", projectRound.Green),
				new SqlParameter("@IndividualReportID", projectRound.IndividualReportID),
				new SqlParameter("@ExtendedSurveyID", projectRound.ExtendedSurveyID),
				new SqlParameter("@ReportID", projectRound.ReportID),
				new SqlParameter("@Logo", projectRound.Logo),
				new SqlParameter("@UseCode", projectRound.UseCode),
				new SqlParameter("@ConfidentialIndividualReportID", projectRound.ConfidentialIndividualReportID),
				new SqlParameter("@SendSurveyAsEmail", projectRound.SendSurveyAsEmail),
				new SqlParameter("@SFTPhost", projectRound.SFTPhost),
				new SqlParameter("@SFTPpath", projectRound.SFTPpath),
				new SqlParameter("@SFTPuser", projectRound.SFTPuser),
				new SqlParameter("@SFTPpass", projectRound.SFTPpass),
				new SqlParameter("@SendSurveyAsPdfTo", projectRound.SendSurveyAsPdfTo),
				new SqlParameter("@SendSurveyAsPdfToQ", projectRound.SendSurveyAsPdfToQ),
				new SqlParameter("@SendSurveyAsPdfToO", projectRound.SendSurveyAsPdfToO),
				new SqlParameter("@AdHocReportCompareWithParent", projectRound.AdHocReportCompareWithParent),
				new SqlParameter("@FeedBackID", projectRound.FeedBackID)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRound
WHERE ProjectRoundID = @ProjectRoundID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ProjectRoundID", id)
			);
		}
		
		public override ProjectRound Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundID, 
	ProjectID, 
	Internal, 
	Started, 
	Closed, 
	TransparencyLevel, 
	RepeatedEntry, 
	SurveyID, 
	LangID, 
	CONVERT(VARCHAR(255), RoundKey) RoundKey, 
	EmailFromAddress, 
	ReminderInterval, 
	Layout, 
	SelfRegistration, 
	Timeframe, 
	Yellow, 
	Green, 
	IndividualReportID, 
	ExtendedSurveyID, 
	ReportID, 
	Logo, 
	UseCode, 
	ConfidentialIndividualReportID, 
	SendSurveyAsEmail, 
	SFTPhost, 
	SFTPpath, 
	SFTPuser, 
	SFTPpass, 
	SendSurveyAsPdfTo, 
	SendSurveyAsPdfToQ, 
	SendSurveyAsPdfToO, 
	AdHocReportCompareWithParent, 
	FeedBackID
FROM ProjectRound
WHERE ProjectRoundID = @ProjectRoundID";
			ProjectRound projectRound = null;
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@ProjectRoundID", id))) {
				if (rs.Read()) {
					projectRound = new ProjectRound {
						ProjectRoundID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						Started = GetDateTime(rs, 3),
						Closed = GetDateTime(rs, 4),
						TransparencyLevel = GetInt32(rs, 5),
						RepeatedEntry = GetInt32(rs, 6),
						SurveyID = GetInt32(rs, 7),
						LangID = GetInt32(rs, 8),
//						RoundKey = GetGuid(rs, 9),
						RoundKey = GetString(rs, 9),
						EmailFromAddress = GetString(rs, 10),
						ReminderInterval = GetInt32(rs, 11),
						Layout = GetInt32(rs, 12),
						SelfRegistration = GetInt32(rs, 13),
						Timeframe = GetInt32(rs, 14),
						Yellow = GetInt32(rs, 15),
						Green = GetInt32(rs, 16),
						IndividualReportID = GetInt32(rs, 17),
						ExtendedSurveyID = GetInt32(rs, 18),
						ReportID = GetInt32(rs, 19),
						Logo = GetInt32(rs, 20),
						UseCode = GetInt32(rs, 21),
						ConfidentialIndividualReportID = GetInt32(rs, 22),
						SendSurveyAsEmail = GetInt32(rs, 23),
						SFTPhost = GetString(rs, 24),
						SFTPpath = GetString(rs, 25),
						SFTPuser = GetString(rs, 26),
						SFTPpass = GetString(rs, 27),
						SendSurveyAsPdfTo = GetString(rs, 28),
						SendSurveyAsPdfToQ = GetInt32(rs, 29),
						SendSurveyAsPdfToO = GetInt32(rs, 30),
						AdHocReportCompareWithParent = GetInt32(rs, 31),
						FeedBackID = GetInt32(rs, 32)
					};
				}
			}
			return projectRound;
		}
		
		public override IList<ProjectRound> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundID, 
	ProjectID, 
	Internal, 
	Started, 
	Closed, 
	TransparencyLevel, 
	RepeatedEntry, 
	SurveyID, 
	LangID, 
	RoundKey, 
	EmailFromAddress, 
	ReminderInterval, 
	Layout, 
	SelfRegistration, 
	Timeframe, 
	Yellow, 
	Green, 
	IndividualReportID, 
	ExtendedSurveyID, 
	ReportID, 
	Logo, 
	UseCode, 
	ConfidentialIndividualReportID, 
	SendSurveyAsEmail, 
	SFTPhost, 
	SFTPpath, 
	SFTPuser, 
	SFTPpass, 
	SendSurveyAsPdfTo, 
	SendSurveyAsPdfToQ, 
	SendSurveyAsPdfToO, 
	AdHocReportCompareWithParent, 
	FeedBackID
FROM ProjectRound";
			var projectRounds = new List<ProjectRound>();
			using (var rs = ExecuteReader(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					projectRounds.Add(new ProjectRound {
						ProjectRoundID = GetInt32(rs, 0),
						ProjectID = GetInt32(rs, 1),
						Internal = GetString(rs, 2),
						Started = GetDateTime(rs, 3),
						Closed = GetDateTime(rs, 4),
						TransparencyLevel = GetInt32(rs, 5),
						RepeatedEntry = GetInt32(rs, 6),
						SurveyID = GetInt32(rs, 7),
						LangID = GetInt32(rs, 8),
//						RoundKey = GetGuid(rs, 9),
						RoundKey = GetString(rs, 9),
						EmailFromAddress = GetString(rs, 10),
						ReminderInterval = GetInt32(rs, 11),
						Layout = GetInt32(rs, 12),
						SelfRegistration = GetInt32(rs, 13),
						Timeframe = GetInt32(rs, 14),
						Yellow = GetInt32(rs, 15),
						Green = GetInt32(rs, 16),
						IndividualReportID = GetInt32(rs, 17),
						ExtendedSurveyID = GetInt32(rs, 18),
						ReportID = GetInt32(rs, 19),
						Logo = GetInt32(rs, 20),
						UseCode = GetInt32(rs, 21),
						ConfidentialIndividualReportID = GetInt32(rs, 22),
						SendSurveyAsEmail = GetInt32(rs, 23),
						SFTPhost = GetString(rs, 24),
						SFTPpath = GetString(rs, 25),
						SFTPuser = GetString(rs, 26),
						SFTPpass = GetString(rs, 27),
						SendSurveyAsPdfTo = GetString(rs, 28),
						SendSurveyAsPdfToQ = GetInt32(rs, 29),
						SendSurveyAsPdfToO = GetInt32(rs, 30),
						AdHocReportCompareWithParent = GetInt32(rs, 31),
						FeedBackID = GetInt32(rs, 32)
					});
				}
			}
			return projectRounds;
		}
	}
}
