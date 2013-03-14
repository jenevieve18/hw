//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core.Repositories;

namespace HW.Core.Services
{
	public class ReportService
	{
		IReportRepository reportRepository;
		ILanguageRepository langRepository;
		IProjectRepository projRepository;
		ISponsorRepository sponsorRepository;
		IDepartmentRepository departmentRepository;
		
		public ReportService(IReportRepository reportRepository)
		{
			this.reportRepository = reportRepository;
		}
	}
}
