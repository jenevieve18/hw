using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;
using System.Collections;
using HW.Core.Repositories.Sql;

namespace HW.Adm
{
	public partial class sponsorSetup : System.Web.UI.Page
	{
        int sponsorID = 0;
        int PRUID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            sponsorID = (HttpContext.Current.Request.QueryString["SponsorID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["SponsorID"]) : 0);

            SqlDataReader rs;

            if (HttpContext.Current.Request.QueryString["MoveUp"] != null)
            {
                rs = Db.rs("SELECT SortOrder FROM SponsorProjectRoundUnit WHERE ABS(SponsorID) = " + sponsorID + " AND SurveyID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
                if (rs.Read())
                {
                    SqlDataReader rs2 = Db.rs("SELECT TOP 1 SurveyID, SortOrder FROM SponsorProjectRoundUnit WHERE SortOrder < " + rs.GetInt32(0) + " AND ABS(SponsorID) = " + sponsorID + " ORDER BY SortOrder DESC");
                    if (rs2.Read())
                    {
                        Db.exec("UPDATE SponsorProjectRoundUnit SET SortOrder = " + rs.GetInt32(0) + " WHERE ABS(SponsorID) = " + sponsorID + " AND SurveyID = " + rs2.GetInt32(0));
                        Db.exec("UPDATE SponsorProjectRoundUnit SET SortOrder = " + rs2.GetInt32(1) + " WHERE ABS(SponsorID) = " + sponsorID + " AND SurveyID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
                    }
                    rs2.Close();
                }
                rs.Close();
                HttpContext.Current.Response.Redirect("sponsorSetup.aspx?SponsorID=" + sponsorID, true);
            }

            if (HttpContext.Current.Request.QueryString["MoveUpBQ"] != null)
            {
                rs = Db.rs("SELECT SortOrder FROM SponsorBQ WHERE ABS(SponsorID) = " + sponsorID + " AND BQID = " + HttpContext.Current.Request.QueryString["MoveUpBQ"]);
                if (rs.Read())
                {
                    SqlDataReader rs2 = Db.rs("SELECT TOP 1 BQID, SortOrder FROM SponsorBQ WHERE SortOrder < " + rs.GetInt32(0) + " AND ABS(SponsorID) = " + sponsorID + " ORDER BY SortOrder DESC");
                    if (rs2.Read())
                    {
                        Db.exec("UPDATE SponsorBQ SET SortOrder = " + rs.GetInt32(0) + " WHERE ABS(SponsorID) = " + sponsorID + " AND BQID = " + rs2.GetInt32(0));
                        Db.exec("UPDATE SponsorBQ SET SortOrder = " + rs2.GetInt32(1) + " WHERE ABS(SponsorID) = " + sponsorID + " AND BQID = " + HttpContext.Current.Request.QueryString["MoveUpBQ"]);
                    }
                    rs2.Close();
                }
                rs.Close();
                HttpContext.Current.Response.Redirect("sponsorSetup.aspx?SponsorID=" + sponsorID, true);
            }

            Save.Click += new EventHandler(Save_Click);
            Back.Click += new EventHandler(Back_Click);
            Close.Click += new EventHandler(Close_Click);
            Delete.Click += new EventHandler(Delete_Click);

            if (sponsorID != 0)
            {
                AddExtendedSurvey.Click += new EventHandler(AddExtendedSurvey_Click);
            }
            else
            {
                AddExtendedSurvey.Visible = false;
            }

            if (!IsPostBack)
            {
                SuperSponsorID.Items.Add(new ListItem("< none >", "NULL"));
                LID.Items.Add(new ListItem("< let user select >", "NULL"));
                rs = Db.rs("SELECT LangID, Lang FROM Lang");
                while (rs.Read())
                {
                    LID.Items.Add(new ListItem(rs.GetString(1), (rs.GetInt32(0) + 1).ToString()));
                }
                rs.Close();
                rs = Db.rs("SELECT SuperSponsorID, SuperSponsor, Comment FROM SuperSponsor");
                while (rs.Read())
                {
                    SuperSponsorID.Items.Add(new ListItem(rs.GetString(1) + (rs.IsDBNull(2) ? "" : " " + rs.GetString(2)), rs.GetInt32(0).ToString()));
                }
                rs.Close();
                if (sponsorID != 0)
                {
                    rs = Db.rs("SELECT " +
                        "s.Sponsor, " +
                        "s.Application, " +
                        "CAST(s.SponsorKey AS VARCHAR(64)), " +
                        "s.TreatmentOffer, " +
                        "s.TreatmentOfferText, " +
                        "s.TreatmentOfferEmail, " +
                        "s.TreatmentOfferIfNeededText, " +
                        "s.TreatmentOfferBQ, " +
                        "s.TreatmentOfferBQfn, " +
                        "s.TreatmentOfferBQmorethan, " +
                        "s.InfoText, " +
                        "s.ConsentText, " +
                        "s.SuperSponsorID, " +
                        "s.AlternativeTreatmentOfferText, " +
                        "s.AlternativeTreatmentOfferEmail, " +
                        "s.LID, " +
                        "s.MinUserCountToDisclose " +
                        "FROM Sponsor s WHERE s.SponsorID = " + sponsorID);
                    if (rs.Read())
                    {
                        Sponsor.Text = rs.GetString(0);
                        App.Text = rs.GetString(1);
                        SponsorKey.Text = rs.GetString(2).Substring(0, 8) + sponsorID.ToString();
                        Logotype.Text = "<img src=\"img/sponsor/" + rs.GetString(2).Substring(0, 8) + sponsorID.ToString() + ".gif\"/>";
                        TreatmentOffer.Checked = !rs.IsDBNull(3);
                        TreatmentOfferText.Text = (rs.IsDBNull(4) ? "" : rs.GetString(4));
                        TreatmentOfferEmail.Text = (rs.IsDBNull(5) ? "" : rs.GetString(5));
                        TreatmentOfferIfNeededText.Text = (rs.IsDBNull(6) ? "" : rs.GetString(6));
                        TreatmentOfferBQ.Text = (rs.IsDBNull(7) ? "" : rs.GetInt32(7).ToString());
                        TreatmentOfferBQfn.Text = (rs.IsDBNull(8) ? "" : rs.GetInt32(8).ToString());
                        TreatmentOfferBQmorethan.Text = (rs.IsDBNull(9) ? "" : rs.GetInt32(9).ToString());
                        InfoText.Text = (rs.IsDBNull(10) ? "" : rs.GetString(10));
                        ConsentText.Text = (rs.IsDBNull(11) ? "" : rs.GetString(11));
                        if (!rs.IsDBNull(12))
                        {
                            SuperSponsorID.SelectedValue = rs.GetInt32(12).ToString();
                        }
                        AlternativeTreatmentOfferText.Text = (rs.IsDBNull(13) ? "" : rs.GetString(13));
                        AlternativeTreatmentOfferEmail.Text = (rs.IsDBNull(14) ? "" : rs.GetString(14));
                        LID.SelectedValue = (rs.IsDBNull(15) ? "NULL" : rs.GetInt32(15).ToString());
                        MinUserCountToDisclose.Text = (rs.IsDBNull(16) ? "10" : rs.GetInt32(16).ToString());
                    }
                    rs.Close();
                }
            }

            rs = Db.rs("SELECT " +
                "d.DepartmentID, " +
                "dbo.cf_departmentTree(d.DepartmentID,' &gt; '), " +
                "d.MinUserCountToDisclose " +
                "FROM Department d " +
                "WHERE d.SponsorID = " + sponsorID + " " +
                "ORDER BY dbo.cf_departmentSortString(d.DepartmentID)");
            if (rs.Read())
            {
                Departments.Controls.Add(new LiteralControl("<table style=\"margin:20px;\" width=\"900\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"><tr><td colspan=\"2\"><b>Minimum users to disclose counters on Department level</b></td></tr>"));
                do
                {
                    Departments.Controls.Add(new LiteralControl("<tr><td>"));
                    TextBox tb = new TextBox();
                    tb.Width = 30;
                    tb.ID = "MinUserCountToDisclose" + rs.GetInt32(0);
                    Departments.Controls.Add(tb);
                    if (!IsPostBack && !rs.IsDBNull(2))
                    {
                        tb.Text = rs.GetInt32(2).ToString();
                    }
                    Departments.Controls.Add(new LiteralControl("</td><td width=\"100%\">&nbsp;" + rs.GetString(1) + "</td></tr>"));
                }
                while (rs.Read());
                Departments.Controls.Add(new LiteralControl("</table>"));
            }
            rs.Close();

            int extendedSurveyCount = 0;
            rs = Db.rs("SELECT COUNT(*) FROM SponsorExtendedSurvey ses WHERE ses.SponsorID = " + sponsorID);
            if (rs.Read())
            {
                extendedSurveyCount = rs.GetInt32(0);
            }
            rs.Close();
            if (extendedSurveyCount > 0)
            {
                int tmpExtendedSurveyCount = 0;
                rs = Db.rs("SELECT " +
                    "SponsorExtendedSurveyID, " +       // 0
                    "ProjectRoundID, " +                // 1
                    "RequiredUserCount, " +             // 2
                    "Internal, " +                      // 3
                    "EformFeedbackID, " +               // 4
                    "PreviousProjectRoundID, " +        // 5
                    "IndividualFeedbackID, " +          // 6
                    "RoundText " +                      // 7
                    "FROM SponsorExtendedSurvey " +
                    "WHERE SponsorID = " + sponsorID);
                if (rs.Read())
                {
                    SponsorExtendedSurvey.Controls.Add(new LiteralControl("" +
                        "<table style=\"margin:20px;\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                        "<tr><td colspan=\"3\" style=\"font-size:16px;\" align=\"center\">Extended surveys</td></tr>" +
                        "<tr><td colspan=\"3\">&nbsp;</td></tr>" +
                        ""));
                    do
                    {
                        tmpExtendedSurveyCount++;

                        #region ...
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"10\"></TD></TR>"));
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD BGCOLOR=\"#cc0000\" COLSPAN=\"2\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"10\"></TD></TR>"));

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<tr><td colspan=\"2\"><a style=\"text-decoration:none;font-size:14px;font-weight:bold;\" href=\"javascript:document.getElementById('SESROW" + rs.GetInt32(0) + "').style.display=(document.getElementById('SESROW" + rs.GetInt32(0) + "').style.display==''?'none':'');\">?</a> Survey&nbsp;"));
                        DropDownList ProjectRoundID = new DropDownList();
                        ProjectRoundID.ID = "ProjectRoundID" + rs.GetInt32(0);
                        SponsorExtendedSurvey.Controls.Add(ProjectRoundID);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;Start&nbsp;"));
                        TextBox Started = new TextBox();
                        Started.ID = "Started" + rs.GetInt32(0);
                        Started.Width = Unit.Pixel(120);
                        SponsorExtendedSurvey.Controls.Add(Started);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;End&nbsp;"));
                        TextBox Closed = new TextBox();
                        Closed.ID = "Closed" + rs.GetInt32(0);
                        Closed.Width = Unit.Pixel(120);
                        SponsorExtendedSurvey.Controls.Add(Closed);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("</td></tr>"));

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">Subject&nbsp;"));
                        TextBox InternalSES = new TextBox();
                        InternalSES.ID = "InternalSES" + rs.GetInt32(0);
                        InternalSES.Width = Unit.Pixel(200);
                        SponsorExtendedSurvey.Controls.Add(InternalSES);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;Round&nbsp;"));

                        TextBox RoundText = new TextBox();
                        RoundText.ID = "RoundText" + rs.GetInt32(0);
                        RoundText.Width = Unit.Pixel(70);
                        SponsorExtendedSurvey.Controls.Add(RoundText);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;Required&nbsp;answer&nbsp;count&nbsp;"));

                        TextBox RequiredUserCountSES = new TextBox();
                        RequiredUserCountSES.ID = "RequiredUserCountSES" + rs.GetInt32(0);
                        RequiredUserCountSES.Width = Unit.Pixel(35);
                        SponsorExtendedSurvey.Controls.Add(RequiredUserCountSES);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;Feedback&nbsp;"));
                        DropDownList EformFeedbackID = new DropDownList();
                        EformFeedbackID.ID = "EformFeedbackID" + rs.GetInt32(0);
                        SponsorExtendedSurvey.Controls.Add(EformFeedbackID);

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;Top-Level-Compare&nbsp;"));
                        CheckBox EformFeedbackCompare = new CheckBox();
                        EformFeedbackCompare.ID = "EformFeedbackCompare" + rs.GetInt32(0);
                        SponsorExtendedSurvey.Controls.Add(EformFeedbackCompare);

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("</td></tr>"));

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">Link feedback to previous&nbsp;"));
                        DropDownList PreviousProjectRoundID = new DropDownList();
                        PreviousProjectRoundID.ID = "PreviousProjectRoundID" + rs.GetInt32(0);
                        SponsorExtendedSurvey.Controls.Add(PreviousProjectRoundID);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;Individual feedback&nbsp;"));
                        DropDownList IndividualFeedbackID = new DropDownList();
                        IndividualFeedbackID.ID = "IndividualFeedbackID" + rs.GetInt32(0);
                        SponsorExtendedSurvey.Controls.Add(IndividualFeedbackID);
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("</td>"));
                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("</tr>"));

                        if (!IsPostBack)
                        {
                            PreviousProjectRoundID.Items.Add(new ListItem("< none >", "NULL"));

                            ProjectRoundID.Items.Add(new ListItem("< select >", "NULL"));
                            EformFeedbackID.Items.Add(new ListItem("< none >", "NULL"));
                            EformFeedbackID.Items.Add(new ListItem("Generic WebbQPS feedback", "1"));

                            IndividualFeedbackID.Items.Add(new ListItem("No", "NULL"));
                            IndividualFeedbackID.Items.Add(new ListItem("Yes", "1"));

                            InternalSES.Text = (!rs.IsDBNull(3) ? rs.GetString(3) : "");
                            RequiredUserCountSES.Text = (!rs.IsDBNull(2) ? rs.GetInt32(2).ToString() : "10");
                            EformFeedbackID.SelectedValue = (!rs.IsDBNull(4) ? rs.GetInt32(4).ToString() : "NULL");
                            IndividualFeedbackID.SelectedValue = (!rs.IsDBNull(6) ? rs.GetInt32(6).ToString() : "NULL");

                            RoundText.Text = (!rs.IsDBNull(7) ? rs.GetString(7) : "");
                        }

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<tr id=\"SESROW" + rs.GetInt32(0) + "\"" + (tmpExtendedSurveyCount == extendedSurveyCount ? "" : " style=\"display:none;\"") + "><td colspan=\"2\"><table style=\"margin:20px;\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"));

                        SqlDataReader rs2 = Db.rs("SELECT " +
                            "pr.ProjectRoundID, " +
                            "pr.Internal, " +
                            "p.Internal, " +
                            "pr.Started, " +
                            "pr.Closed, " +
                            "pr.AdHocReportCompareWithParent " +
                            "FROM Project p " +
                            "INNER JOIN ProjectRound pr ON p.ProjectID = pr.ProjectID " +
                            "ORDER BY p.Internal, pr.Internal", "eFormSqlConnection");
                        while (rs2.Read())
                        {
                            if (!IsPostBack)
                            {
                                ProjectRoundID.Items.Add(new ListItem(rs2.GetString(2) + ", " + rs2.GetString(1), rs2.GetInt32(0).ToString()));
                                PreviousProjectRoundID.Items.Add(new ListItem(rs2.GetString(2) + ", " + rs2.GetString(1), rs2.GetInt32(0).ToString()));

                                if (!rs.IsDBNull(5) && rs.GetInt32(5) == rs2.GetInt32(0))
                                {
                                    PreviousProjectRoundID.SelectedValue = rs.GetInt32(5).ToString();
                                }
                            }
                            if (!rs.IsDBNull(1) && rs.GetInt32(1) == rs2.GetInt32(0))
                            {
                                if (!IsPostBack)
                                {
                                    EformFeedbackCompare.Checked = rs2.IsDBNull(5);

                                    ProjectRoundID.SelectedValue = rs.GetInt32(1).ToString();
                                    if (!rs2.IsDBNull(3))
                                    {
                                        Started.Text = rs2.GetDateTime(3).ToString("yyyy-MM-dd HH:mm");
                                    }
                                    if (!rs2.IsDBNull(4))
                                    {
                                        if (rs2.GetDateTime(4) > DateTime.Now)
                                        {
                                            Closed.Attributes["style"] += "background-color:#00cc00;";
                                        }
                                        Closed.Text = rs2.GetDateTime(4).ToString("yyyy-MM-dd HH:mm");
                                    }
                                    else
                                    {
                                        Closed.Attributes["style"] += "background-color:#00cc00;";
                                    }
                                }
                                SqlDataReader rs3 = Db.rs("SELECT p.ProjectRoundQOID, q.Internal, o.Internal, o.OptionType, o.OptionID FROM ProjectRoundQO p INNER JOIN Question q ON q.QuestionID = p.QuestionID INNER JOIN [Option] o ON p.OptionID = o.OptionID WHERE p.ProjectRoundID = " + rs.GetInt32(1) + " ORDER BY p.SortOrder", "eFormSqlConnection");
                                if (rs3.Read())
                                {
                                    SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"10\"></TD></TR>"));
                                    SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD COLSPAN=\"2\">Mappings to HealthWatch background questions</TD></TR>"));
                                    do
                                    {
                                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD BGCOLOR=\"#CCCCCC\" COLSPAN=\"2\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));
                                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<TR><TD valign=\"top\">&nbsp;-&gt;&nbsp;"));
                                        DropDownList BQID = new DropDownList();
                                        BQID.ID = "ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs3.GetInt32(0);
                                        BQID.Width = Unit.Pixel(150);
                                        BQID.Items.Add(new ListItem("< select >", "NULL"));
                                        SponsorExtendedSurvey.Controls.Add(BQID);
                                        TextBox FN = new TextBox();
                                        FN.ID = "ProjectRoundID" + rs.GetInt32(0) + "FN" + rs3.GetInt32(0);
                                        FN.Width = Unit.Pixel(20);
                                        SponsorExtendedSurvey.Controls.Add(FN);
                                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;" + rs3.GetString(1) + ", " + rs3.GetString(2) + "&nbsp;</TD><TD valign=\"top\">"));

                                        SqlDataReader rs4 = Db.rs("SELECT " +
                                            "BQ.BQID, " +
                                            "BQ.Internal, " +
                                            "b.BQID, " +
                                            "b.FN " +
                                            "FROM BQ " +
                                            "INNER JOIN SponsorBQ s ON BQ.BQID = s.BQID " +
                                            "LEFT OUTER JOIN SponsorExtendedSurveyBQ b ON b.SponsorExtendedSurveyID = " + rs.GetInt32(0) + " " +
                                            "AND b.ProjectRoundQOID = " + rs3.GetInt32(0) + " " +
                                            "AND b.BQID = BQ.BQID " +
                                            "WHERE s.SponsorID = " + sponsorID);
                                        while (rs4.Read())
                                        {
                                            if (!IsPostBack)
                                            {
                                                BQID.Items.Add(new ListItem(rs4.GetString(1), rs4.GetInt32(0).ToString()));
                                                if (!rs4.IsDBNull(2))
                                                {
                                                    BQID.SelectedValue = rs4.GetInt32(2).ToString();
                                                }
                                                if (!rs4.IsDBNull(3))
                                                {
                                                    FN.Text = rs4.GetInt32(3).ToString();
                                                }
                                            }
                                            if (!rs4.IsDBNull(2))
                                            {
                                                if (rs3.GetInt32(3) == 1 || rs3.GetInt32(3) == 6 || rs3.GetInt32(3) == 7)
                                                {
                                                    SqlDataReader rs5 = Db.rs("SELECT " +
                                                        "oc.OptionComponentID, " +
                                                        "oc.Internal " +
                                                        "FROM OptionComponents ocs " +
                                                        "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
                                                        "WHERE ocs.OptionID = " + rs3.GetInt32(4) + " " +
                                                        "ORDER BY ocs.SortOrder", "eFormSqlConnection");
                                                    while (rs5.Read())
                                                    {
                                                        DropDownList BA = new DropDownList();
                                                        BA.ID = "ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs3.GetInt32(0) + "BA" + rs5.GetInt32(0);
                                                        BA.Width = Unit.Pixel(150);
                                                        BA.Items.Add(new ListItem("< select >", "NULL"));
                                                        SqlDataReader rs6 = Db.rs("SELECT " +
                                                            "BA.BAID, " +
                                                            "BA.Internal, " +
                                                            "s.BAID " +
                                                            "FROM BA " +
                                                            "LEFT OUTER JOIN SponsorExtendedSurveyBA s ON s.SponsorExtendedSurveyID = " + rs.GetInt32(0) + " AND s.ProjectRoundQOID = " + rs3.GetInt32(0) + " AND s.OptionComponentID = " + rs5.GetInt32(0) + " AND s.BAID = BA.BAID " +
                                                            "WHERE BA.BQID = " + rs4.GetInt32(0));
                                                        while (rs6.Read())
                                                        {
                                                            BA.Items.Add(new ListItem(rs6.GetString(1), rs6.GetInt32(0).ToString()));
                                                            if (!IsPostBack)
                                                            {
                                                                if (!rs6.IsDBNull(2) && rs6.GetInt32(0) == rs6.GetInt32(2))
                                                                {
                                                                    BA.SelectedValue = rs6.GetInt32(0).ToString();
                                                                }
                                                            }
                                                        }
                                                        rs6.Close();
                                                        SponsorExtendedSurvey.Controls.Add(BA);
                                                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;" + rs5.GetString(1) + "&nbsp;<BR/>"));
                                                    }
                                                    rs5.Close();
                                                }
                                            }
                                        }
                                        rs4.Close();
                                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("</TD></TR>"));
                                    }
                                    while (rs3.Read());
                                }
                                rs3.Close();
                            }
                        }
                        rs2.Close();
                        #endregion

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("<tr><td colspan=\"3\"><b>Hide</b>&nbsp;&nbsp;<b>Ext</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b title=\"Required answer count\">RAC</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b title=\"Treatment offer email\">TOE</b></td></tr>"));
                        rs2 = Db.rs("SELECT " +
                            "d.DepartmentID, " +
                            "dbo.cf_departmentTree(d.DepartmentID,' &gt; '), " +
                            "sesd.RequiredUserCount, " +
                            "sesd.Hide, " +
                            "sesd.Ext, " +
                            "sesd.TreatmentOfferEmail " +
                            "FROM Department d " +
                            "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON d.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = " + rs.GetInt32(0) + " " +
                            "WHERE d.SponsorID = " + sponsorID + " " +
                            "ORDER BY dbo.cf_departmentSortString(d.DepartmentID)");
                        while (rs2.Read())
                        {
                            SponsorExtendedSurvey.Controls.Add(new LiteralControl("<tr><td colspan=\"3\">"));

                            CheckBox cb = new CheckBox();
                            cb.ID = "SES" + rs.GetInt32(0) + "D" + rs2.GetInt32(0) + "HIDE";
                            SponsorExtendedSurvey.Controls.Add(cb);
                            if (!IsPostBack && !rs2.IsDBNull(3))
                            {
                                cb.Checked = true;
                            }

                            SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));

                            cb = new CheckBox();
                            cb.ID = "SES" + rs.GetInt32(0) + "D" + rs2.GetInt32(0) + "EXT";
                            SponsorExtendedSurvey.Controls.Add(cb);
                            if (!IsPostBack && !rs2.IsDBNull(4))
                            {
                                cb.Checked = true;
                            }

                            SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));

                            TextBox tb = new TextBox();
                            tb.Width = Unit.Pixel(35);
                            tb.ID = "SES" + rs.GetInt32(0) + "D" + rs2.GetInt32(0);
                            SponsorExtendedSurvey.Controls.Add(tb);
                            if (!IsPostBack && !rs2.IsDBNull(2))
                            {
                                tb.Text = rs2.GetInt32(2).ToString();
                            }

                            SponsorExtendedSurvey.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;&nbsp;"));

                            tb = new TextBox();
                            tb.Width = Unit.Pixel(100);
                            tb.ID = "SES" + rs.GetInt32(0) + "TOE" + rs2.GetInt32(0);
                            SponsorExtendedSurvey.Controls.Add(tb);
                            if (!IsPostBack && !rs2.IsDBNull(5))
                            {
                                tb.Text = rs2.GetString(5);
                            }

                            SponsorExtendedSurvey.Controls.Add(new LiteralControl(" " + rs2.GetString(1) + "</td></tr>"));
                        }
                        rs2.Close();

                        SponsorExtendedSurvey.Controls.Add(new LiteralControl("</table></td></tr>"));
                    }
                    while (rs.Read());

                    SponsorExtendedSurvey.Controls.Add(new LiteralControl("" +
                        "<tr><td colspan=\"3\">&nbsp;</td></tr>" +
                        "</table>" +
                        ""));
                }
                rs.Close();
            }

            Surveys.Controls.Add(new LiteralControl("<TR><TD BGCOLOR=\"#CCCCCC\" COLSPAN=\"6\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));

            rs = Db.rs("SELECT ProjectRoundUnitID FROM SystemSettings");
            if (rs.Read())
            {
                PRUID = rs.GetInt32(0);
            }
            rs.Close();

            Hashtable surveys = new Hashtable();
            rs = Db.rs("SELECT " +
                "s.SurveyID, " +
                "s.Internal " +
                "FROM ProjectRoundUnit pru " +
                "INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID " +
                "INNER JOIN Project p ON pr.ProjectID = p.ProjectID " +
                "INNER JOIN ProjectSurvey ps ON p.ProjectID = ps.ProjectID " +
                "INNER JOIN Survey s ON ps.SurveyID = s.SurveyID " +
                "WHERE pru.ProjectRoundUnitID = " + PRUID, "eFormSqlConnection");
            while (rs.Read())
            {
                surveys.Add(rs.GetInt32(0), rs.GetString(1));
            }
            rs.Close();
            Hashtable reports = new Hashtable();
            rs = Db.rs("SELECT r.ReportID, r.Internal FROM Report r", "eFormSqlConnection");
            while (rs.Read())
            {
                reports.Add(rs.GetInt32(0), rs.GetString(1));
            }
            rs.Close();
            int cx = 0;
            rs = Db.rs("SELECT SurveyID, Nav, SponsorID, ProjectRoundUnitID, OnlyEveryDays, GoToStatistics FROM SponsorProjectRoundUnit WHERE ABS(SponsorID) = " + sponsorID + " ORDER BY SortOrder");
            while (rs.Read())
            {
                Surveys.Controls.Add(new LiteralControl("<TR><TD><A HREF=\"sponsorSetup.aspx?SponsorID=" + sponsorID + "&MoveUp=" + rs.GetInt32(0) + "\"><img width=\"11\" src=\"img/" + (cx++ > 0 ? "UpToolSmall" : "null") + ".gif\" border=\"0\"></A></TD><TD>"));
                CheckBox cb = new CheckBox();
                cb.ID = "SurveyID" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (sponsorID == rs.GetInt32(2));
                }
                Surveys.Controls.Add(cb);
                Surveys.Controls.Add(new LiteralControl("</TD><TD>" + surveys[rs.GetInt32(0)] + "</TD><TD>"));
                TextBox t = new TextBox();
                t.ID = "Nav" + rs.GetInt32(0);
                t.Width = Unit.Pixel(150);
                if (!IsPostBack)
                {
                    t.Text = rs.GetString(1);
                }
                Surveys.Controls.Add(t);

                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));
                DropDownList iReportID = new DropDownList();
                iReportID.ID = "IndividualReportID" + rs.GetInt32(0);
                iReportID.Items.Add(new ListItem("< default >", "0"));
                foreach (DictionaryEntry de in reports)
                {
                    iReportID.Items.Add(new ListItem((string)de.Value, de.Key.ToString()));
                }
                Surveys.Controls.Add(iReportID);

                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));
                DropDownList reportID = new DropDownList();
                reportID.ID = "ReportID" + rs.GetInt32(0);
                reportID.Items.Add(new ListItem("< default >", "0"));
                foreach (DictionaryEntry de in reports)
                {
                    reportID.Items.Add(new ListItem((string)de.Value, de.Key.ToString()));
                }
                Surveys.Controls.Add(reportID);
                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));

                TextBox t2 = new TextBox();
                t2.ID = "OnlyEveryDays" + rs.GetInt32(0);
                t2.Width = Unit.Pixel(35);
                if (!IsPostBack && !rs.IsDBNull(4))
                {
                    t2.Text = rs.GetInt32(4).ToString();
                }
                Surveys.Controls.Add(t2);

                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));
                CheckBox cb2 = new CheckBox();
                cb2.ID = "GoToStatistics" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb2.Checked = !rs.IsDBNull(5);
                }
                Surveys.Controls.Add(cb2);

                Surveys.Controls.Add(new LiteralControl("</TD></tr>"));

                if (!IsPostBack)
                {
                    SqlDataReader rs2 = Db.rs("SELECT IndividualReportID, ReportID FROM ProjectRoundUnit WHERE ProjectRoundUnitID = " + rs.GetInt32(3), "eFormSqlConnection");
                    if (rs2.Read())
                    {
                        if (!rs2.IsDBNull(0))
                        {
                            iReportID.SelectedValue = rs2.GetInt32(0).ToString();
                        }
                        if (!rs2.IsDBNull(1))
                        {
                            reportID.SelectedValue = rs2.GetInt32(1).ToString();
                        }
                    }
                    rs2.Close();
                }

                surveys.Remove(rs.GetInt32(0));
            }
            rs.Close();
            Surveys.Controls.Add(new LiteralControl("<TR><TD BGCOLOR=\"#CCCCCC\" COLSPAN=\"6\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));
            foreach (DictionaryEntry de in surveys)
            {
                Surveys.Controls.Add(new LiteralControl("<TR><TD>&nbsp;</TD><TD>"));
                CheckBox cb = new CheckBox();
                cb.ID = "SurveyID" + de.Key.ToString();
                Surveys.Controls.Add(cb);
                Surveys.Controls.Add(new LiteralControl("</TD><TD>" + (string)de.Value + "</TD><TD>"));
                TextBox t = new TextBox();
                t.ID = "Nav" + de.Key.ToString();
                t.Width = Unit.Pixel(200);
                if (!IsPostBack)
                {
                    t.Text = (string)de.Value;
                }
                Surveys.Controls.Add(t);
                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));
                DropDownList iReportID = new DropDownList();
                iReportID.ID = "IndividualReportID" + de.Key.ToString();
                iReportID.Items.Add(new ListItem("< default >", "0"));
                foreach (DictionaryEntry det in reports)
                {
                    iReportID.Items.Add(new ListItem((string)det.Value, det.Key.ToString()));
                }
                Surveys.Controls.Add(iReportID);

                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));
                DropDownList reportID = new DropDownList();
                reportID.ID = "ReportID" + de.Key.ToString();
                reportID.Items.Add(new ListItem("< default >", "0"));
                foreach (DictionaryEntry det in reports)
                {
                    reportID.Items.Add(new ListItem((string)det.Value, det.Key.ToString()));
                }
                Surveys.Controls.Add(reportID);

                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));

                TextBox t2 = new TextBox();
                t2.ID = "OnlyEveryDays" + de.Key.ToString();
                t2.Width = Unit.Pixel(35);
                Surveys.Controls.Add(t2);

                Surveys.Controls.Add(new LiteralControl("</TD><TD>"));
                CheckBox cb2 = new CheckBox();
                cb2.ID = "GoToStatistics" + de.Key.ToString();
                Surveys.Controls.Add(cb2);

                Surveys.Controls.Add(new LiteralControl("</TD></tr>"));
            }

            BQ.Controls.Add(new LiteralControl("<TR><TD BGCOLOR=\"#CCCCCC\" COLSPAN=\"7\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));

            cx = 0;
            bool inc = true;
            rs = Db.rs("SELECT " +
                "bq.BQID, " +                       // 0
                "bq.Internal, " +
                "sbq.SponsorID, " +
                "sbq.Forced, " +
                "sbq.Hidden, " +
                "sbq.InGrpAdmin, " +                // 5
                "sbq.IncludeInTreatmentReq, " +
                "sbq.Organize " +
                "FROM BQ " +
                "LEFT OUTER JOIN SponsorBQ sbq ON bq.BQID = sbq.BQID AND ABS(sbq.SponsorID) = " + sponsorID + " " +
                "ORDER BY ISNULL(sbq.SortOrder,9999), bq.Internal");
            while (rs.Read())
            {
                if (inc && rs.IsDBNull(2))
                {
                    inc = false;
                    BQ.Controls.Add(new LiteralControl("<TR><TD BGCOLOR=\"#CCCCCC\" COLSPAN=\"7\"><IMG SRC=\"img/null.gif\" width=\"1\" height=\"1\"></TD></TR>"));
                }
                BQ.Controls.Add(new LiteralControl("<TR><TD>" + (inc ? "<A HREF=\"sponsorSetup.aspx?SponsorID=" + sponsorID + "&MoveUpBQ=" + rs.GetInt32(0) + "\"><img width=\"11\" src=\"img/" + (cx++ > 0 ? "UpToolSmall" : "null") + ".gif\" border=\"0\"></A>" : "") + "</TD><TD>"));
                CheckBox cb = new CheckBox();
                cb.ID = "BQID" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (!rs.IsDBNull(2) && rs.GetInt32(2) == sponsorID);
                }
                BQ.Controls.Add(cb);
                BQ.Controls.Add(new LiteralControl("</TD><TD>" + rs.GetString(1) + "</TD><TD>"));

                cb = new CheckBox();
                cb.ID = "Forced" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (!rs.IsDBNull(3) && rs.GetInt32(3) == 1);
                }
                BQ.Controls.Add(cb);

                BQ.Controls.Add(new LiteralControl("</TD><TD>"));

                cb = new CheckBox();
                cb.ID = "Hidden" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (!rs.IsDBNull(4) && rs.GetInt32(4) == 1);
                }
                BQ.Controls.Add(cb);

                BQ.Controls.Add(new LiteralControl("</TD><TD>"));

                cb = new CheckBox();
                cb.ID = "InGrpAdmin" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (!rs.IsDBNull(5) && rs.GetInt32(5) == 1);
                }
                BQ.Controls.Add(cb);

                BQ.Controls.Add(new LiteralControl("</TD><TD>"));

                cb = new CheckBox();
                cb.ID = "IncludeInTreatmentReq" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (!rs.IsDBNull(6) && rs.GetInt32(6) == 1);
                }
                BQ.Controls.Add(cb);

                BQ.Controls.Add(new LiteralControl("</TD><TD>"));

                cb = new CheckBox();
                cb.ID = "Organize" + rs.GetInt32(0);
                if (!IsPostBack)
                {
                    cb.Checked = (!rs.IsDBNull(7) && rs.GetInt32(7) == 1);
                }
                BQ.Controls.Add(cb);

                BQ.Controls.Add(new LiteralControl("</TD></tr>"));
            }
            rs.Close();
        }

        void Close_Click(object sender, EventArgs e)
        {
            Db.exec("UPDATE Sponsor SET Closed = GETDATE() WHERE SponsorID = " + sponsorID);
            HttpContext.Current.Response.Redirect("sponsor.aspx", true);
        }
        void Delete_Click(object sender, EventArgs e)
        {
            Db.exec("UPDATE Sponsor SET Deleted = GETDATE() WHERE SponsorID = " + sponsorID);
            HttpContext.Current.Response.Redirect("sponsor.aspx", true);
        }

        void AddExtendedSurvey_Click(object sender, EventArgs e)
        {
            Db.exec("INSERT INTO SponsorExtendedSurvey (SponsorID) VALUES (" + sponsorID + ")");

            HttpContext.Current.Response.Redirect("sponsorSetup.aspx?SponsorID=" + sponsorID, true);
        }

        void Save_Click(object sender, EventArgs e)
        {
            SqlDataReader rs;
            string sponsorKey = "";

            if (sponsorID != 0)
            {
                Db.exec("UPDATE Sponsor SET " +
                    "SuperSponsorID = " + SuperSponsorID.SelectedValue + ", " +
                    "Sponsor = '" + Sponsor.Text.Replace("'", "''") + "', " +
                    "Application = '" + App.Text.Replace("'", "''") + "', " +
                    "TreatmentOffer = " + (TreatmentOffer.Checked ? "1" : "NULL") + ", " +
                    "TreatmentOfferText = " + (TreatmentOfferText.Text != "" ? "'" + TreatmentOfferText.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "TreatmentOfferEmail = " + (TreatmentOfferEmail.Text != "" ? "'" + TreatmentOfferEmail.Text.Replace("'", "''") + "'" : "NULL") + ", " +

                    "AlternativeTreatmentOfferText = " + (AlternativeTreatmentOfferText.Text != "" ? "'" + AlternativeTreatmentOfferText.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "AlternativeTreatmentOfferEmail = " + (AlternativeTreatmentOfferEmail.Text != "" ? "'" + AlternativeTreatmentOfferEmail.Text.Replace("'", "''") + "'" : "NULL") + ", " +

                    "TreatmentOfferIfNeededText = " + (TreatmentOfferIfNeededText.Text != "" ? "'" + TreatmentOfferIfNeededText.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "TreatmentOfferBQ = " + (TreatmentOfferBQ.Text != "" ? "'" + TreatmentOfferBQ.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "TreatmentOfferBQfn = " + (TreatmentOfferBQfn.Text != "" ? "'" + TreatmentOfferBQfn.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "TreatmentOfferBQmorethan = " + (TreatmentOfferBQmorethan.Text != "" ? "'" + TreatmentOfferBQmorethan.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "InfoText = " + (InfoText.Text != "" ? "'" + InfoText.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "ConsentText = " + (ConsentText.Text != "" ? "'" + ConsentText.Text.Replace("'", "''") + "'" : "NULL") + ", " +
                    "MinUserCountToDisclose = " + (MinUserCountToDisclose.Text != "" ? Convert.ToInt32(MinUserCountToDisclose.Text).ToString() : "NULL") + ", " +
                    "LID = " + (LID.SelectedValue) + " " +
                    "WHERE SponsorID = " + sponsorID);

                rs = Db.rs("SELECT " +
                "d.DepartmentID " +
                "FROM Department d " +
                "WHERE d.SponsorID = " + sponsorID);
                while (rs.Read())
                {
                    string s = ((TextBox)Departments.FindControl("MinUserCountToDisclose" + rs.GetInt32(0))).Text;
                    Db.exec("UPDATE Department SET MinUserCountToDisclose = " + (s.Trim() == "" ? "NULL" : Convert.ToInt32(s).ToString()) + " WHERE DepartmentID = " + rs.GetInt32(0));
                }
                rs.Close();

                rs = Db.rs("SELECT ProjectRoundUnitID, CAST(SponsorKey AS VARCHAR(64)) FROM Sponsor WHERE SponsorID = " + sponsorID);
                if (rs.Read())
                {
                    PRUID = rs.GetInt32(0);
                    Db.exec("UPDATE ProjectRoundUnit " +
                                "SET Unit = '" + Sponsor.Text.Replace("'", "''") + "' " +
                                "WHERE ProjectRoundUnitID = " + PRUID, "eFormSqlConnection");

                    sponsorKey = rs.GetString(1).Substring(0, 8);
                }
                rs.Close();

                rs = Db.rs("SELECT SponsorExtendedSurveyID, ProjectRoundID FROM SponsorExtendedSurvey WHERE SponsorID = " + sponsorID);
                while (rs.Read())
                {
                    SqlDataReader rs2 = Db.rs("SELECT " +
                        "d.DepartmentID, " +
                        "sesd.SponsorExtendedSurveyDepartmentID " +
                        "FROM Department d " +
                        "LEFT OUTER JOIN SponsorExtendedSurveyDepartment sesd ON d.DepartmentID = sesd.DepartmentID AND sesd.SponsorExtendedSurveyID = " + rs.GetInt32(0) + " " +
                        "WHERE d.SponsorID = " + sponsorID);
                    while (rs2.Read())
                    {
                        bool hide = ((CheckBox)SponsorExtendedSurvey.FindControl("SES" + rs.GetInt32(0) + "D" + rs2.GetInt32(0) + "HIDE")).Checked;
                        bool ext = ((CheckBox)SponsorExtendedSurvey.FindControl("SES" + rs.GetInt32(0) + "D" + rs2.GetInt32(0) + "EXT")).Checked;
                        string t = ((TextBox)SponsorExtendedSurvey.FindControl("SES" + rs.GetInt32(0) + "D" + rs2.GetInt32(0))).Text;
                        string toe = ((TextBox)SponsorExtendedSurvey.FindControl("SES" + rs.GetInt32(0) + "TOE" + rs2.GetInt32(0))).Text;
                        if (t == "" && !hide && !ext && toe == "")
                        {
                            if (!rs2.IsDBNull(1))
                            {
                                Db.exec("DELETE FROM SponsorExtendedSurveyDepartment WHERE SponsorExtendedSurveyDepartmentID = " + rs2.GetInt32(1));
                            }
                        }
                        else if (!rs2.IsDBNull(1))
                        {
                            Db.exec("UPDATE SponsorExtendedSurveyDepartment SET TreatmentOfferEmail = " + (toe == "" ? "NULL" : "'" + toe.Replace("'", "") + "'") + ", Hide = " + (hide ? "1" : "NULL") + ", Ext = " + (ext ? "1" : "NULL") + ", RequiredUserCount = " + (t == "" ? "NULL" : Convert.ToInt32(t).ToString()) + " WHERE SponsorExtendedSurveyDepartmentID = " + rs2.GetInt32(1));
                        }
                        else
                        {
                            Db.exec("INSERT INTO SponsorExtendedSurveyDepartment (TreatmentOfferEmail,SponsorExtendedSurveyID, DepartmentID, RequiredUserCount, Hide, Ext) VALUES (" + (toe == "" ? "NULL" : "'" + toe.Replace("'", "") + "'") + "," + rs.GetInt32(0) + "," + rs2.GetInt32(0) + "," + (t == "" ? "NULL" : Convert.ToInt32(t).ToString()) + "," + (hide ? "1" : "NULL") + "," + (ext ? "1" : "NULL") + ")");
                        }
                    }
                    rs2.Close();

                    string ProjectRoundID = ((DropDownList)SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0))).SelectedValue;
                    string PreviousProjectRoundID = ((DropDownList)SponsorExtendedSurvey.FindControl("PreviousProjectRoundID" + rs.GetInt32(0))).SelectedValue;
                    string Started = ((TextBox)SponsorExtendedSurvey.FindControl("Started" + rs.GetInt32(0))).Text;
                    string Closed = ((TextBox)SponsorExtendedSurvey.FindControl("Closed" + rs.GetInt32(0))).Text;
                    string Internal = ((TextBox)SponsorExtendedSurvey.FindControl("InternalSES" + rs.GetInt32(0))).Text;
                    string RoundText = ((TextBox)SponsorExtendedSurvey.FindControl("RoundText" + rs.GetInt32(0))).Text;
                    string RequiredUserCount = ((TextBox)SponsorExtendedSurvey.FindControl("RequiredUserCountSES" + rs.GetInt32(0))).Text;
                    string EformFeedbackID = ((DropDownList)SponsorExtendedSurvey.FindControl("EformFeedbackID" + rs.GetInt32(0))).SelectedValue;
                    string IndividualFeedbackID = ((DropDownList)SponsorExtendedSurvey.FindControl("IndividualFeedbackID" + rs.GetInt32(0))).SelectedValue;
                    Db.exec("UPDATE SponsorExtendedSurvey SET " +
                        "ProjectRoundID = " + Convert.ToInt32(ProjectRoundID) + ", " +
                        "Internal = '" + Internal.Replace("'", "''") + "', " +
                        "RoundText = '" + RoundText.Replace("'", "''") + "', " +
                        "RequiredUserCount = " + Convert.ToInt32(RequiredUserCount) + ", " +
                        "EformFeedbackID = " + EformFeedbackID.Replace("'", "") + ", " +
                        "PreviousProjectRoundID = " + PreviousProjectRoundID + ", " +
                        "IndividualFeedbackID = " + IndividualFeedbackID + " " +
                        "WHERE SponsorExtendedSurveyID = " + rs.GetInt32(0));
                    if (Started != "")
                    {
                        try
                        {
                            Convert.ToDateTime(Started);
                            Db.exec("UPDATE ProjectRound SET " +
                                "Started = '" + Started.Replace("'", "") + "' " +
                                "WHERE ProjectRoundID = " + Convert.ToInt32(ProjectRoundID), "eFormSqlConnection");
                        }
                        catch (Exception) { }
                    }
                    if (Closed != "")
                    {
                        try
                        {
                            Convert.ToDateTime(Closed);
                            Db.exec("UPDATE ProjectRound SET " +
                                "Closed = '" + Closed.Replace("'", "") + "' " +
                                "WHERE ProjectRoundID = " + Convert.ToInt32(ProjectRoundID), "eFormSqlConnection");
                        }
                        catch (Exception) { }
                    }
                    Db.exec("UPDATE ProjectRound SET " +
                        "AdHocReportCompareWithParent = " + (((CheckBox)SponsorExtendedSurvey.FindControl("EformFeedbackCompare" + rs.GetInt32(0))).Checked ? "NULL" : "1") + " WHERE ProjectRoundID = " + Convert.ToInt32(ProjectRoundID), "eFormSqlConnection");
                    if (!rs.IsDBNull(1))
                    {
                        Db.exec("DELETE FROM SponsorExtendedSurveyBQ WHERE SponsorExtendedSurveyID = " + rs.GetInt32(0));
                        Db.exec("DELETE FROM SponsorExtendedSurveyBA WHERE SponsorExtendedSurveyID = " + rs.GetInt32(0));
                        rs2 = Db.rs("SELECT " +
                            "p.ProjectRoundQOID, " +
                            "o.OptionType, " +
                            "o.OptionID " +
                            "FROM ProjectRoundQO p " +
                            "INNER JOIN Question q ON q.QuestionID = p.QuestionID " +
                            "INNER JOIN [Option] o ON p.OptionID = o.OptionID " +
                            "WHERE p.ProjectRoundID = " + rs.GetInt32(1) + " " +
                            "ORDER BY p.SortOrder", "eFormSqlConnection");
                        while (rs2.Read())
                        {
                            if (SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs2.GetInt32(0)) != null)
                            {
                                if (((DropDownList)SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs2.GetInt32(0))).SelectedValue != "NULL")
                                {
                                    int BQID = Convert.ToInt32(((DropDownList)SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs2.GetInt32(0))).SelectedValue);
                                    string FN = ((TextBox)SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "FN" + rs2.GetInt32(0))).Text;
                                    Db.exec("INSERT INTO SponsorExtendedSurveyBQ (SponsorExtendedSurveyID,ProjectRoundQOID,BQID,FN) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + "," + BQID + "," + (FN != "" ? FN : "NULL") + ")");
                                }
                            }
                            if (rs2.GetInt32(1) == 1 || rs2.GetInt32(1) == 6 || rs2.GetInt32(1) == 7)
                            {
                                SqlDataReader rs3 = Db.rs("SELECT " +
                                    "oc.OptionComponentID " +
                                    "FROM OptionComponents ocs " +
                                    "INNER JOIN OptionComponent oc ON ocs.OptionComponentID = oc.OptionComponentID " +
                                    "WHERE ocs.OptionID = " + rs2.GetInt32(2) + " " +
                                    "ORDER BY ocs.SortOrder", "eFormSqlConnection");
                                while (rs3.Read())
                                {
                                    if (SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs2.GetInt32(0) + "BA" + rs3.GetInt32(0)) != null)
                                    {
                                        if (((DropDownList)SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs2.GetInt32(0) + "BA" + rs3.GetInt32(0))).SelectedValue != "NULL")
                                        {
                                            int BAID = Convert.ToInt32(((DropDownList)SponsorExtendedSurvey.FindControl("ProjectRoundID" + rs.GetInt32(0) + "BQ" + rs2.GetInt32(0) + "BA" + rs3.GetInt32(0))).SelectedValue);
                                            Db.exec("INSERT INTO SponsorExtendedSurveyBA (SponsorExtendedSurveyID,ProjectRoundQOID,OptionComponentID,BAID) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + "," + rs3.GetInt32(0) + "," + BAID + ")");
                                        }
                                    }
                                }
                                rs3.Close();
                            }
                        }
                        rs2.Close();
                    }
                }
                rs.Close();
            }
            else
            {
                PRUID = Db.createProjectRoundUnit(PRUID, Sponsor.Text, 0, 0, 0);

                Db.exec("INSERT INTO Sponsor (Sponsor, Application, ProjectRoundUnitID) VALUES ('" + Sponsor.Text.Replace("'", "''") + "','" + App.Text.Replace("'", "''") + "'," + PRUID + ")");

                rs = Db.rs("SELECT SponsorID, CAST(SponsorKey AS VARCHAR(64)) FROM Sponsor ORDER BY SponsorID DESC");
                if (rs.Read())
                {
                    sponsorID = rs.GetInt32(0);
                    sponsorKey = rs.GetString(1).Substring(0, 8);
                }
                rs.Close();
            }

            if (Logo.PostedFile != null && Logo.PostedFile.ContentLength != 0)
            {
                if (System.IO.File.Exists(Server.MapPath("img/sponsor/" + sponsorKey + sponsorID.ToString() + ".gif")))
                {
                    System.IO.File.Delete(Server.MapPath("img/sponsor/" + sponsorKey + sponsorID.ToString() + ".gif"));
                }
                Logo.PostedFile.SaveAs(Server.MapPath("img/sponsor/" + sponsorKey + sponsorID.ToString() + ".gif"));
            }

            rs = Db.rs("SELECT s.SurveyID, s.Internal FROM ProjectRoundUnit pru INNER JOIN ProjectRound pr ON pru.ProjectRoundID = pr.ProjectRoundID INNER JOIN Project p ON pr.ProjectID = p.ProjectID INNER JOIN ProjectSurvey ps ON p.ProjectID = ps.ProjectID INNER JOIN Survey s ON ps.SurveyID = s.SurveyID WHERE pru.ProjectRoundUnitID = " + PRUID, "eFormSqlConnection");
            while (rs.Read())
            {
                SqlDataReader rs2 = Db.rs("SELECT SponsorProjectRoundUnitID, ProjectRoundUnitID FROM SponsorProjectRoundUnit WHERE ABS(SponsorID) = " + sponsorID + " AND SurveyID = " + rs.GetInt32(0));
                if (rs2.Read())
                {
                    Db.exec("UPDATE SponsorProjectRoundUnit SET " +
                        "Nav = '" + ((TextBox)Surveys.FindControl("Nav" + rs.GetInt32(0))).Text.Replace("'", "''") + "', " +
                        "Feedback = '" + ((TextBox)Surveys.FindControl("Nav" + rs.GetInt32(0))).Text.Replace("'", "''") + "', " +
                        "SponsorID = " + (((CheckBox)Surveys.FindControl("SurveyID" + rs.GetInt32(0))).Checked ? "" : "-") + sponsorID + ", " +
                        "GoToStatistics = " + (((CheckBox)Surveys.FindControl("GoToStatistics" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", " +
                        "OnlyEveryDays = " + (((TextBox)Surveys.FindControl("OnlyEveryDays" + rs.GetInt32(0))).Text != "" ? Convert.ToInt32(((TextBox)Surveys.FindControl("OnlyEveryDays" + rs.GetInt32(0))).Text).ToString() : "NULL") + " " +
                        "WHERE SponsorProjectRoundUnitID = " + rs2.GetInt32(0));
                    Db.exec("UPDATE ProjectRoundUnit SET ReportID = " + ((DropDownList)Surveys.FindControl("ReportID" + rs.GetInt32(0))).SelectedValue + ", IndividualReportID = " + ((DropDownList)Surveys.FindControl("IndividualReportID" + rs.GetInt32(0))).SelectedValue + ", Unit = '" + ((TextBox)Surveys.FindControl("Nav" + rs.GetInt32(0))).Text.Replace("'", "''") + "' WHERE ProjectRoundUnitID = " + rs2.GetInt32(1), "eFormSqlConnection");
                }
                else if (((CheckBox)Surveys.FindControl("SurveyID" + rs.GetInt32(0))).Checked)
                {
                    int sPRUID = Db.createProjectRoundUnit(PRUID, ((TextBox)Surveys.FindControl("Nav" + rs.GetInt32(0))).Text, rs.GetInt32(0), Convert.ToInt32(((DropDownList)Surveys.FindControl("IndividualReportID" + rs.GetInt32(0))).SelectedValue), Convert.ToInt32(((DropDownList)Surveys.FindControl("ReportID" + rs.GetInt32(0))).SelectedValue));
                    Db.exec("INSERT INTO SponsorProjectRoundUnit (" +
                        "SponsorID, " +
                        "ProjectRoundUnitID, " +
                        "Nav, " +
                        "Feedback, " +
                        "SurveyID," +
                        "OnlyEveryDays," +
                        "GoToStatistics" +
                        ") VALUES (" +
                        "" + sponsorID + "," +
                        "" + sPRUID + "," +
                        "'" + ((TextBox)Surveys.FindControl("Nav" + rs.GetInt32(0))).Text.Replace("'", "''") + "'," +
                        "'" + ((TextBox)Surveys.FindControl("Nav" + rs.GetInt32(0))).Text.Replace("'", "''") + "'," +
                        "" + rs.GetInt32(0) + "," +
                        "" + (((TextBox)Surveys.FindControl("OnlyEveryDays" + rs.GetInt32(0))).Text != "" ? Convert.ToInt32(((TextBox)Surveys.FindControl("OnlyEveryDays" + rs.GetInt32(0))).Text).ToString() : "NULL") + "," +
                        "" + (((CheckBox)Surveys.FindControl("GoToStatistics" + rs.GetInt32(0))).Checked ? "1" : "NULL") + "" +
                        ")");
                    SqlDataReader rs3 = Db.rs("SELECT TOP 1 SponsorProjectRoundUnitID FROM SponsorProjectRoundUnit ORDER BY SponsorProjectRoundUnitID DESC");
                    if (rs3.Read())
                    {
                        Db.exec("UPDATE SponsorProjectRoundUnit SET SortOrder = " + rs3.GetInt32(0) + " WHERE SponsorProjectRoundUnitID = " + rs3.GetInt32(0));
                    }
                    rs3.Close();
                }
                rs2.Close();
            }
            rs.Close();

            rs = Db.rs("SELECT " +
                "bq.BQID, " +
                "sbq.SponsorBQID " +
                "FROM BQ " +
                "LEFT OUTER JOIN SponsorBQ sbq ON bq.BQID = sbq.BQID AND ABS(sbq.SponsorID) = " + sponsorID);
            while (rs.Read())
            {
                if (!rs.IsDBNull(1))
                {
                    Db.exec("UPDATE SponsorBQ SET " +
                        "SponsorID = " + (((CheckBox)BQ.FindControl("BQID" + rs.GetInt32(0))).Checked ? "" : "-") + sponsorID + ", " +
                        "Forced = " + (((CheckBox)BQ.FindControl("Forced" + rs.GetInt32(0))).Checked ? "1" : "0") + ", " +
                        "InGrpAdmin = " + (((CheckBox)BQ.FindControl("InGrpAdmin" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", " +
                        "IncludeInTreatmentReq = " + (((CheckBox)BQ.FindControl("IncludeInTreatmentReq" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", " +
                        "Organize = " + (((CheckBox)BQ.FindControl("Organize" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ", " +
                        "Hidden = " + (((CheckBox)BQ.FindControl("Hidden" + rs.GetInt32(0))).Checked ? "1" : "0") + " " +
                        "WHERE SponsorBQID = " + rs.GetInt32(1));
                }
                else if (((CheckBox)BQ.FindControl("BQID" + rs.GetInt32(0))).Checked)
                {
                    Db.exec("INSERT INTO SponsorBQ (BQID,SponsorID,Forced,Hidden,InGrpAdmin,IncludeInTreatmentReq,Organize) VALUES (" + rs.GetInt32(0) + "," + sponsorID + "," + (((CheckBox)BQ.FindControl("Forced" + rs.GetInt32(0))).Checked ? "1" : "0") + "," + (((CheckBox)BQ.FindControl("Hidden" + rs.GetInt32(0))).Checked ? "1" : "0") + "," + (((CheckBox)BQ.FindControl("InGrpAdmin" + rs.GetInt32(0))).Checked ? "1" : "NULL") + "," + (((CheckBox)BQ.FindControl("IncludeInTreatmentReq" + rs.GetInt32(0))).Checked ? "1" : "NULL") + "," + (((CheckBox)BQ.FindControl("Organize" + rs.GetInt32(0))).Checked ? "1" : "NULL") + ")");
                    SqlDataReader rs2 = Db.rs("SELECT TOP 1 SponsorBQID FROM SponsorBQ WHERE SponsorID = " + sponsorID + " AND BQID = " + rs.GetInt32(0) + " ORDER BY SponsorBQID DESC");
                    if (rs2.Read())
                    {
                        Db.exec("UPDATE SponsorBQ SET SortOrder = " + rs2.GetInt32(0) + " WHERE SponsorBQID = " + rs2.GetInt32(0));
                    }
                    rs2.Close();
                }
            }
            rs.Close();

            HttpContext.Current.Response.Redirect("sponsorSetup.aspx?SponsorID=" + sponsorID, true);
        }

        void Back_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("sponsor.aspx", true);
        }
	}
}