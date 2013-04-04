//	<file>
//		<license></license>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HWgrp;
using NUnit.Framework;

namespace HW.Tests.Views
{
	[TestFixture]
	public class ReportImageTests
	{
		SqlAnswerRepository answerRepository;
		SqlReportRepository reportRepository;
		SqlIndexRepository indexRepository;
		reportImage p;
		
		[SetUp]
		public void Setup()
		{
			answerRepository = new SqlAnswerRepository();
			reportRepository = new SqlReportRepository();
			indexRepository = new SqlIndexRepository();
			AppContext.SetRepositoryFactory(new SqlRepositoryFactory());
			p = new reportImage();
		}
		
		[Test]
		public void TestSetReportPart()
		{
			ReportPart r = reportRepository.ReadReportPart(1, 1);
			ExtendedGraph g = new ExtendedGraph(100, 100, "#FFFFFF");
//			p.SetReportPart("", true, r, g, 1, 1, 2010, 2013, 1, true, true, "LinePlot");
		}
		
		[Test]
		public void TestType8LinePlot()
		{
			answerRepository.ReadByGroup("dbo.cf_year2WeekEven", 2012, 2013, "");
		}
		
		[Test]
		public void a()
		{
			var x = new SqlReportRepository().FindComponentsByPart(1);
		}
		
		[Test]
		public void a1()
		{
			var x = new SqlReportRepository().ReadComponentByPartAndLanguage(1, 1);
		}
		
		[Test]
		public void b()
		{
			var x = new SqlDepartmentRepository().FindBySponsorWithSponsorAdminIn(1, 1, "0");
		}
		
		[Test]
		public void c()
		{
			var x = new SqlReportRepository().ReadComponentByPartAndLanguage(0, 1);
		}
		
		[Test]
		public void d()
		{
			int rpid = 14;
			var x = reportRepository.ReadReportPart(rpid, 1);
		}
		
		[Test]
		public void e()
		{
			string s = "\r\nSELECT tmp.DT,\r\n\tAVG(tmp.V),\r\n\tCOUNT(tmp.V),\r\n\tSTDEV(tmp.V)\r\nFROM (\r\n\tSELECT dbo.cf_year2WeekEven(a.EndDT) AS DT, AVG(av.ValueInt) AS V\r\n\tFROM Answer a\r\n\t\r\n\tINNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID\r\n\tINNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = 911\r\n\tINNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID\r\n\tINNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, 8) = '00000039'\r\n\tINNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID\r\n\t\tAND av.QuestionID = 238\r\n\t\tAND av.OptionID = 55\r\n\tWHERE a.EndDT IS NOT NULL\r\n\tAND YEAR(a.EndDT) >= 2012\r\n\tAND YEAR(a.EndDT) <= 2013\r\n\tGROUP BY a.ProjectRoundUserID, dbo.cf_year2WeekEven(a.EndDT)\r\n) tmp\r\nGROUP BY tmp.DT\r\nORDER BY tmp.DT";
			Console.WriteLine(s);
		}
		
		[Test]
		public void f()
		{
			string j = "\r\n\tINNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID\r\n\tINNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = 911\r\n\tINNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID\r\n\tINNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString, 8) = '00000039'";
			var answers = answerRepository.FindByQuestionAndOptionJoinedAndGrouped2(j, "dbo.cf_year2WeekEven", 238, 55, 2011, 2013);
			foreach (var a in answers) {
				foreach (var v in a.Values) {
//					Console.Write(a.SomeInteger + "\t");
//					Console.WriteLine(v.ValueInt);
				}
				if (a.DT == 52324) {
					HWList l = a.GetIntValues();
					Console.WriteLine(l.ToString());
				}
			}
		}
	}
}
