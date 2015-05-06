using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class exerciseAreaSetup : System.Web.UI.Page
{
    int eaid = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Save.Click += new EventHandler(Save_Click);
        eaid = (HttpContext.Current.Request.QueryString["ExerciseAreaID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ExerciseAreaID"]) : 0);
        if (HttpContext.Current.Request.QueryString["DeleteImage"] != null)
        {
            Db.exec("UPDATE ExerciseArea SET ExerciseAreaImg = NULL WHERE ExerciseAreaID = " + eaid);
            HttpContext.Current.Response.Redirect("exerciseAreaSetup.aspx?ExerciseAreaID=" + eaid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
        }
        SqlDataReader rs = Db.rs("SELECT LangID FROM Lang");
        while (rs.Read())
        {
            ExerciseLang.Controls.Add(new LiteralControl("<tr><td colspan=\"2\"><hr/></td></tr><tr><td><img src=\"img/langID_" + rs.GetInt32(0) + ".gif\" align=\"right\"/>Name</td><td>"));
            TextBox tb = new TextBox();
            tb.ID = "ExerciseLang" + rs.GetInt32(0);
            tb.Width = Unit.Pixel(200);
            ExerciseLang.Controls.Add(tb);
            ExerciseLang.Controls.Add(new LiteralControl("</td></tr>"));
        }
        rs.Close();

        if (!IsPostBack)
        {
            if (eaid != 0)
            {
                rs = Db.rs("SELECT ea.ExerciseAreaImg FROM ExerciseArea ea WHERE ea.ExerciseAreaID = " + eaid);
                if (rs.Read())
                {
                    if (!rs.IsDBNull(0))
                    {
                        ExerciseImgUploaded.Text = "<img src=\"https://www.healthwatch.se/" + rs.GetString(0) + "\"/><br/>[<a href=\"exerciseAreaSetup.aspx?ExerciseAreaID=" + eaid + "&DeleteImage=1\">delete</a>]";
                    }
                }
                rs.Close();
                rs = Db.rs("SELECT eal.ExerciseArea, eal.Lang FROM ExerciseAreaLang eal WHERE eal.ExerciseAreaID = " + eaid);
                while (rs.Read())
                {
                    ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(1))).Text = (rs.IsDBNull(0) ? "" : rs.GetString(0));
                }
                rs.Close();
            }
        }
    }

    void Save_Click(object sender, EventArgs e)
    {
        SqlDataReader rs;

        if (eaid == 0)
        {
            Db.exec("INSERT INTO ExerciseArea (ExerciseAreaImg) VALUES (NULL)");
            rs = Db.rs("SELECT TOP 1 ExerciseAreaID FROM ExerciseArea ORDER BY ExerciseAreaID DESC");
            if (rs.Read())
            {
                eaid = rs.GetInt32(0);
            }
            rs.Close();
            Db.exec("UPDATE ExerciseArea SET ExerciseAreaSortOrder = ExerciseAreaID WHERE ExerciseAreaSortOrder IS NULL");
        }
        rs = Db.rs("SELECT l.LangID, el.ExerciseAreaLangID FROM Lang l LEFT OUTER JOIN ExerciseAreaLang el ON l.LangID = el.Lang AND el.ExerciseAreaID = " + eaid);
        while (rs.Read())
        {
            if (!rs.IsDBNull(1))
            {
                if (((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text != "")
                {
                    Db.exec("UPDATE ExerciseAreaLang SET ExerciseArea = '" + ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text.Replace("'", "''") + "' WHERE ExerciseAreaLangID = " + rs.GetInt32(1));
                }
                else
                {
                    Db.exec("UPDATE ExerciseAreaLang SET ExerciseAreaID = -ABS(ExerciseAreaID) WHERE ExerciseAreaID = " + eaid + " AND Lang = " + rs.GetInt32(0));
                }
            }
            else
            {
                if (((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text != "")
                {
                    Db.exec("INSERT INTO ExerciseAreaLang (ExerciseAreaID, Lang, ExerciseArea) VALUES (" + eaid + "," + rs.GetInt32(0) + "," +
                        "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text.Replace("'", "''") + "'" +
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
                    string url = "img/exercise/area" + eaid + "." + format;
                    string fname = System.Configuration.ConfigurationManager.AppSettings["hwUNC"] + "\\img\\exercise\\area" + eaid + "." + format;
                    if (System.IO.File.Exists(fname))
                    {
                        System.IO.File.Move(fname, fname + "." + DateTime.Now.ToString("yyyyMMddHHmmss"));
                    }
                    ExerciseImg.PostedFile.SaveAs(fname);

                    Db.exec("UPDATE ExerciseArea SET ExerciseAreaImg = '" + url + "' WHERE ExerciseAreaID = " + eaid);
                }
            }
            catch (Exception)
            {
            }
        }
        HttpContext.Current.Response.Redirect("exerciseAreaSetup.aspx?ExerciseAreaID=" + eaid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
    }
}