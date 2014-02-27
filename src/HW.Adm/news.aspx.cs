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
    public partial class news : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                transLangFrom.Items.Add(new ListItem("en", "en"));
                transLangFrom.Items.Add(new ListItem("sv", "sv"));
                transLangFrom.Items.Add(new ListItem("ja", "ja"));
                transLangFrom.Items.Add(new ListItem("ru", "ru"));
                transLangFrom.Items.Add(new ListItem("es", "es"));

                transLangTo.Items.Add(new ListItem("en", "en"));
                transLangTo.Items.Add(new ListItem("sv", "sv"));
                transLangTo.Items.Add(new ListItem("ja", "ja"));
                transLangTo.Items.Add(new ListItem("ru", "ru"));
                transLangTo.Items.Add(new ListItem("es", "es"));

                transLangFrom.SelectedValue = "en";
                transLangTo.SelectedValue = "sv";

                SqlDataReader rs = Db.rs("SELECT NewsCategoryID, NewsCategory FROM NewsCategory", "newsSqlConnection");
                while (rs.Read())
                {
                    NewsCategoryID.Items.Add(new ListItem(rs.GetString(1), rs.GetInt32(0).ToString()));
                }
                rs.Close();
                SourceType.Items.Add(new ListItem("< Favourites >", "-1"));
                SourceType.Items.Add(new ListItem("< All other >", "0"));
                LangID.Items.Add(new ListItem("< All >", "-1"));
                LangID.Items.Add(new ListItem("English", "1"));
                LangID.Items.Add(new ListItem("Swedish", "0"));
                loadSources();
                LinkLangID.Items.Add(new ListItem("Swedish", "0"));
                LinkLangID.Items.Add(new ListItem("English", "1"));

                Save.Text = "Save";
                DeleteArticle.Text = "Delete";
                Delete.Text = "Delete selected";
                Cancel.Text = "Cancel";
                AddArticle.Text = "Add";
            }

            Save.Click += new EventHandler(Save_Click);
            Delete.Click += new EventHandler(Delete_Click);
            Cancel.Click += new EventHandler(Cancel_Click);
            DeleteArticle.Click += new EventHandler(DeleteArticle_Click);
            AddArticle.Click += new EventHandler(AddArticle_Click);
            DeleteOldNews.Click += new EventHandler(DeleteOldNews_Click);
            LangID.SelectedIndexChanged += new EventHandler(LangID_SelectedIndexChanged);
            SourceType.SelectedIndexChanged += new EventHandler(SourceType_SelectedIndexChanged);
        }

        void SourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSources();
        }

        private void loadSources()
        {
            SourceID.Items.Clear();
            SourceID.Items.Add(new ListItem("< All >", "0"));
            string sql = "SELECT " +
                    "ns.sourceID, " +
                    "ns.source, " +
                    "COUNT(*) " +
                    "FROM NewsSource ns " +
                    "INNER JOIN NewsChannel nc ON ns.sourceID = nc.sourceID " +
                    "INNER JOIN NewsRSS nr ON nc.channelID = nr.channelID " +
                    "WHERE (nr.Deleted = 0 OR nr.Deleted IS NULL) AND " + (SourceType.SelectedValue == "0" ? "(ns.Favourite = 0 OR ns.Favourite IS NULL)" : "ns.Favourite = 1") + " " + (LangID.SelectedValue != "-1" ? "AND nc.LangID = " + LangID.SelectedValue + " " : "") +
                    "GROUP BY ns.sourceID, ns.source " +
                    "ORDER BY ns.source";
            //HttpContext.Current.Response.Write(sql);
            //HttpContext.Current.Response.End();
            SqlDataReader rs = Db.rs(sql, "newsSqlConnection");
            while (rs.Read())
            {
                SourceID.Items.Add(new ListItem(rs.GetString(1) + " (" + rs.GetInt32(2) + ")", rs.GetInt32(0).ToString()));
            }
            rs.Close();

        }

        void LangID_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSources();
        }

        void DeleteOldNews_Click(object sender, EventArgs e)
        {
            Db.exec("UPDATE NewsRSS SET Deleted = 1 WHERE DT < DATEADD(d,-7,GETDATE())", "newsSqlConnection");
        }

        void AddArticle_Click(object sender, EventArgs e)
        {
            LoadNews.Value = "1";
            NewsID.Value = "0";
        }

        void DeleteArticle_Click(object sender, EventArgs e)
        {
            Db.exec("UPDATE News SET Deleted = GETDATE() WHERE NewsID = " + NewsID.Value, "newsSqlConnection");
            LoadNews.Value = "0";
            NewsID.Value = "0";
        }

        void Cancel_Click(object sender, EventArgs e)
        {
            LoadNews.Value = "0";
            NewsID.Value = "0";
        }

        void Delete_Click(object sender, EventArgs e)
        {
            //string formKeys = "";
            //foreach (string s in HttpContext.Current.Request.Form.AllKeys)
            //    formKeys += s + " = " + (HttpContext.Current.Request.Form[s] != null ? HttpContext.Current.Request.Form[s] : "") + "\r\n";
            //HttpContext.Current.Response.Write(formKeys);

            SqlDataReader rs = Db.rs("SELECT TOP 50 n.rssID FROM NewsRSS n INNER JOIN NewsChannel c ON n.channelID = c.channelID INNER JOIN NewsSource s ON c.sourceID = s.SourceID WHERE n.Deleted = 0 " + (SourceID.SelectedValue == "-1" ? "AND s.Favourite = 1 " : (SourceID.SelectedValue != "0" ? "AND c.sourceID = " + Convert.ToInt32(SourceID.SelectedValue) + " " : "")) + "ORDER BY n.DT DESC", "newsSqlConnection");
            while (rs.Read())
            {
                if (HttpContext.Current.Request.Form["rssIDdelete"] != null)
                {
                    string s = "#" + HttpContext.Current.Request.Form["rssIDdelete"].ToString().Replace(" ", "").Replace(",", "#") + "#";
                    if (s.IndexOf("#" + rs.GetInt32(0) + "#") != -1)
                    {
                        Db.exec("UPDATE NewsRSS SET Deleted = 1 WHERE rssID = " + rs.GetInt32(0), "newsSqlConnection");
                    }
                }
            }
            rs.Close();
        }

        void Save_Click(object sender, EventArgs e)
        {
            int newsID = Convert.ToInt32(NewsID.Value);

            SqlDataReader rs;

            DateTime newsDT = DateTime.Now;
            try
            {
                newsDT = Convert.ToDateTime(DT.Text);
            }
            catch (Exception) { }
            string link = Link.Text.Replace("'", "''");
            if (link != "")
            {
                if (!link.StartsWith("http://") && !link.StartsWith("https://"))
                {
                    link = "http://" + link;
                }
                link = "'" + link + "'";
            }
            else
            {
                link = "NULL";
            }
            if (newsID == 0)
            {
                Db.exec("INSERT INTO News (NewsCategoryID,Headline,DT,Teaser,Body,Link,LinkText,LinkLangID,Published,DirectFromFeed,OnlyInCategory) VALUES (" +
                    "" + (NewsCategoryID.SelectedIndex != -1 ? Convert.ToInt32(NewsCategoryID.SelectedValue).ToString() : "NULL") + "," +
                    "'" + Header.Text.Replace("'", "''") + "'," +
                    "'" + newsDT.ToString("yyyy-MM-dd HH:mm") + "'," +
                    "'" + Teaser.Text.Replace("'", "''") + "'," +
                    "'" + Body.Text.Replace("'", "''") + "'," +
                    "" + link + "," +
                    "'" + LinkText.Text.Replace("'", "''") + "'," +
                    "" + (LinkLangID.SelectedIndex != -1 ? Convert.ToInt32(LinkLangID.SelectedValue) : 1) + "," +
                    "" + (Published.Checked ? "GETDATE()" : "NULL") + "," +
                    "" + (DirectFromFeed.Checked ? "1" : "NULL") + "," +
                    "" + (OnlyInCategory.Checked ? "1" : "NULL") + "" +
                    ")", "newsSqlConnection");
                rs = Db.rs("SELECT TOP 1 NewsID FROM News ORDER BY NewsID DESC", "newsSqlConnection");
                if (rs.Read())
                {
                    newsID = rs.GetInt32(0);
                }
                rs.Close();
            }
            else
            {
                Db.exec("UPDATE News SET " +
                    "NewsCategoryID = " + (NewsCategoryID.SelectedIndex != -1 ? Convert.ToInt32(NewsCategoryID.SelectedValue).ToString() : "NULL") + "," +
                    "Headline = '" + Header.Text.Replace("'", "''") + "'," +
                    "DT = '" + newsDT.ToString("yyyy-MM-dd HH:mm") + "'," +
                    "Teaser = '" + Teaser.Text.Replace("'", "''") + "'," +
                    "Body = '" + Body.Text.Replace("'", "''") + "'," +
                    "Link = " + link + "," +
                    "LinkText = '" + LinkText.Text.Replace("'", "''") + "'," +
                    "LinkLangID = " + (LinkLangID.SelectedIndex != -1 ? Convert.ToInt32(LinkLangID.SelectedValue) : 1) + "," +
                    "Published = " + (Published.Checked ? "GETDATE()" : "NULL") + ", " +
                    "DirectFromFeed = " + (DirectFromFeed.Checked ? "1" : "NULL") + ", " +
                    "OnlyInCategory = " + (OnlyInCategory.Checked ? "1" : "NULL") + " " +
                    "WHERE NewsID = " + newsID, "newsSqlConnection");
            }

            if (ImageID.PostedFile != null && ImageID.PostedFile.ContentLength != 0)
            {
                try
                {
                    bool allowedExtension = false;
                    string ext = ImageID.PostedFile.FileName.Substring(ImageID.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                    switch (ext)
                    {
                        case "gif":
                            allowedExtension = true;
                            break;
                        case "jpg":
                            allowedExtension = true;
                            break;
                    }
                    if (allowedExtension)
                    {
                        Db.exec("INSERT INTO NewsImage (Ext) VALUES ('" + ext + "')", "newsSqlConnection");
                        int newsImageID = 0;
                        rs = Db.rs("SELECT TOP 1 NewsImageID FROM NewsImage ORDER BY NewsImageID DESC", "newsSqlConnection");
                        if (rs.Read())
                        {
                            newsImageID = rs.GetInt32(0);
                        }
                        rs.Close();

                        System.IO.Directory.CreateDirectory(Server.MapPath("img/news/" + newsImageID));

                        string filename = getFilename(Header.Text, newsImageID);

                        ImageID.PostedFile.SaveAs(Server.MapPath("img/news/" + newsImageID + "/" + filename + "." + ext));

                        Db.exec("UPDATE NewsImage SET Filename = '" + filename + "' WHERE NewsImageID = " + newsImageID, "newsSqlConnection");
                        Db.exec("UPDATE News SET ImageID = " + newsImageID + " WHERE NewsID = " + newsID, "newsSqlConnection");
                    }
                }
                catch (Exception ex) { HttpContext.Current.Response.Write(ex.Message); }
            }
            else
            {
                rs = Db.rs("SELECT n.ImageID, i.Filename, i.Ext FROM News n INNER JOIN NewsImage i ON n.ImageID = i.NewsImageID WHERE n.NewsID = " + newsID, "newsSqlConnection");
                if (rs.Read() && !rs.IsDBNull(0))
                {
                    string filename = getFilename(Header.Text, rs.GetInt32(0));
                    System.IO.File.Move(Server.MapPath("img/news/" + rs.GetInt32(0) + "/" + rs.GetString(1) + "." + rs.GetString(2)), Server.MapPath("img/news/" + rs.GetInt32(0) + "/" + filename + "." + rs.GetString(2)));
                    Db.exec("UPDATE NewsImage SET Filename = '" + filename + "' WHERE NewsImageID = " + rs.GetInt32(0), "newsSqlConnection");
                }
                rs.Close();
            }
            if (TeaserImageID.PostedFile != null && TeaserImageID.PostedFile.ContentLength != 0)
            {
                try
                {
                    bool allowedExtension = false;
                    string ext = TeaserImageID.PostedFile.FileName.Substring(TeaserImageID.PostedFile.FileName.LastIndexOf(".") + 1).ToLower();
                    switch (ext)
                    {
                        case "gif":
                            allowedExtension = true;
                            break;
                        case "jpg":
                            allowedExtension = true;
                            break;
                    }
                    if (allowedExtension)
                    {
                        Db.exec("INSERT INTO NewsImage (Ext) VALUES ('" + ext + "')", "newsSqlConnection");
                        int newsImageID = 0;
                        rs = Db.rs("SELECT TOP 1 NewsImageID FROM NewsImage ORDER BY NewsImageID DESC", "newsSqlConnection");
                        if (rs.Read())
                        {
                            newsImageID = rs.GetInt32(0);
                        }
                        rs.Close();

                        System.IO.Directory.CreateDirectory(Server.MapPath("img/news/" + newsImageID));

                        string filename = getFilename(Header.Text, newsImageID);

                        TeaserImageID.PostedFile.SaveAs(Server.MapPath("img/news/" + newsImageID + "/" + filename + "." + ext));

                        Db.exec("UPDATE NewsImage SET Filename = '" + filename + "' WHERE NewsImageID = " + newsImageID, "newsSqlConnection");
                        Db.exec("UPDATE News SET TeaserImageID = " + newsImageID + " WHERE NewsID = " + newsID, "newsSqlConnection");
                    }
                }
                catch (Exception) { }
            }
            else
            {
                rs = Db.rs("SELECT n.TeaserImageID, i.Filename, i.Ext FROM News n INNER JOIN NewsImage i ON n.TeaserImageID = i.NewsImageID WHERE n.NewsID = " + newsID, "newsSqlConnection");
                if (rs.Read() && !rs.IsDBNull(0))
                {
                    string filename = getFilename(Header.Text, rs.GetInt32(0));
                    System.IO.File.Move(Server.MapPath("img/news/" + rs.GetInt32(0) + "/" + rs.GetString(1) + "." + rs.GetString(2)), Server.MapPath("img/news/" + rs.GetInt32(0) + "/" + filename + "." + rs.GetString(2)));
                    Db.exec("UPDATE NewsImage SET Filename = '" + filename + "' WHERE NewsImageID = " + rs.GetInt32(0), "newsSqlConnection");
                }
                rs.Close();
            }

            LoadNews.Value = "0";
            NewsID.Value = "0";
        }

        private string getFilename(string f, int q)
        {
            string filename = "";
            for (int i = 0; i < f.Length && i < 250; i++)
            {
                try
                {
                    int x = (int)f.ToLower()[i];
                    if (x >= 97 && x <= 122)
                    {
                        filename += f.ToLower()[i];
                    }
                }
                catch (Exception) { }
            }
            if (filename == "")
                filename = q.ToString();

            return filename;
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            string buffer = "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                "<tr>" +
                "<td valign=\"bottom\"><img src=\"verticalText.aspx?STR=Show\"></td>" +
                "<td valign=\"bottom\"><a href=\"Javascript:selectAll(true);\"><img src=\"verticalText.aspx?STR=Delete\" border=\"0\"></A></td>" +
                "<td valign=\"bottom\"><B>Date/time</B></td>" +
                "<td valign=\"bottom\"><B>Heading</B></td>" +
                //"<td valign=\"bottom\"><a href=\"Javascript:toggleLang();\"><img src=\"verticalText.aspx?STR=Language\" border=\"0\"></A></td>" +
                "<td valign=\"bottom\"><img src=\"verticalText.aspx?STR=Language\" border=\"0\"></td>" +
                "<td valign=\"bottom\"><B>Source</B></td>" +
                "</tr>" +
                "<tr><td bgcolor=\"#333333\" colspan=\"6\"><img src=\"img/null.gif\" width=\"1\" height=\"1\"></td></tr>";

            SqlDataReader rs = Db.rs("SELECT " +
                "TOP 25 " +
                "n.rssID, " +
                "n.link, " +
                "n.altlink, " +
                "n.title, " +
                "n.description, " +
                "n.dt, " +
                "c.langID, " +
                "s.sourceShort " +
                "FROM NewsRSS n " +
                "INNER JOIN NewsChannel c ON n.channelID = c.channelID " +
                "INNER JOIN NewsSource s ON c.sourceID = s.SourceID " +
                "WHERE n.Deleted = 0 AND " + (SourceType.SelectedValue == "-1" ? "s.Favourite = 1" : "(s.Favourite = 0 OR s.Favourite IS NULL)") + " " + (SourceID.SelectedValue != "0" ? "AND c.sourceID = " + Convert.ToInt32(SourceID.SelectedValue) + " " : "") + (LangID.SelectedValue != "-1" ? " AND c.LangID = " + LangID.SelectedValue : "") +
                "ORDER BY n.DT DESC", "newsSqlConnection");
            while (rs.Read())
            {
                buffer += "<tr id=\"h" + rs.GetInt32(0) + "\">" +
                    "<td valign=\"top\"><input type=\"radio\" value=\"" + rs.GetInt32(0) + "\" onclick=\"toggleNews();\" name=\"rssID\"" + (HttpContext.Current.Request.Form["rssID"] != null && ("#" + HttpContext.Current.Request.Form["rssID"].ToString().Replace(" ", "").Replace(",", "#") + "#").IndexOf("#" + rs.GetInt32(0) + "#") != -1 ? " checked" : "") + "></td>" +
                    "<td valign=\"top\"><input type=\"checkbox\" value=\"" + rs.GetInt32(0) + "\" name=\"rssIDdelete\"" + (HttpContext.Current.Request.Form["rssIDdelete"] != null && ("#" + HttpContext.Current.Request.Form["rssIDdelete"].ToString().Replace(" ", "").Replace(",", "#") + "#").IndexOf("#" + rs.GetInt32(0) + "#") != -1 ? " checked" : "") + "></td>" +
                    "<td valign=\"top\"><nobr>" + rs.GetDateTime(5).ToString("yyMMdd HH:mm") + "&nbsp;</nobr><input type=\"hidden\" name=\"rssDT" + rs.GetInt32(0) + "\" id=\"rssDT" + rs.GetInt32(0) + "\" value=\"" + rs.GetDateTime(5).ToString("yyyy-MM-dd HH:mm") + "\"></td>" +
                    "<td VALIGN=\"top\" id=\"t" + rs.GetInt32(0) + "\">" + rs.GetString(3) + "</td><td VALIGN=\"top\">&nbsp;<img id=\"i" + rs.GetInt32(0) + "\" src=\"img/LangID_" + rs.GetInt32(6) + ".gif\"/>&nbsp;</td>" +
                    "<td VALIGN=\"top\"><nobr><a href=\"" + rs.GetString(1) + "\" target=\"_blank\" class=\"noTD\">" + rs.GetString(7) + "</A></nobr></td>" +
                    "</tr>";
                buffer += "<tr id=\"d" + rs.GetInt32(0) + "\" style=\"display:none\"><td colspan=\"2\">&nbsp;</td><td colspan=\"4\" id=\"rssArt" + rs.GetInt32(0) + "\">" + rs.GetString(4) + "</td></tr>";
                buffer += "<tr id=\"l" + rs.GetInt32(0) + "\" style=\"display:none\"><td colspan=\"2\">&nbsp;</td><td colspan=\"4\"><A ID=\"rssLink" + rs.GetInt32(0) + "\" HREF=\"" + rs.GetString(1) + "\" TARGET=\"_blank\">" + rs.GetString(1) + "</A></td></tr>";
                if (!rs.IsDBNull(2) && rs.GetString(2) != "")
                {
                    buffer += "<tr id=\"a" + rs.GetInt32(0) + "\" style=\"display:none\"><td colspan=\"2\">&nbsp;</td><td colspan=\"4\"><A ID=\"rssLinkAlt" + rs.GetInt32(0) + "\" HREF=\"" + rs.GetString(2) + "\" TARGET=\"_blank\">" + rs.GetString(2) + "</A></td></tr>";
                }
            }
            rs.Close();
            buffer += "</table>";

            Right.Controls.Add(new LiteralControl(buffer));

            buffer = "";

            if (DeleteImage.Value == "1" || DeleteImage.Value == "2")
            {
                Db.exec("UPDATE News SET " + (DeleteImage.Value == "1" ? "Teaser" : "") + "ImageID = NULL WHERE NewsID = " + Convert.ToInt32(NewsID.Value), "newsSqlConnection");
                DeleteImage.Value = "0";
            }

            if (LoadNews.Value == "1")
            {
                ChangeNews.Visible = true;
                NewsListContainer.Visible = false;

                if (NewsID.Value != "0")
                {
                    DeleteArticle.Visible = true;

                    rs = Db.rs("SELECT n.NewsCategoryID, n.Headline, n.DT, n.Teaser, n.Body, n.Link, n.LinkText, n.LinkLangID, n.TeaserImageID, ti.Filename, ti.Ext, n.ImageID, i.Filename, i.Ext, n.Published, n.DirectFromFeed, n.OnlyInCategory FROM News n LEFT OUTER JOIN NewsImage ti ON n.TeaserImageID = ti.NewsImageID LEFT OUTER JOIN NewsImage i ON n.ImageID = i.NewsImageID WHERE n.NewsID = " + Convert.ToInt32(NewsID.Value), "newsSqlConnection");
                    if (rs.Read())
                    {
                        if (!rs.IsDBNull(0))
                            if (NewsCategoryID.Items.FindByValue(rs.GetInt32(0).ToString()) != null)
                                NewsCategoryID.SelectedValue = rs.GetInt32(0).ToString();
                        Header.Text = rs.GetString(1);
                        DT.Text = rs.GetDateTime(2).ToString("yyyy-MM-dd HH:mm");
                        Teaser.Text = rs.GetString(3);
                        Body.Text = rs.GetString(4);
                        Link.Text = (!rs.IsDBNull(5) ? rs.GetString(5) : "");
                        LinkText.Text = rs.GetString(6);
                        if (!rs.IsDBNull(7))
                            if (LinkLangID.Items.FindByValue(rs.GetInt32(7).ToString()) != null)
                                LinkLangID.SelectedValue = rs.GetInt32(7).ToString();
                        if (!rs.IsDBNull(8))
                            TeaserImage.Text = "<img onmouseover=\"document.getElementById('img1').innerHTML='<img src=\\'' + this.src + '\\'/>';document.getElementById('img1').style.display='';\" onmouseout=\"document.getElementById('img1').style.display='none';\" border=\"1\" src=\"img/news/" + rs.GetInt32(8) + "/" + rs.GetString(9) + "." + rs.GetString(10) + "\" height=\"12\" />&nbsp;[<A class=\"small\" HREF=\"JavaScript:document.forms[0].DeleteImage.value=1;document.forms[0].submit();\">delete</A>]";
                        else
                            TeaserImage.Text = "";
                        if (!rs.IsDBNull(11))
                            Image.Text = "<img onmouseover=\"document.getElementById('img2').innerHTML='<img src=\\'' + this.src + '\\'/>';document.getElementById('img2').style.display='';\" onmouseout=\"document.getElementById('img2').style.display='none';\" border=\"1\" src=\"img/news/" + rs.GetInt32(11) + "/" + rs.GetString(12) + "." + rs.GetString(13) + "\" height=\"12\" />&nbsp;[<A class=\"small\" HREF=\"JavaScript:document.forms[0].DeleteImage.value=2;document.forms[0].submit();\">delete</A>]";
                        else
                            Image.Text = "";
                        Published.Checked = (!rs.IsDBNull(14));
                        DirectFromFeed.Checked = (!rs.IsDBNull(15) && rs.GetInt32(15) == 1);
                        OnlyInCategory.Checked = (!rs.IsDBNull(16) && rs.GetInt32(16) == 1);
                    }
                    rs.Close();
                }
                else
                {
                    NewsCategoryID.SelectedIndex = -1;
                    Header.Text = "";
                    DT.Text = "";
                    Teaser.Text = "";
                    Body.Text = "";
                    Link.Text = "";
                    LinkText.Text = "Till artikeln";
                    LinkLangID.SelectedIndex = -1;
                    Image.Text = "";
                    TeaserImage.Text = "";
                    Published.Checked = false;
                    DirectFromFeed.Checked = false;
                    OnlyInCategory.Checked = false;
                }
            }
            else
            {
                DeleteArticle.Visible = false;
                ChangeNews.Visible = false;
                NewsListContainer.Visible = true;

                buffer += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                    "<tr><td valign=\"bottom\"><B>Date/time</B>&nbsp;</td><td valign=\"bottom\">&nbsp;<img src=\"verticalText.aspx?STR=Published\">&nbsp;</td><td valign=\"bottom\"><B>Heading</B></td></tr>" +
                    "<tr><td bgcolor=\"#333333\" colspan=\"3\"><img src=\"img/null.gif\" width=\"1\" height=\"1\"></td></tr>";
                rs = Db.rs("SELECT TOP 50 NewsID, Headline, DT, Published FROM News WHERE Deleted IS NULL ORDER BY DT DESC", "newsSqlConnection");
                while (rs.Read())
                {
                    buffer += "<TR id=\"row" + rs.GetInt32(0) + "\">" +
                        "<td valign=\"top\"><nobr>" + rs.GetDateTime(2).ToString("yyMMdd HH:mm") + "</nobr>&nbsp;&nbsp;</td>" +
                        "<td valign=\"top\">&nbsp;&nbsp;" + (!rs.IsDBNull(3) ? "X" : "") + "&nbsp;&nbsp;</td>" +
                        "<td valign=\"top\"><A onmouseout=\"document.getElementById('row" + rs.GetInt32(0) + "').style.backgroundColor='#ffffff';\" onmouseover=\"document.getElementById('row" + rs.GetInt32(0) + "').style.backgroundColor='#f2f2f2';\" HREF=\"JavaScript:document.forms[0].LoadNews.value=1;document.forms[0].NewsID.value=" + rs.GetInt32(0) + ";document.forms[0].submit();\" class=\"noTD\">" + (rs.GetString(1) == "" ? "[ no heading ]" : rs.GetString(1)) + "</A></td>" +
                        "</TR>";
                }
                rs.Close();
                buffer += "</table>";
            }

            NewsList.Text = buffer;

            Page.RegisterStartupScript("TOGGLE", "<script language=\"JavaScript\">toggleNews();</script>");
        }
    }
}