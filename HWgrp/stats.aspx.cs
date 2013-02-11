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
		IList<Department> departments;
		IList<SponsorBackgroundQuestion> questions;
		int sponsorID = 0;
		int sponsorAdminID = 0;
		ILanguageRepository langRepository = AppContext.GetRepositoryFactory().CreateLanguageRepository();
		IProjectRepository projRepository = AppContext.GetRepositoryFactory().CreateProjectRepository();
		ISponsorRepository sponsorRepository = AppContext.GetRepositoryFactory().CreateSponsorRepository();
		IDepartmentRepository departmentRepository = AppContext.GetRepositoryFactory().CreateDepartmentRepository();
		IReportRepository reportRepository = AppContext.GetRepositoryFactory().CreateReportRepository();
		
		public IList<SponsorProjectRoundUnitLanguage> Languages {
			set {
				int pruid = 0;
				foreach (var l in value) {
					LangID.Items.Add(new ListItem(l.Language.Name, l.Language.Id.ToString()));
					if (l.Id == 2) {
						LangID.SelectedValue = l.Language.Id.ToString();
					}
					pruid = l.SponsorProjectRoundUnit.ProjectRoundUnit.Id;
				}
			}
		}
		
		public IList<SponsorProjectRoundUnit> ProjectRoundUnits {
			set {
				foreach (var p in value) {
					ProjectRoundUnitID.Items.Add(new ListItem(p.Navigation, p.ProjectRoundUnit.Id.ToString()));
				}
				GroupBy.SelectedValue = "7";
			}
		}
		
		public IList<SponsorBackgroundQuestion> BackgroundQuestions {
			set {
				this.questions = value;
				foreach (var s in questions) {
					BQ.Items.Add(new ListItem(s.Question.Internal, s.Id.ToString()));
				}
				BQ.SelectedIndex = 0;
			}
		}
		
		public IList<Department> Departments {
			set {
				this.departments = value;
				Org.Controls.Add(new LiteralControl("<br>"));
				IHGHtmlTable table = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(HttpContext.Current.Session["Sponsor"].ToString()) { ColSpan = 3 }));
				bool[] DX = new bool[8];
				foreach (var d in departments) {
					IHGHtmlTableRow row = new IHGHtmlTableRow(new IHGHtmlTableCell(new CheckBox { ID = "DID" + d.Id }), new IHGHtmlTableCell(d.Name));
					
					int depth = d.Depth;
					DX[depth] = d.Siblings > 0;

					IList<Control> images = new List<Control>();
					for (int i = 1; i <= depth; i++) {
						images.Add(new HtmlImage { Src = string.Format("img/{0}.gif", i == depth ? (DX[i] ? "T" : "L") : (DX[i] ? "I" : "null")), Width = 19, Height = 20 });
					}
					IHGHtmlTable imagesTable = new IHGHtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
					imagesTable.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(images), new IHGHtmlTableCell(d.Name)));
					
					IHGHtmlTableCell imageCell = new IHGHtmlTableCell(imagesTable);
					row.Cells.Add(imageCell);
					table.Rows.Add(row);
				}
				Org.Controls.Add(table);
			}
		}
		
		IList<BaseModel> SelectedDepartments {
			get {
				var selectedDepartments = new List<BaseModel>();
//				var departments = sponsorAdminID != -1 ? departmentRepository.FindBySponsorWithSponsorAdmin(sponsorID, sponsorAdminID) : departmentRepository.FindBySponsorOrderedBySortString(sponsorID);
				foreach (var d in departments) {
					if (((CheckBox)Org.FindControl("DID" + d.Id.ToString())).Checked) {
						selectedDepartments.Add(d);
					}
				}
				return selectedDepartments;
			}
		}
		
		IList<BaseModel> SelectedQuestions {
			get {
				var selectedQuestions = new List<BaseModel>();
//				foreach (var q in questions) {
				foreach (var q in sponsorRepository.FindBySponsor(sponsorID)) {
					if (BQ.Items.FindByValue(q.Id.ToString()).Selected) {
						selectedQuestions.Add(q);
					}
				}
				return selectedQuestions;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);
			sponsorAdminID = Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]);
			
			if (sponsorID != 0) {
				if (!IsPostBack) {
					Languages = langRepository.FindBySponsor(sponsorID);

					int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
					ProjectRoundUnits = sponsorRepository.FindBySponsorAndLanguage(sponsorID, selectedLangID);

					for (int i = 2005; i <= DateTime.Now.Year; i++) {
						FromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
						ToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
					}
					FromYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
					ToYear.SelectedValue = DateTime.Now.Year.ToString();
					
					BackgroundQuestions = sponsorRepository.FindBySponsor(sponsorID);
				}
				Departments = departmentRepository.FindBySponsorWithSponsorAdminInDepth(sponsorID, sponsorAdminID);
			} else {
				HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}
			Execute.Click += new EventHandler(Execute_Click);
		}
		
		public void SetReportPartLanguages(IList<ReportPartLanguage> reportParts, IList<BaseModel> urlModels)
		{
			var selectedDepartments = SelectedDepartments;
			string URL = GetURL(urlModels);
			int cx = 0;
			
			if (selectedDepartments.Count == 1) {
				var allNone = new CheckBox() { ID = "selectAll" };
				
				IHGRadioButtonList plotType = new IHGRadioButtonList();
				plotType.ID = "graphTypes";
				plotType.Items.Add(new ListItem("Line Plot", "LinePlot") { Selected = true });
				plotType.Items.Add(new ListItem("Box Plot", "BoxPlot"));
				StatsImg.Controls.Add(allNone);
				StatsImg.Controls.Add(plotType);
			} else {
				StatsImg.Controls.Add(new LiteralControl(""));
			}
			
//			var allNone = new CheckBox() { ID = "selectAll" };
//
//			var graphTypes = new DropDownList() { ID = "graphTypes" };
//			graphTypes.Items.Add(new ListItem("Line Plot", "LinePlot"));
//			if (selectedDepartments.Count == 1) {
//				graphTypes.Items.Add(new ListItem("Box Plot", "BoxPlot"));
//			}
//
//			StatsImg.Controls.Add(allNone);
//			StatsImg.Controls.Add(new LiteralControl("Graph Type: "));
//			StatsImg.Controls.Add(graphTypes);
			
			foreach (var r in reportParts) {
				if (cx == 0) {
					StatsImg.Controls.Add(new LiteralControl("<div>&nbsp;<br>&nbsp;<br></div>"));
				} else {
					StatsImg.Controls.Add(new LiteralControl("<div style='page-break-before:always;'>&nbsp;<br>&nbsp;<br></div>"));
				}
				
				string imgID = "img" + cx;

				HtmlTable table = new HtmlTable { Border = 0, CellSpacing = 0, CellPadding = 0 };
				IHGHtmlTableCell headerCell = new IHGHtmlTableCell(r.Subject) { Align = "Center", VAlign = "Middle", Height = "140", FontSize = "24px" };
				table.Rows.Add(new IHGHtmlTableRow(headerCell));
				
//				IHGHtmlTableCell subjectCell = new IHGHtmlTableCell(new CheckBox() { ID = "chk" + cx }, new LiteralControl(r.Subject), new HtmlInputHidden() { Value = GetReportImageUrl(r.ReportPart.Id, "reportImage", URL) }) { FontSize = "18px" };
				IHGHtmlTableCell subjectCell = new IHGHtmlTableCell() { FontSize = "18px" };
				if (selectedDepartments.Count == 1) {
					subjectCell.Controls.Add(new CheckBox() { ID = "chk" + cx });
					subjectCell.Controls.Add(new LiteralControl(r.Subject));
					subjectCell.Controls.Add(new HtmlInputHidden() { Value = GetReportImageUrl(r.ReportPart.Id, "reportImage", URL) });
				} else {
					subjectCell.Controls.Add(new LiteralControl(r.Subject));
				}
				table.Rows.Add(new IHGHtmlTableRow(subjectCell));

				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(r.Header.Replace("\r", "").Replace("\n", "<br>"))));
				
				var img = new HtmlImage { ID = imgID, Src = GetReportImageUrl(r.ReportPart.Id, "reportImage", URL) };
				img.Attributes.Add("class", "img");
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(img)));
				var a = new HtmlAnchor { Target = "_blank", HRef = GetReportImageUrl(r.ReportPart.Id, "Export", URL) };
				a.Controls.Add(new HtmlImage { Src = "images/page_white_acrobat.png" });
				table.Rows.Add(new IHGHtmlTableRow(new IHGHtmlTableCell(a)));
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

		IList<BaseModel> GetUrlModels(int grouping)
		{
			switch (grouping) {
				case 1:
				case 2:
					return SelectedDepartments;
				case 3:
					return SelectedQuestions;
				default:
					return new List<BaseModel>();
			}
		}
		
		protected string GetReportImageUrl(int id, string page, string URL)
		{
			string s = HttpContext.Current.Request.QueryString["Plot"] != null ? "&Plot=" + HttpContext.Current.Request["Plot"] : "";
			string reportImageUrl = string.Format(
				"{13}.aspx?LangID={0}&FY={1}&TY={2}&SAID={3}&SID={4}&{5}STDEV={6}&GB={7}&RPID={8}&PRUID={9}{10}&GRPNG={11}{12}",
				Convert.ToInt32(LangID.SelectedValue),
				FromYear.SelectedValue,
				ToYear.SelectedValue,
				sponsorAdminID,
				sponsorID,
				Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "Anonymized=1&" : "",
				STDEV.Checked ? "1" : "0",
				GroupBy.SelectedValue,
				id,
				ProjectRoundUnitID.SelectedValue,
				URL,
				Convert.ToInt32(Grouping.SelectedValue),
				s,
				page
			);
			return reportImageUrl;
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			Org.Visible = (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2");
			BQ.Visible = (Grouping.SelectedValue == "3");
		}
		
		string GetURL(IList<BaseModel> models)
		{
			string URL = "";
			foreach (var m in models) {
				URL += "," + m.Id;
			}
			URL = "&GID=0" + URL;
			return URL;
		}

		void Execute_Click(object sender, EventArgs e)
		{
			int selectedLangID = Convert.ToInt32(LangID.SelectedValue);
			int selectedProjectRoundUnitID = Convert.ToInt32(ProjectRoundUnitID.SelectedValue);
			int grouping = Convert.ToInt32(Grouping.SelectedValue);
			
			var reportParts = reportRepository.FindByProjectAndLanguage(selectedProjectRoundUnitID, selectedLangID);
			SetReportPartLanguages(reportParts, GetUrlModels(grouping));
		}
	}
}