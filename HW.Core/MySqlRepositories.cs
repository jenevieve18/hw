//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace HW.Core
{
	public class MySqlRepositoryFactory : IRepositoryFactory
	{
		public ILanguageRepository CreateLanguageRepository()
		{
			return new MySqlLanguageRepository();
		}
		
		public IDepartmentRepository CreateDepartmentRepository()
		{
			return new MySqlDepartmentRepository();
		}
		
		public IProjectRepository CreateProjectRepository()
		{
			return new MySqlProjectRepository();
		}
		
		public ISponsorRepository CreateSponsorRepository()
		{
			return new MySqlSponsorRepository();
		}
		
		public IReportRepository CreateReportRepository()
		{
			return new MySqlReportRepository();
		}
		
		public IAnswerRepository CreateAnswerRepository()
		{
			throw new NotImplementedException();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			throw new NotImplementedException();
		}
		
		public IIndexRepository CreateIndexRepository()
		{
			throw new NotImplementedException();
		}
	}
	
	public class BaseMySqlRepository<T> : IBaseRepository<T>
	{
		MySqlConnection con;
		
		public BaseMySqlRepository()
		{
			con = new MySqlConnection(ConfigurationSettings.AppSettings["mysql"]);
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
		
		protected MySqlDataReader ExecuteReader(string query)
		{
			MySqlCommand cmd = new MySqlCommand(query, con);
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
	
	public class MySqlLanguageRepository : BaseMySqlRepository<Language>, ILanguageRepository
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
	
	public class MySqlProjectRepository : BaseMySqlRepository<Project>, IProjectRepository //IProjectRoundUnitRepository
	{
		public ProjectRoundUnit ReadByID(int projectRoundUnitID)
		{
			string query = string.Format(
				@"SELECT SortString, 1 /*dbo.cf_unitLangID(ProjectRoundUnitID)*/
  FROM ProjectRoundUnit
 WHERE ProjectRoundUnitID = {0}",
				projectRoundUnitID
			);
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
	}
	
	public class MySqlSponsorRepository : BaseMySqlRepository<Sponsor>, ISponsorRepository
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
				while (rs.Read()) {
					var s = new SponsorBackgroundQuestion {};
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
	}
	
	public class MySqlDepartmentRepository : BaseMySqlRepository<Department>, IDepartmentRepository
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
	
	public class MySqlReportRepository : BaseMySqlRepository<Report>, IReportRepository
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
			using (MySqlDataReader rs = ExecuteReader(query)) {
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
}
