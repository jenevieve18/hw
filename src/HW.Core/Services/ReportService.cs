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
		SqlReportRepository reportRepo = new SqlReportRepository();
		SqlReportPartRepository reportPartRepo = new SqlReportPartRepository();
		SqlReportPartLangRepository reportPartLangRepo =  new SqlReportPartLangRepository();
		SqlReportPartComponentRepository reportPartComponentRepo = new SqlReportPartComponentRepository();
		
		SqlAnswerRepository answerRepo = new SqlAnswerRepository();
		
		SqlProjectRepository projectRepo = new SqlProjectRepository();
		
		SqlOptionRepository optionRepo = new SqlOptionRepository();
		
		SqlDepartmentRepository departmentRepo = new SqlDepartmentRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		
		SqlWeightedQuestionOptionRepository weightedQuestionOptionRepo = new SqlWeightedQuestionOptionRepository();
		
		SqlIndexRepository indexRepo = new SqlIndexRepository();
		
		SqlSponsorRepository sponsorRepo = new SqlSponsorRepository();
		SqlSponsorAdminRepository sponsorAdminRepo = new SqlSponsorAdminRepository();
		
		public ReportService()
		{
		}
		
//		int lastCount = 0;
//		float lastVal = 0;
//		string lastDesc = "";
//		Hashtable res = new Hashtable();
//		Hashtable cnt = new Hashtable();
		
//		public ReportService(SqlAnswerRepository answerRepo,
//		                     SqlReportRepository reportRepo,
//		                     SqlProjectRepository projectRepo,
//		                     SqlOptionRepository optionRepo,
//		                     SqlDepartmentRepository departmentRepo,
//		                     SqlQuestionRepository questionRepo,
//		                     SqlIndexRepository indexRepo,
//		                     SqlSponsorRepository sponsorRepo,
//		                     SqlSponsorAdminRepository sponsorAdminRepo)
//		{
//			this.answerRepo = answerRepo;
//			this.reportRepo = reportRepo;
//			this.projectRepo = projectRepo;
//			this.optionRepo = optionRepo;
//			this.departmentRepo = departmentRepo;
//			this.questionRepo = questionRepo;
//			this.indexRepo = indexRepo;
//			this.sponsorRepo = sponsorRepo;
//			this.sponsorAdminRepo = sponsorAdminRepo;
//		}
		
		public IList<IReportPart> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
//			return reportRepo.FindByProjectAndLanguage(projectRoundID, langID);
			var reportPartLangs = reportRepo.FindByProjectAndLanguage(projectRoundID, langID);
			foreach (var rpl in reportPartLangs) {
				rpl.Languages = reportPartLangRepo.FindByReportPart(rpl.ReportPart.Id);
				rpl.Components = reportPartComponentRepo.FindByReportPart(rpl.ReportPart.Id) as List<ReportPartComponent>;
				foreach (var rpc in rpl.Components) {
					rpc.WeightedQuestionOption = weightedQuestionOptionRepo.Read(rpc.WeightedQuestionOptionID);
					rpc.Index = indexRepo.Read(rpc.IdxID);
				}
			}
			return reportPartLangs;
		}
		
		public ReportPart ReadReportPart(int reportPartID)
		{
			var rp = reportPartRepo.Read(reportPartID);
			if (rp != null) {
				rp.Languages = reportPartLangRepo.FindByReportPart(reportPartID);
				rp.Components = reportPartComponentRepo.FindByReportPart(reportPartID) as List<ReportPartComponent>;
				foreach (var rpc in rp.Components) {
					rpc.WeightedQuestionOption = weightedQuestionOptionRepo.Read(rpc.WeightedQuestionOptionID);
					rpc.Index = indexRepo.Read(rpc.IdxID);
				}
			}
			return rp;
		}
		
		public IAdmin ReadSponsor(int sponsorID)
		{
			return sponsorRepo.ReadSponsor(sponsorID);
		}
		
		public SponsorAdmin ReadSponsorAdmin(int sponsorAdminID)
		{
			return sponsorAdminRepo.Read(sponsorAdminID);
		}
		
		public ProjectRoundUnit ReadProjectRoundUnit(int projectRoundUnitID)
		{
			return projectRepo.ReadRoundUnit(projectRoundUnitID);
		}
		
		public ReportPart ReadReportPart(int reportPartID, int langID)
		{
//			return reportRepo.ReadReportPart(reportPartID, langID);
			var rp = reportRepo.ReadReportPart(reportPartID, langID);
			rp.Languages = reportPartLangRepo.FindByReportPart(reportPartID);
			rp.Components = reportPartComponentRepo.FindByReportPart(reportPartID) as List<ReportPartComponent>;
			foreach (var rpc in rp.Components) {
				rpc.WeightedQuestionOption = weightedQuestionOptionRepo.Read(rpc.WeightedQuestionOptionID);
				rpc.Index = indexRepo.Read(rpc.IdxID);
			}
			return rp;
		}
		
		public IGraphFactory GetGraphFactory(bool hasAnswerKey)
		{
			if (hasAnswerKey) {
				return new UserLevelGraphFactory(answerRepo, reportRepo);
			} else {
				return new GroupStatsGraphFactory(answerRepo, reportRepo, projectRepo, optionRepo, indexRepo, questionRepo, departmentRepo);
			}
		}
	}
}
