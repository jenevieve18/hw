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
		SqlSponsorRepository sponsorRepo;
		SqlSponsorAdminRepository sponsorAdminRepo;
		
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		Hashtable res = new Hashtable();
		Hashtable cnt = new Hashtable();
		
		public ReportService(SqlAnswerRepository answerRepository,
		                     SqlReportRepository reportRepository,
		                     SqlProjectRepository projectRepository,
		                     SqlOptionRepository optionRepository,
		                     SqlDepartmentRepository departmentRepository,
		                     SqlQuestionRepository questionRepository,
		                     SqlIndexRepository indexRepository,
		                     SqlSponsorRepository sponsorRepo,
		                     SqlSponsorAdminRepository sponsorAdminRepo)
		{
			this.answerRepository = answerRepository;
			this.reportRepository = reportRepository;
			this.projectRepository = projectRepository;
			this.optionRepository = optionRepository;
			this.departmentRepository = departmentRepository;
			this.questionRepository = questionRepository;
			this.indexRepository = indexRepository;
			this.sponsorRepo = sponsorRepo;
			this.sponsorAdminRepo = sponsorAdminRepo;
		}
		
		public IList<IReportPart> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			return reportRepository.FindByProjectAndLanguage(projectRoundID, langID);
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
			return projectRepository.ReadRoundUnit(projectRoundUnitID);
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
	}
}
