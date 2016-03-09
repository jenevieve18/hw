using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using HW.Core.Models;
using HW.Core.Repositories;
using HW.Core.Repositories.Sql;

namespace HW.Core.Helpers
{
	public interface IGraphFactory
	{
		ExtendedGraph CreateGraph(string key, ReportPart p, int langID, int pruid, int fy, int ty, int GB, bool hasGrouping, int plot, int width, int height, string bg, int grpng, int sponsorAdminID, int sid, string gid, object disabled, int point, int sponsorMinUserCountToDisclose, int fm, int tm);

//		string CreateGraph2(string key, ReportPart p, int langID, int pruid, int fy, int ty, int gb, bool hasGrouping, int plot, int grpng, int sponsorAdminID, int sid, string gid, object disabled, int sponsorMinUserCountToDisclose, int fm, int tm);
		
//		void CreateGraph3(string key, ReportPart p, int langID, int pruid, int fy, int ty, int gb, bool hasGrouping, int plot, int grpng, int sponsorAdminID, int sid, string gid, object disabled, ExcelWriter w, ref int i, int sponsorMinUserCountToDisclose, int fm, int tm);
		void CreateGraph3(ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, int plot, int GRPNG, int sponsorAdminID, int SID, string GID, ExcelWriter writer, ref int index, int sponsorMinUserCountToDisclose, int fm, int tm);
		
		event EventHandler<MergeEventArgs> ForMerge;
	}
	
	public class UserLevelGraphFactory : IGraphFactory
	{
		SqlAnswerRepository answerRepository;
		SqlReportRepository reportRepository;
		
		public UserLevelGraphFactory(SqlAnswerRepository answerRepository, SqlReportRepository reportRepository)
		{
			this.answerRepository = answerRepository;
			this.reportRepository = reportRepository;
		}
		
		public ExtendedGraph CreateGraph(string key, ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, int plot, int width, int height, string bg, int GRPNG, int sponsorAdminID, int SID, string GID, object disabled, int point, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			int cx = p.Components.Capacity;
			int answerID = 0;
			int projectRoundUserID = 0;
			Answer a = answerRepository.ReadByKey(key);
			if (a != null) {
				answerID = a.Id;
				if (langID == 0) {
					langID = a.Language.Id;
				}
				projectRoundUserID = a.ProjectRoundUser.Id;
			}
			LanguageFactory.SetCurrentCulture(langID);

			ExtendedGraph g = new ExtendedGraph(width, height, bg);
			g.setMinMax(0f, 100f);

			if (p.Type == 8) {
				int dx = answerRepository.CountByProject(projectRoundUserID, fy, ty, fm, tm);
				if (dx == 1) {
					p.Type = 9;
				} else {
					cx = dx;
				}
			}
			if (p.Type == 8) {
				g.computeSteping(cx);
				g.drawOutlines(11);

				int bx = 0;
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
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
					foreach (Answer aa in answerRepository.FindByQuestionAndOptionWithYearSpan(c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, fm, tm)) {
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
			} else if (p.Type == 9) {
				g.computeSteping(cx + 2);
				g.drawOutlines(11);
				g.drawAxis();

				cx = 0;

				bool hasReference = false;

				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
					a = answerRepository.ReadByQuestionAndOption(answerID, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id);
					if (a != null) {
						int color = c.WeightedQuestionOption.GetColor(a.Values[0].ValueInt);
						g.drawBar(color, ++cx, a.Values[0].ValueInt);
						if (c.WeightedQuestionOption.TargetValue != 0) {
							hasReference = true;
							g.drawReference(cx, c.WeightedQuestionOption.TargetValue);
						}
						g.drawBottomString(c.WeightedQuestionOption.Languages[0].Question, cx, true);
					}
				}

				// g.drawAxisExpl("poäng", 0, false, false);

				if (hasReference) {
					g.drawReference(450, 25, " = riktvärde");
				}

				g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
				g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
				g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
			}
			return g;
		}
		
