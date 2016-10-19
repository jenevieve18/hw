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
	}
	
	public class Grouping
	{
		public const int None = 0;
		public const int UsersOnUnit = 1;
		public const int UsersOnUnitAndSubUnits = 2;
		public const int BackgroundVariable = 3;
	}
	
	public static class GroupBy
	{
		public const int OneWeek = 1;
		public const int TwoWeeksStartWithOdd = 2;
		public const int OneMonth = 3;
		public const int ThreeMonths = 4;
		public const int SixMonths = 5;
		public const int OneYear = 6;
		public const int TwoWeeksStartWithEven = 7;
	}
	
	public class GroupFactory
	{
		public static string GetGroupBy(int groupBy)
		{
			switch (groupBy) {
					case GroupBy.OneWeek: return "dbo.cf_yearWeek";
					case GroupBy.TwoWeeksStartWithOdd: return "dbo.cf_year2Week";
					case GroupBy.OneMonth: return "dbo.cf_yearMonth";
					case GroupBy.ThreeMonths: return "dbo.cf_year3Month";
					case GroupBy.SixMonths: return "dbo.cf_year6Month";
					case GroupBy.OneYear: return "YEAR";
					case GroupBy.TwoWeeksStartWithEven: return "dbo.cf_year2WeekEven";
					default: throw new NotSupportedException();
			}
		}
		
		#region
//		public static int GetCount(int grouping, int sponsorAdminID, int sponsorID, int projectRoundUnitID, string departmentIDs, ref string extraDesc, Dictionary<string, string> desc, Dictionary<string, string> join, List<string> item, Dictionary<string, int> mins, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
//		{
//			int count = 0;
//			switch (grouping) {
//				case Grouping.None:
//					{
//						string tmpDesc = "";
//						int sslen = 0;
//						string tmpSS = "";
//						int i = 0;
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							if (i == 0) {
//								mins.Add("1", d.MinUserCountToDisclose);
//							}
//							if (sslen == 0) {
//								sslen = d.SortString.Length;
//							}
//							if (sslen == d.SortString.Length) {
//								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
//								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
//							} else {
//								break;
//							}
//							i++;
//						}
//
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
//						break;
//					}
//				case Grouping.UsersOnUnit:
//					{
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
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
//						}
//						break;
//					}
//				case Grouping.UsersOnUnitAndSubUnits:
//					{
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
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
//						}
//						break;
//					}
//				case Grouping.BackgroundVariable:
//					{
//						string tmpSelect = "";
//						string tmpJoin = "";
//						string tmpOrder = "";
//
//						string tmpDesc = "";
//						int sslen = 0;
//						string tmpSS = "";
//						int i = 0;
//
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							if (i == 0) {
//								sponsorMinUserCountToDisclose = d.MinUserCountToDisclose;
//							}
//							if (sslen == 0) {
//								sslen = d.SortString.Length;
//							}
//							if (sslen == d.SortString.Length) {
//								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
//								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
//							} else {
//								break;
//							}
//							i++;
//						}
//						string bqid = departmentIDs.Replace("'", "");
//						departmentIDs = "";
//						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
//						foreach (var bq in questions) {
//							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);
//
//							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);
//
//							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
//							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
//							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
//						}
//						string[] gids = departmentIDs.Split(',');
//
//						string query = "SELECT " +
//							tmpSelect +
//							tmpJoin +
//							tmpOrder;
//						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
//						foreach (var bq in questions) {
//							string key = "";
//							string txt = "";
//							string sql = string.Format(
//								@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
//								projectRoundUnitID,
//								sslen,
//								tmpSS
//							);
//
//							foreach (var a in bq.Answers) {
//								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
//								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
//								sql += string.Format(
//									@"
//INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									bq.Id,
//									a.Id
//								);
//							}
//							count++;
//
//							item.Add(key);
//							desc.Add(key, txt);
//							mins.Add(key, sponsorMinUserCountToDisclose);
//							join.Add(key, sql);
//						}
//						break;
//					}
//			}
//			return count;
//		}
		#endregion
		
		public static List<IDepartment> GetCount2(int grouping, Sponsor sponsor, ProjectRoundUnit projectRoundUnit, string departmentIDs, ref string extraDesc, SqlQuestionRepository questionRepository, IList<Department> departments)
		{
			var departmentsWithJoinQuery = new List<IDepartment>();
			switch (grouping) {
				case Grouping.None:
					{
						string description = "";
						int sortStringLength = 0;
						string sortString = "";
						int i = 0;
						int minUserCountToDisclose = 0;
						foreach (Department d in departments) {
							if (i == 0) {
								minUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sortStringLength == 0) {
								sortStringLength = d.SortString.Length;
							}
							if (sortStringLength == d.SortString.Length) {
								description += (description != "" ? ", " : "") + d.Name + "+";
								sortString += (sortString != "" ? "," : "") + "'" + d.SortString + "'";
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
							projectRoundUnit.Id,
							sortStringLength,
							sortString
						);
						departmentsWithJoinQuery.Add(new Department("1", description, minUserCountToDisclose, query));
						break;
					}
				case Grouping.UsersOnUnit:
					{
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
								projectRoundUnit.Id,
								d.Id
							);
							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
						}
						break;
					}
				case Grouping.UsersOnUnitAndSubUnits:
					{
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
								projectRoundUnit.Id,
								d.SortString.Length,
								d.SortString
							);
							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
						}
						break;
					}
				case Grouping.BackgroundVariable:
					{
						string tmpSelect = "";
						string tmpJoin = "";
						string tmpOrder = "";

						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						int i = 0;
						
						int minUserCountToDisclose = sponsor.MinUserCountToDisclose;

						foreach (Department d in departments) {
							if (i == 0) {
								minUserCountToDisclose = d.MinUserCountToDisclose;
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
//						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
//						foreach (var bq in questions) {
						using (var rs2 = Db.rs(query)) {
							while (rs2.Read()) {
							string key = "";
							string txt = "";
							string sql = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
								projectRoundUnit.Id,
								sslen,
								tmpSS
							);
//							foreach (var a in bq.Answers) {
//								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
//								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
//								sql += string.Format(
//									@"
//INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									bq.Id,
//									a.Id
//								);
//							}
							for (int j = 0; j < gids.Length; j++) {
								key += string.Format("{0}{1}", (key != "" ? "X" : ""), rs2.GetInt32(0 + j * 3));
								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), rs2.GetString(1 + j * 3));
								sql += string.Format(
									@"
INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
									gids[j],
									rs2.GetInt32(0 + j * 3)
								);
							}
							departmentsWithJoinQuery.Add(new Department(key, txt, minUserCountToDisclose, sql));
							}
						}
						break;
					}
			}
			return departmentsWithJoinQuery;
		}
		
		#region
