//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HW.Core
{
	public class SqlRepositoryFactory : IRepositoryFactory
	{
		public ILanguageRepository CreateLanguageRepository()
		{
			return new SqlLanguageRepository();
		}
		
		public IDepartmentRepository CreateDepartmentRepository()
		{
			return new SqlDepartmentRepository();
		}
		
		public IProjectRepository CreateProjectRepository()
		{
			return new SqlProjectRepository();
		}
		
		public ISponsorRepository CreateSponsorRepository()
		{
			return new SqlSponsorRepository();
		}
		
		public IReportRepository CreateReportRepository()
		{
			return new SqlReportRepository();
		}
		
		public IAnswerRepository CreateAnswerRepository()
		{
			return new SqlAnswerRepository();
		}
		
		public IOptionRepository CreateOptionRepository()
		{
			return new SqlOptionRepository();
		}
		
		public IIndexRepository CreateIndexRepository()
		{
			return new SqlIndexRepository();
		}
		
		public IQuestionRepository CreateQuestionRepository()
		{
			return new SqlQuestionRepository();
		}
		
		public IManagerFunctionRepository CreateManagerFunctionRepository()
		{
			return new SqlManagerFunctionRepository();
		}
		
		public IExerciseRepository CreateExerciseRepository()
		{
			return new SqlExerciseRepository();
		}
		
		public IUserRepository CreateUserRepository()
		{
			return new SqlUserRepository();
		}
	}
	
	public class BaseSqlRepository<T> : IBaseRepository<T>
	{
		SqlConnection con;
		
		public BaseSqlRepository()
		{
		}
		
		public virtual void SaveOrUpdate(T t)
		{
			throw new NotImplementedException();
		}
		
		public void Delete(T t)
		{
			throw new NotImplementedException();
		}
		
		public virtual T Read(int id)
		{
			throw new NotImplementedException();
		}
		
		public virtual IList<T> FindAll()
		{
			throw new NotImplementedException();
		}
		
		protected void ExecuteNonQuery(string query, string connectionName, params SqlParameter[] parameters)
		{
			con = new SqlConnection(ConfigurationSettings.AppSettings[connectionName]);
			OpenConnection();
			SqlCommand cmd = new SqlCommand(query, con);
			foreach (var p in parameters) {
				if (p.Value == null) {
					p.Value = DBNull.Value;
				}
				cmd.Parameters.Add(p);
			}
			cmd.ExecuteNonQuery();
		}
		
		protected SqlDataReader ExecuteReader(string query)
		{
			return ExecuteReader(query, "SqlConnection");
		}
		
		protected SqlDataReader ExecuteReader(string query, string connectionName)
		{
			con = new SqlConnection(ConfigurationSettings.AppSettings[connectionName]);
			OpenConnection();
			SqlCommand cmd = new SqlCommand(query, con);
			return cmd.ExecuteReader(CommandBehavior.CloseConnection);
		}
		
		protected DateTime? GetDateTime(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? null : (DateTime?)rs.GetDateTime(index);
		}
		
		protected void SetDateTime(DateTime date, SqlDataReader rs, int index)
		{
			if (rs.IsDBNull(index)) {
				date = rs.GetDateTime(index);
			}
		}
		
		protected string GetString(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? null : rs.GetString(index);
		}
		
		protected bool GetBoolean(SqlDataReader rs, int index)
		{
			return rs.IsDBNull(index) ? false : rs.GetBoolean(index);
		}
		
		protected int GetInt32(SqlDataReader rs, int index)
		{
//			return rs.IsDBNull(index) ? -1 : rs.GetInt32(index);
			return rs.IsDBNull(index) ? 0 : rs.GetInt32(index);
		}
		
		void CloseConnection()
		{
			if (con.State == ConnectionState.Open) {
				con.Close();
			}
		}
		
		void OpenConnection()
		{
			if (con.State == ConnectionState.Closed) {
				con.Open();
			}
		}
	}
	
	public class SqlAnswerRepository : BaseSqlRepository<Answer>, IAnswerRepository
	{
		public void UpdateAnswer(int projectRoundUnitID, int projectRoundUserID)
		{
			string query = string.Format(
				@"
UPDATE [eform]..[Answer] SET ProjectRoundUnitID = {0} WHERE ProjectRoundUserID = {1}",
				projectRoundUnitID,
				projectRoundUserID
			);
//			Db2.exec(query, "eFormSqlConnection");
			Db.exec(query, "eFormSqlConnection");
		}
		
		public int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM Answer a
INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
WHERE a.EndDT IS NOT NULL
AND av.ValueInt = {0}
{1}
{2}
AND av.OptionID = {3}
AND av.QuestionID = {4}
AND LEFT(pru.SortString, 5) = '{5}'",
				val,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				optionID,
				questionID,
				sortString
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountByProject(int projectRoundUserID, int yearFrom, int yearTo)
		{
			string query = string.Format(
				@"
SELECT COUNT(DISTINCT dbo.cf_yearMonthDay(a.EndDT))
FROM Answer a
WHERE a.EndDT IS NOT NULL
{1}
{2}
AND a.ProjectRoundUserID = {0}",
				projectRoundUserID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : ""
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountByDate(int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM Answer a
INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
WHERE a.EndDT IS NOT NULL
{0}
{1}
AND LEFT(pru.SortString, {3}) = '{2}'",
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString,
				sortString.Length
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public Answer ReadByProjectRound(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT pru.Email,
	opl.Text
FROM ProjectRoundUser pru
INNER JOIN Answer a ON pru.ProjectRoundUserID = a.ProjectRoundUserID
INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND (av.QuestionID = 2473 OR av.QuestionID = 3470) AND (av.OptionID = 421 OR av.OptionID = 989) AND (av.ValueInt = 1262 OR av.ValueInt = 2623)
LEFT OUTER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 2588 AND av2.OptionID = 711
LEFT OUTER JOIN OptionComponentLang opl ON av2.ValueInt = opl.OptionComponentID AND opl.LangID = 1
WHERE a.EndDT IS NOT NULL AND pru.ProjectRoundID = {0}
ORDER BY pru.Email",
				projectRoundID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer {
						ProjectRoundUser = new ProjectRoundUser { Email = rs.GetString(0) }
					};
					a.Values = new List<AnswerValue>();
					do {
						var o = new Option {
							CurrentComponent = new OptionComponentLanguage { Text = rs.GetString(1) }
						};
						a.Values.Add(new AnswerValue { Option = o });
					}
					while (rs.Read());
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadByKey(string key)
		{
			string query = string.Format(
				@"
SELECT a.AnswerID,
	dbo.cf_unitLangID(a.ProjectRoundUnitID),
	a.ProjectRoundUserID
FROM Answer a
WHERE REPLACE(a.AnswerKey,'-','') = '{0}'",
				key
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer() {
						Id = rs.GetInt32(0),
						ProjectRoundUser = new ProjectRoundUser { Id = rs.GetInt32(2) }
					};
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadByQuestionAndOption(int answerID, int questionID, int optionID)
		{
			string query = string.Format(
				@"
SELECT av.ValueInt
FROM AnswerValue av
WHERE av.DeletedSessionID IS NULL
AND av.AnswerID = {0}
AND av.QuestionID = {1}
AND av.OptionID = {2}",
				answerID,
				questionID,
				optionID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer();
					a.Values = new List<AnswerValue>(
						new AnswerValue[] {
							new AnswerValue { Answer = a, ValueInt = rs.GetInt32(0) }
						}
					);
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"
SELECT {0}(MAX(a.EndDT)) - {0}(MIN(a.EndDT)),
	{0}(MIN(a.EndDT)),
	{0}(MAX(a.EndDT))
FROM Answer a
INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID
WHERE a.EndDT IS NOT NULL
AND a.EndDT >= pr.Started
{1}
{2}
AND LEFT(pru.SortString, {4}) = '{3}'",
				groupBy,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString,
				sortString.Length
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
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
				@"
SELECT MAX(tmp2.VA + tmp2.SD),
	MIN(tmp2.VA - tmp2.SD)
FROM (
	SELECT AVG(tmp.V) AS VA, STDEV(tmp.V) AS SD
	FROM (
		SELECT {0}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
		FROM Answer a
		INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
			AND av.QuestionID = {1}
			AND av.OptionID = {2}
		INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
		INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID
		WHERE a.EndDT IS NOT NULL
		{3}
		{4}
		AND LEFT(pru.SortString, 5) = '{5}'
		GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
	) tmp
	GROUP BY tmp.DT
) tmp2",
				groupBy,
				questionID,
				optionID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer() {
						Max = GetInt32(rs, 0), //rs.GetInt32(0),
						Min = GetInt32(rs, 1) //rs.GetInt32(1)
					};
					return a;
				}
			}
			return null;
		}
		
		public IList<BackgroundAnswer> FindBackgroundAnswers(int bqID)
		{
			string query = string.Format(
				@"
SELECT BAID,
	Internal
FROM BA
WHERE BQID = {0}
ORDER BY SortOrder",
				bqID
			);
			var answers = new List<BackgroundAnswer>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a = new BackgroundAnswer {
						Id = rs.GetInt32(0),
						Internal = rs.GetString(1)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
		
//		public IList<Answer> e(int projectRoundID)
//		{
//			string query = string.Format(
//				@"
		//SELECT av1.ValueInt,
//	u.Email
		//FROM ProjectRoundUser u
		//INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
		//INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 350 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL
		//WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
//				projectRoundID
//			);
//			var answers =  new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				while (rs.Read()) {
//					var a = new Answer { ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(1) }};
//					a.Values = new List<AnswerValue>(
//						new AnswerValue[] {
//							new AnswerValue { ValueInt = rs.GetInt32(0) },
//						}
//					);
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
//
//		public IList<Answer> d(int projectRoundID)
//		{
//			string query = string.Format(
//				@"
		//SELECT av1.ValueInt,
//	u.Email
		//FROM ProjectRoundUser u
		//INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
		//INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 349 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL
		//WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
//				projectRoundID
//			);
//			var answers =  new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				while (rs.Read()) {
//					var a = new Answer { ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(1) }};
//					a.Values = new List<AnswerValue>(
//						new AnswerValue[] {
//							new AnswerValue { ValueInt = rs.GetInt32(0) },
//						}
//					);
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
//
//		public IList<Answer> c(int projectRoundID)
//		{
//			string query = string.Format(
//				@"
		//SELECT av1.ValueInt,
//	u.Email
		//FROM ProjectRoundUser u
		//INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
		//INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 370 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL
		//WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
//				projectRoundID
//			);
//			var answers =  new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				while (rs.Read()) {
//					var a = new Answer { ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(1) }};
//					a.Values = new List<AnswerValue>(
//						new AnswerValue[] {
//							new AnswerValue { ValueInt = rs.GetInt32(0) },
//						}
//					);
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
//
//		public IList<Answer> z(int projectRoundID)
//		{
//			string query = string.Format(
//				@"
		//SELECT av1.ValueInt,
//	u.Email
		//FROM ProjectRoundUser u
		//INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
		//INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 331 AND av1.OptionID = 98 AND av1.DeletedSessionID IS NULL
		//WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
//				projectRoundID
//			);
//			var answers =  new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				while (rs.Read()) {
//					var a = new Answer {
//						ProjectRoundUser = new ProjectRoundUser { Email = rs.GetString(1) }
//					};
//					a.Values = new List<AnswerValue>(
//						new AnswerValue[] {
//							new AnswerValue { ValueInt = rs.GetInt32(0) }
//						}
//					);
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
//
//		public IList<Answer> y(int projectRoundID)
//		{
//			string query = string.Format(
//				@"
		//SELECT av1.ValueInt,
//	u.Email
		//FROM ProjectRoundUser u
		//INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
		//INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 374 AND av1.OptionID = 86 AND av1.DeletedSessionID IS NULL
		//WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
//				projectRoundID
//			);
//			var answers =  new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				while (rs.Read()) {
//					var a = new Answer {
//						ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(1) }
//					};
//					a.Values = new List<AnswerValue>(
//						new AnswerValue [] {
//							new AnswerValue { ValueInt = rs.GetInt32(0) }
//						}
//					);
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
//
		public IList<Answer> FindByRoundQuestionAndOption(int projectRoundID, int questionID, int optionID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueInt,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID
	AND av1.QuestionID = {1}
	AND av1.OptionID = {2}
	AND av1.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL
AND u.ProjectRoundID = {0}",
				projectRoundID,
				questionID,
				optionID
			);
			var answers =  new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer {
						ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(1) }
					};
					a.Values = new List<AnswerValue>(
						new AnswerValue [] {
							new AnswerValue { ValueInt = rs.GetInt32(0) }
						}
					);
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> g(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueDecimal,
	av2.ValueInt,
	av3.ValueDecimal,
	av4.ValueInt,
	av5.ValueInt,
	av6.ValueInt,
	av7.ValueInt,
	av8.ValueInt,
	av9.ValueInt,
	av10.ValueInt,
	av11.ValueDecimal,
	av12.ValueDecimal,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 311 AND av1.OptionID = 81 AND av1.DeletedSessionID IS NULL
INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 310 AND av2.OptionID = 79 AND av2.DeletedSessionID IS NULL
INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 538 AND av3.OptionID = 82 AND av3.DeletedSessionID IS NULL
INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 368 AND av4.OptionID = 109 AND av4.DeletedSessionID IS NULL
INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 369 AND av5.OptionID = 110 AND av5.DeletedSessionID IS NULL
INNER JOIN AnswerValue av6 ON a.AnswerID = av6.AnswerID AND av6.QuestionID = 539 AND av6.OptionID = 134 AND av6.DeletedSessionID IS NULL
LEFT OUTER JOIN AnswerValue av7 ON a.AnswerID = av7.AnswerID AND av7.QuestionID = 352 AND av7.OptionID = 104 AND av7.DeletedSessionID IS NULL
LEFT OUTER JOIN AnswerValue av8 ON a.AnswerID = av8.AnswerID AND av8.QuestionID = 356 AND av8.OptionID = 90 AND av8.DeletedSessionID IS NULL
LEFT OUTER JOIN AnswerValue av9 ON a.AnswerID = av9.AnswerID AND av9.QuestionID = 638 AND av9.OptionID = 90 AND av9.DeletedSessionID IS NULL
INNER JOIN AnswerValue av10 ON a.AnswerID = av10.AnswerID AND av10.QuestionID = 639 AND av10.OptionID = 137 AND av10.DeletedSessionID IS NULL
INNER JOIN AnswerValue av11 ON a.AnswerID = av11.AnswerID AND av11.QuestionID = 314 AND av11.OptionID = 83 AND av11.DeletedSessionID IS NULL
INNER JOIN AnswerValue av12 ON a.AnswerID = av12.AnswerID AND av12.QuestionID = 313 AND av12.OptionID = 82 AND av12.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
				projectRoundID
			);
			var answers =  new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer { ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(12) }};
					a.Values = new List<AnswerValue>();
					a.Values.Add(new AnswerValue { ValueDecimal = rs.GetDecimal(0) });
					a.Values.Add(new AnswerValue { ValueInt = GetInt32(rs, 1) });
					a.Values.Add(new AnswerValue { ValueDecimal = rs.GetDecimal(2) });
					a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(3) });
					a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(4) });
					a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(5) });
					a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(6) });
					a.Values.Add(new AnswerValue { ValueInt = GetInt32(rs, 7) });
					a.Values.Add(new AnswerValue { ValueInt = GetInt32(rs, 8) });
					a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(9) });
					a.Values.Add(new AnswerValue { ValueDecimal = rs.GetDecimal(10) });
					a.Values.Add(new AnswerValue { ValueDecimal = rs.GetDecimal(11) });
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> f(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueInt,
	av2.ValueInt,
	av3.ValueInt,
	av4.ValueInt,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 364 AND av1.OptionID = 108 AND av1.DeletedSessionID IS NULL
INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 208 AND av2.OptionID = 41 AND av2.DeletedSessionID IS NULL
INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 210 AND av3.OptionID = 41 AND av3.DeletedSessionID IS NULL
INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 213 AND av4.OptionID = 42 AND av4.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
				projectRoundID
			);
			var answers =  new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer { ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(4) }};
					a.Values = new List<AnswerValue>(
						new AnswerValue[] {
							new AnswerValue { ValueInt = rs.GetInt32(0) },
							new AnswerValue { ValueInt = rs.GetInt32(1) },
							new AnswerValue { ValueInt = rs.GetInt32(2) },
							new AnswerValue { ValueInt = rs.GetInt32(3) },
						}
					);
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> b(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueInt,
	av2.ValueInt,
	av3.ValueInt,
	av4.ValueInt,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 339 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL
INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 346 AND av2.OptionID = 90 AND av2.DeletedSessionID IS NULL
INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 347 AND av3.OptionID = 90 AND av3.DeletedSessionID IS NULL
INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 348 AND av4.OptionID = 90 AND av4.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
				projectRoundID
			);
			var answers =  new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer { ProjectRoundUser = new ProjectRoundUser{ Email = rs.GetString(4) }};
					a.Values = new List<AnswerValue>(
						new AnswerValue[] {
							new AnswerValue { ValueInt = rs.GetInt32(0) },
							new AnswerValue { ValueInt = rs.GetInt32(1) },
							new AnswerValue { ValueInt = rs.GetInt32(2) },
							new AnswerValue { ValueInt = rs.GetInt32(3) }
						}
					);
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> a(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueInt,
	av2.ValueInt,
	av3.ValueInt,
	av4.ValueInt,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 337 AND av1.OptionID = 90 AND av1.DeletedSessionID IS NULL
INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 341 AND av2.OptionID = 90 AND av2.DeletedSessionID IS NULL
INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 342 AND av3.OptionID = 90 AND av3.DeletedSessionID IS NULL
INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 343 AND av4.OptionID = 90 AND av4.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
				projectRoundID
			);
			var answers =  new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer { ProjectRoundUser = new ProjectRoundUser { Email = rs.GetString(4) }};
					a.Values = new List<AnswerValue>(
						new AnswerValue[] {
							new AnswerValue { ValueInt = rs.GetInt32(0) },
							new AnswerValue { ValueInt = rs.GetInt32(1) },
							new AnswerValue { ValueInt = rs.GetInt32(2) },
							new AnswerValue { ValueInt = rs.GetInt32(3) },
						}
					);
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> x(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueInt,
	av2.ValueInt,
	av3.ValueInt,
	av4.ValueInt,
	av5.ValueInt,
	av6.ValueInt,
	av7.ValueInt,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 387 AND av1.OptionID = 115 AND av1.DeletedSessionID IS NULL
INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 388 AND av2.OptionID = 115 AND av2.DeletedSessionID IS NULL
INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 389 AND av3.OptionID = 115 AND av3.DeletedSessionID IS NULL
INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 390 AND av4.OptionID = 115 AND av4.DeletedSessionID IS NULL
INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 391 AND av5.OptionID = 115 AND av5.DeletedSessionID IS NULL
INNER JOIN AnswerValue av6 ON a.AnswerID = av6.AnswerID AND av6.QuestionID = 392 AND av6.OptionID = 115 AND av6.DeletedSessionID IS NULL
INNER JOIN AnswerValue av7 ON a.AnswerID = av7.AnswerID AND av7.QuestionID = 393 AND av7.OptionID = 122 AND av7.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
				projectRoundID
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer();
					a.ProjectRoundUser = new ProjectRoundUser { Email = rs.GetString(7) };
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByProjectRound(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT av1.ValueInt,
	av2.ValueInt,
	av3.ValueInt,
	av4.ValueInt,
	av5.ValueInt,
	av13.ValueInt,
	av14.ValueInt,
	av15.ValueInt,
	av16.ValueInt,
	u.Email
FROM ProjectRoundUser u
INNER JOIN Answer a ON a.ProjectRoundUserID = u.ProjectRoundUserID
INNER JOIN AnswerValue av1 ON a.AnswerID = av1.AnswerID AND av1.QuestionID = 380 AND av1.OptionID = 114 AND av1.DeletedSessionID IS NULL
INNER JOIN AnswerValue av2 ON a.AnswerID = av2.AnswerID AND av2.QuestionID = 381 AND av2.OptionID = 114 AND av2.DeletedSessionID IS NULL
INNER JOIN AnswerValue av3 ON a.AnswerID = av3.AnswerID AND av3.QuestionID = 382 AND av3.OptionID = 114 AND av3.DeletedSessionID IS NULL
INNER JOIN AnswerValue av4 ON a.AnswerID = av4.AnswerID AND av4.QuestionID = 383 AND av4.OptionID = 114 AND av4.DeletedSessionID IS NULL
INNER JOIN AnswerValue av5 ON a.AnswerID = av5.AnswerID AND av5.QuestionID = 384 AND av5.OptionID = 114 AND av5.DeletedSessionID IS NULL
INNER JOIN AnswerValue av13 ON a.AnswerID = av13.AnswerID AND av13.QuestionID = 401 AND av13.OptionID = 116 AND av13.DeletedSessionID IS NULL
INNER JOIN AnswerValue av14 ON a.AnswerID = av14.AnswerID AND av14.QuestionID = 402 AND av14.OptionID = 116 AND av14.DeletedSessionID IS NULL
INNER JOIN AnswerValue av15 ON a.AnswerID = av15.AnswerID AND av15.QuestionID = 403 AND av15.OptionID = 116 AND av15.DeletedSessionID IS NULL
INNER JOIN AnswerValue av16 ON a.AnswerID = av16.AnswerID AND av16.QuestionID = 404 AND av16.OptionID = 116 AND av16.DeletedSessionID IS NULL
WHERE a.EndDT IS NOT NULL AND u.ProjectRoundID = {0}",
				projectRoundID
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer();
					a.ProjectRoundUser = new ProjectRoundUser { Email = rs.GetString(9) };
					a.Values = new List<AnswerValue>(
						new AnswerValue[] {
							new AnswerValue { ValueInt = rs.GetInt32(0) },
							new AnswerValue { ValueInt = rs.GetInt32(1) },
							new AnswerValue { ValueInt = rs.GetInt32(2) },
							new AnswerValue { ValueInt = rs.GetInt32(3) },
							new AnswerValue { ValueInt = rs.GetInt32(4) },
							new AnswerValue { ValueInt = rs.GetInt32(5) },
							new AnswerValue { ValueInt = rs.GetInt32(6) },
							new AnswerValue { ValueInt = rs.GetInt32(7) },
							new AnswerValue { ValueInt = rs.GetInt32(8) }
						}
					);
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionWithYearSpan(int questionID, int optionID, int yearFrom, int yearTo)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_yearMonthDay(a.EndDT), AVG(av.ValueInt)
FROM Answer a
LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {0} AND av.OptionID = {1}
WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = 1
{2}
{3}
GROUP BY 1 dbo.cf_yearMonthDay(a.EndDT)
ORDER BY 1 dbo.cf_yearMonthDay(a.EndDT)",
				questionID,
				optionID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : ""
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer() {
						SomeString = rs.GetString(0),
						Average = rs.GetFloat(1)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
		{
			string query = string.Format(
				@"
SELECT tmp.DT,
	AVG(tmp.V),
	COUNT(tmp.V),
	STDEV(tmp.V)
FROM (
	SELECT {1}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
	FROM Answer a
	{0}
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
		AND av.QuestionID = {2}
		AND av.OptionID = {3}
	WHERE a.EndDT IS NOT NULL
	{4}
	{5}
	GROUP BY a.ProjectRoundUserID, {1}(a.EndDT)
) tmp
GROUP BY tmp.DT
ORDER BY tmp.DT",
				join,
				groupBy,
				questionID,
				optionID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : ""
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
				while (rs.Read()) {
					var a = new Answer {
						SomeInteger = rs.GetInt32(0),
						AverageV = rs.GetInt32(1),
						CountV = rs.GetInt32(2),
						StandardDeviation = rs.GetFloat(3)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
		{
			string query = string.Format(
				@"
SELECT tmp.DT,
	AVG(tmp.V),
	COUNT(tmp.V),
	STDEV(tmp.V)
FROM (
	SELECT {1}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
	FROM Answer a
	{0}
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
		AND av.QuestionID = {2}
		AND av.OptionID = {3}
	WHERE a.EndDT IS NOT NULL
	{4}
	{5}
	GROUP BY a.ProjectRoundUserID, {1}(a.EndDT)
) tmp
GROUP BY tmp.DT
ORDER BY tmp.DT",
				join,
				groupBy,
				questionID,
				optionID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : ""
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
				while (rs.Read()) {
					var a = new Answer {
						SomeInteger = rs.GetInt32(0),
						AverageV = rs.GetInt32(1),
						CountV = rs.GetInt32(2),
						StandardDeviation = rs.GetFloat(3)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionGrouped(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"
SELECT tmp.DT,
	AVG(tmp.V),
	COUNT(tmp.V),
	STDEV(tmp.V)
FROM (
	SELECT {0}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
	FROM Answer a
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
		AND av.QuestionID = {1}
		AND av.OptionID = {2}
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID
	WHERE a.EndDT IS NOT NULL
		AND a.EndDT >= pr.Started
		{3}
		{4}
		AND LEFT(pru.SortString, {6}) = '{5}'
	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
) tmp
GROUP BY tmp.DT
ORDER BY tmp.DT",
				groupBy,
				questionID,
				optionID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString,
				sortString.Length
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
				while (rs.Read()) {
					var a = new Answer {
						SomeInteger = rs.GetInt32(0),
						AverageV = rs.GetInt32(1),
						CountV = rs.GetInt32(2),
						StandardDeviation = rs.GetFloat(3)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
	}
	
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
						Id = rs.GetInt32(0),
						YellowLow = rs.GetInt32(1),
						GreenLow = rs.GetInt32(2),
						GreenHigh = rs.GetInt32(3),
						YellowHigh = rs.GetInt32(4),
						Question = new Question { Id = rs.GetInt32(5) },
						Option = new Option { Id = rs.GetInt32(6) }
					};
					return c;
				}
			}
			return null;
		}
		
		public ReportPart ReadReportPart(int reportPartID)
		{
			string query = string.Format(
				@"
SELECT rp.Type,
	(SELECT COUNT(*) FROM ReportPartComponent rpc WHERE rpc.ReportPartID = rp.ReportPartID),
	rp.QuestionID,
	rp.OptionID,
	rp.RequiredAnswerCount,
	rp.PartLevel
FROM ReportPart rp
WHERE rp.ReportPartID = {0}",
				reportPartID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var p = new ReportPart();
					p.Type = rs.GetInt32(0);
					p.Components = new List<ReportPartComponent>(rs.GetInt32(1));
					if (!rs.IsDBNull(2)) {
						p.Question = new Question { Id = rs.GetInt32(2) };
					}
					if (!rs.IsDBNull(3)) {
						p.Option = new Option { Id = rs.GetInt32(3) };
					}
					p.RequiredAnswerCount = rs.GetInt32(4);
					p.PartLevel = GetInt32(rs, 5);
					return p;
				}
			}
			return null;
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			string query = string.Format(
				@"
SELECT rp.ReportPartID,
	rpl.Subject,
	rpl.Header,
	rpl.Footer,
	rp.Type
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
		
		public IList<ReportPartComponent> FindComponents(int reportID)
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
FROM ReportPartComponent rpc INNER JOIN Idx i ON rpc.IdxID = i.IdxID
WHERE rpc.ReportPartID = {0}
ORDER BY rpc.SortOrder",
				reportID
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
	wqo.OptionID,
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
	wqo.OptionID,
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
	}
	
	public class SqlIndexRepository : BaseSqlRepository<Index>, IIndexRepository
	{
		public IList<Index> FindByLanguage(int idxID, int langID, int yearFrom, int yearTo, string sortString)
		{
			string query = string.Format(
				@"
SELECT AVG(tmp.AX),
	tmp.Idx,
	tmp.IdxID,
	COUNT(*) AS DX
FROM (
	SELECT 100 * CAST(SUM(ipc.Val * ip.Multiple) AS REAL) / i.MaxVal
		AS AX,
		i.IdxID,
		il.Idx,
		i.CX,
		i.AllPartsRequired,
		COUNT(*) AS BX
	FROM Idx i
	INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
	INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
	INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID
	INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID
		AND ip.OptionID = av.OptionID
		AND av.ValueInt = ipc.OptionComponentID
	INNER JOIN Answer a ON av.AnswerID = a.AnswerID
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	WHERE a.EndDT IS NOT NULL
		AND i.IdxID = {0}
		AND LEFT(pru.SortString, {5}) = '{4}'
		{2}
		{3}
	GROUP BY i.IdxID,
		a.AnswerID,
		i.MaxVal,
		il.Idx,
		i.CX,
		i.AllPartsRequired
) tmp
WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX
GROUP BY tmp.IdxID, tmp.Idx",
				idxID,
				langID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString,
				sortString.Length
			);
			var indexes = new List<Index>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var i = new Index();
					i.AverageAX = rs.GetFloat(0);
					i.Languages = new List<IndexLanguage>(
						new IndexLanguage[] {
							new IndexLanguage { IndexName = rs.GetString(1) }
						}
					);
					i.Id = rs.GetInt32(2);
					i.CountDX = rs.GetInt32(3);
					indexes.Add(i);
				}
			}
			return indexes;
		}
		
		public Index ReadByIdAndLanguage(int idxID, int langID)
		{
			string query = string.Format(
				@"
SELECT ip.OtherIdxID,
	il.Idx,
	i.MaxVal,
	ip.Multiple
FROM Idx i
INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = {1}
INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID
WHERE i.IdxID = {0}",
				idxID,
				langID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var i = new Index();
					i.Languages = new List<IndexLanguage>(
						new IndexLanguage[] {
							new IndexLanguage { IndexName = rs.GetString(1) }
						}
					);
					i.MaxValue = rs.GetInt32(2);
					i.Parts = new List<IndexPart>();
					do {
						i.Parts.Add(new IndexPart { OtherIndex = new Index { Id = rs.GetInt32(0) }, Multiple = rs.GetInt32(3) });
					} while (rs.Read());
					return i;
				}
			}
			return null;
		}
	}
	
	public class SqlOptionRepository : BaseSqlRepository<Option>, IOptionRepository
	{
		public int CountByOption(int optionID)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM OptionComponents WHERE OptionID = {0}",
				optionID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public IList<OptionComponentLanguage> FindComponentsByLanguage(int optionID, int langID)
		{
			string query = string.Format(
				@"
SELECT oc.OptionComponentID,
	ocl.Text
FROM OptionComponents ocs
INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID
INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = {1}
WHERE ocs.OptionID = {0}
ORDER BY ocs.SortOrder",
				optionID,
				langID
			);
			var components = new List<OptionComponentLanguage>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var c = new OptionComponentLanguage();
					c.Text = rs.GetString(1);
					c.Component = new OptionComponent { Id = rs.GetInt32(0) };
					components.Add(c);
				}
			}
			return components;
		}
	}
	
	public class SqlProjectRepository : BaseSqlRepository<Project>, IProjectRepository
	{
		public void UpdateProjectRoundUser(int projectRoundUnitID, int proejctRoundUserID)
		{
			string query = string.Format(
				@"
UPDATE [eform]..[ProjectRoundUser] SET ProjectRoundUnitID = {0} WHERE ProjectRoundUserID = {1}",
				projectRoundUnitID,
				proejctRoundUserID
			);
//			Db2.exec(query);
			Db.exec(query, "eFormSqlConnection");
		}
		
		public int CountForSortString(string sortString)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM ProjectRoundUnit pru WHERE LEFT(pru.SortString, {1}) = '{0}'",
				sortString,
				sortString.Length
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public ProjectRoundUnit ReadRoundUnit(int projectRoundUnitID)
		{
			string query = string.Format(
				@"
SELECT SortString,
	dbo.cf_unitLangID(ProjectRoundUnitID)
FROM ProjectRoundUnit
WHERE ProjectRoundUnitID = {0}",
				projectRoundUnitID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var p = new ProjectRoundUnit();
					p.SortString = rs.GetString(0);
					p.Language = new Language { Id = rs.GetInt32(1) };
					return p;
				}
			}
			return null;
		}
		
		public ProjectRound ReadRound(int projectRoundID)
		{
			string query = string.Format(
				@"
SELECT Started,
   Closed
FROM ProjectRound
WHERE ProjectRoundID = {0}",
				projectRoundID
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var p = new ProjectRound {
						Started = GetDateTime(rs, 0),
						Closed = GetDateTime(rs, 1)
					};
					return p;
				}
			}
			return null;
		}
		
		public IList<ProjectRoundUnit> FindRoundUnitsBySortString(string sortString)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_projectUnitTree(pru.ProjectRoundUnitID, ' » '),
	SortString
FROM ProjectRoundUnit pru
WHERE LEFT(pru.SortString, {1}) = '{0}'",
				sortString,
				sortString.Length
			);
			var units = new List<ProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var u = new ProjectRoundUnit() {
						TreeString = rs.GetString(0),
						SortString = rs.GetString(1)
					};
					units.Add(u);
				}
			}
			return units;
		}
	}
	
	// HealthWatch Repositories
	
	public class SqlExerciseRepository : BaseSqlRepository<Exercise>, IExerciseRepository
	{
		public IList<Exercise> FindByAreaAndCategory(int areaID, int categoryID, int langID, int sort)
		{
			string query = string.Format(
				@"
SELECT el.New,
	NULL,
	evl.ExerciseVariantLangID,
	eal.ExerciseArea,
	eal.ExerciseAreaID,
	e.ExerciseImg,
	e.ExerciseID,
	ea.ExerciseAreaImg,
	el.Exercise,
	el.ExerciseTime,
	el.ExerciseTeaser,
	evl.ExerciseFile,
	evl.ExerciseFileSize,
	evl.ExerciseContent,
	evl.ExerciseWindowX,
	evl.ExerciseWindowY,
	et.ExerciseTypeID,
	etl.ExerciseType,
	etl.ExerciseSubtype,
	ecl.ExerciseCategory
FROM [ExerciseArea] ea
INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID
INNER JOIN [Exercise] e ON ea.ExerciseAreaID = e.ExerciseAreaID
INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
LEFT OUTER JOIN [ExerciseCategory] ec ON e.ExerciseCategoryID = ec.ExerciseCategoryID
LEFT OUTER JOIN [ExerciseCategoryLang] ecl ON ec.ExerciseCategoryID = ecl.ExerciseCategoryID AND ecl.Lang = eal.Lang
WHERE eal.Lang = el.Lang
AND e.RequiredUserLevel = 10
AND el.Lang = evl.Lang
AND evl.Lang = etl.Lang
AND etl.Lang = {0}
{1}
{2}
ORDER BY
{3}
HASHBYTES('MD2',CAST(RAND({4})*e.ExerciseID AS VARCHAR(16))) ASC,
et.ExerciseTypeSortOrder ASC",
				langID,
				(categoryID != 0 ? "AND e.ExerciseCategoryID = " + categoryID + " " : ""),
				(areaID != 0 ? "AND e.ExerciseAreaID = " + areaID + " " : ""),
				(sort == 1 ? "(SELECT COUNT(*) FROM ExerciseStats esX INNER JOIN ExerciseVariantLang evlX ON esX.ExerciseVariantLangID = evlX.ExerciseVariantLangID INNER JOIN ExerciseVariant evX ON evlX.ExerciseVariantID = evX.ExerciseVariantID WHERE evX.ExerciseID = e.ExerciseID) DESC, " : (sort == 2 ? "el.Exercise ASC, " : "")),
				DateTime.Now.Second * DateTime.Now.Minute
			);
			var exercises = new List<Exercise>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var e = new Exercise(); // TODO: Get exercise values!
					e.Id = rs.GetInt32(6);
					e.Image = GetString(rs, 5);
					e.CurrentLanguage = new ExerciseLanguage {
						IsNew = rs.GetBoolean(0),
						ExerciseName = rs.GetString(8),
						Time = rs.GetString(9),
						Teaser = rs.GetString(10)
					};
					e.CurrentArea = new ExerciseAreaLanguage {
						Id = rs.GetInt32(4),
						AreaName = rs.GetString(3)
					};
					e.CurrentVariant = new ExerciseVariantLanguage {
						Id = GetInt32(rs, 2),
						File = GetString(rs, 11),
						Size = GetInt32(rs, 12),
						Content = GetString(rs, 13),
						ExerciseWindowX = GetInt32(rs, 14),
						ExerciseWindowY = GetInt32(rs, 15)
					};
					e.CurrentType = new ExerciseTypeLanguage {
						TypeName = GetString(rs, 17),
						SubTypeName = GetString(rs, 18)
					};
					e.CurrentCategory = new ExerciseCategoryLanguage {
						CategoryName = GetString(rs, 19)
					};
					exercises.Add(e);
				}
			}
			return exercises;
		}
		
		public IList<ExerciseCategoryLanguage> FindCategories(int areaID, int categoryID, int langID)
		{
			string query = string.Format(
				@"
SELECT eal.ExerciseCategory,
	eal.ExerciseCategoryID
FROM [ExerciseCategory] ea
INNER JOIN [ExerciseCategoryLang] eal ON ea.ExerciseCategoryID = eal.ExerciseCategoryID
WHERE eal.Lang = {1}
AND (
	SELECT COUNT(*)
	FROM Exercise e
	INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
	INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
	INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
	INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
	INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
	WHERE e.ExerciseCategoryID = ea.ExerciseCategoryID
	{2}
	AND eal.Lang = el.Lang
	AND e.RequiredUserLevel = 10
	AND el.Lang = evl.Lang
	AND evl.Lang = etl.Lang
) > 0
ORDER BY CASE eal.ExerciseCategoryID WHEN {0} THEN NULL ELSE ea.ExerciseCategorySortOrder END",
				categoryID,
				langID,
				areaID != 0 ? "AND e.ExerciseAreaID = " + areaID + " " : ""
			);
			var categories = new List<ExerciseCategoryLanguage>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var c = new ExerciseCategoryLanguage {
						Category = new ExerciseCategory { Id = rs.GetInt32(1) },
						CategoryName = rs.GetString(0)
					};
					categories.Add(c);
				}
			}
			return categories;
		}
		
		public IList<ExerciseAreaLanguage> FindAreas(int areaID, int langID)
		{
			string query = string.Format(
				@"
SELECT eal.ExerciseArea,
	eal.ExerciseAreaID
FROM [ExerciseArea] ea
INNER JOIN [ExerciseAreaLang] eal ON ea.ExerciseAreaID = eal.ExerciseAreaID
WHERE eal.Lang = {1} AND (
	SELECT COUNT(*)
	FROM Exercise e
	INNER JOIN [ExerciseLang] el ON e.ExerciseID = el.ExerciseID
	INNER JOIN [ExerciseVariant] ev ON e.ExerciseID = ev.ExerciseID
	INNER JOIN [ExerciseVariantLang] evl ON ev.ExerciseVariantID = evl.ExerciseVariantID
	INNER JOIN [ExerciseType] et ON ev.ExerciseTypeID = et.ExerciseTypeID
	INNER JOIN [ExerciseTypeLang] etl ON et.ExerciseTypeID = etl.ExerciseTypeID
	WHERE e.ExerciseAreaID = ea.ExerciseAreaID
	AND eal.Lang = el.Lang
	AND e.RequiredUserLevel = 10
	AND el.Lang = evl.Lang
	AND evl.Lang = etl.Lang
) > 0
ORDER BY CASE eal.ExerciseAreaID WHEN {0} THEN NULL ELSE ea.ExerciseAreaSortOrder END",
				areaID,
				langID
			);
			var areas = new List<ExerciseAreaLanguage>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a = new ExerciseAreaLanguage {
						Area = new ExerciseArea { Id = rs.GetInt32(1) },
						AreaName = rs.GetString(0)
					};
					areas.Add(a);
				}
			}
			return areas;
		}
	}
	
	public class SqlQuestionRepository : BaseSqlRepository<Question>, IQuestionRepository
	{
		public IList<BackgroundQuestion> FindBackgroundQuestions(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT BQ.Internal,
	BQ.BQID,
	BQ.Type
FROM SponsorBQ sbq
INNER JOIN BQ ON sbq.BQID = BQ.BQID
WHERE sbq.SponsorID = {0}
AND sbq.Hidden = 1
ORDER BY sbq.SortOrder",
				sponsorID
			);
			var questions = new List<BackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var q = new BackgroundQuestion {
						Internal = rs.GetString(0),
						Id = rs.GetInt32(1),
						Type = rs.GetInt32(2)
					};
					questions.Add(q);
				}
			}
			return questions;
		}
		
		public IList<BackgroundQuestion> FindLikeBackgroundQuestions(int bqID)
		{
			string query = string.Format(
				@"
SELECT BQ.BQID, BQ.Internal FROM BQ WHERE BQ.BQID IN (23)",
				bqID
			);
			var questions = new List<BackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var q = new BackgroundQuestion {
						Id = rs.GetInt32(0),
						Internal = rs.GetString(1)
					};
					questions.Add(q);
				}
			}
			return questions;
		}
	}
	
	public class SqlManagerFunctionRepository : BaseSqlRepository<ManagerFunction>, IManagerFunctionRepository
	{
		public ManagerFunction ReadFirstFunctionBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT TOP (1) mf.ManagerFunction,
	mf.URL,
	mf.Expl
FROM ManagerFunction AS mf {0}",
				sponsorAdminID != -1 ? "INNER JOIN SponsorAdminFunction AS s ON s.ManagerFunctionID = mf.ManagerFunctionID WHERE s.SponsorAdminID = " + sponsorAdminID : ""
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var f = new ManagerFunction();
					f.Function = rs.GetString(0);
					f.URL = rs.GetString(1);
					f.Expl = rs.GetString(2);
					return f;
				}
			}
			return null;
		}
		
		public override IList<ManagerFunction> FindAll()
		{
			string query = string.Format(
				@"
SELECT ManagerFunctionID, ManagerFunction, Expl FROM ManagerFunction"
			);
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Id = rs.GetInt32(0),
						Function = rs.GetString(1),
						Expl = rs.GetString(2)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
		
		public IList<ManagerFunction> FindBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT mf.ManagerFunction,
mf.URL,
mf.Expl
FROM SponsorAdminFunction saf
INNER JOIN ManagerFunction mf ON saf.ManagerFunctionID = mf.ManagerFunctionID
WHERE saf.SponsorAdminID = {0}
ORDER BY mf.ManagerFunctionID",
				sponsorAdminID
			);
			var functions = new List<ManagerFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f = new ManagerFunction {
						Function = rs.GetString(0),
						URL = rs.GetString(1),
						Expl = rs.GetString(2)
					};
					functions.Add(f);
				}
			}
			return functions;
		}
	}
	
	public class SqlDepartmentRepository : BaseSqlRepository<Department>, IDepartmentRepository
	{
		public override void SaveOrUpdate(Department d)
		{
			string query = string.Format(
				@"
INSERT INTO Department (SponsorID, Department, ParentDepartmentID)
VALUES ({0}, '{1}', {2})",
				d.Sponsor.Id,
				d.Name,
				d.Parent.Id
			);
			Db2.exec(query);
		}
		
		public void SaveSponsorAdminDepartment(SponsorAdminDepartment d)
		{
			string query = string.Format(
				@"
INSERT INTO SponsorAdminDepartment (SponsorAdminID, DepartmentID)
VALUES ({0}, {1})",
				d.Id, d.Department.Id
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateDepartment2(Department d)
		{
			string query = string.Format(
				@"
UPDATE Department SET Department = '{0}',
DepartmentShort = '{1}',
ParentDepartmentID = {2}
WHERE DepartmentID = {3}",
				d.Name,
				d.ShortName,
				d.Parent.Id,
				d.Id
			);
			Db2.exec(query);
		}
		
		public void UpdateDepartment(Department d)
		{
			string query = string.Format(
				@"
UPDATE Department SET DepartmentShort = '{0}',
SortOrder = {1}
WHERE DepartmentID = {2}",
				d.ShortName,
				d.SortOrder,
				d.Id
			);
			Db2.exec(query);
		}
		
		public void UpdateDepartmentBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Department SET SortString = dbo.cf_departmentSortString(DepartmentID)
WHERE SponsorID = {0}",
				sponsorID
			);
			Db2.exec(query);
		}
		
		public void DeleteSponsorAdminDepartment(int sponsorAdminID, int departmentID)
		{
			string query = string.Format(
				@"
DELETE FROM SponsorAdminDepartment
WHERE DepartmentID = {1} AND SponsorAdminID = {0}",
				sponsorAdminID,
				departmentID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public Department ReadBySponsor(int sponsorId)
		{
			string query = string.Format(
				@"
SELECT DepartmentID
FROM Department
WHERE SponsorID = {0} ORDER BY DepartmentID DESC",
				sponsorId
			);
			using (SqlDataReader rs = Db2.rs(query)) {
				if (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0)
					};
					return d;
				}
			}
			return null;
		}
		
		public override Department Read(int id)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
WHERE d.DepartmentID = {0}",
				id
			);
			using (SqlDataReader rs = Db2.rs(query)) {
				if (rs.Read()) {
					var d = new Department {
						TreeName = rs.GetString(0)
					};
					return d;
				}
			}
			return null;
		}
		
		public Department ReadByIdAndSponsor(int departmentID, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.SortString,
	d.ParentDepartmentID,
	d.Department,
	d.DepartmentShort
FROM Department d
WHERE d.SponsorID = " + sponsorID + " AND d.DepartmentID = " + departmentID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var d = new Department {
						SortString = rs.GetString(0),
						Parent = new Department { Id = rs.GetInt32(1) },
						Name = rs.GetString(2),
						ShortName = rs.GetString(3)
					};
					return d;
				}
			}
			return null;
		}
		
		public IList<SponsorAdminDepartment> b(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format(
					@"ISNULL(sad.DepartmentID,sa.SuperUser)
FROM Department d
INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = {0}
LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
AND sad.SponsorAdminID = {0} ",
					sponsorAdminID)
				: @"1
FROM Department d ";
			
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	{1}
WHERE d.SponsorID = {0}",
				sponsorID,
				j
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new SponsorAdminDepartment {
						Admin = new SponsorAdmin { SuperUser = rs.GetBoolean(1) },
						Department = new Department {
							Id = rs.GetInt32(0)
						}
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<SponsorAdminDepartment> a(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format(
					@"ISNULL(sad.DepartmentID, sa.SuperUser),
	d.DepartmentShort
FROM Department d
INNER JOIN SponsorAdmin sa ON sa.SponsorAdminID = {0}
LEFT OUTER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
AND sad.SponsorAdminID = {0} ",
					sponsorAdminID
				)
				: @"1,
	d.DepartmentShort
FROM Department d ";
			
			string query = string.Format(
				@"
SELECT d.Department,
	dbo.cf_departmentDepth(d.DepartmentID),
	d.DepartmentID,
	(
		SELECT COUNT(*) FROM Department x
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL)
		AND d.SponsorID = x.SponsorID
		AND d.SortString < x.SortString
	),
	{1}
WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				j
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new SponsorAdminDepartment {
						Admin = new SponsorAdmin { SuperUser = rs.GetBoolean(4) },
						Department = new Department {
							Name = rs.GetString(0),
							Depth = rs.GetInt32(1),
							Id = rs.GetInt32(2),
							Siblings = rs.GetInt32(3),
							ShortName = rs.GetString(5)
						}
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentID
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {1} AND d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				sponsorAdminID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department { Id = rs.GetInt32(0) };
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminOnTree(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{1}d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				j
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0),
						TreeName = rs.GetString(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT DepartmentShort,
	DepartmentID
FROM Department
WHERE DepartmentShort IS NOT NULL
AND SponsorID = {0}",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						ShortName = rs.GetString(0),
						Id = rs.GetInt32(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorOrderedBySortString(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	DepartmentShort
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
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
			string query = string.Format(
				@"
SELECT d.Department,
	LEN(d.SortString),
	d.SortString
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY LEN(d.SortString)",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						SortString = rs.GetString(2)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminAndTree(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{1}d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				j
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Id = rs.GetInt32(0),
						TreeName = rs.GetString(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminSortStringAndTree(int sponsorID, string sortString, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT d.DepartmentID,
	dbo.cf_departmentTree(d.DepartmentID,' » ')
FROM Department d
{3}d.SponsorID = {0}
AND LEFT(d.SortString,{2}) <> '{1}'
ORDER BY d.SortString",
				sponsorID,
				sortString,
				sortString.Length,
				j
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						TreeName = rs.GetString(1)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorWithSponsorAdminInDepth(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT d.Department,
	d.DepartmentID,
	d.DepartmentShort,
	dbo.cf_departmentDepth(d.DepartmentID),
	(
		SELECT COUNT(*) FROM Department x
		INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = {1}
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID
			OR x.ParentDepartmentID IS NULL
			AND d.ParentDepartmentID IS NULL)
			AND d.SponsorID = x.SponsorID
			AND d.SortString < x.SortString
	)
FROM Department d
INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {1} AND d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID,
				sponsorAdminID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						Id = rs.GetInt32(1),
						ShortName = rs.GetString(2),
						Depth = rs.GetInt32(3),
						Siblings = rs.GetInt32(4)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<Department> FindBySponsorInDepth(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT d.DepartmentAnonymized,
	d.DepartmentID,
	'',
	dbo.cf_departmentDepth(d.DepartmentID),
	(
		SELECT COUNT(*) FROM Department x
		WHERE (x.ParentDepartmentID = d.ParentDepartmentID
			OR x.ParentDepartmentID IS NULL
			AND d.ParentDepartmentID IS NULL)
			AND d.SponsorID = x.SponsorID
			AND d.SortString < x.SortString
	)
FROM Department d
WHERE d.SponsorID = {0}
ORDER BY d.SortString",
				sponsorID
			);
			var departments = new List<Department>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d = new Department {
						Name = rs.GetString(0),
						Id = rs.GetInt32(1),
						ShortName = rs.GetString(2),
						Depth = rs.GetInt32(3)
					};
					departments.Add(d);
				}
			}
			return departments;
		}
	}
	
	public class SqlLanguageRepository : BaseSqlRepository<Language>, ILanguageRepository
	{
		public IList<SponsorProjectRoundUnitLanguage> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT sprul.LangID,
	spru.ProjectRoundUnitID,
	l.LID,
	l.Language
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
INNER JOIN LID l ON ISNULL(sprul.LangID, 1) = l.LID
WHERE spru.SponsorID = {0}
ORDER BY spru.SortOrder, spru.SponsorProjectRoundUnitID, l.LID",
				sponsorID);
			var languages = new List<SponsorProjectRoundUnitLanguage>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var l = new SponsorProjectRoundUnitLanguage {
						Language = new Language { Id = rs.GetInt32(2), Name = rs.GetString(3) },
						SponsorProjectRoundUnit = new SponsorProjectRoundUnit {
							ProjectRoundUnit = new ProjectRoundUnit { Id = rs.GetInt32(1) }
						}
					};
					languages.Add(l);
				}
			}
			return languages;
		}
	}
	
	public class SqlUserRepository : BaseSqlRepository<User>, IUserRepository
	{
		public void SaveUserProfileBackgroundQuestion(UserProfileBackgroundQuestion s)
		{
			string query = string.Format(
				@"
INSERT INTO UserProfileBQ (UserProfileID, BQID, ValueInt, ValueText, ValueDate)
VALUES (@UserProfileID, @BQID, @ValueInt, @ValueText, @ValueDate)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@UserProfileID", s.Profile.Id),
				new SqlParameter("@BQID", s.Question.Id),
				new SqlParameter("@ValueInt", s.ValueInt),
				new SqlParameter("@ValueText", s.ValueText),
				new SqlParameter("@ValueDate", s.ValueDate)
			);
		}
		
		public void UpdateUser(int userID, int sponsorID, int departmentID)
		{
			string query = "UPDATE [User] SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID;
//			Db2.exec(query, "healthWatchSqlConnection");
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateUserProfile(int userID, int sponsorID, int departmentID)
		{
			string query = "UPDATE UserProfile SET DepartmentID = " + departmentID + ", SponsorID = " + sponsorID + " WHERE UserID = " + userID;
//			Db2.exec(query, "healthWatchSqlConnection");
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateUserProjectRoundUser(int projectRoundUnitID, int userProjectRoundUserID)
		{
			string query = string.Format(
				@"
UPDATE UserProjectRoundUser SET ProjectRoundUnitID = {0}
WHERE UserProjectRoundUserID = {1}",
				projectRoundUnitID,
				userProjectRoundUserID
			);
//			Db2.exec(query, "healthWatchSqlConnection");
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateEmailFailure(int userID)
		{
			string query = "UPDATE [User] SET EmailFailure = GETDATE() WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateLastReminderSent(int userID)
		{
			string query = "UPDATE [User] SET ReminderLastSent = GETDATE() WHERE UserID = " + userID;
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public User ReadByIdAndSponsorExtendedSurvey(int userID, int sponsorExtendedSurveyID)
		{
			string query = string.Format(
				@"
SELECT ses.ExtraEmailBody,
	ses.ExtraEmailSubject,
	u.Email,
	u.UserID,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
WHERE u.UserID = {0}",
				userID,
				sponsorExtendedSurveyID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var s = new Sponsor();
					s.ExtendedSurveys = new List<SponsorExtendedSurvey>(
						new SponsorExtendedSurvey[] {
							new SponsorExtendedSurvey { ExtraEmailBody = rs.GetString(0), ExtraEmailSubject = rs.GetString(1) }
						}
					);
					var u = new User {
						Sponsor = s,
						Email = rs.GetString(2),
						Id = rs.GetInt32(3),
						ReminderLink = rs.GetInt32(4),
						UserKey = rs.GetString(5)
					};
					return u;
				}
			}
			return null;
		}
		
		public User ReadById(int userID)
		{
			string query = string.Format(
				@"
SELECT UserID,
	SponsorID
FROM [User]
WHERE UserID = {0}",
				userID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Sponsor = new Sponsor { Id = rs.GetInt32(1) }
					};
					return u;
				}
			}
			return null;
		}
		
		public IList<UserProjectRoundUser> FindUserProjectRoundUser(int sponsorID, int surveyID, int userID)
		{
			string query = string.Format(
				@"
SELECT upru.UserProjectRoundUserID,
	upru.ProjectRoundUserID
FROM UserProjectRoundUser upru
INNER JOIN [user] hu ON upru.UserID = hu.UserID
INNER JOIN [eform]..[ProjectRoundUser] pru ON upru.ProjectRoundUserID = pru.ProjectRoundUserID
INNER JOIN [eform]..[ProjectRoundUnit] u ON pru.ProjectRoundUnitID = u.ProjectRoundUnitID
WHERE hu.SponsorID = {0}
AND u.SurveyID = {1}
AND upru.UserID = {2}",
				sponsorID,
				surveyID,
				userID
			);
			var users = new List<UserProjectRoundUser>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new UserProjectRoundUser {
						Id = rs.GetInt32(0),
						ProjectRoundUser = new ProjectRoundUser { Id = rs.GetInt32(1) }
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> Find2(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? "INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND "
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
{1}si.SponsorID = {0}
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND "
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
LEFT OUTER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2}u.SponsorID = {0}
AND (pr.Started <= GETDATE() OR ISNULL(d.PreviewExtendedSurveys,si.PreviewExtendedSurveys) IS NOT NULL)
AND x.AnswerID IS NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorAdminID,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						ReminderLink = rs.GetInt32(2),
						UserKey = rs.GetString(3)
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorWithExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? "INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = " + sponsorAdminID + " AND "
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
INNER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2}u.SponsorID = {0}
AND x.FinishedEmail IS NULL
AND x.AnswerID IS NOT NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorExtendedSurveyID,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						ReminderLink = rs.GetInt32(2),
						UserKey = rs.GetString(3)
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public IList<User> FindBySponsorWithLoginDays(int sponsorID, int sponsorAdminID, int loginDays)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT u.UserID,
	u.Email,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12)
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
{2}u.SponsorID =  {0}
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND u.Email NOT LIKE '%DELETED'
AND dbo.cf_daysFromLastLogin(u.UserID) >= {1}
AND (u.ReminderLastSent IS NULL OR DATEADD(hh,1,u.ReminderLastSent) < GETDATE())",
				sponsorID,
				loginDays,
				j
			);
			var users = new List<User>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var u = new User {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						ReminderLink = rs.GetInt32(2),
						UserKey = rs.GetString(3)
					};
					users.Add(u);
				}
			}
			return users;
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
INNER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2}u.SponsorID = {0}
AND x.FinishedEmail IS NULL
AND x.AnswerID IS NOT NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorExtendedSurveyID,
				j
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountBySponsorWithAdminAndExtendedSurvey2(int sponsorID, int sponsorAdminID, int sponsorExtendedSurveyID)
		{
			string j = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON u.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM [User] u
INNER JOIN Department d ON u.DepartmentID = d.DepartmentID
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorExtendedSurveyID = {1}
INNER JOIN SponsorInvite si ON u.UserID = si.UserID AND si.SponsorID = ses.SponsorID
INNER JOIN eform..ProjectRound pr ON pr.ProjectRoundID = ses.ProjectRoundID
LEFT OUTER JOIN UserSponsorExtendedSurvey x ON u.UserID = x.UserID AND x.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID AND x.AnswerID IS NOT NULL
LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON si.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
{2} u.SponsorID = {0}
AND (pr.Started <= GETDATE() OR ISNULL(d.PreviewExtendedSurveys,si.PreviewExtendedSurveys) IS NOT NULL)
AND x.AnswerID IS NULL
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND sesd.Hide IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				sponsorExtendedSurveyID,
				j
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int a(int sponsorID, int sponsorAdminID)
		{
			string q = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM [User] u
INNER JOIN SponsorInvite si ON u.UserID = si.UserID
{1} si.SponsorID = {0}
AND u.Email IS NOT NULL
AND u.Email <> ''
AND si.StoppedReason IS NULL
AND u.Email NOT LIKE '%DELETED'",
				sponsorID,
				q
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
	}
	
	public class SqlSponsorRepository : BaseSqlRepository<Sponsor>, ISponsorRepository
	{
		public void UpdateSponsorLastLoginSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET LoginLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateLastAllMessageSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET AllMessageLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateSponsorLastInviteReminderSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET InviteReminderLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateSponsorLastInviteSent(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET InviteLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void Z(int sponsorInviteID, string previewExtendedSurveys)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET PreviewExtendedSurveys = {0}
WHERE SponsorInviteID = {1}",
				previewExtendedSurveys,
				sponsorInviteID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsorInviteSent(int userID, int sponsorInviteID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET UserID = {0}, Sent = GETDATE()
WHERE SponsorInviteID = {1}",
				userID,
				sponsorInviteID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateNullUserForUserInvite(int userID)
		{
			string query = string.Format(
				@"
UPDATE SponsorInvite SET UserID = NULL
WHERE UserID = {0}",
				userID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastEmailSent(int sponsorExtendedSurveyID)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET EmailLastSent = GETDATE()
WHERE SponsorExtendedSurveyID = {0}",
				sponsorExtendedSurveyID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateExtendedSurveyLastFinishedSent(int sponsorExtendedSurveyID)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET FinishedLastSent = GETDATE()
WHERE SponsorExtendedSurveyID = {0}",
				sponsorExtendedSurveyID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void UpdateSponsor(int sponsorID)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET AllMessageLastSent = GETDATE()
WHERE SponsorID = {0}",
				sponsorID
			);
			Db.exec(query, "healthWatchSqlConnection"); // TODO: move to department???
		}
		
		public void UpdateSponsorExtendedSurvey(SponsorExtendedSurvey s)
		{
			string query = string.Format(
				@"
UPDATE SponsorExtendedSurvey SET
EmailSubject = @EmailSubject,
EmailBody = @EmailBody,
FinishedEmailSubject = @FinishedEmailSubject,
FinishedEmailBody = @FinishedEmailBody
WHERE SponsorExtendedSurveyID = @SponsorExtendedSurveyID"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@EmailSubject", s.EmailSubject),
				new SqlParameter("@EmailBody", s.EmailBody),
				new SqlParameter("@FinishedEmailSubject", s.FinishedEmailSubject),
				new SqlParameter("@FinishedEmailBody", s.FinishedEmailBody),
				new SqlParameter("@SponsorExtendedSurveyID", s.Id)
			);
		}
		
		public void UpdateSponsor(Sponsor s)
		{
			string query = string.Format(
				@"
UPDATE Sponsor SET
	InviteTxt = @InviteTxt,
	InviteReminderTxt = @InviteReminderTxt,
	AllMessageSubject = @AllMessageSubject,
	LoginTxt = @LoginTxt,
	InviteSubject = @InviteSubject,
	InviteReminderSubject = @InviteReminderSubject,
	AllMessageBody = @AllMessageBody,
	LoginSubject = @LoginSubject,
	LoginDays = @LoginDays,
	LoginWeekday = @LoginWeekday
WHERE SponsorID = @SponsorID"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@InviteTxt", s.InviteText),
				new SqlParameter("@InviteReminderTxt", s.InviteReminderText),
				new SqlParameter("@AllMessageSubject", s.AllMessageSubject),
				new SqlParameter("@LoginTxt", s.LoginText),
				new SqlParameter("@InviteSubject", s.InviteSubject),
				new SqlParameter("@InviteReminderSubject", s.InviteReminderSubject),
				new SqlParameter("@AllMessageBody", s.AllMessageBody),
				new SqlParameter("@LoginSubject", s.LoginSubject),
				new SqlParameter("@LoginDays", s.LoginDays),
				new SqlParameter("@LoginWeekday", s.LoginWeekday),
				new SqlParameter("@SponsorID", s.Id)
			);
		}
		
		public void UpdateSponsorAdmin(SponsorAdmin a)
		{
			string p = (a.Password != "Not shown" && a.Password != "")
				? string.Format(", Pas = '{0}'", a.Password.Replace("'", "''"))
				: "";
			string query = string.Format(
				@"
UPDATE SponsorAdmin SET ReadOnly = {0},
	Email = '{1}',
	Name = '{2}',
	Usr = '{3}'
	{4}
	SuperUser = {5}
WHERE SponsorAdminID = {6}
AND SponsorID = {7}",
				a.ReadOnly,
				a.Email.Replace("'", "''"),
				a.Name.Replace("'", "''"),
				a.Usr.Replace("'", ""),
				p,
				a.SuperUser,
				a.Id,
				a.Sponsor.Id
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public void SaveSponsorAdmin(SponsorAdmin a)
		{
//			string query = string.Format(
//				@"
			//INSERT INTO SponsorAdmin (Email, Name, Usr, Pas, SponsorID, SuperUser, ReadOnly)
			//VALUES ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6})",
//				a.Email.Replace("'", "''"),
//				a.Name.Replace("'", "''"),
//				a.Usr.Replace("'", "''"),
//				a.Password.Replace("'", "''"),
//				a.Sponsor.Id,
//				a.SuperUser,
//				a.ReadOnly
//			);
//			Db.exec(query, "healthWatchSqlConnection");
			string query = string.Format(
				@"
INSERT INTO SponsorAdmin (Email, Name, Usr, Pas, SponsorID, SuperUser, ReadOnly)
VALUES (@Email, @Name, @Usr, @Pas, @SponsorID, @SuperUser, @ReadOnly)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@Email", a.Email),
				new SqlParameter("@Name", a.Name),
				new SqlParameter("@Usr", a.Usr),
				new SqlParameter("@Pas", a.Password),
				new SqlParameter("@SponsorID", a.Sponsor.Id),
				new SqlParameter("@SuperUser", a.SuperUser),
				new SqlParameter("@ReadOnly", a.ReadOnly)
			);
		}
		
		public void SaveSponsorAdminFunction(SponsorAdminFunction f)
		{
//			string query = string.Format(
//				@"
			//INSERT INTO SponsorAdminFunction (SponsorAdminID, ManagerFunctionID)
			//VALUES ({0}, {1})",
//				f.Admin.Id,
//				f.Function.Id
//			);
//			Db.exec(query, "healthWatchSqlConnection");
			string query = string.Format(
				@"
INSERT INTO SponsorAdminFunction (SponsorAdminID, ManagerFunctionID)
VALUES (@SponsorAdminID, @ManagerFunctionID)"
			);
			ExecuteNonQuery(
				query,
				"healthWatchSqlConnection",
				new SqlParameter("@SponsorAdminID", f.Admin.Id),
				new SqlParameter("@ManagerFunctionID", f.Function.Id)
			);
		}
		
		public void DeleteSponsorAdmin(int sponsorAdminID)
		{
			Db.exec("DELETE FROM SponsorAdminFunction WHERE SponsorAdminID = " + sponsorAdminID, "healthWatchSqlConnection");
		}
		
		public void UpdateDeletedAdmin(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
UPDATE SponsorAdmin
	SET SponsorID = -ABS(SponsorID), Usr = Usr + 'DELETED'
WHERE SponsorAdminID = {1}
	AND SponsorID = {0}",
				sponsorID,
				sponsorAdminID
			);
			Db.exec(query, "healthWatchSqlConnection");
		}
		
		public int CountSentInvitesBySponsor(int sponsorID, DateTime dateSent)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite AS si
LEFT OUTER JOIN [User] AS u ON si.UserID = u.UserID
WHERE (si.SponsorID = {0})
	AND (ISNULL(u.Created, si.Sent) < '{1}')
	OR (si.SponsorID = {0}) AND (si.Sent < '{1}')",
				sponsorID,
				dateSent.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountCreatedInvitesBySponsor(int sponsorID, DateTime dateCreated)
		{
			string query = string.Format(
				@"
SELECT COUNT(*)
FROM SponsorInvite si
INNER JOIN [User] u ON si.UserID = u.UserID
WHERE si.SponsorID = {0} AND u.Created < '{1}'",
				sponsorID,
				dateCreated.ToString("yyyy-MM-dd")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, int SAID)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID,
	Name,
	Usr,
	Email,
	SuperUser,
	ReadOnly
FROM SponsorAdmin
WHERE (SponsorAdminID <> {1} OR SuperUser = 1)
AND SponsorAdminID = {2}
AND SponsorID = {0}",
				sponsorID,
				sponsorAdminID,
				SAID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0),
						Name = rs.GetString(1),
						Usr = rs.GetString(2),
						Email = rs.GetString(3),
						SuperUser = rs.GetBoolean(4),
						ReadOnly = rs.GetBoolean(5)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT SuperUser FROM SponsorAdmin WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						SuperUser = GetBoolean(rs, 0)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorID, int sponsorAdminID, string password)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID
FROM SponsorAdmin
WHERE SponsorID = {0}
AND SponsorAdminID = {1}
AND Pas = '{2}'",
				sponsorID,
				sponsorAdminID,
				password.Replace("'", "''")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInviteByUser(int userID)
		{
			string query = string.Format(
				@"
SELECT Email,
	DepartmentID,
	StoppedReason,
	Stopped
FROM SponsorInvite
WHERE SponsorInviteID = " + userID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = GetString(rs, 0), //rs.GetString(0),
						Department = new Department { Id = GetInt32(rs, 1) }, //rs.GetInt32(1) },
						StoppedReason = GetInt32(rs, 2), //rs.GetInt32(2),
						Stopped = GetDateTime(rs, 3) //rs.GetDateTime(3)
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInviteBySponsor(int inviteID, int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.InviteTxt,
	s.InviteSubject,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8),
	si.UserID,
	u.ReminderLink,
	LEFT(REPLACE(CONVERT(VARCHAR(255),u.UserKey),'-',''),12),
	s.LoginTxt,
	s.LoginSubject
FROM Sponsor s
INNER JOIN SponsorInvite si ON s.SponsorID = si.SponsorID
LEFT OUTER JOIN [User] u ON u.UserID = si.UserID
WHERE s.SponsorID = " + sponsorID + " AND si.SponsorInviteID = " + inviteID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = rs.GetString(2),
						InvitationKey = rs.GetString(3),
						User = new User {
							Id = rs.GetInt32(4),
							ReminderLink = rs.GetInt32(5),
							UserKey = rs.GetString(6)
						},
						Sponsor = new Sponsor {
							InviteText = rs.GetString(0),
							InviteSubject = rs.GetString(1),
							LoginText = rs.GetString(7),
							LoginSubject = rs.GetString(8)
						}
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInvite ReadSponsorInvite(int sponsorInviteID)
		{
			string query = string.Format(
				@"
SELECT Email
FROM SponsorInvite
WHERE SponsorInviteID = {0}",
				sponsorInviteID
			);
			using (SqlDataReader rs = Db2.rs(query)) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						Email = rs.GetString(0)
					};
					return i;
				}
			}
			return null;
		}
		
		public SponsorInviteBackgroundQuestion ReadSponsorInviteBackgroundQuestion(int sponsorID, int userID, int bqID)
		{
			string query = string.Format(
				@"
SELECT sib.BAID,
	sib.ValueInt,
	sib.ValueText,
	sib.ValueDate,
	bq.Type,
	up.UserProfileID
FROM SponsorInvite si
INNER JOIN SponsorInviteBQ sib ON si.SponsorInviteID = sib.SponsorInviteID AND sib.BQID = {2}
INNER JOIN bq ON sib.BQID = bq.BQID
INNER JOIN [User] u ON si.UserID = u.UserID
INNER JOIN UserProfile up ON u.UserProfileID = up.UserProfileID
LEFT OUTER JOIN UserProfileBQ upbq ON up.UserProfileID = upbq.UserProfileID AND upbq.BQID = bq.BQID
WHERE upbq.UserBQID IS NULL
AND si.UserID = {1}
AND si.SponsorID = {0}",
				sponsorID,
				userID,
				bqID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var i = new SponsorInvite {
						User = new User {
							Profile = new UserProfile { Id = GetInt32(rs, 5) }
						}
					};
					var sib = new SponsorInviteBackgroundQuestion {
						Answer = new BackgroundAnswer { Id = rs.GetInt32(0) },
						ValueInt = GetInt32(rs, 1),
						ValueText = GetString(rs, 2),
						ValueDate = GetDateTime(rs, 3),
						Question = new BackgroundQuestion { Type = GetInt32(rs, 4) },
						Invite = i
					};
					return sib;
				}
			}
			return null;
		}
		
		public Sponsor X(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.Sponsor,
	ss.SuperSponsorID,
	ssl.Header
FROM Sponsor s
LEFT OUTER JOIN SuperSponsor ss ON s.SuperSponsorID = ss.SuperSponsorID
LEFT OUTER JOIN SuperSponsorLang ssl ON ss.SuperSponsorID = ssl.SuperSponsorID AND ssl.LangID = 1
WHERE s.SponsorID = {0}",
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var s = new Sponsor();
					s.Name = GetString(rs, 0);
//					if (!rs.IsDBNull(1)) {
					var u = new SuperSponsor { Id = GetInt32(rs, 1) }; //rs.GetInt32(1) };
					s.SuperSponsor = u;
//					if (!rs.IsDBNull(2)) {
					u.Languages = new List<SuperSponsorLanguage>(
						new SuperSponsorLanguage[] {
							new SuperSponsorLanguage { Header = GetString(rs, 2) }
						}
					);
//					}
//					}
					return s;
				}
			}
			return null;
		}
		
		public Sponsor ReadSponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT s.InviteTxt,
	s.InviteReminderTxt,
	s.LoginTxt,
	s.InviteSubject,
	s.InviteReminderSubject,
	s.LoginSubject,
	s.InviteLastSent,
	s.InviteReminderLastSent,
	s.LoginLastSent,
	s.LoginDays,
	s.LoginWeekday,
	s.AllMessageSubject,
	s.AllMessageBody,
	s.AllMessageLastSent