		/*
		public string CreateGraph2(string key, ReportPart p, int langID, int PRUID, int fy, int ty, int GB, bool hasGrouping, int plot, int GRPNG, int sponsorAdminID, int SID, string GID, object disabled, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			int cx = p.Components.Capacity;
			int answerID = 0;
			int projectRoundUserID = 0;
			Answer a = answerRepository.ReadByKey(key);
			if (a != null) {
				answerID = a.Id;
				if (langID == 0) {
					langID = a.Language.Id;
				}
				projectRoundUserID = a.ProjectRoundUser.Id;
			}
			LanguageFactory.SetCurrentCulture(langID);

			StringBuilder s = new StringBuilder();
//			ExtendedGraph g = new ExtendedGraph(width, height, bg);
//			g.setMinMax(0f, 100f);

			if (p.Type == 8) {
				int dx = answerRepository.CountByProject(projectRoundUserID, fy, ty, fm, tm);
				if (dx == 1) {
					p.Type = 9;
				} else {
					cx = dx;
				}
			}
			if (p.Type == 8) {
//				g.computeSteping(cx);
//				g.drawOutlines(11);

				int bx = 0;
				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
					if (bx == 0) {
//						g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, false, true);
//						g.drawAxis(false);
					} else {
//						g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, true, true);
//						g.drawAxis(true);
					}
					float lastVal = -1f;
					int lastCX = 0;
					cx = 0;
					foreach (Answer aa in answerRepository.FindByQuestionAndOptionWithYearSpan(c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id, fy, ty, fm, tm)) {
						if (bx == 0) {
//							g.drawBottomString(aa.SomeString, cx);
						}
						float newVal = aa.Average;
						if (lastVal != -1f && newVal != -1f) {
//							g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
							lastCX = cx;
						}
						cx++;
						lastVal = newVal;
					}
					bx++;
				}
			} else if (p.Type == 9) {
//				g.computeSteping(cx + 2);
//				g.drawOutlines(11);
//				g.drawAxis();

				cx = 0;

				bool hasReference = false;

				foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(p.Id, langID)) {
					a = answerRepository.ReadByQuestionAndOption(answerID, c.WeightedQuestionOption.Question.Id, c.WeightedQuestionOption.Option.Id);
					if (a != null) {
						int color = c.WeightedQuestionOption.GetColor(a.Values[0].ValueInt);
//						g.drawBar(color, ++cx, a.Values[0].ValueInt);
						if (c.WeightedQuestionOption.TargetValue != 0) {
							hasReference = true;
//							g.drawReference(cx, c.QuestionOption.TargetValue);
						}
//						g.drawBottomString(c.QuestionOption.Languages[0].Question, cx, true);
					}
				}

				if (hasReference) {
//					g.drawReference(450, 25, " = riktvärde");
				}

//				g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
//				g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
//				g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);
			}
//			return g;
			return s.ToString();
		}
		*/
		
//		public void CreateGraph3(string key, ReportPart p, int langID, int pruid, int fy, int ty, int gb, bool hasGrouping, int plot, int grpng, int spons, int sid, string gid, object disabled, ExcelWriter w, ref int i, int sponsorMinUserCountToDisclose, int fm, int tm)
		public void CreateGraph3(ReportPart p, int langID, int pruid, int fy, int ty, int gb, bool hasGrouping, int plot, int grpng, int spons, int sid, string gid, ExcelWriter w, ref int i, int sponsorMinUserCountToDisclose, int fm, int tm)
		{
			throw new NotImplementedException();
		}
		
		public event EventHandler<MergeEventArgs> ForMerge;
		
		protected virtual void OnForMerge(MergeEventArgs e)
		{
			if (ForMerge != null) {
				ForMerge(this, e);
			}
		}
	}
	
	public class MergeEventArgs : EventArgs
	{
		public int WeeksCount { get; set; }
	}
}
