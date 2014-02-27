using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HW.Core.Helpers;
using System.Data.SqlClient;

namespace HW.Adm
{
    public partial class exerciseCategorySetup : System.Web.UI.Page
    {
        int eaid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Save.Click += new EventHandler(Save_Click);
            eaid = (HttpContext.Current.Request.QueryString["ExerciseCategoryID"] != null ? Convert.ToInt32(HttpContext.Current.Request.QueryString["ExerciseCategoryID"]) : 0);

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
                    rs = Db.rs("SELECT eal.ExerciseCategory, eal.Lang FROM ExerciseCategoryLang eal WHERE eal.ExerciseCategoryID = " + eaid);
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
                Db.exec("INSERT INTO ExerciseCategory DEFAULT VALUES");
                rs = Db.rs("SELECT TOP 1 ExerciseCategoryID FROM ExerciseCategory ORDER BY ExerciseCategoryID DESC");
                if (rs.Read())
                {
                    eaid = rs.GetInt32(0);
                }
                rs.Close();
                Db.exec("UPDATE ExerciseCategory SET ExerciseCategorySortOrder = ExerciseCategoryID WHERE ExerciseCategorySortOrder IS NULL");
            }
            rs = Db.rs("SELECT l.LangID, el.ExerciseCategoryLangID FROM Lang l LEFT OUTER JOIN ExerciseCategoryLang el ON l.LangID = el.Lang AND el.ExerciseCategoryID = " + eaid);
            while (rs.Read())
            {
                if (!rs.IsDBNull(1))
                {
                    if (((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text != "")
                    {
                        Db.exec("UPDATE ExerciseCategoryLang SET ExerciseCategory = '" + ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text.Replace("'", "''") + "' WHERE ExerciseCategoryLangID = " + rs.GetInt32(1));
                    }
                    else
                    {
                        Db.exec("UPDATE ExerciseCategoryLang SET ExerciseCategoryID = -ABS(ExerciseCategoryID) WHERE ExerciseCategoryID = " + eaid + " AND Lang = " + rs.GetInt32(0));
                    }
                }
                else
                {
                    if (((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text != "")
                    {
                        Db.exec("INSERT INTO ExerciseCategoryLang (ExerciseCategoryID, Lang, ExerciseCategory) VALUES (" + eaid + "," + rs.GetInt32(0) + "," +
                            "'" + ((TextBox)ExerciseLang.FindControl("ExerciseLang" + rs.GetInt32(0))).Text.Replace("'", "''") + "'" +
                            ")");
                    }
                }
            }
            rs.Close();

            HttpContext.Current.Response.Redirect("exerciseCategorySetup.aspx?ExerciseCategoryID=" + eaid + "&Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next(), true);
        }
    }
}