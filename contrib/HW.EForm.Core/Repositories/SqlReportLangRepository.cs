using System;
using HW.EForm.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
	
namespace HW.EForm.Core.Repositories
{
	public class SqlReportLangRepository : BaseSqlRepository<ReportLang>
	{
		public SqlReportLangRepository()
		{
		}
		
		public override void Save(ReportLang reportLang)
		{
			string query = @"
INSERT INTO ReportLang(
	ReportLangID, 
	ReportID, 
	LangID, 
	Feedback, 
	FeedbackJapaneseUnicode
)
VALUES(
	@ReportLangID, 
	@ReportID, 
	@LangID, 
	@Feedback, 
	@FeedbackJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportLangID", reportLang.ReportLangID),
				new SqlParameter("@ReportID", reportLang.ReportID),
				new SqlParameter("@LangID", reportLang.LangID),
				new SqlParameter("@Feedback", reportLang.Feedback),
				new SqlParameter("@FeedbackJapaneseUnicode", reportLang.FeedbackJapaneseUnicode)
			);
		}
		
		public override void Update(ReportLang reportLang, int id)
		{
			string query = @"
UPDATE ReportLang SET
	ReportLangID = @ReportLangID,
	ReportID = @ReportID,
	LangID = @LangID,
	Feedback = @Feedback,
	FeedbackJapaneseUnicode = @FeedbackJapaneseUnicode
WHERE ReportLangID = @ReportLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportLangID", reportLang.ReportLangID),
				new SqlParameter("@ReportID", reportLang.ReportID),
				new SqlParameter("@LangID", reportLang.LangID),
				new SqlParameter("@Feedback", reportLang.Feedback),
				new SqlParameter("@FeedbackJapaneseUnicode", reportLang.FeedbackJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ReportLang
WHERE ReportLangID = @ReportLangID";
			ExecuteNonQuery(
				query,
				new SqlParameter("@ReportLangID", id)
			);
		}
		
		public override ReportLang Read(int id)
		{
			string query = @"
SELECT 	ReportLangID, 
	ReportID, 
	LangID, 
	Feedback, 
	FeedbackJapaneseUnicode
FROM ReportLang
WHERE ReportLangID = @ReportLangID";
			ReportLang reportLang = null;
			using (var rs = ExecuteReader(query, new SqlParameter("@ReportLangID", id))) {
				if (rs.Read()) {
					reportLang = new ReportLang {
						ReportLangID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Feedback = GetString(rs, 3),
						FeedbackJapaneseUnicode = GetString(rs, 4)
					};
				}
			}
			return reportLang;
		}
		
		public override IList<ReportLang> FindAll()
		{
			string query = @"
SELECT 	ReportLangID, 
	ReportID, 
	LangID, 
	Feedback, 
	FeedbackJapaneseUnicode
FROM ReportLang";
			var reportLangs = new List<ReportLang>();
			using (var rs = ExecuteReader(query)) {
				while (rs.Read()) {
					reportLangs.Add(new ReportLang {
						ReportLangID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Feedback = GetString(rs, 3),
						FeedbackJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return reportLangs;
		}
		
		public IList<ReportLang> FindByReport(int reportID)
		{
			string query = @"
SELECT 	ReportLangID, 
	ReportID, 
	LangID, 
	Feedback, 
	FeedbackJapaneseUnicode
FROM ReportLang
WHERE ReportID = @ReportID";
			var reportLangs = new List<ReportLang>();
			using (var rs = ExecuteReader(query, new SqlParameter("@ReportID", reportID))) {
				while (rs.Read()) {
					reportLangs.Add(new ReportLang {
						ReportLangID = GetInt32(rs, 0),
						ReportID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Feedback = GetString(rs, 3),
						FeedbackJapaneseUnicode = GetString(rs, 4)
					});
				}
			}
			return reportLangs;
		}
	}
}
