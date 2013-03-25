//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Core.Services
{
	public class ReportService
	{
		IAnswerRepository answerRepository;
		IReportRepository reportRepository;
		IProjectRepository projectRepository;
		IOptionRepository optionRepository;
		IDepartmentRepository departmentRepository;
		IQuestionRepository questionRepository;
		IIndexRepository indexRepository;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		Hashtable res = new Hashtable();
		Hashtable cnt = new Hashtable();
		
		public ReportService(IAnswerRepository answerRepository, IReportRepository reportRepository, IProjectRepository projectRepository, IOptionRepository optionRepository, IDepartmentRepository departmentRepository, IQuestionRepository questionRepository, IIndexRepository indexRepository)
		{
			this.answerRepository = answerRepository;
			this.reportRepository = reportRepository;
			this.projectRepository = projectRepository;
			this.optionRepository = optionRepository;
			this.departmentRepository = departmentRepository;
			this.questionRepository = questionRepository;
			this.indexRepository = indexRepository;
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			return reportRepository.FindByProjectAndLanguage(projectRoundID, langID);
		}
		
		public ReportPart ReadReportPart(int rpid, int langID)
		{
			return reportRepository.ReadReportPart(rpid, langID);
		}
		
		public IGraphFactory GetGraphFactory(bool hasAnswerKey)
		{
			if (hasAnswerKey) {
				return new UserLevelGraphFactory(answerRepository, reportRepository);
			} else {
				return new GroupStatsGraphFactory(answerRepository, reportRepository, projectRepository, optionRepository, indexRepository, questionRepository, departmentRepository);
			}
		}
		
		public ReportPart ReadReportPart2(int rpid, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, string plot, int GRPNG, int SPONS, int SID, string GID, int point)
		{
			var r = reportRepository.ReadReportPart(rpid, langID);
			SetReportPart(r, langID, PRUID, fy, ty, GB, hasGrouping, plot, GRPNG, SPONS, SID, GID, point);
			return r;
		}
		
		public void SetReportPart(ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, string plot, int GRPNG, int SPONS, int SID, string GID, int point)
		{
			string sortString = "";
			int minDT = 0;
			int maxDT = 0;
			ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(PRUID);
			if (roundUnit != null) {
				sortString = roundUnit.SortString;
				if (langID == 0) {
					langID = roundUnit.Language.Id;
				}
			}
			Dictionary<string, List<Answer>> weeks = new Dictionary<string, List<Answer>>();
			List<Answer> week =  new List<Answer>();
			List<Department> departments = new List<Department>();

			if (p.Type == 1) {
				decimal tot = answerRepository.CountByDate(fy, ty, sortString);

				if (p.RequiredAnswerCount > Convert.ToInt32(tot)) {
					// TODO: Empty graph
				} else {
					p.Option.Components = optionRepository.FindComponentsByLanguage(p.Option.Id, langID);
					foreach (OptionComponents c in p.Option.Components) {
						int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, p.Option.Id, p.Question.Id, sortString);
					}
				}
			} else if (p.Type == 3) {
				p.Components = reportRepository.FindComponents(p.Id);
				foreach (ReportPartComponent c in p.Components) {
					SortedList all = new SortedList();
					foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
						res = new Hashtable();
						if (c.Index.Parts.Count == 0) {
							GetIdxVal(c.Index.Id, u.SortString, langID, fy, ty);
						} else {
							GetOtherIdxVal(c.Index.Id, u.SortString, langID, fy, ty);
						}

						if (all.Contains(lastVal)) {
							all[lastVal] += "," + u.TreeString;
						} else {
							all.Add(lastVal, u.TreeString);
						}
					}

					for (int i = all.Count - 1; i >= 0; i--) {
						int color = c.Index.GetColor(Convert.ToInt32(all.GetKey(i)));
						string[] u = all.GetByIndex(i).ToString().Split(',');
						foreach (string s in u) {
						}
					}
				}
			} else if (p.Type == 2) {
				p.Components = reportRepository.FindComponents(p.Id);
				foreach (ReportPartComponent c in p.Components) {
					if (c.Index.Parts.Count == 0) {
						GetIdxVal(c.Index.Id, sortString, langID, fy, ty);
					} else {
						GetOtherIdxVal(c.Index.Id, sortString, langID, fy, ty);
					}
					int color = c.Index.GetColor(lastVal);
				}
			} else if (p.Type == 8) {
				if (GB == 0) {
					GB = 2;
				}
				
				string groupBy = GroupFactory.GetGroupBy(GB);

				Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
				if (answer != null) {
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}

				if (hasGrouping) {
					int COUNT = 0;
					Hashtable desc = new Hashtable();
					Hashtable join = new Hashtable();
					ArrayList item = new ArrayList();
					string extraDesc = "";
					
					COUNT = GroupFactory.GetCount(GRPNG, SPONS, SID, PRUID, GID, ref extraDesc, desc, join, item, departmentRepository, questionRepository);
					
					int bx = 0;
					foreach(string i in item) {
						int lastDT = minDT - 1;
						p.Question.Answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(join[i].ToString(), groupBy, p.Question.Id, p.Option.Id, fy, ty);
						int ii = minDT;
						int jj = 0;
						foreach (Answer a in p.Question.Answers) {
							jj++;
							while (lastDT + 1 < a.DT) {
								lastDT++;
							}
							if (a.Values.Count >= p.RequiredAnswerCount) {
								if (COUNT == 1) {
								}
							}
							lastDT = a.DT;
							ii++;
						}
						bx++;
					}
				} else {
					int bx = 0;
					p.Components = reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID);
					foreach (ReportPartComponent c in p.Components) {
						int lastDT = minDT - 1;
						foreach (Answer a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
							}

							if (a.CountV >= p.RequiredAnswerCount) {
							}
							lastDT = a.DT;
						}
						bx++;
					}
				}
			}
		}
		
