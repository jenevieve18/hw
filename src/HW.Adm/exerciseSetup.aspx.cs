﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CKEditor.NET;

public partial class exerciseSetup : System.Web.UI.Page
{
    int eid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Save.Click += new EventHandler(Save_Click);
        eid = (HttpContext.Current.Request.QueryString["ExerciseID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ExerciseID"]) : 0);
        if (HttpContext.Current.Request.QueryString["DeleteImage"] != null)
        {
            Db.exec("UPDATE Exercise SET ExerciseImg = NULL WHERE ExerciseID = " + eid);
            HttpContext.Current.Response.Redirect("exerciseSetup.aspx?ExerciseID=" + eid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
        }
        if (HttpContext.Current.Request.QueryString["DeleteVariant"] != null)
        {
            Db.exec("UPDATE ExerciseVariant SET ExerciseID = -ABS(ExerciseID) WHERE ExerciseVariantID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["DeleteVariant"]) + " AND ExerciseID = " + eid);
        }
        SqlDataReader rs = Db.rs("SELECT " +
            "ev.ExerciseVariantID, " +
            "ev.ExerciseTypeID, " +
            "etl.ExerciseType, " +
            "etl.ExerciseSubType " +
            "FROM ExerciseVariant ev " +
            "INNER JOIN ExerciseTypeLang etl ON ev.ExerciseTypeID = etl.ExerciseTypeID " +
            "WHERE etl.Lang = 1 " +
            "AND ev.ExerciseID = " + eid);
        while (rs.Read())
        {
            ExerciseVariant.Controls.Add(new LiteralControl("<tr><td colspan=\"2\"><hr/></td></tr>"));
            ExerciseVariant.Controls.Add(new LiteralControl("<tr><td colspan=\"2\">" +
                //"<a href=\"javascript:if(confirm('Are you sure?')){location.href='exerciseSetup.aspx?ExerciseID=" + eid + "&DeleteVariant=" + rs.GetInt32(0) + "';}\"><img src=\"img/DelToolSmall.gif\" border=\"0\"/></a> " + 
                rs.GetString(2) + (!rs.IsDBNull(3) ? " (" + rs.GetString(3) + ")" : "") + "</td></tr>"));
            SqlDataReader rs2 = Db.rs("SELECT " +
                "l.LangID, " +
                "evl.ExerciseContent, " +
                "evl.ExerciseFile, " +
                "evl.ExerciseWindowX, " +
                "evl.ExerciseWindowY " +
                "FROM Lang l " +
                "LEFT OUTER JOIN ExerciseVariantLang evl ON evl.Lang = l.LangID AND evl.ExerciseVariantID = " + rs.GetInt32(0));
            while (rs2.Read())
            {
                ExerciseVariant.Controls.Add(new LiteralControl("<tr><td valign=\"top\"><img src=\"img/langID_" + rs2.GetInt32(0) + ".gif\" align=\"right\"/></td><td>"));
                switch (rs.GetInt32(1))
                {
                    case 1:
                		//CKEditorControl tb = new CKEditorControl();
                        TextBox tb = new TextBox();
                        string editorID = "EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0);
                        tb.ID = editorID;
                        tb.TextMode = TextBoxMode.MultiLine;
                        tb.Rows = 20;
                        tb.Width = 800;
                        tb.CssClass = "text summernote";
                        ExerciseVariant.Controls.Add(tb);
//                        ExerciseVariant.Controls.Add(new LiteralControl(
//string.Format(@"<script>var editor{0} = CodeMirror.fromTextArea(document.getElementById('{0}'), {{
//              lineNumbers: true,
//              mode: 'xml'
//          }});</script><hr>", editorID)));
                        if (!IsPostBack)
                        {
                            tb.Text = (!rs2.IsDBNull(1) ? rs2.GetString(1) : "");
                        }
                        break;
                    default:
                        if (!rs2.IsDBNull(2))
                        {
                            ExerciseVariant.Controls.Add(new LiteralControl("<a target=\"_blank\" href=\"http://www.healthwatch.se/exercise/" + rs2.GetString(2) + "\">" + rs2.GetString(2) + "</a><br/>"));
                        }
                        else
                        {
                            ExerciseVariant.Controls.Add(new LiteralControl("<span style=\"color:#cc0000;\">Missing</span><br/>"));
                        }
                        System.Web.UI.HtmlControls.HtmlInputFile f = new System.Web.UI.HtmlControls.HtmlInputFile();
                        f.ID = "EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0);
                        ExerciseVariant.Controls.Add(f);
                        break;
                }
                ExerciseVariant.Controls.Add(new LiteralControl("</td></tr>"));
            }
            rs2.Close();
        }
        rs.Close();
        rs = Db.rs("SELECT LangID FROM Lang");
        while (rs.Read())
        {
            ExerciseLang.Controls.Add(new LiteralControl("<tr><td colspan=\"2\"><hr/></td></tr><tr><td><img src=\"img/langID_" + rs.GetInt32(0) + ".gif\" align=\"right\"/>Name</td><td>"));
            TextBox tb = new TextBox();
            tb.ID = "ExerciseLang" + rs.GetInt32(0);
            tb.Width = Unit.Pixel(200);
            ExerciseLang.Controls.Add(tb);
            ExerciseLang.Controls.Add(new LiteralControl("</td></tr><tr><td>Teaser</td><td>"));
            tb = new TextBox();
            tb.ID = "ExerciseLangTeaser" + rs.GetInt32(0);
            tb.Width = Unit.Pixel(500);
            ExerciseLang.Controls.Add(tb);
            ExerciseLang.Controls.Add(new LiteralControl("</td></tr><tr><td>Time</td><td>"));
            tb = new TextBox();
            tb.ID = "ExerciseLangTime" + rs.GetInt32(0);
            tb.Width = Unit.Pixel(100);
            ExerciseLang.Controls.Add(tb);
            ExerciseLang.Controls.Add(new LiteralControl("</td></tr>"));
        }
        rs.Close();

        if (!IsPostBack)
        {
            ExerciseTypeID.Items.Add(new ListItem("< none >", "0"));
            rs = Db.rs("SELECT et.ExerciseTypeID, etl.ExerciseType, etl.ExerciseSubtype FROM ExerciseType et INNER JOIN ExerciseTypeLang etl ON et.ExerciseTypeID = etl.ExerciseTypeID AND etl.Lang = 1");
            while (rs.Read())
            {
                ExerciseTypeID.Items.Add(new ListItem(rs.GetString(1) + (!rs.IsDBNull(2) ? " (" + rs.GetString(2) + ")" : ""), rs.GetInt32(0).ToString()));
            }
            rs.Close();
            rs = Db.rs("SELECT ea.ExerciseAreaID, (SELECT TOP 1 eal.ExerciseArea FROM ExerciseAreaLang eal WHERE eal.ExerciseAreaID = ea.ExerciseAreaID) FROM ExerciseArea ea ORDER BY ea.ExerciseAreaSortOrder");
            while (rs.Read())
            {
                ExerciseAreaID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
            }
            rs.Close();
            ExerciseCategoryID.Items.Add(new ListItem("< none >", "0"));
            rs = Db.rs("SELECT ea.ExerciseCategoryID, (SELECT TOP 1 eal.ExerciseCategory FROM ExerciseCategoryLang eal WHERE eal.ExerciseCategoryID = ea.ExerciseCategoryID) FROM ExerciseCategory ea ORDER BY ea.ExerciseCategorySortOrder");
            while (rs.Read())
            {
                ExerciseCategoryID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
            }
            rs.Close();

            if(eid != 0)
            {
                rs = Db.rs(
@"SELECT e.ExerciseAreaID,
e.ExerciseImg,
e.RequiredUserLevel,
e.Minutes,
e.ExerciseCategoryID,
e.Script
FROM Exercise e WHERE e.ExerciseID = " + eid);
                if(rs.Read())
                {
                    ExerciseAreaID.SelectedValue = rs.GetInt32(0).ToString();
                    if(!rs.IsDBNull(1))
                    {
                        ExerciseImgUploaded.Text = "<img src=\"https://www.healthwatch.se/" + rs.GetString(1) + "\"/><br/>[<a href=\"exerciseSetup.aspx?ExerciseID=" + eid + "&DeleteImage=1\">delete</a>]";
                    }
                    RequiredUserLevel.SelectedValue = rs.GetInt32(2).ToString();
                    Minutes.Text = (rs.IsDBNull(3) ? 0 : rs.GetInt32(3)).ToString();
                    if (!rs.IsDBNull(4))
                    {
                        ExerciseCategoryID.SelectedValue = rs.GetInt32(4).ToString();
                    }
                    if (!rs.IsDBNull(5))
                    {
                        textBoxJavascript.Text = rs.GetString(5);
                    }
                }
                rs.Close();
                rs = Db.rs("SELECT el.Exercise, el.Lang, el.ExerciseTeaser, el.ExerciseTime FROM ExerciseLang el WHERE el.ExerciseID = " + eid);
                while (rs.Read())
                {
                    ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(1))).Text = (rs.IsDBNull(0) ? "" : rs.GetString(0));
                    ((TextBox)ExerciseLang.FindControl("ExerciseLangTeaser" + rs.GetInt32(1))).Text = (rs.IsDBNull(2) ? "" : rs.GetString(2));
                    ((TextBox)ExerciseLang.FindControl("ExerciseLangTime" + rs.GetInt32(1))).Text = (rs.IsDBNull(3) ? "" : rs.GetString(3));
                }
                rs.Close();
            }
        }
    }

    void Save_Click(object sender, EventArgs e)
    {
        SqlDataReader rs;

        if (eid != 0)
        {
            //Db.exec("UPDATE Exercise SET " +
            //    "ExerciseAreaID = " + Convert.ToInt32(ExerciseAreaID.SelectedValue) + ", " +
            //    "ExerciseCategoryID = " + (Convert.ToInt32(ExerciseCategoryID.SelectedValue) != 0 ? Convert.ToInt32(ExerciseCategoryID.SelectedValue).ToString() : "NULL") + ", " +
            //    "RequiredUserLevel = " + Convert.ToInt32(RequiredUserLevel.SelectedValue) + ", " +
            //    "Minutes = " + Convert.ToInt32(Minutes.Text) + ", " +
            //    "Script = '" + textBoxJavascript.Text.Replace("'", "''") + "'" + " " +
            //    "WHERE ExerciseID = " + eid);
            string query = @"
UPDATE Exercise SET 
ExerciseAreaID = @ExerciseAreaID, 
ExerciseCategoryID = @ExerciseCategoryID, 
RequiredUserLevel = @RequiredUserLevel, 
Minutes = @Minutes, 
Script = @Script 
WHERE ExerciseID = @ExerciseID";
            Db.ExecuteNonQuery(
                query,
                new SqlParameter("@ExerciseAreaID", Convert.ToInt32(ExerciseAreaID.SelectedValue)),
                new SqlParameter("@ExerciseCategoryID", (Convert.ToInt32(ExerciseCategoryID.SelectedValue) != 0 ? Convert.ToInt32(ExerciseCategoryID.SelectedValue).ToString() : (object)DBNull.Value)),
                new SqlParameter("@RequiredUserLevel", Convert.ToInt32(RequiredUserLevel.SelectedValue)),
                new SqlParameter("@Minutes", Convert.ToInt32(Minutes.Text)),
                new SqlParameter("@Script", textBoxJavascript.Text),
                new SqlParameter("@ExerciseID", eid)
                );
        }
        else
        {
            //Db.exec("INSERT INTO Exercise (ExerciseCategoryID,ExerciseAreaID,RequiredUserLevel,Minutes,Script) VALUES (" + (Convert.ToInt32(ExerciseCategoryID.SelectedValue) != 0 ? Convert.ToInt32(ExerciseCategoryID.SelectedValue).ToString() : "NULL") + "," + Convert.ToInt32(ExerciseAreaID.SelectedValue) + "," + Convert.ToInt32(RequiredUserLevel.SelectedValue) + "," + Convert.ToInt32(Minutes.Text) + ", '" + textBoxJavascript.Text.Replace("'", "''") + "')");
            string query = @"
INSERT INTO Exercise (ExerciseCategoryID,ExerciseAreaID,RequiredUserLevel,Minutes,Script) 
VALUES (@ExerciseCategoryID,@ExerciseAreaID,@RequiredUserLevel,@Minutes,@Script)";
            Db.ExecuteNonQuery(
                query,
                new SqlParameter("@ExerciseCategoryID", Convert.ToInt32(ExerciseCategoryID.SelectedValue) != 0 ? Convert.ToInt32(ExerciseCategoryID.SelectedValue).ToString() : (object)DBNull.Value),
                new SqlParameter("@ExerciseAreaID", ExerciseAreaID.SelectedValue),
                new SqlParameter("@RequiredUserLevel", RequiredUserLevel.SelectedValue),
                new SqlParameter("@Minutes", Minutes.Text),
                new SqlParameter("@Script", textBoxJavascript.Text)
            );
            rs = Db.rs("SELECT TOP 1 ExerciseID FROM Exercise ORDER BY ExerciseID DESC");
            if (rs.Read())
            {
                eid = rs.GetInt32(0);
            }
            rs.Close();
        }
        rs = Db.rs("SELECT " +
            "ev.ExerciseVariantID, " +
            "ev.ExerciseTypeID " +
            "FROM ExerciseVariant ev " +
            "WHERE ev.ExerciseID = " + eid);
        while (rs.Read())
        {
            SqlDataReader rs2 = Db.rs("SELECT " +
                "l.LangID, " +
                "evl.ExerciseVariantLangID " +
                "FROM Lang l " +
                "LEFT OUTER JOIN ExerciseVariantLang evl ON evl.Lang = l.LangID AND evl.ExerciseVariantID = " + rs.GetInt32(0));
            while (rs2.Read())
            {
                switch (rs.GetInt32(1))
                {
                    case 1:
                        if (!rs2.IsDBNull(1))
                        {
                            //Db.exec("UPDATE ExerciseVariantLang SET ExerciseContent = '" + ((TextBox)ExerciseVariant.FindControl("EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0))).Text.Replace("'", "''") + "' WHERE ExerciseVariantLangID = " + rs2.GetInt32(1));
                            string query = "UPDATE ExerciseVariantLang SET ExerciseContent = @ExerciseContent WHERE ExerciseVariantLangID = @ExerciseVariantLangID";
                            Db.ExecuteNonQuery(
                                query,
                                new SqlParameter("@ExerciseContent", ((TextBox)ExerciseVariant.FindControl("EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0))).Text),
                                new SqlParameter("@ExerciseVariantLangID", rs2.GetInt32(1))
                                );
                        }
                        else
                        {
                            //Db.exec("INSERT INTO ExerciseVariantLang (ExerciseVariantID,Lang,ExerciseContent) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + ",'" + ((TextBox)ExerciseVariant.FindControl("EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0))).Text.Replace("'", "''") + "')");
                            string query = "INSERT INTO ExerciseVariantLang (ExerciseVariantID,Lang,ExerciseContent) " +
                                "VALUES (@ExerciseVariantID,@Lang,@ExerciseContent)";
                            Db.ExecuteNonQuery(
                                query,
                                new SqlParameter("@ExerciseVariantID", rs.GetInt32(0)),
                                new SqlParameter("@Lang", rs2.GetInt32(0)),
                                new SqlParameter("@ExerciseContent", ((TextBox)ExerciseVariant.FindControl("EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0))).Text)
                                );
                        }
                        break;
                    default:
                        System.Web.UI.HtmlControls.HtmlInputFile f = ((System.Web.UI.HtmlControls.HtmlInputFile)ExerciseVariant.FindControl("EV" + rs.GetInt32(0) + "L" + rs2.GetInt32(0)));
                        if (f.PostedFile != null && f.PostedFile.ContentLength != 0)
                        {
                            string fff = f.PostedFile.FileName;
                            string ffff = rs.GetInt32(0) + "." + rs2.GetInt32(0) + fff.Substring(fff.LastIndexOf("."));
                            string ff = System.Configuration.ConfigurationManager.AppSettings["hwContentUNC"] + "\\exercise\\" + ffff;
                            if (System.IO.File.Exists(ff))
                            {
                                System.IO.File.Move(ff, ff + "." + DateTime.Now.Ticks);
                            }
                            f.PostedFile.SaveAs(ff);
                            if (!rs2.IsDBNull(1))
                            {
                                Db.exec("UPDATE ExerciseVariantLang SET ExerciseFilesize = " + f.PostedFile.ContentLength + ", ExerciseFile = '" + ffff + "' WHERE ExerciseVariantLangID = " + rs2.GetInt32(1));
                            }
                            else
                            {
                                Db.exec("INSERT INTO ExerciseVariantLang (ExerciseVariantID,Lang,ExerciseFilesize,ExerciseFile) VALUES (" + rs.GetInt32(0) + "," + rs2.GetInt32(0) + "," + f.PostedFile.ContentLength + ",'" + ffff + "')");
                            }
                        }
                        break;
                }
            }
            rs2.Close();
        }
        rs.Close();
        rs = Db.rs("SELECT l.LangID, el.ExerciseLangID FROM Lang l LEFT OUTER JOIN ExerciseLang el ON l.LangID = el.Lang AND el.ExerciseID = " + eid);
        while (rs.Read())
        {
            if (!rs.IsDBNull(1))
            {
                if (((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text != "")
                {
                    Db.exec("UPDATE ExerciseLang SET Exercise = '" + ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text.Replace("'", "''") + "', " +
                        "ExerciseTeaser = " + (((TextBox)ExerciseLang.FindControl("ExerciseLangTeaser" + rs.GetInt32(0))).Text != "" ? "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLangTeaser" + rs.GetInt32(0))).Text.Replace("'", "''") + "'" : "NULL") + "," +
                        "ExerciseTime = " + (((TextBox)ExerciseLang.FindControl("ExerciseLangTime" + rs.GetInt32(0))).Text != "" ? "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLangTime" + rs.GetInt32(0))).Text.Replace("'", "''") + "'" : "NULL") + " " +
                        "WHERE ExerciseLangID = " + rs.GetInt32(1));
                }
                else
                {
                    Db.exec("UPDATE ExerciseLang SET ExerciseID = -ABS(ExerciseID) WHERE ExerciseID = " + eid + " AND Lang = " + rs.GetInt32(0));
                }
            }
            else
            {
                if (((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text != "")
                {
                    Db.exec("INSERT INTO ExerciseLang (ExerciseID, Lang, Exercise, ExerciseTeaser, ExerciseTime) VALUES (" + eid + "," + rs.GetInt32(0) + "," +
                        "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text.Replace("'", "''") + "'," +
                        (((TextBox)ExerciseLang.FindControl("ExerciseLangTeaser" + rs.GetInt32(0))).Text != "" ? "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLangTeaser" + rs.GetInt32(0))).Text.Replace("'", "''") + "'" : "NULL") + "," +
                        (((TextBox)ExerciseLang.FindControl("ExerciseLangTime" + rs.GetInt32(0))).Text != "" ? "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLangTime" + rs.GetInt32(0))).Text.Replace("'", "''") + "'" : "NULL") +
                        ")");
                }
            }
        }
        rs.Close();
        if (ExerciseImg.PostedFile != null && ExerciseImg.PostedFile.ContentLength > 0)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(ExerciseImg.PostedFile.InputStream);
                string format = "";
                if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)) format = "GIF";
                else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)) format = "JPG";
                else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)) format = "PNG";

                if (format != "")
                {
                    string url = "img/exercise/" + eid + "." + format;
                    string fname = System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\img\\exercise\\" + eid + "." + format;
                    if (System.IO.File.Exists(fname))
                    {
                        System.IO.File.Move(fname, fname + "." + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    }
                    ExerciseImg.PostedFile.SaveAs(fname);

                    Db.exec("UPDATE Exercise SET ExerciseImg = '" + url + "' WHERE ExerciseID = " + eid);
                }
            }
            catch (Exception)
            {
            }
        }
        if (ExerciseTypeID.SelectedValue != "0")
        {
            Db.exec("INSERT INTO ExerciseVariant (ExerciseID,ExerciseTypeID) VALUES (" + eid + "," + Convert.ToInt32(ExerciseTypeID.SelectedValue) + ")");
        }
        HttpContext.Current.Response.Redirect("exerciseSetup.aspx?ExerciseID=" + eid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
    }
}