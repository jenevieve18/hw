using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Util.Exporters;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Util.Graphs
{
	public class UserLevelGraphFactory : BaseGraphFactory
	{
		SqlAnswerRepository answerRepo;
		
		SqlReportRepository reportRepo;
		
		public UserLevelGraphFactory(SqlAnswerRepository answerRepo, SqlReportRepository reportRepo)
		{
			this.answerRepo = answerRepo;
			this.reportRepo = reportRepo;
		}
		
		public ExtendedGraph GetWeightedQuestionOptionGraph(int width, int height, string bg, int cx, ReportPart reportPart, int langID, DateTime dateFrom, DateTime dateTo)
		{
			ExtendedGraph g = new ExtendedGraph(width, height, bg);
			g.setMinMax(0f, 100f);
			
			g.computeSteping(cx);
			g.drawOutlines(11);

			int bx = 0;
			foreach (ReportPartComponent c in reportRepo.FindComponentsByPartAndLanguage2(reportPart.Id, langID)) {
				if (bx == 0) {
					g.drawAxisExpl(c.WeightedQuestionOption.Languages[0].Question, bx + 4, false, true);
					g.drawAxis(false);
				} else {
					g.drawAxisExpl(c.WeightedQuestionOption.Languages[0].Question, bx + 4, true, true);
					g.drawAxis(true);
				}
				float lastVal = -1f;
				int lastCX = 0;
				cx = 0;
				foreach (Answer aa in answerRepo.FindByQuestionAndOptionWithYearSpan(c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month)) {
					if (bx == 0) {
						g.drawBottomString(aa.SomeString, cx);
					}
					float newVal = aa.Average;
					if (lastVal != -1f && newVal != -1f) {
						g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
						lastCX = cx;
					}
					cx++;
					lastVal = newVal;
				}
				bx++;
			}
			return g;
		}
		
		public ExtendedGraph GetNineGraph(int width, int height, string bg, int cx, ReportPart reportPart, int langID, int answerID)
		{
			ExtendedGraph g = new ExtendedGraph(width, height, bg);
			g.setMinMax(0f, 100f);
			
			g.computeSteping(cx + 2);
			g.drawOutlines(11);
			g.drawAxis();

			cx = 0;

			bool hasReference = false;

			foreach (ReportPartComponent c in reportRepo.FindComponentsByPartAndLanguage2(reportPart.Id, langID)) {
//				a = answerRepo.ReadByQuestionAndOption(answerID, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id);
				var a = answerRepo.ReadByQuestionAndOption(answerID, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id);
				if (a != null) {
//					int color = c.WeightedQuestionOption.GetColor(a.Values[0].ValueInt);
//					g.drawBar(color, ++cx, a.Values[0].ValueInt);
					int color = c.WeightedQuestionOption.GetColor((int)a.Values[0].ValueDouble);
					g.drawBar(color, ++cx, (int)a.Values[0].ValueDouble);
					if (c.WeightedQuestionOption.TargetVal != 0) {
						hasReference = true;
						g.drawReference(cx, c.WeightedQuestionOption.TargetVal);
					}
					g.drawBottomString(c.WeightedQuestionOption.Languages[0].Question, cx, true);
				}
			}

//			g.drawAxisExpl("poäng", 0, false, false);

			if (hasReference) {
				g.drawReference(450, 25, " = riktvärde");
			}

			g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
			g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
			g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
			return g;
		}
		
		public ExtendedGraph CreateGraph(string key, ReportPart reportPart, int langID, int projectRoundUnitID, DateTime dateFrom, DateTime dateTo, int GB, bool hasGrouping, int plot, int width, int height, string bg, int GRPNG, int sponsorAdminID, int sponsorID, string departmentIDs, object disabled, int point, int sponsorMinUserCountToDisclose)
		{
			int cx = reportPart.Components.Capacity;
			int answerID = 0;
			int projectRoundUserID = 0;
			Answer a = answerRepo.ReadByKey(key);
			if (a != null) {
				answerID = a.Id;
				if (langID == 0) {
					langID = a.Language.Id;
				}
				projectRoundUserID = a.ProjectRoundUser.Id;
			}
			LanguageFactory.SetCurrentCulture(langID);

//			ExtendedGraph g = new ExtendedGraph(width, height, bg);
//			g.setMinMax(0f, 100f);
			
			ExtendedGraph g = null;

			if (reportPart.Type == ReportPartType.WeightedQuestionOption) {
				int dx = answerRepo.CountByProject(projectRoundUserID, dateFrom.Year, dateTo.Year, dateFrom.Month, dateTo.Month);
				if (dx == 1) {
					reportPart.Type = 9;
				} else {
					cx = dx;
				}
			}
			
			if (reportPart.Type == ReportPartType.WeightedQuestionOption) {
				g = GetWeightedQuestionOptionGraph(width, height, bg, cx, reportPart, langID, dateFrom, dateTo);
			} else if (reportPart.Type == ReportPartType.Nine) {
				g = GetNineGraph(width, height, bg, cx, reportPart, langID, answerID);
			}
			return g;
		}

		public override void CreateGraphForExcelWriter(ReportPart p, int langID, ProjectRoundUnit projectRoundUnit, DateTime dateFrom, DateTime dateTo, int gb, bool hasGrouping, int plot, int grpng, SponsorAdmin sponsorAdmin, Sponsor sponsor, string departmentIDs, ExcelWriter w, ref int i)
		{
			throw new NotImplementedException();
		}
	}
}
