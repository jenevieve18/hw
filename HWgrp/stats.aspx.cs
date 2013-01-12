//	<file>
//		<license></license>
//		<owner name="Jens Pettersson" email=""/>
//		<owner name="Ian Escarro" email="ian.escarro@gmail.com"/>
//	</file>

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using HW.Core;

namespace HWgrp
{
	public partial class stats : System.Web.UI.Page
	{
		protected IList<ReportPartLanguage> reportParts = null;
		int sponsorID = 0;
		ILanguageRepository langRepository = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		IProjectRepository projRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();

		protected IList<Department> departments;

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			
			if (sponsorID != 0) {
				if (!IsPostBack) {
					int pruid = 0;
					foreach (var l in langRepository.FindBySponsor(sponsorID)) {
						LangID.Items.Add(new ListItem(l.Language.Name, l.Language.Id.ToString()));
						if (l.Id == 2) {
							LangID.SelectedValue = l.Language.Id.ToString();
						}
						pruid = l.SponsorProjectRoundUnit.ProjectRoundUnit.Id;
					}
					
					int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
					foreach (var p in sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID)) {
						ProjectRoundUnitID.Items.Add(new ListItem(p.Navigation, p.Id.ToString()));
					}
					GroupBy.SelectedValue = "7";

					for (int i = 2005; i <= DateTime.Now.Year; i++) {
						FromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
						ToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
					}
					FromYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
					ToYear.SelectedValue = DateTime.Now.Year.ToString();

					foreach (var s in sponsorRepository.FindBySponsor(sponsorID)) {
						BQ.Items.Add(new ListItem(s.Question.Internal, s.Id.ToString()));
					}
					BQ.SelectedIndex = 0;
				}

				Org.Controls.Add(new LiteralControl("<br>"));
				Org.Controls.Add(new LiteralControl("<table border='0' cellspacing='0' cellpadding='0' style='border:0;border-collapse:collapse;border-spacing:0'><tr><td colspan='3'>" + HttpContext.Current.Session["Sponsor"] + "</td><tr>"));
				bool[] DX = new bool[8];
				int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
				foreach (var d in departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID)) {
					Org.Controls.Add(new LiteralControl("<tr>"));
					Org.Controls.Add(new LiteralControl("<td>"));
					CheckBox O = new CheckBox();
					O.ID = "DID" + d.Id;
					Org.Controls.Add(O);
					Org.Controls.Add(new LiteralControl("</td>"));
					Org.Controls.Add(new LiteralControl("<td>" + d.Name + "</td>"));
					
					Org.Controls.Add(new LiteralControl("<td>"));
					int depth = d.Depth;
					DX[depth] = d.Siblings > 0;

					string img = "";
					for (int i = 1; i <= depth; i++) {
						img += string.Format("<img src='img/{0}.gif' width='19' height='20'>", i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null"));
					}
					Org.Controls.Add(new LiteralControl("<table border='0' cellspacing='0' cellpadding='0' style='border:0;border-collapse:collapse;border-spacing:0'><tr><td>" + img + "</td><td>" + d.Name + "</td></tr></table>"));
					Org.Controls.Add(new LiteralControl("</td>"));
					Org.Controls.Add(new LiteralControl("</tr>"));
				}
				Org.Controls.Add(new LiteralControl("</table>"));
			} else {
				HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}