FROM Sponsor s
WHERE s.SponsorID = {0}",
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var s = new Sponsor {
						InviteText = GetString(rs, 0),
						InviteReminderText = GetString(rs, 1),
						LoginText = GetString(rs, 2),
						InviteSubject = GetString(rs, 3),
						InviteReminderSubject = GetString(rs, 4),
						LoginSubject = GetString(rs, 5),
						InviteLastSent = GetDateTime(rs, 6),
						InviteReminderLastSent = GetDateTime(rs, 7),
						LoginLastSent = GetDateTime(rs, 8),
						LoginDays = GetInt32(rs, 9),
						LoginWeekday = GetInt32(rs, 10),
						AllMessageSubject = GetString(rs, 11),
						AllMessageBody = GetString(rs, 12),
						AllMessageLastSent = GetDateTime(rs, 13),
					};
					return s;
				}
			}
			return null;
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID, string usr)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID FROM SponsorAdmin WHERE SponsorID = {0} AND Usr = '{1}'",
				sponsorAdminID,
				usr.Replace("'", "")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0)
					};
					return a;
				}
			}
			return null;
		}
		
//		public SponsorAdmin ReadSponsorAdmin2(int sponsorAdminID, string usr)
//		{
//			string query = string.Format(
//				@"
		//SELECT SponsorAdminID FROM SponsorAdmin WHERE Usr = '{0}' {1}",
