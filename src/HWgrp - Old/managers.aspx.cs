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

public partial class managers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.QueryString["Delete"] != null)
        {
            Db.exec("UPDATE SponsorAdmin SET SponsorID = -ABS(SponsorID), Usr = Usr+'DELETED' WHERE SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Request.QueryString["Delete"]) + " " +
                "AND SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
        }
        if (Convert.ToInt32(HttpContext.Current.Session["SponsorID"]) != 0)
        {
            Managers.Text = " <tr><td><B>Name</B>&nbsp;&nbsp;</td><td><b>Roles</b></td></tr>";
            SqlDataReader rs = Db.rs("SELECT " +
                "sa.SponsorAdminID, " +
                "sa.Usr, " +
                "sa.Name, " +
                "sa.ReadOnly " +
                "FROM SponsorAdmin sa " +
                "WHERE (sa.SponsorAdminID <> " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + " OR sa.SuperUser = 1) " +
                (HttpContext.Current.Session["SponsorAdminID"].ToString() != "-1" ?     // Only see managers with no department access (yet) or with access to at least one of "my" departments
                "AND ((SELECT COUNT(*) FROM SponsorAdminDepartment sad WHERE sad.SponsorAdminID = sa.SponsorAdminID) = 0 OR (SELECT COUNT(*) FROM SponsorAdminDepartment sad INNER JOIN SponsorAdminDepartment sad2 ON sad.DepartmentID = sad2.DepartmentID WHERE sad.SponsorAdminID = sa.SponsorAdminID AND sad2.SponsorAdminID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorAdminID"]) + ") > 0) " : "") +
                "AND sa.SponsorID = " + Convert.ToInt32(HttpContext.Current.Session["SponsorID"]));
            while (rs.Read())
            {
                Managers.Text += "<TR><TD>" + (rs.IsDBNull(3) ? "" : "<img src=\"img/locked.gif\"/> ") + "<A HREF=\"managerSetup.aspx?SAID=" + rs.GetInt32(0) + "\">" + (rs.IsDBNull(2) || rs.GetString(2) == "" ? (rs.IsDBNull(1) || rs.GetString(1) == "" ? "&gt; empty &lt;" : rs.GetString(1)) : rs.GetString(2)) + "</A>&nbsp;&nbsp;</TD><TD>";

                int cx = 0;
                SqlDataReader rs2 = Db.rs("SELECT " +
                "mf.ManagerFunction " +
                "FROM SponsorAdminFunction saf " +
                "INNER JOIN ManagerFunction mf ON saf.ManagerFunctionID = mf.ManagerFunctionID " +
                "WHERE saf.SponsorAdminID = " + rs.GetInt32(0) + " " +
                "ORDER BY mf.ManagerFunctionID");
                while (rs2.Read())
                {
                    Managers.Text += (cx++ > 0 ? ", " : "") + rs2.GetString(0);
                }
                rs2.Close();

                Managers.Text += "</TD><TD><A HREF=\"javascript:if(confirm('Are you sure you want to delete this manager?')){location.href='managers.aspx?Delete=" + rs.GetInt32(0) + "';}\"><img src=\"img/deltoolsmall.gif\" border=\"0\"/></TD></TR>";
            }
            rs.Close();
        }
        else
        {
            HttpContext.Current.Response.Redirect("default.aspx", true);
        }
    }
}
