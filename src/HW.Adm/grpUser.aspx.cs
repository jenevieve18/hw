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
using HW.Core.Helpers;

public partial class grpUser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int cx = 0;
            SqlDataReader rs = Db.rs("SELECT " +
                "u.SponsorAdminID, " +
                "u.Usr, " +
                "s.Sponsor, " +
                "LEFT(REPLACE(CONVERT(VARCHAR(255),u.SponsorAdminKey),'-',''),8), " +
                "s.SponsorID, " +
                "(SELECT COUNT(*) FROM SponsorAdminSession sas WHERE u.SponsorAdminID = sas.SponsorAdminID) AS CX, " +
                "(SELECT SUM(DATEDIFF(minute, sas.DT, ISNULL(sas.EndDT, DATEADD(minute, 1, sas.DT)))) FROM SponsorAdminSession sas WHERE u.SponsorAdminID = sas.SponsorAdminID) AS TX, " +
                "u.ReadOnly, " +
                "u.Email " +
                "FROM Sponsor s " +
                "INNER JOIN SponsorAdmin u ON s.SponsorID = u.SponsorID " +
                "ORDER BY s.Sponsor, u.Usr");
            while (rs.Read())
            {
                List.Text += "<TR" + (cx++ % 2 == 0 ? " BGCOLOR=\"#F2F2F2\"" : "") + ">" +
                    //"<TD><A HREF=\"grpUserSetup.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&SAID=" + rs.GetInt32(0) + "\">" + rs.GetString(1) + "</A>&nbsp;&nbsp;&nbsp;</TD>" +
                    "<TD><A HREF=\"grpUserSetup.aspx?Rnd=" + (new Random(unchecked((int)DateTime.Now.Ticks))).Next() + "&SAID=" + rs.GetInt32(0) + "\">" + (DbHelper.GetString(rs, 1) != "" ? DbHelper.GetString(rs, 1) : DbHelper.GetString(rs, 8)) + "</A>&nbsp;&nbsp;&nbsp;</TD>" +
                    "<TD>" + rs.GetString(2) + "&nbsp;&nbsp;</TD>" +
                    "<TD>" +
//                	"<img width=\"16\" src=\"" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/img/" + (rs.IsDBNull(7) ? "null" : "locked") + ".gif\"/>" +
                	"<img width=\"16\" src=\"" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/img/" + (DbHelper.GetInt32(rs, 7) != 1 ? "null" : "locked") + ".gif\"/>" +
                        "&nbsp;&nbsp;" +
                        "<A HREF=\"" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURL"] + "/default.aspx?SAKEY=" + rs.GetString(3) + rs.GetInt32(4).ToString() + "\" TARGET=\"_blank\">Log in (" + System.Configuration.ConfigurationManager.AppSettings["hwGrpURLtext"] + ")</A>" +
                        "&nbsp;" +
                        "<A HREF=\"" + System.Configuration.ConfigurationManager.AppSettings["hwStageGrpURL"] + "/default.aspx?SAKEY=" + rs.GetString(3) + rs.GetInt32(4).ToString() + "\" TARGET=\"_blank\">(" + System.Configuration.ConfigurationManager.AppSettings["hwStageGrpURLtext"] + ")</A>" +
                        "&nbsp;&nbsp;" +
                    "</TD>" +
                    "<TD>" + rs.GetInt32(5) + "&nbsp;&nbsp;</TD>" +
                    "<TD>" + (rs.IsDBNull(6) ? "" : rs.GetInt32(6).ToString() + " min") + "&nbsp;&nbsp;</TD>" +
                    "</TR>";
            }
            rs.Close();
        }
    }
}