//				usr.Replace("'", ""),
//				(sponsorAdminID != 0 ? " AND SponsorAdminID != " + sponsorAdminID : "")
//			);
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				if (rs.Read()) {
//					var a = new SponsorAdmin {
//						Id = rs.GetInt32(0)
//					};
//					return a;
//				}
//			}
//			return null;
//		}
//
		public bool SponsorAdminExists(int sponsorAdminID, string usr)
		{
			string query = string.Format(
				@"
SELECT SponsorAdminID FROM SponsorAdmin WHERE Usr = '{0}' {1}",
				usr.Replace("'", ""),
				(sponsorAdminID != 0 ? " AND SponsorAdminID != " + sponsorAdminID : "")
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					return true;
				}
			}
			return false;
		}
		
		public SponsorAdmin ReadSponsorAdmin(string SKEY, string SAKEY, string SA, string SAID, string ANV, string LOS)
		{
			string s1 = (SKEY == null ? "sa.SponsorAdminID, " : "-1, ");
			string s3 = (SKEY == null ? "sa.Anonymized, " : "NULL, ");
			string s4 = (SKEY == null ? "sa.SeeUsers, " : (SA != null ? "sas.SeeUsers, " : "1, "));
			string s6 = (SKEY == null ? "sa.ReadOnly, " : "NULL, ");
			string s7 = (SKEY == null ? "ISNULL(sa.Name,sa.Usr) " : "'Internal administrator' ");
			string j = (ANV != null && LOS != null || SAKEY != null ?
			            "INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID " +
			            (SAKEY != null ?
			             "WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '" + SAKEY.Substring(0, 8).Replace("'", "") + "' " +
			             "AND s.SponsorID = " + SAKEY.Substring(8).Replace("'", "")
			             :
			             "WHERE sa.Usr = '" + ANV.Replace("'", "") + "' " +
			             "AND sa.Pas = '" + LOS.Replace("'", "") + "'")
			            :
			            (SA != null ?
			             "INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = " + Convert.ToInt32(SAID) + " "
			             :
			             ""
			            ) +
			            "WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '" + SKEY.Substring(0, 8).Replace("'", "") + "' " +
			            "AND s.SponsorID = " + SKEY.Substring(8).Replace("'", "")
			           );
//			string j = ANV != null && LOS != null || SAKEY != null
//				? string.Format(
//					@"INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID {0}",
//					SAKEY != null
//					? string.Format(
//						@"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '{0}' AND s.SponsorID = {1}",
//						SAKEY.Substring(0, 8).Replace("'", ""),
//						SAKEY.Substring(8).Replace("'", "")
//					)
//					: string.Format(
//						@"WHERE sa.Usr = '{0}' AND sa.Pas = '{1}'",
//						ANV.Replace("'", ""),
//						LOS.Replace("'", "")
//					)
//				)
//				: string.Format(
//					@"{0}WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '{1}' AND s.SponsorID = {2}",
//					SA != null
//					? string.Format(
//						@"INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = {0} ",
//						Convert.ToInt32(SAID)
//					)
//					: "",
//					SKEY.Substring(0, 8).Replace("'", ""),
//					SKEY.Substring(8).Replace("'", "")
//				);
//			string j = "";
//			if (ANV != null && LOS != null || SAKEY != null) {
//				j += "INNER JOIN SponsorAdmin sa ON sa.SponsorID = s.SponsorID ";
//				if (SAKEY != null) {
//					j += string.Format(
//						@"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),sa.SponsorAdminKey),'-',''),8) = '{0}' AND s.SponsorID = {1}",
//						SAKEY.Substring(0, 8).Replace("'", ""),
//						SAKEY.Substring(8).Replace("'", "")
//					);
//				} else {
//					string.Format(
//						@"WHERE sa.Usr = '{0}' AND sa.Pas = '{1}'",
//						ANV.Replace("'", ""),
//						LOS.Replace("'", "")
//					);
//				}
//			} else {
//				if (SA != null) {
//					j += string.Format(
//						@"INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID AND sas.SuperAdminID = {0} ",
//						Convert.ToInt32(SAID)
//					);
//				}
//				j += string.Format(
//					@"WHERE LEFT(REPLACE(CONVERT(VARCHAR(255),s.SponsorKey),'-',''),8) = '{1}' AND s.SponsorID = {2}",
//					SKEY.Substring(0, 8).Replace("'", ""),
//					SKEY.Substring(8).Replace("'", "")
//				);
//			}
			string u = "";
			if (SKEY == null && SAKEY == null) {
				u = string.Format(
					@"
UNION ALL
SELECT NULL,
	NULL,
	NULL,
	NULL,
	NULL,
	sa.SuperAdminID,
	NULL,
	sa.Username
FROM SuperAdmin sa
WHERE sa.Username = '{0}'
AND sa.Password = '{1}'",
					ANV.Replace("'", ""),
					LOS.Replace("'", "")
				);
			}
			string query = string.Format(
				@"
SELECT s.SponsorID,
	{0}
	s.Sponsor,
	{1}
	{2}
	NULL,
	{3}
	{4}
FROM Sponsor s
{5}
{6}",
				s1,
				s3,
				s4,
				s6,
				s7,
				j,
				u
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var a = new SponsorAdmin {
						Id = GetInt32(rs, 1),
						Sponsor = new Sponsor { Id = GetInt32(rs, 0), Name = GetString(rs, 2) },
						Anonymized = GetInt32(rs, 3) == 1, //GetBoolean(rs, 3),
						SeeUsers = GetInt32(rs, 4) == 1, //GetBoolean(rs, 4),
						SuperAdmin = GetInt32(rs, 5) == 1, // FIXME: Is this really boolean?
						ReadOnly = GetBoolean(rs, 6),
						Name = GetString(rs, 7)
					};
					return a;
				}
			}
			return null;
		}
		
		public SponsorProjectRoundUnit ReadSponsorProjectRoundUnit(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT spru.ProjectRoundUnitID,
	spru.SurveyID
FROM SponsorProjectRoundUnit spru
WHERE spru.SponsorID = {0}",
				sponsorID
			);
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				if (rs.Read()) {
					var u = new SponsorProjectRoundUnit {
						Id = rs.GetInt32(0),
						Survey = new Survey { Id = rs.GetInt32(1) }
					};
					return u;
				}
			}
			return null;
		}
		
		public IList<SponsorInviteBackgroundQuestion> FindInviteBackgroundQuestionsByUser(int userID)
		{
			string query = string.Format(
				@"
SELECT s.BQID,
	s.BAID,
	BQ.Type,
	s.ValueInt,
	s.ValueDate,
	s.ValueText,
	BQ.Restricted
FROM SponsorInviteBQ s
INNER JOIN BQ ON BQ.BQID = s.BQID
WHERE s.SponsorInviteID = " + userID
			);
			var invites = new List<SponsorInviteBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInviteBackgroundQuestion {
						Question = new BackgroundQuestion { Id = rs.GetInt32(0), Type = rs.GetInt32(2), Restricted = rs.GetInt32(6) },
						Answer = new BackgroundAnswer { Id = rs.GetInt32(1) },
						ValueInt = rs.GetInt32(3),
						ValueDate = GetDateTime(rs, 4),
						ValueText = GetString(rs, 5)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorInvite> FindInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format(@"INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT si.SponsorInviteID,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8)
FROM SponsorInvite si
{1}si.SponsorID = {0}
AND si.UserID IS NULL
AND si.StoppedReason IS NULL
AND si.Sent IS NULL",
				sponsorID,
				j
			);
			var invites = new List<SponsorInvite>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInvite {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						InvitationKey = rs.GetString(2)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsorAdmin(int sponsorID, int sponsorAdminID)
		{
			string w = sponsorAdminID != -1
				? string.Format(
					@"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID
WHERE sad.SponsorAdminID = {0}
AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT ses.ProjectRoundID,
	ses.EmailSubject,
	ses.EmailBody,
	ses.EmailLastSent,
	ses.Internal,
	ses.SponsorExtendedSurveyID,
	ses.FinishedEmailSubject,
	ses.FinishedEmailBody,
	ses.RoundText
FROM SponsorExtendedSurvey ses
INNER JOIN Sponsor s ON ses.SponsorID = s.SponsorID
INNER JOIN Department d ON s.SponsorID = d.SponsorID
LEFT OUTER JOIN SponsorExtendedSurveyDepartment dd ON dd.SponsorExtendedSurveyID = ses.SponsorExtendedSurveyID
	AND dd.DepartmentID = d.DepartmentID
{1} ses.SponsorID = {0}
AND dd.Hide IS NULL
ORDER BY ses.SponsorExtendedSurveyID DESC",
				sponsorID,
				w
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						ProjectRoundUnit = new ProjectRoundUnit { Id = rs.GetInt32(0) },
						EmailSubject = rs.GetString(1),
						EmailBody = rs.GetString(2),
						EmailLastSent = rs.GetDateTime(3),
						Internal = rs.GetString(4),
						Id = rs.GetInt32(5),
						FinishedEmailSubject = rs.GetString(6),
						FinishedEmailBody = rs.GetString(7),
						RoundText = rs.GetString(8)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT ses.ProjectRoundID,
	ses.Internal,
	ses.RoundText,
	ses.IndividualFeedbackEmailSubject,
	ses.IndividualFeedbackEmailBody
FROM SponsorExtendedSurvey ses
WHERE ses.SponsorID = {0}
ORDER BY ses.SponsorExtendedSurveyID",
				sponsorID
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorExtendedSurvey {
						ProjectRoundUnit = new ProjectRoundUnit { Id = rs.GetInt32(0) },
						Internal = rs.GetString(1),
						RoundText = rs.GetString(2),
						IndividualFeedbackEmailSubject = rs.GetString(3),
						IndividualFeedbackEmailBody = rs.GetString(4)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorExtendedSurvey> FindExtendedSurveysBySuperAdmin(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.Sponsor,
	ses.ProjectRoundID,
	ses.Internal,
	ses.RoundText,
	ss.SurveyID,
	ss.Internal,
	(SELECT COUNT(*) FROM eform..Answer a WHERE a.ProjectRoundID = r.ProjectRoundID AND a.EndDT IS NOT NULL) AS CX
FROM Sponsor s
INNER JOIN SponsorExtendedSurvey ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform..ProjectRound r ON ses.ProjectRoundID = r.ProjectRoundID
INNER JOIN eform..Survey ss ON r.SurveyID = ss.SurveyID
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
ORDER BY s.Sponsor, ses.Internal, ses.RoundText",
				superAdminID
			);
			var surveys = new List<SponsorExtendedSurvey>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var v = new Survey { Id = rs.GetInt32(4), Name = rs.GetString(5) };
					var u = new ProjectRoundUnit { Id = rs.GetInt32(1), Survey = v };
					u.Answers = new List<Answer>(rs.GetInt32(6));
					var s = new SponsorExtendedSurvey {
						Sponsor = new Sponsor { Name = rs.GetString(0) },
						ProjectRoundUnit = u,
						Internal = rs.GetString(2),
						RoundText = rs.GetString(3)
					};
					surveys.Add(s);
				}
			}
			return surveys;
		}
		
		public IList<SponsorInvite> FindSentInvitesBySponsor(int sponsorID, int sponsorAdminID)
		{
			string j = sponsorAdminID != -1
				? string.Format("INNER JOIN SponsorAdminDepartment sad ON si.DepartmentID = sad.DepartmentID WHERE sad.SponsorAdminID = {0} AND ", sponsorAdminID)
				: "WHERE ";
			string query = string.Format(
				@"
SELECT DISTINCT si.SponsorInviteID,
	si.Email,
	LEFT(REPLACE(CONVERT(VARCHAR(255),si.InvitationKey),'-',''),8)
FROM SponsorInvite si
{1}si.SponsorID = {0}
AND si.UserID IS NULL
AND si.StoppedReason IS NULL
AND si.Sent IS NOT NULL
AND DATEADD(hh,1,si.Sent) < GETDATE()",
				sponsorID,
				j
			);
			var invites = new List<SponsorInvite>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var i = new SponsorInvite {
						Id = rs.GetInt32(0),
						Email = rs.GetString(1),
						InvitationKey = rs.GetString(2)
					};
					invites.Add(i);
				}
			}
			return invites;
		}
		
		public IList<SponsorAdminDepartment> FindAdminDepartmentBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT DepartmentID FROM SponsorAdminDepartment WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			var departments = new List<SponsorAdminDepartment>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var d =  new SponsorAdminDepartment {
						Department = new Department { Id = rs.GetInt32(0) }
					};
					departments.Add(d);
				}
			}
			return departments;
		}
		
		public IList<SponsorAdminFunction> FindAdminFunctionBySponsorAdmin(int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT ManagerFunctionID FROM SponsorAdminFunction WHERE SponsorAdminID = {0}",
				sponsorAdminID
			);
			var functions = new List<SponsorAdminFunction>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var f =  new SponsorAdminFunction {
						Function = new ManagerFunction { Id = rs.GetInt32(0) }
					};
					functions.Add(f);
				}
			}
			return functions;
		}
		
		public IList<SponsorAdmin> FindAdminBySponsor(int sponsorID, int sponsorAdminID)
		{
			string query = string.Format(
				@"
SELECT sa.SponsorAdminID,
	sa.Usr,
	sa.Name,
	sa.ReadOnly
FROM SponsorAdmin sa
WHERE (sa.SponsorAdminID <> {1} OR sa.SuperUser = 1)
{2}
AND sa.SponsorID = {0}",
				sponsorID,
				sponsorAdminID,
				sponsorAdminID != -1 ? "AND ((SELECT COUNT(*) FROM SponsorAdminDepartment sad WHERE sad.SponsorAdminID = sa.SponsorAdminID) = 0 OR (SELECT COUNT(*) FROM SponsorAdminDepartment sad INNER JOIN SponsorAdminDepartment sad2 ON sad.DepartmentID = sad2.DepartmentID WHERE sad.SponsorAdminID = sa.SponsorAdminID AND sad2.SponsorAdminID = " + sponsorAdminID + ") > 0) " : ""
			);
			var admins = new List<SponsorAdmin>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var a = new SponsorAdmin {
						Id = rs.GetInt32(0),
						Usr = rs.GetString(1),
						Name = rs.GetString(2),
						ReadOnly = GetInt32(rs, 3) == 1
					};
				}
			}
			return admins;
		}
		
		public IList<SponsorBackgroundQuestion> FindBySponsor(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT sbq.BQID,
	BQ.Internal
FROM SponsorBQ sbq
INNER JOIN BQ ON BQ.BQID = sbq.BQID
WHERE (BQ.Comparison = 1 OR sbq.Hidden = 1)
AND BQ.Type IN (1, 7)
AND sbq.SponsorID = {0}",
				sponsorID
			);
			var sponsors = new List<SponsorBackgroundQuestion>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new SponsorBackgroundQuestion();
					s.Id = rs.GetInt32(0);
					s.Question = new BackgroundQuestion { Internal = rs.GetString(1) };
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
		
		public IList<SponsorProjectRoundUnit> FindBySponsorAndLanguage(int sponsorID, int langID)
		{
			string query = string.Format(
				@"
SELECT ISNULL(sprul.Nav, '?'),
	spru.ProjectRoundUnitID
FROM SponsorProjectRoundUnit spru
LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID
WHERE spru.SponsorID = {0} AND ISNULL(sprul.LangID, 1) = {1}",
				sponsorID,
				langID
			);
			var projects = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var p = new SponsorProjectRoundUnit();
					p.Navigation = rs.GetString(0);
					p.ProjectRoundUnit = new ProjectRoundUnit { Id = rs.GetInt32(1) };
					projects.Add(p);
				}
			}
			return projects;
		}
		
		public IList<SponsorProjectRoundUnit> FindDistinctRoundUnitsWithReportBySuperAdmin(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT DISTINCT s.Sponsor,
	ses.ProjectRoundUnitID,
	ses.Nav,
	rep.ReportID,
	rep.Internal,
	(
		SELECT COUNT(DISTINCT a.ProjectRoundUserID)
		FROM eform..Answer a
		WHERE a.ProjectRoundUnitID = r.ProjectRoundUnitID
			AND a.EndDT >= '{1}' AND a.EndDT < '{2}'
	) AS CX
FROM Sponsor s
INNER JOIN SponsorProjectRoundUnit ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform..ProjectRoundUnit r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
INNER JOIN eform..Report rep ON rep.ReportID = r.ReportID
INNER JOIN SuperAdminSponsor sas ON s.SponsorID = sas.SponsorID
WHERE s.Deleted IS NULL AND sas.SuperAdminID = {0}
ORDER BY s.Sponsor, ses.Nav",
				superAdminID,
				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
				DateTime.Now.ToString("yyyy-MM-01")
			);
			var units = new List<SponsorProjectRoundUnit>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var p = new ProjectRoundUnit {
						Id = rs.GetInt32(1),
						Report = new Report { Id = rs.GetInt32(3), Internal = rs.GetString(4) },
						Answers = new List<Answer>(rs.GetInt32(5))
					};
					var u = new SponsorProjectRoundUnit {
						Sponsor = new Sponsor { Name = rs.GetString(0) },
						ProjectRoundUnit = p,
						Navigation = rs.GetString(2)
					};
					units.Add(u);
				}
			}
			return units;
		}
		
		public IList<Sponsor> FindAndCountDetailsBySuperAdmin(int superAdminID)
		{
			string query = string.Format(
				@"
SELECT s.SponsorID,
	s.Sponsor,
	LEFT(REPLACE(CONVERT(VARCHAR(255), s.SponsorKey), '-', ''), 8),
	(SELECT COUNT(*) FROM SponsorExtendedSurvey AS ses WHERE (SponsorID = s.SponsorID)),
	(SELECT COUNT(*) FROM SponsorInvite AS si WHERE (Sent IS NOT NULL) AND (SponsorID = s.SponsorID)),
	(SELECT COUNT(*) FROM SponsorInvite AS si INNER JOIN [User] AS u ON si.UserID = u.UserID WHERE (si.SponsorID = s.SponsorID)),
	(SELECT MIN(Sent) FROM SponsorInvite AS si WHERE (SponsorID = s.SponsorID)),
	sas.SeeUsers,
	(SELECT COUNT(*) FROM SponsorInvite AS si WHERE (SponsorID = s.SponsorID)),
	s.Closed
FROM Sponsor AS s
INNER JOIN SuperAdminSponsor AS sas ON s.SponsorID = sas.SponsorID
WHERE (sas.SuperAdminID = {0}) AND (s.Deleted IS NULL)
ORDER BY s.Sponsor",
				superAdminID
			);
			var sponsors = new List<Sponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new Sponsor {
						Id = rs.GetInt32(0),
						Name = rs.GetString(1),
						SponsorKey = rs.GetString(2),
						ClosedAt = GetDateTime(rs, 9),
						MinimumInviteDate = GetDateTime(rs, 6)
					};
					s.ExtendedSurveys = new List<SponsorExtendedSurvey>(GetInt32(rs, 3));
					s.SentInvites = new List<SponsorInvite>(GetInt32(rs, 4));
					s.ActiveInvites = new List<SponsorInvite>(GetInt32(rs, 5));
					s.SuperAdminSponsors = new List<SuperAdminSponsor>(
						new SuperAdminSponsor[] {
							new SuperAdminSponsor { SeeUsers = GetInt32(rs, 7) == 1 }
						}
					);
					s.Invites = new List<SponsorInvite>(GetInt32(rs, 8));
					sponsors.Add(s);
				}
			}
			return sponsors;
		}
		
		public IList<Sponsor> Y(int sponsorID)
		{
			string query = string.Format(
				@"
SELECT DISTINCT s.Sponsor,
	ses.ProjectRoundUnitID,
	ses.Nav,
	rep.ReportID,
	rep.Internal,
	(
		SELECT COUNT(DISTINCT a.ProjectRoundUserID)
		FROM eform.dbo.Answer AS a
		WHERE (a.ProjectRoundUnitID = r.ProjectRoundUnitID)
			AND (a.EndDT >= '{1}')
			AND (a.EndDT < '{2}')
	) AS CX
FROM Sponsor AS s
INNER JOIN SponsorProjectRoundUnit AS ses ON ses.SponsorID = s.SponsorID
INNER JOIN eform.dbo.ProjectRoundUnit AS r ON ses.ProjectRoundUnitID = r.ProjectRoundUnitID
INNER JOIN eform.dbo.Report AS rep ON rep.ReportID = r.ReportID
INNER JOIN SuperAdminSponsor AS sas ON s.SponsorID = sas.SponsorID
WHERE (s.Deleted IS NULL) AND (sas.SuperAdminID = {0})
ORDER BY s.Sponsor, ses.Nav",
				sponsorID,
				DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"),
				DateTime.Now.ToString("yyyy-MM-01")
			);
			var sponsors = new List<Sponsor>();
			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
				while (rs.Read()) {
					var s = new Sponsor();
					s.Name = rs.GetString(0);
					s.ProjectRoundUnit = new ProjectRoundUnit {
						Report = new Report { Id = rs.GetInt32(3), Internal = rs.GetString(4) }
					};
				}
			}
			return sponsors;
		}
	}
}