//		public IList<IDepartment> GetDepartmentsWithJoinQuery(int grouping, int sponsorAdminID, int sponsorID, int projectRoundUnitID, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, int sponsorMinUserCountToDisclose)
//		{
//			var departmentsWithJoinQuery = new List<IDepartment>();
//			switch (grouping) {
//				case Grouping.None:
//					{
//						string tmpDesc = "";
//						int sslen = 0;
//						string tmpSS = "";
//						int i = 0;
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
//						int minUserCountToDisclose = 0;
//						foreach (Department d in departments) {
//							if (i == 0) {
//								minUserCountToDisclose = d.MinUserCountToDisclose;
//							}
//							if (sslen == 0) {
//								sslen = d.SortString.Length;
//							}
//							if (sslen == d.SortString.Length) {
//								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
//								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
//							} else {
//								break;
//							}
//							i++;
//						}
//						string query = string.Format(
//							@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2}) ",
//							projectRoundUnitID,
//							sslen,
//							tmpSS
//						);
//						departmentsWithJoinQuery.Add(new Department("1", tmpDesc, minUserCountToDisclose, query));
//						break;
//					}
//				case Grouping.UsersOnUnit:
//					{
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							string query = string.Format(
//								@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
//								projectRoundUnitID,
//								d.Id
//							);
//							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
//						}
//						break;
//					}
//				case Grouping.UsersOnUnitAndSubUnits:
//					{
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorID, sponsorAdminID, departmentIDs, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorID, departmentIDs, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							string query = string.Format(
//								@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
//								projectRoundUnitID,
//								d.SortString.Length,
//								d.SortString
//							);
//						}
//						break;
//					}
//				case Grouping.BackgroundVariable:
//					{
//						string tmpSelect = "";
//						string tmpJoin = "";
//						string tmpOrder = "";
//
//						string tmpDesc = "";
//						int sortStringLength = 0;
//						string tmpSS = "";
//						int i = 0;
//
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							if (i == 0) {
//								sponsorMinUserCountToDisclose = d.MinUserCountToDisclose;
//							}
//							if (sortStringLength == 0) {
//								sortStringLength = d.SortString.Length;
//							}
//							if (sortStringLength == d.SortString.Length) {
//								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
//								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
//							} else {
//								break;
//							}
//							i++;
//						}
//						string bqid = departmentIDs.Replace("'", "");
//						departmentIDs = "";
//						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
//						foreach (var bq in questions) {
//							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);
//
//							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);
//
//							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
//							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
//							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
//						}
//						string[] gids = departmentIDs.Split(',');
//
//						string query = "SELECT " +
//							tmpSelect +
//							tmpJoin +
//							tmpOrder;
//						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
//						foreach (var bq in questions) {
//							string key = "";
//							string txt = "";
//							string sql = string.Format(
//								@"
//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
//								projectRoundUnitID,
//								sortStringLength,
//								tmpSS
//							);
//
//							foreach (var a in bq.Answers) {
//								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
//								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
//								sql += string.Format(
//									@"
//INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									bq.Id,
//									a.Id
//								);
//							}
//							departmentsWithJoinQuery.Add(new Department(key, txt, sponsorMinUserCountToDisclose, sql));
//						}
//						break;
//					}
//			}
//			return departmentsWithJoinQuery;
//		}
		#endregion
		
		public static List<IDepartment> GetDepartmentsWithJoinQueryForStepCount(int grouping, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository)
		{
			var departmentsWithJoinQuery = new List<IDepartment>();
			switch (grouping) {
				case Grouping.None:
					{
						string tmpDesc = "";
						int sortStringLength = 0;
						string tmpSS = "";
						int i = 0;
						IList<Department> departments = sponsorAdmin != null ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsor.Id, sponsorAdmin.Id, sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsor.Id, sponsor.MinUserCountToDisclose);
						int minUserCountToDisclose = 0;
						foreach (Department d in departments) {
							if (i == 0) {
								minUserCountToDisclose = d.MinUserCountToDisclose;
							}
							if (sortStringLength == 0) {
								sortStringLength = d.SortString.Length;
							}
							if (sortStringLength == d.SortString.Length) {
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
							sortStringLength,
							tmpSS
						);
						departmentsWithJoinQuery.Add(new Department("1", tmpDesc, minUserCountToDisclose, query));
						break;
					}
				case Grouping.UsersOnUnit:
					{
						IList<Department> departments = sponsorAdmin != null ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsor.Id, sponsorAdmin.Id, departmentIDs, sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsor.Id, departmentIDs, sponsor.MinUserCountToDisclose);
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID AND up.DepartmentID = {0}",
								d.Id
							);
							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
						}
						break;
					}
				case Grouping.UsersOnUnitAndSubUnits:
					{
						IList<Department> departments = sponsorAdmin != null ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsor.Id, sponsorAdmin.Id, departmentIDs, sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsor.Id, departmentIDs, sponsor.MinUserCountToDisclose);
						foreach (Department d in departments) {
							string query = string.Format(
								@"
INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID
INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND LEFT(d.SortString, {0}) = '{1}'",
								d.SortString.Length,
								d.SortString
							);
							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
						}
						break;
					}
				case Grouping.BackgroundVariable:
					throw new NotSupportedException();
					#region
//					{
//						string tmpSelect = "";
//						string tmpJoin = "";
//						string tmpOrder = "";
//
//						string tmpDesc = "";
//						int sslen = 0;
//						string tmpSS = "";
//						int i = 0;
//
//						IList<Department> departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID, sponsorMinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID, sponsorMinUserCountToDisclose);
//						foreach (Department d in departments) {
//							if (i == 0) {
//								sponsorMinUserCountToDisclose = d.MinUserCountToDisclose;
//							}
//							if (sslen == 0) {
//								sslen = d.SortString.Length;
//							}
//							if (sslen == d.SortString.Length) {
//								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
//								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
//							} else {
//								break;
//							}
//							i++;
//						}
//						string bqid = departmentIDs.Replace("'", "");
//						departmentIDs = "";
//						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
//						foreach (var bq in questions) {
//							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);
//
//							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);
//
//							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
//							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
//							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
//						}
//						string[] gids = departmentIDs.Split(',');
//
//						string query = "SELECT " +
//							tmpSelect +
//							tmpJoin +
//							tmpOrder;
//						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
//						foreach (var bq in questions) {
//							string key = "";
//							string txt = "";
//							string sql = string.Format(
//								@"
//					INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
//					INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
//					INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
//					INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
//								"509", //pruid,
//								sslen,
//								tmpSS
//							);
//
//							foreach (var a in bq.Answers) {
//								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
//								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
//								sql += string.Format(
//									@"
//					INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									bq.Id,
//									a.Id
//								);
//							}
//							y.Add(new Department(key, txt, sponsorMinUserCountToDisclose, sql));
//						}
//						break;
//					}
					#endregion
			}
			return departmentsWithJoinQuery;
		}
		
		#region
