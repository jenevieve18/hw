using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public interface IWeightedQuestionOptionLangRepository : IBaseRepository<WeightedQuestionOption>
	{
	}
	
	public class SqlWeightedQuestionOptionLangRepository : BaseSqlRepository<WeightedQuestionOptionLang>
	{
		public SqlWeightedQuestionOptionLangRepository()
		{
		}
		
		public override void Save(WeightedQuestionOptionLang weightedQuestionOptionLang)
		{
			string query = @"
INSERT INTO WeightedQuestionOptionLang(
	WeightedQuestionOptionLangID, 
	WeightedQuestionOptionID, 
	LangID, 
	WeightedQuestionOption, 
	FeedbackHeader, 
	Feedback, 
	FeedbackRedLow, 
	FeedbackYellowLow, 
	FeedbackGreen, 
	FeedbackYellowHigh, 
	FeedbackRedHigh, 
	ActionRedLow, 
	ActionYellowLow, 
	ActionGreen, 
	ActionYellowHigh, 
	ActionRedHigh, 
	WeightedQuestionOptionJapaneseUnicode, 
	FeedbackHeaderJapaneseUnicode, 
	FeedbackJapaneseUnicode, 
	FeedbackRedLowJapaneseUnicode, 
	FeedbackYellowLowJapaneseUnicode, 
	FeedbackGreenJapaneseUnicode, 
	FeedbackYellowHighJapaneseUnicode, 
	FeedbackRedHighJapaneseUnicode, 
	ActionRedLowJapaneseUnicode, 
	ActionYellowLowJapaneseUnicode, 
	ActionGreenJapaneseUnicode, 
	ActionYellowHighJapaneseUnicode, 
	ActionRedHighJapaneseUnicode
)
VALUES(
	@WeightedQuestionOptionLangID, 
	@WeightedQuestionOptionID, 
	@LangID, 
	@WeightedQuestionOption, 
	@FeedbackHeader, 
	@Feedback, 
	@FeedbackRedLow, 
	@FeedbackYellowLow, 
	@FeedbackGreen, 
	@FeedbackYellowHigh, 
	@FeedbackRedHigh, 
	@ActionRedLow, 
	@ActionYellowLow, 
	@ActionGreen, 
	@ActionYellowHigh, 
	@ActionRedHigh, 
	@WeightedQuestionOptionJapaneseUnicode, 
	@FeedbackHeaderJapaneseUnicode, 
	@FeedbackJapaneseUnicode, 
	@FeedbackRedLowJapaneseUnicode, 
	@FeedbackYellowLowJapaneseUnicode, 
	@FeedbackGreenJapaneseUnicode, 
	@FeedbackYellowHighJapaneseUnicode, 
	@FeedbackRedHighJapaneseUnicode, 
	@ActionRedLowJapaneseUnicode, 
	@ActionYellowLowJapaneseUnicode, 
	@ActionGreenJapaneseUnicode, 
	@ActionYellowHighJapaneseUnicode, 
	@ActionRedHighJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WeightedQuestionOptionLangID", weightedQuestionOptionLang.WeightedQuestionOptionLangID),
				new SqlParameter("@WeightedQuestionOptionID", weightedQuestionOptionLang.WeightedQuestionOptionID),
				new SqlParameter("@LangID", weightedQuestionOptionLang.LangID),
				new SqlParameter("@WeightedQuestionOption", weightedQuestionOptionLang.WeightedQuestionOption),
				new SqlParameter("@FeedbackHeader", weightedQuestionOptionLang.FeedbackHeader),
				new SqlParameter("@Feedback", weightedQuestionOptionLang.Feedback),
				new SqlParameter("@FeedbackRedLow", weightedQuestionOptionLang.FeedbackRedLow),
				new SqlParameter("@FeedbackYellowLow", weightedQuestionOptionLang.FeedbackYellowLow),
				new SqlParameter("@FeedbackGreen", weightedQuestionOptionLang.FeedbackGreen),
				new SqlParameter("@FeedbackYellowHigh", weightedQuestionOptionLang.FeedbackYellowHigh),
				new SqlParameter("@FeedbackRedHigh", weightedQuestionOptionLang.FeedbackRedHigh),
				new SqlParameter("@ActionRedLow", weightedQuestionOptionLang.ActionRedLow),
				new SqlParameter("@ActionYellowLow", weightedQuestionOptionLang.ActionYellowLow),
				new SqlParameter("@ActionGreen", weightedQuestionOptionLang.ActionGreen),
				new SqlParameter("@ActionYellowHigh", weightedQuestionOptionLang.ActionYellowHigh),
				new SqlParameter("@ActionRedHigh", weightedQuestionOptionLang.ActionRedHigh),
				new SqlParameter("@WeightedQuestionOptionJapaneseUnicode", weightedQuestionOptionLang.WeightedQuestionOptionJapaneseUnicode),
				new SqlParameter("@FeedbackHeaderJapaneseUnicode", weightedQuestionOptionLang.FeedbackHeaderJapaneseUnicode),
				new SqlParameter("@FeedbackJapaneseUnicode", weightedQuestionOptionLang.FeedbackJapaneseUnicode),
				new SqlParameter("@FeedbackRedLowJapaneseUnicode", weightedQuestionOptionLang.FeedbackRedLowJapaneseUnicode),
				new SqlParameter("@FeedbackYellowLowJapaneseUnicode", weightedQuestionOptionLang.FeedbackYellowLowJapaneseUnicode),
				new SqlParameter("@FeedbackGreenJapaneseUnicode", weightedQuestionOptionLang.FeedbackGreenJapaneseUnicode),
				new SqlParameter("@FeedbackYellowHighJapaneseUnicode", weightedQuestionOptionLang.FeedbackYellowHighJapaneseUnicode),
				new SqlParameter("@FeedbackRedHighJapaneseUnicode", weightedQuestionOptionLang.FeedbackRedHighJapaneseUnicode),
				new SqlParameter("@ActionRedLowJapaneseUnicode", weightedQuestionOptionLang.ActionRedLowJapaneseUnicode),
				new SqlParameter("@ActionYellowLowJapaneseUnicode", weightedQuestionOptionLang.ActionYellowLowJapaneseUnicode),
				new SqlParameter("@ActionGreenJapaneseUnicode", weightedQuestionOptionLang.ActionGreenJapaneseUnicode),
				new SqlParameter("@ActionYellowHighJapaneseUnicode", weightedQuestionOptionLang.ActionYellowHighJapaneseUnicode),
				new SqlParameter("@ActionRedHighJapaneseUnicode", weightedQuestionOptionLang.ActionRedHighJapaneseUnicode)
			);
		}
		
		public override void Update(WeightedQuestionOptionLang weightedQuestionOptionLang, int id)
		{
			string query = @"
UPDATE WeightedQuestionOptionLang SET
	WeightedQuestionOptionLangID = @WeightedQuestionOptionLangID,
	WeightedQuestionOptionID = @WeightedQuestionOptionID,
	LangID = @LangID,
	WeightedQuestionOption = @WeightedQuestionOption,
	FeedbackHeader = @FeedbackHeader,
	Feedback = @Feedback,
	FeedbackRedLow = @FeedbackRedLow,
	FeedbackYellowLow = @FeedbackYellowLow,
	FeedbackGreen = @FeedbackGreen,
	FeedbackYellowHigh = @FeedbackYellowHigh,
	FeedbackRedHigh = @FeedbackRedHigh,
	ActionRedLow = @ActionRedLow,
	ActionYellowLow = @ActionYellowLow,
	ActionGreen = @ActionGreen,
	ActionYellowHigh = @ActionYellowHigh,
	ActionRedHigh = @ActionRedHigh,
	WeightedQuestionOptionJapaneseUnicode = @WeightedQuestionOptionJapaneseUnicode,
	FeedbackHeaderJapaneseUnicode = @FeedbackHeaderJapaneseUnicode,
	FeedbackJapaneseUnicode = @FeedbackJapaneseUnicode,
	FeedbackRedLowJapaneseUnicode = @FeedbackRedLowJapaneseUnicode,
	FeedbackYellowLowJapaneseUnicode = @FeedbackYellowLowJapaneseUnicode,
	FeedbackGreenJapaneseUnicode = @FeedbackGreenJapaneseUnicode,
	FeedbackYellowHighJapaneseUnicode = @FeedbackYellowHighJapaneseUnicode,
	FeedbackRedHighJapaneseUnicode = @FeedbackRedHighJapaneseUnicode,
	ActionRedLowJapaneseUnicode = @ActionRedLowJapaneseUnicode,
	ActionYellowLowJapaneseUnicode = @ActionYellowLowJapaneseUnicode,
	ActionGreenJapaneseUnicode = @ActionGreenJapaneseUnicode,
	ActionYellowHighJapaneseUnicode = @ActionYellowHighJapaneseUnicode,
	ActionRedHighJapaneseUnicode = @ActionRedHighJapaneseUnicode
WHERE WeightedQuestionOptionLangID = @WeightedQuestionOptionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WeightedQuestionOptionLangID", weightedQuestionOptionLang.WeightedQuestionOptionLangID),
				new SqlParameter("@WeightedQuestionOptionID", weightedQuestionOptionLang.WeightedQuestionOptionID),
				new SqlParameter("@LangID", weightedQuestionOptionLang.LangID),
				new SqlParameter("@WeightedQuestionOption", weightedQuestionOptionLang.WeightedQuestionOption),
				new SqlParameter("@FeedbackHeader", weightedQuestionOptionLang.FeedbackHeader),
				new SqlParameter("@Feedback", weightedQuestionOptionLang.Feedback),
				new SqlParameter("@FeedbackRedLow", weightedQuestionOptionLang.FeedbackRedLow),
				new SqlParameter("@FeedbackYellowLow", weightedQuestionOptionLang.FeedbackYellowLow),
				new SqlParameter("@FeedbackGreen", weightedQuestionOptionLang.FeedbackGreen),
				new SqlParameter("@FeedbackYellowHigh", weightedQuestionOptionLang.FeedbackYellowHigh),
				new SqlParameter("@FeedbackRedHigh", weightedQuestionOptionLang.FeedbackRedHigh),
				new SqlParameter("@ActionRedLow", weightedQuestionOptionLang.ActionRedLow),
				new SqlParameter("@ActionYellowLow", weightedQuestionOptionLang.ActionYellowLow),
				new SqlParameter("@ActionGreen", weightedQuestionOptionLang.ActionGreen),
				new SqlParameter("@ActionYellowHigh", weightedQuestionOptionLang.ActionYellowHigh),
				new SqlParameter("@ActionRedHigh", weightedQuestionOptionLang.ActionRedHigh),
				new SqlParameter("@WeightedQuestionOptionJapaneseUnicode", weightedQuestionOptionLang.WeightedQuestionOptionJapaneseUnicode),
				new SqlParameter("@FeedbackHeaderJapaneseUnicode", weightedQuestionOptionLang.FeedbackHeaderJapaneseUnicode),
				new SqlParameter("@FeedbackJapaneseUnicode", weightedQuestionOptionLang.FeedbackJapaneseUnicode),
				new SqlParameter("@FeedbackRedLowJapaneseUnicode", weightedQuestionOptionLang.FeedbackRedLowJapaneseUnicode),
				new SqlParameter("@FeedbackYellowLowJapaneseUnicode", weightedQuestionOptionLang.FeedbackYellowLowJapaneseUnicode),
				new SqlParameter("@FeedbackGreenJapaneseUnicode", weightedQuestionOptionLang.FeedbackGreenJapaneseUnicode),
				new SqlParameter("@FeedbackYellowHighJapaneseUnicode", weightedQuestionOptionLang.FeedbackYellowHighJapaneseUnicode),
				new SqlParameter("@FeedbackRedHighJapaneseUnicode", weightedQuestionOptionLang.FeedbackRedHighJapaneseUnicode),
				new SqlParameter("@ActionRedLowJapaneseUnicode", weightedQuestionOptionLang.ActionRedLowJapaneseUnicode),
				new SqlParameter("@ActionYellowLowJapaneseUnicode", weightedQuestionOptionLang.ActionYellowLowJapaneseUnicode),
				new SqlParameter("@ActionGreenJapaneseUnicode", weightedQuestionOptionLang.ActionGreenJapaneseUnicode),
				new SqlParameter("@ActionYellowHighJapaneseUnicode", weightedQuestionOptionLang.ActionYellowHighJapaneseUnicode),
				new SqlParameter("@ActionRedHighJapaneseUnicode", weightedQuestionOptionLang.ActionRedHighJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM WeightedQuestionOptionLang
WHERE WeightedQuestionOptionLangID = @WeightedQuestionOptionLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@WeightedQuestionOptionLangID", id)
			);
		}
		
		public override WeightedQuestionOptionLang Read(int id)
		{
			string query = @"
SELECT 	WeightedQuestionOptionLangID, 
	WeightedQuestionOptionID, 
	LangID, 
	WeightedQuestionOption, 
	FeedbackHeader, 
	Feedback, 
	FeedbackRedLow, 
	FeedbackYellowLow, 
	FeedbackGreen, 
	FeedbackYellowHigh, 
	FeedbackRedHigh, 
	ActionRedLow, 
	ActionYellowLow, 
	ActionGreen, 
	ActionYellowHigh, 
	ActionRedHigh, 
	WeightedQuestionOptionJapaneseUnicode, 
	FeedbackHeaderJapaneseUnicode, 
	FeedbackJapaneseUnicode, 
	FeedbackRedLowJapaneseUnicode, 
	FeedbackYellowLowJapaneseUnicode, 
	FeedbackGreenJapaneseUnicode, 
	FeedbackYellowHighJapaneseUnicode, 
	FeedbackRedHighJapaneseUnicode, 
	ActionRedLowJapaneseUnicode, 
	ActionYellowLowJapaneseUnicode, 
	ActionGreenJapaneseUnicode, 
	ActionYellowHighJapaneseUnicode, 
	ActionRedHighJapaneseUnicode
FROM WeightedQuestionOptionLang
WHERE WeightedQuestionOptionLangID = @WeightedQuestionOptionLangID";
			WeightedQuestionOptionLang weightedQuestionOptionLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@WeightedQuestionOptionLangID", id))) {
				if (rs.Read()) {
					weightedQuestionOptionLang = new WeightedQuestionOptionLang {
						WeightedQuestionOptionLangID = GetInt32(rs, 0),
						WeightedQuestionOptionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						WeightedQuestionOption = GetString(rs, 3),
						FeedbackHeader = GetString(rs, 4),
						Feedback = GetString(rs, 5),
						FeedbackRedLow = GetString(rs, 6),
						FeedbackYellowLow = GetString(rs, 7),
						FeedbackGreen = GetString(rs, 8),
						FeedbackYellowHigh = GetString(rs, 9),
						FeedbackRedHigh = GetString(rs, 10),
						ActionRedLow = GetString(rs, 11),
						ActionYellowLow = GetString(rs, 12),
						ActionGreen = GetString(rs, 13),
						ActionYellowHigh = GetString(rs, 14),
						ActionRedHigh = GetString(rs, 15),
						WeightedQuestionOptionJapaneseUnicode = GetString(rs, 16),
						FeedbackHeaderJapaneseUnicode = GetString(rs, 17),
						FeedbackJapaneseUnicode = GetString(rs, 18),
						FeedbackRedLowJapaneseUnicode = GetString(rs, 19),
						FeedbackYellowLowJapaneseUnicode = GetString(rs, 20),
						FeedbackGreenJapaneseUnicode = GetString(rs, 21),
						FeedbackYellowHighJapaneseUnicode = GetString(rs, 22),
						FeedbackRedHighJapaneseUnicode = GetString(rs, 23),
						ActionRedLowJapaneseUnicode = GetString(rs, 24),
						ActionYellowLowJapaneseUnicode = GetString(rs, 25),
						ActionGreenJapaneseUnicode = GetString(rs, 26),
						ActionYellowHighJapaneseUnicode = GetString(rs, 27),
						ActionRedHighJapaneseUnicode = GetString(rs, 28)
					};
				}
			}
			return weightedQuestionOptionLang;
		}
		
		public override IList<WeightedQuestionOptionLang> FindAll()
		{
			string query = @"
SELECT 	WeightedQuestionOptionLangID, 
	WeightedQuestionOptionID, 
	LangID, 
	WeightedQuestionOption, 
	FeedbackHeader, 
	Feedback, 
	FeedbackRedLow, 
	FeedbackYellowLow, 
	FeedbackGreen, 
	FeedbackYellowHigh, 
	FeedbackRedHigh, 
	ActionRedLow, 
	ActionYellowLow, 
	ActionGreen, 
	ActionYellowHigh, 
	ActionRedHigh, 
	WeightedQuestionOptionJapaneseUnicode, 
	FeedbackHeaderJapaneseUnicode, 
	FeedbackJapaneseUnicode, 
	FeedbackRedLowJapaneseUnicode, 
	FeedbackYellowLowJapaneseUnicode, 
	FeedbackGreenJapaneseUnicode, 
	FeedbackYellowHighJapaneseUnicode, 
	FeedbackRedHighJapaneseUnicode, 
	ActionRedLowJapaneseUnicode, 
	ActionYellowLowJapaneseUnicode, 
	ActionGreenJapaneseUnicode, 
	ActionYellowHighJapaneseUnicode, 
	ActionRedHighJapaneseUnicode
FROM WeightedQuestionOptionLang";
			var weightedQuestionOptionLangs = new List<WeightedQuestionOptionLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					weightedQuestionOptionLangs.Add(new WeightedQuestionOptionLang {
						WeightedQuestionOptionLangID = GetInt32(rs, 0),
						WeightedQuestionOptionID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						WeightedQuestionOption = GetString(rs, 3),
						FeedbackHeader = GetString(rs, 4),
						Feedback = GetString(rs, 5),
						FeedbackRedLow = GetString(rs, 6),
						FeedbackYellowLow = GetString(rs, 7),
						FeedbackGreen = GetString(rs, 8),
						FeedbackYellowHigh = GetString(rs, 9),
						FeedbackRedHigh = GetString(rs, 10),
						ActionRedLow = GetString(rs, 11),
						ActionYellowLow = GetString(rs, 12),
						ActionGreen = GetString(rs, 13),
						ActionYellowHigh = GetString(rs, 14),
						ActionRedHigh = GetString(rs, 15),
						WeightedQuestionOptionJapaneseUnicode = GetString(rs, 16),
						FeedbackHeaderJapaneseUnicode = GetString(rs, 17),
						FeedbackJapaneseUnicode = GetString(rs, 18),
						FeedbackRedLowJapaneseUnicode = GetString(rs, 19),
						FeedbackYellowLowJapaneseUnicode = GetString(rs, 20),
						FeedbackGreenJapaneseUnicode = GetString(rs, 21),
						FeedbackYellowHighJapaneseUnicode = GetString(rs, 22),
						FeedbackRedHighJapaneseUnicode = GetString(rs, 23),
						ActionRedLowJapaneseUnicode = GetString(rs, 24),
						ActionYellowLowJapaneseUnicode = GetString(rs, 25),
						ActionGreenJapaneseUnicode = GetString(rs, 26),
						ActionYellowHighJapaneseUnicode = GetString(rs, 27),
						ActionRedHighJapaneseUnicode = GetString(rs, 28)
					});
				}
			}
			return weightedQuestionOptionLangs;
		}
	}
}
