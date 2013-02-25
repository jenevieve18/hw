//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using HW.Core.Repositories.Sql;
using HWgrp;
using NUnit.Framework;

namespace HW.Tests.Views
{
	[TestFixture]
	public class StatsTests
	{
		stats p;
		SqlLanguageRepository langRepository;
		SqlSponsorRepository sponsorRepository;
		SqlDepartmentRepository departmentRepository;
		SqlReportRepository reportRepository;
		
		[SetUp]
		public void Setup()
		{
			langRepository = new SqlLanguageRepository();
			sponsorRepository = new SqlSponsorRepository();
			departmentRepository = new SqlDepartmentRepository();
			reportRepository = new SqlReportRepository();
			AppContext.SetRepositoryFactory(new SqlRepositoryFactory());
			p = new stats();
		}
		
		[Test]
		public void TestProperties()
		{
			int sponsorID = 0;
//			p.Languages = langRepository.FindBySponsor(sponsorID);
			int selectedLangID = 0;
			p.ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);
//			p.BackgroundQuestions = sponsorRepository.FindBySponsor(sponsorID);
			int sponsorAdminID = 0;
			p.Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
		}
		
		[Test]
		public void a()
		{
			int sponsorID = 1;
			int selectedLangID = 1;
			var x = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);
		}
		
		[Test]
		public void b()
		{
			int selectedProjectRoundUnitID = 0;
			int selectedLangID = 1;
			var x = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
		}
	}
}
