using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
	public partial class reportImage : System.Web.UI.Page
	{
		int lastCount = 0;
		float lastVal = 0;
		string lastDesc = "";
		System.Collections.Hashtable res = new System.Collections.Hashtable();
		System.Collections.Hashtable cnt = new System.Collections.Hashtable();
		
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
		IAnswerRepository answerRepository = AppContext.GetRepositoryFactory().CreateAnswerRepository();
		IProjectRepository projectRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		IOptionRepository optionRepository = AppContext.GetRepositoryFactory().CreateOptionRepository();
		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IIndexRepository indexRepository = AppContext.GetRepositoryFactory().CreateIndexRepository();
		IQuestionRepository questionRepository = AppContext.GetRepositoryFactory().CreateQuestionRepository();

		void getIdxVal(int idx, string sortString, int langID)
		{
//			SqlDataReader rs = Db.rs("SELECT " +
//			                         "AVG(tmp.AX), " +
//			                         "tmp.Idx, " +
//			                         "tmp.IdxID, " +
//			                         "COUNT(*) AS DX " +
//			                         "FROM " +
//			                         "(" +
//			                         "SELECT " +
//			                         "100*CAST(SUM(ipc.Val*ip.Multiple) AS REAL)/i.MaxVal AS AX, " +
//			                         "i.IdxID, " +
//			                         "il.Idx, " +
//			                         "i.CX, " +
//			                         "i.AllPartsRequired, " +
//			                         "COUNT(*) AS BX " +
//			                         "FROM Idx i " +
//			                         "INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = " + langID + " " +
//			                         "INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
//			                         "INNER JOIN IdxPartComponent ipc ON ip.IdxPartID = ipc.IdxPartID " +
//			                         "INNER JOIN AnswerValue av ON ip.QuestionID = av.QuestionID AND ip.OptionID = av.OptionID AND av.ValueInt = ipc.OptionComponentID " +
//			                         "INNER JOIN Answer a ON av.AnswerID = a.AnswerID " +
//			                         "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//			                         "WHERE a.EndDT IS NOT NULL AND i.IdxID = " + idx + " AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
//			                         (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//			                         (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//			                         "GROUP BY i.IdxID, a.AnswerID, i.MaxVal, il.Idx, i.CX, i.AllPartsRequired" +
//			                         ") tmp " +
//			                         "WHERE tmp.AllPartsRequired = 0 OR tmp.CX = tmp.BX " +
//			                         "GROUP BY tmp.IdxID, tmp.Idx", "eFormSqlConnection");
//			while (rs.Read())
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			foreach (Index i in indexRepository.FindByLanguage(idx, langID, fy, ty, sortString))
			{
//				lastCount = rs.GetInt32(3);
//				lastVal = (float)Convert.ToDouble(rs.GetValue(0));
//				lastDesc = rs.GetString(1);
				lastCount = i.CountDX;
				lastVal = i.AverageAX;
				lastDesc = i.Languages[0].IndexName;

//				if (!res.Contains(rs.GetInt32(2)))
//					res.Add(rs.GetInt32(2), lastVal);
//				if (!cnt.Contains(rs.GetInt32(2)))
//					cnt.Add(rs.GetInt32(2), lastCount);
				if (!res.Contains(i.Id))
					res.Add(i.Id, lastVal);
				if (!cnt.Contains(i.Id))
					cnt.Add(i.Id, lastCount);
			}
//			rs.Close();
		}

		void getOtherIdxVal(int idx, string sortString, int langID)
		{
			float tot = 0;
			//int div = 0;
			int max = 0;
			int minCnt = Int32.MaxValue;
//			SqlDataReader rs = Db.rs("SELECT " +
//			                         "ip.OtherIdxID, " +
//			                         "il.Idx, " +
//			                         "i.MaxVal, " +
//			                         "ip.Multiple " +
//			                         "FROM Idx i " +
//			                         "INNER JOIN IdxLang il ON i.IdxID = il.IdxID AND il.LangID = " + langID + " " +
//			                         "INNER JOIN IdxPart ip ON i.IdxID = ip.IdxID " +
//			                         "WHERE i.IdxID = " + idx, "eFormSqlConnection");
//			if (rs.Read())
			Index index = indexRepository.ReadByIdAndLanguage(idx, langID);
			if (index != null)
			{
//				lastDesc = rs.GetString(1);
				lastDesc = index.Languages[0].IndexName;
//				do
				foreach (IndexPart p in index.Parts)
				{
//					max += 100 * rs.GetInt32(3);
					max += 100 * p.Multiple;
//					if (res.Contains(rs.GetInt32(0)))
					if (res.Contains(p.OtherIndex.Id))
					{
//						tot += (float)res[rs.GetInt32(0)] * rs.GetInt32(3);
//						minCnt = Math.Min((int)cnt[rs.GetInt32(0)], minCnt);
						tot += (float)res[p.OtherIndex.Id] * p.Multiple;
						minCnt = Math.Min((int)cnt[p.OtherIndex.Id], minCnt);
					}
					else
					{
//						getIdxVal(rs.GetInt32(0), sortString, langID);
//						tot += lastVal * rs.GetInt32(3);
//						minCnt = Math.Min(lastCount, minCnt);
						getIdxVal(p.OtherIndex.Id, sortString, langID);
						tot += lastVal * p.Multiple;
						minCnt = Math.Min(lastCount, minCnt);
					}
					//div = rs.GetInt32(2);
				}
//				while (rs.Read());
			}
//			rs.Close();
			lastVal = 100 * tot / max;
			lastCount = minCnt;
		}
		
//		void DrawBottomString(int groupBy, Graph g, int i, int dx, string str)
//		{
//			switch (groupBy) {
//				case 1:
//					{
//						int d = i;
//						int w = d % 52;
//						if (w == 0)
//						{
//							w = 52;
//						}
//						string v = "v" + w + ", " + (d / 52) + str;
//						g.drawBottomString(v, dx, true);
//						break;
//					}
//				case 2:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0)
//						{
//							w = 52;
//						}
//						string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52 + str;
//						g.drawBottomString(v, dx, true);
//						break;
//					}
//				case 3:
//					{
//						int d = i;
//						int w = d % 12;
//						if (w == 0)
//						{
//							w = 12;
//						}
//						string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + str;
//						g.drawBottomString(v, dx, true);
//						break;
//					}
//				case 4:
//					{
//						int d = i * 3;
//						int w = d % 12;
//						if (w == 0)
//						{
//							w = 12;
//						}
//						string v = "Q" + (w / 3) + ", " + ((d - w) / 12) + str;
//						g.drawBottomString(v, dx, true);
//						break;
//					}
//				case 5:
//					{
//						int d = i * 6;
//						int w = d % 12;
//						if (w == 0)
//						{
//							w = 12;
//						}
//						string v = ((d - w) / 12) + "/" + (w / 6) + str;
//						g.drawBottomString(v, dx, true);
//						break;
//					}
//				case 6:
//					{
//						g.drawBottomString(i.ToString() + str, dx, true);
//						break;
//					}
//				case 7:
//					{
//						int d = i * 2;
//						int w = d % 52;
//						if (w == 0)
//						{
//							w = 52;
//						}
//						string v = "v" + w + "-" + ((w == 52 ? 0 : w) + 1) + ", " + ((d + 1) - (d % 52)) / 52 + str;
//						g.drawBottomString(v, dx, true);
//						break;
//					}
//			}
//		}
		
		bool HasAnswerKey {
			get { return HttpContext.Current.Request.QueryString["AK"] != null; }
		}
		
		bool HasWidth {
			get { return HttpContext.Current.Request.QueryString["W"] != null; }
		}
		
		bool HasHeight {
			get { return HttpContext.Current.Request.QueryString["H"] != null; }
		}
		
		bool HasBackground {
			get { return HttpContext.Current.Request.QueryString["BG"] != null; }
		}
		
		int Width {
			get {
				if (HasWidth) {
					return Convert.ToInt32(HttpContext.Current.Request.QueryString["W"]);
				} else {
					return 550;
				}
			}
		}
		
		int Height {
			get {
				if (HasHeight) {
					return Convert.ToInt32(HttpContext.Current.Request.QueryString["H"]);
				} else {
					return 440;
				}
			}
		}
		
		string Background {
			get {
				if (HasBackground) {
					return "#" + HttpContext.Current.Request.QueryString["BG"];
				} else {
					return "#EFEFEF";
				}
			}
		}
		
		bool HasGrouping {
//			get { return HttpContext.Current.Request.QueryString["GRPNG"] == null || HttpContext.Current.Request.QueryString["GRPNG"] == "0"; }
			get { return HttpContext.Current.Request.QueryString["GRPNG"] != null || HttpContext.Current.Request.QueryString["GRPNG"] != "0"; }
		}
		
		
		bool IsStandardDeviation {
			get { return HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			int cx = 0;
			int type = 0;
			int q = 0;
			int o = 0;
			int rac = 0;
			int pl = 0;
			ExtendedGraph g = null;

			int steps = 0;
			int GB = (HttpContext.Current.Request.QueryString["GB"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["GB"].ToString()) : 0);
			bool stdev = (HttpContext.Current.Request.QueryString["STDEV"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 : false);
			string groupBy = "";
			
			int fy = HttpContext.Current.Request.QueryString["FY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) : 0;
			int ty = HttpContext.Current.Request.QueryString["TY"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) : 0;
			
			int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);

//			SqlDataReader rs = Db.rs("SELECT " +
//			                         "rp.Type, " +
//			                         "(" +
//			                         "SELECT COUNT(*) " +
//			                         "FROM ReportPartComponent rpc " +
//			                         "WHERE rpc.ReportPartID = rp.ReportPartID" +
//			                         "), " +
//			                         "rp.QuestionID, " +
//			                         "rp.OptionID, " +
//			                         "rp.RequiredAnswerCount, " +
//			                         "rp.PartLevel " +
//			                         "FROM ReportPart rp " +
//			                         "WHERE rp.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"], "eFormSqlConnection");
//			if (rs.Read())
			int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
			ReportPart reportPart = reportRepository.ReadReportPart(rpid);
			if (reportPart != null)
			{
//				type = rs.GetInt32(0);
//				cx = rs.GetInt32(1);
//				q = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
//				o = (rs.IsDBNull(3) ? 0 : rs.GetInt32(3));
//				rac = (rs.IsDBNull(4) ? 0 : rs.GetInt32(4));
//				pl = (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
				type = reportPart.Type;
				cx = reportPart.Components.Count;
				q = reportPart.Question.Id;
				o = reportPart.Option.Id;
				rac = reportPart.RequiredAnswerCount;
				pl = reportPart.PartLevel;
			}
//			rs.Close();

//			if (HttpContext.Current.Request.QueryString["AK"] != null)
			if (HasAnswerKey)
			{
				#region User-level

				int answerID = 0;
//				int langID = (HttpContext.Current.Request.QueryString["LangID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["LangID"]) : 0);
				int projectRoundUserID = 0;
//				rs = Db.rs("SELECT " +
//				           "a.AnswerID, " +
//				           "dbo.cf_unitLangID(a.ProjectRoundUnitID), " +
//				           "a.ProjectRoundUserID " +
//				           "FROM Answer a " +
//				           "WHERE REPLACE(CONVERT(VARCHAR(255),a.AnswerKey),'-','') = '" + HttpContext.Current.Request.QueryString["AK"] + "'", "eFormSqlConnection");
				Answer a = answerRepository.ReadByKey(HttpContext.Current.Request.QueryString["AK"]);
//				if (rs.Read())
				if (a != null)
				{
//					answerID = rs.GetInt32(0);
					answerID = a.Id;
					if (langID == 0)
					{
//						langID = rs.GetInt32(1);
						langID = a.Language.Id;
					}
//					projectRoundUserID = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
					projectRoundUserID = a.ProjectRoundUser.Id;
				}
//				rs.Close();

//				switch (langID)
//				{
//						case 1:System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE"); break;
//						case 2: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); break;
//				}
				LanguageFactory.SetCurrentCulture(langID);

//				if (HttpContext.Current.Request.QueryString["W"] != null && HttpContext.Current.Request.QueryString["H"] != null && HttpContext.Current.Request.QueryString["BG"] != null)
//				if (HasWidth && HasHeight && HasBackground)
//				{
				////					g = new Graph(Convert.ToInt32(HttpContext.Current.Request.QueryString["W"]), Convert.ToInt32(HttpContext.Current.Request.QueryString["H"]), "#" + HttpContext.Current.Request.QueryString["BG"]);
//					g = new Graph(Width, Height, Background);
//				}
//				else
//				{
//					g = new Graph(550, 440, "#EFEFEF");
//				}
				g = new ExtendedGraph(Width, Height, Background);
				g.setMinMax(0f, 100f);

				if (type == 8)
				{
//					int dx = 0;
//					rs = Db.rs("SELECT COUNT(DISTINCT dbo.cf_yearMonthDay(a.EndDT)) " +
//					           "FROM Answer a " +
//					           "WHERE a.EndDT IS NOT NULL " +
//					           (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//					           (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//					           "AND a.ProjectRoundUserID = " + projectRoundUserID, "eFormSqlConnection");
					int dx = answerRepository.CountByProject(projectRoundUserID, Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]), Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]));
//					if (rs.Read())
//					{
//						dx = Convert.ToInt32(rs.GetValue(0));
					if (dx == 1)
					{
						type = 9;
					}
					else
					{
						cx = dx;
					}
//					}
//					rs.Close();
				}
				if (type == 8)
				{
					g.computeSteping(cx);
					g.drawOutlines(11);

					int bx = 0;
//					rs = Db.rs("SELECT " +
//					           "rpc.WeightedQuestionOptionID, " +	// 0
//					           "wqol.WeightedQuestionOption, " +
//					           "wqo.QuestionID, " +
//					           "wqo.OptionID " +
//					           "FROM ReportPartComponent rpc " +
//					           "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//					           "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
//					           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//					           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//					while (rs.Read() && bx <= 1)
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID))
					{
						if (bx == 0)
						{
//							g.drawAxisExpl(rs.GetString(1), bx + 4, false, true);
							g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, false, true);
							g.drawAxis(false);
						}
						else
						{
//							g.drawAxisExpl(rs.GetString(1), bx + 4, true, true);
							g.drawAxisExpl(c.QuestionOption.Languages[0].Question, bx + 4, true, true);
							g.drawAxis(true);
						}
						float lastVal = -1f;
						int lastCX = 0;
						cx = 0;
