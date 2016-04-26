using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Models
{
	public class Group : BaseModel
	{
		public string Description { get; set; }
		
		public class Grouping
		{
			public const int None = 0;
			public const int UsersOnUnit = 1;
			public const int UsersOnUnitAndSubUnits = 2;
			public const int BackgroundVariable = 3;
		}
		
		public static class GroupBy // TODO: Make this enum, it's awkward having this constant class!
		{
			public const int OneWeek = 1;
			public const int TwoWeeksStartWithOdd = 2;
			public const int OneMonth = 3;
			public const int ThreeMonths = 4;
			public const int SixMonths = 5;
			public const int OneYear = 6;
			public const int TwoWeeksStartWithEven = 7;
		}
	}
	
	public class GroupFactory
	{
		public static string GetGroupBy(int GB)
		{
			switch (GB) {
					case Group.GroupBy.OneWeek: return "dbo.cf_yearWeek";
					case Group.GroupBy.TwoWeeksStartWithOdd: return "dbo.cf_year2Week";
					case Group.GroupBy.OneMonth: return "dbo.cf_yearMonth";
					case Group.GroupBy.ThreeMonths: return "dbo.cf_year3Month";
					case Group.GroupBy.SixMonths: return "dbo.cf_year6Month";
					case Group.GroupBy.OneYear: return "YEAR";
					case Group.GroupBy.TwoWeeksStartWithEven: return "dbo.cf_year2WeekEven";
					default: throw new NotSupportedException();
			}
		}
		
		public static int GetCount(int grouping, int sponsorAdminID, int sponsorID, int projectRoundUnitID, string departmentIDs, ref string extraDesc, Dictionary<string, string> desc, Dictionary<string, string> join, List<string> item, Dictionary<string, int> mins, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
		{
			int count = 0;
			switch (grouping) {
				case Group.Grouping.None:
					{
						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							if (i == 0) {
								mins.Add("1", d.MinUserCountToDisclose);
							}
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
							} else {
								break;
							}
							i++;
						}

						item.Add("1");
						desc.Add("1", tmpDesc);
						join.Add(
							"1",
							string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2}) ",
								projectRoundUnitID,
								sslen,
								tmpSS
							)
						);
						count++;
						break;
					}
				case Group.Grouping.UsersOnUnit:
					{
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							item.Add(d.Id.ToString());
							desc.Add(d.Id.ToString(), d.Name);
							mins.Add(d.Id.ToString(), d.MinUserCountToDisclose);
							join.Add(
								d.Id.ToString(),
								string.Format(
									@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
									projectRoundUnitID,
									d.Id
								)
							);
							count++;
						}
						break;
					}
				case Group.Grouping.UsersOnUnitAndSubUnits:
					{
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							item.Add(d.Id.ToString());
							desc.Add(d.Id.ToString(), d.Name);
							mins.Add(d.Id.ToString(), d.MinUserCountToDisclose);
							join.Add(
								d.Id.ToString(),
								string.Format(
									@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
									projectRoundUnitID,
									d.SortString.Length,
									d.SortString
								)
							);
							count++;
						}
						break;
					}
				case Group.Grouping.BackgroundVariable:
					{
						string tmpSelect = "";
						string tmpJoin = "";
						string tmpOrder = "";

						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;

						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							if (i == 0) {
								sponsorMinUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
							} else {
								break;
							}
							i++;
						}
						string bqid = departmentIDs.Replace("'", "");
						departmentIDs = "";
						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
						foreach (var bq in questions) {
							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);

							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);

							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
						}
						string[] gids = departmentIDs.Split(',');

//						SqlDataReader rs2;
						string query = "SELECT " +
							tmpSelect +
							tmpJoin +
							tmpOrder;
//						rs2 = Db.rs(query);
//						while (rs2.Read()) {
						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
						foreach (var bq in questions) {
							string key = "";
							string txt = "";
							string sql = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
								projectRoundUnitID,
								sslen,
								tmpSS
							);

