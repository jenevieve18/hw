using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlProjectRoundLangRepository : BaseSqlRepository<ProjectRoundLang>
	{
		public SqlProjectRoundLangRepository()
		{
		}
		
		public override void Save(ProjectRoundLang projectRoundLang)
		{
			string query = @"
INSERT INTO ProjectRoundLang(
	ProjectRoundLangID, 
	LangID, 
	ProjectRoundID, 
	InvitationSubject, 
	InvitationBody, 
	ReminderSubject, 
	ReminderBody, 
	SurveyName, 
	SurveyIntro, 
	UnitText, 
	ThankyouText, 
	ExtraInvitationSubject, 
	ExtraInvitationBody, 
	ExtraReminderSubject, 
	ExtraReminderBody, 
	InvitationSubjectJapaneseUnicode, 
	InvitationBodyJapaneseUnicode, 
	ReminderSubjectJapaneseUnicode, 
	ReminderBodyJapaneseUnicode, 
	SurveyNameJapaneseUnicode, 
	SurveyIntroJapaneseUnicode, 
	UnitTextJapaneseUnicode, 
	ThankyouTextJapaneseUnicode, 
	ExtraInvitationSubjectJapaneseUnicode, 
	ExtraInvitationBodyJapaneseUnicode, 
	ExtraReminderSubjectJapaneseUnicode, 
	ExtraReminderBodyJapaneseUnicode
)
VALUES(
	@ProjectRoundLangID, 
	@LangID, 
	@ProjectRoundID, 
	@InvitationSubject, 
	@InvitationBody, 
	@ReminderSubject, 
	@ReminderBody, 
	@SurveyName, 
	@SurveyIntro, 
	@UnitText, 
	@ThankyouText, 
	@ExtraInvitationSubject, 
	@ExtraInvitationBody, 
	@ExtraReminderSubject, 
	@ExtraReminderBody, 
	@InvitationSubjectJapaneseUnicode, 
	@InvitationBodyJapaneseUnicode, 
	@ReminderSubjectJapaneseUnicode, 
	@ReminderBodyJapaneseUnicode, 
	@SurveyNameJapaneseUnicode, 
	@SurveyIntroJapaneseUnicode, 
	@UnitTextJapaneseUnicode, 
	@ThankyouTextJapaneseUnicode, 
	@ExtraInvitationSubjectJapaneseUnicode, 
	@ExtraInvitationBodyJapaneseUnicode, 
	@ExtraReminderSubjectJapaneseUnicode, 
	@ExtraReminderBodyJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundLangID", projectRoundLang.ProjectRoundLangID),
				new SqlParameter("@LangID", projectRoundLang.LangID),
				new SqlParameter("@ProjectRoundID", projectRoundLang.ProjectRoundID),
				new SqlParameter("@InvitationSubject", projectRoundLang.InvitationSubject),
				new SqlParameter("@InvitationBody", projectRoundLang.InvitationBody),
				new SqlParameter("@ReminderSubject", projectRoundLang.ReminderSubject),
				new SqlParameter("@ReminderBody", projectRoundLang.ReminderBody),
				new SqlParameter("@SurveyName", projectRoundLang.SurveyName),
				new SqlParameter("@SurveyIntro", projectRoundLang.SurveyIntro),
				new SqlParameter("@UnitText", projectRoundLang.UnitText),
				new SqlParameter("@ThankyouText", projectRoundLang.ThankyouText),
				new SqlParameter("@ExtraInvitationSubject", projectRoundLang.ExtraInvitationSubject),
				new SqlParameter("@ExtraInvitationBody", projectRoundLang.ExtraInvitationBody),
				new SqlParameter("@ExtraReminderSubject", projectRoundLang.ExtraReminderSubject),
				new SqlParameter("@ExtraReminderBody", projectRoundLang.ExtraReminderBody),
				new SqlParameter("@InvitationSubjectJapaneseUnicode", projectRoundLang.InvitationSubjectJapaneseUnicode),
				new SqlParameter("@InvitationBodyJapaneseUnicode", projectRoundLang.InvitationBodyJapaneseUnicode),
				new SqlParameter("@ReminderSubjectJapaneseUnicode", projectRoundLang.ReminderSubjectJapaneseUnicode),
				new SqlParameter("@ReminderBodyJapaneseUnicode", projectRoundLang.ReminderBodyJapaneseUnicode),
				new SqlParameter("@SurveyNameJapaneseUnicode", projectRoundLang.SurveyNameJapaneseUnicode),
				new SqlParameter("@SurveyIntroJapaneseUnicode", projectRoundLang.SurveyIntroJapaneseUnicode),
				new SqlParameter("@UnitTextJapaneseUnicode", projectRoundLang.UnitTextJapaneseUnicode),
				new SqlParameter("@ThankyouTextJapaneseUnicode", projectRoundLang.ThankyouTextJapaneseUnicode),
				new SqlParameter("@ExtraInvitationSubjectJapaneseUnicode", projectRoundLang.ExtraInvitationSubjectJapaneseUnicode),
				new SqlParameter("@ExtraInvitationBodyJapaneseUnicode", projectRoundLang.ExtraInvitationBodyJapaneseUnicode),
				new SqlParameter("@ExtraReminderSubjectJapaneseUnicode", projectRoundLang.ExtraReminderSubjectJapaneseUnicode),
				new SqlParameter("@ExtraReminderBodyJapaneseUnicode", projectRoundLang.ExtraReminderBodyJapaneseUnicode)
			);
		}
		
		public override void Update(ProjectRoundLang projectRoundLang, int id)
		{
			string query = @"
UPDATE ProjectRoundLang SET
	ProjectRoundLangID = @ProjectRoundLangID,
	LangID = @LangID,
	ProjectRoundID = @ProjectRoundID,
	InvitationSubject = @InvitationSubject,
	InvitationBody = @InvitationBody,
	ReminderSubject = @ReminderSubject,
	ReminderBody = @ReminderBody,
	SurveyName = @SurveyName,
	SurveyIntro = @SurveyIntro,
	UnitText = @UnitText,
	ThankyouText = @ThankyouText,
	ExtraInvitationSubject = @ExtraInvitationSubject,
	ExtraInvitationBody = @ExtraInvitationBody,
	ExtraReminderSubject = @ExtraReminderSubject,
	ExtraReminderBody = @ExtraReminderBody,
	InvitationSubjectJapaneseUnicode = @InvitationSubjectJapaneseUnicode,
	InvitationBodyJapaneseUnicode = @InvitationBodyJapaneseUnicode,
	ReminderSubjectJapaneseUnicode = @ReminderSubjectJapaneseUnicode,
	ReminderBodyJapaneseUnicode = @ReminderBodyJapaneseUnicode,
	SurveyNameJapaneseUnicode = @SurveyNameJapaneseUnicode,
	SurveyIntroJapaneseUnicode = @SurveyIntroJapaneseUnicode,
	UnitTextJapaneseUnicode = @UnitTextJapaneseUnicode,
	ThankyouTextJapaneseUnicode = @ThankyouTextJapaneseUnicode,
	ExtraInvitationSubjectJapaneseUnicode = @ExtraInvitationSubjectJapaneseUnicode,
	ExtraInvitationBodyJapaneseUnicode = @ExtraInvitationBodyJapaneseUnicode,
	ExtraReminderSubjectJapaneseUnicode = @ExtraReminderSubjectJapaneseUnicode,
	ExtraReminderBodyJapaneseUnicode = @ExtraReminderBodyJapaneseUnicode
WHERE ProjectRoundLangID = @ProjectRoundLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundLangID", projectRoundLang.ProjectRoundLangID),
				new SqlParameter("@LangID", projectRoundLang.LangID),
				new SqlParameter("@ProjectRoundID", projectRoundLang.ProjectRoundID),
				new SqlParameter("@InvitationSubject", projectRoundLang.InvitationSubject),
				new SqlParameter("@InvitationBody", projectRoundLang.InvitationBody),
				new SqlParameter("@ReminderSubject", projectRoundLang.ReminderSubject),
				new SqlParameter("@ReminderBody", projectRoundLang.ReminderBody),
				new SqlParameter("@SurveyName", projectRoundLang.SurveyName),
				new SqlParameter("@SurveyIntro", projectRoundLang.SurveyIntro),
				new SqlParameter("@UnitText", projectRoundLang.UnitText),
				new SqlParameter("@ThankyouText", projectRoundLang.ThankyouText),
				new SqlParameter("@ExtraInvitationSubject", projectRoundLang.ExtraInvitationSubject),
				new SqlParameter("@ExtraInvitationBody", projectRoundLang.ExtraInvitationBody),
				new SqlParameter("@ExtraReminderSubject", projectRoundLang.ExtraReminderSubject),
				new SqlParameter("@ExtraReminderBody", projectRoundLang.ExtraReminderBody),
				new SqlParameter("@InvitationSubjectJapaneseUnicode", projectRoundLang.InvitationSubjectJapaneseUnicode),
				new SqlParameter("@InvitationBodyJapaneseUnicode", projectRoundLang.InvitationBodyJapaneseUnicode),
				new SqlParameter("@ReminderSubjectJapaneseUnicode", projectRoundLang.ReminderSubjectJapaneseUnicode),
				new SqlParameter("@ReminderBodyJapaneseUnicode", projectRoundLang.ReminderBodyJapaneseUnicode),
				new SqlParameter("@SurveyNameJapaneseUnicode", projectRoundLang.SurveyNameJapaneseUnicode),
				new SqlParameter("@SurveyIntroJapaneseUnicode", projectRoundLang.SurveyIntroJapaneseUnicode),
				new SqlParameter("@UnitTextJapaneseUnicode", projectRoundLang.UnitTextJapaneseUnicode),
				new SqlParameter("@ThankyouTextJapaneseUnicode", projectRoundLang.ThankyouTextJapaneseUnicode),
				new SqlParameter("@ExtraInvitationSubjectJapaneseUnicode", projectRoundLang.ExtraInvitationSubjectJapaneseUnicode),
				new SqlParameter("@ExtraInvitationBodyJapaneseUnicode", projectRoundLang.ExtraInvitationBodyJapaneseUnicode),
				new SqlParameter("@ExtraReminderSubjectJapaneseUnicode", projectRoundLang.ExtraReminderSubjectJapaneseUnicode),
				new SqlParameter("@ExtraReminderBodyJapaneseUnicode", projectRoundLang.ExtraReminderBodyJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ProjectRoundLang
WHERE ProjectRoundLangID = @ProjectRoundLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ProjectRoundLangID", id)
			);
		}
		
		public override ProjectRoundLang Read(int id)
		{
			string query = @"
SELECT 	ProjectRoundLangID, 
	LangID, 
	ProjectRoundID, 
	InvitationSubject, 
	InvitationBody, 
	ReminderSubject, 
	ReminderBody, 
	SurveyName, 
	SurveyIntro, 
	UnitText, 
	ThankyouText, 
	ExtraInvitationSubject, 
	ExtraInvitationBody, 
	ExtraReminderSubject, 
	ExtraReminderBody, 
	InvitationSubjectJapaneseUnicode, 
	InvitationBodyJapaneseUnicode, 
	ReminderSubjectJapaneseUnicode, 
	ReminderBodyJapaneseUnicode, 
	SurveyNameJapaneseUnicode, 
	SurveyIntroJapaneseUnicode, 
	UnitTextJapaneseUnicode, 
	ThankyouTextJapaneseUnicode, 
	ExtraInvitationSubjectJapaneseUnicode, 
	ExtraInvitationBodyJapaneseUnicode, 
	ExtraReminderSubjectJapaneseUnicode, 
	ExtraReminderBodyJapaneseUnicode
FROM ProjectRoundLang
WHERE ProjectRoundLangID = @ProjectRoundLangID";
			ProjectRoundLang projectRoundLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ProjectRoundLangID", id))) {
				if (rs.Read()) {
					projectRoundLang = new ProjectRoundLang {
						ProjectRoundLangID = GetInt32(rs, 0),
						LangID = GetInt32(rs, 1),
						ProjectRoundID = GetInt32(rs, 2),
						InvitationSubject = GetString(rs, 3),
						InvitationBody = GetString(rs, 4),
						ReminderSubject = GetString(rs, 5),
						ReminderBody = GetString(rs, 6),
						SurveyName = GetString(rs, 7),
						SurveyIntro = GetString(rs, 8),
						UnitText = GetString(rs, 9),
						ThankyouText = GetString(rs, 10),
						ExtraInvitationSubject = GetString(rs, 11),
						ExtraInvitationBody = GetString(rs, 12),
						ExtraReminderSubject = GetString(rs, 13),
						ExtraReminderBody = GetString(rs, 14),
						InvitationSubjectJapaneseUnicode = GetString(rs, 15),
						InvitationBodyJapaneseUnicode = GetString(rs, 16),
						ReminderSubjectJapaneseUnicode = GetString(rs, 17),
						ReminderBodyJapaneseUnicode = GetString(rs, 18),
						SurveyNameJapaneseUnicode = GetString(rs, 19),
						SurveyIntroJapaneseUnicode = GetString(rs, 20),
						UnitTextJapaneseUnicode = GetString(rs, 21),
						ThankyouTextJapaneseUnicode = GetString(rs, 22),
						ExtraInvitationSubjectJapaneseUnicode = GetString(rs, 23),
						ExtraInvitationBodyJapaneseUnicode = GetString(rs, 24),
						ExtraReminderSubjectJapaneseUnicode = GetString(rs, 25),
						ExtraReminderBodyJapaneseUnicode = GetString(rs, 26)
					};
				}
			}
			return projectRoundLang;
		}
		
		public override IList<ProjectRoundLang> FindAll()
		{
			string query = @"
SELECT 	ProjectRoundLangID, 
	LangID, 
	ProjectRoundID, 
	InvitationSubject, 
	InvitationBody, 
	ReminderSubject, 
	ReminderBody, 
	SurveyName, 
	SurveyIntro, 
	UnitText, 
	ThankyouText, 
	ExtraInvitationSubject, 
	ExtraInvitationBody, 
	ExtraReminderSubject, 
	ExtraReminderBody, 
	InvitationSubjectJapaneseUnicode, 
	InvitationBodyJapaneseUnicode, 
	ReminderSubjectJapaneseUnicode, 
	ReminderBodyJapaneseUnicode, 
	SurveyNameJapaneseUnicode, 
	SurveyIntroJapaneseUnicode, 
	UnitTextJapaneseUnicode, 
	ThankyouTextJapaneseUnicode, 
	ExtraInvitationSubjectJapaneseUnicode, 
	ExtraInvitationBodyJapaneseUnicode, 
	ExtraReminderSubjectJapaneseUnicode, 
	ExtraReminderBodyJapaneseUnicode
FROM ProjectRoundLang";
			var projectRoundLangs = new List<ProjectRoundLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					projectRoundLangs.Add(new ProjectRoundLang {
						ProjectRoundLangID = GetInt32(rs, 0),
						LangID = GetInt32(rs, 1),
						ProjectRoundID = GetInt32(rs, 2),
						InvitationSubject = GetString(rs, 3),
						InvitationBody = GetString(rs, 4),
						ReminderSubject = GetString(rs, 5),
						ReminderBody = GetString(rs, 6),
						SurveyName = GetString(rs, 7),
						SurveyIntro = GetString(rs, 8),
						UnitText = GetString(rs, 9),
						ThankyouText = GetString(rs, 10),
						ExtraInvitationSubject = GetString(rs, 11),
						ExtraInvitationBody = GetString(rs, 12),
						ExtraReminderSubject = GetString(rs, 13),
						ExtraReminderBody = GetString(rs, 14),
						InvitationSubjectJapaneseUnicode = GetString(rs, 15),
						InvitationBodyJapaneseUnicode = GetString(rs, 16),
						ReminderSubjectJapaneseUnicode = GetString(rs, 17),
						ReminderBodyJapaneseUnicode = GetString(rs, 18),
						SurveyNameJapaneseUnicode = GetString(rs, 19),
						SurveyIntroJapaneseUnicode = GetString(rs, 20),
						UnitTextJapaneseUnicode = GetString(rs, 21),
						ThankyouTextJapaneseUnicode = GetString(rs, 22),
						ExtraInvitationSubjectJapaneseUnicode = GetString(rs, 23),
						ExtraInvitationBodyJapaneseUnicode = GetString(rs, 24),
						ExtraReminderSubjectJapaneseUnicode = GetString(rs, 25),
						ExtraReminderBodyJapaneseUnicode = GetString(rs, 26)
					});
				}
			}
			return projectRoundLangs;
		}
	}
}
