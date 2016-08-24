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
		SqlReportRepository rr = new SqlReportRepository();
		SqlReportPartRepository rpr = new SqlReportPartRepository();
		SqlQuestionRepository qr = new SqlQuestionRepository();
		
		public ReportService()
		{
		}

        public IList<Report> FindAllReports()
        {
            return rr.FindAll();
        }
		
		public Report ReadReport(int reportID)
		{
			var r = rr.Read(reportID);
			r.Parts = rpr.FindByReport(reportID);
			foreach (var p in r.Parts) {
				p.Question = qr.Read(p.QuestionID);
			}
			return r;
		}
	}
}