//							for (int i= 0; i < gids.Length; i++) {
							foreach (var a in bq.Answers) {
//								key += (key != "" ? "X" : "") + rs2.GetInt32(0 + i * 3);
//								txt += (txt != "" ? " / " : "") + rs2.GetString(1 + i * 3);
//								sql += string.Format(
//									@"
								//INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									gids[i],
//									rs2.GetInt32(0 + i * 3)
//								);
								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
								sql += string.Format(
									@"
INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
									bq.Id,
									a.Id
								);
							}
							count++;

							item.Add(key);
							desc.Add(key, txt);
							mins.Add(key, sponsorMinUserCountToDisclose);
							join.Add(key, sql);
						}
//						rs2.Close();
						break;
					}
			}
			return count;
		}
		
//		public IList<DepartmentAndQuery> GetDepartmentsWithJoinQuery(int grouping, int sponsorAdminID, int sponsorID, int projectRoundUnitID, string departmentIDs, ref string extraDesc, Dictionary<string, string> desc, Dictionary<string, string> join, List<string> item, Dictionary<string, int> mins, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
//		public IList<DepartmentAndQuery> GetDepartmentsWithJoinQuery(int grouping, int sponsorAdminID, int sponsorID, int projectRoundUnitID, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
		public IList<IDepartment> GetDepartmentsWithJoinQuery(int grouping, int sponsorAdminID, int sponsorID, int projectRoundUnitID, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
		{
//			int count = 0;
//			var y = new List<DepartmentAndQuery>();
			var y = new List<IDepartment>();
			switch (grouping) {
				case Group.Grouping.None:
					{
						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
						int minUserCountToDisclose = 0;
						foreach (Department d in departments) {
							if (i == 0) {
//								mins.Add("1", d.MinUserCountToDisclose);
								minUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
							} else {
								break;
							}
							i++;
						}

						string query = string.Format(
							@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2}) ",
							projectRoundUnitID,
							sslen,
							tmpSS
						);
//						y.Add(new DepartmentAndQuery("1", tmpDesc, minUserCountToDisclose, query));
						y.Add(new Department("1", tmpDesc, minUserCountToDisclose, query));
//						item.Add("1");
//						desc.Add("1", tmpDesc);
//						join.Add(
//							"1",
//							string.Format(
//								@"
						//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
						//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
						//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = {0}
						//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2}) ",
//								projectRoundUnitID,
//								sslen,
//								tmpSS
//							)
//						);
//						count++;
						break;
					}
				case Group.Grouping.UsersOnUnit:
					{
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
								projectRoundUnitID,
								d.Id
							);
//							y.Add(new DepartmentAndQuery(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
							y.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
//							item.Add(d.Id.ToString());
//							desc.Add(d.Id.ToString(), d.Name);
//							mins.Add(d.Id.ToString(), d.MinUserCountToDisclose);
//							join.Add(
//								d.Id.ToString(),
//								string.Format(
//									@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
//									projectRoundUnitID,
//									d.Id
//								)
//							);
//							count++;
						}
						break;
					}
				case Group.Grouping.UsersOnUnitAndSubUnits:
					{
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
								projectRoundUnitID,
								d.SortString.Length,
								d.SortString
							);
//							item.Add(d.Id.ToString());
//							desc.Add(d.Id.ToString(), d.Name);
//							mins.Add(d.Id.ToString(), d.MinUserCountToDisclose);
//							join.Add(
//								d.Id.ToString(),
//								string.Format(
//									@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
//									projectRoundUnitID,
//									d.SortString.Length,
//									d.SortString
//								)
//							);
//							count++;
						}
						break;
					}
				case Group.Grouping.BackgroundVariable:
					{
						string tmpSelect = "";
						string tmpJoin = "";
						string tmpOrder = "";

						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;

						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							if (i == 0) {
								sponsorMinUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
							} else {
								break;
							}
							i++;
						}
						string bqid = departmentIDs.Replace("'", "");
						departmentIDs = "";
						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
						foreach (var bq in questions) {
							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);

							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);

							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
						}
						string[] gids = departmentIDs.Split(',');

						string query = "SELECT " +
							tmpSelect +
							tmpJoin +
							tmpOrder;
						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
						foreach (var bq in questions) {
							string key = "";
							string txt = "";
							string sql = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
								projectRoundUnitID,
								sslen,
								tmpSS
							);

							foreach (var a in bq.Answers) {
								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
								sql += string.Format(
									@"
INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
									bq.Id,
									a.Id
								);
							}
