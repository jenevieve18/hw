//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace HW.Core
{
	public class BaseSQLiteRepository<T> : IBaseRepository<T>
	{
		SQLiteConnection con;
		
		public BaseSQLiteRepository()
		{
			con = new SQLiteConnection(ConfigurationSettings.AppSettings["sqlite"]);
		}
		
		public void SaveOrUpdate(T t)
		{
			throw new NotImplementedException();
		}
		
		public void Delete(T t)
		{
			throw new NotImplementedException();
		}
		
		public T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public IList<T> FindAll()
		{
			throw new NotImplementedException();
		}
		
		protected SQLiteDataReader ExecuteReader(string query)
		{
			SQLiteCommand cmd = new SQLiteCommand(query, con);
			OpenConnection();
			return cmd.ExecuteReader();
		}
		
		void OpenConnection()
		{
			if (con.State == ConnectionState.Closed) {
				con.Open();
			}
		}
	}
	
	public class SQLiteDepartmentRepository : BaseSQLiteRepository<Department>, IDepartmentRepository
	{
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"SELECT d.Department,
       d.DepartmentID,
       d.DepartmentShort,
       dbo.cf_departmentDepth(d.DepartmentID),
       (SELECT COUNT(*)
          FROM    Department x
               INNER JOIN
                  SponsorAdminDepartment xx
               ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = {1}
         WHERE (x.ParentDepartmentID = d.ParentDepartmentID
                OR x.ParentDepartmentID IS NULL
                   AND d.ParentDepartmentID IS NULL)
               AND d.SponsorID = x.SponsorID
               AND d.SortString < x.SortString)
  FROM    Department d
       INNER JOIN
          SponsorAdminDepartment sad
       ON d.DepartmentID = sad.DepartmentID
 WHERE sad.SponsorAdminID = {1} AND d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				sponsorAdminID
			);
			var departments = new List<Department>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						Id = rs.GetInt32(1),
						ShortName = rs.GetString(2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorInDepth(int sponsorID)
		{
			string query = string.Format(
				@"SELECT d.DepartmentAnonymized,
       d.DepartmentID,
       '',
       dbo.cf_departmentDepth(d.DepartmentID),
       (SELECT COUNT(*)
          FROM Department x
         WHERE (x.ParentDepartmentID = d.ParentDepartmentID
                OR x.ParentDepartmentID IS NULL
                   AND d.ParentDepartmentID IS NULL)
               AND d.SponsorID = x.SponsorID
               AND d.SortString < x.SortString)
  FROM Department d
 WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID
			);
			var departments = new List<Department>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						Id = rs.GetInt32(1),
						ShortName = rs.GetString(2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"SELECT d.DepartmentID
  FROM    Department d
       INNER JOIN
          SponsorAdminDepartment sad
       ON d.DepartmentID = sad.DepartmentID
 WHERE sad.SponsorAdminID = {1} AND d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				sponsorAdminID
			);
			var departments = new List<Department>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"SELECT d.DepartmentID
  FROM Department d
 WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID
			);
			var departments = new List<Department>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor2(int sponsorID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class SQLiteIndexRepository : BaseSQLiteRepository<Index>, IIndexRepository
	{
		public IList<Index> FindByLanguage(int id, int langID)
		{
			string query = string.Format(
				@"SELECT AVG(tmp.AX),
       tmp.Idx,
       tmp.IdxID,
       COUNT(*) AS DX
  FROM (SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal
                  AS AX,
               i.IdxID,
               il.Idx,
               i.CX,
               i.AllPartsRequired,
               COUNT(*) AS BX
          FROM Idx i
               INNER JOIN IdxLang il
                  ON i.IdxID = il.IdxID AND il.LangID = 1
               INNER JOIN IdxPart ip
                  ON i.IdxID = ip.IdxID
               INNER JOIN IdxPartComponent ipc
                  ON ip.IdxPartID = ipc.IdxPartID
               INNER JOIN AnswerValue av
                  ON     ip.QuestionID = av.QuestionID
                     AND ip.OptionID = av.OptionID
                     AND av.ValueInt = ipc.OptionComponentID
               INNER JOIN Answer a
                  ON av.AnswerID = a.AnswerID
               INNER JOIN ProjectRoundUnit pru
                  ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
         WHERE     a.EndDT IS NOT NULL
               AND i.IdxID = 0
               AND SUBSTR(pru.SortString, 1, 10) = '1'
               AND DATE('Y', a.EndDT) >= 0
               AND DATE('Y', a.EndDT) <= 0
        GROUP BY i.IdxID,
                 a.AnswerID,
                 i.MaxVal,
                 il.Idx,
                 i.CX,
                 i.AllPartsRequired) tmp
 WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
GROUP BY tmp.IdxID, tmp.Idx",
				id,
				langID
			);
			var indexes = new List<Index>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var i = new Index {};
					indexes.Add(i);
				}
			}
			return indexes;
		}
		
		public Index ReadByIdAndLanguage(int idxID, int langID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class SQLiteProjectRepository : BaseSQLiteRepository<Project>, IProjectRepository //IProjectRoundUnitRepository
	{
		public ProjectRoundUnit ReadByID(int projectRoundUnitID)
		{
			string query = string.Format(
				@"SELECT SortString, 1 /*dbo.cf_unitLangID(ProjectRoundUnitID)*/
  FROM ProjectRoundUnit
 WHERE ProjectRoundUnitID = {0}",
				projectRoundUnitID
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var p = new ProjectRoundUnit();
					return p;
				}
			}
			return null;
		}
		
		public int CountForSortString(string sortString)
		{
			string query = string.Format(
				@"SELECT COUNT(*)
  FROM ProjectRoundUnit pru
 WHERE SUBSTR(pru.SortString, {1}) = '{0}'",
				sortString,
				sortString.Length
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
	}
	
	public class SQLiteWeightedQuestionOptionRepository : BaseSQLiteRepository<WeightedQuestionOption>, IWeightedQuestionOptionRepository
	{
		public IList<WeightedQuestionOption> FindByReportAndLanguage(int reportPartID, int langID)
		{
			string query = string.Format(
				@"SELECT rpc.WeightedQuestionOptionID,
       wqol.WeightedQuestionOption,
       wqo.TargetVal,
       wqo.YellowLow,
       wqo.GreenLow,
       wqo.GreenHigh,
       wqo.YellowHigh,
       wqo.QuestionID,
       wqo.OptionID,
  FROM ReportPartComponent rpc
       INNER JOIN WeightedQuestionOption wqo
          ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
       INNER JOIN WeightedQuestionOptionLang wqol
          ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID
             AND wqol.LangID = {1}
 WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportPartID,
				langID
			);
			var options = new List<WeightedQuestionOption>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var o = new WeightedQuestionOption();
					o.Id = rs.GetInt32(0);
					o.LocalizedOption = new WeightedQuestionOptionLanguage { Question = rs.GetString(1) };
					o.TargetValue = rs.GetInt32(2);
					o.YellowLow = rs.GetInt32(3);
					o.GreenLow = rs.GetInt32(4);
					o.GreenHigh = rs.GetInt32(5);
					o.YellowHigh = rs.GetInt32(6);
					o.Question = new Question { Id = rs.GetInt32(7) };
					o.Option = new Option { Id = rs.GetInt32(8) };
					options.Add(o);
				}
			}
			return options;
		}
		
		public IList<WeightedQuestionOption> FindByReportAndLanguage(int reportPartID)
		{
			string query = string.Format(
				@"SELECT rpc.WeightedQuestionOptionID,
       wqo.YellowLow,
       wqo.GreenLow,
       wqo.GreenHigh,
       wqo.YellowHigh,
       wqo.QuestionID,
       wqo.OptionID,
  FROM    ReportPartComponent rpc
       INNER JOIN
          WeightedQuestionOption wqo
       ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
 WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportPartID
			);
			var options = new List<WeightedQuestionOption>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var o = new WeightedQuestionOption();
					o.Id = rs.GetInt32(0);
					o.YellowLow = rs.GetInt32(1);
					o.GreenLow = rs.GetInt32(2);
					o.GreenHigh = rs.GetInt32(3);
					o.YellowHigh = rs.GetInt32(4);
					o.Question = new Question { Id = rs.GetInt32(5) };
					o.Option = new Option { Id = rs.GetInt32(6) };
					options.Add(o);
				}
			}
			return options;
		}
	}
	
	public class SQLiteAnswerRepository : BaseSQLiteRepository<Answer>, IAnswerRepository
	{
		public Answer ReadByKey(string key)
		{
			string query = string.Format(
				@"SELECT a.AnswerID,
       /*dbo.cf_unitLangID(a.ProjectRoundUnitID),*/
       a.ProjectRoundUserID
  FROM Answer a
 WHERE REPLACE(a.AnswerKey,'-','') = '{0}'",
				key
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var a = new Answer();
					a.Id = rs.GetInt32(0);
					a.Project = new ProjectRoundUnit { Id = rs.GetInt32(1) }; // TODO:
					return a;
				}
			}
			return null;
		}
		
		public int CountByProject(int projectRoundUserID, int yearFrom, int yearTo)
		{
			string query = string.Format(
				@"SELECT COUNT(*) /*DISTINCT dbo.cf_yearMonthDay(a.EndDT))*/
  FROM Answer a
 WHERE     a.EndDT IS NOT NULL
       AND DATE('Y', a.EndDT) >= {1}
       AND DATE('Y', a.EndDT) <= {2}
       AND a.ProjectRoundUserID = {0}",
				projectRoundUserID,
				yearFrom,
				yearTo
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountByDate(int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"SELECT COUNT(*)
  FROM    Answer a
       INNER JOIN
          ProjectRoundUnit pru
       ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
 WHERE     a.EndDT IS NOT NULL
       AND DATE('Y', a.EndDT) >= {0}
       AND DATE('Y', a.EndDT) <= {1}
       AND SUBSTR(pru.SortString, 5) = '{2}'",
				yearFrom,
				yearTo,
				sortString
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public IList<Answer> FindByQuestionAndOption(int questionID, int optionID, int yearFrom, int yearTo)
		{
			string query = string.Format(
				@"SELECT 1 /*dbo.cf_yearMonthDay(a.EndDT), AVG(av.ValueInt)*/
  FROM    Answer a
       LEFT OUTER JOIN
          AnswerValue av
       ON a.AnswerID = av.AnswerID AND av.QuestionID = {0} AND av.OptionID = {1}
 WHERE     a.EndDT IS NOT NULL
       AND a.ProjectRoundUserID = 1
       AND DATE('Y', a.EndDT) >= {2}
       AND DATE('Y', a.EndDT) <= {3}
GROUP BY 1 /*dbo.cf_yearMonthDay(a.EndDT)*/
ORDER BY 1 /*dbo.cf_yearMonthDay(a.EndDT)*/",
				questionID,
				optionID,
				yearFrom,
				yearTo
			);
			var answers = new List<Answer>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var a = new Answer();
					a.Average = rs.GetFloat(1);
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public Answer ReadByQuestionAndOption(int answerID, int questionID, int optionID)
		{
			string query = string.Format(
				@"SELECT av.ValueInt
  FROM AnswerValue av
 WHERE     av.DeletedSessionID IS NULL
       AND av.AnswerID = {0}
       AND av.QuestionID = {1}
       AND av.OptionID = {2}",
				answerID,
				questionID,
				optionID
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var a = new Answer();
					a.Value = new AnswerValue { Value = rs.GetFloat(0) };
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"SELECT 1 /*{0}(MAX(a.EndDT)) - {0}(MIN(a.EndDT))*/,
       1 /*{0}(MIN(a.EndDT))*/,
       1 /*{0}(MAX(a.EndDT))*/
  FROM Answer a
       INNER JOIN ProjectRoundUnit pru
          ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
       INNER JOIN ProjectRound pr
          ON pru.ProjectRoundID = pr.ProjectRoundID
 WHERE     a.EndDT IS NOT NULL
       AND a.EndDT >= pr.Started
       AND DATE('Y', a.EndDT) >= {1}
       AND DATE('Y', a.EndDT) <= {2}
       AND SUBSTR(pru.SortString, {4}) = '{3}'",
				groupBy,
				yearFrom,
				yearTo,
				sortString,
				sortString.Length
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var a = new Answer();
					a.DummyValue1 = rs.GetInt32(0);
					a.DummyValue2 = rs.GetInt32(1);
					a.DummyValue3 = rs.GetInt32(2);
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadMinMax(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"SELECT MAX(tmp2.VA + tmp2.SD), MIN(tmp2.VA - tmp2.SD)
  FROM (SELECT AVG(tmp.V) AS VA, STDEV(tmp.V) AS SD
          FROM (SELECT 1 /*{0}(a.EndDT)*/ AS DT, AVG(av.ValueInt) AS V
                  FROM Answer a
                       INNER JOIN AnswerValue av
                          ON     a.AnswerID = av.AnswerID
                             AND av.QuestionID = {1}
                             AND av.OptionID = {2}
                       INNER JOIN ProjectRoundUnit pru
                          ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
                       INNER JOIN ProjectRound pr
                          ON pru.ProjectRoundID = pr.ProjectRoundID
                 WHERE     a.EndDT IS NOT NULL
                       AND DATE('Y', a.EndDT) >= {3}
                       AND DATE('Y', a.EndDT) <= {4}
                       AND SUBSTR(pru.SortString, 5) = '{5}'
                GROUP BY a.ProjectRoundUserID, 1 /*{0}(a.EndDT)*/) tmp
        GROUP BY tmp.DT) tmp2",
				groupBy,
				questionID,
				optionID,
				yearFrom,
				yearTo,
				sortString
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var a = new Answer();
					a.Max = rs.GetInt32(0);
					a.Min = rs.GetInt32(1);
					return a;
				}
			}
			return null;
		}
		
		public int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString)
		{
			throw new NotImplementedException();
		}
	}
	
	public class SQLiteSponsorRepository : BaseSQLiteRepository<Sponsor>, ISponsorRepository
	{
		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorID, int langID)
		{
			string query = string.Format(
				@"SELECT IFNULL(sprul.Nav, '?'), spru.ProjectRoundUnitID
  FROM    SponsorProjectRoundUnit spru
       LEFT OUTER JOIN
          SponsorProjectRoundUnitLang sprul
       ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
 WHERE spru.SponsorID = {0} AND IFNULL(sprul.LangID, 1) = {1}",
				sponsorID,
				langID
			);
			var projects = new List<SponsorProjectRoundUnit>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var p = new SponsorProjectRoundUnit();
					projects.Add(p);
				}
			}
			return projects;
		}
		
		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"SELECT sbq.BQID, BQ.Internal
  FROM SponsorBQ sbq INNER JOIN BQ ON BQ.BQID = sbq.BQID
 WHERE     (BQ.Comparison = 1 OR sbq.Hidden = 1)
       AND BQ.Type IN (1, 7)
       AND sbq.SponsorID = {0}",
				sponsorID
			);
			var sponsors = new List<SponsorBackgroundQuestion>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var s = new SponsorBackgroundQuestion {};
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
	}
	
	public class SQLiteReportRepository : BaseSQLiteRepository<Report>, IReportRepository
	{
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			string query = string.Format(
				@"SELECT rp.ReportPartID,
       rpl.Subject,
       rpl.Header,
       rpl.Footer,
       rp.Type
  FROM ProjectRoundUnit pru
       INNER JOIN Report r
          ON r.ReportID = pru.ReportID
       INNER JOIN ReportPart rp
          ON r.ReportID = rp.ReportID
       INNER JOIN ReportPartLang rpl
          ON rp.ReportPartID = rpl.ReportPartID AND rpl.LangID = {1}
 WHERE pru.ProjectRoundUnitID = {0}
ORDER BY rp.SortOrder",
				projectRoundID,
				langID
			);
			var languages = new List<ReportPartLanguage>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var l = new ReportPartLanguage {
						Id = rs.GetInt32(0),
						Subject = rs.GetString(1),
						Header = rs.GetString(2),
						Footer = rs.GetString(3),
					};
					languages.Add(l);
				}
			}
			return languages;
		}
		
		public ReportPart FindByReportPart(int reportPartID)
		{
			string query = string.Format(
				@"SELECT rp.Type,
       (SELECT COUNT(*)
          FROM ReportPartComponent rpc
         WHERE rpc.ReportPartID = rp.ReportPartID),
       rp.QuestionID,
       rp.OptionID,
       rp.RequiredAnswerCount,
       rp.PartLevel
  FROM ReportPart rp
 WHERE rp.ReportPartID = {0}",
				reportPartID
			);
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				if (rs.Read()) {
					var p = new ReportPart { };
					p.Components = new List<ReportPartComponent>(rs.GetInt32(1));
					p.Question = new Question { Id = rs.GetInt32(2) };
					return p;
				}
			}
			return null;
		}
		
		public IList<ReportPartComponent> FindComponents(int reportID)
		{
			throw new NotImplementedException();
		}
	}
	
	public class SQLiteLanguageRepository : BaseSQLiteRepository<Language>, ILanguageRepository
	{
		public IList<Language> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"SELECT sprul.LangID,
       spru.ProjectRoundUnitID,
       l.LID,
       l.Language
  FROM SponsorProjectRoundUnit spru
       LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul
          ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
       INNER JOIN LID l
          ON IFNULL(sprul.LangID, 1) = l.LID
 WHERE spru.SponsorID = {0}
ORDER BY spru.SortOrder, spru.SponsorProjectRoundUnitID, l.LID",
				sponsorID);
			var languages = new List<Language>();
			using (SQLiteDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var l = new Language {
						Id = rs.GetInt32(2),
						Name = rs.GetString(3)
					};
					languages.Add(l);
				}
			}
			return languages;
		}
	}
}
