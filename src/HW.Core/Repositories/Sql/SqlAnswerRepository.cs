using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Models;

namespace HW.Core.Repositories.Sql
{
	public class SqlAnswerRepository : BaseSqlRepository<Answer>
	{
		public int CountByValueWithDateOptionAndQuestion(int val, int yearFrom, int yearTo, int optionID, int questionID, string sortString, int fm, int tm)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM Answer a
INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
WHERE a.EndDT IS NOT NULL
AND av.ValueInt = {0}
--AND YEAR(a.EndDT) >= {1}
--AND YEAR(a.EndDT) <= {2}
AND (YEAR(a.EndDT) = {1} AND MONTH(a.EndDT) >= {7} OR YEAR(a.EndDT) > {1})
AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) <= {8} OR YEAR(a.EndDT) < {2})
AND av.OptionID = {3}
AND av.QuestionID = {4}
AND LEFT(pru.SortString, {5}) = '{6}'",
				val,
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				optionID,
				questionID,
				sortString.Length,
				sortString,
				fm,
				tm
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountByProject(int projectRoundUserID, int yearFrom, int yearTo, int fm, int tm)
		{
			string query = string.Format(
				@"
SELECT COUNT(DISTINCT dbo.cf_yearMonthDay(a.EndDT))
FROM Answer a
WHERE a.EndDT IS NOT NULL
--AND YEAR(a.EndDT) >= {1}
--AND YEAR(a.EndDT) <= {2}
AND (YEAR(a.EndDT) = {1} AND MONTH(a.EndDT) >= {3} OR YEAR(a.EndDT) > {1})
AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) <= {4} OR YEAR(a.EndDT) < {2})
AND a.ProjectRoundUserID = {0}",
				projectRoundUserID,
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				fm,
				tm
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					return rs.GetInt32(0);
				}
			}
			return 0;
		}
		
		public int CountByDate(int yearFrom, int yearTo, string sortString, int fm, int tm)
		{
			string query = string.Format(
				@"
SELECT COUNT(*) FROM Answer a
INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
WHERE a.EndDT IS NOT NULL
--AND YEAR(a.EndDT) >= {0}
--AND YEAR(a.EndDT) <= {1}
AND (YEAR(a.EndDT) = {0} AND MONTH(a.EndDT) >= {4} OR YEAR(a.EndDT) > {0})
AND (YEAR(a.EndDT) = {1} AND MONTH(a.EndDT) <= {5} OR YEAR(a.EndDT) < {1})
AND LEFT(pru.SortString, {2}) = '{3}'",
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString.Length,
				sortString,
				fm,
				tm
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
						Language = new Language { Id = rs.GetInt32(1) },
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
		
		/// <summary>
		/// Used in SuperReportImage thus no MonthFrom and MonthTo!
		/// </summary>
		/// <param name="groupBy"></param>
		/// <param name="yearFrom"></param>
		/// <param name="yearTo"></param>
		/// <param name="rnds"></param>
		/// <returns></returns>
		public Answer ReadByGroup(string groupBy, string yearFrom, string yearTo, string rnds)
		{
			string query = string.Format(
				@"
SELECT {0}(MAX(a.EndDT)) - {0}(MIN(a.EndDT)),
	{0}(MIN(a.EndDT)),
	{0}(MAX(a.EndDT))
FROM Answer a
INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
WHERE a.EndDT IS NOT NULL
AND a.EndDT >= '{1}'
AND a.EndDT < '{2}'
{3}",
				groupBy,
				yearFrom,
				yearTo,
				rnds
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer();
					a.DummyValue1 = GetInt32(rs, 0, 0);
					a.DummyValue2 = GetInt32(rs, 1, 0);
					a.DummyValue3 = GetInt32(rs, 2, 0);
					return a;
				}
			}
			return null;
		}
		
//		public Answer Read3(int answerID, int questionID)
//		{
//			string query = string.Format(
//				@"
//SELECT COUNT(*)
//FROM AnswerValue WHERE AnswerID = {0}
//AND QuestionID = {1}
//AND DeletedSessionID IS NULL
//AND (ValueInt IS NOT NULL OR ValueDecimal IS NOT NULL OR ValueDateTime IS NOT NULL OR ValueText IS NOT NULL)",
//				answerID,
//				questionID
//			);
//			using (SqlDataReader rs3 = Db.rs(query, "eFormSqlConnection")) {
//				if (rs3.Read()) {
//					return new Answer {
//						CountV = GetInt32(rs3, 0)
//					};
//				}
//			}
//			return null;
//		}
//		
//		public Answer Read2(int projectRoundUserID)
//		{
//			string query = string.Format(
//				@"
//SELECT a.AnswerID,
//	a.CurrentPage
//FROM Answer a
//WHERE a.ProjectRoundUserID = {0}",
//				projectRoundUserID
//			);
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
//				if (rs.Read()) {
//					return new Answer {
//						Id = GetInt32(rs, 0),
//						CurrentPage = GetInt32(rs, 1)
//					};
//				}
//			}
//			return null;
//		}
//		
//		public BackgroundAnswer Read(string bqID, int val)
//		{
//			string query = string.Format(
//				@"
//SELECT BAID
//FROM BA
//WHERE BQID = {0}
//AND Value = {1}",
//				bqID,
//				val
//			);
//			using (SqlDataReader rs = Db.rs(query)) {
//				if (rs.Read()) {
//					return new BackgroundAnswer {
//						Id = GetInt32(rs, 0)
//					};
//				}
//			}
//			return null;
//		}
//		
		public Answer ReadByGroup(string groupBy, int yearFrom, int yearTo, string sortString, int monthFrom, int monthTo)
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
AND (YEAR(a.EndDT) = {1} AND MONTH(a.EndDT) >= {5} OR YEAR(a.EndDT) > {1})
AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) <= {6} OR YEAR(a.EndDT) < {2})
AND LEFT(pru.SortString, {3}) = '{4}'",
				groupBy,
                yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString.Length,
				sortString,
                monthFrom,
                monthTo
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer();
					a.DummyValue1 = GetInt32(rs, 0, 0);
					a.DummyValue2 = GetInt32(rs, 1, 0);
					a.DummyValue3 = GetInt32(rs, 2, 0);
					return a;
				}
			}
			return null;
		}
		
		/// <summary>
		/// Used in SuperReportImage thus no MonthFrom and MonthTo!
		/// </summary>
		/// <param name="groupBy"></param>
		/// <param name="questionID"></param>
		/// <param name="optionID"></param>
		/// <param name="yearFrom"></param>
		/// <param name="yearTo"></param>
		/// <param name="rnds"></param>
		/// <returns></returns>
		public Answer ReadMinMax(string groupBy, int questionID, int optionID, string yearFrom, string yearTo, string rnds)
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
		WHERE a.EndDT IS NOT NULL
		AND a.EndDT >= '{3}'
		AND a.EndDT < '{4}'
		{5}
		GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
	) tmp
	GROUP BY tmp.DT
) tmp2",
				groupBy,
				questionID,
				optionID,
				yearFrom,
				yearTo,
				rnds
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer() {
//						Max = (float)GetDouble(rs, 0),
//						Min = (float)GetDouble(rs, 1)
						Max = (float)GetDouble(rs, 0, 100),
						Min = (float)GetDouble(rs, 1, 0)
					};
					return a;
				}
			}
			return null;
		}
		
		public Answer ReadMinMax(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString, int fm, int tm)
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
		AND LEFT(pru.SortString, {5}) = '{6}'
		GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
	) tmp
	GROUP BY tmp.DT
) tmp2",
				groupBy,
				questionID,
				optionID,
				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString.Length,
				sortString
			);
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					var a = new Answer() {
						Max = (float)GetDouble(rs, 0),
						Min = (float)GetDouble(rs, 1)
					};
					return a;
				}
			}
			return null;
		}
		