//							y.Add(new DepartmentAndQuery(key, txt, sponsorMinUserCountToDisclose, sql));
							y.Add(new Department(key, txt, sponsorMinUserCountToDisclose, sql));
//							item.Add(key);
//							desc.Add(key, txt);
//							mins.Add(key, sponsorMinUserCountToDisclose);
//							join.Add(key, sql);
						}
						break;
					}
			}
//			return count;
			return y;
		}
		
//		public static List<DepartmentAndQuery> GetDepartmentsWithJoinQueryForStepCount(int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
		public static List<IDepartment> GetDepartmentsWithJoinQueryForStepCount(int grouping, int sponsorAdminID, int sponsorID, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
		{
//			var y = new List<DepartmentAndQuery>();
			var y = new List<IDepartment>();
			switch (grouping) {
				case Group.Grouping.None:
					{
						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
						int minUserCountToDisclose = 0;
						foreach (Department d in departments) {
							if (i == 0) {
								minUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
							} else {
								break;
							}
							i++;
						}
						string query = string.Format(
							@"
INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
INNER JOIN healthWatch..UserProfile up ON up.UserProfileID = um.UserProfileID
INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND LEFT(d.SortString, {0}) IN ({1}) ",
							sslen,
							tmpSS
						);
//						string query = string.Format(
//							@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2}) ",
//							"509", //pruid,
//							sslen,
//							tmpSS
//						);
//						y.Add(new DepartmentAndQuery("1", tmpDesc, minUserCountToDisclose, query));
						y.Add(new Department("1", tmpDesc, minUserCountToDisclose, query));
						break;
					}
				case Group.Grouping.UsersOnUnit:
					{
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID AND up.DepartmentID = {0}",
								d.Id
							);
//							y.Add(new DepartmentAndQuery(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
							y.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
						}
						break;
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							string query = string.Format(
//								@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
//								"509", //pruid,
//								d.Id
//							);
//							y.Add(new DepartmentAndQuery(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
//						}
//						break;
					}
				case Group.Grouping.UsersOnUnitAndSubUnits:
					{
						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID
INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND LEFT(d.SortString, {0}) = '{1}'",
								d.SortString.Length,
								d.SortString
							);
//							y.Add(new DepartmentAndQuery(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
							y.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
						}
						break;
					}
				case Group.Grouping.BackgroundVariable:
					{
						string tmpSelect = "";
						string tmpJoin = "";
						string tmpOrder = "";

						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;

						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
						foreach (Department d in departments) {
							if (i == 0) {
								sponsorMinUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
							} else {
								break;
							}
							i++;
						}
						string bqid = departmentIDs.Replace("'", "");
						departmentIDs = "";
						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
						foreach (var bq in questions) {
							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);

							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);

							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
						}
						string[] gids = departmentIDs.Split(',');

						string query = "SELECT " +
							tmpSelect +
							tmpJoin +
							tmpOrder;
						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
						foreach (var bq in questions) {
							string key = "";
							string txt = "";
							string sql = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
								"509", //pruid,
								sslen,
								tmpSS
							);

							foreach (var a in bq.Answers) {
								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
								sql += string.Format(
									@"
INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
									bq.Id,
									a.Id
								);
							}
//							y.Add(new DepartmentAndQuery(key, txt, sponsorMinUserCountToDisclose, sql));
							y.Add(new Department(key, txt, sponsorMinUserCountToDisclose, sql));
						}
						break;
					}
			}
			return y;
		}
	}
	
//	public class DepartmentAndQuery : Department, IDepartment
//	{
//		public string Key { get; set; }
//		public string Query { get; set; }
//		
//		public DepartmentAndQuery()
//		{
//		}
//		
//		public DepartmentAndQuery(string key, string name, int minUserCountToDisclose, string query)
//		{
//			this.Key = key;
//			this.Name = name;
//			this.MinUserCountToDisclose = minUserCountToDisclose;
//			this.Query = query;
//		}
//	}
}