//						SqlDataReader rs2 = Db.rs("SELECT " +
//						                          "dbo.cf_yearMonthDay(a.EndDT), " +
//						                          "AVG(av.ValueInt) " +
//						                          "FROM Answer a " +
//						                          "LEFT OUTER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
//						                          "WHERE a.EndDT IS NOT NULL AND a.ProjectRoundUserID = " + projectRoundUserID + " " +
//						                          (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//						                          (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//						                          "GROUP BY dbo.cf_yearMonthDay(a.EndDT) " +
//						                          "ORDER BY dbo.cf_yearMonthDay(a.EndDT)", "eFormSqlConnection");
//						while (rs2.Read())
						foreach (Answer aa in answerRepository.FindByQuestionAndOptionWithYearSpan(c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty))
						{
							if (bx == 0)
							{
//								g.drawBottomString(rs2.GetString(0), cx);
								g.drawBottomString(aa.SomeString, cx);
							}
//							float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
							float newVal = aa.Average;
							if (lastVal != -1f && newVal != -1f)
							{
								g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal);
								lastCX = cx;
							}
							cx++;
							lastVal = newVal;
						}
//						rs2.Close();

						bx++;
					}
//					rs.Close();
				}
				else if (type == 9)
				{
					#region Bars
					g.computeSteping(cx + 2);
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;

					bool hasReference = false;

//					rs = Db.rs("SELECT " +
//					           "rpc.WeightedQuestionOptionID, " +	// 0
//					           "wqol.WeightedQuestionOption, " +
//					           "wqo.TargetVal, " +
//					           "wqo.YellowLow, " +
//					           "wqo.GreenLow, " +
//					           "wqo.GreenHigh, " +					// 5
//					           "wqo.YellowHigh, " +
//					           "wqo.QuestionID, " +
//					           "wqo.OptionID " +
//					           "FROM ReportPartComponent rpc " +
//					           "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//					           "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
//					           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//					           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//					while (rs.Read())
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID))
					{
//						SqlDataReader rs2 = Db.rs("SELECT " +
//						                          "av.ValueInt " +
//						                          "FROM AnswerValue av " +
//						                          "WHERE av.DeletedSessionID IS NULL " +
//						                          "AND av.AnswerID = " + answerID + " " +
//						                          "AND av.QuestionID = " + rs.GetInt32(7) + " " +
//						                          "AND av.OptionID = " + rs.GetInt32(8), "eFormSqlConnection");
						a = answerRepository.ReadByQuestionAndOption(answerID, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id);
//						if (rs2.Read())
						if (a != null)
						{
							int color = IndexFactory.GetColor(c.QuestionOption, a.Values[0].ValueInt);
							#region Commented and used GetColor
							/*int color = 2;
//							if (!rs.IsDBNull(3) && rs.GetInt32(3) >= 0 && rs.GetInt32(3) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(3))
							if (c.QuestionOption.YellowLow >= 0 && c.QuestionOption.YellowLow <= 100 && a.Values[0].ValueInt >= c.QuestionOption.YellowLow)
								color = 1;
//							if (!rs.IsDBNull(4) && rs.GetInt32(4) >= 0 && rs.GetInt32(4) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(4))
							if (c.QuestionOption.GreenLow >= 0 && c.QuestionOption.GreenLow <= 100 && a.Values[0].ValueInt >= c.QuestionOption.GreenLow)
								color = 0;
//							if (!rs.IsDBNull(5) && rs.GetInt32(5) >= 0 && rs.GetInt32(5) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(5))
							if (c.QuestionOption.GreenHigh >= 0 && c.QuestionOption.GreenHigh <= 100 && a.Values[0].ValueInt >= c.QuestionOption.GreenHigh)
								color = 1;
//							if (!rs.IsDBNull(6) && rs.GetInt32(6) >= 0 && rs.GetInt32(6) <= 100 && rs2.GetInt32(0) >= rs.GetInt32(6))
							if (c.QuestionOption.YellowHigh >= 0 && c.QuestionOption.GreenHigh <= 100 && a.Values[0].ValueInt >= c.QuestionOption.YellowHigh)
								color = 2;

//							g.drawBar(color, ++cx, rs2.GetInt32(0));*/
							#endregion
							g.drawBar(color, ++cx, a.Values[0].ValueInt);
//							if (!rs.IsDBNull(2))
							if (c.QuestionOption.TargetValue != 0)
							{
								hasReference = true;
//								g.drawReference(cx, rs.GetInt32(2));
								g.drawReference(cx, c.QuestionOption.TargetValue);
							}
//							g.drawBottomString(rs.GetString(1), cx, true);
							g.drawBottomString(c.QuestionOption.Languages[0].Question, cx, true);
						}
//						rs2.Close();
					}