//		public IList<BackgroundAnswer> FindBackgroundAnswers(int bqID)
//		{
//			string query = string.Format(
//				@"
//SELECT BAID,
//	Internal
//FROM BA
//WHERE BQID = {0}
//ORDER BY SortOrder",
//				bqID
//			);
//			var answers = new List<BackgroundAnswer>();
//			using (SqlDataReader rs = Db.rs(query, "healthWatchSqlConnection")) {
//				while (rs.Read()) {
//					var a = new BackgroundAnswer {
//						Id = rs.GetInt32(0),
//						Internal = rs.GetString(1)
//					};
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
//		
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
		
		public IList<Answer> FindByQuestionAndOptionWithYearSpan(int questionID, int optionID, int yearFrom, int yearTo, int fm, int tm)
		{
			string query = string.Format(
				@"
SELECT dbo.cf_yearMonthDay(a.EndDT), AVG(av.ValueInt)
FROM Answer a
LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {0} AND av.OptionID = {1}
WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = 1
--AND YEAR(a.EndDT) >= {2}
--AND YEAR(a.EndDT) <= {3}
AND (YEAR(a.EndDT) = {2} AND MONTH(a.EndDT) >= {4} OR YEAR(a.EndDT) > {2})
AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) <= {5} OR YEAR(a.EndDT) < {3})
GROUP BY 1 dbo.cf_yearMonthDay(a.EndDT)
ORDER BY 1 dbo.cf_yearMonthDay(a.EndDT)",
				questionID,
				optionID,
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				fm,
				tm
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
		
