using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories;

namespace HW.Core.Services
{
	public class ReportService2
	{
		IReportRepository reportRepo;
		IReportPartRepository reportPartRepo;
		IReportPartComponentRepository reportPartComponentRepo;
		IIndexRepository indexRepo;
		IQuestionRepository questionRepo;
		
		public ReportService2(IReportRepository reportRepo,
		                      IReportPartRepository reportPartRepo,
		                      IReportPartComponentRepository reportPartComponentRepo,
		                      IQuestionRepository questionRepo,
		                      IIndexRepository indexRepo)
		{
			this.reportRepo = reportRepo;
			this.reportPartRepo = reportPartRepo;
			this.reportPartComponentRepo = reportPartComponentRepo;
			this.questionRepo = questionRepo;
			this.indexRepo = indexRepo;
		}
		
		public Report ReadReport(int reportID)
		{
			var r = reportRepo.Read(reportID);
			r.Parts = reportPartRepo.FindByReport(reportID);
			foreach (var rp in r.Parts) {
				rp.Question = questionRepo.Read(rp.QuestionID);
				rp.Components = reportPartComponentRepo.FindByReportPart(rp.ReportPartID);
				foreach (var rpc in rp.Components) {
					rpc.Index = indexRepo.Read(rpc.IdxID);
				}
			}
			return r;
		}
	}
}
