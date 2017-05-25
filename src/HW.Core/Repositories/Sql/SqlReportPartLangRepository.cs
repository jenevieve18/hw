using System;
using HW.Core.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HW.Core.Repositories.Sql
{
	public class SqlReportPartLangRepository : BaseSqlRepository<ReportPartLang>
	{
		public SqlReportPartLangRepository()
		{
		}
		
		public override void Save(ReportPartLang reportPartLang)
		{
			string query = @"
INSERT INTO ReportPartLang(
	ReportPartLangID, 
	ReportPartID, 
	LangID, 
	Subject, 
	Header, 
	Footer, 
	AltText, 
	SubjectJapaneseUnicode, 
	HeaderJapaneseUnicode, 
	FooterJapaneseUnicode, 
	AltTextJapaneseUnicode
)
VALUES(
	@ReportPartLangID, 
	@ReportPartID, 
	@LangID, 
	@Subject, 
	@Header, 
	@Footer, 
	@AltText, 
	@SubjectJapaneseUnicode, 
	@HeaderJapaneseUnicode, 
	@FooterJapaneseUnicode, 
	@AltTextJapaneseUnicode
)";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ReportPartLangID", reportPartLang.ReportPartLangID),
				new SqlParameter("@ReportPartID", reportPartLang.ReportPartID),
				new SqlParameter("@LangID", reportPartLang.LangID),
				new SqlParameter("@Subject", reportPartLang.Subject),
				new SqlParameter("@Header", reportPartLang.Header),
				new SqlParameter("@Footer", reportPartLang.Footer),
				new SqlParameter("@AltText", reportPartLang.AltText),
				new SqlParameter("@SubjectJapaneseUnicode", reportPartLang.SubjectJapaneseUnicode),
				new SqlParameter("@HeaderJapaneseUnicode", reportPartLang.HeaderJapaneseUnicode),
				new SqlParameter("@FooterJapaneseUnicode", reportPartLang.FooterJapaneseUnicode),
				new SqlParameter("@AltTextJapaneseUnicode", reportPartLang.AltTextJapaneseUnicode)
			);
		}
		
		public override void Update(ReportPartLang reportPartLang, int id)
		{
			string query = @"
UPDATE ReportPartLang SET
	ReportPartLangID = @ReportPartLangID,
	ReportPartID = @ReportPartID,
	LangID = @LangID,
	Subject = @Subject,
	Header = @Header,
	Footer = @Footer,
	AltText = @AltText,
	SubjectJapaneseUnicode = @SubjectJapaneseUnicode,
	HeaderJapaneseUnicode = @HeaderJapaneseUnicode,
	FooterJapaneseUnicode = @FooterJapaneseUnicode,
	AltTextJapaneseUnicode = @AltTextJapaneseUnicode
WHERE ReportPartLangID = @ReportPartLangID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ReportPartLangID", reportPartLang.ReportPartLangID),
				new SqlParameter("@ReportPartID", reportPartLang.ReportPartID),
				new SqlParameter("@LangID", reportPartLang.LangID),
				new SqlParameter("@Subject", reportPartLang.Subject),
				new SqlParameter("@Header", reportPartLang.Header),
				new SqlParameter("@Footer", reportPartLang.Footer),
				new SqlParameter("@AltText", reportPartLang.AltText),
				new SqlParameter("@SubjectJapaneseUnicode", reportPartLang.SubjectJapaneseUnicode),
				new SqlParameter("@HeaderJapaneseUnicode", reportPartLang.HeaderJapaneseUnicode),
				new SqlParameter("@FooterJapaneseUnicode", reportPartLang.FooterJapaneseUnicode),
				new SqlParameter("@AltTextJapaneseUnicode", reportPartLang.AltTextJapaneseUnicode)
			);
		}
		
		public override void Delete(int id)
		{
			string query = @"
DELETE FROM ReportPartLang
WHERE ReportPartLangID = @ReportPartLangID";
			ExecuteNonQuery(
				query,
				"eFormSqlConnection",
				new SqlParameter("@ReportPartLangID", id)
			);
		}
		
		public override ReportPartLang Read(int id)
		{
			string query = @"
SELECT 	ReportPartLangID, 
	ReportPartID, 
	LangID, 
	Subject, 
	Header, 
	Footer, 
	AltText, 
	SubjectJapaneseUnicode, 
	HeaderJapaneseUnicode, 
	FooterJapaneseUnicode, 
	AltTextJapaneseUnicode
FROM ReportPartLang
WHERE ReportPartLangID = @ReportPartLangID";
			ReportPartLang reportPartLang = null;
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@ReportPartLangID", id))) {
				if (rs.Read()) {
					reportPartLang = new ReportPartLang {
						ReportPartLangID = GetInt32(rs, 0),
						ReportPartID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Subject = GetString(rs, 3),
						Header = GetString(rs, 4),
						Footer = GetString(rs, 5),
						AltText = GetString(rs, 6),
						SubjectJapaneseUnicode = GetString(rs, 7),
						HeaderJapaneseUnicode = GetString(rs, 8),
						FooterJapaneseUnicode = GetString(rs, 9),
						AltTextJapaneseUnicode = GetString(rs, 10)
					};
				}
			}
			return reportPartLang;
		}
		
		public override IList<ReportPartLang> FindAll()
		{
			string query = @"
SELECT 	ReportPartLangID, 
	ReportPartID, 
	LangID, 
	Subject, 
	Header, 
	Footer, 
	AltText, 
	SubjectJapaneseUnicode, 
	HeaderJapaneseUnicode, 
	FooterJapaneseUnicode, 
	AltTextJapaneseUnicode
FROM ReportPartLang";
			var reportPartLangs = new List<ReportPartLang>();
			using (var rs = ExecuteReader(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					reportPartLangs.Add(new ReportPartLang {
						ReportPartLangID = GetInt32(rs, 0),
						ReportPartID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Subject = GetString(rs, 3),
						Header = GetString(rs, 4),
						Footer = GetString(rs, 5),
						AltText = GetString(rs, 6),
						SubjectJapaneseUnicode = GetString(rs, 7),
						HeaderJapaneseUnicode = GetString(rs, 8),
						FooterJapaneseUnicode = GetString(rs, 9),
						AltTextJapaneseUnicode = GetString(rs, 10)
					});
				}
			}
			return reportPartLangs;
		}
		
		public IList<ReportPartLang> FindByReportPart(int reportPartID)
		{
			string query = @"
SELECT 	ReportPartLangID, 
	ReportPartID, 
	LangID, 
	Subject, 
	Header, 
	Footer, 
	AltText, 
	SubjectJapaneseUnicode, 
	HeaderJapaneseUnicode, 
	FooterJapaneseUnicode, 
	AltTextJapaneseUnicode
FROM ReportPartLang
WHERE ReportPartID = @ReportPartID";
			var reportPartLangs = new List<ReportPartLang>();
			using (var rs = ExecuteReader(query, "eFormSqlConnection", new SqlParameter("@ReportPartID", reportPartID))) {
				while (rs.Read()) {
					reportPartLangs.Add(new ReportPartLang {
						ReportPartLangID = GetInt32(rs, 0),
						ReportPartID = GetInt32(rs, 1),
						LangID = GetInt32(rs, 2),
						Subject = GetString(rs, 3),
						Header = GetString(rs, 4),
						Footer = GetString(rs, 5),
						AltText = GetString(rs, 6),
						SubjectJapaneseUnicode = GetString(rs, 7),
						HeaderJapaneseUnicode = GetString(rs, 8),
						FooterJapaneseUnicode = GetString(rs, 9),
						AltTextJapaneseUnicode = GetString(rs, 10)
					});
				}
			}
			return reportPartLangs;
		}
	}
}
