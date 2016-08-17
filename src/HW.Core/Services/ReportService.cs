using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ReportService
	{
		SqlAnswerRepository answerRepository;
		SqlReportRepository reportRepository;
		SqlProjectRepository projectRepository;
		SqlOptionRepository optionRepository;
		SqlDepartmentRepository departmentRepository;
		SqlQuestionRepository questionRepository;
		SqlIndexRepository indexRepository;
		SqlSponsorRepository sponsorRepository;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		Hashtable res = new Hashtable();
		Hashtable cnt = new Hashtable();
		
		public ReportService(SqlAnswerRepository answerRepository, SqlReportRepository reportRepository, SqlProjectRepository projectRepository, SqlOptionRepository optionRepository, SqlDepartmentRepository departmentRepository, SqlQuestionRepository questionRepository, SqlIndexRepository indexRepository, SqlSponsorRepository sponsorRepository)
		{
			this.answerRepository = answerRepository;
			this.reportRepository = reportRepository;
			this.projectRepository = projectRepository;
			this.optionRepository = optionRepository;
			this.departmentRepository = departmentRepository;
			this.questionRepository = questionRepository;
			this.indexRepository = indexRepository;
			this.sponsorRepository = sponsorRepository;
		}
		
		public IList<IReportPart> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			return reportRepository.FindByProjectAndLanguage(projectRoundID, langID);
		}
		
		public ISponsor ReadSponsor(int sponsorID)
		{
			return sponsorRepository.ReadSponsor(sponsorID);
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
		
//		void GetIdxVal(int idx, string sortString, int langID, int fy, int ty, int fm, int tm)
//		{
//			foreach (Index i in indexRepository.FindByLanguage(idx, langID, fy, ty, sortString, fm, tm)) {
//				lastCount = i.CountDX;
//				lastVal = i.AverageAX;
//				lastDesc = i.Languages[0].IndexName;
//				if (!res.Contains(i.Id)) {
//					res.Add(i.Id, lastVal);
//				}
//				if (!cnt.Contains(i.Id)) {
//					cnt.Add(i.Id, lastCount);
//				}
//			}
//		}
//
//		void GetOtherIdxVal(int idx, string sortString, int langID, int fy, int ty, int fm, int tm)
//		{
//			float tot = 0;
//			int max = 0;
//			int minCnt = Int32.MaxValue;
//			Index index = indexRepository.ReadByIdAndLanguage(idx, langID);
//			if (index != null) {
//				lastDesc = index.Languages[0].IndexName;
//				foreach (IndexPart p in index.Parts) {
//					max += 100 * p.Multiple;
//					if (res.Contains(p.OtherIndex.Id)) {
//						tot += (float)res[p.OtherIndex.Id] * p.Multiple;
//						minCnt = Math.Min((int)cnt[p.OtherIndex.Id], minCnt);
//					} else {
//						GetIdxVal(p.OtherIndex.Id, sortString, langID, fy, ty, fm, tm);
//						tot += lastVal * p.Multiple;
//						minCnt = Math.Min(lastCount, minCnt);
//					}
//				}
//			}
//			lastVal = 100 * tot / max;
//			lastCount = minCnt;
//		}
//		
//		public string GetReportImageUrl(int GB, int rpid, int rplid, int fy, int ty, int langID, int PRUID, int GRPNG, int SPONS, int SID, string GID, string plot, string path, int distribution)
//		{
//			P p = new P("reportImage.aspx");
//			p.Q.Add("LangID", langID);
//			p.Q.Add("FY", fy);
//			p.Q.Add("TY", ty);
//			p.Q.Add("SAID", SPONS);
//			p.Q.Add("SID", SID);
//			p.Q.Add("DIST", distribution);
//			p.Q.Add("GB", GB);
//			p.Q.Add("RPID", rpid);
//			p.Q.Add("RPLID", rplid);
//			p.Q.Add("PRUID", PRUID);
//			p.Q.Add("GRPNG", GRPNG);
//			return p.ToString();
//		}
	}
}
