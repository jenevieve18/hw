//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using HW.Core.Models;
using NHibernate;

namespace HW.Core.Repositories.NHibernate
{
	public class NHibernateReportRepository : BaseNHibernateRepository<Report>, IReportRepository
	{
		public NHibernateReportRepository()
		{
		}
		
		public void SaveOrUpdateReportPart(ReportPart part)
		{
			SaveOrUpdate<ReportPart>(part);
		}
		
		public void SaveOrUpdateReportPartLanguage(ReportPartLanguage partLang)
		{
			SaveOrUpdate<ReportPartLanguage>(partLang);
		}
		
		public ReportPart ReadReportPart(int reportPartID, int langID)
		{
			return NHibernateHelper.OpenSession().Load<ReportPart>(reportPartID);
		}
		
		public ReportPartLanguage ReadReportPartLanguage(int reportPartLangID)
		{
			return NHibernateHelper.OpenSession().Load<ReportPartLanguage>(reportPartLangID);
		}
		
		public ReportPartComponent ReadComponentByPartAndLanguage(int reportPartID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage(int reportPartID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartComponent> FindComponentsByPartAndLanguage2(int reportPartID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartComponent> FindComponentsByPart(int reportPartID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartComponent> FindComponents(int reportPartID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartLanguage> FindByProjectAndLanguage(int projectRoundID, int langID)
		{
			throw new NotImplementedException();
		}
		
		public IList<ReportPartLanguage> FindPartLanguagesByReport(int reportID)
		{
			throw new NotImplementedException();
		}
	}
}
