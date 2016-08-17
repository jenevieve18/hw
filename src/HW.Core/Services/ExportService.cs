// <file>
//  <license></license>
//  <owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
// </file>

using System;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Core.Services
{
	public class ExportService
	{
		SqlReportRepository rr = new SqlReportRepository();
		
		public ExportService()
		{
		}
		
		public ReportPart ReadReportPart(int reportPartID, int langID)
		{
			var r = rr.ReadReportPart(reportPartID, langID);
			return r;
		}
	}
}