//		public string DrawBottomString(int groupBy, int i, int dx, string str)
//		{
//			switch (groupBy) {
//				case 1:
//					{
//						int d = i;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = string.Format("v{0}, {1}{2}", w, d / 52, str);
//						return v;
//					}
//				case 2:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = string.Format("v{0}-{1}, {2}{3}", w - 2, w, (d - ((d - 1) % 52)) / 52, str);
//						return v;
//					}
//				case 3:
//					{
//						int d = i;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
//						return v;
//					}
//				case 4:
//					{
//						int d = i * 3;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = string.Format("Q{0}, {1}{2}", w / 3, (d - w) / 12, str);
//						return v;
//					}
//				case 5:
//					{
//						int d = i * 6;
//						int w = d % 12;
//						if (w == 0) {
//							w = 12;
//						}
//						string v = string.Format("{0}/{1}{2}", (d - w) / 12, w / 6, str);
//						return v;
//					}
//				case 6:
//					{
//						string v = i.ToString() + str;
//						return v;
//					}
//				case 7:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0) {
//							w = 52;
//						}
//						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
//						return v;
//					}
//				default:
//					throw new NotSupportedException();
//			}
//		}
//
		void GetIdxVal(int idx, string sortString, int langID, int fy, int ty)
		{
			foreach (Index i in indexRepository.FindByLanguage(idx, langID, fy, ty, sortString)) {
				lastCount = i.CountDX;
				lastVal = i.AverageAX;
				lastDesc = i.Languages[0].IndexName;
				if (!res.Contains(i.Id)) {
					res.Add(i.Id, lastVal);
				}
				if (!cnt.Contains(i.Id)) {
					cnt.Add(i.Id, lastCount);
				}
			}
		}

		void GetOtherIdxVal(int idx, string sortString, int langID, int fy, int ty)
		{
			float tot = 0;
			int max = 0;
			int minCnt = Int32.MaxValue;
			Index index = indexRepository.ReadByIdAndLanguage(idx, langID);
			if (index != null) {
				lastDesc = index.Languages[0].IndexName;
				foreach (IndexPart p in index.Parts) {
					max += 100 * p.Multiple;
					if (res.Contains(p.OtherIndex.Id)) {
						tot += (float)res[p.OtherIndex.Id] * p.Multiple;
						minCnt = Math.Min((int)cnt[p.OtherIndex.Id], minCnt);
					} else {
						GetIdxVal(p.OtherIndex.Id, sortString, langID, fy, ty);
						tot += lastVal * p.Multiple;
						minCnt = Math.Min(lastCount, minCnt);
					}
				}
			}
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}
		
		public string GetReportImageUrl(int GB, int rpid, int rplid, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution)
		{
			P p = new P("reportImage.aspx");
			p.Q.Add("LangID", langID);
			p.Q.Add("FY", fy);
			p.Q.Add("TY", ty);
			p.Q.Add("SAID", SPONS);
			p.Q.Add("SID", SID);
			p.Q.Add("DIST", distribution);
			p.Q.Add("GB", GB);
			p.Q.Add("RPID", rpid);
			p.Q.Add("RPLID", rplid);
			p.Q.Add("PRUID", PRUID);
			p.Q.Add("GRPNG", GRPNG);
			return p.ToString();
		}
	}
}