			Execute.Click += new EventHandler(Execute_Click);
		}

		void Execute_Click(object sender, EventArgs e)
		{
			int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(ProjectRoundUnitID.SelectedValue);
			
			string URL = GetURL(Convert.ToInt32(Grouping.SelectedValue));
			reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
			int cx = 0;
			foreach (var r in reportParts) {
				if (cx == 0) {
					StatsImg.Controls.Add(new LiteralControl("<div>&nbsp;<br>&nbsp;<br></div>"));
				} else {
					StatsImg.Controls.Add(new LiteralControl("<div style='page-break-before:always;'>&nbsp;<br>&nbsp;<br></div>"));
				}
				
				HtmlTable table = new HtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				IHGHtmlTableCell headerCell = new IHGHtmlTableCell(r.Subject) { Align = "Center", VAlign = "Middle", Height = "140" };
				headerCell.Style["font-size"] = "24px";
				table.Rows.Add(new IHGHtmlTableRow(headerCell));
				IHGHtmlTableCell subjectCell = new IHGHtmlTableCell(r.Subject);
				subjectCell.Style["font-size"] = "18px";
				table.Rows.Add(new IHGHtmlTableRow(subjectCell));
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Header.Replace("\r", "").Replace("\n", "<br>"))));

                string imgID = "img" + cx;
				IHGRadioButtonList plotType = new IHGRadioButtonList();
				plotType.ID = "plt" + cx;
				plotType.Items.Add("LinePlot");
				plotType.Items.Add("BoxPlot");
                plotType.Attributes.Add("onclick", string.Format("javascript:xxx('{0}', '{1}', '{2}')", plotType.ID, imgID, GetReportImageUrl(r.Id, URL)));
				
				IHGHtmlTableCell plotTypeCell = new IHGHtmlTableCell(plotType);
				plotTypeCell.Style["font-size"] = "10px";
				table.Rows.Add(new IHGHtmlTableRow(plotTypeCell));
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(new HtmlImage() { ID = imgID, Src = GetReportImageUrl(r.Id, URL) })));
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Footer.Replace("\r", "").Replace("\n", "<br>"))));
				StatsImg.Controls.Add(table);
                cx++;
			}
		}

		#region export
		/*
    private void exportStats(int GRPNG, int PRUID, int SPONS, int SID, string GID, int langID)
    {
        System.Text.StringBuilder export = new System.Text.StringBuilder();

        int COUNT = 0;
        Hashtable desc = new Hashtable();
        Hashtable join = new Hashtable();
        ArrayList item = new ArrayList();
        string extraDesc = "";

        #region Grouping
        switch (GRPNG)
        {
            case 0:
                {
                    string tmpDesc = ""; int sslen = 0; string tmpSS = "";

                    SqlDataReader rs2 = Db.rs("SELECT " +
                        (Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
                        "LEN(d.SortString), " +
                        "d.SortString " +
                        "FROM Department d " +
                        (SPONS != -1 ?
                        "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
                        "WHERE sad.SponsorAdminID = " + SPONS + " "
                        :
                        "WHERE d.SponsorID = " + SID + " "
                        ) +
                        "ORDER BY LEN(d.SortString)");
                    while (rs2.Read())
                    {
                        if (sslen == 0)
                        {
                            sslen = rs2.GetInt32(1);
                        }
                        if (sslen == rs2.GetInt32(1))
                        {
                            tmpDesc += (tmpDesc != "" ? ", " : "") + rs2.GetString(0) + "+";
                            tmpSS += (tmpSS != "" ? "," : "") + "'" + rs2.GetString(2) + "'";
                        }
                        else
                        {
                            break;
                        }
                    }
                    rs2.Close();

                    item.Add("1");
                    desc.Add("1", tmpDesc);
                    join.Add("1", "" +
                        "INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
                        "INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID " +
                        "INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
                        //"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS + " ");
                        "INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + sslen + ") IN (" + tmpSS + ") ");
                    COUNT++;
                    break;
                }
            case 1:
                {
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        (Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
                        "d.DepartmentID " +
                        "FROM Department d " +
                        (SPONS != -1 ?
                        "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
                        "WHERE sad.SponsorAdminID = " + SPONS + " "
                        :
                        "WHERE d.SponsorID = " + SID + " "
                        ) +
                        "AND d.DepartmentID IN (" + GID + ") " +
                        "ORDER BY d.SortString");
                    while (rs2.Read())
                    {
                        item.Add(rs2.GetInt32(1).ToString());
                        desc.Add(rs2.GetInt32(1).ToString(), rs2.GetString(0));
                        join.Add(rs2.GetInt32(1).ToString(), "" +
                            "INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
                            "INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
                            "INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID AND HWup.DepartmentID = " + rs2.GetInt32(1) + " " +
                            "INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS + " ");
                        COUNT++;
                    }
                    rs2.Close();
                    break;
                }
            case 2:
                {
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        (Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
                        "d.DepartmentID, " +
                        "d.SortString " +
                        "FROM Department d " +
                        (SPONS != -1 ?
                        "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
                        "WHERE sad.SponsorAdminID = " + SPONS + " "
                        :
                        "WHERE d.SponsorID = " + SID + " "
                        ) +
                        "AND d.DepartmentID IN (" + GID + ") " +
                        "ORDER BY d.SortString");
                    while (rs2.Read())
                    {
                        item.Add(rs2.GetInt32(1).ToString());
                        desc.Add(rs2.GetInt32(1).ToString(), rs2.GetString(0));
                        join.Add(rs2.GetInt32(1).ToString(), "" +
                            "INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
                            "INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
                            "INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID " +
                            //"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS + " " +
                            //"INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + rs2.GetString(2).Length + ") = '" + rs2.GetString(2) + "' ");
                            "INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + rs2.GetString(2).Length + ") = '" + rs2.GetString(2) + "' ");
                        COUNT++;
                    }
                    rs2.Close();
                    break;
                }
            case 3:
                {
                    string tmpSelect = "", tmpJoin = "", tmpOrder = "";

                    string tmpDesc = ""; int sslen = 0; string tmpSS = "";

                    SqlDataReader rs2 = Db.rs("SELECT " +
                        (Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
                        "LEN(d.SortString), " +
                        "d.SortString " +
                        "FROM Department d " +
                        (SPONS != -1 ?
                        "INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
                        "WHERE sad.SponsorAdminID = " + SPONS + " "
                        :
                        "WHERE d.SponsorID = " + SID + " "
                        ) +
                        "ORDER BY LEN(d.SortString)");
                    while (rs2.Read())
                    {
                        if (sslen == 0)
                        {
                            sslen = rs2.GetInt32(1);
                        }
                        if (sslen == rs2.GetInt32(1))
                        {
                            tmpDesc += (tmpDesc != "" ? ", " : "") + rs2.GetString(0) + "+";
                            tmpSS += (tmpSS != "" ? "," : "") + "'" + rs2.GetString(2) + "'";
                        }
                        else
                        {
                            break;
                        }
                    }
                    rs2.Close();

                    rs2 = Db.rs("SELECT BQ.BQID, BQ.Internal FROM BQ WHERE BQ.BQID IN (" + GID.Replace("'", "") + ")");
                    GID = "";
                    while (rs2.Read())
                    {
                        GID += (GID != "" ? "," : "") + rs2.GetInt32(0);

                        extraDesc += (extraDesc != "" ? " / " : "") + rs2.GetString(1);

                        tmpSelect += (tmpSelect != "" ? " ," : "") + "ba" + rs2.GetInt32(0) + ".BAID,ba" + rs2.GetInt32(0) + ".Internal ";
                        tmpJoin += (tmpJoin != "" ? "INNER JOIN BA ba" + rs2.GetInt32(0) + " ON ba" + rs2.GetInt32(0) + ".BQID = " + rs2.GetInt32(0) + " " : "FROM BA ba" + rs2.GetInt32(0) + " ");
                        tmpOrder += (tmpOrder != "" ? ", ba" + rs2.GetInt32(0) + ".SortOrder" : "WHERE ba" + rs2.GetInt32(0) + ".BQID = " + rs2.GetInt32(0) + " ORDER BY ba" + rs2.GetInt32(0) + ".SortOrder");
                    }
                    rs2.Close();
                    string[] GIDS = GID.Split(',');

                    rs2 = Db.rs("SELECT " +
                        tmpSelect +
                        tmpJoin +
                        tmpOrder);
                    while (rs2.Read())
                    {
                        string key = "", txt = "", sql = "" +
                            "INNER JOIN healthWatch..UserProjectRoundUserAnswer HWa ON a.AnswerID = HWa.AnswerID " +
                            "INNER JOIN healthWatch..UserProjectRoundUser HWu ON HWa.ProjectRoundUserID = HWu.ProjectRoundUserID AND HWu.ProjectRoundUnitID = " + PRUID + " " +
                            "INNER JOIN healthWatch..UserProfile HWup ON HWa.UserProfileID = HWup.UserProfileID " +
                            //"INNER JOIN healthWatch..SponsorAdminDepartment HWsad ON HWup.DepartmentID = HWsad.DepartmentID AND HWsad.SponsorAdminID = " + SPONS;
                            "INNER JOIN healthWatch..Department HWd ON HWup.DepartmentID = HWd.DepartmentID AND LEFT(HWd.SortString," + sslen + ") IN (" + tmpSS + ") ";

                        for (int i = 0; i < GIDS.Length; i++)
                        {
                            key += (key != "" ? "X" : "") + rs2.GetInt32(0 + i * 2);
                            txt += (txt != "" ? " / " : "") + rs2.GetString(1 + i * 2);
                            sql += "INNER JOIN healthWatch..UserProfileBQ HWp" + GIDS[i] + " ON HWup.UserProfileID = HWp" + GIDS[i] + ".UserProfileID AND HWp" + GIDS[i] + ".BQID = " + GIDS[i] + " AND HWp" + GIDS[i] + ".ValueInt = " + rs2.GetInt32(0 + i * 2);
                        }
                        COUNT++;

                        item.Add(key);
                        desc.Add(key, txt);
                        join.Add(key, sql);
                    }
                    rs2.Close();

                    break;
                }
        }
        #endregion

        SqlDataReader rpid = Db.rs("SELECT " +
				"rp.ReportPartID, " +
				"rpl.Subject, " +
				"rpl.Header, " +
				"rpl.Footer, " +
				"rp.Type " +
				"FROM ProjectRoundUnit pru " +
				"INNER JOIN Report r ON r.ReportID = pru.ReportID " +
				"INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
				"INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID " +
				"AND rpl.LangID = dbo.cf_unitLangID(pru.ProjectRoundUnitID) " +
				"WHERE pru.ProjectRoundUnitID = " + PRUID + " " +
				"ORDER BY rp.SortOrder", "eFormSqlConnection");
        while (rpid.Read())
        {
             SqlDataReader rs = Db.rs("SELECT " +
                "rpc.WeightedQuestionOptionID, " +	// 0
                "wqol.WeightedQuestionOption, " +
                "wqo.QuestionID, " +
                "wqo.OptionID " +
                "FROM ReportPartComponent rpc " +
                "INNER JOIN WeightedQuestionOption wqo ON rpc.WeightedQuestionOptionID = wqo.WeightedQuestionOptionID " +
                "INNER JOIN WeightedQuestionOptionLang wqol ON wqo.WeightedQuestionOptionID = wqol.WeightedQuestionOptionID AND wqol.LangID = " + langID + " " +
                "WHERE rpc.ReportPartID = " + rpid.GetInt32(0) + " " +
                "ORDER BY rpc.SortOrder", "eFormSqlConnection");
            if (rs.Read())
            {
                int bx = 0;

                foreach (string i in item)
                {
                    export.Append((string)desc[i]);
                    float lastVal = -1f;
                    float lastStd = -1f;
                    int lastCX = 1;
                    int cx = 1;
                    int lastDT = minDT - 1;
                    #region Data loop
                    string SQL = "SELECT " +
                        "tmp.DT, " +
                        "AVG(tmp.V), " +
                        "COUNT(tmp.V), " +
                        "STDEV(tmp.V) " +
                        "FROM (" +
                        "SELECT " +
                        "" + groupBy + "(a.EndDT) AS DT, " +
                        "AVG(av.ValueInt) AS V " +
                        "FROM Answer a " +
                        join[i] +
                        "INNER JOIN AnswerValue av ON a.AnswerID = av.AnswerID AND av.QuestionID = " + rs.GetInt32(2) + " AND av.OptionID = " + rs.GetInt32(3) + " " +
                        "WHERE a.EndDT IS NOT NULL " +
                        "GROUP BY a.ProjectRoundUserID, " + groupBy + "(a.EndDT) " +
                        ") tmp " +
                        "GROUP BY tmp.DT " +
                        "ORDER BY tmp.DT";
                    SqlDataReader rs2 = Db.rs(SQL, "eFormSqlConnection");
                    while (rs2.Read())
                    {
                        //if (lastDT == 0) { lastDT = rs2.GetInt32(0); }

                        while (lastDT + 1 < rs2.GetInt32(0))
                        {
                            lastDT++;
                            cx++;
                        }

                        if (rs2.GetInt32(2) >= rac)
                        {
                            #region Bottom string
                            if (COUNT == 1)
                            {
                                switch (GB)
                                {
                                    case 1:
                                        {
                                            int d = rs2.GetInt32(0);
                                            int w = d % 52;
                                            if (w == 0)
                                            {
                                                w = 52;
                                            }
                                            string v = "v" + w + ", " + (d / 52) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
                                            g.drawBottomString(v, cx, true);
                                            break;
                                        }
                                    case 2:
                                        {
                                            int d = rs2.GetInt32(0) * 2;
                                            int w = d % 52;
                                            if (w == 0)
                                            {
                                                w = 52;
                                            }
                                            //string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52;
                                            string v = "v" + (w - 1) + "-" + w + ", " + (d - ((d - 1) % 52)) / 52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
                                            g.drawBottomString(v, cx, true);
                                            break;
                                        }
                                    case 3:
                                        {
                                            int d = rs2.GetInt32(0);
                                            int w = d % 12;
                                            if (w == 0)
                                            {
                                                w = 12;
                                            }
                                            string v = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[w - 1] + ", " + ((d - w) / 12) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
                                            g.drawBottomString(v, cx, true);
                                            break;
                                        }
                                    case 4:
                                        {
                                            int d = rs2.GetInt32(0) * 3;
                                            int w = d % 12;
                                            if (w == 0)
                                            {
                                                w = 12;
                                            }
                                            string v = "Q" + (w / 3) + ", " + ((d - w) / 12) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
                                            g.drawBottomString(v, cx, true);
                                            break;
                                        }
                                    case 5:
                                        {
                                            int d = rs2.GetInt32(0) * 6;
                                            int w = d % 12;
                                            if (w == 0)
                                            {
                                                w = 12;
                                            }
                                            string v = ((d - w) / 12) + "/" + (w / 6) + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
                                            g.drawBottomString(v, cx, true);
                                            break;
                                        }
                                    case 6:
                                        {
                                            g.drawBottomString(rs2.GetInt32(0).ToString() + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : ""), cx, true);
                                            break;
                                        }
                                    case 7:
                                        {
                                            int d = rs2.GetInt32(0) * 2;
                                            int w = d % 52;
                                            if (w == 0)
                                            {
                                                w = 52;
                                            }
                                            //string v = "v" + (w-1) + "-" + w + "\n" + (d-(d-1)%52)/52;
                                            string v = "v" + w + "-" + (w + 1) + ", " + ((d + 1) - (d % 52)) / 52 + (COUNT == 1 ? ", n = " + rs2.GetInt32(2) : "");
                                            g.drawBottomString(v, cx, true);
                                            break;
                                        }
                                }
                            }
                            #endregion

                            float newVal = (rs2.IsDBNull(1) ? -1f : (float)Convert.ToDouble(rs2.GetValue(1)));
                            float newStd = (rs2.IsDBNull(3) ? -1f : (float)Convert.ToDouble(rs2.GetValue(3)));

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
                        lastDT = rs2.GetInt32(0);
                        cx++;
                    }
                    rs2.Close();
                    #endregion

                    bx++;
                }
            }
            rs.Close();
        }
        rpid.Close();
    }
		 */
		#endregion

		protected string GetURL(int grouping)
		{
			string URL = "";
			switch (grouping) {
				case 1:
				case 2:
					int sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
					var departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID);
					foreach (var d in departments) {
						if (((CheckBox)Org.FindControl("DID" + d.Id.ToString())).Checked) {
							URL += "," + d.Id;
						}
					}

					URL = "&GID=0" + URL;
					break;
				case 3:
					foreach (var s in sponsorRepository.FindBySponsor(sponsorID)) {
						if (BQ.Items.FindByValue(s.Id.ToString()).Selected) {
							URL += "," + s.Id;
						}
					}
					URL = "&GID=0" + URL;
					break;
			}
			return URL;
		}
		
		protected string GetReportImageUrl(int id, string URL)
		{
			string s = HttpContext.Current.Request.QueryString["Plot"] != null ? "&Plot=" + HttpContext.Current.Request["Plot"] : "";
			return "reportImage.aspx?LangID=" + Convert.ToInt32(LangID.SelectedValue) + "&FY=" + FromYear.SelectedValue + "&TY=" + ToYear.SelectedValue + "&SAID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + "&SID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "&" + (Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "Anonymized=1&" : "") + "STDEV=" + (STDEV.Checked ? "1" : "0") + "&GB=" + GroupBy.SelectedValue + "&RPID=" + id + "&PRUID=" + ProjectRoundUnitID.SelectedValue + URL + "&GRPNG=" + Convert.ToInt32(Grouping.SelectedValue) + s;
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			Org.Visible = (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2");
			BQ.Visible = (Grouping.SelectedValue == "3");
		}
	}
}