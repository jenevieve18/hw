using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;

namespace HWgrp___Old
{
	public partial class stats : System.Web.UI.Page
	{
		int sponsorID = 0;
		bool exec = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			sponsorID = Convert.ToInt32(HttpContext.Current.Session["SponsorID"]);

			if (sponsorID != 0)
			{
				SqlDataReader rs;
				if (!IsPostBack)
				{
					int pruid = 0;
					rs = Db.rs("SELECT " +
						"sprul.LangID, " +
						"spru.ProjectRoundUnitID, " +
						"l.LID, " +
						"l.Language " +
						"FROM SponsorProjectRoundUnit spru " +
						"LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID " +
						"INNER JOIN LID l ON ISNULL(sprul.LangID,1) = l.LID " +
						"WHERE spru.SponsorID = " + sponsorID + " ORDER BY spru.SortOrder, spru.SponsorProjectRoundUnitID, l.LID");
					while (rs.Read() && (pruid == 0 || pruid == rs.GetInt32(1)))
					{
						LangID.Items.Add(new ListItem(rs.GetString(3), rs.GetInt32(2).ToString()));
						if (rs.GetInt32(2) == 2)
						{
							LangID.SelectedValue = rs.GetInt32(2).ToString();
						}
						pruid = rs.GetInt32(1);
					}
					rs.Close();

					rs = Db.rs("SELECT " +
						"ISNULL(sprul.Nav,'?'), " +
						"spru.ProjectRoundUnitID " +
						"FROM SponsorProjectRoundUnit spru " +
						"LEFT OUTER JOIN SponsorProjectRoundUnitLang sprul ON spru.SponsorProjectRoundUnitID = sprul.SponsorProjectRoundUnitID " +
						"WHERE spru.SponsorID = " + sponsorID + " " +
						"AND ISNULL(sprul.LangID,1) = " + Convert.ToInt32(LangID.SelectedValue));
					while (rs.Read())
					{
						ProjectRoundUnitID.Items.Add(new ListItem(rs.GetString(0), rs.GetInt32(1).ToString()));
					}
					rs.Close();
					GroupBy.SelectedValue = "7";

					for (int i = 2005; i <= DateTime.Now.Year; i++)
					{
						FromYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
						ToYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
					}
					FromYear.SelectedValue = (DateTime.Now.Year - 1).ToString();
					ToYear.SelectedValue = DateTime.Now.Year.ToString();

					rs = Db.rs("SELECT sbq.BQID, BQ.Internal FROM SponsorBQ sbq INNER JOIN BQ ON BQ.BQID = sbq.BQID WHERE (BQ.Comparison = 1 OR sbq.Hidden = 1) AND BQ.Type IN (1,7) AND sbq.SponsorID = " + sponsorID);
					while (rs.Read())
					{
						BQ.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
					}
					rs.Close();
					BQ.SelectedIndex = 0;
				}

				Org.Controls.Add(new LiteralControl("</BR><TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\" style=\"border:0;border-collapse:collapse; border-spacing:0;\"><TR><TD COLSPAN=\"3\">" + HttpContext.Current.Session["Sponsor"] + "</TD><TR>"));
				bool[] DX = new bool[8];
				rs = Db.rs("SELECT " +
					(Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "d.DepartmentAnonymized" : "d.Department") + ", " +
					"d.DepartmentID, " +
					(Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "''" : "d.DepartmentShort") + ", " +
					"dbo.cf_departmentDepth(d.DepartmentID), " +
					"(" +
						"SELECT COUNT(*) FROM Department x " +
						(HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ? "INNER JOIN SponsorAdminDepartment xx ON x.DepartmentID = xx.DepartmentID AND xx.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " : "") +
						"WHERE (x.ParentDepartmentID = d.ParentDepartmentID OR x.ParentDepartmentID IS NULL AND d.ParentDepartmentID IS NULL) " +
						"AND d.SponsorID = x.SponsorID " +
						"AND d.SortString < x.SortString" +
					") " +		// 4 - Number of departments on same level after this one
					"FROM Department d " +
					(HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
					"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
					"WHERE sad.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " +
					"AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
					"ORDER BY d.SortString");
				while (rs.Read())
				{
					Org.Controls.Add(new LiteralControl("<TR><TD>"));
					CheckBox O = new CheckBox();
					O.ID = "DID" + rs.GetInt32(1);
					Org.Controls.Add(O);
					Org.Controls.Add(new LiteralControl("</TD><TD>" + (!rs.IsDBNull(2) && rs.GetString(2).Trim().ToLower() != rs.GetString(0).Trim().ToLower() ? rs.GetString(2) + "&nbsp;" : "") + "</TD><TD>"));

					int depth = rs.GetInt32(3);
					DX[depth] = (rs.GetInt32(4) > 0);

					string d = "";
					for (int i = 1; i <= depth; i++)
					{
						d += "<img src=\"img/";
						if (i == depth)
						{
							d += (DX[i] ? "T" : "L");
						}
						else
						{
							d += (DX[i] ? "I" : "null");
						}
						d += ".gif\" width=\"19\" height=\"20\"/>";
					}
					Org.Controls.Add(new LiteralControl(
						"<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\" style=\"border:0;border-collapse:collapse; border-spacing:0;\"><TR><TD>" + d + "</TD><TD>" + rs.GetString(0) + "</TD></TR></TABLE>"));

					Org.Controls.Add(new LiteralControl("</TD></TR>"));
				}
				rs.Close();
				Org.Controls.Add(new LiteralControl("</TABLE>"));
			}
			else
			{
				HttpContext.Current.Response.Redirect("default.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
			}

			Execute.Click += new EventHandler(Execute_Click);
		}

		void Execute_Click(object sender, EventArgs e)
		{
			exec = true;
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

		protected override void OnPreRender(EventArgs e)
		{
			Org.Visible = (Grouping.SelectedValue == "1" || Grouping.SelectedValue == "2");
			BQ.Visible = (Grouping.SelectedValue == "3");

			StatsImg.Text = "";

			if (exec)
			{
				string URL = "";
				switch (Convert.ToInt32(Grouping.SelectedValue))
				{
					case 1:
						{
							SqlDataReader rs2 = Db.rs("SELECT " +
								"d.DepartmentID " +
								"FROM Department d " +
								(HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?
								"INNER JOIN SponsorAdminDepartment sad ON d.DepartmentID = sad.DepartmentID " +
								"WHERE sad.SponsorAdminID = " + HttpContext.Current.Session["SponsorAdminID"] + " " +
								"AND " : "WHERE ") + "d.SponsorID = " + sponsorID + " " +
								"ORDER BY d.SortString");
							while (rs2.Read())
							{
								if (((CheckBox)Org.FindControl("DID" + rs2.GetInt32(0))).Checked)
								{
									URL += "," + rs2.GetInt32(0);
								}
							}
							rs2.Close();

							URL = "&GID=0" + URL;
							break;
						}
					case 2:
						{
							goto case 1;
						}
					case 3:
						{
							SqlDataReader rs2 = Db.rs("SELECT sbq.BQID FROM SponsorBQ sbq INNER JOIN BQ ON BQ.BQID = sbq.BQID WHERE (BQ.Comparison = 1 OR sbq.Hidden = 1) AND BQ.Type IN (1,7) AND sbq.SponsorID = " + sponsorID);
							while (rs2.Read())
							{
								if (BQ.Items.FindByValue(rs2.GetInt32(0).ToString()).Selected)
								{
									URL += "," + rs2.GetInt32(0);
								}
							}
							rs2.Close();

							URL = "&GID=0" + URL;
							break;
						}
				}
				#region REMOVED
				/*
			if (!IsPostBack)
			{
				OrgTree.Text = "";
				OrgTree.Text += "<TABLE BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\"><TR><TD><B><U>Unit</U></B>&nbsp;&nbsp;</TD><TD ALIGN=\"CENTER\">&nbsp;&nbsp;<B><U>Action</U></B>&nbsp;&nbsp;</TD></TR>";
				OrgTree.Text += "<TR><TD>" + HttpContext.Current.Session["Sponsor"] + "</TD></TR>";

				rs = Db.rs("SELECT " +
					"d.Department, " +
					"dbo.cf_departmentDepth(d.DepartmentID), " +
					"d.DepartmentID " +
					"FROM Department d " +
					"WHERE d.SponsorID = " + sponsorID + " " +
					"ORDER BY d.SortString");
				while (rs.Read())
				{
					OrgTree.Text += "<TR><TD>";
					for (int i = 0; i < rs.GetInt32(1); i++)
					{
						OrgTree.Text += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
					}
					OrgTree.Text += rs.GetString(0) + "</TD><TD ALIGN=\"CENTER\">&nbsp;&nbsp;&nbsp;&nbsp;</TD></TR>";
				}
				rs.Close();

				OrgTree.Text += "</TABLE>";
			}
			*/
				#endregion

				int cx = 0;
				SqlDataReader rs = Db.rs("SELECT " +
					"rp.ReportPartID, " +
					"rpl.Subject, " +
					"rpl.Header, " +
					"rpl.Footer, " +
					"rp.Type " +
					"FROM ProjectRoundUnit pru " +
					"INNER JOIN Report r ON r.ReportID = pru.ReportID " +
					"INNER JOIN ReportPart rp ON r.ReportID = rp.ReportID " +
					"INNER JOIN ReportPartLang rpl ON rp.ReportPartID = rpl.ReportPartID " +
					"AND rpl.LangID = " + Convert.ToInt32(LangID.SelectedValue) + " " +
					"WHERE pru.ProjectRoundUnitID = " + ProjectRoundUnitID.SelectedValue + " " +
					"ORDER BY rp.SortOrder", "eFormSqlConnection");
				while (rs.Read())
				{
					StatsImg.Text += "<div" + (cx > 0 ? " style=\"page-break-before:always;\"" : "") + ">&nbsp;<br/>&nbsp;<br/></div><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
					StatsImg.Text += "<tr class=\"noscreen\"><td align=\"center\" valign=\"middle\" background=\"img/top_healthWatch.jpg\" height=\"140\" style=\"font-size:24px;\">" + rs.GetString(1) + "</td></tr>";
					StatsImg.Text += "<tr class=\"noprint\"><td style=\"font-size:18px;\">" + rs.GetString(1) + "</td></tr>";

					if (!rs.IsDBNull(2) && rs.GetString(2) != "")
						StatsImg.Text += "<tr><td>" + rs.GetString(2).Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";

					StatsImg.Text += "<tr><td><img src=\"reportImage.aspx?LangID=" + Convert.ToInt32(LangID.SelectedValue) + "&FY=" + FromYear.SelectedValue + "&TY=" + ToYear.SelectedValue + "&SAID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + "&SID=" + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) + "&" + (Convert.ToInt32(HttpContext.Current.Session["Anonymized"]) == 1 ? "Anonymized=1&" : "") + "STDEV=" + (STDEV.Checked ? "1" : "0") + "&GB=" + GroupBy.SelectedValue + "&RPID=" + rs.GetInt32(0) + "&PRUID=" + ProjectRoundUnitID.SelectedValue + URL + "&GRPNG=" + Convert.ToInt32(Grouping.SelectedValue) + "\"/></td></tr>";

					if (!rs.IsDBNull(3) && rs.GetString(3) != "")
						StatsImg.Text += "<tr><td>" + rs.GetString(3).Replace("\r", "").Replace("\n", "<br/>") + "</td></tr>";

					StatsImg.Text += "</table>";

					cx++;
				}
				rs.Close();
			}
		}
	}
}