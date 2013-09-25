using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlReportRepository : BaseSqlRepository<Report>, IReportRepository
	{
		public ReportPartComponent ReadComponentByPartAndLanguage(int reportPartID, int langID)
		{
			string query = string.Format(
				@"
SELECT rpc.WeightedQuestionOptionID,
	wqol.WeightedQuestionOption,
	wqo.QuestionID,
	wqo.OptionID
FROM ReportPartComponent rpc
INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID
	AND wqol.LangID = {1}
WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportPartID,
				langID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var c = new ReportPartComponent();
					c.QuestionOption = new WeightedQuestionOption {
						Question = new Question { Id = rs.GetInt32(2) },
						Option = new Option { Id = rs.GetInt32(3) }
					};
					return c;
				}
			}
			return null;
		}
		
		public ReportPart ReadReportPart(int reportPartID, int langID)
		{
			string query = string.Format(
				@"
SELECT rp.Type,
	(SELECT COUNT(*) FROM ReportPartComponent rpc WHERE rpc.ReportPartID = rp.ReportPartID),
	rp.QuestionID,
	rp.OptionID,
	rp.RequiredAnswerCount,
	rp.PartLevel,
	rp.ReportPartID,
	rpl.Subject,
	rpl.Header,
	rpl.Footer
FROM ReportPart rp,
ReportPartLang rpl 
WHERE rp.ReportPartID = {0}
AND rp.ReportPartID = rpl.ReportPartID
AND rpl.LangID = {1}",
				reportPartID,
				langID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var p = new ReportPart();
					p.Type = rs.GetInt32(0);
					p.Components = new List<ReportPartComponent>(rs.GetInt32(1));
					p.Question = new Question { Id = GetInt32(rs, 2) };
					p.Option = new Option { Id = GetInt32(rs, 3) };
					p.RequiredAnswerCount = rs.GetInt32(4);
					p.PartLevel = GetInt32(rs, 5);
					p.Id = GetInt32(rs, 6);
					p.CurrentLanguage = new ReportPartLanguage {
						Subject = GetString(rs, 7),
						Header = GetString(rs, 8),
						Footer = GetString(rs, 9)
					};
					return p;
				}
			}
			return null;
		}
		
		public override IList<Report> FindAll()
		{
			string query = string.Format(
				@"
SELECT *
FROM Report"
			);
			var reports = new List<Report>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var r = new Report {
						Id = rs.GetInt32(0),
						Internal = rs.GetString(1),
						ReportKey = rs.GetGuid(2)
					};
					reports.Add(r);
				}
			}
			return reports;
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			string query = string.Format(
				@"
SELECT rp.ReportPartID,
	rpl.Subject,
	rpl.Header,
	rpl.Footer,
	rp.Type,
	rpl.ReportPartLangID
FROM ProjectRoundUnit pru
INNER JOIN Report r ON r.ReportID = pru.ReportID
INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID
INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = {1}
WHERE pru.ProjectRoundUnitID = {0}
ORDER BY rp.SortOrder",
				projectRoundID,
				langID
			);
			var languages = new List<ReportPartLanguage>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var l = new ReportPartLanguage {
						Id = rs.GetInt32(5),
						ReportPart = new ReportPart { Id = rs.GetInt32(0), Type = rs.GetInt32(4) },
						Subject = rs.GetString(1),
						Header = rs.GetString(2),
						Footer = rs.GetString(3)
					};
					languages.Add(l);
				}
			}
			return languages;
		}
		
		public IList<ReportPartLanguage> FindPartLanguagesByReport(int reportID)
		{
			string query = string.Format(
				@"
SELECT rp.ReportPartID,
	rpl.Subject,
	rpl.Header,
	rpl.Footer,
	rp.Type
FROM Report r
INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID
INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = 1
WHERE r.ReportID = {0}
ORDER BY rp.SortOrder",
				reportID
			);
			var languages = new List<ReportPartLanguage>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var p = new ReportPartLanguage {
						ReportPart = new ReportPart { Id = rs.GetInt32(0), Type = rs.GetInt32(1) },
						Subject = rs.GetString(1),
						Header = rs.GetString(2),
						Footer = rs.GetString(3),
					};
					languages.Add(p);
				}
			}
			return languages;
		}
		
		public IList<ReportPartComponent> FindComponents(int reportPartID)
		{
			string query = string.Format(
				@"
SELECT rpc.IdxID,
	(SELECT COUNT(*) FROM IdxPart ip WHERE ip.IdxID = rpc.IdxID AND ip.OtherIdxID IS NOT NULL),
	i.TargetVal,
	i.YellowLow,
	i.GreenLow,
	i.GreenHigh,
	i.YellowHigh
FROM ReportPartComponent rpc
INNER JOIN Idx i ON rpc.IdxID = i.IdxID
WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportPartID
			);
			var components = new List<ReportPartComponent>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var c = new ReportPartComponent();
					c.Id = rs.GetInt32(0);
					c.Index = new Index {
						TargetValue = rs.GetInt32(2),
						YellowLow = rs.GetInt32(3),
						GreenLow = rs.GetInt32(4),
						GreenHigh = rs.GetInt32(5),
						YellowHigh = rs.GetInt32(6)
					};
					c.Index.Parts = new List<IndexPart>(rs.GetInt32(1));
					components.Add(c);
				}
			}
			return components;
		}
		
		public IList<ReportPartComponent> FindComponentsByPart(int reportPartID)
		{
			string query = string.Format(
				@"
SELECT rpc.WeightedQuestionOptionID,
	wqo.YellowLow,
	wqo.GreenLow,
	wqo.GreenHigh,
	wqo.YellowHigh,
	wqo.QuestionID,
	wqo.OptionID
FROM    ReportPartComponent rpc
INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportPartID
			);
			var components = new List<ReportPartComponent>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var c = new ReportPartComponent();
					c.QuestionOption = new WeightedQuestionOption {
						Id = rs.GetInt32(0),
						YellowLow = rs.GetInt32(1),
						GreenLow = rs.GetInt32(2),
						GreenHigh = rs.GetInt32(3),
						YellowHigh = rs.GetInt32(4),
						Question = new Question { Id = rs.GetInt32(5) },
						Option = new Option { Id = rs.GetInt32(6) }
					};
					components.Add(c);
				}
			}
			return components;
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage(int reportPartID, int langID)
		{
			string query = string.Format(
				@"
SELECT rpc.WeightedQuestionOptionID,
	wqol.WeightedQuestionOption,
	wqo.QuestionID,
	wqo.OptionID
FROM ReportPartComponent AS rpc
INNER JOIN WeightedQuestionOption AS wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
INNER JOIN WeightedQuestionOptionLang AS wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID
	AND wqol.LangID = {1}
WHERE (rpc.ReportPartID = {0})
ORDER BY rpc.SortOrder",
				reportPartID,
				langID
			);
			var components = new List<ReportPartComponent>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var c = new ReportPartComponent();
					c.Id = rs.GetInt32(0);
					c.QuestionOption = new WeightedQuestionOption {
						Question = new Question { Id = rs.GetInt32(2) },
						Option = new Option { Id = rs.GetInt32(3) }
					};
					components.Add(c);
				}
			}
			return components;
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage2(int reportPartID, int langID)
		{
			string query = string.Format(
				@"
SELECT rpc.WeightedQuestionOptionID,
	wqol.WeightedQuestionOption,
	wqo.TargetVal,
	wqo.YellowLow,
	wqo.GreenLow,
	wqo.GreenHigh,
	wqo.YellowHigh,
	wqo.QuestionID,
	wqo.OptionID
FROM ReportPartComponent rpc
INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID
	AND wqol.LangID = {1}
WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportPartID,
				langID
			);
			var components = new List<ReportPartComponent>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var c = new ReportPartComponent();
					var q = new WeightedQuestionOption {
						Id = rs.GetInt32(0),
						TargetValue = rs.GetInt32(2),
						YellowLow = rs.GetInt32(3),
						GreenLow = rs.GetInt32(4),
						GreenHigh = rs.GetInt32(5),
						YellowHigh = rs.GetInt32(6),
						Question = new Question { Id = rs.GetInt32(7) },
						Option = new Option { Id = rs.GetInt32(8) }
					};
					q.Languages = new List<WeightedQuestionOptionLanguage>(
						new WeightedQuestionOptionLanguage[] {
							new WeightedQuestionOptionLanguage { Question = rs.GetString(1) }
						}
					);
					c.QuestionOption = q;
					components.Add(c);
				}
			}
			return components;
		}
		
		public ReportPartLanguage ReadReportPartLanguage(int reportPartLangID)
		{
			throw new NotImplementedException();
		}
		
		public void SaveOrUpdateReportPart(ReportPart part)
		{
			throw new NotImplementedException();
		}
		
		public void SaveOrUpdateReportPartLanguage(ReportPartLanguage part)
		{
			throw new NotImplementedException();
		}
	}
}
