using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using HW.Core.Helpers;

namespace HW.Adm
{
    public partial class bqSetup : System.Web.UI.Page
    {
        int questionID = 0;
        int answerID = 0;
        bool answerType = false;
        bool freeType = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            AddAnswer.Click += new EventHandler(AddAnswer_Click);
            AddCond.Click += new EventHandler(AddCond_Click);
            Save.Click += new EventHandler(Save_Click);
            Back.Click += new EventHandler(Back_Click);

            questionID = (HttpContext.Current.Request.QueryString["BQID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["BQID"]) : 0);
            answerID = (HttpContext.Current.Request.QueryString["BAID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["BAID"]) : 0);

            SqlDataReader rs;

            if (HttpContext.Current.Request.QueryString["Delete"] != null)
            {
                Db.exec("UPDATE BA SET BQID = -" + questionID + " WHERE BAID = " + HttpContext.Current.Request.QueryString["Delete"] + " AND BQID = " + questionID);
                HttpContext.Current.Response.Redirect("bqSetup.aspx?BQID=" + questionID, true);
            }
            if (HttpContext.Current.Request.QueryString["DeleteV"] != null)
            {
                Db.exec("DELETE FROM BQvisibility WHERE BQvisibilityID = " + HttpContext.Current.Request.QueryString["DeleteV"]);
                HttpContext.Current.Response.Redirect("bqSetup.aspx?BQID=" + questionID, true);
            }
            if (HttpContext.Current.Request.QueryString["MoveUp"] != null)
            {
                rs = Db.rs("SELECT SortOrder FROM BA WHERE BQID = " + questionID + " AND BAID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
                if (rs.Read())
                {
                    SqlDataReader rs2 = Db.rs("SELECT TOP 1 BAID, SortOrder FROM BA WHERE BQID = " + questionID + " AND SortOrder < " + rs.GetInt32(0) + " ORDER BY SortOrder DESC");
                    if (rs2.Read())
                    {
                        Db.exec("UPDATE BA SET SortOrder = " + rs2.GetInt32(1) + " WHERE BAID = " + HttpContext.Current.Request.QueryString["MoveUp"]);
                        Db.exec("UPDATE BA SET SortOrder = " + rs.GetInt32(0) + " WHERE BAID = " + rs2.GetInt32(0));
                    }
                    rs2.Close();
                }
                rs.Close();
                HttpContext.Current.Response.Redirect("bqSetup.aspx?BQID=" + questionID, true);
            }

            if (!IsPostBack)
            {
                BQIDBAID.Items.Add(new ListItem("< none >", "0:0"));
                if (questionID != 0)
                {
                    rs = Db.rs("SELECT Internal, Type,MaxLength,ReqLength,DefaultVal,Comparison,MeasurementUnit,Layout,Variable FROM BQ WHERE BQID = " + questionID);
                    if (rs.Read())
                    {
                        Internal.Text = rs.GetString(0);
                        Type.SelectedValue = rs.GetInt32(1).ToString();
                        MaxLength.Text = (rs.IsDBNull(2) ? "" : rs.GetInt32(2).ToString());
                        ReqLength.Text = (rs.IsDBNull(3) ? "" : rs.GetInt32(3).ToString());
                        DefaultVal.Text = (rs.IsDBNull(4) ? "" : rs.GetString(4));
                        Comparison.SelectedValue = (rs.IsDBNull(5) ? "NULL" : "1");
                        MeasurementUnit.Text = (rs.IsDBNull(6) ? "" : rs.GetString(6));
                        Layout.SelectedValue = (rs.IsDBNull(7) ? "NULL" : rs.GetInt32(7).ToString());
                        Variable.Text = (rs.IsDBNull(8) ? "" : rs.GetString(8));
                    }
                    rs.Close();

                    rs = Db.rs("SELECT BQID, Internal FROM BQ WHERE Type IN (1,6,7)");
                    while (rs.Read())
                    {
                        SqlDataReader rs2 = Db.rs("SELECT a.BAID, a.Internal FROM BA a WHERE a.BAID NOT IN (SELECT DISTINCT x.BAID FROM BQvisibility x WHERE x.BQID = a.BQID AND x.ChildBQID = " + questionID + ") AND BQID = " + rs.GetInt32(0));
                        while (rs2.Read())
                        {
                            BQIDBAID.Items.Add(new ListItem((rs.GetString(1).Length > 20 ? rs.GetString(1).Substring(0, 17) + "..." : rs.GetString(1)) + " = " + (rs2.GetString(1).Length > 20 ? rs2.GetString(1).Substring(0, 17) + "..." : rs2.GetString(1)), rs.GetInt32(0).ToString() + ":" + rs2.GetInt32(0).ToString()));
                        }
                        rs2.Close();
                    }
                    rs.Close();

                    if (answerID != 0)
                    {
                        rs = Db.rs("SELECT Internal FROM BA WHERE BAID = " + answerID);
                        if (rs.Read())
                        {
                            Answer.Text = rs.GetString(0);
                        }
                        rs.Close();
                    }
                }
                else
                {
                    Comparison.SelectedValue = "NULL";
                }
            }
        }

        void AddCond_Click(object sender, EventArgs e)
        {
            Condition.Visible = true;
        }

        void Back_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("bq.aspx", true);
        }

        void Save_Click(object sender, EventArgs e)
        {
            if (questionID == 0)
            {
                if (Type.SelectedValue == "4" || Type.SelectedValue == "2")
                {
                    Db.exec("INSERT INTO BQ (Variable,Internal,Type,MaxLength,ReqLength,DefaultVal,Comparison,MeasurementUnit) VALUES ('" + Variable.Text.Replace("'", "''") + "','" + Internal.Text.Replace("'", "''") + "'," + Type.SelectedValue + "," + (MaxLength.Text != "" ? MaxLength.Text.Replace("'", "") : "NULL") + "," + (ReqLength.Text != "" ? ReqLength.Text.Replace("'", "") : "NULL") + "," + (DefaultVal.Text != "" ? "'" + DefaultVal.Text.Replace("'", "''") + "'" : "NULL") + "," + Comparison.SelectedValue + "," + (MeasurementUnit.Text != "" ? "'" + MeasurementUnit.Text.Replace("'", "''") + "'" : "NULL") + ")");
                }
                else if (Type.SelectedValue == "1")
                {
                    Db.exec("INSERT INTO BQ (Variable,Layout,Internal,Type,Comparison) VALUES ('" + Variable.Text.Replace("'", "''") + "'," + Layout.SelectedValue + ",'" + Internal.Text.Replace("'", "''") + "'," + Type.SelectedValue + "," + Comparison.SelectedValue + ")");
                }
                else
                {
                    Db.exec("INSERT INTO BQ (Variable,Internal,Type,Comparison) VALUES ('" + Variable.Text.Replace("'", "''") + "','" + Internal.Text.Replace("'", "''") + "'," + Type.SelectedValue + "," + Comparison.SelectedValue + ")");
                }
                SqlDataReader rs = Db.rs("SELECT TOP 1 BQID FROM BQ ORDER BY BQID DESC");
                if (rs.Read())
                {
                    questionID = rs.GetInt32(0);
                }
                rs.Close();
            }
            else
            {
                if (Type.SelectedValue == "4" || Type.SelectedValue == "2")
                {
                    Db.exec("UPDATE BQ SET Variable = '" + Variable.Text.Replace("'", "") + "', Comparison = " + Comparison.SelectedValue + ", Type = " + Type.SelectedValue + ", Internal = '" + Internal.Text.Replace("'", "''") + "',DefaultVal=" + (DefaultVal.Text != "" ? "'" + DefaultVal.Text.Replace("'", "''") + "'" : "NULL") + ",MeasurementUnit=" + (MeasurementUnit.Text != "" ? "'" + MeasurementUnit.Text.Replace("'", "''") + "'" : "NULL") + ",MaxLength=" + (MaxLength.Text != "" ? MaxLength.Text.Replace("'", "") : "NULL") + ",ReqLength=" + (ReqLength.Text != "" ? ReqLength.Text.Replace("'", "") : "NULL") + " WHERE BQID = " + questionID);
                }
                else if (Type.SelectedValue == "1")
                {
                    Db.exec("UPDATE BQ SET Variable = '" + Variable.Text.Replace("'", "") + "', Layout = " + Layout.SelectedValue + ", Comparison = " + Comparison.SelectedValue + ", Type = " + Type.SelectedValue + ", Internal = '" + Internal.Text.Replace("'", "''") + "' WHERE BQID = " + questionID);
                }
                else
                {
                    Db.exec("UPDATE BQ SET Variable = '" + Variable.Text.Replace("'", "") + "', Comparison = " + Comparison.SelectedValue + ", Type = " + Type.SelectedValue + ", Internal = '" + Internal.Text.Replace("'", "''") + "' WHERE BQID = " + questionID);
                }
            }

            if (answerID != 0 && (Type.SelectedValue == "1" || Type.SelectedValue == "7" || Type.SelectedValue == "9"))
            {
                Db.exec("UPDATE BA SET Internal = '" + Answer.Text.Replace("'", "''") + "' WHERE BAID = " + answerID);
            }
            else if (Answer.Text != "" && (Type.SelectedValue == "1" || Type.SelectedValue == "7" || Type.SelectedValue == "9"))
            {
                Db.exec("INSERT INTO BA (BQID,Internal) VALUES (" + questionID + ",'" + Answer.Text.Replace("'", "''") + "')");
                SqlDataReader rs = Db.rs("SELECT TOP 1 BAID FROM BA ORDER BY BAID DESC");
                if (rs.Read())
                {
                    Db.exec("UPDATE BA SET SortOrder = " + rs.GetInt32(0) + " WHERE BAID = " + rs.GetInt32(0));
                }
                rs.Close();
            }

            if (BQIDBAID.SelectedValue != "0:0")
            {
                Db.exec("INSERT INTO BQvisibility (ChildBQID,BQID,BAID) VALUES (" + questionID + "," + BQIDBAID.SelectedValue.Split(':')[0] + "," + BQIDBAID.SelectedValue.Split(':')[1] + ")");
            }
            HttpContext.Current.Response.Redirect("bqSetup.aspx?BQID=" + questionID, true);
        }

        void AddAnswer_Click(object sender, EventArgs e)
        {
            NewAnswer.Visible = true;
            Answer.Text = "";
            answerID = 0;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            answerType = (Type.SelectedValue == "1" || Type.SelectedValue == "7" || Type.SelectedValue == "9");
            freeType = (Type.SelectedValue == "4" || Type.SelectedValue == "2");

            if (answerID != 0 && questionID != 0 && answerType)
            {
                NewAnswer.Visible = true;
            }
            FreeParams.Visible = freeType;
            RadioParams.Visible = (Type.SelectedValue == "1");

            AddAnswer.Visible = (questionID != 0 && answerType);
            AddCond.Visible = (questionID != 0);
            Answers.Visible = (questionID != 0 && answerType);

            if (questionID != 0)
            {
                SqlDataReader rs;

                if (answerType)
                {
                    rs = Db.rs("SELECT BAID, Internal, Value FROM BA WHERE BQID = " + questionID + " ORDER BY SortOrder");
                    if (rs.Read())
                    {
                        Answers.Text = "<B>Answers</B>";
                        int cx = 0;
                        do
                        {
                            Answers.Text += "<br/><A HREF=\"bqSetup.aspx?BQID=" + questionID + "&MoveUp=" + rs.GetInt32(0) + "\"><img height=\"11\" src=\"img/" + (cx++ > 0 ? "UpToolSmall" : "null") + ".gif\" border=\"0\"></A>&nbsp;<A HREF=\"bqSetup.aspx?BQID=" + questionID + "&BAID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>&nbsp;(" + (rs.IsDBNull(2) ? "?" : rs.GetInt32(2).ToString()) + ")&nbsp;<A HREF=\"bqSetup.aspx?BQID=" + questionID + "&Delete=" + rs.GetInt32(0) + "\"><img height=\"11\" src=\"img/DelToolSmall.gif\" border=\"0\"></A>";
                        }
                        while (rs.Read());
                    }
                    rs.Close();
                }

                rs = Db.rs("SELECT x.BQvisibilityID, bq.Internal, ba.Internal FROM BQvisibility x INNER JOIN BQ ON x.BQID = BQ.BQID INNER JOIN BA ON x.BAID = BA.BAID WHERE ChildBQID = " + questionID);
                if (rs.Read())
                {
                    Visibility.Text = "<B>Visibility conditions</B> (OR logic)";
                    int cx = 0;
                    do
                    {
                        Visibility.Text += "<br/>" + rs.GetString(1) + " = " + rs.GetString(2) + "&nbsp;<A HREF=\"bqSetup.aspx?BQID=" + questionID + "&DeleteV=" + rs.GetInt32(0) + "\"><img height=\"11\" src=\"img/DelToolSmall.gif\" border=\"0\"></A>";
                    }
                    while (rs.Read());
                }
                rs.Close();
            }
        }
    }
}