//					rs.Close();

					//g.drawAxisExpl("poäng", 0, false, false);

					if (hasReference)
					{
						g.drawReference(450, 25, " = riktvärde");
					}

					g.drawColorExplBox("Hälsosam nivå", 0, 100, 30);
					g.drawColorExplBox("Förbättringsbehov", 1, 250, 30);
					g.drawColorExplBox("Ohälsosam nivå", 2, 400, 30);

					#endregion
				}
				#endregion
			}
			else
			{
				#region group stats

				string sortString = "";
				int minDT = 0;
				int maxDT = 0;
//				rs = Db.rs("SELECT " +
//				           "SortString, " +
//				           "dbo.cf_unitLangID(ProjectRoundUnitID) " +
//				           "FROM ProjectRoundUnit " +
//				           "WHERE ProjectRoundUnitID = " + HttpContext.Current.Request.QueryString["PRUID"], "eFormSqlConnection");
				ProjectRoundUnit roundUnit = projectRepository.ReadRoundUnit(Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]));
//				if (rs.Read())
				if (roundUnit != null)
				{
//					sortString = rs.GetString(0);
					sortString = roundUnit.SortString;
					if (langID == 0)
					{
//						langID = rs.GetInt32(1);
						langID = roundUnit.Language.Id;
					}
				}
//				rs.Close();

