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
    public partial class stats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            int totalUsers = 0;
            SqlDataReader rs = Db.rs("SELECT COUNT(*) FROM [User]");
            if (rs.Read())
            {
                totalUsers = rs.GetInt32(0);
            }
            rs.Close();

            rs = Db.rs("SELECT BQID, Internal FROM BQ WHERE Type IN (1,7) AND IncludeInDemographics = 1");
            while (rs.Read())
            {
                SqlDataReader rs2 = Db.rs("SELECT COUNT(*), BA.Internal FROM [User] u INNER JOIN UserProfile p ON u.UserProfileID = p.UserProfileID INNER JOIN UserProfileBQ b ON p.UserProfileID = b.UserProfileID AND b.BQID = " + rs.GetInt32(0) + " INNER JOIN BA ON BA.BAID = b.ValueInt GROUP BY BA.Internal");
                while (rs2.Read())
                {
                    StatsRight.Text += "<TR><TD>" + rs.GetString(1) + ":&nbsp;" + rs2.GetString(1) + "&nbsp;&nbsp;</TD><TD>" + Math.Round((double)rs2.GetInt32(0) / (double)totalUsers * 100, 1).ToString().Replace(".", ",") + "%</TD></TR>";
                }
                rs2.Close();
            }
            rs.Close();

            Stats.Text = "<TR><TD><B>Year/month</B>&nbsp;&nbsp;</TD><TD><B>Created users</B></TR>";
            int cx = 0;
            rs = Db.rs("SELECT YEAR(Created), MONTH(Created), COUNT(*) FROM [User] GROUP BY YEAR(Created), MONTH(Created) ORDER BY YEAR(Created), MONTH(Created)");
            while (rs.Read())
            {
                cx += rs.GetInt32(2);
                Stats.Text += "<TR><TD>" + rs.GetInt32(0) + "/" + rs.GetInt32(1) + "&nbsp;&nbsp;</TD><TD>" + rs.GetInt32(2) + "</TD></TR>";
            }
            rs.Close();
            Stats.Text += "<TR><TD><B>Total</B>&nbsp;&nbsp;</TD><TD><B>" + cx + "</B></TD></TR>";

            Stats.Text += "<TR><TD COLSPAN=\"2\">&nbsp;</TD></TR>";

            Stats.Text += "<TR><TD><B>Visit count</B>&nbsp;&nbsp;</TD><TD><B>Users</B></TR>";
            rs = Db.rs("SELECT tmp.CX, COUNT(*) FROM (SELECT COUNT(*) AS CX FROM [User] u INNER JOIN Session s ON u.UserID = s.UserID GROUP BY u.UserID) tmp GROUP BY tmp.CX ORDER BY tmp.CX");
            while (rs.Read())
            {
                Stats.Text += "<TR><TD>" + rs.GetInt32(0) + "&nbsp;&nbsp;</TD><TD>" + rs.GetInt32(1) + "</TD></TR>";
            }
            rs.Close();

            Stats.Text += "<TR><TD COLSPAN=\"2\">&nbsp;</TD></TR>";

            Stats.Text += "<TR><TD><B>Year/month</B>&nbsp;&nbsp;</TD><TD><B>Website visits (incl. bots)</B></TR>";
            cx = 0;
            rs = Db.rs("SELECT YEAR(DT), MONTH(DT), COUNT(*) FROM [Session] GROUP BY YEAR(DT), MONTH(DT) ORDER BY YEAR(DT), MONTH(DT)");
            while (rs.Read())
            {
                cx += rs.GetInt32(2);
                Stats.Text += "<TR><TD>" + rs.GetInt32(0) + "/" + rs.GetInt32(1) + "&nbsp;&nbsp;</TD><TD>" + rs.GetInt32(2) + "</TD></TR>";
            }
            rs.Close();
            Stats.Text += "<TR><TD><B>Total</B>&nbsp;&nbsp;</TD><TD><B>" + cx + "</B></TD></TR>";

            Stats.Text += "<TR><TD COLSPAN=\"2\">&nbsp;</TD></TR>";

            Stats.Text += "<TR><TD><B>Year/month</B>&nbsp;&nbsp;</TD><TD><B>Website visits (excl. bots)</B></TR>";
            cx = 0;
            rs = Db.rs("SELECT YEAR(DT), MONTH(DT), COUNT(*) FROM [Session] WHERE UserAgent IS NOT NULL AND UserAgent NOT LIKE '%bot%' GROUP BY YEAR(DT), MONTH(DT) ORDER BY YEAR(DT), MONTH(DT)");
            while (rs.Read())
            {
                cx += rs.GetInt32(2);
                Stats.Text += "<TR><TD>" + rs.GetInt32(0) + "/" + rs.GetInt32(1) + "&nbsp;&nbsp;</TD><TD>" + rs.GetInt32(2) + "</TD></TR>";
            }
            rs.Close();
            Stats.Text += "<TR><TD><B>Total</B>&nbsp;&nbsp;</TD><TD><B>" + cx + "</B></TD></TR>";
        }
    }
}