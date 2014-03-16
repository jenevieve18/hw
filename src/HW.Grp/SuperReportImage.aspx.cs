using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HW.Core.Helpers;
using HW.Core.Models;
using HW.Core.Repositories.Sql;

namespace HW.Grp
{
	public partial class SuperReportImage : System.Web.UI.Page
	{
		SqlDepartmentRepository departmentRepository = new SqlDepartmentRepository();
		SqlReportRepository reportRepository = new SqlReportRepository();
		SqlAnswerRepository answerRepository = new SqlAnswerRepository();
		
		protected void Page_Load(object sender, EventArgs e)
		{
			string rnds1 = (Request.QueryString["RNDS1"] != null ? Request.QueryString["RNDS1"] : "");
			string rnds2 = (Request.QueryString["RNDS2"] != null ? Request.QueryString["RNDS2"] : "");
			string rndsd1 = (Request.QueryString["RNDSD1"] != null ? Request.QueryString["RNDSD1"] : "");
			string rndsd2 = (Request.QueryString["RNDSD2"] != null ? Request.QueryString["RNDSD2"] : "");
			string pid1 = (Request.QueryString["PID1"] != null ? Request.QueryString["PID1"] : "");
			string pid2 = (Request.QueryString["PID2"] != null ? Request.QueryString["PID2"] : "");

			// For min/max
			string rnds = (rnds1 == "0" || rnds2 == "0" ? "" : " AND pru.ProjectRoundUnitID IN (" + rnds1 + (rnds2 != "" ? "," + rnds2 : "") + ")");

			if (rnds1 == "0") {
				rnds1 = " AND pru.ProjectRoundUnitID NOT IN (" + Request.QueryString["N"].ToString() + ")";
			} else {
				if (rnds1 != "") {
					rnds1 = " AND (pru.ProjectRoundUnitID IN (" + rnds1 + ")";
					if (pid1 != "") {
						rnds1 += " OR a.ProjectRoundUserID IN (" + pid1 + ")";
					}
					rnds1 += ")";
				} else {
					rnds1 = " AND a.ProjectRoundUserID IN (" + pid1 + ")";
				}
			}
			if (rnds2 == "") {
				rnds2 = "";
			} else {
				if (rnds2 == "0") {
					rnds2 = " AND pru.ProjectRoundUnitID NOT IN (" + Request.QueryString["N"].ToString() + ")";
				} else {
					if (rnds2 != "") {
						rnds2 = " AND (pru.ProjectRoundUnitID IN (" + rnds2 + ")";
						if (pid2 != "") {
							rnds2 += " OR a.ProjectRoundUserID IN (" + pid2 + ")";
						}
						rnds2 += ")";
					} else {
						rnds2 = " AND a.ProjectRoundUserID IN (" + pid2 + ")";
					}
				}
			}

			string join1 = "";
//			string query = "";
			if (rndsd1 != "") {
//				query = string.Format(
//					@"
				//SELECT d.Department,
//	d.DepartmentID,
//	d.SortString
				//FROM Department d
				//WHERE d.DepartmentID IN ({0})
				//ORDER BY d.SortString",
//					rndsd1
//				);
//				SqlDataReader rs2 = Db.rs(query);
//				while (rs2.Read()) {
				foreach (var d in departmentRepository.FindIn(rndsd1)) {
					join1 += string.Format(
						@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString,{0}) = '{1}' ",
//						rs2.GetString(2).Length,
//						rs2.GetString(2)
						d.SortString.Length,
						d.SortString
					);
				}
//				rs2.Close();
			}
			string join2 = "";
			if (rndsd2 != "") {
//				query = string.Format(
//					@"
				//SELECT d.Department,
//	d.DepartmentID,
//	d.SortString
				//FROM Department d
				//WHERE d.DepartmentID IN ({0})
				//ORDER BY d.SortString",
//					rndsd2
//				);
//				SqlDataReader rs2 = Db.rs(query);
//				while (rs2.Read()) {
				foreach (var d in departmentRepository.FindIn(rndsd2)) {
					join2 += string.Format(
						@"
INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID
INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID
INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID
INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString,{0}) = '{1}' ",
//						rs2.GetString(2).Length,
//						rs2.GetString(2)
						d.SortString.Length,
						d.SortString
					);
				}
//				rs2.Close();
			}

			int cx = 0;
			int type = 0;
			int q = 0;
			int o = 0;
			int rac = 0;
			int pl = 0;
//			Graph g;
			ExtendedGraph g;