//		public static List<IDepartment> GetDepartmentsWithJoinQueryForStepCount2(int grouping, SponsorAdmin sponsorAdmin, string departmentIDs, ref string extraDesc, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository)
//		{
//			var departmentsWithJoinQuery = new List<IDepartment>();
//			switch (grouping) {
//				case Group.Grouping.None:
//					{
//						string tmpDesc = "";
//						int sortStringLength = 0;
//						string tmpSS = "";
//						int i = 0;
//						IList<Department> departments = sponsorAdmin.Id != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorAdmin.Sponsor.Id, sponsorAdmin.Id, sponsorAdmin.Sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorAdmin.Sponsor.Id, sponsorAdmin.Sponsor.MinUserCountToDisclose);
//						int minUserCountToDisclose = 0;
//						foreach (Department d in departments) {
//							if (i == 0) {
//								minUserCountToDisclose = d.MinUserCountToDisclose;
//							}
//							if (sortStringLength == 0) {
//								sortStringLength = d.SortString.Length;
//							}
//							if (sortStringLength == d.SortString.Length) {
//								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
//								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
//							} else {
//								break;
//							}
//							i++;
//						}
//						string query = string.Format(
//							@"
		//INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
		//INNER JOIN healthWatch..UserProfile up ON up.UserProfileID = um.UserProfileID
		//INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND LEFT(d.SortString, {0}) IN ({1}) ",
