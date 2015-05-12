using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IReportRepository : IBaseRepository<Report>
	{
		IList<ReportPartLanguage> FindByProjectAndLanguage2(int projectRoundUnitID, int langID, int departmentID);
		
		IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundUnitID, int langID);
	}
	
	public class ReportRepositoryStub : BaseRepositoryStub<Report>, IReportRepository
	{
		public IList<ReportPartLanguage> FindByProjectAndLanguage2(int projectRoundUnitID, int langID, int departmentID)
		{
			return new[] {
				new ReportPartLanguage {},
				new ReportPartLanguage {},
				new ReportPartLanguage {}
			};
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundUnitID, int langID)
		{
			return new[] {
				new ReportPartLanguage {},
				new ReportPartLanguage {},
				new ReportPartLanguage {}
			};
		}
	}
}
