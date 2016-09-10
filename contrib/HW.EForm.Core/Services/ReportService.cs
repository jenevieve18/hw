// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.EForm.Core.Models;
using HW.EForm.Core.Repositories;

namespace HW.EForm.Core.Services
{
	public class ReportService
	{
		SqlReportRepository reportRepo = new SqlReportRepository();
		SqlReportLangRepository reportLangRepo = new SqlReportLangRepository();
		SqlReportPartRepository reportPartRepo = new SqlReportPartRepository();
		
		SqlQuestionRepository questionRepo = new SqlQuestionRepository();
		
		public ReportService()
		{
		}

        public IList<Report> FindAllReports()
        {
            return reportRepo.FindAll();
        }
		
		public Report ReadReport(int reportID)
		{
			var r = reportRepo.Read(reportID);
			r.Languages = reportLangRepo.FindByReport(reportID);
			r.Parts = reportPartRepo.FindByReport(reportID);
			foreach (var rp in r.Parts) {
				rp.Question = questionRepo.Read(rp.QuestionID);
			}
			return r;
		}
	}
}
