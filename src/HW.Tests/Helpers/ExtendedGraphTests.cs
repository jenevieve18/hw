
using System;
using System.Windows.Forms;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;
using HW.Core.Services;
using NUnit.Framework;
using System.Drawing;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class ExtendedGraphTests
	{
		[Test]
		public void TestMethod()
		{
			ReportService service = new ReportService(
				new SqlAnswerRepository(),
				new SqlReportRepository(),
				new SqlProjectRepository(),
				new SqlOptionRepository(),
				new SqlDepartmentRepository(),
				new SqlQuestionRepository(),
				new SqlIndexRepository(),
				new SqlSponsorRepository()
			);
			
			ExtendedGraph g = null;
			
			Sponsor s = service.ReadSponsor(83);
			ReportPart r = service.ReadReportPart(14, 1);
			
			var f = service.GetGraphFactory(false);
			g = f.CreateGraph(null, r, 1, 2643, 2011, 2014, 7, true, 0, 550, 440, "#efefef", 2, 514, 83, "0,923,925,927,928,929,930", null, 0, s.MinUserCountToDisclose);
			
			var v = new Form();
			var p = new PictureBox();
			p.Image = (Image)g.objBitmap;
			p.Dock = DockStyle.Fill;
			v.Controls.Add(p);
			v.ShowDialog();
		}
	}
}