			int steps = 0, GB = 3;
			bool stdev = (rnds2 == "");
			string groupBy = "";

//			query = string.Format(
//				@"
			//SELECT rp.Type,
//	(
//		SELECT COUNT(*)
//		FROM ReportPartComponent rpc
//		WHERE rpc.ReportPartID = rp.ReportPartID
//	),
//	rp.QuestionID,
//	rp.OptionID,
//	rp.RequiredAnswerCount,
//	rp.PartLevel
			//FROM ReportPart rp
			//WHERE rp.ReportPartID = {0}",
//				Request.QueryString["RPID"]
//			);
//			SqlDataReader rs = Db.rs(query, "eFormSqlConnection");
//			if (rs.Read()) {
			var r = reportRepository.ReadReportPart(Convert.ToInt32(Request.QueryString["RPID"]));
			if (r != null) {
//				type = rs.GetInt32(0);
//				cx = rs.GetInt32(1);
//				q = (rs.IsDBNull(2) ? 0 : rs.GetInt32(2));
//				o = (rs.IsDBNull(3) ? 0 : rs.GetInt32(3));
//				rac = (rs.IsDBNull(4) ? 0 : rs.GetInt32(4));
//				pl = (rs.IsDBNull(5) ? 0 : rs.GetInt32(5));
				type = r.Type;
				cx = r.Components.Capacity;
				q = r.Question.Id;
				o = r.Option.Id;
				rac = r.RequiredAnswerCount;
				pl = r.PartLevel;
			}
//			rs.Close();

			#region group stats

			int langID = 1;
			int minDT = 0;
			int maxDT = 0;

			if (type == 8) {
//				switch (GB) {
//						case 1: groupBy = "dbo.cf_yearWeek"; break;
//						case 2: groupBy = "dbo.cf_year2Week"; break;
//						case 3: groupBy = "dbo.cf_yearMonth"; break;
//						case 4: groupBy = "dbo.cf_year3Month"; break;
//						case 5: groupBy = "dbo.cf_year6Month"; break;
//						case 6: groupBy = "YEAR"; break;
//						case 7: groupBy = "dbo.cf_year2WeekEven"; break;
//				}
				groupBy = GroupFactory.GetGroupBy(GB);
//				g = new Graph(895, 440, "#FFFFFF");
				g = new ExtendedGraph(895, 440, "#FFFFFF");

//				query = string.Format(
//					@"
//				SELECT {0}(MAX(a.EndDT)) - {0}(MIN(a.EndDT)),
//	{0}(MIN(a.EndDT)),
//	{0}(MAX(a.EndDT))
//				FROM Answer a
//				INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
//				WHERE a.EndDT IS NOT NULL
//				AND a.EndDT >= '{1}'
//				AND a.EndDT < '{2}'
//				{3}",
//					groupBy,
//					Request.QueryString["FDT"],
//					Request.QueryString["TDT"],
//					rnds
//				);
//				SqlDataReader rs = Db.rs(query, "eFormSqlConnection");
//				if (rs.Read()) {
//					cx = Convert.ToInt32(rs.GetValue(0)) + 3;
//					minDT = rs.GetInt32(1);
//					maxDT = rs.GetInt32(2);
//				}
//				rs.Close();
				Answer answer = answerRepository.ReadByGroup(groupBy, Request.QueryString["FDT"], Request.QueryString["TDT"], rnds);
				if (answer != null) {
					cx = answer.DummyValue1 + 3;
					minDT = answer.DummyValue2;
					maxDT = answer.DummyValue3;
				}

//				query = string.Format(
//					@"
				//SELECT rpc.WeightedQuestionOptionID,
//	wqo.QuestionID,
//	wqo.OptionID,
//	wqo.YellowLow,
//	wqo.GreenLow,
//	wqo.GreenHigh,
//	wqo.YellowHigh
				//FROM ReportPartComponent rpc
				//INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
				//WHERE rpc.ReportPartID = {0}
				//ORDER BY rpc.SortOrder",
//					Request.QueryString["RPID"]
//				);
//				rs = Db.rs(query, "eFormSqlConnection");
//				while (rs.Read()) {
				foreach (var p in reportRepository.FindComponentsByPart(ConvertHelper.ToInt32(Request.QueryString["RPID"]))) {
//					query = string.Format(
//						@"
					//SELECT MAX(tmp2.VA + tmp2.SD),
//	MIN(tmp2.VA - tmp2.SD)
					//FROM (
//	SELECT AVG(tmp.V) AS VA,
//		STDEV(tmp.V) AS SD
//	FROM (
//		SELECT {0}(a.EndDT) AS DT,
//			AVG(av.ValueInt) AS V
//		FROM Answer a
//		INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
//		INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
//		WHERE a.EndDT IS NOT NULL
//		AND a.EndDT >= '{3}'
//		AND a.EndDT < '{4}'
//		{5}
//		GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
//	) tmp
//	GROUP BY tmp.DT
					//) tmp2",
//						groupBy,
					////						rs.GetInt32(1),
//						p.QuestionOption.Question.Id,
					////						rs.GetInt32(2),
//						p.QuestionOption.Option.Id,
//						Request.QueryString["FDT"],
//						Request.QueryString["TDT"],
//						rnds
//					);
//					SqlDataReader rs2 = Db.rs(query, "eFormSqlConnection");
//					if (rs2.Read()) {
//						g.setMinMax((float)Convert.ToDouble(rs2.GetValue(1)), (float)Convert.ToDouble(rs2.GetValue(0)));
//						g.roundMinMax();
//						//g.computeMinMax(0.01f, 0.01f);
//					} else {
//						g.setMinMax(0f, 100f);
//					}
//					rs2.Close();
					Answer a = answerRepository.ReadMinMax(groupBy, p.QuestionOption.Question.Id, p.QuestionOption.Option.Id, Request.QueryString["FDT"], Request.QueryString["TDT"], rnds);
					if (a != null) {
						g.setMinMax((float)Convert.ToDouble(a.Min), (float)Convert.ToDouble(a.Max));
						g.roundMinMax();
					} else {
						g.setMinMax(0f, 100f);
					}

//					if (!rs.IsDBNull(3) && !rs.IsDBNull(4) && !rs.IsDBNull(5) && !rs.IsDBNull(6)) {
//						if (rs.GetInt32(3) > 0) {
					if (p.QuestionOption.YellowLow > 0) {
//							g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(3))), "FFA8A8");                             // red
						g.drawBgFromString(g.minVal, Math.Min(g.maxVal, (float)Convert.ToDouble(p.QuestionOption.YellowLow)), "FFA8A8");                             // red
					}
//						if (rs.GetInt32(3) < 100 && rs.GetInt32(4) > 0) {
					if (p.QuestionOption.YellowLow < 100 && p.QuestionOption.GreenLow > 0) {
//							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(3))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(4))), "FFFEBE");    // yellow
						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.QuestionOption.YellowLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(p.QuestionOption.GreenLow)), "FFFEBE");    // yellow
					}
//						if (rs.GetInt32(4) < 100 && rs.GetInt32(5) > 0) {
					if (p.QuestionOption.GreenLow < 100 && p.QuestionOption.GreenHigh > 0) {
//							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(4))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(5))), "CCFFBB");   // green
						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.QuestionOption.GreenLow)), Math.Min(g.maxVal, (float)Convert.ToDouble(p.QuestionOption.GreenHigh)), "CCFFBB");   // green
					}
