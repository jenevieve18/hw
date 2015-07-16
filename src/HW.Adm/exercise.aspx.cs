using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class exercise : System.Web.UI.Page
{
    protected void buttonSaveExerciseStatusClick(object sender, EventArgs e)
    {
        string[] statuses = Request.Form.GetValues("exercise-status");
        foreach (var s in statuses)
        {
            string[] x = s.Split('|');
            string query = string.Format(@"
update exercise set status = {0}
where exerciseid = {1}",
                       x[1],
                       x[0]
            );
            Db.exec(query);
        }
        Response.Redirect("exercise.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next());
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlDataReader rs, rs2;

        if (HttpContext.Current.Request.QueryString["MoveUp"] != null)
        {
            rs = Db.rs("SELECT " +
                    "ExerciseID, " +
                    "ExerciseAreaID, " +
                    "ExerciseSortOrder, " +
                    "Minutes " +
                    "FROM Exercise " +
                    "WHERE ExerciseID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["MoveUp"]));
            if(rs.Read())
            {
                rs2 = Db.rs("SELECT TOP 1 " +
                    "ExerciseID, " +
                    "ExerciseSortOrder " +
                    "FROM Exercise " +
                    "WHERE ExerciseSortOrder < " + rs.GetInt32(2) + " " +
                    "AND ExerciseAreaID = " + rs.GetInt32(1) + " " +
                    "ORDER BY ExerciseSortOrder DESC");
                if (rs2.Read())
                {
                    Db.exec("UPDATE Exercise SET ExerciseSortOrder = " + rs.GetInt32(2) + " WHERE ExerciseID = " + rs2.GetInt32(0));
                    Db.exec("UPDATE Exercise SET ExerciseSortOrder = " + rs2.GetInt32(1) + " WHERE ExerciseID = " + rs.GetInt32(0));
                }
                rs2.Close();
            }
            rs.Close();

            HttpContext.Current.Response.Redirect("exercise.aspx", true);
        }
        if (HttpContext.Current.Request.QueryString["MoveUpArea"] != null)
        {
            rs = Db.rs("SELECT " +
                    "ExerciseAreaID, " +
                    "ExerciseAreaSortOrder " +
                    "FROM ExerciseArea " +
                    "WHERE ExerciseAreaID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["MoveUpArea"]));
            if (rs.Read())
            {
                rs2 = Db.rs("SELECT TOP 1 " +
                    "ExerciseAreaID, " +
                    "ExerciseAreaSortOrder " +
                    "FROM ExerciseArea " +
                    "WHERE ExerciseAreaSortOrder < " + rs.GetInt32(1) + " " +
                    "ORDER BY ExerciseAreaSortOrder DESC");
                if (rs2.Read())
                {
                    Db.exec("UPDATE ExerciseArea SET ExerciseAreaSortOrder = " + rs.GetInt32(1) + " WHERE ExerciseAreaID = " + rs2.GetInt32(0));
                    Db.exec("UPDATE ExerciseArea SET ExerciseAreaSortOrder = " + rs2.GetInt32(1) + " WHERE ExerciseAreaID = " + rs.GetInt32(0));
                }
                rs2.Close();
            }
            rs.Close();

            HttpContext.Current.Response.Redirect("exercise.aspx", true);
        }

        int cx = 0;
        int EAID = 0;
        rs = Db.rs("SELECT DISTINCT " +
            "e.ExerciseID, " +
            "e.ExerciseAreaID, " +
            "(SELECT TOP 1 el.Exercise FROM ExerciseLang el WHERE el.ExerciseID = e.ExerciseID), " +
            "(SELECT TOP 1 eal.ExerciseArea FROM ExerciseAreaLang eal WHERE eal.ExerciseAreaID = e.ExerciseAreaID), " +
            "(SELECT COUNT(*) FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl2 ON es.ExerciseVariantLangID = evl2.ExerciseVariantLangID INNER JOIN ExerciseVariant ev2 ON evl2.ExerciseVariantID = ev2.ExerciseVariantID WHERE ev2.ExerciseID = e.ExerciseID), " +
            "(SELECT COUNT(*) FROM ExerciseVariant ev3 INNER JOIN ExerciseVariantLang evl3 ON ev3.ExerciseVariantID = evl3.ExerciseVariantID WHERE ev3.ExerciseID = e.ExerciseID), " +
            "(SELECT COUNT(DISTINCT es.UserID) FROM ExerciseStats es INNER JOIN ExerciseVariantLang evl2 ON es.ExerciseVariantLangID = evl2.ExerciseVariantLangID INNER JOIN ExerciseVariant ev2 ON evl2.ExerciseVariantID = ev2.ExerciseVariantID WHERE ev2.ExerciseID = e.ExerciseID), " +
            "ea.ExerciseAreaSortOrder, " +
            "e.ExerciseSortOrder, " +
            "e.Minutes, " +
            "e.RequiredUserLevel, " +
            "(SELECT TOP 1 eal.ExerciseCategory FROM ExerciseCategoryLang eal WHERE eal.ExerciseCategoryID = e.ExerciseCategoryID), " +
            "e.ExerciseCategoryID, " + 
            "e.Status " +
            "FROM Exercise e " +
            "INNER JOIN ExerciseArea ea ON e.ExerciseAreaID = ea.ExerciseAreaID " +
            "ORDER BY ea.ExerciseAreaSortOrder, e.ExerciseSortOrder");
        while (rs.Read())
        {
            if (EAID != rs.GetInt32(1))
            {
                Exercise.Text += "<TR><TD COLSPAN=\"5\">&nbsp;</TD></TR>";
                Exercise.Text += "<TR><TD COLSPAN=\"5\">" + (EAID != 0 ? "<A HREF=\"exercise.aspx?MoveUpArea=" + rs.GetInt32(1) + "\"><img src=\"img/UpToolSmall.gif\" border=\"0\"></A>" : "<img src=\"img/null.gif\" width=\"10\" height=\"0\" border=\"0\">") + "&nbsp;<B><A HREF=\"exerciseAreaSetup.aspx?ExerciseAreaID=" + rs.GetInt32(1) + "\">" + rs.GetString(3) + "</A></B></TD></TR>";
                EAID = rs.GetInt32(1);
                cx = 0;
            }
            string todo = "<select id='exercise-status' name='exercise-status'>";
            var categories = new[] {
                new { id = 0, name = "Open" },
                new { id = 1, name = "In progress" },
                new { id = 2, name = "Done" }
            };
            foreach (var t in categories)
            {
                string selected = (!rs.IsDBNull(13) && rs.GetInt32(13) == t.id) ? " selected" : ""; ;
                todo += string.Format("<option value='{2}|{0}'{3}>{1}", t.id.ToString(), t.name, rs.GetInt32(0), selected);
                todo += "</option>";
            }
            todo += "</select>";
            Exercise.Text += "<TR" + (cx % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + ">" +
                "<td>" + todo + "</td>" +
                "<TD>&nbsp;&nbsp;&nbsp;" + (cx != 0 ? "<A HREF=\"exercise.aspx?MoveUp=" + rs.GetInt32(0) + "\"><img src=\"img/UpToolSmall.gif\" border=\"0\"></A>" : "<img src=\"img/null.gif\" width=\"10\" height=\"0\" border=\"0\">") + (rs.IsDBNull(11) || rs.IsDBNull(12) ? "" : "<A" + (rs.GetInt32(5) == 0 ? " STYLE=\"color:#cccccc;\" " : "") + " HREF=\"exerciseCategorySetup.aspx?ExerciseCategoryID=" + rs.GetInt32(12) + "\">" + rs.GetString(11) + "</A> &gt; ") + "&nbsp;<A" + (rs.GetInt32(5) == 0 ? " STYLE=\"color:#cccccc;\" " : "") + " HREF=\"exerciseSetup.aspx?ExerciseID=" + rs.GetInt32(0) + "\">" + rs.GetString(2) + "</A>&nbsp;&nbsp;</TD>" +
                "<TD>" + rs.GetInt32(4) + "&nbsp;&nbsp;</TD>" +
                "<TD>" + rs.GetInt32(6) + "&nbsp;&nbsp;</TD>" +
                "<TD>" + (rs.GetInt32(5) > 0 ? "Yes" : "No") + "&nbsp;&nbsp;</TD>" +
                "<TD><A HREF=\"JavaScript:void(window.clipboardData.clearData());void(window.clipboardData.setData(\'Text','&lt;EXID" + rs.GetInt32(0).ToString().PadLeft(3, '0') + "&gt;\'));\">&lt;EXID" + rs.GetInt32(0).ToString().PadLeft(3, '0') + "&gt;</A>&nbsp;&nbsp;</TD>" +
                "<TD>" + (rs.IsDBNull(9) ? "N/A" : rs.GetInt32(9).ToString()) + "&nbsp;&nbsp;</TD>" +
                "<TD>" + (rs.IsDBNull(10) || rs.GetInt32(10) == 0 ? "End user" : "Manager") + "&nbsp;&nbsp;</TD>" +
                "<TD>";
            rs2 = Db.rs("SELECT Lang FROM ExerciseLang WHERE ExerciseID = " + rs.GetInt32(0));
            while (rs2.Read())
            {
                Exercise.Text += "<img src=\"img/langID_" + rs2.GetInt32(0) + ".gif\"/>";
            }
            rs2.Close();
            Exercise.Text += "</TD>" +
                "</TR>";
            cx++;
        }
        rs.Close();
    }
}