//							sortStringLength,
//							tmpSS
//						);
//						departmentsWithJoinQuery.Add(new Department("1", tmpDesc, minUserCountToDisclose, query));
//						break;
//					}
//				case Group.Grouping.UsersOnUnit:
//					{
//						IList<Department> departments = sponsorAdmin.Id != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorAdmin.Sponsor.Id, sponsorAdmin.Id, departmentIDs, sponsorAdmin.Sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorAdmin.Sponsor.Id, departmentIDs, sponsorAdmin.Sponsor.MinUserCountToDisclose);
//						foreach (Department d in departments) {
//							string query = string.Format(
//								@"
		//INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
		//INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID AND up.DepartmentID = {0}",
//								d.Id
//							);
//							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
//						}
//						break;
//					}
//				case Group.Grouping.UsersOnUnitAndSubUnits:
//					{
//						IList<Department> departments = sponsorAdmin.Id != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(sponsorAdmin.Sponsor.Id, sponsorAdmin.Id, departmentIDs, sponsorAdmin.Sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortStringIn(sponsorAdmin.Sponsor.Id, departmentIDs, sponsorAdmin.Sponsor.MinUserCountToDisclose);
//						foreach (Department d in departments) {
//							string query = string.Format(
//								@"
		//INNER JOIN healthwatch..UserSponsorProject usp ON usp.UserID = um.UserID AND usp.ConsentDT IS NOT NULL
		//INNER JOIN healthwatch..UserProfile up ON up.UserProfileID = um.UserProfileID
		//INNER JOIN healthWatch..Department d ON d.DepartmentID = up.DepartmentID AND LEFT(d.SortString, {0}) = '{1}'",
