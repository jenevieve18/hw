using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using HW.Core;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;
using NUnit.Framework;

namespace HW.Tests.Helpers
{
	[TestFixture]
	public class ExtendedGraphTests
	{
		ExtendedGraph g;
		Form f;
		PictureBox p;
		SqlOptionRepository optionRepository;
		SqlAnswerRepository answerRepository;
		SqlReportRepository reportRepository;
		SqlProjectRepository projectRepository;
		SqlIndexRepository indexRepository;
		SqlDepartmentRepository departmentRepository;
		SqlQuestionRepository questionRepository;
		
//		float lastVal = 0;
//		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
//		int lastCount = 0;
		
		bool HasGrouping {
			get { return true; }
//			get { return false; }
		}
		
		bool IsStandardDeviation {
//			get { return HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1; }
			get { return false; }
		}
		
		[SetUp]
		public void Setup()
		{
			optionRepository = new SqlOptionRepository();
			answerRepository = new SqlAnswerRepository();
			reportRepository = new SqlReportRepository();
			projectRepository = new SqlProjectRepository();
			indexRepository = new SqlIndexRepository();
			departmentRepository = new SqlDepartmentRepository();
			questionRepository = new SqlQuestionRepository();
			
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
//			f.ShowDialog();
		}
		
		[Test]
		public void TestMinimumValue()
		{
			int steps = 10;
			
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			g.setMinMax(0f, 100f);
			g.computeSteping(steps + 1 + 2);
			g.drawOutlines(11);
			g.drawAxis(new object());
			
			g.drawLine(20, 0, (int)g.maxH + 20, 0, (int)g.maxH + 23, 1);
			g.drawLine(20, 0, (int)g.maxH + 23, -5, (int)g.maxH + 26, 1);
			g.drawLine(20, -5, (int)g.maxH + 26, 5, (int)g.maxH + 32, 1);
			g.drawLine(20, 5, (int)g.maxH + 32, 0, (int)g.maxH + 35, 1);
			g.drawLine(20, 0, (int)g.maxH + 35, 0, (int)g.maxH + 38, 1);
		}
		
		[Test]
		public void TestType1()
		{
			List<Bar> bars = new List<Bar>();
			Series s = new Series { Color = 5 };
			int tot = 10;
//			foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(1, 1)) {
			foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(1, 1)) {
				var x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, 2011, 2012, 1, 1, "", 3, 3);
				var b = new Bar {
//					Description = c.Text,
					Description = c.Component.CurrentLanguage.Text,
					Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
					Color = 5
				};
				bars.Add(b);
			}
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			g.DrawBars(new object(), 10, tot, bars);
			g.Draw();
		}
		
		[Test]
		public void TestType1b()
		{
			Series s = new Series { Color = 5 };
			int tot = 10;
//			foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(1, 1)) {
			foreach (OptionComponents c in optionRepository.FindComponentsByLanguage(1, 1)) {
				var x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, 2011, 2012, 1, 1, "", 3, 3);
				s.Points.Add(
					new PointV {
//						Description = c.Text,
						Description = c.Component.CurrentLanguage.Text,
						Y = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0))
					}
				);
			}
			g = new ExtendedGraph(895, 550, "#FFFFFF");
			g.Series.Add(s);
			g.Type = new BarGraphType();
			g.Draw();
		}
		
		[Test]
		public void TestType1InsufficientEvidence()
		{
			g = new ExtendedGraph(895, 50, "#FFFFFF");
			g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
		}
		
		[Test]
		public void TestType8()
		{
			int steps = 10;
			int[] xValues = new int[] { 10, 40, 50, 10, 3, 45, 22 };
			string[] explanations = new string[] { "Department 0", "Department 1", "Department 2", "Department 3", "Department 4", "Department 5", "Department 6" };
			
			g = new ExtendedGraph(895, 440, "#FFFFFF");
			g.setMinMax(0f, 100f);
//			g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(40)), "FFA8A8");
//			g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(40)), Math.Min(g.maxVal, (float)Convert.ToDouble(60)), "FFFEBE");
//			g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(60)), Math.Min(g.maxVal, (float)Convert.ToDouble(101)), "CCFFBB");
			g.drawBgFromString2(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(40)), "FFA8A8", "");
			g.drawBgFromString2(Math.Max(g.minVal, (float)Convert.ToDouble(40)), Math.Min(g.maxVal, (float)Convert.ToDouble(60)), "FFFEBE", "");
			g.drawBgFromString2(Math.Max(g.minVal, (float)Convert.ToDouble(60)), Math.Min(g.maxVal, (float)Convert.ToDouble(101)), "CCFFBB", "");
			
			g.computeSteping(steps + 1 + 2);
			g.drawOutlines(11);
			g.drawAxis(new object());
			
			g.drawAxis(false);
			g.drawAxisExpl(string.Format("% (n = {0})", 10), 5, false, false);
			
			int cx = 0;
			int breaker = 6;
			foreach (int x in xValues) {
				g.drawColorExplBox(explanations[cx], cx + 4, 130 + (int)((cx % breaker) * 120), 20 + (int)Math.Floor((double)cx / breaker) * 15);
				g.drawCircle(cx + 1, x, 4);
				g.drawStepLine(cx + 4, 1, 10, 1, 10, 2 + 0);
				cx++;
			}
		}
		
		[Test]
		public void TestType9()
		{
			g = new ExtendedGraph(895, 550, "#FFFFFF");
		}
		
		[Test]
		public void TestType8HasKey()
		{
			string[] explanations = new string[] { "Question 0", "Question 1" };
			string[] xLabels = new string[] { "X0", "X1", "X2", "X3", "X4" };
			int[] xValues = new int[] { 40, 50, 30, 60, 10 };
			int steps = 10;
			
			g = new ExtendedGraph(550, 440, "#FFFFFF");
			g.setMinMax(0f, 100f);
			g.computeSteping(steps);
			g.drawOutlines(11);
			
			int bx = 0;
			foreach (string s in explanations) {
				if (bx == 0) {
					g.drawAxisExpl(s, bx + 4, false, true);
					g.drawAxis(false);
				} else {
					g.drawAxisExpl(s, bx + 4, true, true);
					g.drawAxis(true);
				}
				float lastVal = -1f;
				int lastCX = 0;
				int cx = 0;
				foreach (int x in xValues) {
					if (bx == 0) {
						g.drawBottomString(xLabels[cx], cx);
					}
					float newVal = x;
					if (lastVal != -1f && newVal != -1f) {
						g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
						lastCX = cx;
					}
					cx++;
					lastVal = newVal;
				}
				bx++;
			}
		}
	}
}