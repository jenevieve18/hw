/*
 * Created by SharpDevelop.
 * User: Ian
 * Date: 7/13/2016
 * Time: 7:07 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using HW.Core.Models;
using NUnit.Framework;

namespace HW.Core.Tests.Helpers
{
	[TestFixture]
	public class ExtendedGraphTests2
	{
		ExtendedGraph g;
		Form f;
		PictureBox p;
		
		ReportService service = new ReportService(
			new SqlAnswerRepository(),
			new SqlReportRepository(),
			new SqlProjectRepository(),
			new SqlOptionRepository(),
			new SqlDepartmentRepository(),
			new SqlQuestionRepository(),
			new SqlIndexRepository(),
			new SqlSponsorRepository(),
			new SqlSponsorAdminRepository()
		);
		
		[SetUp]
		public void Setup()
		{
			g = new ExtendedGraph(800, 600, "#FFFFFF");
			
			f = new Form();
			f.Size = new Size(1000, 650);
			f.KeyDown += delegate(object sender, KeyEventArgs e) {
				if (e.KeyCode == Keys.Escape) {
					f.Close();
				}
			};
			
			p = new PictureBox();
			p.Dock = DockStyle.Fill;
		}
		
		[TearDown]
		public void Teardown()
		{
			p.Image = g.objBitmap;
			f.Controls.Add(p);
			f.ShowDialog();
		}
		
		[Test]
		public void TestBoxPlot()
		{
			g.SetMinMax(0, 100);
			g.DrawBackgroundFromIndex(new BaseIndex(0, 40, 60, 101, 101));
			g.DrawComputingSteps(null, 24);
			
			g.DrawBottomString(52313, 52334, 7);
//			g.SetXAxis(GroupStatsGraphFactory.GetBottomStrings(52313, 52334, 7).ToArray());
			
			g.Explanations.Add(
				new Explanation {
					Description = "mean value",
					Color = 0,
					Right = false,
					Box = false,
					HasAxis = false
				}
			);
			
			var r =  new Random();
			
			int breaker = 3;
			int itemWidth = 240;
			
			for (int i = 0; i < 5; i++) {
				var s = new Series {
					Description = "Series " + i.ToString(),
					Color = i + 4,
					X = 130 + (int)((i % breaker) * itemWidth),
					Y = 20 + (int)Math.Floor((double)i / breaker) * 15
				};
				
				for (int j = 0; j < 22; j++) {
					
					var l = new System.Collections.Generic.List<double>();
					for (int k = 0; k < 10; k++) {
						l.Add((double)r.Next(1, 100));
					}
					
					s.Points.Add(new PointV { X = j + 1, Values = new HWList(l) });
				}
				g.Series.Add(s);
			}
			
//			g.Type = new LineGraphType(0, 2);
			g.Type = new BoxPlotGraphType();
			g.Draw();
		}
		
		[Test]
		public void TestModelSponsorProject()
		{
			var p = new SponsorProject();
//			p.ProjectName = "Project1";
			p.Subject = "Project1";
			
			var m = new Measure();
			m.Components.Add(new MeasureComponent());
			m.Components.Add(new MeasureComponent());
			m.Components.Add(new MeasureComponent());
			
			p.Measures.Add(new SponsorProjectMeasure { Measure = m });
			p.Measures.Add(new SponsorProjectMeasure { Measure = m });
		}
		
		[Test]
		public void TestUnitMeasureWithUnits()
		{
			IAdmin s = service.ReadSponsor(3);
//			SponsorProject r = new SqlMeasureRepository().ReadSponsorProject(2);
			SponsorProject r = new SqlSponsorProjectRepository().Read(2);
			
			var p = new {
				langID = 2,
//				yearFrom = 2016,
//				yearTo = 2016,
				dateFrom = new DateTime(2016, 4, 1),
				dateTo = new DateTime(2016, 4, 1),
				GB = GroupBy.TwoWeeksStartWithEven,
				grouping = Grouping.UsersOnUnit,
//				sponsorAdminID = 10,
//				sponsorID = 3,
				sponsorAdmin = new SqlSponsorAdminRepository().Read(10),
				sponsor = s,
//				monthFrom = 4,
//				monthTo = 4,
//				sponsorMinUserCountToDisclose = 2,
			};
			
			var exporter = new ForStepCount(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository(), new SqlMeasureRepository());
			
			g = exporter.CreateGraph(
				r,
				p.langID,
				p.dateFrom,
				p.dateTo,
				p.GB,
				true,
				PlotType.Line,
				p.grouping,
				p.sponsorAdmin,
				p.sponsor as Sponsor,
				"0,6",
				null,
				0
			);
			g.Draw();
		}
		
		[Test]
		public void TestUnitMeasureWithSubUnits()
		{
			IAdmin s = service.ReadSponsor(3);
//			SponsorProject r = new SqlMeasureRepository().ReadSponsorProject(2);
			SponsorProject r = new SqlSponsorProjectRepository().Read(2);
			
			var p = new {
				langID = 2,
//				yearFrom = 2016,
//				yearTo = 2016,
				dateFrom = new DateTime(2016, 4, 1),
				dateTo = new DateTime(2016, 4, 1),
				GB = GroupBy.TwoWeeksStartWithEven,
				grouping = Grouping.UsersOnUnitAndSubUnits,
//				sponsorAdminID = 10,
//				sponsorID = 3,
				sponsorAdmin = new SqlSponsorAdminRepository().Read(10),
				sponsor = s as Sponsor,
//				monthFrom = 4,
//				monthTo = 4,
//				sponsorMinUserCountToDisclose = 2,
			};
			
			var exporter = new ForStepCount(new SqlAnswerRepository(), new SqlReportRepository(), new SqlProjectRepository(), new SqlOptionRepository(), new SqlIndexRepository(), new SqlQuestionRepository(), new SqlDepartmentRepository(), new SqlMeasureRepository());
			
			g = exporter.CreateGraph(
				r,
				p.langID,
				p.dateFrom,
				p.dateTo,
				p.GB,
				true,
				PlotType.Line,
				p.grouping,
				p.sponsorAdmin,
				p.sponsor,
				"0,7,8",
				null,
				0
			);
			g.Draw();
		}
		
//		[Test]
//		public void a()
//		{
//			IAdmin s = service.ReadSponsor(83);
//			ReportPart r = service.ReadReportPart(14, 2);
//			
//			var exporter = new GroupStatsGraphFactory(
//				new SqlAnswerRepository(),
//				new SqlReportRepository(),
//				new SqlProjectRepository(),
//				new SqlOptionRepository(),
//				new SqlIndexRepository(),
//				new SqlQuestionRepository(),
//				new SqlDepartmentRepository()
//			);
//			
//			var p = new {
//				dateFrom = new DateTime(2011, 1, 1),
//				dateTo = new DateTime(2012, 1, 1),
//				groupBy = GroupBy.TwoWeeksStartWithEven,
//				hasGrouping = true,
//				grouping = Grouping.UsersOnUnit,
//				sponsorAdminID = 791,
//				sponsorID = 83,
//				projectRoundUnitID = 2643,
//				sponsorMinUserCountToDisclose = 2,
//				point = 0,
//				langID = 2,
//			};
//			
//			g = exporter.CreateGraph(
//				null,
//				r,
//				p.langID,
//				p.projectRoundUnitID,
//				p.dateFrom,
//				p.dateTo,
//				p.groupBy,
//				p.hasGrouping,
//				PlotType.LineSD,
//				0,
//				0,
//				"#ffffff",
//				p.grouping,
//				p.sponsorAdminID,
//				p.sponsorID,
//				"0,925",
//				null,
//				p.point,
//				p.sponsorMinUserCountToDisclose
//			);
//			g.Draw();
//		}
//		
//		[Test]
//		public void b()
//		{
//			IAdmin s = service.ReadSponsor(83);
//			ReportPart r = service.ReadReportPart(14, 2);
//			
//			var exporter = new GroupStatsGraphFactory(
//				new SqlAnswerRepository(),
//				new SqlReportRepository(),
//				new SqlProjectRepository(),
//				new SqlOptionRepository(),
//				new SqlIndexRepository(),
//				new SqlQuestionRepository(),
//				new SqlDepartmentRepository()
//			);
//			
//			var p = new {
//				dateFrom = new DateTime(2011, 1, 1),
//				dateTo = new DateTime(2012, 1, 1),
//				groupBy = GroupBy.TwoWeeksStartWithEven,
//				hasGrouping = true,
//				grouping = Grouping.UsersOnUnitAndSubUnits,
//				sponsorAdminID = 791,
//				sponsorID = 83,
//				projectRoundUnitID = 2643,
//				sponsorMinUserCountToDisclose = 2,
//				point = 0,
//				langID = 2,
//			};
//			
//			g = exporter.CreateGraph(
//				null,
//				r,
//				p.langID,
//				p.projectRoundUnitID,
//				p.dateFrom,
//				p.dateTo,
//				p.groupBy,
//				p.hasGrouping,
//				PlotType.LineSD,
//				0,
//				0,
//				"#ffffff",
//				p.grouping,
//				p.sponsorAdminID,
//				p.sponsorID,
//				"0,925",
//				null,
//				p.point,
//				p.sponsorMinUserCountToDisclose
//			);
//			g.Draw();
//		}
	}
}