//				switch (langID)
//				{
//						case 1: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE"); break;
//						case 2: System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US"); break;
//				}
				LanguageFactory.SetCurrentCulture(langID);

				#region Commented
				/*if (type == 1)
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

//					rs = Db.rs("SELECT COUNT(*) FROM OptionComponents WHERE OptionID = " + o, "eFormSqlConnection");
//					if (rs.Read())
//					{
//						cx = rs.GetInt32(0) + 1 + 2;
//					}
//					rs.Close();
					cx = optionRepository.CountByOption(o);
					cx += 1 + 2;
				}
				else if (type == 3)
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

//					rs = Db.rs("SELECT COUNT(*) FROM ProjectRoundUnit pru " +
//					           "WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
//					if (rs.Read())
//					{
//						cx = rs.GetInt32(0) + 2;
//					}
//					rs.Close();
					cx = projectRepository.CountForSortString(sortString);
					cx += 2;
				}
				else if (type == 8)
				{
					if (GB == 0)
					{
						GB = 2;
					}
//					switch (GB)
//					{
//							case 1: groupBy = "dbo.cf_yearWeek"; break;
//							case 2: groupBy = "dbo.cf_year2Week"; break;
//							case 3: groupBy = "dbo.cf_yearMonth"; break;
//							case 4: groupBy = "dbo.cf_year3Month"; break;
//							case 5: groupBy = "dbo.cf_year6Month"; break;
//							case 6: groupBy = "YEAR"; break;
//							case 7: groupBy = "dbo.cf_year2WeekEven"; break;
//					}
					groupBy = GroupFactory.GetGroupBy(GB);
					g = new Graph(895, 440, "#FFFFFF");

//					rs = Db.rs("SELECT " +
//					           "" + groupBy + "(MAX(a.EndDT)) - " + groupBy + "(MIN(a.EndDT)), " +
//					           "" + groupBy + "(MIN(a.EndDT)), " +
//					           "" + groupBy + "(MAX(a.EndDT)) " +
//					           "FROM Answer a " +
//					           "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//					           "INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
//					           "WHERE a.EndDT IS NOT NULL " +
//					           "AND a.EndDT >= pr.Started " +
//					           (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//					           (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//					           "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
					Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
//					if (rs.Read() && !rs.IsDBNull(0))
					if (answer != null)
					{
//						cx = Convert.ToInt32(rs.GetValue(0)) + 3;
//						minDT = rs.GetInt32(1);
//						maxDT = rs.GetInt32(2);
						cx = answer.DummyValue1 + 3;
						minDT = answer.DummyValue2;
						maxDT = answer.DummyValue3;
					}
//					rs.Close();

//					rs = Db.rs("SELECT " +
//					           "rpc.WeightedQuestionOptionID, " +	// 0
//					           "wqo.QuestionID, " +
//					           "wqo.OptionID, " +
//					           "wqo.YellowLow, " +
//					           "wqo.GreenLow, " +
//					           "wqo.GreenHigh, " +
//					           "wqo.YellowHigh " +
//					           "FROM ReportPartComponent rpc " +
//					           "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//					           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//					           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//					int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
//					while (rs.Read())
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid))
					{
//						if (HttpContext.Current.Request.QueryString["GRPNG"] == null || HttpContext.Current.Request.QueryString["GRPNG"] == "0")
						if (HasNoGrouping)
						{
//							SqlDataReader rs2 = Db.rs("SELECT " +
//							                          "MAX(tmp2.VA + tmp2.SD), " +
//							                          "MIN(tmp2.VA - tmp2.SD) " +
//							                          "FROM (" +
//							                          "SELECT " +
//							                          "AVG(tmp.V) AS VA, " +
//							                          "STDEV(tmp.V) AS SD " +
//							                          "FROM (" +
//							                          "SELECT " +
//							                          "" + groupBy + "(a.EndDT) AS DT, " +
//							                          "AVG(av.ValueInt) AS V " +
//							                          "FROM Answer a " +
//							                          "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(1) + " AND av.OptionID = " + rs.GetInt32(2) + " " +
//							                          "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//							                          "INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
//							                          "WHERE a.EndDT IS NOT NULL " +
//							                          //"AND a.EndDT >= pr.Started " +
//							                          (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//							                          (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//							                          "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
//							                          "GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
//							                          ") tmp " +
//							                          "GROUP BY tmp.DT " +
//							                          ") tmp2", "eFormSqlConnection");
							Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
//							if (rs2.Read() && !rs2.IsDBNull(0))
							if (a != null)
							{
//								float min = (float)Convert.ToDouble(rs2.GetValue(1));
//								if (min < 0) { min = 0; }
//								float max = (float)Convert.ToDouble(rs2.GetValue(0));
//								if (max > 100) { max = 100; }
								float min = a.Min < 0 ? 0 : a.Min;
								float max = a.Max > 100 ? 100 : a.Max;
								g.setMinMax(min, max);
								g.roundMinMax();
								//g.computeMinMax(0.01f, 0.01f);
							}
							else
							{
								g.setMinMax(0f, 100f);
							}
//							rs2.Close();
						}
						else
						{
							g.setMinMax(0f, 100f);
						}

//						if (!rs.IsDBNull(3) && !rs.IsDBNull(4) && !rs.IsDBNull(5) && !rs.IsDBNull(6))
//						{
//							if (rs.GetInt32(3) > 0)
						if (c.QuestionOption.YellowLow > 0)
						{
//								g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(3))), "FFA8A8");                             // red
							g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.YellowLow)), "FFA8A8");                             // red
						}
//							if (rs.GetInt32(3) < 100 && rs.GetInt32(4) > 0)
						if (c.QuestionOption.YellowLow < 100 && c.QuestionOption.GreenLow > 0)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(3))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(4))), "FFFEBE");    // yellow
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.YellowLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.GreenLow)), "FFFEBE");    // yellow
						}
//							if (rs.GetInt32(4) < 100 && rs.GetInt32(5) > 0)
						if (c.QuestionOption.GreenLow < 100 && c.QuestionOption.GreenHigh > 0)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(4))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(5))), "CCFFBB");   // green
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.GreenLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.GreenHigh)), "CCFFBB");   // green
						}
//							if (rs.GetInt32(5) < 100 && rs.GetInt32(6) > 0)
						if (c.QuestionOption.GreenHigh < 100 && c.QuestionOption.YellowHigh > 0)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(5))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(6))), "FFFEBE"); // yellow
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.GreenHigh)), Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.YellowHigh)), "FFFEBE"); // yellow
						}
//							if (rs.GetInt32(6) < 100)
						if (c.QuestionOption.YellowHigh < 100)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(6))), g.maxVal, "FFA8A8");                           // red
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.YellowHigh)), g.maxVal, "FFA8A8");                           // red
						}
//						}
					}
//					rs.Close();

					if (g.minVal != 0f)
					{
						// Crunched graph sign
						g.drawLine(20, 0, (int)g.maxH + 20, 0, (int)g.maxH + 23, 1);
						g.drawLine(20, 0, (int)g.maxH + 23, -5, (int)g.maxH + 26, 1);
						g.drawLine(20, -5, (int)g.maxH + 26, 5, (int)g.maxH + 32, 1);
						g.drawLine(20, 5, (int)g.maxH + 32, 0, (int)g.maxH + 35, 1);
						g.drawLine(20, 0, (int)g.maxH + 35, 0, (int)g.maxH + 38, 1);
					}
				}
				else
				{
					g = new Graph(895, 550, "#FFFFFF");
					g.setMinMax(0f, 100f);

					cx += 2;
				}

				steps = cx;
				g.computeSteping((steps <= 1 ? 2 : steps));
				g.drawOutlines(11);
				g.drawAxis();

				cx = 0;*/
				#endregion

				if (type == 1)
				{
					#region Question

					#region Commented
//					decimal tot = 0;

//					rs = Db.rs("SELECT COUNT(*) FROM Answer a " +
//					           "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//					           "WHERE a.EndDT IS NOT NULL " +
//					           (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//					           (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//					           "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
//					if (rs.Read() && !rs.IsDBNull(0))
//					{
//						tot = Convert.ToDecimal(rs.GetInt32(0));
//					}
//					rs.Close();
					#endregion
					decimal tot = answerRepository.CountByDate(fy, ty, sortString);

					if (rac > Convert.ToInt32(tot))
					{
						g = new ExtendedGraph(895, 50, "#FFFFFF");
						g.drawStringInGraph("Ingen återkoppling pga otillräckligt underlag", 300, -30);
					}
					else
					{
						g = new ExtendedGraph(895, 550, "#FFFFFF");
						List<Bar> bars = new List<Bar>();
						foreach (OptionComponentLanguage c in optionRepository.FindComponentsByLanguage(o, langID)) {
							int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, 2011, 2012, 1, 1, "");
							var b = new Bar {
								Description = c.Text,
								Value = Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)),
								Color = 5
							};
							bars.Add(b);
						}
						cx = optionRepository.CountByOption(o);
						g.DrawBars(HttpContext.Current.Request.QueryString["DISABLED"], cx, tot, bars);
						g.drawAxisExpl(string.Format("% (n = {0})", tot), 5, false, false);
						#region Commented
						/*g = new Graph(895, 550, "#FFFFFF");
						g.setMinMax(0f, 100f);

						cx = optionRepository.CountByOption(o);
						cx += 1 + 2;

						steps = cx;
						g.computeSteping((steps <= 1 ? 2 : steps));
						g.drawOutlines(11);
						g.drawAxis();

						cx = 0;
						decimal sum = 0;
						
						g.drawAxisExpl("% (n = " + tot + ")", 5, false, false);
						
//						rs = Db.rs("SELECT oc.OptionComponentID, ocl.Text FROM OptionComponents ocs " +
//						           "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
//						           "INNER JOIN OptionComponentLang ocl ON oc.OptionComponentID = ocl.OptionComponentID AND ocl.LangID = " + langID + " " +
//						           "WHERE ocs.OptionID = " + o + " ORDER BY ocs.SortOrder", "eFormSqlConnection");
//						while (rs.Read())
						var components = optionRepository.FindComponentsByLanguage(o, langID);
						foreach (OptionComponentLanguage c in components)
						{
							cx++;

//							SqlDataReader rs2 = Db.rs("SELECT COUNT(*) FROM Answer a " +
//							                          "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID " +
//							                          "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//							                          "WHERE a.EndDT IS NOT NULL AND av.ValueInt = " + rs.GetInt32(0) + " " +
//							                          (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//							                          (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//							                          "AND av.OptionID = " + o + " " +
//							                          "AND av.QuestionID = " + q + " " +
//							                          "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
							int x = answerRepository.CountByValueWithDateOptionAndQuestion(c.Component.Id, fy, ty, o, q, sortString);
//							if (rs2.Read())
//							{
//								sum += Convert.ToDecimal(rs2.GetInt32(0));
//								g.drawBar(5, cx, Convert.ToInt32(Math.Round(Convert.ToDecimal(rs2.GetInt32(0)) / tot * 100M, 0)));
//							}
//							rs2.Close();
//							g.drawBottomString(rs.GetString(1), cx, true);
							sum += x;
							g.drawBar(5, cx, Convert.ToInt32(Math.Round(Convert.ToDecimal(x) / tot * 100M, 0)));
							g.drawBottomString(c.Text, cx, true);
						}
//						rs.Close();

						g.drawBar(4, ++cx, Convert.ToInt32(Math.Round((tot - sum) / tot * 100M, 0)));
						g.drawBottomString("Inget svar", cx, true);*/
						#endregion
					}
					#endregion
				}
				else if (type == 3)
				{
					#region Benchmark
					g = new ExtendedGraph(895, 550, "#FFFFFF");
					
					List<Bar> bars = new List<Bar>();
					List<int> referenceLines = new List<int>();
					
					foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
						System.Collections.SortedList all = new System.Collections.SortedList();

						foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString)) {
							res = new System.Collections.Hashtable();

							if (c.Index.Parts.Count == 0) {
								getIdxVal(c.Index.Id, u.SortString, langID);
							} else {
								getOtherIdxVal(c.Index.Id, u.SortString, langID);
							}

							if (all.Contains(lastVal)) {
								all[lastVal] += "," + u.TreeString;
							} else {
								all.Add(lastVal, u.TreeString);
							}
						}

						for (int i = all.Count - 1; i >= 0; i--) {
							int color = IndexFactory.GetColor(c.Index, Convert.ToInt32(all.GetKey(i)));
							string[] u = all.GetByIndex(i).ToString().Split(',');

							foreach (string s in u) {
								bars.Add(new Bar { Color = color, Value = Convert.ToInt32(all.GetKey(i)), Description = s });
							}
						}
						referenceLines.Add(c.Index.TargetValue);
					}
					g.DrawBars(HttpContext.Current.Request.QueryString["DISABLED"], cx, bars, referenceLines);
					g.drawAxisExpl("poäng", 0, false, false);
					#region Commented
					/*
//					g.setMinMax(0f, 100f);
//
//					cx = projectRepository.CountForSortString(sortString);
//					cx += 2;
//
//					steps = cx;
//					g.computeSteping((steps <= 1 ? 2 : steps));
//					g.drawOutlines(11);
//					g.drawAxis();
//
//					cx = 0;
					
//					rs = Db.rs("SELECT " +
//					           "rpc.IdxID, " +
//					           "(SELECT COUNT(*) FROM IdxPart ip WHERE ip.IdxID = rpc.IdxID AND ip.OtherIdxID IS NOT NULL), " +
//					           "i.TargetVal, " +
//					           "i.YellowLow, " +
//					           "i.GreenLow, " +
//					           "i.GreenHigh, " +
//					           "i.YellowHigh " +
//					           "FROM ReportPartComponent rpc " +
//					           "INNER JOIN Idx i ON rpc.IdxID = i.IdxID " +
//					           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//					           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//					while (rs.Read())
//					int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
					foreach (ReportPartComponent c in reportRepository.FindComponents(rpid))
					{
						System.Collections.SortedList all = new System.Collections.SortedList();

//						SqlDataReader rs2 = Db.rs("SELECT dbo.cf_projectUnitTree(pru.ProjectRoundUnitID,' » '), SortString FROM ProjectRoundUnit pru " +
//						                          "WHERE LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
//						while (rs2.Read())
						foreach (ProjectRoundUnit u in projectRepository.FindRoundUnitsBySortString(sortString))
						{
							res = new System.Collections.Hashtable();

//							if (rs.GetInt32(1) == 0)
							if (c.Index.Parts.Count == 0)
							{
//								getIdxVal(rs.GetInt32(0), rs2.GetString(1), langID);
								getIdxVal(c.Index.Id, u.SortString, langID);
							}
							else
							{
//								getOtherIdxVal(rs.GetInt32(0), rs2.GetString(1), langID);
								getOtherIdxVal(c.Index.Id, u.SortString, langID);
							}

							if (all.Contains(lastVal))
							{
//								all[lastVal] += "," + rs2.GetString(0);
								all[lastVal] += "," + u.TreeString;
							}
							else
							{
//								all.Add(lastVal, rs2.GetString(0));
								all.Add(lastVal, u.TreeString);
							}
						}
//						rs2.Close();

						for (int i = all.Count - 1; i >= 0; i--)
						{
							int color = IndexFactory.GetColor(c.Index, Convert.ToInt32(all.GetKey(i)));

							string[] u = all.GetByIndex(i).ToString().Split(',');

							foreach (string s in u)
							{
								g.drawBar(color, ++cx, Convert.ToInt32(all.GetKey(i)));
								//g.drawReference(cx,rs.GetInt32(2));
								g.drawBottomString(s, cx, true);
							}
						}

//						g.drawReferenceLine(rs.GetInt32(2), " = riktvärde");
						g.drawReferenceLine(c.Index.TargetValue, " = riktvärde");
					}
//					rs.Close();

					g.drawAxisExpl("poäng", 0, false, false);

					//g.drawReferenceLineExpl(770,25," = riktvärde");
					 */
					#endregion
					#endregion
				}
				else if (type == 2)
				{
					#region Index
					g = new ExtendedGraph(895, 550, "#FFFFFF");
					List<Bar> bars = new List<Bar>();
					foreach (ReportPartComponent c in reportRepository.FindComponents(rpid)) {
						if (c.Index.Parts.Count == 0) {
							getIdxVal(c.Index.Id, sortString, langID);
						} else {
							getOtherIdxVal(c.Index.Id, sortString, langID);
						}
						int color = IndexFactory.GetColor(c.Index, lastVal);
						bars.Add(new Bar { Value = lastVal, Color = color, Description = lastDesc, Reference = c.Index.TargetValue });
					}
					g.DrawBars(HttpContext.Current.Request.QueryString["DISABLED"], cx, bars);
					g.drawAxisExpl("poäng", 0, false, false);
					g.drawReference(780, 25, " = riktvärde");
					#region Commented
					/*g.setMinMax(0f, 100f);

					cx += 2;
					
					steps = cx;
					g.computeSteping((steps <= 1 ? 2 : steps));
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;
					
//					rs = Db.rs("SELECT " +
//					           "rpc.IdxID, " +
//					           "(SELECT COUNT(*) FROM IdxPart ip WHERE ip.IdxID = rpc.IdxID AND ip.OtherIdxID IS NOT NULL), " +
//					           "i.TargetVal, " +
//					           "i.YellowLow, " +
//					           "i.GreenLow, " +
//					           "i.GreenHigh, " +
//					           "i.YellowHigh " +
//					           "FROM ReportPartComponent rpc " +
//					           "INNER JOIN Idx i ON rpc.IdxID = i.IdxID " +
//					           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//					           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//					int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
//					while (rs.Read())
					foreach (ReportPartComponent c in reportRepository.FindComponents(rpid))
					{
//						if (rs.GetInt32(1) == 0)
						if (c.Index.Parts.Count == 0)
						{
//							getIdxVal(rs.GetInt32(0), sortString, langID);
							getIdxVal(c.Index.Id, sortString, langID);
						}
						else
						{
//							getOtherIdxVal(rs.GetInt32(0), sortString, langID);
							getOtherIdxVal(c.Index.Id, sortString, langID);
						}
						int color = IndexFactory.GetColor(c.Index, lastVal);
						
						g.drawBar(color, ++cx, lastVal);

//						if (!rs.IsDBNull(2) && rs.GetInt32(2) >= 0 && rs.GetInt32(2) <= 100)
						if (c.Index.TargetValue >= 0 && c.Index.TargetValue <= 100)
//							g.drawReference(cx, rs.GetInt32(2));
							g.drawReference(cx, c.Index.TargetValue);

						g.drawBottomString(lastDesc, cx, true);
					}
//					rs.Close();

					g.drawAxisExpl("poäng", 0, false, false);

					g.drawReference(780, 25, " = riktvärde");*/
					#endregion
					#endregion
				}
				else if (type == 8)
				{
					int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
					int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
					int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
					int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
					string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");
					
					if (GB == 0) {
						GB = 2;
					}
					
					groupBy = GroupFactory.GetGroupBy(GB);
					g = new ExtendedGraph(895, 440, "#FFFFFF");

					Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
					if (answer != null) {
						cx = answer.DummyValue1 + 3;
						minDT = answer.DummyValue2;
						maxDT = answer.DummyValue3;
					}

					List<IIndex> indexes = new List<IIndex>();
					List<IMinMax> minMaxes = new List<IMinMax>();
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
						if (!HasGrouping) {
							Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
							if (a != null) {
								minMaxes.Add(a);
							} else {
								minMaxes.Add(new Answer());
							}
						} else {
							minMaxes.Add(new Answer());
						}
						indexes.Add(c.QuestionOption);
					}
					g.SetMinMaxes(minMaxes);
					g.DrawBackgroundFromIndexes(indexes);
					g.DrawComputingSteps(HttpContext.Current.Request.QueryString["DISABLED"], cx);
					g.Type = new BoxPlotGraphType();

					cx = 0;
					
					g.DrawBottomString(minDT, maxDT, GB);
					
					List<IExplanation> explanations = new List<IExplanation>();
					List<IExplanation> explanationBoxes = new List<IExplanation>();
					
					if (HasGrouping) {
						int COUNT = 0;
						Hashtable desc = new Hashtable();
						Hashtable join = new Hashtable();
						ArrayList item = new ArrayList();
						string extraDesc = "";
						
						COUNT = GroupFactory.GetCount(GRPNG, SPONS, SID, PRUID, GID, ref extraDesc, desc, join, item, departmentRepository, questionRepository);
						
						int breaker = 6, itemWidth = 120;
						if (COUNT < 6) {
							breaker = 4;
							itemWidth = 180;
						}
						if (COUNT < 4) {
							breaker = 3;
							itemWidth = 240;
						}
						
						explanations.Add(
							new Explanation {
								Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
								Color = 0,
								Right = false,
								Box = false,
								HasAxis = false
							}
						);
						ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
						if (c != null) {
							int bx = 0;
							cx = 1;
							foreach(string i in item) {
								explanationBoxes.Add(
									new Explanation {
										Description = (string)desc[i],
										Color = bx + 4,
										X = 130 + (int)((bx % breaker) * itemWidth),
										Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
									}
								);
//						cx = 1;
								int lastDT = minDT - 1;
								Series s = new Series { Color = bx + 4 };
								foreach (var a in answerRepository.FindByQuestionAndOptionJoinedAndGrouped(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty)) {
									while (lastDT + 1 < a.SomeInteger) {
										lastDT++;
										cx++;
									}
//							if (a.CountV >= rac) {
//								if (COUNT == 1) {
//									g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.CountV : ""));
//								}
									List<double> n = new List<double>();
									foreach (var v in a.Values) {
										n.Add((double)v.ValueDecimal);
									}
									HWList l = new HWList(n);
									s.Points.Add(
										new PointV {
											X = cx,
											Y = (float)l.Median,
											UpperWhisker = l.UpperWhisker,
											LowerWhisker = l.LowerWhisker,
											UpperBox = l.UpperBox,
											LowerBox = l.LowerBox
										}
									);
//							}
									lastDT = a.SomeInteger;
//							cx++;
								}
								g.Series.Add(s);
								bx++;
								cx++;
							}
						}
					} else {
						int bx = 0;
						foreach (var c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
							if (bx == 0) {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = false,
										Box = true,
										HasAxis = false
									}
								);
							} else {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = true,
										Box = false,
										HasAxis = true
									}
								);
							}
							cx = 1;
							int lastDT = minDT - 1;
							Series s = new Series { Color = bx + 4 };
							foreach (var a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
								while (lastDT + 1 < a.SomeInteger) {
									lastDT++;
									cx++;
								}

								if (a.CountV >= rac) {
									g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
									s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 3 });
								}
								lastDT = a.SomeInteger;
								cx++;
							}
							g.Series.Add(s);
							bx++;
						}
					}
					g.DrawExplanations(explanations);
					g.DrawExplanationBoxes(explanationBoxes);
					g.Draw();
					/*if (GB == 0) {
						GB = 2;
					}
					
					groupBy = GroupFactory.GetGroupBy(GB);
					g = new ExtendedGraph(895, 440, "#FFFFFF");

					Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
					if (answer != null) {
						cx = answer.DummyValue1 + 3;
						minDT = answer.DummyValue2;
						maxDT = answer.DummyValue3;
					}

					List<IIndex> indexes = new List<IIndex>();
					List<IMinMax> minMaxes = new List<IMinMax>();
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid)) {
						if (!HasGrouping) {
							Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
							if (a != null) {
								minMaxes.Add(a);
							} else {
								minMaxes.Add(new Answer());
							}
						} else {
							minMaxes.Add(new Answer());
						}
						indexes.Add(c.QuestionOption);
					}
					g.SetMinMaxes(minMaxes);
					g.DrawBackgroundFromIndexes(indexes);
					g.DrawComputingSteps(HttpContext.Current.Request.QueryString["DISABLED"], cx);
					g.Type = new LineGraphType(stdev);

					cx = 0;
					
					g.DrawBottomString(minDT, maxDT, GB);
					
					List<IExplanation> explanations = new List<IExplanation>();
					List<IExplanation> explanationBoxes = new List<IExplanation>();
					
					if (HasGrouping) {
						int COUNT = 0;
						Hashtable desc = new Hashtable();
						Hashtable join = new Hashtable();
						ArrayList item = new ArrayList();
						string extraDesc = "";
						
						COUNT = GroupFactory.GetCount(GRPNG, SPONS, SID, PRUID, GID, ref extraDesc, desc, join, item, departmentRepository, questionRepository);
						
						int breaker = 6, itemWidth = 120;
						if (COUNT < 6) {
							breaker = 4;
							itemWidth = 180;
						}
						if (COUNT < 4) {
							breaker = 3;
							itemWidth = 240;
						}
						
						explanations.Add(
							new Explanation {
								Description = (extraDesc != "" ? extraDesc + "\n" : "") + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
								Color = 0,
								Right = false,
								Box = false,
								HasAxis = false
							}
						);
						ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
						if (c != null) {
							int bx = 0;
							foreach(string i in item) {
								explanationBoxes.Add(
									new Explanation {
										Description = (string)desc[i],
										Color = bx + 4,
										X = 130 + (int)((bx % breaker) * itemWidth),
										Y = 20 + (int)Math.Floor((double)bx / breaker) * 15
									}
								);
								cx = 1;
								int lastDT = minDT - 1;
								Series s = new Series { Color = bx + 4 };
								foreach (var a in answerRepository.FindByQuestionAndOptionJoinedAndGrouped(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty)) {
									while (lastDT + 1 < a.SomeInteger) {
										lastDT++;
										cx++;
									}
									if (a.CountV >= rac) {
										if (COUNT == 1) {
											g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.CountV : ""));
										}
										s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 2 + (!stdev ? 1 : 0) });
									}
									lastDT = a.SomeInteger;
									cx++;
								}
								g.Series.Add(s);
								bx++;
							}
						}
					} else {
						int bx = 0;
						foreach (var c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID)) {
							if (bx == 0) {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = false,
										Box = true,
										HasAxis = false
									}
								);
							} else {
								explanations.Add(
									new Explanation {
										Description = c.QuestionOption.Languages[0].Question + ", " + LanguageFactory.GetMeanText(langID) + (IsStandardDeviation ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""),
										Color = bx + 4,
										Right = true,
										Box = false,
										HasAxis = true
									}
								);
							}
							cx = 1;
							int lastDT = minDT - 1;
							Series s = new Series { Color = bx + 4 };
							foreach (var a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString)) {
								while (lastDT + 1 < a.SomeInteger) {
									lastDT++;
									cx++;
								}

								if (a.CountV >= rac) {
									g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
									s.Points.Add(new PointV { X = cx, Y = a.AverageV, Deviation = a.StandardDeviation, T = 3 });
								}
								lastDT = a.SomeInteger;
								cx++;
							}
							g.Series.Add(s);
							bx++;
						}
					}
					g.DrawExplanations(explanations);
					g.DrawExplanationBoxes(explanationBoxes);
					g.Draw();*/
					
					#region Commented
					/*
					#region Bottom string
					if (GB == 0)
					{
						GB = 2;
					}
//					switch (GB)
//					{
//							case 1: groupBy = "dbo.cf_yearWeek"; break;
//							case 2: groupBy = "dbo.cf_year2Week"; break;
//							case 3: groupBy = "dbo.cf_yearMonth"; break;
//							case 4: groupBy = "dbo.cf_year3Month"; break;
//							case 5: groupBy = "dbo.cf_year6Month"; break;
//							case 6: groupBy = "YEAR"; break;
//							case 7: groupBy = "dbo.cf_year2WeekEven"; break;
//					}
					groupBy = GroupFactory.GetGroupBy(GB);
					g = new ExtendedGraph(895, 440, "#FFFFFF");

//					rs = Db.rs("SELECT " +
//					           "" + groupBy + "(MAX(a.EndDT)) - " + groupBy + "(MIN(a.EndDT)), " +
//					           "" + groupBy + "(MIN(a.EndDT)), " +
//					           "" + groupBy + "(MAX(a.EndDT)) " +
//					           "FROM Answer a " +
//					           "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//					           "INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
//					           "WHERE a.EndDT IS NOT NULL " +
//					           "AND a.EndDT >= pr.Started " +
//					           (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//					           (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//					           "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "'", "eFormSqlConnection");
					Answer answer = answerRepository.ReadByGroup(groupBy, fy, ty, sortString);
//					if (rs.Read() && !rs.IsDBNull(0))
					if (answer != null)
					{
//						cx = Convert.ToInt32(rs.GetValue(0)) + 3;
//						minDT = rs.GetInt32(1);
//						maxDT = rs.GetInt32(2);
						cx = answer.DummyValue1 + 3;
						minDT = answer.DummyValue2;
						maxDT = answer.DummyValue3;
					}
//					rs.Close();

//					rs = Db.rs("SELECT " +
//					           "rpc.WeightedQuestionOptionID, " +	// 0
//					           "wqo.QuestionID, " +
//					           "wqo.OptionID, " +
//					           "wqo.YellowLow, " +
//					           "wqo.GreenLow, " +
//					           "wqo.GreenHigh, " +
//					           "wqo.YellowHigh " +
//					           "FROM ReportPartComponent rpc " +
//					           "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//					           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//					           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//					int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
//					while (rs.Read())
					foreach (ReportPartComponent c in reportRepository.FindComponentsByPart(rpid))
					{
//						if (HttpContext.Current.Request.QueryString["GRPNG"] == null || HttpContext.Current.Request.QueryString["GRPNG"] == "0")
						if (!HasGrouping)
						{
//							SqlDataReader rs2 = Db.rs("SELECT " +
//							                          "MAX(tmp2.VA + tmp2.SD), " +
//							                          "MIN(tmp2.VA - tmp2.SD) " +
//							                          "FROM (" +
//							                          "SELECT " +
//							                          "AVG(tmp.V) AS VA, " +
//							                          "STDEV(tmp.V) AS SD " +
//							                          "FROM (" +
//							                          "SELECT " +
//							                          "" + groupBy + "(a.EndDT) AS DT, " +
//							                          "AVG(av.ValueInt) AS V " +
//							                          "FROM Answer a " +
//							                          "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(1) + " AND av.OptionID = " + rs.GetInt32(2) + " " +
//							                          "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//							                          "INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
//							                          "WHERE a.EndDT IS NOT NULL " +
//							                          //"AND a.EndDT >= pr.Started " +
//							                          (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//							                          (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//							                          "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
//							                          "GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
//							                          ") tmp " +
//							                          "GROUP BY tmp.DT " +
//							                          ") tmp2", "eFormSqlConnection");
							Answer a = answerRepository.ReadMinMax(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString);
//							if (rs2.Read() && !rs2.IsDBNull(0))
							if (a != null)
							{
//								float min = (float)Convert.ToDouble(rs2.GetValue(1));
//								if (min < 0) { min = 0; }
//								float max = (float)Convert.ToDouble(rs2.GetValue(0));
//								if (max > 100) { max = 100; }
								float min = a.Min < 0 ? 0 : a.Min;
								float max = a.Max > 100 ? 100 : a.Max;
								g.setMinMax(min, max);
								g.roundMinMax();
								//g.computeMinMax(0.01f, 0.01f);
							}
							else
							{
								g.setMinMax(0f, 100f);
							}
//							rs2.Close();
						}
						else
						{
							g.setMinMax(0f, 100f);
						}

//						if (!rs.IsDBNull(3) && !rs.IsDBNull(4) && !rs.IsDBNull(5) && !rs.IsDBNull(6))
//						{
//							if (rs.GetInt32(3) > 0)
						if (c.QuestionOption.YellowLow > 0)
						{
//								g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(3))), "FFA8A8");                             // red
							g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.YellowLow)), "FFA8A8");                             // red
						}
//							if (rs.GetInt32(3) < 100 && rs.GetInt32(4) > 0)
						if (c.QuestionOption.YellowLow < 100 && c.QuestionOption.GreenLow > 0)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(3))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(4))), "FFFEBE");    // yellow
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.YellowLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.GreenLow)), "FFFEBE");    // yellow
						}
//							if (rs.GetInt32(4) < 100 && rs.GetInt32(5) > 0)
						if (c.QuestionOption.GreenLow < 100 && c.QuestionOption.GreenHigh > 0)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(4))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(5))), "CCFFBB");   // green
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.GreenLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.GreenHigh)), "CCFFBB");   // green
						}
//							if (rs.GetInt32(5) < 100 && rs.GetInt32(6) > 0)
						if (c.QuestionOption.GreenHigh < 100 && c.QuestionOption.YellowHigh > 0)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(5))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(6))), "FFFEBE"); // yellow
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.GreenHigh)), Math.Min(g.maxVal, (float)Convert.ToDouble(c.QuestionOption.YellowHigh)), "FFFEBE"); // yellow
						}
//							if (rs.GetInt32(6) < 100)
						if (c.QuestionOption.YellowHigh < 100)
						{
//								g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(6))), g.maxVal, "FFA8A8");                           // red
							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(c.QuestionOption.YellowHigh)), g.maxVal, "FFA8A8");                           // red
						}
//						}
					}
//					rs.Close();

					if (g.minVal != 0f)
					{
						// Crunched graph sign
						g.drawLine(20, 0, (int)g.maxH + 20, 0, (int)g.maxH + 23, 1);
						g.drawLine(20, 0, (int)g.maxH + 23, -5, (int)g.maxH + 26, 1);
						g.drawLine(20, -5, (int)g.maxH + 26, 5, (int)g.maxH + 32, 1);
						g.drawLine(20, 5, (int)g.maxH + 32, 0, (int)g.maxH + 35, 1);
						g.drawLine(20, 0, (int)g.maxH + 35, 0, (int)g.maxH + 38, 1);
					}
					
					steps = cx;
					g.computeSteping((steps <= 1 ? 2 : steps));
					g.drawOutlines(11);
					g.drawAxis();

					cx = 0;
					
					int dx = 0;
					for (int i = minDT; i <= maxDT; i++) {
						dx++;
//						DrawBottomString(GB, g, i, dx, "");
						g.DrawBottomString(GB, i, dx, "");
					}
					#endregion
					
//					if (HttpContext.Current.Request.QueryString["GRPNG"] != null)
					if (HasGrouping)
					{
						#region GRPNG
						int COUNT = 0;
						Hashtable desc = new Hashtable();
						Hashtable join = new Hashtable();
						ArrayList item = new ArrayList();
						string extraDesc = "";
						
						int GRPNG = Convert.ToInt32(HttpContext.Current.Request.QueryString["GRPNG"]);
						int PRUID = Convert.ToInt32(HttpContext.Current.Request.QueryString["PRUID"]);
						int SPONS = Convert.ToInt32((HttpContext.Current.Request.QueryString["SAID"] != null ? HttpContext.Current.Request.QueryString["SAID"] : HttpContext.Current.Session["SponsorAdminID"]));
						int SID = Convert.ToInt32((HttpContext.Current.Request.QueryString["SID"] != null ? HttpContext.Current.Request.QueryString["SID"] : HttpContext.Current.Session["SponsorID"]));
						string GID = (HttpContext.Current.Request.QueryString["GID"] != null ? HttpContext.Current.Request.QueryString["GID"].ToString().Replace(" ", "") : "");

						COUNT = GroupFactory.GetCount(GRPNG, SPONS, SID, PRUID, GID, ref extraDesc, desc, join, item, departmentRepository, questionRepository);
						
						int breaker = 6, itemWidth = 120;
						if (COUNT < 6)
						{
							breaker = 4; itemWidth = 180;
						}
						if (COUNT < 4)
						{
							breaker = 3; itemWidth = 240;
						}
//						rs = Db.rs("SELECT " +
//						           "rpc.WeightedQuestionOptionID, " +	// 0
//						           "wqol.WeightedQuestionOption, " +
//						           "wqo.QuestionID, " +
//						           "wqo.OptionID " +
//						           "FROM ReportPartComponent rpc " +
//						           "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//						           "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
//						           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//						           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
						
//						int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
						ReportPartComponent c = reportRepository.ReadComponentByPartAndLanguage(rpid, langID);
//						if (rs.Read())
						if (c != null)
						{
							int bx = 0;

							foreach(string i in item)
							{
								if (bx == 0)
								{
									g.drawAxis(false);
									g.drawAxisExpl((extraDesc != "" ? extraDesc + "\n" : "") + (langID == 1 ? "Medelvärde" : "Mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), 0, false, false);
								}
								g.drawColorExplBox((string)desc[i], bx + 4, 130 + (int)((bx % breaker) * itemWidth), 20 + (int)Math.Floor((double)bx / breaker) * 15);
								float lastVal = -1f;
								float lastStd = -1f;
								int lastCX = 1;
								cx = 1;
								int lastDT = minDT-1;
								#region Data loop
//								string SQL = "SELECT " +
//									"tmp.DT, " +
//									"AVG(tmp.V), " +
//									"COUNT(tmp.V), " +
//									"STDEV(tmp.V) " +
//									"FROM (" +
//									"SELECT " +
//									"" + groupBy + "(a.EndDT) AS DT, " +
//									"AVG(av.ValueInt) AS V " +
//									"FROM Answer a " +
//									join[i] +
//									"INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
//									"WHERE a.EndDT IS NOT NULL " +
//									(HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//									(HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//									"GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
//									") tmp " +
//									"GROUP BY tmp.DT " +
//									"ORDER BY tmp.DT";
//								//HttpContext.Current.Response.Write("<HTML><BODY><!--" + SQL + "--></BODY></HTML>");
//								//HttpContext.Current.Response.End();
//								SqlDataReader rs2 = Db.rs(SQL, "eFormSqlConnection");
//								while (rs2.Read())
								foreach (var a in answerRepository.FindByQuestionAndOptionJoinedAndGrouped(join[i].ToString(), groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty))
								{
									//if (lastDT == 0) { lastDT = rs2.GetInt32(0); }

//									while (lastDT + 1 < rs2.GetInt32(0))
									while (lastDT + 1 < a.SomeInteger)
									{
										lastDT++;
										cx++;
									}

//									if (rs2.GetInt32(2) >= rac)
									if (a.CountV >= rac)
									{
										#region Bottom string
										if (COUNT == 1)
										{
//											DrawBottomString(GB, g, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.CountV : ""));
											g.DrawBottomString(GB, a.SomeInteger, cx, (COUNT == 1 ? ", n = " + a.CountV : ""));
										}
										#endregion

//										float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
//										float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));
										float newVal = a.AverageV;
										float newStd = a.StandardDeviation;

										if (stdev)
										{
											g.drawLine(bx + 4, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
											g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
											g.drawLine(bx + 4, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
										}

										if (newVal != -1f)
										{
											if (lastVal != -1f)
											{
												g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal, 2 + (!stdev ? 1 : 0));
											}
											lastCX = cx;
										}
										lastVal = newVal;
										lastStd = newStd;

										g.drawCircle(cx, newVal, bx + 4);
									}
//									lastDT = rs2.GetInt32(0);
									lastDT = a.SomeInteger;
									cx++;
								}
//								rs2.Close();
								#endregion

								bx++;
							}
						}
//						rs.Close();
						#endregion
					}
					else
					{
						int bx = 0;
//						rs = Db.rs("SELECT " +
//						           "rpc.WeightedQuestionOptionID, " +	// 0
//						           "wqol.WeightedQuestionOption, " +
//						           "wqo.QuestionID, " +
//						           "wqo.OptionID " +
//						           "FROM ReportPartComponent rpc " +
//						           "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
//						           "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
//						           "WHERE rpc.ReportPartID = " + HttpContext.Current.Request.QueryString["RPID"] + " " +
//						           "ORDER BY rpc.SortOrder", "eFormSqlConnection");
//						int rpid = Convert.ToInt32(HttpContext.Current.Request.QueryString["RPID"]);
//						while (rs.Read() && bx <= 1)
						foreach (var c in reportRepository.FindComponentsByPartAndLanguage2(rpid, langID))
						{
							if (bx == 0)
							{
//								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), bx + 4, false, true);
								g.drawAxisExpl(c.QuestionOption.Languages[0].Option + ", " + (langID == 1 ? "medelvärde" : "mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), bx + 4, false, true);
								g.drawAxis(false);
							}
							else
							{
//								g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), bx + 4, true, true);
								g.drawAxisExpl(c.QuestionOption.Languages[0].Option + ", " + (langID == 1 ? "medelvärde" : "mean value") + (HttpContext.Current.Request.QueryString["STDEV"] != null && Convert.ToInt32(HttpContext.Current.Request.QueryString["STDEV"]) == 1 ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), bx + 4, true, true);
								g.drawAxis(true);
							}
							float lastVal = -1f;
							float lastStd = -1f;
							int lastCX = 1;
							cx = 1;
							int lastDT = minDT-1;
							#region Data loop
//							SqlDataReader rs2 = Db.rs("SELECT " +
//							                          "tmp.DT, " +
//							                          "AVG(tmp.V), " +
//							                          "COUNT(tmp.V), " +
//							                          "STDEV(tmp.V) " +
//							                          "FROM (" +
//							                          "SELECT " +
//							                          "" + groupBy + "(a.EndDT) AS DT, " +
//							                          "AVG(av.ValueInt) AS V " +
//							                          "FROM Answer a " +
//							                          "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
//							                          "INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID " +
//							                          "INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
//							                          "WHERE a.EndDT IS NOT NULL " +
//							                          "AND a.EndDT >= pr.Started " +
//							                          (HttpContext.Current.Request.QueryString["FY"] != null ? "AND YEAR(a.EndDT) >= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["FY"]) + " " : "") +
//							                          (HttpContext.Current.Request.QueryString["TY"] != null ? "AND YEAR(a.EndDT) <= " + Convert.ToInt32(HttpContext.Current.Request.QueryString["TY"]) + " " : "") +
//							                          "AND LEFT(pru.SortString," + sortString.Length + ") = '" + sortString + "' " +
//							                          "GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
//							                          ") tmp " +
//							                          "GROUP BY tmp.DT " +
//							                          "ORDER BY tmp.DT", "eFormSqlConnection");
//							while (rs2.Read())
							foreach (var a in answerRepository.FindByQuestionAndOptionGrouped(groupBy, c.QuestionOption.Question.Id, c.QuestionOption.Option.Id, fy, ty, sortString))
							{
								//if (lastDT == 0) { lastDT = rs2.GetInt32(0); }

//								while (lastDT + 1 < rs2.GetInt32(0))
								while (lastDT + 1 < a.SomeInteger)
								{
									lastDT++;
									cx++;
								}

//								if (rs2.GetInt32(2) >= rac)
								if (a.CountV >= rac)
								{
									#region Bottom string
//									DrawBottomString(GB, g, a.SomeInteger, cx, ", n = " + a.CountV);
									g.DrawBottomString(GB, a.SomeInteger, cx, ", n = " + a.CountV);
									#endregion

//									float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
//									float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));
									float newVal = a.AverageV;
									float newStd = a.StandardDeviation;

									g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
									g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
									g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);

									if (newVal != -1f)
									{
										if (lastVal != -1f)
										{
											g.drawStepLine(bx + 4, lastCX, lastVal, cx, newVal, 3);
										}
										lastCX = cx;
									}
									lastVal = newVal;
									lastStd = newStd;

									g.drawCircle(cx, newVal);
								}
//								lastDT = rs2.GetInt32(0);
								lastDT = a.SomeInteger;
								cx++;
							}
//							rs2.Close();
							#endregion

							bx++;
						}
//						rs.Close();
					}
					 */
					#endregion
				}

				#endregion
			}

			// g.printCopyRight();
			g.render();
		}
	}
}