//		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo)
//		{
//			string query = string.Format(
//				@"
//SELECT tmp.DT,
//	AVG(tmp.V),
//	COUNT(tmp.V),
//	STDEV(tmp.V)
//FROM (
//	SELECT {1}(a.EndDT) AS DT,
//		AVG(av.ValueInt) AS V
//	FROM Answer a
//	{0}
//	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {2} AND av.OptionID = {3}
//	WHERE a.EndDT IS NOT NULL
//	{4}
//	{5}
//	GROUP BY a.ProjectRoundUserID, {1}(a.EndDT)
//) tmp
//GROUP BY tmp.DT
//ORDER BY tmp.DT",
//				join,
//				groupBy,
//				questionID,
//				optionID,
//				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
//				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : ""
//			);
//			var answers = new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
//				while (rs.Read()) {
//					var a = new Answer {
//						DT = rs.GetInt32(0),
//						AverageV = rs.GetInt32(1),
//						CountV = GetInt32(rs, 2),
//						StandardDeviation = (float)GetDouble(rs, 3)
//					};
//					answers.Add(a);
//				}
//			}
//			return answers;
//		}
		
		public IList<Answer> FindByQuestionAndOptionJoinedAndGrouped2(string join, string groupBy, int questionID, int optionID, int yearFrom, int yearTo, int monthFrom, int monthTo)
		{
			string query = string.Format(
				@"
SELECT {1}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
FROM Answer a
{0}
INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {2} AND av.OptionID = {3}
WHERE a.EndDT IS NOT NULL
--AND YEAR(a.EndDT) >= {4}
--AND YEAR(a.EndDT) <= {5}
AND (YEAR(a.EndDT) = {4} AND MONTH(a.EndDT) >= {6} OR YEAR(a.EndDT) > {4})
AND (YEAR(a.EndDT) = {5} AND MONTH(a.EndDT) <= {7} OR YEAR(a.EndDT) < {5})
GROUP BY a.ProjectRoundUserID, {1}(a.EndDT)",
				join,
				groupBy,
				questionID,
				optionID,
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : ""
				monthFrom,
				monthTo
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
				if (rs.Read()) {
					bool done = false;
					while (!done) {
						var a = new Answer { };
						do {
							a.DT = rs.GetInt32(0);
							a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(1) });
							done = !rs.Read();
						} while (!done && rs.GetInt32(0) == a.DT);
						answers.Add(a);
					}
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionGrouped4(string groupBy, int questionID, int optionID, string join1, string yearFrom, string yearTo, string rnds1)
		{
			string query = string.Format(
				@"
--SELECT tmp.DT,
--	AVG(tmp.V),
--	COUNT(tmp.V),
--	STDEV(tmp.V)
--FROM (
	SELECT {0}(a.EndDT) AS DT,
		AVG(av.ValueInt) AS V
	FROM Answer a
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	{3}
	WHERE a.EndDT IS NOT NULL
	AND a.EndDT >= '{4}'
	AND a.EndDT < '{5}'
	{6}
	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
	ORDER BY DT
--) tmp
--GROUP BY tmp.DT
--ORDER BY tmp.DT",
				groupBy,
				questionID,
				optionID,
				join1,
				yearFrom,
				yearTo,
				rnds1
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				if (rs.Read()) {
					bool done = false;
					while (!done) {
						var a = new Answer { };
						do {
							a.DT = rs.GetInt32(0);
							a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(1) });
							done = !rs.Read();
						} while (!done && rs.GetInt32(0) == a.DT);
						answers.Add(a);
					}
				}
			}
			return answers;
		}
		
		/// <summary>
		/// Used in SuperReportImage thus no MonthFrom and MonthTo!
		/// </summary>
		/// <param name="groupBy"></param>
		/// <param name="questionID"></param>
		/// <param name="optionID"></param>
		/// <param name="join1"></param>
		/// <param name="yearFrom"></param>
		/// <param name="yearTo"></param>
		/// <param name="rnds1"></param>
		/// <returns></returns>
		public IList<Answer> FindByQuestionAndOptionGrouped3(string groupBy, int questionID, int optionID, string join1, string yearFrom, string yearTo, string rnds1)
		{
			string query = string.Format(
				@"
SELECT tmp.DT,
	AVG(tmp.V),
	COUNT(tmp.V),
	STDEV(tmp.V)
FROM (
	SELECT {0}(a.EndDT) AS DT,
		AVG(av.ValueInt) AS V
	FROM Answer a
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	{3}
	WHERE a.EndDT IS NOT NULL
	AND a.EndDT >= '{4}'
	AND a.EndDT < '{5}'
	{6}
	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
) tmp
GROUP BY tmp.DT
ORDER BY tmp.DT",
				groupBy,
				questionID,
				optionID,
				join1,
				yearFrom,
				yearTo,
				rnds1
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlConnection")) {
				while (rs.Read()) {
					var a = new Answer {
						DT = rs.GetInt32(0),
						AverageV = GetInt32(rs, 1, -1),
						CountV = GetInt32(rs, 2),
						StandardDeviation = (float)GetDouble(rs, 3, -1)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionGroupedX(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString, int fm, int tm)
		{
			string query = string.Format(
				@"
--SELECT tmp.DT,
--	AVG(tmp.V),
--	COUNT(tmp.V),
--	STDEV(tmp.V)
--FROM (
	SELECT {0}(a.EndDT) AS DT,
		AVG(av.ValueInt) AS V
	FROM Answer a
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID
	WHERE a.EndDT IS NOT NULL
		AND a.EndDT >= pr.Started
		--AND YEAR(a.EndDT) >= {3}
		--AND YEAR(a.EndDT) <= {4}
		AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) >= {7} OR YEAR(a.EndDT) > {3})
		AND (YEAR(a.EndDT) = {4} AND MONTH(a.EndDT) <= {8} OR YEAR(a.EndDT) < {4})
		AND LEFT(pru.SortString, {5}) = '{6}'
	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
--) tmp
--GROUP BY tmp.DT
--ORDER BY tmp.DT",
				groupBy,
				questionID,
				optionID,
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString.Length,
				sortString,
				fm,
				tm
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
				if (rs.Read()) {
					bool done = false;
					while (!done) {
						var a = new Answer { };
						do {
							a.DT = rs.GetInt32(0);
							a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(1) });
							done = !rs.Read();
						} while (!done && rs.GetInt32(0) == a.DT);
						answers.Add(a);
					}
				}
			}
			return answers;
		}
		
		public IList<Answer> FindByQuestionAndOptionGrouped(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString, int fm, int tm)
		{
			string query = string.Format(
				@"
SELECT tmp.DT,
	AVG(tmp.V),
	COUNT(tmp.V),
	STDEV(tmp.V)
FROM (
	SELECT {0}(a.EndDT) AS DT,
		AVG(av.ValueInt) AS V
	FROM Answer a
	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
	INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID
	WHERE a.EndDT IS NOT NULL
		AND a.EndDT >= pr.Started
		--AND YEAR(a.EndDT) >= {3}
		--AND YEAR(a.EndDT) <= {4}
		AND (YEAR(a.EndDT) = {3} AND MONTH(a.EndDT) >= {7} OR YEAR(a.EndDT) > {3})
		AND (YEAR(a.EndDT) = {4} AND MONTH(a.EndDT) <= {8} OR YEAR(a.EndDT) < {4})
		AND LEFT(pru.SortString, {5}) = '{6}'
	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
) tmp
GROUP BY tmp.DT
ORDER BY tmp.DT",
				groupBy,
				questionID,
				optionID,
				yearFrom, //yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
				yearTo, //yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
				sortString.Length,
				sortString,
				fm,
				tm
			);
			var answers = new List<Answer>();
			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
				while (rs.Read()) {
					var a = new Answer {
						DT = rs.GetInt32(0),
						AverageV = rs.GetInt32(1),
						CountV = rs.GetInt32(2),
						//StandardDeviation = rs.GetFloat(3)
                        StandardDeviation = (float)GetDouble(rs, 3, -1)
					};
					answers.Add(a);
				}
			}
			return answers;
		}
		