//						if (rs.GetInt32(5) < 100 && rs.GetInt32(6) > 0) {
					if (p.QuestionOption.GreenHigh < 100 && p.QuestionOption.YellowHigh > 0) {
//							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(5))), Math.Min(g.maxVal, (float)Convert.ToDouble(rs.GetInt32(6))), "FFFEBE"); // yellow
						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.QuestionOption.GreenHigh)), Math.Min(g.maxVal, (float)Convert.ToDouble(p.QuestionOption.YellowHigh)), "FFFEBE"); // yellow
					}
//						if (rs.GetInt32(6) < 100) {
					if (p.QuestionOption.YellowHigh < 100) {
//							g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(rs.GetInt32(6))), g.maxVal, "FFA8A8");                           // red
						g.drawBgFromString(Math.Max(g.minVal, (float)Convert.ToDouble(p.QuestionOption.YellowHigh)), g.maxVal, "FFA8A8");                           // red
					}
//					}
				}
//				rs.Close();

//				if (g.minVal != 0f) {
//					// Crunched graph sign
//					g.drawLine(20, 0, (int)g.maxH + 20, 0, (int)g.maxH + 23, 1);
//					g.drawLine(20, 0, (int)g.maxH + 23, -5, (int)g.maxH + 26, 1);
//					g.drawLine(20, -5, (int)g.maxH + 26, 5, (int)g.maxH + 32, 1);
//					g.drawLine(20, 5, (int)g.maxH + 32, 0, (int)g.maxH + 35, 1);
//					g.drawLine(20, 0, (int)g.maxH + 35, 0, (int)g.maxH + 38, 1);
//				}
				g.DrawWiggle();
			} else {
//				g = new Graph(895, 550, "#FFFFFF");
				g = new ExtendedGraph(895, 550, "#FFFFFF");
				g.setMinMax(0f, 100f);

				cx += 2;
			}

			steps = cx;
			g.computeSteping(steps);
			g.drawOutlines(11);
			g.drawAxis();

			cx = 0;

			if (type == 8) {
				#region Bottom string
//				int dx = 0;
//				for (int i = minDT; i <= maxDT; i++) {
//					dx++;
//
//					switch (GB) {
//						case 1:
//							{
//								int d = i;
//								int w = d % 52;
//								if (w == 0) {
//									w = 52;
//								}
//								string v = "v" + w + ", " + (d / 52);
//								g.drawBottomString(v, dx, true);
//								break;
//							}
//						case 2:
//							{
//								int d = i * 2;
//								int w = d % 52;
//								if (w == 0) {
//									w = 52;
//								}
//								string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52;
//								g.drawBottomString(v, dx, true);
//								break;
//							}
//						case 3:
//							{
//								int d = i;
//								int w = d % 12;
//								if (w == 0) {
//									w = 12;
//								}
//								string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12);
//								g.drawBottomString(v, dx, true);
//								break;
//							}
//						case 4:
//							{
//								int d = i * 3;
//								int w = d % 12;
//								if (w == 0) {
//									w = 12;
//								}
//								string v = "Q" + (w / 3) + ", " + ((d - w) / 12);
//								g.drawBottomString(v, dx, true);
//								break;
//							}
//						case 5:
//							{
//								int d = i * 6;
//								int w = d % 12;
//								if (w == 0) {
//									w = 12;
//								}
//								string v = ((d - w) / 12) + "/" + (w / 6);
//								g.drawBottomString(v, dx, true);
//								break;
//							}
//						case 6:
//							{
//								g.drawBottomString(i.ToString(), dx, true);
//								break;
//							}
//						case 7:
//							{
//								int d = i * 2;
//								int w = d % 52;
//								if (w == 0) {
//									w = 52;
//								}
//								string v = "v" + w + "-" + (w + 1) + ", " + ((d + 1) - (d % 52)) / 52;
//								g.drawBottomString(v, dx, true);
//								break;
//							}
//					}
//				}
				#endregion
				g.DrawBottomString(minDT, maxDT, GB);

				int bx = 0;
//				query = string.Format(
//					@"
				//SELECT rpc.WeightedQuestionOptionID,
//	wqol.WeightedQuestionOption,
//	wqo.QuestionID,
//	wqo.OptionID
				//FROM ReportPartComponent rpc
				//INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID
				//INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = {0}
				//WHERE rpc.ReportPartID = {1}
				//ORDER BY rpc.SortOrder",
//					langID,
//					Request.QueryString["RPID"]
//				);
//				rs = Db.rs(query, "eFormSqlConnection");
//				if (rs.Read()) {
				var p = reportRepository.ReadComponentByPartAndLanguage(ConvertHelper.ToInt32(Request.QueryString["RPID"]), langID);
				if (p != null) {
//					g.drawAxisExpl(rs.GetString(1) + ", " + (langID == 1 ? "medelvärde" : "mean value") + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), 20, false, false);
					g.drawAxisExpl(p.QuestionOption.Languages[0].Question + ", " + (langID == 1 ? "medelvärde" : "mean value") + (stdev ? " " + HttpUtility.HtmlDecode("&plusmn;") + "SD" : ""), 20, false, false);
					g.drawAxis(false);

					g.drawColorExplBox(Request.QueryString["R1"].ToString(), 4, 300, 20);

					float lastVal = -1f;
					float lastStd = -1f;
					int lastCX = 1;
					cx = 1;
					int lastDT = minDT - 1;
					string yearFrom = Request.QueryString["FDT"];
					string yearTo = Request.QueryString["TDT"];
					
					#region Data loop 1
//					query = string.Format(
//						@"
//					SELECT tmp.DT,
//	AVG(tmp.V),
//	COUNT(tmp.V),
//	STDEV(tmp.V)
//					FROM (
//	SELECT {0}(a.EndDT) AS DT,
//		AVG(av.ValueInt) AS V
//	FROM Answer a
//	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
//	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
//	{3}
//	WHERE a.EndDT IS NOT NULL
//	AND a.EndDT >= '{4}'
//	AND a.EndDT < '{5}'
//	{6}
//	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
//					) tmp
//					GROUP BY tmp.DT
//					ORDER BY tmp.DT",
//						groupBy,
//						p.QuestionOption.Question.Id,
//						p.QuestionOption.Option.Id,
//						join1,
//						yearFrom,
//						yearTo,
//						rnds1
//					);
//					SqlDataReader rs2 = Db.rs(query, "eFormSqlConnection");
//					while (rs2.Read()) {
					foreach (var a in answerRepository.FindByQuestionAndOptionGrouped3(groupBy, p.QuestionOption.Question.Id, p.QuestionOption.Option.Id, join1, yearFrom, yearTo, rnds1)) {
//						while (lastDT + 1 < rs2.GetInt32(0)) {
						while (lastDT + 1 < a.DT) {
							lastDT++;
							cx++;
						}

//						if (rs2.GetInt32(2) >= rac) {
						if (a.CountV >= rac) {
//							float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
//							float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));
							float newVal = (a.AverageV == -1 ? -1f : (float)Convert.ToDouble(a.AverageV));
							float newStd = (a.StandardDeviation == -1 ? -1f : (float)Convert.ToDouble(a.StandardDeviation));

							if (stdev) {
								g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
								g.drawLine(20, cx * g.steping, Convert.ToInt32(g.maxH - ((newVal - newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
								g.drawLine(20, cx * g.steping - 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), cx * g.steping + 10, Convert.ToInt32(g.maxH - ((newVal + newStd) - g.minVal) / (g.maxVal - g.minVal) * g.maxH), 1);
							}

							if (newVal != -1f) {
								if (lastVal != -1f) {
									g.drawStepLine(4, lastCX, lastVal, cx, newVal, 3);
								}
								if (rnds2 == "") {
//									g.drawBottomString("\n\n\n\n\n\nn=" + rs2.GetInt32(2).ToString(), cx, false);
									g.drawBottomString("\n\n\n\n\n\nn=" + a.CountV.ToString(), cx, false);
								}
								lastCX = cx;
							}
							lastVal = newVal;
							lastStd = newStd;

							g.drawCircle(cx, newVal);
						}
//						lastDT = rs2.GetInt32(0);
						lastDT = a.DT;
						cx++;
					}
//					rs2.Close();
					#endregion

					if (rnds2 != "") {
						g.drawColorExplBox(Request.QueryString["R2"].ToString(), 5, 600, 20);

						lastVal = -1f;
						lastStd = -1f;
						lastCX = 1;
						cx = 1;
						lastDT = minDT - 1;
						#region Data loop 2
//						query = string.Format(
//							@"
//						SELECT tmp.DT,
//	AVG(tmp.V),
//	COUNT(tmp.V),
//	STDEV(tmp.V)
//						FROM (
//	SELECT {0}(a.EndDT) AS DT,
//		AVG(av.ValueInt) AS V
//	FROM Answer a
//	INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = {1} AND av.OptionID = {2}
//	INNER JOIN ProjectRoundUnit pru ON a.ProjectRoundUnitID = pru.ProjectRoundUnitID
//	{3}
//	WHERE a.EndDT IS NOT NULL
//	AND a.EndDT >= '{4}'
//	AND a.EndDT < '{5}'
//	{6}
//	GROUP BY a.ProjectRoundUserID, {0}(a.EndDT)
//						) tmp
//						GROUP BY tmp.DT
//						ORDER BY tmp.DT",
//							groupBy,
//							p.QuestionOption.Question.Id,
//							p.QuestionOption.Option.Id,
//							join2,
//							Request.QueryString["FDT"],
//							Request.QueryString["TDT"],
//							rnds2
//						);
//						rs2 = Db.rs(query, "eFormSqlConnection");
//						while (rs2.Read()) {
						foreach (var a in answerRepository.FindByQuestionAndOptionGrouped3(groupBy, p.QuestionOption.Question.Id, p.QuestionOption.Option.Id, join2, Request.QueryString["FDT"], Request.QueryString["TDT"], rnds2)) {
//							while (lastDT + 1 < rs2.GetInt32(0)) {
							while (lastDT + 1 < a.DT) {
								lastDT++;
								cx++;
							}

//							if (rs2.GetInt32(2) >= rac) {
							if (a.CountV >= rac) {
//								float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
								float newVal = (a.AverageV == -1 ? -1f : (float)Convert.ToDouble(a.AverageV));

								if (newVal != -1f) {
									if (lastVal != -1f) {
										g.drawStepLine(5, lastCX, lastVal, cx, newVal, 3);
									}
									lastCX = cx;
								}
								lastVal = newVal;

								g.drawCircle(cx, newVal);
							}
//							lastDT = rs2.GetInt32(0);
							lastDT = a.DT;
							cx++;
						}
//						rs2.Close();
						#endregion
					}
					bx++;
				}
//				rs.Close();
			}

			#endregion

			// g.printCopyRight();
			g.render();
		}
	}
}