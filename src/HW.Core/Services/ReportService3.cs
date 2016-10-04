// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using System.Collections.Generic;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ReportService3
	{
		SqlReportRepository reportRepo = new SqlReportRepository();
		SqlReportPartRepository reportPartRepo = new SqlReportPartRepository();
		SqlReportPartLangRepository reportPartLangRepo = new SqlReportPartLangRepository();
		SqlReportPartComponentRepository reportPartComponentRepo = new SqlReportPartComponentRepository();
		
		SqlWeightedQuestionOptionRepository weightedQuestionOptionRepo = new SqlWeightedQuestionOptionRepository();
		
		SqlIndexRepository indexRepo = new SqlIndexRepository();
		
		public ReportService3()
		{
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
	}
}
