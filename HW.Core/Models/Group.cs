//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using HW.Core.Helpers;
using HW.Core.Repositories;

namespace HW.Core.Models
{
	public class Group : BaseModel
	{
		public string Description { get; set; }
		
		public class Grouping // TODO: Make this enum, it's awkward having this constant class!
		{
			public const int None = 0;
			public const int UsersOnUnit = 1;
			public const int UsersOnUnitAndSubUnits = 2;
			public const int BackgroundVariable = 3;
		}
		
		public class GroupBy // TODO: Make this enum, it's awkward having this constant class!
		{
			public const int OneWeek = 1;
			public const int TwoWeeksStartWithEvent = 7;
			public const int TwoWeeksStartWithOdd = 2;
			public const int OneMonth = 3;
			public const int ThreeMonths = 4;
			public const int SixMonths = 5;
			public const int OneYear = 6;
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
					case Group.GroupBy.TwoWeeksStartWithEvent: return "dbo.cf_year2WeekEven";
					default: throw new NotSupportedException();
			}
		}
		
//		public static int GetCount(int GRPNG, int SPONS, int SID, int PRUID, string GID, ref string extraDesc, Hashtable desc, Hashtable join, ArrayList item, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository)
		public static int GetCount(int GRPNG, int SPONS, int SID, int PRUID, string GID, ref string extraDesc, Dictionary<string, string> desc, Dictionary<string, string> join, List<string> item, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository)
		{
			int COUNT = 0;
			switch (GRPNG)
			{
				case Group.Grouping.None:
					{
						string tmpDesc = "";
						int sslen = 0;
						string tmpSS = "";
						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(SID, SPONS) : departmentRepository.FindBySponsorOrderedBySortString(SID);
						foreach (Department d in departments) {
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
							} else {
								break;
							}
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
								PRUID,
								sslen,
								tmpSS
							)
						);
						COUNT++;
						break;
					}
				case Group.Grouping.UsersOnUnit:
					{
						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(SID, SPONS, GID) : departmentRepository.FindBySponsorOrderedBySortStringIn(SID, GID);
						foreach (Department d in departments) {
							item.Add(d.Id.ToString());
							desc.Add(d.Id.ToString(), d.Name);
							join.Add(
								d.Id.ToString(),
								string.Format(
									@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = {1}",
									PRUID,
									d.Id
								)
							);
							COUNT++;
						}
						break;
					}
				case Group.Grouping.UsersOnUnitAndSubUnits:
					{
						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdminIn(SID, SPONS, GID) : departmentRepository.FindBySponsorOrderedBySortStringIn(SID, GID);
						foreach (Department d in departments) {
							item.Add(d.Id.ToString());
							desc.Add(d.Id.ToString(), d.Name);
							join.Add(
								d.Id.ToString(),
								string.Format(
									@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) = '{2}'",
									PRUID,
									d.SortString.Length,
									d.SortString
								)
							);
							COUNT++;
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

						IList<Department> departments = SPONS != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(SID, SPONS) : departmentRepository.FindBySponsorOrderedBySortString(SID);
						foreach (Department d in departments) {
							if (sslen == 0) {
								sslen = d.SortString.Length;
							}
							if (sslen == d.SortString.Length) {
//								tmpDesc += (tmpDesc != "" ? ", " : "") + d.Name + "+";
//								tmpSS += (tmpSS != "" ? "," : "") + "'" + d.SortString + "'";
								tmpDesc += string.Format("{0}{1}+", (tmpDesc != "" ? ", " : ""), d.Name);
								tmpSS += string.Format("{0}'{1}'", (tmpSS != "" ? "," : ""), d.SortString);
							} else {
								break;
							}
						}
						string bqid = GID.Replace("'", "");
						GID = "";
						var questions = questionRepository.FindLikeBackgroundQuestions(bqid);
						foreach (var bq in questions) {
//							GID += (GID != "" ? "," : "") + bq.Id;
							GID += string.Format("{0}{1}", (GID != "" ? "," : ""), bq.Id);

//							extraDesc += (extraDesc != "" ? " / " : "") + bq.Internal;
							extraDesc += string.Format("{0}{1}", (extraDesc != "" ? " / " : ""), bq.Internal);

//							tmpSelect += (tmpSelect != "" ? " ," : "") + "ba" + bq.Id + ".BAID,ba" + bq.Id + ".Internal ";
							tmpSelect += string.Format("{0}ba{1}.BAID,ba{1}.Internal,ba{1}.BQID", (tmpSelect != "" ? " ," : ""), bq.Id); // TODO: Add BQID here!
//							tmpJoin += (tmpJoin != "" ? "INNER JOIN BA ba" + bq.Id + " ON ba" + bq.Id + ".BQID = " + bq.Id + " " : "FROM BA ba" + bq.Id + " ");
							tmpJoin += (tmpJoin != "" ? string.Format("INNER JOIN BA ba{0} ON ba{0}.BQID = {0} ", bq.Id) : string.Format(" FROM BA ba{0} ", bq.Id));
//							tmpOrder += (tmpOrder != "" ? ", ba" + bq.Id + ".SortOrder" : "WHERE ba" + bq.Id + ".BQID = " + bq.Id + " ORDER BY ba" + bq.Id + ".SortOrder");
							tmpOrder += (tmpOrder != "" ? string.Format(", ba{0}.SortOrder", bq.Id) : string.Format("WHERE ba{0}.BQID = {0} ORDER BY ba{0}.SortOrder", bq.Id));
						}
						string[] GIDS = GID.Split(',');

						SqlDataReader rs2;
						string query = "SELECT " +
							tmpSelect +
							tmpJoin +
							tmpOrder;
						rs2 = Db.rs(query);
						while (rs2.Read()) {
//						foreach (var bq in questionRepository.FindBackgroundQuestionsWithAnswers(query, GIDS.Length)) {
							string key = "";
							string txt = "";
							string sql = string.Format(
								@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = {0}
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, {1}) IN ({2})",
								PRUID,
								sslen,
								tmpSS
							);

							for (int i= 0; i < GIDS.Length; i++) {
//							foreach (var a in bq.Answers) {
								key += (key != "" ? "X" : "") + rs2.GetInt32(0 + i * 3);
								txt += (txt != "" ? " / " : "") + rs2.GetString(1 + i * 3);
								sql += string.Format(
									@"
INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
									GIDS[i],
									rs2.GetInt32(0 + i * 3)
								);
//								key += string.Format("{0}{1}", (key != "" ? "X" : ""), a.Id);
//								txt += string.Format("{0}{1}", (txt != "" ? " / " : ""), a.Internal);
//								sql += string.Format(
//									@"
//INNER JOIN healthWatch..UserProfileBQ HWp{0} ON HWup.UserProfileID = HWp{0}.UserProfileID AND HWp{0}.BQID = {0} AND HWp{0}.ValueInt = {1}",
//									bq.Id,
//									a.Id
//								);
							}
							COUNT++;

							item.Add(key);
							desc.Add(key, txt);
							join.Add(key, sql);
						}
						rs2.Close();
						break;
					}
			}
			return COUNT;
		}
	}
}