//								d.SortString.Length,
//								d.SortString
//							);
//							departmentsWithJoinQuery.Add(new Department(d.Id.ToString(), d.Name, d.MinUserCountToDisclose, query));
//						}
//						break;
//					}
//				case Group.Grouping.BackgroundVariable:
		////					throw new NotSupportedException();
//					{
//						string tmpSelect = "";
//						string tmpJoin = "";
//						string tmpOrder = "";
//
//						string tmpDesc = "";
//						int sslen = 0;
//						string tmpSS = "";
//						int i = 0;
//
//						IList<Department> departments = sponsorAdmin.Sponsor.Id != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorAdmin.Sponsor.Id, sponsorAdmin.Id, sponsorAdmin.Sponsor.MinUserCountToDisclose) : departmentRepository.FindBySponsorOrderedBySortString(sponsorAdmin.Sponsor.Id, sponsorAdmin.Sponsor.MinUserCountToDisclose);
//						foreach (Department d in departments) {
//							if (i == 0) {
//								sponsorAdmin.Sponsor.MinUserCountToDisclose = d.MinUserCountToDisclose;
//							}
//							if (sslen == 0) {
//								sslen = d.SortString.Length;
//							}
//							if (sslen == d.SortString.Length) {
//								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
//								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
//							} else {
//								break;
//							}
//							i++;
//						}
//						string bqid = departmentIDs.Replace("'", "");
//						departmentIDs = "";
//						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
//						foreach (var bq in questions) {
//							departmentIDs += string.Format("{0}{1}", (departmentIDs != "" ? "," : ""), bq.Id);
//
//							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);
//
//							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
//							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
//							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
//						}
//						string[] gids = departmentIDs.Split(',');
//
//						string query = "SELECT " +
//							tmpSelect +
//							tmpJoin +
//							tmpOrder;
//						questions = questionRepository.FindBackgroundQuestionsWithAnswers(query, gids.Length);
//						foreach (var bq in questions) {
//							string key = "";
//							string txt = "";
//							string sql = string.Format(
//								@"
		//INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
		//INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
		//INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
		//INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
//								"509", //pruid,
//								sslen,
//								tmpSS
//							);
//
//							foreach (var a in bq.Answers) {
//								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
//								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
//								sql += string.Format(
//									@"
		//INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									bq.Id,
//									a.Id
//								);
//							}
//							departmentsWithJoinQuery.Add(new Department(key, txt, sponsorAdmin.Sponsor.MinUserCountToDisclose, sql));
//						}
//						break;
//					}
//			}
//			return departmentsWithJoinQuery;
//		}
		#endregion
	}
}