//		public IList<Answer> FindByQuestionAndOptionGrouped2(string groupBy, int questionID, int optionID, int yearFrom, int yearTo, string sortString)
//		{
//			string query = string.Format(
//				@"
//SELECT {0}(a.EndDT) AS DT, AVG(av.ValueInt) AS V
//FROM Answer a
//INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID
//	AND av.QuestionID = {1}
//	AND av.OptionID = {2}
//INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
//INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID
//WHERE a.EndDT IS NOT NULL
//	AND a.EndDT >= pr.Started
//	{3}
//	{4}
//	AND LEFT(pru.SortString, {5}) = '{6}'
//GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)",
//				groupBy,
//				questionID,
//				optionID,
//				yearFrom != 0 ? "AND YEAR(a.EndDT) >= " + yearFrom : "",
//				yearTo != 0 ? "AND YEAR(a.EndDT) <= " + yearTo : "",
//				sortString.Length,
//				sortString
//			);
//			var answers = new List<Answer>();
//			using (SqlDataReader rs = Db.rs(query, "eFormSqlconnection")) {
//				if (rs.Read()) {
//					bool done = false;
//					while (!done) {
//						var a = new Answer { };
//						do {
//							a.DT = rs.GetInt32(0);
//							a.Values.Add(new AnswerValue { ValueInt = rs.GetInt32(1) });
//							done = !rs.Read();
//						} while (!done && rs.GetInt32(0) == a.DT);
//						answers.Add(a);
//					}
//				}
//			}
//			return answers;
//		}
	}
}
