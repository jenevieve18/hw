using System;
using System.Collections.Generic;
using HW.Core.Models;

namespace HW.Core.Repositories
{
	public interface IReportRepository : IBaseRepository<Report>
	{
		IList<ReportPartLang> FindByProjectAndLanguage2(int projectRoundUnitID, int langID, int departmentID);
		IList<ReportPartLang> FindByProjectAndLanguage(int projectRoundUnitID, int langID);
	}
	
	public interface IReportPartRepository : IBaseRepository<ReportPart>
	{
		IList<ReportPart> FindByReport(int reportID);
	}
	
	public interface IReportPartComponentRepository : IBaseRepository<ReportPartComponent>
	{
		List<ReportPartComponent> FindByReportPart(int reportPartID);
	}
	
	public class ReportRepositoryStub : BaseRepositoryStub<Report>, IReportRepository
	{
		public ReportRepositoryStub()
		{
			data.Add(new Report { ReportID = 1, Internal = "HME HW group report" });
		}
		
		public override Report Read(int id)
		{
			return data.Find(x => x.ReportID == id);
		}
		
		public IList<ReportPartLang> FindByProjectAndLanguage2(int projectRoundUnitID, int langID, int departmentID)
		{
			return new[] {
				new ReportPartLang {},
				new ReportPartLang {},
				new ReportPartLang {}
			};
		}
		
		public IList<ReportPartLang> FindByProjectAndLanguage(int projectRoundUnitID, int langID)
		{
			return new[] {
				new ReportPartLang {},
				new ReportPartLang {},
				new ReportPartLang {}
			};
		}
	}
	
	public class ReportPartRepositoryStub : BaseRepositoryStub<ReportPart>, IReportPartRepository
	{
		public ReportPartRepositoryStub()
		{
			data.Add(new ReportPart { ReportPartID = 1, ReportID = 1, Internal = "HME L" });
			data.Add(new ReportPart { ReportPartID = 2, ReportID = 1, Internal = "HME Motivation" });
			data.Add(new ReportPart { ReportPartID = 3, ReportID = 1, Internal = "m1a / completely disagree-completely agree", QuestionID = 1 });
		}
		
		public IList<ReportPart> FindByReport(int reportID)
		{
			return data.FindAll(x => x.ReportID == reportID);
		}
	}
	
	public class ReportPartComponentRepositoryStub : BaseRepositoryStub<ReportPartComponent>, IReportPartComponentRepository
	{
		public ReportPartComponentRepositoryStub()
		{
			data.Add(new ReportPartComponent { ReportPartID = 1, IdxID = 2 });
			data.Add(new ReportPartComponent { ReportPartID = 2, IdxID = 1 });
		}
		
		public List<ReportPartComponent> FindByReportPart(int reportPartID)
		{
			return data.FindAll(x => x.ReportPartID == reportPartID);
		}
	}
}
